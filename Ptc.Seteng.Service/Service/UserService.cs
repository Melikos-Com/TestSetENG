using Newtonsoft.Json;
using Ptc.AspnetMvc.Authentication;
using Ptc.AspnetMvc.Authentication.Repository;
using Ptc.AspnetMvc.Authentication.Service;
using Ptc.Logger;
using Ptc.Logger.Service;
using Ptc.Seteng.Factory;
using Ptc.Seteng.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Ptc.Seteng.Service
{
    public class UserService : IClientUserService, IAspUserService
    {
        private readonly ISystemLog _logger;
        private readonly IUserFactory _userFactory;
        private readonly IVendorFactory _venderFactory;
        private readonly IBaseRepository<DataBase.TUSRMST, Tusrmst> _userRepo;
        private readonly IBaseRepository<DataBase.TVENDER, Tvender> _venderRepo;
        private readonly IBaseRepository<DataBase.TSYSROL, RoleAuth> _aspRoleRepo;
        private readonly IBaseRepository<DataBase.TVenderTechnician, TvenderTechnician> _technicianRepo;
        private readonly IBaseRepository<DataBase.TUSRVENRELATION, TUSRVENRELATION> _tusrvenrelationRepo;

        public UserService(ISystemLog Logger,
                           IUserFactory UserFactory,
                           IVendorFactory VendorFactory,
                           IBaseRepository<DataBase.TUSRMST, Tusrmst> UserRepo,
                           IBaseRepository<DataBase.TVENDER, Tvender> VenderRepo,
                           IBaseRepository<DataBase.TSYSROL, RoleAuth> AspRoleRepo,
                           IBaseRepository<DataBase.TVenderTechnician, TvenderTechnician> TechnicianRepo,
                           IBaseRepository<DataBase.TUSRVENRELATION, TUSRVENRELATION> tusrvenrelationRepo)
        {
            _logger = Logger;
            _userRepo = UserRepo;
            _venderFactory = VendorFactory;
            _venderRepo = VenderRepo;
            _userFactory = UserFactory;
            _aspRoleRepo = AspRoleRepo;
            _technicianRepo = TechnicianRepo;
            _tusrvenrelationRepo = tusrvenrelationRepo;
        }

        /// <summary>
        /// [client]技師登入
        /// </summary>
        /// <param name="Account"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public TvenderTechnician VendorLogin(string Account, string Password, string UUID)
        {
            _logger.Info($"APP登入-帳號:{Account},密碼:{Password},UUID:{UUID}");

            #region 取得技師資訊

            var con = new Conditions<DataBase.TVenderTechnician>();

            con.And(x => x.Account == Account);
            con.And(x => x.Password == Password);
            con.Include(x => x.TVENDER);
            con.And(x => x.TVENDER.Comp_Cd == "711");       
            TvenderTechnician result = _technicianRepo.Get(con);

            #endregion 檢核技師相關欄位

            #region 相關驗證

            if (result == null)
            {
                _logger.Info("帳號或密碼不存在");
                throw new NullReferenceException($"登入失敗");
            }


            if (!result.Enable)
            {
                _logger.Info("帳號尚未啟用");
                throw new InvalidProgramException($"帳號尚未啟用");
            }

            var ConTusrven = new Conditions<DataBase.TUSRVENRELATION>();
            ConTusrven.And(x => x.Comp_Cd == result.CompCd);
            ConTusrven.And(x => x.Vender_Cd == result.VenderCd);
            var Tusrven = _tusrvenrelationRepo.Get(ConTusrven);

            if (Tusrven == null)
            {
                _logger.Info("廠商未服務711");
                throw new InvalidProgramException($"廠商未服務711");
            }

            //2018/06/19因為廠商輸入密碼錯誤6次會造成帳號被關閉且技師無法登入APP，經與玉萍討論，在決定檢核廠商的規則前，暫時不進行檢核 by 天生
            //if (_venderFactory.CheckVender(result.CompCd, Tusrven.User_Id) == false)
            //{
            //    _logger.Info("廠商已被關閉");
            //    throw new InvalidProgramException($"廠商已被關閉");
            //}

            if (!string.IsNullOrEmpty(result.DeviceID) && result.DeviceID != UUID)
            {
                _logger.Info("UUID重複");
                throw new ArgumentOutOfRangeException($"已經有其他設備登入過，是否強制登入?");
            }


            #endregion

            #region 寫入相關資訊

            _logger.Info($"APP登入-準備更新資訊");

            con.Allow(x => x.LastLoginTime);
            con.Allow(x => x.DeviceID);

            if (!_technicianRepo.Update(con, new TvenderTechnician()
            {
                Account = Account,
                Password = Password,
                DeviceID = UUID,
                LastLoginTime = DateTime.Now
            }))
                throw new Exception("登入失敗");

            #endregion

            return result;
        }
        /// <summary>
        /// [client]技師登出
        /// </summary>
        /// <param name="Account"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public Boolean VendorLogout(string Account, string Password)
        {
            _logger.Info($"APP登出-帳號:{Account},密碼:{Password}");

            #region 取得技師資訊

            var con = new Conditions<DataBase.TVenderTechnician>();

            con.And(x => x.Account == Account);
            con.And(x => x.Password == Password);

            //con.Include(x => x.TVENDER.TCMPDAT);

            TvenderTechnician result = _technicianRepo.Get(con);

            if (result == null)
                throw new NullReferenceException($"[ERROR]=> 廠商登入時,帳號或密碼不存在");

            #endregion 檢核技師相關欄位

            #region 清空相關欄位

            _logger.Info($"APP登出-準備更新資訊");

            result.DeviceID = "";
            result.RegistrationID = "";

            con.Allow(x => x.DeviceID);
            con.Allow(x => x.RegistrationID);

            _technicianRepo.Update(con, result);

            #endregion

            return true;

        }

        /// <summary>
        /// [server 更新使用者資訊]
        /// </summary>
        /// <param name="user"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool Update(UserBase User, RoleAuth Role)
        {

            #region 找到對應的使用者

            var uCon = new Conditions<DataBase.TUSRMST>();

            uCon.And(x => x.Comp_Cd == User.CompCd &&
                          x.User_Id == User.UserId);


            //uCon.Include(x => x.TSYSROL);

            Tusrmst user = _userRepo.Get(uCon);


            if (user == null)
                throw new NullReferenceException($"[ERROR]=>找不到對應的使用者資訊,公司代號:{User.CompCd},使用者ID:{User.UserId}");


            #endregion

            #region 找到對應權限

            var rCon = new Conditions<DataBase.TSYSROL>();

            rCon.And(x => x.Comp_Cd == Role.CompCd &&
                          x.Role_Id == Role.RoleId);

            RoleAuth role = _aspRoleRepo.Get(rCon);

            if (role == null)
                throw new NullReferenceException($"[ERROR]=>找不到對應的權限資訊,公司代號:{User.CompCd},權限ID:{User.RoleId}");


            #endregion

            #region 組合物件

            List<AuthItem> pageAuth = CulcPageAuth(role.PageAuth, User.PageAuth);

            user.RoleId = role.RoleId;

            user.PageAuth = pageAuth != null ? JsonConvert.SerializeObject(pageAuth) : string.Empty;


            #endregion

            #region 更新資料

            uCon.Allow(x => x.Role_Id,
                       x => x.PageAuth);


            if (!_userRepo.Update(uCon, user))
                throw new Exception("[ERROR]=>更新使用者資訊失敗");

            #endregion

            return true;

        }
        /// <summary>
        /// 組合頁面權限
        /// </summary>
        /// <param name="roleAuth">使用者本身的</param>
        /// <param name="userAuth">系統設定的</param>
        /// <returns></returns>
        private List<AuthItem> CulcPageAuth(List<AuthItem> roleAuth, List<AuthItem> userAuth)
        {

            //準備要回傳的
            List<AuthItem> AuthList = new List<AuthItem>();


            if (userAuth != null && userAuth.Count > 0)
                AuthList.AddRange(userAuth);

            //角色權限-篩選清單
            var userAuthDic = roleAuth.ToDictionary(x => string.Format("{0}-{1}-{2}", x.GroupName, x.AuthType, x.isDeny));

            //以使用者選擇的清單為主,對NetRole 做比對
            foreach (var viewItem in userAuth ?? new List<AuthItem>())
            {
                string key = string.Format("{0}-{1}-{2}", viewItem.GroupName, viewItem.AuthType, viewItem.isDeny);

                //如果一樣的,就從新增清單刪除
                if (userAuthDic.ContainsKey(key))
                {
                    AuthList.Remove(userAuthDic[key]);
                }
                else  //若角色權限沒有,使用者選擇的清單有,為白名單
                {
                    AuthList.Where(x => string.Format("{0}-{1}-{2}", x.GroupName, x.AuthType, x.isDeny) == key).SingleOrDefault().isDeny = false;
                }
            }

            //選擇的權限-篩選清單
            var viewAuthDic = userAuth?.ToDictionary(x => string.Format("{0}-{1}", x.GroupName, x.isDeny));

            //將選擇的權限清單與角色權限做比對
            foreach (var userItem in roleAuth)
            {
                string key = string.Format("{0}-{1}", userItem.GroupName, userItem.isDeny);

                //若系統角色權限有,畫面的權限沒有,新增為黑名單
                if (!viewAuthDic.ContainsKey(key))
                {
                    AuthList.Add(new AuthItem()
                    {
                        AuthType = userItem.AuthType,
                        GroupName = userItem.GroupName,
                        isDeny = true,
                    });
                }

            }

            return AuthList;

        }
        /// <summary>
        /// [server]整合使用者資訊
        /// </summary>
        /// <returns></returns>
        UserBase IAspUserService.Integration(string UserId)
        {
            if (string.IsNullOrEmpty(UserId))
                throw new ArgumentNullException($"取得使用者資訊時,並無傳入相關資訊");

            return _userFactory.GetAspUser(UserId);
        }

    }
}

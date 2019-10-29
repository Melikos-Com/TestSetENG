using Ptc.Logger;
using Ptc.Logger.Service;
using Ptc.Seteng.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng.Service
{
    /// <summary>
    /// 附加函式
    /// </summary>
    public class AttachedMethods
    {
        private readonly ISystemLog _logger;
        private readonly IBaseRepository<DataBase.TCMPDAT, Tcmpdat> _compRepo;
        private readonly IBaseRepository<DataBase.TCALLOG, Tcallog> _callogRepo;
        private readonly IBaseRepository<DataBase.TVENDER, Tvender> _venderRepo;
        private readonly IBaseRepository<DataBase.TVenderTechnician, TvenderTechnician> _technicianRepo;


        public AttachedMethods(ISystemLog Logger,
                               IBaseRepository<DataBase.TCALLOG, Tcallog> CallogRepo,
                               IBaseRepository<DataBase.TCMPDAT, Tcmpdat> CompRepo,
                               IBaseRepository<DataBase.TVENDER, Tvender> VenderRepo,
                               IBaseRepository<DataBase.TVenderTechnician, TvenderTechnician> TechnicianRepo)
        {

            _logger = Logger;
            _compRepo = CompRepo;
            _venderRepo = VenderRepo;
            _callogRepo = CallogRepo;
            _technicianRepo = TechnicianRepo;
        }


        /// <summary>
        /// 取得案件資訊
        /// </summary>
        /// <param name="Sn"></param>
        /// <returns></returns>
        public virtual Tcallog GetCallog(string Comp_Cd,string Sn)
        {
            if (String.IsNullOrEmpty(Comp_Cd))
                throw new ArgumentNullException($"[ERROR]=>取得案件資訊時,並沒有給入公司代號");
            if (String.IsNullOrEmpty(Sn))
                throw new ArgumentNullException($"[ERROR]=>取得案件資訊時,並沒有給入案件資代號");

            var con = new Conditions<DataBase.TCALLOG>();

            con.Include(x => x.TAcceptedLog);

            con.And(x => x.Comp_Cd == Comp_Cd);
            con.And(x => x.Sn == Sn);

            Tcallog data = _callogRepo.Get(con);

            if (data == null)
                throw new NullReferenceException($"[ERROR]=>找不到對應的案件資資訊,案件編號:{Sn}");

            return data;

        }
        /// <summary>
        /// 取得公司資訊
        /// </summary>
        /// <param name="compCd"></param>
        /// <returns></returns>
        public virtual Tcmpdat GetComp(string compCd)
        {

            if (String.IsNullOrEmpty(compCd))
                throw new ArgumentNullException($"[ERROR]=>取得公司資訊時,並沒有給入公司代號");

            var con = new Conditions<DataBase.TCMPDAT>();

            con.And(x => x.Comp_Cd == compCd);

            Tcmpdat data = _compRepo.Get(con);

            if (data == null)
                throw new NullReferenceException($"[ERROR]=>找不到對應的公司資訊,公司編號:{compCd}");

            return data;

        }
        /// <summary>
        /// 取得案件與圖片
        /// </summary>
        /// <param name="Sn"></param>
        /// <returns></returns>
        public virtual Tcallog GetCallogWithImg(string Comp_Cd,string Sn)
        {
            if (String.IsNullOrEmpty(Comp_Cd))
                throw new ArgumentNullException($"[ERROR]=>取得案件資訊時,並沒有給入公司代號");
            if (String.IsNullOrEmpty(Sn))
                throw new ArgumentNullException($"[ERROR]=>取得案件資訊時,並沒有給入案件資代號");

            var con = new Conditions<DataBase.TCALLOG>();

            con.And(x => x.Comp_Cd == Comp_Cd);
            con.And(x => x.Sn == Sn);

            Tcallog data = _callogRepo.Get(con);

            if (data == null)
                throw new NullReferenceException($"[ERROR]=>找不到對應的案件資資訊,案件編號:{Sn}");

            return data;
        }
        /// <summary>
        /// 取得技師資訊
        /// </summary>Class1.cs
        /// <param name="CompCd"></param>
        /// <param name="Account"></param>
        /// <returns></returns>
        public virtual TvenderTechnician GetTechnician(string compCd, string account)
        {
            if (String.IsNullOrEmpty(compCd) || String.IsNullOrEmpty(account))
                throw new ArgumentNullException($"[ERROR]=>取得技師資訊時,並沒有給入相關資訊");

            var con = new Conditions<DataBase.TVenderTechnician>();

            con.And(x => x.Comp_Cd == compCd &&
                         x.Account == account);

            TvenderTechnician data = _technicianRepo.Get(con);

            if (data == null)
                throw new NullReferenceException($"[ERROR]=>找不到對應的技師資訊,公司編號:{compCd},技師帳號:{account}");

            return data;

        }
        /// <summary>
        /// 取得廠商資訊
        /// </summary>
        /// <param name="compCd"></param>
        /// <param name="venderCd"></param>
        /// <returns></returns>
        public virtual Tvender GetTvender(string compCd, string venderCd)
        {
            if (String.IsNullOrEmpty(compCd) || String.IsNullOrEmpty(venderCd))
                throw new ArgumentNullException($"[ERROR]=>取得廠商資訊時,並沒有給入相關資訊");

            var con = new Conditions<DataBase.TVENDER>();

            con.And(x => x.Comp_Cd == compCd &&
                         x.Vender_Cd == venderCd);

            Tvender data = _venderRepo.Get(con);

            if (data == null)
                throw new NullReferenceException($"[ERROR]=>找不到對應的廠商資訊,公司編號:{compCd},廠商代號:{venderCd}");

            return data;
        }
    }
}

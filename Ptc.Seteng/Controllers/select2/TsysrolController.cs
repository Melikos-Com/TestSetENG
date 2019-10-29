using Ptc.AspnetMvc.Authentication;
using Ptc.AspnetMvc.Models;
using Ptc.Logger;
using Ptc.Logger.Service;
using Ptc.Seteng.Filter;
using Ptc.Seteng.Helpers;
using Ptc.Seteng.Models;
using Ptc.Seteng.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Ptc.Seteng.Controllers
{
    [AspnetMvc.Filter.AuthenticationFilter]
    public class TsysrolController : BaseController
    {
        private readonly ISystemLog _logger;
        private readonly IBaseRepository<DataBase.TSYSROL, RoleAuth> _aspRoleRepo;

        public TsysrolController(ISystemLog Logger,
                                    IBaseRepository<DataBase.TSYSROL, RoleAuth> AspRoleRepo)
        {
            _logger = Logger;
            _aspRoleRepo = AspRoleRepo;
        }


        /// <summary>
        /// 取得權限清單
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>

        public JsonResult Get(TsysrolApiViewModel data)
        {
            try
            {
                if (data == null)
                 throw new ArgumentNullException($"no input data : {nameof(TsysrolApiViewModel) }"); 

                var con = new Conditions<DataBase.TSYSROL>(data.pageSize, data.page);

                data.GetProperties()
                    .Select(x => x.Avatar<AvatarAttribute>(data))
                    .Where(x => x.Key != null)
                    .ForEach(g => con.ConvertToFilter(g.Key.SubstituteName,
                                                      g.Key.ExpressionType,
                                                      g.Key.PredicateType,
                                                      g.Value));

                if (!string.IsNullOrEmpty(data.keyword))
                {
                    con.And(x => x.Role_Id.Contains(data.keyword) ||
                                 x.Role_Name.Contains(data.keyword));
                }

                con.Order(OrderType.Asc, x => x.Role_Id);
                
       

                #region DataRange

                var _user = (this.User.Identity as AspnetMvc.Models.PtcIdentity).currentUser;

                con.And(x => x.Comp_Cd == _user.CompCd);



                #endregion

                var list = _aspRoleRepo.GetList(con);
                
                IEnumerable<Select2> result = list.Select(x => new Select2(x.RoleId, $"{x.RoleId}-{x.RoleName}", x));

                return Json(new { items = result, totalCount = list.TotalCount });

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return Json(ex.Message);
            }

        }

    }
}
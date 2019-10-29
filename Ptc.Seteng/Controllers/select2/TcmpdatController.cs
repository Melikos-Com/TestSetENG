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
using System.Web.Http;
using System.Web.Mvc;

namespace Ptc.Seteng.Controllers
{

    public class TcmpdatController : BaseController
    {
        private readonly ISystemLog _logger;
        private readonly IBaseRepository<DataBase.TCMPDAT, Tcmpdat> _baseRepo;

        public TcmpdatController(ISystemLog Logger,
                                    IBaseRepository<DataBase.TCMPDAT, Tcmpdat> BaseRepo)
        {

            _logger = Logger;
            _baseRepo = BaseRepo;
        }

        /// <summary>
        /// 取得公司
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [AspnetMvc.Filter.AuthenticationFilter]
        public JsonResult Get(TcmpdatApiViewModel data)
        {
            try
            {
                if (data == null)
                { throw new ArgumentNullException($"no input data : {nameof(TastkndApiViewModel) }"); }

                var con = new Conditions<DataBase.TCMPDAT>(data.pageSize, data.page);

                data.GetProperties()
                    .Select(x => x.Avatar<AvatarAttribute>(data))
                    .Where(x => x.Key != null)
                    .ForEach(g => con.ConvertToFilter(g.Key.SubstituteName,
                                                       g.Key.ExpressionType,
                                                       g.Key.PredicateType,
                                                       g.Value));

                if (!string.IsNullOrEmpty(data.keyword))
                {
                    con.And(x => x.Comp_Cd.Contains(data.keyword) ||
                                 x.Comp_Name.Contains(data.keyword));
                }

             

                con.Order(OrderType.Asc, x => x.Sort_Seq);

                #region DataRange

                var _user = (this.User.Identity as AspnetMvc.Models.PtcIdentity).currentUser;

                con.And(x => x.Comp_Cd == _user.CompCd);

                #endregion
                var meta = _baseRepo.GetList(con);

                IEnumerable<Select2> result = meta.Select(x => new Select2(x.CompCd, x.CompShort, x));

             
                return Json(new {items = result,totalCount = meta.TotalCount});
    
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return Json(ex.Message);

            }

        }


        [System.Web.Mvc.AllowAnonymous]
        public JsonResult GetComp(TcmpdatApiViewModel data)
        {
            try
            {
                if (data == null)
                { throw new ArgumentNullException($"no input data : {nameof(TastkndApiViewModel) }"); }

                var con = new Conditions<DataBase.TCMPDAT>(data.pageSize, data.page);

                data.GetProperties()
                    .Select(x => x.Avatar<AvatarAttribute>(data))
                    .Where(x => x.Key != null)
                    .ForEach(g => con.ConvertToFilter(g.Key.SubstituteName,
                                                       g.Key.ExpressionType,
                                                       g.Key.PredicateType,
                                                       g.Value));

                if (!string.IsNullOrEmpty(data.keyword))
                {
                    con.And(x => x.Comp_Cd.Contains(data.keyword) ||
                                 x.Comp_Name.Contains(data.keyword));
                }

                con.Order(OrderType.Asc, x => x.Sort_Seq);


                var meta = _baseRepo.GetList(con);

                IEnumerable<Select2> result = meta.Select(x => new Select2(x.CompCd, x.CompShort, x));


                return Json(new { items = result, totalCount = meta.TotalCount });

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return Json(ex.Message);

            }

        }
    }
}

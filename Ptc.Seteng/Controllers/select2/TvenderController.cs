using Ptc.AspnetMvc.Models;
using Ptc.Logger;
using Ptc.Logger.Service;
using Ptc.Seteng.Filter;
using Ptc.Seteng.Helpers;
using Ptc.Seteng.Models;
using Ptc.Seteng.Repository;
using Ptc.Seteng.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ptc.Seteng.Controllers
{
    [AspnetMvc.Filter.AuthenticationFilter]
    public class TvenderController : BaseController
    {
        private readonly ISystemLog _logger;
        private readonly IBaseRepository<DataBase.TVENDER, Tvender> _baseRepo;

        public TvenderController(ISystemLog Logger,
                                    IBaseRepository<DataBase.TVENDER, Tvender> BaseRepo)
        {
            _logger = Logger;
            _baseRepo = BaseRepo;
        }

        /// <summary>
        /// 取得門市
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public System.Web.Mvc.JsonResult Get(TvenderApiViewModel data)
        {
            try
            {
                if (data == null)
                { throw new ArgumentNullException($"no input data : {nameof(TvenderApiViewModel) }"); }
             
                var con = new Conditions<DataBase.TVENDER>(data.pageSize, data.page);

                data.GetProperties()
                    .Select(x => x.Avatar<AvatarAttribute>(data))
                    .Where(x => x.Key != null)
                    .ForEach(g => con.ConvertToFilter(g.Key.SubstituteName,
                                                      g.Key.ExpressionType,
                                                      g.Key.PredicateType,
                                                      g.Value));

                if (!string.IsNullOrEmpty(data.keyword))
                {
                    con.And(x => x.Vender_Name.Contains(data.keyword) ||
                                 x.Vender_Cd.Contains(data.keyword));
                }

                con.Order(OrderType.Asc, x => x.Vender_Cd);

                #region DataRange

                var _user = (this.User.Identity as AspnetMvc.Models.PtcIdentity).currentUser;

                con.And(x => x.Comp_Cd == _user.CompCd);

                if (_user.DataRange?.VendorCd != null)
                    con.And(x => _user.DataRange.VendorCd.Contains(x.Vender_Cd));

                #endregion

                var meta = _baseRepo.GetList(con);

                IEnumerable<Select2> result = meta.Select(x => new Select2(x.VenderCd, $"{x.VenderCd}-{x.VenderName}", x));

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

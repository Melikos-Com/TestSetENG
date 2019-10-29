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
    [AspnetMvc.Filter.AuthenticationFilter]
    public class TzocodeController : BaseController
    {

        private readonly ISystemLog _logger;
        private readonly IBaseRepository<DataBase.TZOCODE, Tzocode> _baseRepo;

        public TzocodeController(ISystemLog Logger,

                                    IBaseRepository<DataBase.TZOCODE, Tzocode> BaseRepo)
        {

            _logger = Logger;
            _baseRepo = BaseRepo;
        }

        /// <summary>
        /// 取得區域
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public JsonResult Get(TzocodeApiViewModel data)
        {
            try
            {
                if (data == null)
                { throw new ArgumentNullException($"no input data : {nameof(TzocodeApiViewModel) }"); }

                var con = new Conditions<DataBase.TZOCODE>(data.pageSize, data.page);

                data.GetProperties()
                    .Select(x => x.Avatar<AvatarAttribute>(data))
                    .Where(x => x.Key != null)
                    .ForEach(g => con.ConvertToFilter(g.Key.SubstituteName,
                                                      g.Key.ExpressionType,
                                                      g.Key.PredicateType,
                                                      g.Value));

                if (!string.IsNullOrEmpty(data.keyword))
                {
                    con.And(x => x.Zo_Name.Contains(data.keyword) ||
                                 x.Z_O.Contains(data.keyword));
                }

                con.Order(OrderType.Asc, x => x.Z_O);


                #region DataRange

                var _user = (this.User.Identity as AspnetMvc.Models.PtcIdentity).currentUser;

                con.And(x => x.Comp_Cd == _user.CompCd);

                if (_user.DataRange?.ZoCd != null)
                    con.And(x => _user.DataRange.ZoCd.Contains(x.Z_O));

                #endregion


                var meta = _baseRepo.GetList(con);

                IEnumerable<Select2> result = meta.Select(x => new Select2(x.ZoCd, $"{x.ZoCd}-{x.ZoName}", x));

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

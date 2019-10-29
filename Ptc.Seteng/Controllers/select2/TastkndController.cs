using Ptc.Logger;
using Ptc.Logger.Service;
using Ptc.Seteng.Filter;
using Ptc.Seteng.Helpers;
using Ptc.Seteng.Models;
using Ptc.Seteng.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Ptc.Seteng.Controllers
{
    [AspnetMvc.Filter.AuthenticationFilter]
    public class TastkndController : BaseController
    {

        private readonly ISystemLog _logger;
        private readonly IBaseRepository<DataBase.TASTKND, Tastknd> _baseRepo;

        public TastkndController(ISystemLog Logger,
                                    IBaseRepository<DataBase.TASTKND, Tastknd> BaseRepo)
        {
            _logger = Logger;
            _baseRepo = BaseRepo;
        }

        /// <summary>
        /// 取得設備分類
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public JsonResult Get(TastkndApiViewModel data)
        {
            try
            {
                if (data == null)
                { throw new ArgumentNullException($"no input data : {nameof(TastkndApiViewModel) }"); }

                var con = new Conditions<DataBase.TASTKND>(data.pageSize, data.page);

                data.GetProperties()
                    .Select(x => x.Avatar<AvatarAttribute>(data))
                    .Where(x => x.Key != null)
                    .ForEach(g => con.ConvertToFilter(g.Key.SubstituteName,
                                                       g.Key.ExpressionType,
                                                       g.Key.PredicateType,
                                                       g.Value));

                if (!string.IsNullOrEmpty(data.keyword))
                {
                    con.And(x => x.Kind_Cd.Contains(data.keyword) ||
                                 x.Kind_Name.Contains(data.keyword));
                }

                con.Order(OrderType.Asc, x => x.Kind_Cd);

                #region DataRange

                var _user = (this.User.Identity as AspnetMvc.Models.PtcIdentity).currentUser;

                con.And(x => x.Comp_Cd == _user.CompCd);


                #endregion

                var meta = _baseRepo.GetList(con);

                IEnumerable <Select2> result = meta.Select(x => new Select2(x.KindCd, x.KindName, x));


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
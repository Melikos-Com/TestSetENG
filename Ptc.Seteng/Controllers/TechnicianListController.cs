using Ptc.AspnetMvc.Authentication;
using Ptc.AspnetMvc.Authentication.Menu;
using Ptc.AspnetMvc.Filter;
using Ptc.AspnetMvc.Models;
using Ptc.Logger;
using Ptc.Seteng.Filter;
using Ptc.Seteng.Helpers;
using Ptc.Seteng.Models;
using Ptc.Seteng.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace Ptc.Seteng.Controllers
{
    [AuthenticationFilter]
    public class TechnicianListController : BaseController
    {
        private readonly Logger.Service.ISystemLog _logger;
        private readonly IBaseRepository<DataBase.TVenderTechnician, TvenderTechnician> _technicianRepo;

        public TechnicianListController(Logger.Service.ISystemLog Logger,
                                         IBaseRepository<DataBase.TVenderTechnician, TvenderTechnician> TechnicianRepo)
        {

            this._logger = Logger;
            this._technicianRepo = TechnicianRepo;

        }
        [MenuNode(
        Title = "技師名單",
        Description = "技師名單",
        PrefixedNodeID = "home",
        isEntry = true)]
        public ActionResult Index()
        {
            var _user = ((PtcIdentity)this.User.Identity).currentUser;
            return View(new TechnicianListViewModel() { CompCd = _user.CompCd, });
        }

        [MenuNode(
        Title = "取得列表-功能",
        Description = "取得列表-功能")]
        public ActionResult GetList(DataTablesReqModel<List<TechnicianListViewModel>> data)
        {

            List<TechnicianListViewModel> models = data.criteria;

            DataTablesRespModel result = new DataTablesRespModel(data.draw);

            try
            {

                var con = new Conditions<DataBase.TVenderTechnician>(data.length, (data.start / data.length));

                models?.ForEach(model =>
                {

                    var component = new List<Expression<Func<DataBase.TVenderTechnician, Boolean>>>();

                    model.GetProperties()?
                         .Select(x => x.Avatar<AvatarAttribute>(model))
                         .Where(x => x.Key != null)
                         .ForEach(g =>
                         {
                             component.Add(con.CombinationExpression(
                                  g.Key.SubstituteName,
                                  g.Key.ExpressionType,
                                  g.Value));
                         });

                    con.ConvertToMultiFilter(component);
                });


                data.order?.ForEach(x => con.Order(x.dir, data.columns[x.column].name));

                //con.Include(x => x.TVENDER.TCMPDAT);
                //con.And(x => x.Enable == true);  只查詢啟用帳號
                #region DataRange

                var _user = (this.User.Identity as AspnetMvc.Models.PtcIdentity).currentUser;

                con.And(x => x.Comp_Cd == _user.CompCd);

                if (_user.DataRange?.VendorCd != null)
                    con.And(x => _user.DataRange.VendorCd.Contains(x.Vender_Cd));

                #endregion
                var list = _technicianRepo.GetList(con);


                PagedList<TvenderTechnician> meta = new PagedList<TvenderTechnician>(list);


                result.data = meta.Select(x => new TechnicianListResultViewModel(x).colData)
                                  .ToArray();

                result.TotalCount(con.TotalCount);



            }
            catch (AutoMapper.AutoMapperMappingException ex)
            {
                _logger.Error(ex);
                result.error = ex.InnerException.Message;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                result.error = ex.Message;
            }
            return Json(result);


        }

        [MenuNode(
        Title = "取得技師信息-畫面",
        Description = "取得技師信息-畫面")]
        public ActionResult Detail(string Account, string CompCd, string VenderCd, AuthNodeType mode)
        {

            try
            {
                if (MethodHelper.IsNullOrEmpty(Account, CompCd, VenderCd))
                    throw new ArgumentException($"取得技師信息時,并没有给入对应的参数");


                var con = new Conditions<DataBase.TVenderTechnician>();

                con.And(x => x.Comp_Cd == CompCd);
                con.And(x => x.Vender_Cd == VenderCd);
                con.And(x => x.Account == Account);

                //con.Include(x => x.TVENDER.TCMPDAT);

                TvenderTechnician technician = _technicianRepo.Get(con);

                if (technician == null)
                    throw new NullReferenceException($"取得不到技師信息,公司编号:{CompCd} , 厂商编号:{VenderCd} ,账号为:{Account}");

                return View("Edit", new TechnicianListDetailViewModel(technician) { ActionType = mode });

            }
            catch (Exception ex)
            {

                _logger.Error(ex.Message);
                return View();

            }


        }
    }
}

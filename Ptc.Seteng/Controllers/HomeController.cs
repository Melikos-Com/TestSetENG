using Ptc.AspnetMvc.Authentication.Menu;
using Ptc.AspnetMvc.Filter;
using Ptc.AspnetMvc.Models;
using Ptc.AspNetMvc;
using Ptc.Logger;
using Ptc.Logger.Service;
using Ptc.Seteng;
using Ptc.Seteng.Filter;
using Ptc.Seteng.Helpers;
using Ptc.Seteng.Models;
using Ptc.Seteng.Repository;
using Ptc.Seteng.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Ptc.Seteng.Controllers
{
    [AuthenticationFilter]
    public class HomeController : BaseController
    {
        private readonly ISystemLog _logger;
        private readonly ITechnicianService _technicianService;
        private readonly IBaseRepository<DataBase.TASSETS, Tassets> _baseRepo;

        public HomeController(ISystemLog Logger,
                              ITechnicianService TechnicianService,
                              IBaseRepository<DataBase.TASSETS, Tassets> BaseRepo)
        {
            _logger = Logger;
            _baseRepo = BaseRepo;
            _technicianService = TechnicianService;
        }

        [MenuNode(
        Title = "home",
        Description = "Root",
        PrefixedNodeID = "home",
        isEntry = true)]
        public ActionResult Index()
        {
            try
            {

           
                TassetsViewModel modal = new TassetsViewModel();

                return View(modal);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                TempData["exMessage"] = ex.Message;
                return View();

            }
        }

        [MenuNode(
        Title = "取得清單",
        Description = "取得清單")]
        public ActionResult GetListBySingle(DataTablesReqModel<TassetsViewModel> data)
        {

            DataTablesRespModel result = new DataTablesRespModel(data.draw);

            try
            {
                if (data == null)
                    throw new ArgumentNullException($"no input data");


                Conditions<DataBase.TASSETS> con = new Conditions<DataBase.TASSETS>
                                                         (data.length, (data.start / data.length));

                TassetsViewModel model = data.criteria;

                model?.GetProperties()?
                      .Select(x => x.Avatar<AvatarAttribute>(model))
                      .Where(x => x.Key != null)
                      .ForEach(g => con.ConvertToFilter(g.Key.SubstituteName,
                                                        g.Key.ExpressionType,
                                                        g.Key.PredicateType,
                                                        g.Value));

                data.order?.ForEach(x =>
                {
                    con.Order(x.dir, data.columns[x.column].name);
                });
       
                PagedList<Tassets> meta = new PagedList<Tassets>(_baseRepo.GetList(con), (data.start / data.length), data.length);

                result.data = meta.Select(x => new TassetsResultViewModel(x).colData)
                                  .ToArray();

                result.TotalCount(meta.TotalCount);

            }
            catch (Exception ex)
            {

                _logger.Error(ex);
                result.error = ex.Message;
            }

            return Json(result);

        }

        [MenuNode(
        Title = "取得清單",
        Description = "取得清單")]
        public ActionResult GetList(DataTablesReqModel<List<TassetsViewModel>> data)
        {
            List<TassetsViewModel> models = data.criteria;

            DataTablesRespModel result = new DataTablesRespModel(data.draw);

            try
            {
                Conditions<DataBase.TASSETS> con = new Conditions<DataBase.TASSETS>
                                                      (data.length, (data.start / data.length));

                models?.ForEach(model =>
                {

                    var component = new List<Expression<Func<DataBase.TASSETS, Boolean>>>();

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

                data.order?.ForEach(x =>
                {
                    con.Order(x.dir, data.columns[x.column].name);
                });
                #region DataRange

                var _user = (this.User.Identity as AspnetMvc.Models.PtcIdentity).currentUser;

                con.And(x => x.Comp_Cd == _user.CompCd);


                #endregion
                PagedList<Tassets> meta = new PagedList<Tassets>(_baseRepo.GetList(con),(data.start / data.length), data.length);

                result.data = meta.Select(x => new TassetsResultViewModel(x).colData)
                                  .ToArray();

                result.TotalCount(meta.TotalCount);

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
        Title = "批次刪除",
        Description = "批次刪除")]
        public ActionResult Delete(List<TassetsResultViewModel> data)
        {
            var result = new JsonResult();

            try
            {
                Conditions<DataBase.TASSETS> con = new Conditions<DataBase.TASSETS>();

                var cdList = data.Select(x => x.AssetCd);

                con.And(x => cdList.Contains(x.Asset_Cd));

                var isSuccess = _baseRepo.Remove(con);

                result.Data = new
                {
                    isSuccess = isSuccess,
                    message = $"於{DateTime.Now},刪除完畢"
                };
            }
            catch (AutoMapper.AutoMapperMappingException ex)
            {
                result.Data = new
                {
                    isSuccess = false,
                    message = $"於{DateTime.Now},刪除失敗:{ex.InnerException.Message}"
                };

                _logger.Error(ex);
            }
            catch (Exception ex)
            {
                result.Data = new
                {
                    isSuccess = false,
                    message = $"於{DateTime.Now},刪除失敗:{ex.Message}"
                };
                _logger.Error(ex);
            }


            return Json(result);

        }

        [MenuNode(
        Title = "修改視窗",
        Description = "修改視窗")]
        public ActionResult Update(TassetsResultViewModel data)
        {
            return View();
        }

        [MenuNode(
        Title = "新增視窗",
        Description = "新增視窗")]
        public ActionResult Add()
        {
            return View();
        }

    }
}
using Ptc.AspnetMvc.Models;
using Ptc.Logger;
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

namespace Ptc.Seteng.Controllers.api
{
    /// <summary>
    /// 技師相關
    /// </summary>
    [TokenAuthenticationFilter]
    public class TechnicianController : ApiController
    {

        private readonly Logger.Service.ISystemLog _logger;
        private readonly ITechnicianService _technicianService;
        private readonly IBaseRepository<DataBase.TVenderTechnician, TvenderTechnician> _technicianRepo;
        private readonly IBaseRepository<DataBase.TTechnicianGroup, TtechnicianGroup> _technicianGroupRepo;

        public TechnicianController(Logger.Service.ISystemLog Logger,
                                    ITechnicianService TechnicianService,
                                    IBaseRepository<DataBase.TVenderTechnician, TvenderTechnician> TechnicianRepo,
                                    IBaseRepository<DataBase.TTechnicianGroup, TtechnicianGroup> TechnicianGroupRepo)
        {
            _logger = Logger;
            _technicianRepo = TechnicianRepo;
            _technicianService = TechnicianService;
            _technicianGroupRepo = TechnicianGroupRepo;
        }

        /// <summary>
        /// 取得技師清單
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetList(TechnicianApiViewModel data)
        {
            try
            {
                var user = ((PtcIdentity)this.User.Identity).currentUser;

                var con = new Conditions<DataBase.TVenderTechnician>(data.pageSize, data.page);

                con.And(x => x.Vender_Cd == user.VenderCd &&
                             x.Comp_Cd == user.CompCd);

                //啟用的才可以顯示
                con.And(x => x.Enable == true);

                if (!string.IsNullOrEmpty(data.keyword))
                    con.And(x => x.Account.Contains(data.keyword) || x.Name.Contains(data.keyword));

                PagedList<TvenderTechnician> list = _technicianRepo.GetList(con);

                if (!list.Any())
                {

                    return Request.CreateResponse(
                         HttpStatusCode.OK, new JsonResult<List<TechnicianResultApiViewModel>>()
                         {

                             isSuccess = true,
                             element = new List<TechnicianResultApiViewModel>(),
                             message = "無資料",
                             totalCount = con.TotalCount,
                         });

                }

                IEnumerable<TechnicianResultApiViewModel> result = list.Select(x => new TechnicianResultApiViewModel(x));

                return Request.CreateResponse(
                   HttpStatusCode.OK,
                   new JsonResult<IEnumerable<TechnicianResultApiViewModel>>(result, "[廠商]廠商技師查詢成功", con.TotalCount, true));


            }
            catch (Exception ex)
            {

                _logger.Error(ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                   $"{ ex.GetType().Name}:Message:{ex.Message}");
            }

        }

        /// <summary>
        /// 取得技師群組清單
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetGroupList(TechnicianApiViewModel data)
        {

            try
            {
                var user = ((PtcIdentity)this.User.Identity).currentUser;

                var con = new Conditions<DataBase.TTechnicianGroup>(data.pageSize, data.page);

                con.Include(x => x.TTechnicianGroupClaims
                                  .Select(y => y.TVenderTechnician));

                con.And(x => x.VendorCd == user.VenderCd &&
                             x.CompCd == user.CompCd);


                if (!string.IsNullOrEmpty(data.keyword))
                    con.And(x => x.GroupName.Contains(data.keyword));

                PagedList<TtechnicianGroup> list = _technicianGroupRepo.GetList(con);

                if (!list.Any())
                {

                    return Request.CreateResponse(
                         HttpStatusCode.OK, new JsonResult<List<TechnicianGPResultApiViewModel>>()
                         {

                             isSuccess = true,
                             element = new List<TechnicianGPResultApiViewModel>(),
                             message = "無資料",
                             totalCount = 0,
                         });

                }


                IEnumerable<TechnicianGPResultApiViewModel> result = list.Select(x => new TechnicianGPResultApiViewModel(x));

                return Request.CreateResponse(
                   HttpStatusCode.OK,
                   new JsonResult<IEnumerable<TechnicianGPResultApiViewModel>>(result, "[廠商]廠商技師群組查詢成功", con.TotalCount, true));


            }
            catch (Exception ex)
            {

                _logger.Error(ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                   $"{ ex.GetType().Name}:Message:{ex.Message}");
            }


        }

        /// <summary>
        /// 合併群組與技師
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage MergeTechnician(TechnicianMergeApiViewModel data)
        {
            try
            {
                var user = ((PtcIdentity)this.User.Identity).currentUser;

                #region 組合物件

                IEnumerable<TvenderTechnician> technicians = data.Accounts.Select(technician =>
                {

                    return new TvenderTechnician()
                    {
                        VenderCd = technician.VenderCd,
                        CompCd = technician.CompCd,
                        Account = technician.Account
                    };
                });

                IEnumerable<TtechnicianGroup> groups = data.Groups.Select(group =>
                {
                    return new TtechnicianGroup()
                    {
                        VendorCd = group.VendorCd,
                        CompCd = group.CompCd,
                        Seq = group.Seq
                    };
                });

                #endregion

                List<TvenderTechnician> merged = _technicianService.MergeTechnician(technicians, groups);

                IEnumerable<TechnicianResultApiViewModel> result = merged.Select(x => new TechnicianResultApiViewModel(x));

                return Request.CreateResponse(
                HttpStatusCode.OK,
                new JsonResult<IEnumerable<TechnicianResultApiViewModel>>(result, "[廠商]技師合併通知對象成功", result.Count(), true));
            }
            catch (Exception ex)
            {

                _logger.Error(ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                   $"{ ex.GetType().Name}:Message:{ex.Message}");
            }

        }

    }
}

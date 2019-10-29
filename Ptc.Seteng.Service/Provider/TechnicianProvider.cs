using Newtonsoft.Json;
using Ptc.Logger.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace Ptc.Seteng.Provider
{
    public class TechnicianProvider : ITechnicianProvider
    {
        private readonly ISystemLog _logger;
        public TechnicianProvider(ISystemLog Logger) {
            _logger = Logger;
        }
        ///// <summary>
        ///// 刪除技師圖片
        ///// </summary>
        ///// <param name="Data"></param>
        ///// <param name="Path"></param>
        ///// <returns></returns>
        //public Boolean DeleteImg(TvenderTechnician Data, string filePath)
        //{
        //    using (DataBase.SpccEngCCSysEntities db = new DataBase.SpccEngCCSysEntities())
        //    {

        //        db.Configuration.LazyLoadingEnabled = false;

        //        var query = db.TVenderTechnician
        //                      .SingleOrDefault(x => x.Account == Data.Account &&
        //                                            x.Vender_Cd == Data.VenderCd &&
        //                                            x.Comp_Cd == Data.CompCd);

        //        if (query == null)
        //            throw new NullReferenceException($"[ERROR]=> 刪除技師圖片找不到相關資訊");



        //        var technicianImgPath = JsonConvert.DeserializeObject<List<string>>(query.TechnicianImgPath);
        //        var stickerImgPath = JsonConvert.DeserializeObject<List<string>>(query.StickerImgPath);
        //        var LicenseImgPath = JsonConvert.DeserializeObject<List<string>>(query.LicenselmgPath);

        //        if (technicianImgPath.Contains(filePath))
        //        {
        //            technicianImgPath.Remove(filePath);
        //            query.TechnicianImgPath = JsonConvert.SerializeObject(technicianImgPath);
        //        }
        //        else if (stickerImgPath.Contains(filePath))
        //        {
        //            stickerImgPath.Remove(filePath);
        //            query.StickerImgPath = JsonConvert.SerializeObject(stickerImgPath);
        //        }
        //        else if (LicenseImgPath.Contains(filePath))
        //        {
        //            LicenseImgPath.Remove(filePath);
        //            query.LicenselmgPath = JsonConvert.SerializeObject(LicenseImgPath);
        //        }
        //        else
        //            throw new IndexOutOfRangeException($"[ERROR]=> 刪除技師圖片,圖片位址對應錯誤");




        //        return db.SaveChanges() > 0;

        //    }


        //}

        /// <summary>
        /// 新增可受理案件
        /// </summary>
        /// <returns></returns>
        public Boolean AddAwaitAcceptLog(string CompCd, string Sn, string Account)
        {
            try
            {
                using (DataBase.SETENG_Entities db = new DataBase.SETENG_Entities())
                {
                    db.Configuration.LazyLoadingEnabled = false;

                    var callog = db.TCALLOG
                                   .SingleOrDefault(x => x.Comp_Cd == CompCd &&
                                                         x.Sn == Sn);

                    if (callog == null)
                        throw new NullReferenceException("[ERROR]=>技師新增可受理案件時,找不到案件資訊");

                    var technician = db.TVenderTechnician
                                       .Include("TCALLOG")
                                       .SingleOrDefault(x => x.Comp_Cd == callog.Comp_Cd &&
                                                             x.Vender_Cd == callog.Vender_Cd &&
                                                             x.Account == Account);

                    if (technician == null)
                        throw new NullReferenceException("[ERROR]=>技師新增可受理案件時,找不到技師資訊");

                    technician.TCALLOG.Add(callog);

                    return db.SaveChanges() > 0;

                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        private static string connStr = WebConfigurationManager.ConnectionStrings["SetEngADO"].ConnectionString;

        /// <summary>
        /// 刪除可受理案件
        /// </summary>
        /// <param name="log"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        public Boolean RemoveAwaitAcceptLog(string CompCd, string Sn)
        {

            //using (DataBase.SETENG_Entities db = new DataBase.SETENG_Entities())
            //{
            //    db.Configuration.LazyLoadingEnabled = false;

            //    //var technicians = db.TVenderTechnician
            //    //                    .Include("TCALLOG")
            //    //                    .Where(x => x.TCALLOG.Any(g => g.Sn == Sn && g.Comp_Cd == CompCd));

            //    //if (technicians == null || technicians.Count() == 0)
            //    //    throw new NullReferenceException("[ERROR]=>技師刪除可受理案件時,找不到技師資訊");

            //    //technicians?.ForEach(x =>
            //    //{

            //    //    var rmLog = x.TCALLOG.SingleOrDefault(y => y.Sn == Sn && y.Comp_Cd == CompCd);

            //    //    x.TCALLOG.Remove(rmLog);

            //    //});

            //    var callog = db.TCALLOG
            //                   .Include("TVenderTechnician")
            //                   .Where(x => x.Sn == Sn && x.Comp_Cd == CompCd).SingleOrDefault();

            //    callog.TVenderTechnician = null;

            //    return db.SaveChanges() > 0;

            //}

            _logger.Info($"直接刪除TCallLogClaims，公司別：{CompCd}，案件編號{Sn}");
            //直接刪除TCallLogClaims
            ExecuteNonQuery(@"Delete TCallLogClaims where Sn = @Sn AND CompCdTcallog = @CompCd"
                            , new SqlParameter("@Sn", Sn)
                            , new SqlParameter("@CompCd", CompCd));

            object DataCount = ExecuteScalar(@"SELECT count(*) From TCallLogClaims where Sn = @Sn AND CompCdTcallog = @CompCd"
                                        , new SqlParameter("@Sn", Sn)
                                        , new SqlParameter("@CompCd", CompCd));

            if (Convert.ToInt32(DataCount) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        public int GetCallLogClaimsCount(string CompCd, string Sn)
        {
            _logger.Info($"TCallLogClaims筆數查詢，公司別：{CompCd}，案件編號{Sn}");
            object DataCount = ExecuteScalar(@"SELECT count(*) From TCallLogClaims where Sn = @Sn AND CompCdTcallog = @CompCd"
                                        , new SqlParameter("@Sn", Sn)
                                        , new SqlParameter("@CompCd", CompCd));

            return Convert.ToInt32(DataCount);
        }


            public static int ExecuteNonQuery(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public static object ExecuteScalar(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteScalar();
                }
            }
        }

    }

}

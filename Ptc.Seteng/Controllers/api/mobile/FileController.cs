using Ptc.AspnetMvc.Models;
using Ptc.Logger;

using Ptc.Seteng.Filter;
using Ptc.Seteng.Helpers;
using Ptc.Seteng.Models;
using Ptc.Seteng.Repository;
using Ptc.Seteng.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace Ptc.Seteng.Controllers.api
{
    /// <summary>
    /// 檔案相關
    /// </summary>
    [AllowAnonymous]
    public class FileController : ApiController
    {

        private readonly Logger.Service.ISystemLog _logger;
        private readonly IBaseRepository<DataBase.TCALIMG , Tcalimg > _callogImg;

        public FileController(Logger.Service.ISystemLog Logger,
                              IBaseRepository<DataBase.TCALIMG, Tcalimg> CallogImg)
        {
            _logger = Logger;
            _callogImg = CallogImg;
        }

        /// <summary>
        /// 存放傳入log(廠商)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [TokenAuthenticationFilter]
        [HttpPost]
        public HttpResponseMessage SaveLog()
        {
            try
            {
                HttpMultipartParser.MultipartFormDataParser parser = new HttpMultipartParser.MultipartFormDataParser(HttpContext.Current.Request.InputStream);

                var Files = parser.Files;

                var user = ((PtcIdentity)this.User.Identity).currentUser;

                string url = ServerProfile.GetInstance().LOG_PATH;

                if (Files == null || Files.Count() == 0)
                    throw new ArgumentNullException($"[ERROR]=>上傳LOG FILE時,並未給入檔案");

                foreach (var file in Files)
                {

                    string fullPath = $"{url}/{user.CompCd}/{user.VenderCd}/{user.UserId}/";

                    FileUtil.Save(fullPath, file.Name, file.Data);

                }

            }
            catch (Exception ex)
            {

                _logger.Error(ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                   $"{ ex.GetType().Name}:message:{ex.Message}");
            }

            return Request.CreateResponse(
            HttpStatusCode.OK,
            new JsonResult<Boolean>(true, "上傳LOG FILE成功", 1, true));

        }
        /// <summary>
        /// 存放傳入log(總部)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [StoreTokenAuthenticationFilter]
        [HttpPost]
        public HttpResponseMessage SaveLogHQ()
        {
            try
            {
                HttpMultipartParser.MultipartFormDataParser parser = new HttpMultipartParser.MultipartFormDataParser(HttpContext.Current.Request.InputStream);

                var Files = parser.Files;

                var user = ((PtcIdentity)this.User.Identity).currentUser;

                string url = ServerProfile.GetInstance().LOG_PATH;

                if (Files == null || Files.Count() == 0)
                    throw new ArgumentNullException($"[ERROR]=>上傳LOG FILE時,並未給入檔案");

                foreach (var file in Files)
                {

                    string fullPath = $"{url}/{user.CompCd}/{user.RoleId}/{user.UserId}/";

                    FileUtil.Save(fullPath, file.Name, file.Data);

                }

            }
            catch (Exception ex)
            {

                _logger.Error(ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                   $"{ ex.GetType().Name}:message:{ex.Message}");
            }

            return Request.CreateResponse(
            HttpStatusCode.OK,
            new JsonResult<Boolean>(true, "上傳LOG FILE成功", 1, true));

        }
        /// <summary>
        /// 取得大頭照
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public HttpResponseMessage GetSticker(string filePath)
        {

            //頭貼路徑
            var path = ServerProfile.GetInstance().STICKER_PATH;

            //完整路徑
            var img = Image.FromFile(path + filePath);

            using (MemoryStream ms = new MemoryStream())
            {

                img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                result.Content = new ByteArrayContent(ms.ToArray());
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
                result.Headers.CacheControl = new CacheControlHeaderValue()
                {
                    NoStore = true,
                    NoCache = true,
                    MustRevalidate = true
                };

                img.Dispose();
                ms.Dispose();

                return result;
            }
        }

        /// <summary>
        /// 取得案件圖片
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public HttpResponseMessage GetCallog(string filePath)
        {

            //頭貼路徑
            var path = ServerProfile.GetInstance().CALLOG_PATH;

            //完整路徑
            var img = Image.FromFile(path + filePath);

            using (MemoryStream ms = new MemoryStream())
            {

                img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                result.Content = new ByteArrayContent(ms.ToArray());
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
                result.Headers.CacheControl = new CacheControlHeaderValue()
                {
                    NoStore = true,
                    NoCache = true,
                    MustRevalidate = true
                };

                img.Dispose();
                ms.Dispose();

                return result;
            }
        }

        /// <summary>
        /// 取得案件圖片
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public HttpResponseMessage GetCallog(string CompCd, string Sn , byte  Seq)
        {

            //頭貼路徑
            var path = ServerProfile.GetInstance().CALLOG_PATH;

            var con = new Conditions<DataBase.TCALIMG >();

            con.And(x => x.Comp_Cd  == CompCd  &&   //公司別
                                  x.Sn == Sn &&              //叫修案件
                                  x.Seq == Seq  );           //序號

            Tcalimg   callog = _callogImg .Get (con);
            // Convert Base64 String to byte[]
            byte[] imageBytes = callog.CallImage;
            MemoryStream msImg = new MemoryStream(imageBytes, 0,
              imageBytes.Length);

            // Convert byte[] to Image
            msImg.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(msImg, true);

            //完整路徑
       //     var img = Image.FromFile("data:image/png;base64," + Convert.ToBase64String(callog.CallImage ));

            using (MemoryStream ms = new MemoryStream())
            {

                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                result.Content = new ByteArrayContent(ms.ToArray());
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
                result.Headers.CacheControl = new CacheControlHeaderValue()
                {
                    NoStore = true,
                    NoCache = true,
                    MustRevalidate = true
                };

                image.Dispose();
                image.Dispose();

                return result;
            }
        }

        #region "壓縮過的照片"
        /// <summary>
        /// 取得案件圖片(壓縮)
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public HttpResponseMessage GetcompressionCallog(string filePath)
        {

            //頭貼路徑
            var path = ServerProfile.GetInstance().CALLOG_PATH;

            //完整路徑
            var img = Image.FromFile(path + filePath);
            Bitmap bit = new Bitmap(img, 100, 150);
            using (MemoryStream ms = new MemoryStream())
            {

                bit.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                result.Content = new ByteArrayContent(ms.ToArray());
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/Jpeg");
                result.Headers.CacheControl = new CacheControlHeaderValue()
                {
                    NoStore = true,
                    NoCache = true,
                    MustRevalidate = true
                };

                img.Dispose();
                ms.Dispose();

                return result;
            }
        }

        /// <summary>
        /// 取得案件圖片(壓縮)
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public HttpResponseMessage GetcompressionCallog(string CompCd, string Sn, byte Seq)
        {

            //頭貼路徑
            var path = ServerProfile.GetInstance().CALLOG_PATH;

            var con = new Conditions<DataBase.TCALIMG>();

            con.And(x => x.Comp_Cd == CompCd &&   //公司別
                                  x.Sn == Sn &&              //叫修案件
                                  x.Seq == Seq);           //序號

            Tcalimg callog = _callogImg.Get(con);
            byte[] imageBytes = callog.CallImage;
            MemoryStream msImg = new MemoryStream(imageBytes, 0,
              imageBytes.Length);

            msImg.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(msImg, true);
            Bitmap bit = new Bitmap(image, 100, 150);

            using (MemoryStream ms = new MemoryStream())
            {

                bit.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
                result.Content = new ByteArrayContent(ms.ToArray());
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/Jpeg");
                result.Headers.CacheControl = new CacheControlHeaderValue()
                {
                    NoStore = true,
                    NoCache = true,
                    MustRevalidate = true
                };

                image.Dispose();
                image.Dispose();

                return result;
            }
        }
        #endregion

        /// <summary>
        /// 測試用的
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage Test()
        {

            try
            {
                HttpMultipartParser.MultipartFormDataParser parser = new HttpMultipartParser.MultipartFormDataParser(HttpContext.Current.Request.InputStream);
                
            }
            catch (Exception ex)
            {
                
                _logger.Error(ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                   $"{ ex.GetType().Name}:message:{ex.Message}");
            }

            return null; 
        }

    }
}

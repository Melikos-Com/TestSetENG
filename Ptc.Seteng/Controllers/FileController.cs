using Ptc.AspnetMvc.Filter;
using Ptc.Logger;
using Ptc.Logger.Service;
using Ptc.Seteng.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Ptc.Seteng.Controllers
{

    public class FileController : BaseController
    {
        private readonly ISystemLog _logger;
        public FileController(ISystemLog Logger)
        {
            _logger = Logger;
        }
        /// <summary>
        /// 取得技師證件照
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public ActionResult GetTechnician(string filePath)
        {
            try
            {
                var url = ServerProfile.GetInstance().TECHNICIAN_PATH;

                var fullPath = url + filePath;

                if (!System.IO.File.Exists(fullPath))
                    fullPath = ServerProfile.GetInstance().NON_FILE_PATH;

                if (string.IsNullOrEmpty(fullPath))
                    throw new NullReferenceException($"no find data");

                return File(fullPath, fullPath.GetContentType());
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


        }

        /// <summary>
        /// 取得技師大頭照
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public ActionResult GetSticker(string filePath)
        {
            try
            {
                var url = ServerProfile.GetInstance().STICKER_PATH;

                var fullPath = url + filePath;

                if (!System.IO.File.Exists(fullPath))
                    fullPath = ServerProfile.GetInstance().NON_FILE_PATH;

             

                if (string.IsNullOrEmpty(fullPath))
                    throw new NullReferenceException($"no find data");


                return File(fullPath, fullPath.GetContentType());
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
       

        }


        /// <summary>
        /// 取得技能證書
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public ActionResult GetLicense(string filePath)
        {
            try
            {
                var url = ServerProfile.GetInstance().LICENSE_PATH ;

                var fullPath = url + filePath;

                if (!System.IO.File.Exists(fullPath))
                    fullPath = ServerProfile.GetInstance().NON_FILE_PATH;

                if (string.IsNullOrEmpty(fullPath))
                    throw new NullReferenceException($"no find data");



                return File(fullPath, fullPath.GetContentType());
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


        }

    }
}
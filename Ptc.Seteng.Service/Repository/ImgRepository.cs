using AutoMapper;
using LinqKit;
using Newtonsoft.Json;
using Ptc.Seteng;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Ptc.Seteng.Repository;
using RequestGetImg;
using Ptc.Logger.Service;

namespace Ptc.Seteng.Repository
{

    public class ImgRepository : IImgRepository
    {  
        private readonly ISystemLog _logger;
        private IProcessCallLogFile _getCallLogFile;
        public ImgRepository(ISystemLog logger)
        {
            _logger = logger;
            _getCallLogFile = new GetCallLogImgForApi();
        }
        /// <summary>
        /// 取得門市店名
        /// </summary>
        public string GetStoreName(string CompCd, string StoreCd)
        {
            DataBase.SETENG_Entities db = new DataBase.SETENG_Entities();
            return db.TSTRMST.Where(x => x.Comp_Cd == CompCd && x.Store_Cd == StoreCd).Select(y => y.Store_Name).FirstOrDefault();
        }
        /// <summary>
        /// 取得案件的待受理技師
        /// </summary>
        public Dictionary<string, string> GetAwaitAdoptTechnician(Tcallog data)
        {
            Dictionary<string, string> resault = new Dictionary<string, string>();
            DataBase.SETENG_Entities db = new DataBase.SETENG_Entities();
            var table = db.TTechnicianGroup.Where(x => x.CompCd == data.CompCd && x.VendorCd == data.VenderCd).ToList();
            if (table.Count == 0)
                return new Dictionary<string, string>();
            else
            {
                table.ForEach(x =>
                {
                    var GroupClaims = db.TTechnicianGroupClaims.Where(y => y.Seq == x.Seq).Select(z => z.Account).ToList();
                    GroupClaims.ForEach(g =>
                    {
                        var Technician = db.TVenderTechnician.Where(q => q.Comp_Cd == data.CompCd && q.Vender_Cd == data.VenderCd && q.Account == g).FirstOrDefault();
                        if (!resault.Keys.Contains(g))
                            resault.Add(g, Technician.RegistrationID);
                    });
                });
                return resault;
            }

        }
        /// <summary>
        /// 取得案件的受理技師
        /// </summary>
        public Dictionary<string, string> GetAcceptTechnician(string CompCd, string Sn)
        {
            Dictionary<string, string> resault = new Dictionary<string, string>();
            DataBase.SETENG_Entities db = new DataBase.SETENG_Entities();
            var table = db.TAcceptedLog.Where(x => x.Comp_Cd == CompCd && x.Sn == Sn).Include(y => y.TCALLOG).FirstOrDefault();
            if (table != null)
            {
                var account = table.Account;
                var Technician = db.TVenderTechnician.Where(x => x.Comp_Cd == CompCd && x.Vender_Cd == table.TCALLOG.Vender_Cd && x.Account == account).FirstOrDefault();
                resault.Add(account, Technician.RegistrationID);
                return resault;
            }
            else
                return new Dictionary<string, string>();
        }
        /// <summary>
        /// 取得設施的設備分類
        /// </summary>
        public string GetSpcAssetKind(string CompCd, string Asset)
        {
            DataBase.SETENG_Entities db = new DataBase.SETENG_Entities();
            var table = db.TASSETS.Where(x => x.Comp_Cd == CompCd && x.Asset_Cd == Asset).FirstOrDefault();
            if (table != null)
                return table.Spc_Asset_Kind;
            else
                return "";
        }

        /// <summary>
        /// 查詢照片
        /// </summary>
        public IEnumerable<string> SearchImg(string CompCd, string Sn, int img)
        {
            List<string> resault = new List<string>();

            DataBase.SETENG_Entities db = new DataBase.SETENG_Entities();
            var table = db.TCALIMG.Where(x => x.Comp_Cd == CompCd && x.Sn == Sn && x.Img_Type == img.ToString());
            if (table.ToList().Count == 0)
                return resault;
            else
            {
                foreach (var FileSeq in table.Select(x => x.File_Seq))
                {
                    var File = db.TUpFile.Where(x => x.File_Seq == FileSeq).FirstOrDefault();
                    var data = _getCallLogFile.GetFile(Sn, CompCd, File.File_Name);
                    resault.Add("data:image/jpeg;base64," + Convert.ToBase64String(data.ImgFile));
                }
            }

            return resault;
        }

        /// <summary>
        /// 新增照片
        /// </summary>
        public void AddImg(Tcallog input)
        {
            DataBase.SETENG_Entities db = new DataBase.SETENG_Entities();
            var resault = db.TCALIMG.Where(x => x.Comp_Cd == input.CompCd && x.Sn == input.Sn);
            if (resault.ToList().Count > 0)
            {
                foreach (var seq in resault.Select(x => x.File_Seq).ToList())
                {
                    var query = db.TUpFile.Where(x => x.File_Seq == seq);
                    foreach (var item in query)
                    {
                        bool success = _getCallLogFile.DelFile(input.Sn, input.CompCd, item.File_Name);
                        if (!success)
                        {
                            _logger.Error("圖片刪除錯誤-檔名: " + item.File_Name);
                        }
                    }
                    db.TUpFile.RemoveRange(query);
                }
                db.TCALIMG.RemoveRange(resault);
                db.SaveChanges();
            }
            foreach (var img in input.ImgBeforeFix)
            {

                int index = 0;
                var table = db.TCALIMG.Where(x => x.Comp_Cd == input.CompCd && x.Sn == input.Sn);
                if (table.ToList().Count == 0)
                    index = 1;
                else
                {
                    index = table.Max(x => x.Seq);
                    index = index + 1;
                }
                DataBase.TUpFile item = new DataBase.TUpFile();
                item.Create_Date = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                item.Create_User = input.AcceptedName;
                item.Doc_Type = "jpg";
                item.File_Name = String.Format("{0}-{1}-{2}.jpg", input.CompCd, input.Sn, index);
                item.Pgm_Id = "手機上傳";
                item.File_Size = 0;
                db.TUpFile.Add(item);
                db.SaveChanges();

                DataBase.TUpFile Upfile = db.TUpFile.Where(x => x.File_Name == item.File_Name).FirstOrDefault();
                DataBase.TCALIMG itemImg = new DataBase.TCALIMG();
                itemImg.Comp_Cd = input.CompCd;
                itemImg.File_Seq = Upfile.File_Seq;
                itemImg.Img_Type = ((int)ImgType.BeforeFix).ToString();
                itemImg.Seq = (byte)index;
                itemImg.Sn = input.Sn;
                db.TCALIMG.Add(itemImg);
                db.SaveChanges();

                uploadImage(img, input.Sn, input.CompCd, item.File_Name);
            }
            foreach (var img in input.ImgAfterFix)
            {
                int index = 0;
                var table = db.TCALIMG.Where(x => x.Comp_Cd == input.CompCd && x.Sn == input.Sn);
                if (table.ToList().Count == 0)
                    index = 1;
                else
                {
                    index = table.Max(x => x.Seq);
                    index = index + 1;
                }
                DataBase.TUpFile item = new DataBase.TUpFile();
                item.Create_Date = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                item.Create_User = input.AcceptedName;
                item.Doc_Type = "jpg";
                item.File_Name = String.Format("{0}-{1}-{2}.jpg", input.CompCd, input.Sn, index);
                item.Pgm_Id = "手機上傳";
                item.File_Size = 0;
                db.TUpFile.Add(item);
                db.SaveChanges();

                DataBase.TUpFile Upfile = db.TUpFile.Where(x => x.File_Name == item.File_Name).FirstOrDefault();
                DataBase.TCALIMG itemImg = new DataBase.TCALIMG();
                itemImg.Comp_Cd = input.CompCd;
                itemImg.File_Seq = Upfile.File_Seq;
                itemImg.Img_Type = ((int)ImgType.AfterFix).ToString();
                itemImg.Seq = (byte)index;
                itemImg.Sn = input.Sn;
                db.TCALIMG.Add(itemImg);
                db.SaveChanges();
                uploadImage(img, input.Sn, input.CompCd, item.File_Name);

            }

            foreach (var img in input.Img)
            {
                int index = 0;
                var table = db.TCALIMG.Where(x => x.Comp_Cd == input.CompCd && x.Sn == input.Sn);
                if (table.ToList().Count == 0)
                    index = 1;
                else
                {
                    index = table.Max(x => x.Seq);
                    index = index + 1;
                }
                DataBase.TUpFile item = new DataBase.TUpFile();
                item.Create_Date = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                item.Create_User = input.AcceptedName;
                item.Doc_Type = "jpg";
                item.File_Name = String.Format("{0}-{1}-{2}.jpg", input.CompCd, input.Sn, index);
                item.Pgm_Id = "手機上傳";
                item.File_Size = 0;
                db.TUpFile.Add(item);
                db.SaveChanges();

                DataBase.TUpFile Upfile = db.TUpFile.Where(x => x.File_Name == item.File_Name).FirstOrDefault();
                DataBase.TCALIMG itemImg = new DataBase.TCALIMG();
                itemImg.Comp_Cd = input.CompCd;
                itemImg.File_Seq = Upfile.File_Seq;
                itemImg.Img_Type = ((int)ImgType.Workorder).ToString();
                itemImg.Seq = (byte)index;
                itemImg.Sn = input.Sn;
                db.TCALIMG.Add(itemImg);
                db.SaveChanges();
                uploadImage(img, input.Sn, input.CompCd, item.File_Name);

            }
            foreach (var img in input.ImgSignature)
            {
                int index = 0;
                var table = db.TCALIMG.Where(x => x.Comp_Cd == input.CompCd && x.Sn == input.Sn);
                if (table.Count() == 0)
                    index = 1;
                else
                {
                    index = table.Max(x => x.Seq);
                    index = index + 1;
                }
                DataBase.TUpFile item = new DataBase.TUpFile();
                item.Create_Date = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                item.Create_User = input.AcceptedName;
                item.Doc_Type = "jpg";
                item.File_Name = String.Format("{0}-{1}-{2}.jpg", input.CompCd, input.Sn, index);
                item.Pgm_Id = "手機上傳";
                item.File_Size = 0;
                db.TUpFile.Add(item);
                db.SaveChanges();

                DataBase.TUpFile Upfile = db.TUpFile.Where(x => x.File_Name == item.File_Name).FirstOrDefault();
                DataBase.TCALIMG itemImg = new DataBase.TCALIMG();
                itemImg.Comp_Cd = input.CompCd;
                itemImg.File_Seq = Upfile.File_Seq;
                itemImg.Img_Type = ((int)ImgType.Signature).ToString();
                itemImg.Seq = (byte)index;
                itemImg.Sn = input.Sn;
                db.TCALIMG.Add(itemImg);
                db.SaveChanges();

                uploadImage(img, input.Sn, input.CompCd, item.File_Name);

            }
        }
        private Boolean uploadImage(string img, string sn, string CompCd, string fileName)
        {
            byte[] bytefile = Convert.FromBase64String(img.Replace("data:image/jpeg;base64,", ""));
            bool success = _getCallLogFile.AddFile(sn, CompCd, fileName, bytefile);
            if (!success)
            {
                _logger.Error("圖片新增錯誤-檔名: " + fileName);
                throw new Exception("圖片新增錯誤-檔名");
            }
            return success;
        }

        /// <summary>
        /// 取得下載連結
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetLink(string index)
        {
            DataBase.SETENG_Entities db = new DataBase.SETENG_Entities();
            if (index == "1") //1=>ios 2=>android
                return db.TAPPVER.Where(x => x.APP == "SETENGII_IOS").Select(Y => Y.Url).FirstOrDefault();
            else
                return db.TAPPVER.Where(x => x.APP == "SETENGII_ANDROID").Select(Y => Y.Url).FirstOrDefault();
        }
    }
}

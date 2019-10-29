using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ptc.Seteng.Models
{
    public class CallogDetailApiViewModel
    {
        string _localPath;

        public CallogDetailApiViewModel()
        {
            this._localPath = ServerProfile.GetInstance().LOCAL_API_SITE;
        }

        //public CallogDetailApiViewModel(Tcallog data, Tvender vender) : this()
        //{
        //    //案件明細

        //}
        public CallogDetailApiViewModel(MobileCallogSearch data) : this()
        {
            //案件明細
            this.Sn = data.Sn;
            this.CompCd = data.CompCd;
            this.StoreName = data.StoreName;
            this.StoreAddress = data.StoreAddr;
            this.StoreTel = data.Telno;
            this.AssetCd = data.AssetCd;
            this.AssetName = data.AssetCd + " " + data.AssetName;
            this.CallLevel = data.CallLevel == "1" ? "普通" : "緊急";
            this.DamageDesc = data.DamageDesc;
            this.FcDate = data.FcDate;
            this.FiDate = data.FiDate.ToString("yyyy/MM/dd HH:mm:ss");
            this.FvDate = data.FvDate;
            this.FdDate = data.FdDate;
            this.CallDesc = data.CallDesc;
            this.RemarkAdd = data.RemarkAdd;
            this.IsAccept = data.AcceptAccount == null ? false : true;
            this.CallName = data.CallerName;
            this.VenderName = "";
            this.VenderCd = data.VenderCd;
            this.FixMark = data.Workdesc;
            this.AcceptedAccount = data.AcceptAccount;
            this.AcceptedName = data.Acceptname;
            this.AcceptDatetime = data.AcceptDatetime?.ToString("yyyy/MM/dd HH:mm:ss");
            this.WorkName = data.WorkName;
            this.Finish_Name = data.Finish_Name;
            this.Mtn_Desc = data.Mtn_Desc;
            this.ArriveDate = data.ArriveDate;
            this.FcDate = data.FcDate;
            if (data.CoffeeCup == null)
                this.CoffeeCup = "";
            else
                this.CoffeeCup = data.CoffeeCup.ToString();
        }
        public CallogDetailApiViewModel(MobileCallogSearch data, IEnumerable<string> BeforeFix, IEnumerable<string> AfterFix, IEnumerable<string> WorkOrder, IEnumerable<string> Signature, string SpcAssetKind) : this()
        {
            //案件明細
            this.Sn = data.Sn;
            this.CompCd = data.CompCd;
            this.StoreName = data.StoreName;
            this.StoreAddress = data.StoreAddr;
            this.StoreTel = data.Telno;
            this.AssetCd = data.AssetCd;
            this.AssetName = data.AssetCd + " " + data.AssetName;
            this.CallLevel = data.CallLevel == "1" ? "普通" : "緊急";
            this.DamageDesc = data.DamageDesc;
            this.FcDate = data.FcDate;
            this.FiDate = data.FiDate.ToString("yyyy/MM/dd HH:mm:ss");
            this.FvDate = data.FvDate;
            this.FdDate = data.FdDate;
            this.CallDesc = data.CallDesc;
            this.RemarkAdd = data.RemarkAdd;
            this.IsAccept = data.AcceptAccount == null ? false : true;
            this.CallName = data.CallerName;
            this.VenderCd = data.VenderCd;
            this.FixMark = data.Workdesc;
            this.AcceptedAccount = data.AcceptAccount;
            this.AcceptedName = data.Acceptname;
            this.AcceptDatetime = data.AcceptDatetime?.ToString("yyyy/MM/dd HH:mm:ss");
            this.WorkName = data.WorkName;
            this.Finish_Name = data.Finish_Name;
            this.Mtn_Desc = data.Mtn_Desc;
            this.ArriveDate = data.ArriveDate;
            this.FcDate = data.FcDate;

            this.ImgBeforeFix = new List<string>();
            foreach (var beforeImg in BeforeFix.ToList())
                this.ImgBeforeFix.Add(beforeImg);

            this.ImgAfterFix = new List<string>();
            foreach (var AfterImg in AfterFix.ToList())
                this.ImgAfterFix.Add(AfterImg);

            this.Img = new List<string>();
            foreach (var WorkOrderImg in WorkOrder.ToList())
                this.Img.Add(WorkOrderImg);

            this.ImgSignature = new List<string>();
            foreach (var SignatureImg in Signature.ToList())
                this.ImgSignature.Add(SignatureImg);

            if (SpcAssetKind == "1") //0.一般設備  1.咖啡機  2: 燈管招牌類  3.AJ濾材
            {
                this.IsCoffeeAsset = true;
                if (data.CoffeeCup == null)
                    this.CoffeeCup = "";
                else
                    this.CoffeeCup = data.CoffeeCup.ToString();
            }
            else
                this.IsCoffeeAsset = false;
        }
        public CallogDetailApiViewModel(MobileCallogSearch data, PagedList<Twrkknd> knd, PagedList<Tfinish> fin, PagedList<Tdammtn> mtn, IEnumerable<string> BeforeFix, IEnumerable<string> AfterFix, IEnumerable<string> WorkOrder, IEnumerable<string> Signature, string SpcAssetKind, Dictionary<RecordType, List<TCallLogDateRecord>> dateRecords) : this()
        {
            //案件明細
            this.Sn = data.Sn;
            this.CompCd = data.CompCd;
            this.StoreName = data.StoreName;
            this.StoreAddress = data.StoreAddr;
            this.StoreTel = data.Telno;
            this.AssetCd = data.AssetCd;
            this.AssetName = data.AssetCd + " " + data.AssetName;
            this.SapAssetNo = data.SapAssetNo;
            this.CallLevel = data.CallLevel == "1" ? "普通" : "緊急";
            this.DamageDesc = data.DamageDesc;
            this.FiDate = data.FiDate.ToString("yyyy/MM/dd HH:mm:ss");
            this.FvDate = data.FvDate;
            this.FdDate = data.FdDate;
            this.CallDesc = data.CallDesc;
            this.RemarkAdd = data.RemarkAdd;
            this.IsAccept = data.AcceptAccount == null ? false : true;
            this.CallName = data.CallerName;
            this.VenderCd = data.VenderCd;
            this.VenderName = data.VenderName;
            this.FixMark = data.Workdesc;
            this.AcceptedAccount = data.AcceptAccount;
            this.AcceptedName = data.Acceptname;
            this.AcceptDatetime = data.AcceptDatetime?.ToString("yyyy/MM/dd HH:mm:ss");
            this.WorkName = data.WorkName;
            this.Finish_Name = data.Finish_Name;
            this.Mtn_Desc = data.Mtn_Desc;
            this.ArriveDate = data.ArriveDate;
            this.FcDate = data.FcDate;
            this.Work = knd.Select(x => new Twrkknd(x));
            this.Finish = fin.Select(x => new Tfinish(x));
            this.Proc = mtn.Select(x => new Tdammtn(x));
            this.Pre_Amt = data.Pre_Amt;

            this.ImgBeforeFix = new List<string>();
            foreach (var beforeImg in BeforeFix.ToList())
                this.ImgBeforeFix.Add(beforeImg);

            this.ImgAfterFix = new List<string>();
            foreach (var AfterImg in AfterFix.ToList())
                this.ImgAfterFix.Add(AfterImg);

            this.Img = new List<string>();
            foreach (var WorkOrderImg in WorkOrder.ToList())
                this.Img.Add(WorkOrderImg);

            this.ImgSignature = new List<string>();
            foreach (var SignatureImg in Signature.ToList())
                this.ImgSignature.Add(SignatureImg);

            if (SpcAssetKind == "1") //0.一般設備  1.咖啡機  2: 燈管招牌類  3.AJ濾材
            {
                this.IsCoffeeAsset = true;
                if (data.CoffeeCup == null)
                    this.CoffeeCup = "";
                else
                    this.CoffeeCup = data.CoffeeCup.ToString();
            }
            else
                this.IsCoffeeAsset = false;

            if (dateRecords != null)
            {
                this.IsStoreScanSI = dateRecords.Keys.Count(x => x == RecordType.ArriveDateNotScan) <= 0;


                this.IsStoreScanSO = dateRecords.Keys.Count(x => x == RecordType.LeaveDateNotScan) <= 0;
                if (this.IsStoreScanSO)
                {
                    List<DateTime> SO = new List<DateTime>();
                    if (dateRecords.Count(x => x.Key == RecordType.LeaveDate) > 0)
                        SO.AddRange(dateRecords[RecordType.LeaveDate].Select(x => x.RecordDate));
                    if (dateRecords.Count(x => x.Key == RecordType.LeaveDateTemp) > 0)
                        SO.AddRange(dateRecords[RecordType.LeaveDateTemp].Select(x => x.RecordDate));
                    this.LeaveDate = SO.Count() > 0 ? SO.Min(x => x).ToString("yyyy/MM/dd HH:mm:ss") : null;
                }

                else
                    this.LeaveDate = dateRecords[RecordType.LeaveDateNotScan].Min(x => x.RecordDate).ToString("yyyy/MM/dd HH:mm:ss");

                this.IsSignature = dateRecords.Keys.Count(x => x == RecordType.SignatureNot) <= 0;

                if (this.IsSignature)
                    this.SignatureDate = dateRecords.Count(x => x.Key == RecordType.Signature) > 0 ? dateRecords[RecordType.Signature].Min(x => x.RecordDate).ToString("yyyy/MM/dd HH:mm:ss") : "";
                else
                    this.SignatureDate = dateRecords[RecordType.SignatureNot].Min(x => x.RecordDate).ToString("yyyy/MM/dd HH:mm:ss");

            }
            else
            {
                this.IsStoreScanSI = true;
                this.IsStoreScanSO = true;
                this.IsSignature = true;
            }
        }

        #region 案件明細

        /// <summary>
        /// 公司編號
        /// </summary>
        public string CompCd { get; set; }
        /// <summary>
        /// 立案時間
        /// </summary>
        public string FiDate { get; set; }

        /// <summary>
        /// 通知時間
        /// </summary>
        public string FvDate { get; set; }

        /// <summary>
        /// 立案人
        /// </summary>
        public string CreateUser { get; set; }
        /// <summary>
        /// 叫修編號
        /// </summary>
        public string Sn { get; set; }
        /// <summary>
        /// 門市代號
        /// </summary>
        public string StoreCd { get; set; }
        /// <summary>
        /// 門市名稱
        /// </summary>
        public string StoreName { get; set; }
        /// <summary>
        /// 門市地址
        /// </summary>
        public string StoreAddress { get; set; }
        /// <summary>
        /// 門市電話
        /// </summary>
        public string StoreTel { get; set; }
        /// <summary>
        /// 設備名稱
        /// </summary>
        public string AssetName { get; set; }
        /// <summary>
        /// 設備代號
        /// </summary>
        public string AssetCd { get; set; }
        /// <summary>
        /// SAP財產編號
        /// </summary>
        public string SapAssetNo { get; set; }
        /// <summary>
        /// 叫修描述
        /// </summary>
        public string CallDesc { get; set; }
        /// <summary>
        /// 補充備註
        /// </summary>
        public string RemarkAdd { get; set; }
        /// <summary>
        /// 故障內容
        /// </summary>
        public string DamageDesc { get; set; }
        /// <summary>
        /// 廠商名稱(簡稱)
        /// </summary>
        public string VenderName { get; set; }
        /// <summary>
        /// 廠商代號
        /// </summary>
        public string VenderCd { get; set; }
        /// <summary>
        /// 受理技師帳號
        /// </summary>
        public string AcceptedAccount { get; set; }
        /// <summary>
        /// 受理技師名稱
        /// </summary>
        public string AcceptedName { get; set; }
        /// <summary>
        /// 是否受理
        /// </summary>
        public bool IsAccept { get; set; }
        /// <summary>
        /// 是否由廠商指派
        /// </summary>
        public Boolean IsVndAssign { get; set; }

        /// <summary>
        /// 可受理技師清單
        /// </summary>
        public List<TechnicianResultApiViewModel> Accounts { get; set; }

        /// <summary>
        /// 叫修等級
        /// </summary>
        public string CallLevel { get; set; }

        /// <summary>
        /// 叫修人
        /// </summary>
        public string CallName { get; set; }

        /// <summary>
        /// 工作類型
        /// </summary>
        public IEnumerable<Twrkknd> Work { get; set; }
        /// <summary>
        /// 銷案類型
        /// </summary>
        public IEnumerable<Tfinish> Finish { get; set; }
        /// <summary>
        /// 處理類型
        /// </summary>
        public IEnumerable<Tdammtn> Proc { get; set; }
        #endregion

        #region 銷案資訊
        /// <summary>
        /// 到店時間
        /// </summary>
        public string ArriveDate { get; set; }
        /// <summary>
        /// 離店時間
        /// </summary>
        public string LeaveDate { get; set; }

        /// <summary>
        /// 完修時間 (目前同離店時間)
        /// </summary>
        public string FcDate { get; set; }
        /// <summary>
        /// 應完成時間
        /// </summary>
        public string FdDate { get; set; }
        /// <summary>
        /// 維修描述
        /// </summary>
        public string FixMark { get; set; }

        /// <summary>
        /// 門市是否掃描到店時間
        /// </summary>
        public Boolean IsStoreScanSI { get; set; }
        /// <summary>
        /// 門市是否掃描離店時間
        /// </summary>
        public Boolean IsStoreScanSO { get; set; }

        /// <summary>
        /// 維修描述
        /// </summary>
        public string AcceptDatetime { get; set; }
        /// <summary>
        /// 維修描述
        /// </summary>
        public string WorkName { get; set; }
        /// <summary>
        /// 維修描述
        /// </summary>
        public string Finish_Name { get; set; }
        /// <summary>
        /// 維修描述
        /// </summary>
        public string Mtn_Desc { get; set; }

        /// <summary>
        /// 案件圖片集合-修理前
        /// </summary>
        public List<string> ImgBeforeFix { get; set; }
        /// <summary>
        /// 案件圖片集合-修理後
        /// </summary>
        public List<string> ImgAfterFix { get; set; }
        /// <summary>
        /// 案件圖片-工單或店章
        /// </summary>
        public List<string> Img { get; set; }
        /// <summary>
        /// 案件圖片-門市簽名
        /// </summary>
        public List<string> ImgSignature { get; set; }
        /// <summary>
        /// 門市是否簽名
        /// </summary>
        public Boolean IsSignature { get; set; }
        /// <summary>
        /// 門市(拒絕)簽名時間
        /// </summary>
        public string SignatureDate { get; set; }

        /// <summary>
        /// 工作類型
        /// </summary>
        public string TypeWorkId { get; set; }
        /// <summary>
        /// 銷案類型
        /// </summary>
        public string TypeFinishId { get; set; }
        /// <summary>
        /// 處理類型
        /// </summary>
        public string TypePorcnoId { get; set; }

        /// <summary>
        /// 設備是否為咖啡設備
        /// </summary>
        public bool IsCoffeeAsset { get; set; }

        /// <summary>
        /// 咖啡杯數
        /// </summary>
        public string CoffeeCup { get; set; }

        /// <summary>
        /// 預估金額
        /// </summary>
        public string Pre_Amt { get; set; }
        #endregion


    }
}
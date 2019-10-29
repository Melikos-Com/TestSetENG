using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ptc.Seteng.Models
{
    public class CallogResultApiViewModel
    {
        public CallogResultApiViewModel()
        {

        }

        public CallogResultApiViewModel(Tcallog data)
        {


        }
        public CallogResultApiViewModel(MobileCallogSearch data, bool Enable)
        {
            this.Sn = data.Sn;
            this.StoreCd = data.StoreCd;
            this.StoreName = data.StoreName;
            this.AssetName = data.AssetName;
            this.DamageDesc = data.DamageDesc;
            this.StoreAddress = data.StoreAddr;
            this.CallDesc = data.CallDesc;
            this.CallLevel = data.CallLevel;
            this.FaDate = data.FaDate;
            this.Enable = Enable;
            this.AcceptDatetime = data.AcceptDatetime?.ToString("yyyy/MM/dd HH:mm:ss");
            this.IsRcvDate = data.RcvDate == null ? false : true;

            this.IsSISOVender = true;


            DateTime now = DateTime.Now;
            if (data.CloseSts == (int)Seteng.CloseSts.process)  //處理中
            {
                DateTime Fadate = Convert.ToDateTime(data.FaDate);

                if (!string.IsNullOrEmpty(data.ArriveDate) && DateTime.TryParse(data.ArriveDate,out DateTime ArriveDate))
                {
                    this.TimeDescription = "已到店";
                }
                else if (data.FvDate == data.FaDate)
                {
                    this.TimeDescription = "無時效";
                }
                else if (Fadate > now)
                {

                    TimeSpan ts = Fadate.Subtract(now);

                    if (ts.Days > 0)
                    {
                        this.TimeDescription = string.Format("{0}:{1}:{2}", ts.Days, ts.Hours, ts.Minutes);
                    }
                    else if (ts.Hours > 0)
                    {
                        this.TimeDescription = string.Format("{0}:{1}", ts.Hours, ts.Minutes);
                    }
                    else
                    {

                        this.TimeDescription = string.Format("{0}分", ts.Minutes);
                    }
                    this.Overtime = true;
                }
                else
                {
                    this.TimeDescription = "逾時";
                    this.Overtime = false;
                }
            }
            else  //已銷案
            {
                if (!string.IsNullOrEmpty(data.ArriveDate) && DateTime.TryParse(data.ArriveDate, out DateTime ArriveDate))
                {
                    #region 有完成時間

                    DateTime FaDate = Convert.ToDateTime(data.FaDate);
                    DateTime ArrDate = Convert.ToDateTime(data.ArriveDate);


                    if (ArrDate > FaDate)
                    {
                        this.TimeDescription = "逾時";
                    }
                    else
                    {
                        this.TimeDescription = "時效內";
                    }
                    this.Overtime = false;
                    #endregion
                }
                else
                {
                    //沒有完成時間，無法計算
                    this.TimeDescription = "無";
                    this.Overtime = false;
                }
            }
            this.Timepoint = data.Timepoint;
        }

        /// <summary>
        /// 維修描述
        /// </summary>
        public string FixMark { get; set; }

        /// <summary>
        /// 案件編號
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
        /// 設備名稱
        /// </summary>
        public string AssetName { get; set; }
        /// <summary>
        /// 故障原因
        /// </summary>
        public string DamageDesc { get; set; }
        /// <summary>
        /// 叫修描述
        /// </summary>
        public string CallDesc { get; set; }
        /// <summary>
        /// 門市地址
        /// </summary>
        public string StoreAddress { get; set; }
        /// <summary>
        /// 叫修時間
        /// </summary>
        public string FiDate { get; set; }
        /// <summary>
        /// 通知時間
        /// </summary>
        public string FvDate { get; set; }
        /// <summary>
        /// 應完成時間
        /// </summary>
        public string FdDate { get; set; }
        /// <summary>
        /// 應到店時間
        /// </summary>
        public string FaDate { get; set; }
        /// <summary>
        /// 到店時間
        /// </summary>
        public string ArriveDate { get; set; }
        /// <summary>
        /// 完成時間
        /// </summary>
        public string FcDate { get; set; }
        /// <summary>
        /// 維修圖片
        /// </summary>
        public IEnumerable<string> Img { get; set; }
        /// <summary>
        /// 時間註記
        /// </summary>
        public string TimeDescription { get; set; }
        /// <summary>
        /// 技師名稱(認養人)
        /// </summary>
        public string AcceptedName { get; set; }
        /// <summary>
        /// 判斷是否逾時
        /// </summary>
        public Boolean Overtime { get; set; }

        /// <summary>
        /// 緊急度
        /// </summary>
        public string CallLevel { get; set; }
        /// <summary>
        /// 案件狀態
        /// </summary>
        public string Timepoint { get; set; }

        /// <summary>
        /// 是否顯示卡片
        /// </summary>
        public Boolean Enable { get; set; }

        /// <summary>
        /// 受理時間
        /// </summary>
        public string AcceptDatetime { get; set; }

        /// <summary>
        /// 是否催修
        /// </summary>
        public bool IsRcvDate { get; set; }

        //是否導入SISO流程廠商
        public bool IsSISOVender { get; set; }

        // 門市是否掃描到店時間  
        public Boolean IsStoreScanSI { get; set; }

        // 門市是否掃描離店時間
        public Boolean IsStoreScanSO { get; set; }
    }
}
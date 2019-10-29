using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    /// <summary>
    /// 門市主檔
    /// </summary>
    public class Tstrmst
    {
        private DateTime _closeDate;

        private decimal _squareMeasure;

        private readonly decimal MaxsquareMeasure = 9999.99m;

        /// <summary>
        /// 公司代號
        /// </summary>
        public string CompCd { get; set; }
        /// <summary>
        /// 店號
        /// </summary>
        public string StoreCd { get; set; }
        /// <summary>
        /// 店名
        /// </summary>
        public string StoreName { get; set; }
        /// <summary>
        /// 開店日
        /// </summary>
        public DateTime OpenDate { get; set; }
        /// <summary>
        /// 改造日
        /// </summary>
        public Nullable<DateTime> RebuildDate { get; set; }
        /// <summary>
        /// 關店日
        /// </summary>
        public DateTime CloseDate
        {

            get
            {

                if (_closeDate == DateTime.MinValue || _closeDate == default(DateTime))
                {
                    return DateTime.MaxValue;
                }

                return _closeDate;
            }
            set
            {

                if (_closeDate == DateTime.MinValue || _closeDate == default(DateTime))
                {
                    value = DateTime.MaxValue;
                }

                value = _closeDate;
            }
        }
        /// <summary>
        /// 區域代號
        /// </summary>
        public string ZoCd { get; set; }
        /// <summary>
        /// 區域名稱
        /// </summary>
        public string ZoName { get; set; }
        /// <summary>
        /// 後勤人員代號
        /// </summary>
        public string AstTeamCd { get; set; }
        /// <summary>
        /// 聯繫人
        /// </summary>
        public string Contacter { get; set; }
        /// <summary>
        /// 郵遞區號
        /// </summary>
        public string ZipNum { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Addr { get; set; }
        /// <summary>
        /// 電話
        /// </summary>
        public string TelNo { get; set; }
        /// <summary>
        /// 行動電話
        /// </summary>
        public string MobileTel { get; set; }
        /// <summary>
        /// 傳真號碼
        /// </summary>
        public string FaxNo { get; set; }
        /// <summary>
        /// 營業時間
        /// </summary>
        public string BusinessHours { get; set; }
        /// <summary>
        /// 備註
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 門市面積
        /// </summary>
        public decimal SquareMeasure
        {
            get { return _squareMeasure; }


            set
            {

                var current = (decimal)Math.Round(_squareMeasure, 2);

                if (current > MaxsquareMeasure) { throw new IndexOutOfRangeException($"out of max value : {MaxsquareMeasure} ."); }

                current = value;

            }

        }
        /// <summary>
        /// 外場
        /// </summary>
        public Boolean OutfieldSts { get; set; }
        /// <summary>
        /// 廁所型態
        /// </summary>
        public ToiletType ToiletSts { get; set; }
        /// <summary>
        /// 店鋪風格
        /// </summary>
        public string StoreStyle { get; set; }
        /// <summary>
        /// 是否有獨立空調
        /// </summary>
        public Boolean IndependAircond { get; set; }
        /// <summary>
        /// 是否有消防設備
        /// </summary>
        public Boolean XiaoFang { get; set; }
        /// <summary>
        /// 消防有效日期
        /// </summary>
        public Nullable<DateTime> XiaoFangDate { get; set; }
        /// <summary>
        /// 門市營運狀態
        /// </summary>
        public StoreOperationType StoreSts { get; set; }
        /// <summary>
        /// 建立人員
        /// </summary>
        public string CreateUser { get; set; }
        /// <summary>
        /// 建立時間
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 更新人員
        /// </summary>
        public string UpdateUser { get; set; }
        /// <summary>
        /// 更新時間
        /// </summary>
        public Nullable<DateTime> UpdateDate { get; set; }
        /// <summary>
        /// 百度座標
        /// </summary>
        public BaiduLocation BaiduLocation { get; set; }
        /// <summary>
        /// 門市類型
        /// </summary>
        public StoreType StoreKind { get; set; }
        /// <summary>
        /// 底下的單位設備
        /// </summary>
        public IEnumerable<Tstrast> TSTRAST { get; set; }
        /// <summary>
        /// 所屬的區域
        /// </summary>
        public Tzocode TZOCODE { get; set; }

    }
}

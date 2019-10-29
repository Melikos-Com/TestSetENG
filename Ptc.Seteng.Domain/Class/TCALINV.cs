using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    public class TCALINV
    {
        /// <summary>
        /// 公司代號
        /// </summary>
        public string CompCd { get; set; }

        /// <summary>
        /// 案件編號 , , , , , , , , , , , , , 
        /// </summary>
        public string Sn { get; set; }
        /// <summary>
        /// 折扣類別
        /// </summary>
        public string Debit_Kind { get; set; }
        /// <summary>
        /// 發票號碼
        /// </summary>
        public string Invoice_No { get; set; }
        /// <summary>
        /// 發票日期
        /// </summary>
        public string Invoice_Date { get; set;}
        /// <summary>
        /// 工作類別
        /// </summary>
        public string Work_Id { get; set; }
        /// <summary>
        /// 預估金額
        /// </summary>
        public int Pre_Amt { get; set; }
        /// <summary>
        /// 費用合計
        /// </summary>
        public int Tot_Amt { get; set; }
        /// <summary>
        /// 會計類別
        /// </summary>
        public string Acc_Type { get; set; }
        /// <summary>
        /// 直營分攤
        /// </summary>
        public int Direct_Amt { get; set; }
        /// <summary>
        /// 加盟分攤
        /// </summary>
        public int Join_Amt { get; set; }
        /// <summary>
        /// 公司分攤
        /// </summary>
        public int Comp_Amt { get; set; }
        /// <summary>
        /// 轉財務年月
        /// </summary>
        public int Trans_YM { get; set; }
        /// <summary>
        /// 輸入者代號
        /// </summary>
        public string Create_User_Id { get; set; }
        /// <summary>
        /// 修改人員
        /// </summary>
        public string Update_User { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        public string Update_Date { get; set; }

    }
}

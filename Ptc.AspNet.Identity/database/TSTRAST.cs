//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ptc.AspNet.Identity.database
{
    using System;
    using System.Collections.Generic;
    
    public partial class TSTRAST
    {
        public long Upd_Seq { get; set; }
        public string Comp_Cd { get; set; }
        public string Store_Cd { get; set; }
        public string Asset_Cd { get; set; }
        public string Asset_No { get; set; }
        public string Sap_Asset_No { get; set; }
        public string Vender_Cd { get; set; }
        public decimal Begin_YM { get; set; }
        public string Renew_Type { get; set; }
        public decimal Renew_YM { get; set; }
        public string Del_Sts { get; set; }
        public decimal Spc_YF_Seq { get; set; }
        public decimal Nor_YF_Note { get; set; }
        public string Mod_Reason { get; set; }
        public string Trans_Date { get; set; }
        public string Update_User { get; set; }
        public string Update_Date { get; set; }
        public string Machine_Type { get; set; }
    
        public virtual TLVCREP TLVCREP { get; set; }
    }
}
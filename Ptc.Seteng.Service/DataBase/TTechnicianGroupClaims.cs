//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ptc.Seteng.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class TTechnicianGroupClaims
    {
        public int Seq { get; set; }
        public string CompCd { get; set; }
        public string VendorCd { get; set; }
        public string Account { get; set; }
    
        public virtual TTechnicianGroup TTechnicianGroup { get; set; }
        public virtual TVenderTechnician TVenderTechnician { get; set; }
    }
}

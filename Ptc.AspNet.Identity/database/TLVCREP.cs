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
    
    public partial class TLVCREP
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TLVCREP()
        {
            this.TLVRHST = new HashSet<TLVRHST>();
        }
    
        public long Upd_Seq { get; set; }
        public string Comp_Cd { get; set; }
        public string Store_Cd { get; set; }
        public string Asset_Cd { get; set; }
        public Nullable<System.DateTime> Last_Date { get; set; }
        public Nullable<System.DateTime> Work_Date { get; set; }
        public Nullable<System.DateTime> Next_Date { get; set; }
        public string Abnormal { get; set; }
        public Nullable<bool> Check_Sts { get; set; }
        public Nullable<bool> Close_Sts { get; set; }
        public string Remark { get; set; }
        public bool Rec_Sts { get; set; }
        public string Update_User { get; set; }
        public Nullable<System.DateTime> Update_Date { get; set; }
        public bool Prj_Sts { get; set; }
        public Nullable<System.DateTime> Prj_Date { get; set; }
    
        public virtual TLVKIND TLVKIND { get; set; }
        public virtual TSTRAST TSTRAST { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TLVRHST> TLVRHST { get; set; }
    }
}

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
    
    public partial class TETCINV
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TETCINV()
        {
            this.TETCDTL = new HashSet<TETCDTL>();
        }
    
        public string TransactionNo { get; set; }
        public string TransactionDate { get; set; }
        public string CategoryNo { get; set; }
        public string BuyerIdentifier { get; set; }
        public string BuyerName { get; set; }
        public string SellerIdentifier { get; set; }
        public string SellerName { get; set; }
        public string MainRemark { get; set; }
        public decimal SalesAmount { get; set; }
        public decimal FreeTaxSalesAmount { get; set; }
        public decimal ZeroTaxSalesAmount { get; set; }
        public decimal TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
        public int TaxType { get; set; }
        public decimal TotalAmount { get; set; }
        public string InvoiceType { get; set; }
        public bool BuyerDonateMark { get; set; }
        public bool Pro_Sts { get; set; }
        public string Update_User { get; set; }
        public Nullable<System.DateTime> Update_Date { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TETCDTL> TETCDTL { get; set; }
    }
}
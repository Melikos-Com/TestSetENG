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
    
    public partial class TUpFile
    {
        public long File_Seq { get; set; }
        public byte[] Upload_File { get; set; }
        public string Doc_Type { get; set; }
        public string File_Name { get; set; }
        public decimal File_Size { get; set; }
        public string Pgm_Id { get; set; }
        public string Create_User { get; set; }
        public string Create_Date { get; set; }
    }
}

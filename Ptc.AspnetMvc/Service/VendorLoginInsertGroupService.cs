using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.AspnetMvc.Authentication.Service
{
    public interface VendorLoginInsertGroupService
    {

        /// <summary>
        /// 廠商登入時，自動新增預設群組
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Boolean AddVendorOfGroup(UserBase data);
    }
}

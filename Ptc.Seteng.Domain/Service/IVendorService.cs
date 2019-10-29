using Ptc.AspnetMvc.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng.Service
{
    public interface IVendorService
    {

        /// <summary>
        /// 新增群組資訊
        /// </summary>
        /// <param name="Data"></param>
        /// <param name="Accounts"></param>
        /// <returns></returns>
        Boolean CreateTechnicianGroup(TtechnicianGroup Data,
                                      String[] Accounts);

        /// <summary>
        /// 更新群組資訊
        /// </summary>
        /// <param name="Data"></param>
        /// <param name="Accounts"></param>
        /// <returns></returns>
        Boolean UpdateTechnicianGroup(TtechnicianGroup Data, 
                                      String[] Accounts);
        /// <summary>
        /// 單一加入技師至群組
        /// </summary>
        /// <param name="Data"></param>
        /// <param name="Accounts"></param>
        /// <returns></returns>
        Boolean AddTechnicianGroup(TtechnicianGroup Data,string accounts);

        /// <summary>
        /// 自動建立廠商群組
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Boolean CreateVendorGroup(UserBase uesr);
    }
}

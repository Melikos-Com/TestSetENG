using Ptc.AspnetMvc.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ptc.Seteng
{
    public interface ICallogService
    {
        /// <summary>
        /// 自動通知案件
        /// </summary>
        /// <param name="Sn"></param>
        /// <returns></returns>
        Boolean AutoNotification(string Comp_Cd,string Sn);


        /// <summary>
        /// 廠商通知技師可認養
        /// </summary>
        /// <param name="log"></param>
        /// <param name="accounts"></param>
        /// <returns></returns>
        Boolean VenderNotification(Tcallog log, List<string> accounts);

        /// <summary>
        /// 技師認養案件
        /// </summary>
        /// <param name="log"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        Boolean TechnicianAccept(Tcallog log, string account,Boolean isVndAssign, string username);

        /// <summary>
        /// 廠商改派案件
        /// </summary>
        /// <param name="log"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        Boolean VendorChangeLog(Tcallog log, string account, string username);

        /// <summary>
        /// 技師暫存案件
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        Boolean VendorScratch(Tcallog log);
        /// <summary>
        /// 技師銷案
        /// </summary>
        Boolean VendorConfirm(Tcallog input);

        /// <summary>
        /// 取得新增案件數
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        Tuple<int, int, int> VendorNewsCount(UserBase User);

        /// <summary>
        /// 催修通知
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Boolean PushUrg(Tcallog data);
        /// <summary>
        /// 銷案通知
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Boolean PushFinish(Tcallog data);
        /// <summary>
        /// 多案件、多技師推播
        /// </summary>
        /// <param name="Sn"></param>
        /// <param name="Account"></param>
        /// <returns>從Web進行通知</returns>
        Boolean NotificationForWeb(UserBase user,List<string> Sn, Dictionary<string, string> Account);

        /// <summary>
        /// 多案件、單一技師推播(指派)
        /// </summary>
        /// <param name="user"></param>
        /// <param name="Sn"></param>
        /// <param name="Account"></param>
        /// <returns></returns>
        Boolean NotificationForAppoint(UserBase user, List<string> Sn, List<string> Account);

        /// <summary>
        /// 多案件、單一技師推播
        /// </summary>
        /// <param name="Sn"></param>
        /// <param name="Account"></param>
        /// <returns>從Web進行通知</returns>
        Boolean ChangeNotificationForWeb(UserBase user, List<string> Sn, TvenderTechnician Techniciandata, Dictionary<string, string> OldAccount);

    }
}

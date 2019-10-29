using Ptc.AspnetMvc.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Ptc.Seteng.Service
{
    public interface ITechnicianService
    {

        /// <summary>
        /// 認可技師資訊
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Boolean ConfirmTechnician(TvenderTechnician data);

        /// <summary>
        /// 新增技師資訊
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Boolean CreateTechnician(TvenderTechnician data);

        /// <summary>
        /// 更新技師資訊
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Boolean UpdateTechnician(TvenderTechnician data);

        /// <summary>
        /// 合併所選擇的技師
        /// </summary>
        /// <param name="technicians"></param>
        /// <param name="groups"></param>
        /// <returns></returns>
        List<TvenderTechnician> MergeTechnician(IEnumerable<TvenderTechnician> technicians, 
                                                IEnumerable<TtechnicianGroup> groups);

    }
}

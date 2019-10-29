
using Ptc.AspnetMvc.Authentication;
using System.Collections.Generic;

namespace Ptc.Spcc.CCEng.Service
{
    public interface IDataRangeService

    {
        List<string> GetCondition(UserBase uesr) ;
    }
}

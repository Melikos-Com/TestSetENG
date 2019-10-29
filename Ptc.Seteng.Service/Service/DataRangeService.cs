using Ptc.AspnetMvc.Authentication;
using System;
using System.Collections.Generic;

namespace Ptc.Spcc.CCEng.Service
{
    public class DataRangeService : IDataRangeService
    {
        public List<string> GetCondition(UserBase uesr)
        {
           

            RoleType roleid;

            List<string> datarange = new List<string>();

            if (Enum.TryParse(uesr.RoleId, out roleid))
            {

                switch (roleid)
                {
                    case RoleType.Admin:
                        break;
                    case RoleType.TL:
                        datarange = uesr.DataRange.ZoCd;
                        break;
                    case RoleType.FM:
                        datarange = uesr.DataRange.ZoCd;
                        break;
                    case RoleType.CM:
                        datarange.Add(uesr.UserId);
                        break;
                    case RoleType.Store:
                        datarange = uesr.DataRange.StoreCd;
                        break;
                    case RoleType.Vender:
                        datarange = uesr.DataRange.CompCd;
                        break;
                    default:
                        break;
                }
            }
            else
            {

            }
            return datarange;
        }
    }
}

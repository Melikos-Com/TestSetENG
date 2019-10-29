using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ptc.AspnetMvc.Authentication
{
    public enum AuthNodeType
    {
        All = 0,
        Create = 1,
        Read = 2,
        Edit = 4,
        Delete = 8,
        Export = 16,
        Report = 32
    }
}
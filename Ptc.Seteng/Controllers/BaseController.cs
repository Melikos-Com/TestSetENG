using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPaging;
using Ptc.AspNetMvc;
using Ptc.AspnetMvc.Models;
using System.Web.Http;

namespace Ptc.Seteng.Controllers
{
    public class BaseController : Controller
    {
        public BaseController() { }

        public PtcIdentity LoginUserInfo
        {
            get
            {
                return ((PtcIdentity)this.User.Identity);
            }
        }
  
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DevOpsConf2016.Controllers
{
    public abstract class ControllerBase : Controller
    {
        // GET: ControllerBase
        protected virtual  CustomPrincipal User
        {
            get { return HttpContext.User as CustomPrincipal; }
        }
    }
}
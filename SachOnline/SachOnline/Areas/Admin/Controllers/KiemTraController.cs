using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SachOnline.Areas.Admin.Controllers
{
    public class KiemTraController : Controller
    {
        // GET: Admin/KiemTra
        public KiemTraController()
        {
            if (System.Web.HttpContext.Current.Session["Admin"]==null)
            {
                System.Web.HttpContext.Current.Response.Redirect("~/Admin/Login");
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrangBanHang.Ultils;

namespace TrangBanHang.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        // GET: Admin/HomeAdmin
        public ActionResult Index()
        {
			if (UserSessionHelper.checkSession(Constant.ROLE_EMPLOYEE) || UserSessionHelper.checkSession(Constant.ROLE_ADMIN))
			{
				return View();
			}
			else
			{
				return Redirect("/");
			}
		}
    }
}
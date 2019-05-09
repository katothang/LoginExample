using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrangBanHang.Ultils;

namespace TrangBanHang.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			if (UserSessionHelper.checkSession(Constant.ROLE_EMPLOYEE) || UserSessionHelper.checkSession(Constant.ROLE_ADMIN))
			{
				return Redirect("/Admin/HomeAdmin");
			}
			else if (UserSessionHelper.checkSession(Constant.ROLE_USER))
			{
				return Redirect("User/HomeUser");
			}
			else
			{
				return Redirect("User/HomeUser");
			}
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrangBanHang.Models;
using TrangBanHang.Ultils;

namespace TrangBanHang.Areas.User.Controllers
{
    public class HomeUserController : Controller
    {
        // GET: User/HomeUser
        public ActionResult Index()
        {
			if (UserSessionHelper.checkSession(Constant.ROLE_USER))
			{
				return View();
			}
			else
			{
				return View();
			}
		}

		public ActionResult updateThongTinUser()
		{
			Employee employee = new Employee();

			return View(employee);
		}

		[HttpPost]
		public ActionResult updateThongTinUser(Employee employee)
		{
			string fileName = Path.GetFileNameWithoutExtension(employee.ImageFile.FileName);
			string extension = Path.GetExtension(employee.ImageFile.FileName);
			fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
			employee.Avatar = fileName;
			//fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
			//employee.ImageFile.SaveAs(fileName);
			try
			{
				using (DataWebManagersEntities update = new DataWebManagersEntities())
				{
					update.Employees.AddOrUpdate(employee);
					update.SaveChanges();
				}

				ModelState.Clear();
				ViewBag.SuccessMessage = "tạo thành công";
				return View(new Employee());
			}
			catch
			{
				ViewBag.SuccessMessage = "Vui lòng điền thông tin chính xác";
				return View(new Employee());
			}
		}

	}
}
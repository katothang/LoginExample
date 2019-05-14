using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrangBanHang.Models;
using TrangBanHang.Ultils;

namespace TrangBanHang.Controllers
{
	public class EmployeeController : Controller
	{
		DataWebManagersEntities db = new DataWebManagersEntities();
		DataWebManagersEntitiesCustommer dbCustomer = new DataWebManagersEntitiesCustommer();

		// GET: Employee
		public ActionResult DangKi(int id = 0)
		{
			Employee employee = new Employee();

			return View(employee);
		}

		// GET: Employee\
		[HttpPost]
		public ActionResult DangKi(Employee employee)
		{
			if (UserSessionHelper.checkSession(Constant.ROLE_ADMIN))
			{
				//string fileName = Path.GetFileNameWithoutExtension(file.FileName);
				//string extension = Path.GetExtension(file.FileName);
				//fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
				//employee.Avatar = fileName;

				employee.RoleId = 3;
				employee.Avatar = "abc";
				//fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
				//employee.ImageFile.SaveAs(fileName);
				try
				{
					db.Employees.Add(employee);
					db.SaveChanges();
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
			else
			{
				//string fileName = Path.GetFileNameWithoutExtension(file.FileName);
				//string extension = Path.GetExtension(file.FileName);
				//fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
				//employee.Avatar = fileName;
				
				employee.RoleId = 1;
				employee.Avatar = "abc";
				//fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
				//employee.ImageFile.SaveAs(fileName);
				try
				{
					Customer customer = new Customer();
					customer.UserName = employee.UserName;
					customer.Password = employee.Password;
					customer.FullName = employee.FullName;
					customer.Email = employee.Email;
					customer.Phone = employee.Phone;
					customer.Address = employee.Address;
					customer.Birthday = employee.Birthday;
					customer.Avatar = employee.Avatar;
					dbCustomer.Customers.Add(customer);
					//db.Employees.Add(employee);
					dbCustomer.SaveChanges();
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

		public ActionResult DangNhap()
		{
			try
			{
				if (UserSessionHelper.checkSession(Constant.ROLE_NONE) == false)
				{
					return View("DangNhap");
				}
				else
				{
					return Redirect("/");

				}
			}
			catch
			{
				return View();
			}
		}

		public ActionResult Logout()
		{
			try
			{
					UserSessionHelper.destroyUserSession();
					return Redirect("/");
			}
			catch
			{
				return Redirect("/");
			}
		}

		// GET: Employee\


		[HttpPost]
		public ActionResult DangNhap(string username, string password)
		{
			try
			{
				if(UserSessionHelper.checkSession(Constant.ROLE_NONE) == false)
				{
					UserSession iDuser = UserSessionHelper.getSession();
					var userLogin = db.Employees.Where(a => a.UserName == username && a.Password == password).SingleOrDefault();
					var userLoginCustomer = dbCustomer.Customers.Where(a => a.UserName == username && a.Password == password).SingleOrDefault();
					if (userLogin != null)
					{
						UserSession userSession = new UserSession();
						userSession.id = userLogin.Id;
						userSession.userName = userLogin.UserName;
						userSession.userRole = userLogin.RoleId;
						userSession.userFullName = userLogin.FullName;
						userSession.userAvatar = userLogin.Avatar;
						UserSessionHelper.setSession(userSession);
						return Redirect("/");

					}
					else if(userLoginCustomer != null)
					{
						UserSession userSession = new UserSession();
						userSession.id = userLoginCustomer.Id;
						userSession.userName = userLoginCustomer.UserName;
						userSession.userRole = 3;
						userSession.userFullName = userLoginCustomer.FullName;
						userSession.userAvatar = userLoginCustomer.Avatar;
						UserSessionHelper.setSession(userSession);
						return Redirect("/");
					}
					else
					{
						ViewBag.Error = "Tên đăng nhập hoặc mật khẩu không đúng!!";
						return View();
					}

				}
				return View();
			}
			catch
			{
				return View();
			}
			
		}


		public ActionResult showThongTin()
		{
			try
			{
			
				UserSession userSession = UserSessionHelper.getSession();
                var userLogin = db.Employees.Where(a => a.Id == userSession.id).SingleOrDefault();
               
                if (userLogin == null)
                {
                    userLogin = new Employee();
                    var userLoginCustommer = dbCustomer.Customers.Where(a => a.Id == userSession.id).SingleOrDefault();
                    userLogin.FullName = userLoginCustommer.FullName;
                    userLogin.Address = userLoginCustommer.Address;
                    userLogin.Avatar = userLoginCustommer.Avatar;
                    userLogin.BankAccount = userLoginCustommer.FullName;
                    userLogin.Birthday = userLoginCustommer.Birthday;
                    userLogin.Email = userLoginCustommer.Email;
                    userLogin.UserName = userLoginCustommer.UserName;
                    userLogin.Phone = userLoginCustommer.Phone;
                    if (userLogin.Avatar.Equals("abc"))
                        userLogin.Avatar = "defaultAvata.png";
                    return View(userLogin);
                }
                else
                {
                    if (userLogin.Avatar.Equals("abc"))
                        userLogin.Avatar = "defaultAvata.png";
                    return View(userLogin);
                }
				
			}
			catch
			{
				return Redirect("/");
			}
		}

		public ActionResult updateThongTin()
		{
			Employee employee = new Employee();

			return View(employee);
		}

		[HttpPost]
		public ActionResult updateThongTin(Employee employee)
		{
			if (UserSessionHelper.checkSession(Constant.ROLE_ADMIN) || UserSessionHelper.checkSession(Constant.ROLE_USER) || UserSessionHelper.checkSession(Constant.ROLE_EMPLOYEE))
			{
                string fileName = Path.GetFileNameWithoutExtension(employee.ImageFile.FileName);
                string extension = Path.GetExtension(employee.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
				employee.Avatar = fileName;
				fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
                employee.ImageFile.SaveAs(fileName);

                UserSession userSession = UserSessionHelper.getSession();

			
				
				try
				{
                    if(UserSessionHelper.checkSession(Constant.ROLE_ADMIN) || UserSessionHelper.checkSession(Constant.ROLE_EMPLOYEE))
                    {

                        using (DataWebManagersEntities update = new DataWebManagersEntities())
                        {
                            var userLogin = update.Employees.Where(a => a.Id == userSession.id).SingleOrDefault();
                            userLogin.FullName = employee.FullName;
							userLogin.Avatar = employee.Avatar;
							userLogin.Address = employee.Address;
                            userLogin.BankAccount = employee.BankAccount;
                            userLogin.Birthday = employee.Birthday;
                            userLogin.Email = employee.Email;
                            update.Employees.AddOrUpdate(userLogin);
                            update.SaveChanges();
                        }

                        ModelState.Clear();
                        ViewBag.SuccessMessage = "update thành công";
                        return View();
                    }
                    else
                    {
                        using (DataWebManagersEntitiesCustommer updateCustomer = new DataWebManagersEntitiesCustommer())
                        {
                            var userLoginCustomer = updateCustomer.Customers.Where(a => a.Id == userSession.id).SingleOrDefault();
                            userLoginCustomer.FullName = employee.FullName;
                            userLoginCustomer.Password = employee.Password;
                            userLoginCustomer.Address = employee.Address;
                            userLoginCustomer.Avatar = employee.Avatar;
                            userLoginCustomer.Birthday = employee.Birthday;
                            userLoginCustomer.Email = employee.Email;
                            updateCustomer.Customers.AddOrUpdate(userLoginCustomer);
                            updateCustomer.SaveChanges();
                        }

                        ModelState.Clear();
                        ViewBag.SuccessMessage = "update thành công";
                        return View();
                    }
					
				}
				catch
				{
					ViewBag.SuccessMessage = "Vui lòng điền thông tin chính xác";
					return View();
				}
			}
			else
			{
				return Redirect("/");
			}
			
			 
		}

	}
}
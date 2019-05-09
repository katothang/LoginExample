using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrangBanHang.Ultils
{
	public class UserSessionHelper
	{

		public static void setSession(UserSession user)
		{

			HttpContext.Current.Session["USER"] = user;
		}
		public static UserSession getSession()
		{
			var session = HttpContext.Current.Session["USER"];
			if (session != null)
			{
				return session as UserSession;
			}

			return null;
		}

		public static Boolean checkSession(int role)
		{
			UserSession userSession = getSession();
			if (userSession != null)
			{
				if (userSession.userRole == role || role == Constant.ROLE_NONE)
				{
					return true;
				}

			}
			return false;
		}

		public static void destroyUserSession()
		{
			HttpContext.Current.Session["USER"] = null;
		}
	}
}
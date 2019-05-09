using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrangBanHang.Ultils
{
	public class UserSession
	{
		public int id { get; set; }
		public string userName { get; set; }
		public string userFullName { get; set; }

		public string userAvatar { get; set; }

		public int userRole { get; set; }
	}
}
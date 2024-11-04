using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWEB.Areas.Admin.Controllers
{
    public class LogoutController : Controller
    {
        // GET: Admin/Logout
        public ActionResult DangXuat()
        {
            Session.Clear();
            Session.Abandon();

            // Điều hướng người dùng về trang chủ hoặc trang đăng nhập
            return RedirectToAction("DangNhap", "Auth", new { area = "Admin" });

        }
    }
}
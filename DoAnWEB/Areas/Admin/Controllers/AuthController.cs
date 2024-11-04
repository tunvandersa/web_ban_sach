using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWEB.Models;
namespace DoAnWEB.Areas.Admin.Controllers
{
    public class AuthController : Controller
    {
        // GET: Admin/Auth
        dbSachEntities db = new dbSachEntities();
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(string TaiKhoan, string MatKhau)
        {
           
            var foundUser = db.TaiKhoan.FirstOrDefault(item => item.Username == TaiKhoan && item.Password == MatKhau);
            var kh = db.KhachHang.FirstOrDefault(i => i.Username == TaiKhoan);
            if (foundUser != null)
            {
                Session["username"] = foundUser.Username;
                Session["KhachHang"] = kh;
                if (foundUser.Quyen == "admin")
                {
                    Session["Quyen"] = "admin";
                    return RedirectToAction("Index", "Sach", new { area = "Admin" });
                }
                else
                {
                    if(foundUser.Quyen == "user")
                    {
              
                        Session["Quyen"] = "user";
                        return RedirectToAction("Index", "TrangChu", new { area = "User" });
                    }                        
                }
            }
            
                ViewBag.Error = "Tên đăng nhập hoặc mật khẩu không đúng";
                return RedirectToAction("DangNhap", "Auth", new { area = "Admin" });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWEB.Models;
namespace DoAnWEB.Areas.Admin.Controllers
{
    public class DangKiController : Controller
    {
        // GET: Admin/DangKi
        dbSachEntities db = new dbSachEntities();
        public bool CheckUserName(string userName)
        {
            return db.TaiKhoan.Count(x => x.Username == userName) > 0;
        }
        [HttpGet]
        public ActionResult DangKi()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult DangKi(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                if (CheckUserName(model.Username))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
                    return View(model);
                }
                else
                {
                    if (model.Password != model.ConfirmPassword)
                    {
                        ModelState.AddModelError("", "Mật khẩu nhập lại không giống!");
                        return View(model);
                    }
                    else
                    {
                        var tk = new TaiKhoan
                        {
                            Username = model.Username,
                            Password = model.Password,
                            Quyen = "user",
                        };
                        try
                        {
                            db.TaiKhoan.Add(tk);
                            db.SaveChanges();
                            ViewBag.Result = "true";
                            return View(model);
                        }
                        catch
                        {
                            ViewBag.Result = "false";
                            return View(model);
                        }
                    }
                }
            }
            else
                return View(model);
        }
    }
}
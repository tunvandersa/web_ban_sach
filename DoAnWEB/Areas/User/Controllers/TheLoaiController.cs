using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWEB.Models;

namespace DoAnWEB.Areas.User.Controllers
{
    public class TheLoaiController : Controller
    {
        // GET: User/TheLoai
        private dbSachEntities db = new dbSachEntities();
        public ActionResult TheLoai(int id)
        {
            var theloai = db.TheLoai.Find(id);
            ViewBag.Titles = theloai.TenTL;
            var sach = db.Sach.Where(s => s.MaTL == theloai.MaTL).ToList();
            return View(sach);
        }
    }
}
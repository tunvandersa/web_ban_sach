using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWEB.Models;
using System.Data.Entity;

namespace DoAnWEB.Areas.User.Controllers
{
    public class TrangchuController : Controller
    {
        // GET: User/Trangchu
        private dbSachEntities db = new dbSachEntities();
        public ActionResult Index()
        {

            ViewBag.Titles = "Trang chủ";
            ViewBag.theloai = db.TheLoai.ToList();
            var sachList = db.Sach.ToList();
            var randomSachList = sachList.OrderBy(x => Guid.NewGuid()).Take(8).ToList();
            ViewBag.SachGoiY = randomSachList;
            return View();
        }
        [HttpPost]
        public ActionResult TimKiem(string searchString)
        {
 
            var sachs = db.Sach.Include(b => b.TacGia).Include(b => b.TheLoai).Include(b => b.NhaXuatBan);
            if (!String.IsNullOrEmpty(searchString))
            {

                searchString = searchString.ToLower();
                sachs = sachs.Where(b => b.TenSach.ToLower().Contains(searchString)
                                    || b.TacGia.TenTacGia.ToLower().Contains(searchString));
            }
            return View(sachs);
        }
    }
}
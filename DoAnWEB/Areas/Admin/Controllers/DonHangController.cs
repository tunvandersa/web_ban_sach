using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWEB.Models;
namespace DoAnWEB.Areas.Admin.Controllers
{
    public class DonHangController : Controller
    {
        // GET: Admin/DonHang
        dbSachEntities db = new dbSachEntities();
        public ActionResult Index()
        {
            var hoadon = db.DonHang.OrderByDescending(s => s.MaDonHang).ToList();
            return View(hoadon);
        }
    }
}
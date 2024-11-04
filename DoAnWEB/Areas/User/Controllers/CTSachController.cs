using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWEB.Models;

namespace DoAnWEB.Areas.User.Controllers
{
    public class CTSachController : Controller
    {
        // GET: User/CTSach
        dbSachEntities db = new dbSachEntities();
        public ActionResult Sach (int id )
        {
            ViewBag.Titles = "Thông tin sách";
            var sach = db.Sach.FirstOrDefault(s => s.MaSach == id);
            return View(sach);
        }
        
        }
    }

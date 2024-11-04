using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWEB.Models;

namespace DoAnWEB.Areas.Admin.Controllers
{
    public class TacGiaController : Controller
    {
        // GET: Admin/TacGia
        private dbSachEntities db = new dbSachEntities();
        public ActionResult Index()
        {
            var lst = db.TacGia.ToList();
            return View(lst);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(TacGia tacgia)
        {
            db.TacGia.Add(tacgia);
            db.SaveChanges();
            TempData["Message"] = "Thêm mới thành công";
            return RedirectToAction("Index");
        }
        public ActionResult Edit(  int id)
        {
            TacGia tacgia = db.TacGia.Where(tg => tg.MaTacGia == id).FirstOrDefault();
            return View(tacgia);
        }
        [HttpPost]
        public ActionResult Edit ( TacGia tacgia)
        {
            TacGia tacgia1 = db.TacGia.Where(tg => tg.MaTacGia == tacgia.MaTacGia).FirstOrDefault();
            tacgia1.MaTacGia = tacgia.MaTacGia;
            tacgia1.TenTacGia = tacgia.TenTacGia;
            tacgia1.TieuSu = tacgia.TieuSu;
            db.SaveChanges();
            TempData["Message"] = "Chỉnh sửa thành công";
            return View(tacgia1);
        }
            public ActionResult Delete (int id)
            {
                using (var context = new dbSachEntities())
                {
                    var data = context.TacGia.Find(id);
                    if (data != null)
                    {
                        try
                        {
                            context.TacGia.Remove(data);
                            context.SaveChanges();
                       
                            TempData["Message"] = "Xóa thành công";
                        }
                        catch (Exception ex)
                        {
                            TempData["Error"] = "Lỗi khi xóa: " + ex.Message;
                        
                        }
                    }
                    else
                        return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
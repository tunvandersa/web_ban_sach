using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWEB.Models;
namespace DoAnWEB.Areas.Admin.Controllers
{
    public class TheLoaiController : Controller
    {
        // GET: Admin/TheLoai
        dbSachEntities db = new dbSachEntities();
        public ActionResult Index()
        {
            var lst = db.TheLoai.ToList();
            return View(lst);

        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(TheLoai theloai)
        {
            db.TheLoai.Add(theloai);
            db.SaveChanges();
            TempData["Message"] = "Thêm mới thành công";
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            TheLoai theloai = db.TheLoai.Where(tg => tg.MaTL == id).FirstOrDefault();
            return View(theloai);
        }
        [HttpPost]
        public ActionResult Edit(TheLoai theloai)
        {
            TheLoai theloai1 = db.TheLoai.Where(row => row.MaTL == theloai.MaTL).FirstOrDefault();
            theloai1.MaTL = theloai.MaTL;
            theloai1.TenTL = theloai.TenTL;

            db.SaveChanges();
            TempData["Message"] = "Chỉnh sửa thành công";
            return View(theloai1);
        }
        public ActionResult Delete(int id)
        {
            using (var context = new dbSachEntities())
            {
                var data = context.TheLoai.Find(id);
                if (data != null)
                {
                    try
                    {
                        context.TheLoai.Remove(data);
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
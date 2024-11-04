using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWEB.Models;
namespace DoAnWEB.Areas.Admin.Controllers
{
    public class NXBController : Controller
    {
        // GET: Admin/NXB
        private dbSachEntities db = new dbSachEntities();
        public ActionResult Index()
        {

            var lst = db.NhaXuatBan.ToList();
            return View(lst);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(NhaXuatBan nxb)
        {
            db.NhaXuatBan.Add(nxb);
            db.SaveChanges();
            TempData["Message"] = "Thêm mới thành công";
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int? id)
        {
            NhaXuatBan nxb = db.NhaXuatBan.Where(n => n.MaNXB == id).FirstOrDefault();
            return View(nxb);
        }
        [HttpPost]
        public ActionResult Edit(NhaXuatBan nhaxuatban)
        {
            NhaXuatBan nhaxuatban1 = db.NhaXuatBan.Where(row => row.MaNXB == nhaxuatban.MaNXB).FirstOrDefault();
            nhaxuatban1.MaNXB = nhaxuatban.MaNXB;
            nhaxuatban1.TenNXB = nhaxuatban.TenNXB;
            nhaxuatban1.DiaChi = nhaxuatban.DiaChi;
            nhaxuatban1.DienThoai = nhaxuatban.DienThoai;

            db.SaveChanges();
            TempData["Message"] = "Chỉnh sửa thành công";
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            using (var context = new dbSachEntities())
            {
                var data = context.NhaXuatBan.Find(id);
                if (data != null)
                {
                    try
                    {
                        context.NhaXuatBan.Remove(data);
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
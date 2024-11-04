using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWEB.Models;
using System.Data.Entity;


namespace DoAnWEB.Areas.Admin.Controllers
{
    public class SachController : Controller
    {
        // GET: Admin/Sach
        private dbSachEntities db = new dbSachEntities();
        public ActionResult Index()
        {
            if (Session["Quyen"] == null || Session["Quyen"].ToString() != "admin")
            {
                
                return RedirectToAction("DangNhap", "Auth", new { area = "Admin" }); 
            }

            var sachlist = db.Sach
                     .OrderByDescending(s => s.MaSach) // Sắp xếp theo MaSach
                     .ToList();
            return View(sachlist);

        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Sach sach, HttpPostedFileBase fileanh, string TenTL, string TenTG, string TenNXB)
        {
            if (fileanh == null)
            {
                TempData["Error"] = "Lỗi tải ảnh";
            }
            else
            {

                string rootfolder = Server.MapPath("/Data/");
                string pathImage = rootfolder + fileanh.FileName;
                fileanh.SaveAs(pathImage);
                sach.AnhBia = "/Data/" + fileanh.FileName;

            }

            var theloai = db.TheLoai.FirstOrDefault(tl => tl.TenTL == TenTL);
            var tacgia = db.TacGia.FirstOrDefault(tg => tg.TenTacGia == TenTG);
            var nhaxuatban = db.NhaXuatBan.FirstOrDefault(nxb => nxb.TenNXB == TenNXB);

            if(theloai != null && tacgia != null && nhaxuatban != null)
            {
                sach.MaTL = theloai.MaTL;
                sach.MaTacGia = tacgia.MaTacGia;
                sach.MaNXB = nhaxuatban.MaNXB;
                db.Sach.Add(sach);
                db.SaveChanges();
                TempData["Message"] = "Thêm mới thành công";
                return View();
            }
            else
            {
                TempData["Error"] = "Thêm mới thất bại";
                return View(sach);
            }
        }
        public ActionResult Edit(int id)
        {
            Sach sach = db.Sach.Where(row => row.MaSach == id).FirstOrDefault();
            var tacgia = db.TacGia.Where(tg => tg.MaTacGia == sach.MaTacGia).FirstOrDefault();
            if(tacgia != null)
            {
                ViewBag.ValueTG = tacgia.TenTacGia;
            }
            else
            {
                ViewBag.ValueTG = "";
            }
            return View(sach);
        }
        [HttpPost]
        public ActionResult Edit(Sach sach, HttpPostedFileBase fileanh, string TenTL, string TenTG, string TenNXB)
        {

            Sach sach1 = db.Sach.Where(row => row.MaSach == sach.MaSach).FirstOrDefault();

            if (fileanh == null)
            {
                TempData["Error"] = "Lỗi tải ảnh";
            }
            else
            {
                string rootfolder = Server.MapPath("/Data/");
                string pathImage = rootfolder + fileanh.FileName;
                fileanh.SaveAs(pathImage);
                sach.AnhBia = "/Data/" + fileanh.FileName;
                sach1.AnhBia = sach.AnhBia;
            }
            var theloai = db.TheLoai.FirstOrDefault(tl => tl.TenTL == TenTL);
            var tacgia = db.TacGia.FirstOrDefault(tg => tg.TenTacGia == TenTG);
            var nhaxuatban = db.NhaXuatBan.FirstOrDefault(nxb => nxb.TenNXB == TenNXB);
            if (theloai != null && tacgia != null && nhaxuatban != null)
            {
                sach1.MaTL = theloai.MaTL;
                sach1.MaTacGia = tacgia.MaTacGia;
                sach1.MaNXB = nhaxuatban.MaNXB;
                sach1.MaSach = sach.MaSach;
                sach1.TenSach = sach.TenSach;
                sach1.GiaBan = sach.GiaBan;
                sach1.MoTa = sach.MoTa;
                sach1.Soluongton = sach.Soluongton;
                sach1.SoTrang = sach.SoTrang;
                sach1.PhanTramKM = sach.PhanTramKM;
                sach1.NamXuatBan = sach.NamXuatBan;
                db.SaveChanges();
                ViewBag.Message = "Chỉnh sửa thành công";
                return View(sach1);
            }
            else
            {
                ViewBag.Error = "Chỉnh sửa thất bại";
                return View(sach1);
            }
        }
        public ActionResult Delete (int id)
        {
            using (var context = new dbSachEntities())
            {
                var data = context.Sach.Find(id);
                if (data == null)
                {
                    return HttpNotFound();
                }

                try
                {
                    context.Sach.Remove(data);
                    context.SaveChanges();
                    TempData["Message"] = "Xóa sản phẩm thành công";
                    
                }
                catch (Exception e)
                {
                    TempData["Error"] = "Lỗi khi xóa " + e;
                    
                }

                return RedirectToAction("Index");
            }
        }
    }
   
}
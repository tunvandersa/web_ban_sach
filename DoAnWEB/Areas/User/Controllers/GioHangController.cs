using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWEB.Models;

namespace DoAnWEB.Areas.User.Controllers
{
    public class GioHangController : Controller
    {
        // GET: User/GioHang
        private dbSachEntities db = new dbSachEntities();
        private GioHang getGioHang()
        {
            GioHang giohang = (GioHang)Session["GioHang"];
            if (giohang == null)
            {
                ViewBag.Titles = "Giỏ hàng";
                giohang = new GioHang();
                Session["GioHang"] = giohang;
            }
            return giohang;
        }
        
        public ActionResult Index()
        {
            var giohang = getGioHang();
            Session["SoLuongGH"] = giohang.Items.Count();
 
            return View(giohang);
            
        }
 
        public ActionResult ThemItem(int masach, int soluong)
        {
            var sach = db.Sach.Find(masach);
            var giohang = getGioHang(); 
            if (sach != null)
            {
                giohang.ThemItem(sach.MaSach, sach.TenSach, soluong , (decimal)sach.GiaKM, sach.AnhBia);
            }
            List<ItemGioHang> listgiohang = giohang.Items.ToList();
            var s = listgiohang.FirstOrDefault(i => i.BookID == masach);
            if(s.Quantity > sach.Soluongton)
            {
                s.Quantity = sach.Soluongton;
            }
            return RedirectToAction("Index");
        }
 
        public ActionResult NhapSoLuong(int masach, int soluong)
        {
            var sach = db.Sach.Find(masach);
            var giohang = getGioHang();
            if (sach != null)
            {
                giohang.NhapSoLuong(sach.MaSach, soluong);
            }
            List<ItemGioHang> listgiohang = giohang.Items.ToList();
            var s = listgiohang.FirstOrDefault(i => i.BookID == masach);
            if (s.Quantity > sach.Soluongton)
            {
                s.Quantity = sach.Soluongton;
            }
            return RedirectToAction("Index");
        }

        public ActionResult XoaItem(int masach)
        {
            var giohang = getGioHang();
            giohang.XoaItem(masach);
            return RedirectToAction("Index");
        }
        public ActionResult GiamItem(int masach)
        {
            var giohang = getGioHang();
            giohang.GiamItem(masach);
            return RedirectToAction("Index");
        }
 
        public ActionResult ThanhToan()
        {
            var giohang = getGioHang();
            var khachhang = Session["KhachHang"] as KhachHang;
            if(khachhang == null)
            {
                return RedirectToAction("DangNhap", "Auth", new { area = "Admin" });
            }
            var hoadon = new DonHang
            {
                MaKhachHang = khachhang.MaKhachHang,
                NgayDat = DateTime.Now,
                TongDon = giohang.tongthanhtoan
            };
            db.DonHang.Add(hoadon);
            db.SaveChanges();

            foreach(var item in giohang.Items)
            {
                var cthd = new ChiTietDonHang
                {
                    MaDonHang = hoadon.MaDonHang,
                    MaSach = item.BookID,
                    SoLuong = item.Quantity,
                    GiaBan = item.Price,

                };
                db.ChiTietDonHang.Add(cthd);
                
            }
            db.SaveChanges();
            TempData["Message"] = "Đặt hàng thành công";    
            giohang.XoaGioHang();

            return RedirectToAction("Index");
        }
       
    }
}
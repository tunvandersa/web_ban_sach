using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnWEB.Models
{
    public class GioHang
    {
        private List<ItemGioHang> items = new List<ItemGioHang>();
        public IEnumerable<ItemGioHang> Items => items;
        public void ThemItem (int masach, string tensach, int soluong, decimal gia, string anh)
        {
            var item = items.FirstOrDefault(i => i.BookID == masach);
            if (item == null)
            {
                items.Add(new ItemGioHang
                {

                    ItemID = items.Count + 1,
                    BookID = masach,
                    Title = tensach,
                    Quantity = soluong,
                    Price = gia,
                    Image = anh
                });

            }
            else
            {
                item.Quantity += soluong;
            }
        }

        public void NhapSoLuong(int masach, int soluong)
        {
            var item = items.FirstOrDefault(i => i.BookID == masach);
            item.Quantity = soluong;
        }
        public void XoaItem (int masach)
        {
            items.RemoveAll(i => i.BookID == masach);
        }
        public void GiamItem(int masach)
        {
            var item = items.FirstOrDefault(i => i.BookID == masach);
            if(item.Quantity > 1)
            {
                item.Quantity -= 1;
            }
            else
            {
                items.Remove(item);
            }
        }
        public decimal tongthanhtoan => Items.Sum(i => i.TotalPrice);
        public void XoaGioHang()
        {
            items.Clear();

        }

    }
}
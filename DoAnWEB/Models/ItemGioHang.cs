using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnWEB.Models
{
    public class ItemGioHang
    {
        public int ItemID { get; set; }
        
        public int BookID { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice => Quantity * Price;

    }
}
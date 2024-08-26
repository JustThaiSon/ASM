using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ASM.Entities
{
    public class Product
    {
        public int ProductId { get; set; }

        public string? NameProduct { get; set; }
        public string? ImgUrl { get; set; }

        public string? Description { get; set; }

        public string? CachDung { get; set; }

        public string? LuuY { get; set; }

        public int ThuongHieuId { get; set; }

        public DateTime Hsd { get; set; }

        public int MaNhaSanXuat { get; set; }
        public decimal Price { get; set; }
       
        public NhaSanXuat? NhaSanXuat { get; set; }
        public ThuongHieu? ThuongHieu { get; set; }
        public List<Thuoc_CongDung>? Thuoc_CongDung { get; set; }
        public List<ThanhPhan>? ThanhPhan { get; set; }
        public List<OrderDetails>? OrderDetails { get; set; }
        public List<Product_Category>? Product_Category { get; set; }
        public List<CartItem>? CartItem { get; set; }
    }
}

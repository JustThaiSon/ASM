using System.ComponentModel.DataAnnotations;

namespace ASM.Entities
{
    public class NhaSanXuat
    {
        public int NhaSanXuatId { get; set; }

        public string? TenNhaSanXuat { get; set; }
        public List<Product>? Product { get; set; }
    }
}

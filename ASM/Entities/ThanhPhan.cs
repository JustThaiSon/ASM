using System.ComponentModel.DataAnnotations;

namespace ASM.Entities
{
    public class ThanhPhan
    {
        public int ThanhPhanId { get; set; }

        public string? ThongTinThanhPhan { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}

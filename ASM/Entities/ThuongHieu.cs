namespace ASM.Entities
{
    public class ThuongHieu
    {
        public int ThuongHieuId { get; set; }

        public string? TenThuongHieu { get; set; }
        public List<Product>? Product { get; set; }
    }
}

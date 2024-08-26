namespace ASM.Entities
{
    public class Thuoc_CongDung
    {
        public int ProductId { get; set; }
        public int CongDungId { get; set; }
        public Product? Product { get; set; }
        public CongDung? CongDung { get; set; }
    }
}

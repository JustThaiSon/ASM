using System.ComponentModel.DataAnnotations;

namespace ASM.Entities
{
    public class CongDung
    {
        public int CongDungId { get; set; }

        public string? CongDungThuoc { get; set; }
        public List<Thuoc_CongDung>? Thuoc_CongDung { get; set; }
    }
}

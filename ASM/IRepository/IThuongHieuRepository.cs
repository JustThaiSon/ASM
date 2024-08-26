using ASM.Entities;

namespace ASM.IRepository
{
	public interface IThuongHieuRepository 
	{
		Task<List<ThuongHieu>> GetAllThuongHieu(int page, int pageSize);
		Task<ThuongHieu> GetThuongHieutById(int Id);
		Task<bool> DeleteThuongHieu(int Id);
		Task<bool> UpdateThuongHieu(ThuongHieu ThuongHieu);
		Task<bool> CreateThuongHieu(ThuongHieu ThuongHieu);
		Task<int> GetTotalThuongHieu();
	}
}

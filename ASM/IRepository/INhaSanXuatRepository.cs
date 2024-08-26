using ASM.Entities;

namespace ASM.IRepository
{
	public interface INhaSanXuatRepository 
	{
		Task<List<NhaSanXuat>> GetAllNhaSanXuat(int page, int pageSize);
		Task<NhaSanXuat> GetNhaSanXuatById(int Id);
		Task<bool> DeleteNhaSanXuat(int Id);
		Task<bool> UpdateNhaSanXuat(NhaSanXuat nhaSanXuat);
		Task<bool> CreateNhaSanXuat(NhaSanXuat nhaSanXuat);
		Task<int> GetTotalNhaSanXuat();
	}
}

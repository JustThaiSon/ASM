using ASM.Entities;

namespace ASM.IRepository
{
	public interface ICongDungRepository
	{
		Task<List<CongDung>> GetAllCongDung(int page, int pageSize);
		Task<CongDung> GetCongDungtById(int Id);
		Task<bool> DeleteCongDung(int Id);
		Task<bool> UpdateCongDung(CongDung CongDung);
		Task<bool> CreateCongDung(CongDung CongDung);
		Task<int> GetTotalCongDung();
	}
}

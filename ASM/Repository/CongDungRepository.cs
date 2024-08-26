using ASM.Data;
using ASM.Entities;
using ASM.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ASM.Repository
{
	public class CongDungRepository : ICongDungRepository
	{
		private readonly MyDbContext _context;
		public CongDungRepository(MyDbContext context)
		{
			_context = context;

		}
		public async Task<bool> CreateCongDung(CongDung CongDung)
		{
			await _context.CongDungs.AddAsync(CongDung);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<bool> DeleteCongDung(int Id)
		{
			var DeleteThuocCongDung = await _context.Thuoc_CongDungs.FirstOrDefaultAsync(x => x.CongDungId == Id);
			if (DeleteThuocCongDung == null) return false;
			_context.Thuoc_CongDungs.Remove(DeleteThuocCongDung);
			var delete = await _context.CongDungs.FirstOrDefaultAsync(x => x.CongDungId == Id);
			if (delete == null)
				return false;
			_context.CongDungs.Remove(delete);
			await _context.SaveChangesAsync();
			return true;

		}

		public async Task<List<CongDung>> GetAllCongDung(int page, int pageSize)
		{
			var skipAmount = (page - 1) * pageSize;
			var GetAll = await _context.CongDungs
											.Skip(skipAmount)
											.Take(pageSize)
											.ToListAsync();
			return GetAll;
		}

		public async Task<CongDung> GetCongDungtById(int Id)
		{
			var GetById = await _context.CongDungs.FirstOrDefaultAsync(x => x.CongDungId == Id);
			return GetById;
		}

		public async Task<int> GetTotalCongDung()
		{
			return await _context.CongDungs.CountAsync();
		}

		public async Task<bool> UpdateCongDung(CongDung CongDung)
		{
			var update = await _context.CongDungs.FindAsync(CongDung.CongDungId);
			if (update == null) return false;
			update.CongDungThuoc = CongDung.CongDungThuoc;
			update.CongDungId = CongDung.CongDungId;
			await _context.SaveChangesAsync();
			return true;
		}
	}
}

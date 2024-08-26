using ASM.Data;
using ASM.Entities;
using ASM.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ASM.Repository
{
	public class ThuongHieuRepository : IThuongHieuRepository
	{
		private readonly MyDbContext _context;
        public ThuongHieuRepository(MyDbContext context)
        {
			_context = context;

		}
        public async Task<bool> CreateThuongHieu(ThuongHieu ThuongHieu)
		{
			await _context.ThuongHieus.AddAsync(ThuongHieu);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<bool> DeleteThuongHieu(int Id)
		{
			using (var transaction = _context.Database.BeginTransaction())
			{
				try
				{
					var deleteThanhPhan = await(
						from tp in _context.ThanhPhans
						join p in _context.Products on tp.ProductId equals p.ProductId
						where p.ThuongHieuId == Id
						select tp
					).ToListAsync();

					_context.ThanhPhans.RemoveRange(deleteThanhPhan);

					var deleteThuocCongDung = await(
						from tcd in _context.Thuoc_CongDungs
						join p in _context.Products on tcd.ProductId equals p.ProductId
						where p.ThuongHieuId == Id
						select tcd
					).ToListAsync();

					_context.Thuoc_CongDungs.RemoveRange(deleteThuocCongDung);

					var deleteProductCategories = await(
						from pct in _context.Product_Categorys
						join p in _context.Products on pct.ProductId equals p.ProductId
						where p.ThuongHieuId == Id
						select pct
					).ToListAsync();

					_context.Product_Categorys.RemoveRange(deleteProductCategories);

					var deleteOrderDetails = await(
						from od in _context.OrderDetails
						join p in _context.Products on od.ProductId equals p.ProductId
						where p.ThuongHieuId == Id
						select od
					).ToListAsync();

					_context.OrderDetails.RemoveRange(deleteOrderDetails);

					var ThuongHieuToDelete = await _context.ThuongHieus.FirstOrDefaultAsync(x => x.ThuongHieuId == Id);
					if (ThuongHieuToDelete == null)
						return false;

					 _context.ThuongHieus.Remove(ThuongHieuToDelete);

					var productsToDelete = await _context.Products.Where(p => p.ThuongHieuId == Id).ToListAsync();
					_context.Products.RemoveRange(productsToDelete);

					await _context.SaveChangesAsync();

					await transaction.CommitAsync();

					return true;
				}
				catch (Exception ex)
				{
					await transaction.RollbackAsync();
					return false;
				}
			}
		}

		public async Task<List<ThuongHieu>> GetAllThuongHieu(int page, int pageSize)
		{
			var skipAmount = (page - 1) * pageSize;
			var GetAllThuongHieu = await _context.ThuongHieus
											.Skip(skipAmount)
											.Take(pageSize)
											.ToListAsync();
			return GetAllThuongHieu;
		}

		public async Task<ThuongHieu> GetThuongHieutById(int Id)
		{
			var getbyid = await _context.ThuongHieus.FirstOrDefaultAsync(x=>x.ThuongHieuId == Id);
			return getbyid;
		}

		public async Task<int> GetTotalThuongHieu()
		{
			return await _context.ThuongHieus.CountAsync();
		}

		public async Task<bool> UpdateThuongHieu(ThuongHieu ThuongHieu)
		{
			var Exist = await _context.ThuongHieus.FirstOrDefaultAsync(x => x.ThuongHieuId == ThuongHieu.ThuongHieuId);
			if (Exist == null) return false;
			Exist.ThuongHieuId = ThuongHieu.ThuongHieuId;
			Exist.TenThuongHieu = ThuongHieu.TenThuongHieu;
			await _context.SaveChangesAsync();
			return true;
		}
	}
}

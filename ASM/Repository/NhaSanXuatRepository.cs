using ASM.Data;
using ASM.Entities;
using ASM.IRepository;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ASM.Repository
{
	public class NhaSanXuatRepository : INhaSanXuatRepository
	{
		private readonly MyDbContext _context;
        public NhaSanXuatRepository(MyDbContext context)
        {
			_context = context;

		}
        public async Task<bool> CreateNhaSanXuat(NhaSanXuat nhaSanXuat)
		{
		    await _context.NhaSanXuats.AddAsync(nhaSanXuat);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<bool> DeleteNhaSanXuat(int Id)
		{
			using (var transaction = _context.Database.BeginTransaction())
			{
				try
				{
					var deleteThanhPhan = await (
						from tp in _context.ThanhPhans
						join p in _context.Products on tp.ProductId equals p.ProductId
						where p.MaNhaSanXuat == Id
						select tp
					).ToListAsync();

					_context.ThanhPhans.RemoveRange(deleteThanhPhan);

					var deleteThuocCongDung = await (
						from tcd in _context.Thuoc_CongDungs
						join p in _context.Products on tcd.ProductId equals p.ProductId
						where p.MaNhaSanXuat == Id
						select tcd
					).ToListAsync();

					_context.Thuoc_CongDungs.RemoveRange(deleteThuocCongDung);

					var deleteProductCategories = await (
						from pct in _context.Product_Categorys
						join p in _context.Products on pct.ProductId equals p.ProductId
						where p.MaNhaSanXuat == Id
						select pct
					).ToListAsync();

					_context.Product_Categorys.RemoveRange(deleteProductCategories);

					var deleteOrderDetails = await (
						from od in _context.OrderDetails
						join p in _context.Products on od.ProductId equals p.ProductId
						where p.MaNhaSanXuat == Id
						select od
					).ToListAsync();

					_context.OrderDetails.RemoveRange(deleteOrderDetails);

					var nhaSanXuatToDelete = await _context.NhaSanXuats.FirstOrDefaultAsync(x => x.NhaSanXuatId == Id);
					if (nhaSanXuatToDelete == null)
						return false;

					_context.NhaSanXuats.Remove(nhaSanXuatToDelete);

					var productsToDelete = await _context.Products.Where(p => p.MaNhaSanXuat == Id).ToListAsync();
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


		public async Task<List<NhaSanXuat>> GetAllNhaSanXuat(int page, int pageSize)
		{
			var skipAmount = (page - 1) * pageSize;
			var getNhaSanXuat = await _context.NhaSanXuats
											.Skip(skipAmount)
											.Take(pageSize)
											.ToListAsync();

			return getNhaSanXuat;

	}

		public async Task<NhaSanXuat> GetNhaSanXuatById(int Id)
		{
			return await _context.NhaSanXuats.FirstOrDefaultAsync(x=>x.NhaSanXuatId == Id);
		}

		public async Task<int> GetTotalNhaSanXuat()
		{
			return await _context.NhaSanXuats.CountAsync();
		}

		public async Task<bool> UpdateNhaSanXuat(NhaSanXuat nhaSanXuat)
		{
			var FindNsx = await _context.NhaSanXuats.FirstOrDefaultAsync(x => x.NhaSanXuatId == nhaSanXuat.NhaSanXuatId);
			if (FindNsx == null) return false;
			FindNsx.NhaSanXuatId = nhaSanXuat.NhaSanXuatId;
			FindNsx.TenNhaSanXuat = nhaSanXuat.TenNhaSanXuat;
			await _context.SaveChangesAsync();
			return true;
          
        }
	}
}

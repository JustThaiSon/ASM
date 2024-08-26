using ASM.Data;
using ASM.Entities;
using ASM.IRepository;
using ASM.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace ASM.Repository
{
	public class ProductRepository : IProductRepository
	{
		private readonly MyDbContext _context;
		private readonly IWebHostEnvironment _webHostEnvironment;
		public ProductRepository(MyDbContext _contex, IWebHostEnvironment webHostEnvironment)
		{
			this._context = _contex;
			_webHostEnvironment = webHostEnvironment;
		}
		public async Task<int> CreateProduct(CreateProductViewModel request)
		{
            string fileName = "";
            if (request.Photo != null)
            {
                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                fileName = Guid.NewGuid().ToString() + "_" + request.Photo.FileName;
                string filePath = Path.Combine(uploadFolder, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    request.Photo.CopyTo(fileStream);
                }
            }
            var thuongHieuId = await _context.ThuongHieus
				.Where(th => th.TenThuongHieu == request.ThuongHieu)
				.Select(th => th.ThuongHieuId)
				.FirstOrDefaultAsync();

			var nhaSanXuatId = await _context.NhaSanXuats
				.Where(nsx => nsx.TenNhaSanXuat == request.MhaSanXuat)
				.Select(nsx => nsx.NhaSanXuatId)
				.FirstOrDefaultAsync();

			var congDungId = await _context.CongDungs
				.Where(cd => cd.CongDungThuoc == request.CongDungThuoc)
				.Select(cd => cd.CongDungId)
				.FirstOrDefaultAsync();

			var categoryId = await _context.Categories
				.Where(cd => cd.CategoryName == request.CategoryName)
				.Select(cd => cd.CategoryId)
				.FirstOrDefaultAsync();

			var product = new Product
			{
				NameProduct = request.NameProduct,
				ImgUrl = fileName,
				Description = request.Description,
				CachDung = request.CachDung,
				LuuY = request.LuuY,
				ThuongHieuId = thuongHieuId,
				Hsd = request.Hsd,
				Price = request.Price,
				MaNhaSanXuat = nhaSanXuatId,
				ThanhPhan = new List<ThanhPhan>
				{
					new ThanhPhan
					{
						ThongTinThanhPhan = request.ThongTinThanhPhan
					}
				},
				Thuoc_CongDung = new List<Thuoc_CongDung>
				{
					new Thuoc_CongDung
					{
						CongDungId = congDungId
					}
				},
				Product_Category = new List<Product_Category>
				{
					new Product_Category
					{
						CategoryId = categoryId
					}
				}
			};

			_context.Products.Add(product);
			await _context.SaveChangesAsync();

			return product.ProductId;
		}

		public async Task<bool> DeleteProduct(int Id)
		{
			using (var transaction = await _context.Database.BeginTransactionAsync())
			{
				try
				{
					var thuocCongDungsToDelete = _context.Thuoc_CongDungs.Where(tcd => tcd.ProductId == Id);
					_context.Thuoc_CongDungs.RemoveRange(thuocCongDungsToDelete);
					var CategoryDelete = _context.Product_Categorys.Where(tcd => tcd.ProductId == Id);
					_context.Product_Categorys.RemoveRange(CategoryDelete);

					var thanhPhansToDelete = _context.ThanhPhans.Where(tp => tp.ProductId == Id);
					_context.ThanhPhans.RemoveRange(thanhPhansToDelete);

					var orderDetailsToDelete = _context.OrderDetails.Where(od => od.ProductId == Id);
					_context.OrderDetails.RemoveRange(orderDetailsToDelete);
					var productCategorysToDelete = _context.Product_Categorys.Where(pc => pc.ProductId == Id);
					_context.Product_Categorys.RemoveRange(productCategorysToDelete);

					var productToDelete = _context.Products.Where(p => p.ProductId == Id);
					_context.Products.RemoveRange(productToDelete);

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


		public async Task<List<ProductViewModels>> GetAllProduct(int page, int pageSize)
		{
			var skipAmount = (page - 1) * pageSize;

			var query = from p in _context.Products
						join th in _context.ThuongHieus on p.ThuongHieuId equals th.ThuongHieuId
						join nsx in _context.NhaSanXuats on p.MaNhaSanXuat equals nsx.NhaSanXuatId
						join tp in _context.ThanhPhans on p.ProductId equals tp.ProductId into tpGroup
						join pc in _context.Product_Categorys on p.ProductId equals pc.ProductId
						join ct in _context.Categories on pc.CategoryId equals ct.CategoryId
						from tp in tpGroup.DefaultIfEmpty()
						join tcd in _context.Thuoc_CongDungs on p.ProductId equals tcd.ProductId into tcdGroup
						from tcd in tcdGroup.DefaultIfEmpty()
						join cd in _context.CongDungs on tcd.CongDungId equals cd.CongDungId into cdGroup
						from cd in cdGroup.DefaultIfEmpty()
						select new ProductViewModels
						{
							ProductId = p.ProductId,
							NameProduct = p.NameProduct,
							ImgUrl = p.ImgUrl,
							Description = p.Description,
							CachDung = p.CachDung,
							LuuY = p.LuuY,
							TenThuongHieu = th.TenThuongHieu,
							Hsd = p.Hsd,
							Price = p.Price,
							TenNhaSanXuat = nsx.TenNhaSanXuat,
							ThongTinThanhPhan = tp.ThongTinThanhPhan,
							CongDungThuoc = cd.CongDungThuoc,
							CategoryName = ct.CategoryName
						};

			return await query.Skip(skipAmount).Take(pageSize).ToListAsync(); ;

		}

		public async Task<IEnumerable<Product>> GetAllProducts()
		{
			return await _context.Products.ToListAsync();
		}

		public async Task<List<Category>> GetCategories()
		{
			return await _context.Categories.ToListAsync();
		}

		public async Task<List<CongDung>> GetCongDung()
		{
			var query = await _context.CongDungs.ToListAsync();
			return query;
		}




		public async Task<List<NhaSanXuat>> GetNhaSanXuats()
		{
			var query = await _context.NhaSanXuats.ToListAsync();
			return query;
		}

		public async Task<CreateProductViewModel> GetProductById(int Id)
		{
			var product = await (from p in _context.Products
								 join th in _context.ThuongHieus on p.ThuongHieuId equals th.ThuongHieuId
								 join nsx in _context.NhaSanXuats on p.MaNhaSanXuat equals nsx.NhaSanXuatId
								 join pc in _context.Product_Categorys on p.ProductId equals pc.ProductId
								 join ct in _context.Categories on pc.CategoryId equals ct.CategoryId
								 join tp in _context.ThanhPhans on p.ProductId equals tp.ProductId into tpGroup
								 from tp in tpGroup.DefaultIfEmpty()
								 join tcd in _context.Thuoc_CongDungs on p.ProductId equals tcd.ProductId into tcdGroup
								 from tcd in tcdGroup.DefaultIfEmpty()
								 join cd in _context.CongDungs on tcd.CongDungId equals cd.CongDungId into cdGroup
								 from cd in cdGroup.DefaultIfEmpty()
								 where p.ProductId == Id
								 select new CreateProductViewModel
								 {
									 ProductId = p.ProductId,
									 NameProduct = p.NameProduct,
									 ImgUrl = p.ImgUrl,
									 Description = p.Description,
									 CachDung = p.CachDung,
									 LuuY = p.LuuY,
									 Price = p.Price,
									 ThuongHieu = th.TenThuongHieu,
									 Hsd = p.Hsd,
									 MhaSanXuat = nsx.TenNhaSanXuat,
									 ThongTinThanhPhan = tp.ThongTinThanhPhan,
									 CongDungThuoc = cd.CongDungThuoc,
									 CategoryName = ct.CategoryName
								 }).FirstOrDefaultAsync();

			return product;
		}




		public async Task<List<ThanhPhan>> GetThanhPhan()
		{
			var query = await _context.ThanhPhans.ToListAsync();
			return query;
		}

		public async Task<List<ThuongHieu>> GetThuongHieus()
		{
			var query = await _context.ThuongHieus.ToListAsync();
			return query;
		}

		public async Task<int> GetTotalProduct()
		{
			return await _context.Products.CountAsync();
		}
		public async Task<bool> UpdateProduct(CreateProductViewModel request)
		{
			try
			{
				var productData = await (from p in _context.Products
										 join th in _context.ThuongHieus on p.ThuongHieuId equals th.ThuongHieuId
										 join nsx in _context.NhaSanXuats on p.MaNhaSanXuat equals nsx.NhaSanXuatId
										 join pc in _context.Product_Categorys on p.ProductId equals pc.ProductId
										 join ct in _context.Categories on pc.CategoryId equals ct.CategoryId
										 join tp in _context.ThanhPhans on p.ProductId equals tp.ProductId into tpGroup
										 from tp in tpGroup.DefaultIfEmpty()
										 join tcd in _context.Thuoc_CongDungs on p.ProductId equals tcd.ProductId into tcdGroup
										 from tcd in tcdGroup.DefaultIfEmpty()
										 join cd in _context.CongDungs on tcd.CongDungId equals cd.CongDungId into cdGroup
										 from cd in cdGroup.DefaultIfEmpty()
										 where p.ProductId == request.ProductId
										 select new { Product = p, ThanhPhan = tp, Thuoc_CongDung = tcd, Product_Category = ct })
					.FirstOrDefaultAsync();

				if (productData == null)
				{
					return false;
				}

                string fileName = "";
                if (request.Photo != null)
                {
                    string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    fileName = Guid.NewGuid().ToString() + "_" + request.Photo.FileName;
                    string filePath = Path.Combine(uploadFolder, fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        request.Photo.CopyTo(fileStream);
                    }
                }
                var thuongHieuId = await _context.ThuongHieus
					.Where(th => th.TenThuongHieu == request.ThuongHieu)
					.Select(th => th.ThuongHieuId)
					.FirstOrDefaultAsync();

				var nhaSanXuatId = await _context.NhaSanXuats
					.Where(nsx => nsx.TenNhaSanXuat == request.MhaSanXuat)
					.Select(nsx => nsx.NhaSanXuatId)
					.FirstOrDefaultAsync();

				var congDungId = await _context.CongDungs
					.Where(cd => cd.CongDungThuoc == request.CongDungThuoc)
					.Select(cd => cd.CongDungId)
					.FirstOrDefaultAsync();

				var categoryId = await _context.Categories
					.Where(cat => cat.CategoryName == request.CategoryName)
					.Select(cat => cat.CategoryId)
					.FirstOrDefaultAsync();

				var product = productData.Product;

				product.NameProduct = request.NameProduct;
				product.ImgUrl = fileName;
				product.Description = request.Description;
				product.CachDung = request.CachDung;
				product.LuuY = request.LuuY;
				product.ThuongHieuId = thuongHieuId;
				product.Hsd = request.Hsd;
				product.Price = request.Price;
				product.MaNhaSanXuat = nhaSanXuatId;

				if (productData.ThanhPhan != null)
				{
					productData.ThanhPhan.ThongTinThanhPhan = request.ThongTinThanhPhan;
				}
				if (productData.Thuoc_CongDung != null)
				{
					productData.Thuoc_CongDung.CongDungId = congDungId;
				}
				if (productData.Product_Category != null)
				{
					productData.Product_Category.CategoryId = categoryId;
				}

				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}


	}
}

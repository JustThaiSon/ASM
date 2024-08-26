using ASM.Data;
using ASM.Entities;
using ASM.IRepository;
using ASM.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ASM.Repository
{
    public class CategoryRepository : ICategoryRepository
	{
		private readonly MyDbContext _context;
		public CategoryRepository(MyDbContext context)
		{
			_context = context;

		}
		public async Task<bool> CreateCategory(CategoryViewModels request)
		{
			var ProductId = await _context.Products.Where(x => x.NameProduct == request.NameProduct).Select(x => x.ProductId).FirstOrDefaultAsync();
			if (ProductId == 0)
			{
				return false;
			}
			var Category = new Category()
			{
				CategoryName = request.CategoryName,
				Product_Category = new List<Product_Category>()
				{
					new Product_Category
					{
						ProductId = ProductId,
					}
				}
			};
			await _context.Categories.AddAsync(Category);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<bool> DeleteCategory(int Id)
		{
			var deleteCategory_Product = await _context.Product_Categorys.FirstOrDefaultAsync(x => x.CategoryId == Id);
			if (deleteCategory_Product == null) return false;
			var deleteCategory = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryId == Id);
			if (deleteCategory == null) return false;
			_context.Product_Categorys.Remove(deleteCategory_Product);
			_context.Categories.Remove(deleteCategory);
			await _context.SaveChangesAsync();
			return true;

		}

		public async Task<List<CategoryViewModels>> GetAllCategory(int page, int pageSize)
		{
            var skipAmount = (page - 1) * pageSize; 

            var query = from c in _context.Categories
						 join cp in _context.Product_Categorys
						 on c.CategoryId equals cp.CategoryId
						 join p in _context.Products
						 on cp.ProductId equals p.ProductId
						 select new CategoryViewModels()
						 {
							 CategoryId  = c.CategoryId,
							 CategoryName = c.CategoryName,
                             NameProduct = p.NameProduct
						 };
            return await query.Skip(skipAmount).Take(pageSize).ToListAsync();
        }

		public async Task<CategoryViewModels> GetCategoryById(int Id)
		{
			var category = await (from c in _context.Categories
								  join cp in _context.Product_Categorys
								  on c.CategoryId equals cp.CategoryId
								  join p in _context.Products
								  on cp.ProductId equals p.ProductId
								  where c.CategoryId == Id
								  select new CategoryViewModels()
								  {
									  CategoryName = c.CategoryName,
									  NameProduct = p.NameProduct
								  }).FirstOrDefaultAsync();

			return category;
		}

        public async Task<List<Product>> GetProduct()
        {
           return await _context.Products.ToListAsync();
        }

        public async Task<int> GetTotalCategories()
        {
            return await _context.Categories.CountAsync();
        }

        public async Task<bool> UpdateCategory(CategoryViewModels request)
		{ 
			var ProductId = await _context.Products.Where(x => x.NameProduct == request.NameProduct).Select(x => x.ProductId).FirstOrDefaultAsync();
			if (ProductId == 0)
			{
				return false;
			}
			var Update = await(from c in _context.Categories
						 join cp in _context.Product_Categorys
						 on c.CategoryId equals cp.CategoryId
						 join p in _context.Products
						 on cp.ProductId equals p.ProductId
						 where c.CategoryId == request.CategoryId
						 select new { Product = p, Category = c, Product_Category = cp }).FirstOrDefaultAsync();

			if (Update == null) return false;

            Update.Category.CategoryId = request.CategoryId;
            Update.Category.CategoryName = request.CategoryName;
			Update.Product_Category.ProductId = ProductId;
	



			await _context.SaveChangesAsync();
			return true;
		}
	}
}

using ASM.Entities;
using ASM.ViewModels;

namespace ASM.IRepository
{
    public interface ICategoryRepository
    {
        Task<List<CategoryViewModels>> GetAllCategory(int page, int pageSize);
        Task<CategoryViewModels> GetCategoryById(int Id);
        Task<bool> DeleteCategory(int Id);
        Task<bool> UpdateCategory(CategoryViewModels request);
        Task<bool> CreateCategory(CategoryViewModels request);
        Task<List<Product>> GetProduct();
        Task<int> GetTotalCategories();

    }
}

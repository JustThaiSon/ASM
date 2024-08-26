using ASM.Entities;
using ASM.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ASM.IRepository
{
    public interface IProductRepository
    {
        Task<List<ProductViewModels>> GetAllProduct(int page, int pageSize);
        Task<CreateProductViewModel> GetProductById(int Id);
        Task<bool> DeleteProduct(int Id);
        Task<bool> UpdateProduct(CreateProductViewModel request);
        Task<int> CreateProduct(CreateProductViewModel request);
        Task<List<ThuongHieu>> GetThuongHieus();
        Task<List<NhaSanXuat>> GetNhaSanXuats();
        Task<List<CongDung>> GetCongDung();
        Task<List<ThanhPhan>> GetThanhPhan();
        Task<List<Category>> GetCategories();
        Task<int> GetTotalProduct();
        Task<IEnumerable<Product>> GetAllProducts();

    }
}

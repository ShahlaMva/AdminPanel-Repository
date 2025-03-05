using AdminPanel.Models;

namespace AdminPanel.Services.ProductServ
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task AddProductAsync(Product product);

        Task DeleteProductAsync(int id);
        Task UpdateProductAsync(Product product);
        Task<int> ProductCountAsync();
        Task <IEnumerable<Product>>GetPaginateAsync(int skip, int take);

      
    }
}

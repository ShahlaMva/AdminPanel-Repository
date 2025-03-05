using AdminPanel.Models;

namespace AdminPanel.Repositories.ProductRepo
{
    public interface IProductRepository:IRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductPaginationAsync(int page,int take);
        Task<int> GetProductCountAsync();
    }
}

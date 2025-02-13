using AdminPanel.Models;
using AdminPanel.Repositories.ProductRepo;

namespace AdminPanel.Services.ProductServ
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;        }
       public async Task AddProductAsync(Product product)
        {
            await _productRepository.AddAsync(product);

        }

       public async Task DeleteProductAsync(int id)
        {
            await _productRepository.DeleteAsync(id);
        }

       public async Task<IEnumerable<Product>>GetAllProductsAsync()
        {
           return await _productRepository.GetAllAsync();
        }

       public async Task<Product>GetProductByIdAsync(int id)
        {
          return  await _productRepository.GetByIdAsync(id);
        }

      public async Task UpdateProductAsync(Product product)
        {
            await _productRepository.UpdateAsync(product);
        }
    }
}

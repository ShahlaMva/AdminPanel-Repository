using AdminPanel.Db;
using AdminPanel.Models;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.Repositories.ProductRepo
{
    public class ProductRepository:Repository<Product>, IProductRepository

    {
        public ProductRepository(AppDbContext context):base(context)
        {
            
        }
        public async Task<int> GetProductCountAsync()
        {
            return await _context.Products.CountAsync();
        }
        public async Task<IEnumerable<Product>> GetProductPaginationAsync(int p, int t)
        {
            return await _context.Products.Skip((p - 1) * t).Take(t).ToListAsync();
        }
    }
}

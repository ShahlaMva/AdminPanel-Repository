using AdminPanel.Db;
using AdminPanel.Models;

namespace AdminPanel.Repositories.ProductRepo
{
    public class ProductRepository:Repository<Product>, IProductRepository

    {
        public ProductRepository(AppDbContext context):base(context)
        {
            
        }
    }
}

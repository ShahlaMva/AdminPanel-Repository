using AdminPanel.Db;
using AdminPanel.Models;

namespace AdminPanel.Repositories.CategoryRepo
{
    public class CategoryRepository:Repository<Category>,ICategoryRepository
    {
        public CategoryRepository(AppDbContext context):base(context)
        {
            
        }
    }
}

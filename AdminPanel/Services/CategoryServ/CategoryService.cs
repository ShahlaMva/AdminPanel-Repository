using AdminPanel.Models;
using AdminPanel.Repositories.CategoryRepo;
using Microsoft.Extensions.FileProviders;

namespace AdminPanel.Services.CategoryServ
{
    public class CategoryService:ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository; 
        }
       public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }
        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }
        public async Task DeleteCategoryAsync(int id)
        {
             await _categoryRepository.DeleteAsync(id);
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            
          await _categoryRepository.UpdateAsync(category);
        }

        public async Task AddCategoryAsync(Category category)
        {
           await _categoryRepository.AddAsync(category);
           
        }


       
    }
}

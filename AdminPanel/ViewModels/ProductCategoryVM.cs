using AdminPanel.Models;

namespace AdminPanel.ViewModels
{
    public class ProductCategoryVM
    {
        public Product? Product { get; set; }
        public IEnumerable<Product>? Products { get; set; }
        public IEnumerable<Category>? Categories {  get; set; }  
    }
}

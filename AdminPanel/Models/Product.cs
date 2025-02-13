using System.ComponentModel.DataAnnotations.Schema;

namespace AdminPanel.Models
{
    public class Product:BaseEntity
    {
       
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public decimal? SalePrice { get; set; }
        public int Count { get; set; }
        public decimal DisCount { get; set; }

        public string? ProductPhoto { get; set; }
        [NotMapped]
        public IFormFile? ProductImage { get; set; }
  
        public int CategoryId { get; set; }
        public virtual Category? Category { get; set; }



    }
}

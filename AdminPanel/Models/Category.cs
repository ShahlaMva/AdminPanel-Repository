using System.ComponentModel.DataAnnotations;

namespace AdminPanel.Models
{
    public class Category:BaseEntity
    {
       
        [Required]
        public string? Name { get; set; }
      
        public virtual ICollection<Product>? Products{ get; set; }
    }
}

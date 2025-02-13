namespace AdminPanel.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreateTime { get; set; }
        public bool SoftDelete { get; set; }=false;
        public DateTime UpdateTime { get; set; }
      
    }
}

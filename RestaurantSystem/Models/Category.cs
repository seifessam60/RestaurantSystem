using System.ComponentModel.DataAnnotations;

namespace RestaurantSystem.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "اسم الفئة مطلوب")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        [StringLength(500)]
        public string? Description { get; set; }
        public List<MenuItem>? MenuItems { get; set; }
    }
}

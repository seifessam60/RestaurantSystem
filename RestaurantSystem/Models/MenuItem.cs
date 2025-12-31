using System.ComponentModel.DataAnnotations;

namespace RestaurantSystem.Models
{
    public class MenuItem
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "اسم الصنف مطلوب")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "السعر مطلوب")]
        [Range(0.01, double.MaxValue, ErrorMessage = "السعر يجب أن يكون أكبر من صفر")]
        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

        public bool IsAvailable { get; set; } = true;

        [Required(ErrorMessage = "الفئة مطلوبة")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}

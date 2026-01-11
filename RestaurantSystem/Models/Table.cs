
using System.ComponentModel.DataAnnotations;

namespace RestaurantSystem.Models
{
    public class Table
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "رقم الطاولة مطلوب")]
        [Range(1, 999, ErrorMessage = "رقم الطاولة يجب أن يكون بين 1 و 999")]
        public int TableNumber { get; set; }

        [Range(1, 50, ErrorMessage = "السعة يجب أن تكون بين 1 و 50")]
        public int Capacity { get; set; }

        public bool IsAvailable { get; set; } = true;

        public string? Location { get; set; }

        public List<Order>? Orders { get; set; }
    }
}

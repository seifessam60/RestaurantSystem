using System.ComponentModel.DataAnnotations;

namespace RestaurantSystem.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order? Order { get; set; }

        public int MenuItemId { get; set; }
        public string MenuItemName { get; set; }

        public decimal Price { get; set; }

        [Range(1, 100, ErrorMessage = "الكمية يجب أن تكون بين 1 و 100")]
        public int Quantity { get; set; }

        public string? SpecialInstructions { get; set; } 

        public decimal Total => Price * Quantity;

    }
}

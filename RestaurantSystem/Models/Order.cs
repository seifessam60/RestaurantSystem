using System.ComponentModel.DataAnnotations;

namespace RestaurantSystem.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "رقم الطاولة مطلوب")]
        public int TableId { get; set; }
        public Table? Table { get; set; }

        public string? CustomerName { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public decimal TotalAmount { get; set; }

        public string? Notes { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }

    public enum OrderStatus
    {
        Pending,       
        InKitchen,      
        Ready,          
        Served,         
        Completed,      
        Cancelled
    }
}

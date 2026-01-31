namespace OrderManagement.Api.Domain;

public class Order
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public List<OrderItem> Items { get; set; } = new();
    public List<Payment> Payments { get; set; } = new();
}

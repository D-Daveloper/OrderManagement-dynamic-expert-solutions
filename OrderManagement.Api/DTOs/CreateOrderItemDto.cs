namespace OrderManagement.Api.DTOs;

public class CreateOrderItemDto
{
    public string ProductName { get; set; } = default!;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}

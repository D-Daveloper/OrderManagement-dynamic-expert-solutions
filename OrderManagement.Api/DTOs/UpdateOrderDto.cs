namespace OrderManagement.Api.DTOs;

public class UpdateOrderDto
{
    public List<CreateOrderItemDto> Items { get; set; } = new();
}

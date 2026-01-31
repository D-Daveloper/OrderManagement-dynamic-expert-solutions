namespace OrderManagement.Api.DTOs;

public class CreateOrderDto
{
    public List<CreateOrderItemDto> Items { get; set; } = new();
}

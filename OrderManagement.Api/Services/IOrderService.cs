using OrderManagement.Api.Domain;
using OrderManagement.Api.DTOs;

namespace OrderManagement.Api.Services;

public interface IOrderService
{
    Task<Order> CreateAsync(CreateOrderDto dto);
    Task<Order?> GetByIdAsync(Guid id);
    Task<List<Order>> GetAllAsync();
    Task<List<OrderItem>> GetItemsAsync(Guid orderId);
    Task<Order> UpdateAsync(Guid id, UpdateOrderDto dto);
    Task DeleteAsync(Guid id);
}

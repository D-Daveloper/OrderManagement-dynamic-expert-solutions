using Microsoft.EntityFrameworkCore;
using OrderManagement.Api.Domain;
using OrderManagement.Api.DTOs;
using OrderManagement.Api.Infrastructure;

namespace OrderManagement.Api.Services;

public class OrderService : IOrderService
{
    private readonly AppDbContext _db;

    public OrderService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Order> CreateAsync(CreateOrderDto dto)
    {
        var order = new Order
        {
            Id = Guid.NewGuid(),
            Items = dto.Items.Select(i => new OrderItem
            {
                Id = Guid.NewGuid(),
                ProductName = i.ProductName,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice
            }).ToList()
        };

        _db.Orders.Add(order);
        await _db.SaveChangesAsync();

        return order;
    }

    public async Task<Order?> GetByIdAsync(Guid id)
    {
        return await _db.Orders
            .Include(o => o.Items)
            .Include(o => o.Payments)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<List<Order>> GetAllAsync()
    {
        return await _db.Orders
            .Include(o => o.Items)
            .Include(o => o.Payments)
            .ToListAsync();
    }

    public async Task<List<OrderItem>> GetItemsAsync(Guid orderId)
    {
        return await _db.OrderItems
            .Where(i => i.OrderId == orderId)
            .ToListAsync();
    }

    public async Task<Order> UpdateAsync(Guid id, UpdateOrderDto dto)
    {
        var order = await _db.Orders
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o => o.Id == id) ?? throw new KeyNotFoundException("Order not found");
        if (order.Status == OrderStatus.Completed)
            throw new InvalidOperationException("Completed orders cannot be updated");

        _db.OrderItems.RemoveRange(order.Items);

        order.Items = [.. dto.Items.Select(i => new OrderItem
        {
            Id = Guid.NewGuid(),
            OrderId = order.Id,
            ProductName = i.ProductName,
            Quantity = i.Quantity,
            UnitPrice = i.UnitPrice
        })];

        await _db.SaveChangesAsync();
        return order;
    }

    public async Task DeleteAsync(Guid id)
    {
        var order = await _db.Orders.FindAsync(id);

        if (order == null)
            throw new KeyNotFoundException("Order not found");

        if (order.Status == OrderStatus.Completed)
            throw new InvalidOperationException("Completed orders cannot be deleted");

        _db.Orders.Remove(order);
        await _db.SaveChangesAsync();
    }
}

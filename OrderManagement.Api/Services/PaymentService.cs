using Microsoft.EntityFrameworkCore;
using OrderManagement.Api.Domain;
using OrderManagement.Api.DTOs;
using OrderManagement.Api.Infrastructure;

namespace OrderManagement.Api.Services;

public class PaymentService : IPaymentService
{
    private readonly AppDbContext _db;

    public PaymentService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Payment> CreateAsync(Guid orderId, CreatePaymentDto dto)
    {
        var order = await _db.Orders.FindAsync(orderId);

        if (order == null)
            throw new KeyNotFoundException("Order not found");

        if (order.Status == OrderStatus.Completed)
            throw new InvalidOperationException("Order already completed");

        var payment = new Payment
        {
            Id = Guid.NewGuid(),
            OrderId = orderId,
            Amount = dto.Amount
        };

        order.Status = OrderStatus.Completed;

        _db.Payments.Add(payment);
        await _db.SaveChangesAsync();

        return payment;
    }

    public async Task<List<Payment>> GetByOrderIdAsync(Guid orderId)
    {
        return await _db.Payments
            .Where(p => p.OrderId == orderId)
            .ToListAsync();
    }

    public async Task<List<Payment>> GetAllAsync()
    {
        return await _db.Payments.ToListAsync();
    }
}

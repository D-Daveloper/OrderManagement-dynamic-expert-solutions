using OrderManagement.Api.Domain;
using OrderManagement.Api.DTOs;

namespace OrderManagement.Api.Services;

public interface IPaymentService
{
    Task<Payment> CreateAsync(Guid orderId, CreatePaymentDto dto);
    Task<List<Payment>> GetByOrderIdAsync(Guid orderId);
    Task<List<Payment>> GetAllAsync();
}

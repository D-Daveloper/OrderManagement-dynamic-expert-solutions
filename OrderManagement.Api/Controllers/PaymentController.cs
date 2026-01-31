using Microsoft.AspNetCore.Mvc;
using OrderManagement.Api.DTOs;
using OrderManagement.Api.Services;

namespace OrderManagement.Api.Controllers;

[ApiController]
[Route("api")]
public class PaymentsController(IPaymentService paymentService) : ControllerBase
{
    private readonly IPaymentService _paymentService = paymentService;

    [HttpPost("orders/{orderId:guid}/payments")]
    public async Task<IActionResult> Create(Guid orderId, [FromBody] CreatePaymentDto dto)
    {
        try
        {
            var payment = await _paymentService.CreateAsync(orderId, dto);
            return Ok(payment);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("orders/{orderId:guid}/payments")]
    public async Task<IActionResult> GetByOrder(Guid orderId)
    {
        var payments = await _paymentService.GetByOrderIdAsync(orderId);
        return Ok(payments);
    }

    [HttpGet("payments")]
    public async Task<IActionResult> GetAll()
    {
        var payments = await _paymentService.GetAllAsync();
        return Ok(payments);
    }
}

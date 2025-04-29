using Microsoft.AspNetCore.Mvc;
using MotoRent.Application.DTOs;
using MotoRent.Application.Interfaces.Service;

[ApiController]
[Route("api/deliveryman")]
public class DeliverymanController : ControllerBase
{
    private readonly IDeliverymanService _service;

    public DeliverymanController(IDeliverymanService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateDeliverymanDto dto)
    {
        var result = await _service.CreateAsync(dto);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _service.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateDeliverymanDto dto)
    {
        await _service.UpdateAsync(id, dto);
        return NoContent();
    }
}

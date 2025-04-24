using Microsoft.AspNetCore.Mvc;
using MotoRent.Application.Interfaces;
using MotoRent.Domain.Entities;

namespace MotoRent.API.Controllers;

[ApiController]
[Route("api/rentals")]
public class RentalsController : ControllerBase
{
    private readonly IRentalService _rentalService;

    public RentalsController(IRentalService rentalService)
    {
        _rentalService = rentalService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Rental rental)
    {
        var result = await _rentalService.CreateRentalAsync(rental);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPatch("{id}/finish")]
    public async Task<IActionResult> Finish(Guid id, [FromQuery] DateTime realEndDate)
    {
        var result = await _rentalService.FinishRentalAsync(id, realEndDate);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var rentals = await _rentalService.GetAllAsync();
        return Ok(rentals);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var rental = await _rentalService.GetByIdAsync(id);
        if (rental == null) return NotFound();
        return Ok(rental);
    }
}

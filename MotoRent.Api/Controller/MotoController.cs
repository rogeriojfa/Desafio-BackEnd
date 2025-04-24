using Microsoft.AspNetCore.Mvc;
using MotoRent.Application.DTOs;
using MotoRent.Application.Interfaces;
using MotoRent.Application.Services;
using MotoRent.Domain.Entities;
using MotoRent.Infrastructure.Repositories;


[ApiController]
[Route("api/motos")]
public class MotosController : ControllerBase
{
    private readonly IMotoService _motoService;
    private readonly IRentalService _rentalService;
    private readonly IMotoRepository _repository;

    public MotosController(IMotoService motoService, IMotoRepository repository, IRentalService rentalService)
    {
        _motoService = motoService;
        _rentalService = rentalService;
        _repository = repository;
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] CreateMotoRequest request)
    {
        var moto = new Moto
        {
            Id = Guid.NewGuid(),
            Year = request.Year,
            Model = request.Model,
            Plate = request.Plate
        };

        await _motoService.AddAsync(moto);

        return CreatedAtAction(nameof(GetByLicensePlate), new { plate = moto.Plate }, moto);
    }

    [HttpGet()]
    public async Task<IActionResult> GetByLicensePlate([FromQuery] string licensePlate)
    {
        if (string.IsNullOrEmpty(licensePlate))
            return BadRequest("License plate is required.");

        var moto = await _repository.GetByLicensePlateAsync(licensePlate.ToUpper());
        if (moto != null)
            return Ok(moto);

        return NotFound($"Moto com placa {licensePlate} não encontrada.");
    }

    [HttpPut("{rentalId}/complete")]
    public async Task<ActionResult<RentalResponse>> CompleteRental(Guid rentalId, [FromBody] CompleteRentalRequest request)
    {
        try
        {
            var rentalResponse = await _rentalService.CompleteRentalAsync(rentalId, request.ReturnDate);
            return Ok(rentalResponse);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}

using MotoRent.Application.DTOs;
using MotoRent.Application.Interfaces;

public class RentalService : IRentalService
{
    private readonly IRentalRepository _rentalRepository;

    public RentalService(IRentalRepository rentalRepository)
    {
        _rentalRepository = rentalRepository;
    }

    public async Task<Rental> CreateRentalAsync(Rental rental)
    {
        await _rentalRepository.AddAsync(rental);
        return rental;
    }

    public async Task<Rental?> GetRentalByIdAsync(Guid id)
    {
        return await _rentalRepository.GetByIdAsync(id);
    }

    public async Task CompleteRentalAsync(Guid rentalId, DateTime actualEndDate)
    {
        var rental = await _rentalRepository.GetByIdAsync(rentalId);

        if (rental == null)
            throw new Exception("Locação não encontrada.");

        rental.ActualEndDate = actualEndDate;

        // Exemplo de cálculo de preço final
        var baseValue = rental.CalculateBaseValue();
        var finalPrice = baseValue;

        if (rental.IsEarly)
        {
            var unusedDays = (rental.ExpectedEndDate - actualEndDate).Days;
            var penaltyRate = rental.PlanDays == 7 ? 0.2m : 0.4m;
            var penalty = unusedDays * rental.DailyRate * penaltyRate;
            finalPrice += penalty;
        }
        else if (rental.IsLate)
        {
            var extraDays = (actualEndDate - rental.ExpectedEndDate).Days;
            var fee = extraDays * 50m;
            finalPrice += fee;
        }

        rental.TotalPrice = finalPrice;

        await _rentalRepository.UpdateAsync(rental);
    }

    async Task<RentalResponse> IRentalService.CompleteRentalAsync(Guid rentalId, DateTime actualEndDate)
    {
        var rental = await _rentalRepository.GetByIdAsync(rentalId);

        if (rental == null)
            throw new Exception("Locação não encontrada.");

        rental.ActualEndDate = actualEndDate;

        var baseValue = rental.CalculateBaseValue();
        var finalPrice = baseValue;

        if (rental.IsEarly)
        {
            var unusedDays = (rental.ExpectedEndDate - actualEndDate).Days;
            var penaltyRate = rental.PlanDays == 7 ? 0.2m : 0.4m;
            finalPrice += unusedDays * rental.DailyRate * penaltyRate;
        }
        else if (rental.IsLate)
        {
            var extraDays = (actualEndDate - rental.ExpectedEndDate).Days;
            finalPrice += extraDays * 50m;
        }

        rental.TotalPrice = finalPrice;
        await _rentalRepository.UpdateAsync(rental);

        return new RentalResponse
        {
            Id = rental.Id,
            StartDate = rental.StartDate,
            ExpectedEndDate = rental.ExpectedEndDate,
            ActualEndDate = rental.ActualEndDate,
            TotalPrice = rental.TotalPrice
        };
    }

    public Task<Rental?> FinishRentalAsync(Guid rentalId, DateTime realEndDate)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Rental>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Rental?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}

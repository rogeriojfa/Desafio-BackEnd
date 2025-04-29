using MassTransit;
using MotoRent.Application.DTOs;
using MotoRent.Application.Interfaces;
using MotoRent.Domain.Enums;
using MotoRent.Domain.Results;


public class RentalService : IRentalService
{
    private readonly IRentalRepository _rentalRepository;
    private readonly IMotoRepository _motoRepository;
    private readonly IDeliverymanRepository _deliverymanRepository;

    public RentalService(IRentalRepository rentalRepository, IMotoRepository motoRepository, IDeliverymanRepository deliverymanRepository)
    {
        _rentalRepository = rentalRepository;
        _motoRepository = motoRepository;
        _deliverymanRepository = deliverymanRepository;
    }

    public async Task<Rental> CreateRentalAsync(Guid motoId, Guid deliverymanId, int days, decimal dailyRate)
    {
        Deliveryman? deliveryMan = await _deliverymanRepository.GetByIdAsync(deliverymanId);

        var rental = RentalFactory.Create(motoId, deliveryMan, RentalPlan.Plan45Days);

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

    public async Task<Result> FinishRentalAsync(Guid rentalId)
    {
        var rental = await _rentalRepository.GetByIdAsync(rentalId);

        if (rental == null)
            return Result.Failure("Rental not found.");

        if (rental.Status != RentalStatus.Active)
            return Result.Failure("Rental is not active.");

        rental.ActualEndDate = DateTime.UtcNow;
        rental.Status = RentalStatus.Finished;

        var moto = await _motoRepository.GetByIdAsync(rental.MotoId);
        if (moto == null)
            return Result.Failure("Moto not found.");

        moto.IsAvailable = true;

        await _rentalRepository.UpdateAsync(rental);
        await _motoRepository.UpdateAsync(moto);

        return Result.Success();
    }


    public async Task<IEnumerable<Rental>> GetAllAsync()
    {
        return await _rentalRepository.GetAllAsync();
    }

    Task<ValidationResultExtensions.Result> IRentalService.FinishRentalAsync(Guid rentalId)
    {
        throw new NotImplementedException();
    }

    public Task<Rental> CreateRentalAsync(Rental rental)
    {
        throw new NotImplementedException();
    }
}

using MotoRent.Application.Interfaces;
using MotoRent.Domain.Entities;
using MotoRent.Domain.Services;

namespace MotoRent.Application.Services;

public class RentalService : IRentalService
{
    private readonly IRentalRepository _repository;
    private readonly RentalCalculator _calculator;

    public RentalService(IRentalRepository repository)
    {
        _repository = repository;
        _calculator = new RentalCalculator();
    }

    public async Task<RentalResponse> CreateAsync(CreateRentalRequest request)
        {
            // 1. Validar o entregador
            var deliveryman = await _deliverymanRepository.GetByIdAsync(request.DeliverymanId);
            if (deliveryman == null)
                throw new ArgumentException("Entregador não encontrado.");

            if (deliveryman.CnhType != "A" && deliveryman.CnhType != "A+B")
                throw new ArgumentException("Entregador deve possuir CNH do tipo A ou A+B.");

            // 2. Validar a moto
            var moto = await _motoRepository.GetByIdAsync(request.MotoId);
            if (moto == null)
                throw new ArgumentException("Moto não encontrada.");

            if (await _motoRepository.HasAnyRentalAsync(request.MotoId))
                throw new ArgumentException("Moto já está em uso.");

            // 3. Validar a data de início
            if (request.StartDate.Date <= DateTime.Now.Date)
                throw new ArgumentException("A data de início deve ser o dia seguinte à data atual.");

            // 4. Validar o plano de dias
            var validPlanDays = new[] { 7, 15, 30, 45, 50 };
            if (!validPlanDays.Contains(request.PlanDays))
                throw new ArgumentException("Plano de dias inválido. Os planos disponíveis são: 7, 15, 30, 45 ou 50 dias.");

            // 5. Calcular a data de término esperada
            var expectedEndDate = request.StartDate.AddDays(request.PlanDays);

            // 6. Criar a locação
            var rental = new Rental
            {
                DeliverymanId = request.DeliverymanId,
                MotoId = request.MotoId,
                PlanDays = request.PlanDays,
                StartDate = request.StartDate,
                ExpectedEndDate = expectedEndDate
            };

            await _rentalRepository.AddAsync(rental);

            // 7. Retornar o resultado
            var rentalResponse = new RentalResponse
            {
                Id = rental.Id,
                DeliverymanId = rental.DeliverymanId,
                MotoId = rental.MotoId,
                PlanDays = rental.PlanDays,
                StartDate = rental.StartDate,
                ExpectedEndDate = rental.ExpectedEndDate
            };

            return rentalResponse;
        }

public async Task<RentalResponse> CompleteRentalAsync(Guid rentalId, DateTime returnDate)
{
    // 1. Obter a locação
    var rental = await _rentalRepository.GetByIdAsync(rentalId);
    if (rental == null)
        throw new ArgumentException("Locação não encontrada.");

    // 2. Verificar se a data de devolução é válida
    if (returnDate < rental.StartDate)
        throw new ArgumentException("A data de devolução não pode ser anterior à data de início.");

    // 3. Calcular o valor total da locação
    decimal totalPrice = CalculateTotalPrice(rental, returnDate);

    // 4. Atualizar a locação com a data de devolução e o valor total
    rental.ActualEndDate = returnDate;
    rental.TotalPrice = totalPrice;

    await _rentalRepository.UpdateAsync(rental);

    // 5. Retornar a locação com os detalhes
    var rentalResponse = new RentalResponse
    {
        Id = rental.Id,
        DeliverymanId = rental.DeliverymanId,
        MotoId = rental.MotoId,
        PlanDays = rental.PlanDays,
        StartDate = rental.StartDate,
        ExpectedEndDate = rental.ExpectedEndDate,
        ActualEndDate = rental.ActualEndDate,
        TotalPrice = rental.TotalPrice
    };

    return rentalResponse;
}

private decimal CalculateTotalPrice(Rental rental, DateTime returnDate)
{
    // 6. Verificar o plano de dias e calcular o valor total
    var planPricePerDay = rental.PlanDays switch
    {
        7 => 30m,
        15 => 28m,
        30 => 22m,
        45 => 20m,
        50 => 18m,
        _ => 0m
    };

    var totalPrice = rental.PlanDays * planPricePerDay;

    // 7. Verificar se há multa por devolução antecipada
    if (returnDate < rental.ExpectedEndDate)
    {
        var daysNotUsed = (rental.ExpectedEndDate - returnDate).Days;
        decimal penaltyPercentage = rental.PlanDays switch
        {
            7 => 0.20m, // 20% de multa
            15 => 0.40m, // 40% de multa
            _ => 0m
        };

        var penalty = (daysNotUsed * planPricePerDay) * penaltyPercentage;
        totalPrice -= penalty;
    }

    // 8. Verificar se há valor adicional por devolução tardia
    if (returnDate > rental.ExpectedEndDate)
    {
        var daysLate = (returnDate - rental.ExpectedEndDate).Days;
        totalPrice += daysLate * 50m; // R$50 por dia de atraso
    }

    return totalPrice;
}


    public Task<IEnumerable<Rental>> GetAllAsync() => _repository.GetAllAsync();
    public Task<Rental?> GetByIdAsync(Guid id) => _repository.GetByIdAsync(id);
}

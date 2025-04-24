using MotoRent.Domain.Entities;

namespace MotoRent.Domain.Strategies;

public interface IRentalCalculationStrategy
{
    decimal Calculate(Rental rental, DateTime actualReturnDate);
}

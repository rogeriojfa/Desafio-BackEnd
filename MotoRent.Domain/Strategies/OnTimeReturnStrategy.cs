using MotoRent.Domain.Entities;

namespace MotoRent.Domain.Strategies;

public class OnTimeReturnStrategy : IRentalCalculationStrategy
{
    public decimal Calculate(Rental rental, DateTime actualReturnDate)
    {
        return rental.CalculateBaseValue();
    }
}

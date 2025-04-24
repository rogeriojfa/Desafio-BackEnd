using MotoRent.Domain.Entities;
using MotoRent.Domain.Strategies;

namespace MotoRent.Domain.Services;

public class RentalCalculator
{
    public decimal Calculate(Rental rental, DateTime returnDate)
    {
        IRentalCalculationStrategy strategy;

        if (returnDate < rental.ExpectedEndDate)
        {
            strategy = new EarlyReturnStrategy();
        }
        else if (returnDate > rental.ExpectedEndDate)
        {
            strategy = new LateReturnStrategy();
        }
        else
        {
            strategy = new OnTimeReturnStrategy();
        }

        return strategy.Calculate(rental, returnDate);
    }
}

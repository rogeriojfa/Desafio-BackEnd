using MotoRent.Domain.Entities;

namespace MotoRent.Domain.Strategies;

public class EarlyReturnStrategy : IRentalCalculationStrategy
{
    public decimal Calculate(Rental rental, DateTime actualReturnDate)
    {
        int usedDays = (actualReturnDate - rental.StartDate).Days;
        int unusedDays = rental.PlanDays - usedDays;
        decimal baseValue = usedDays * rental.DailyRate;

        decimal penaltyRate = rental.PlanDays switch
        {
            7 => 0.20m,
            15 => 0.40m,
            _ => 0.0m
        };

        decimal penalty = unusedDays * rental.DailyRate * penaltyRate;

        return baseValue + penalty;
    }
}


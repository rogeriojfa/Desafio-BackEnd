namespace MotoRent.Domain.Strategies;

public class LateReturnStrategy : IRentalCalculationStrategy
{
    public decimal Calculate(Rental rental, DateTime actualReturnDate)
    {
        int originalDays = rental.Days;
        int totalDays = (actualReturnDate - rental.StartDate).Days;
        int extraDays = totalDays - originalDays;

        return rental.DailyRate * originalDays + (extraDays * 50m);
    }
}

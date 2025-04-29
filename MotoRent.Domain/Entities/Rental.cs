using System.ComponentModel.DataAnnotations.Schema;
using MotoRent.Domain.Entities;
using MotoRent.Domain.Enums;

public class Rental
{
    public Guid Id { get; set; }

    public Guid DeliverymanId { get; set; }
    public Guid MotoId { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime ExpectedEndDate { get; set; }
    public DateTime? ActualEndDate { get; set; }

    public int PlanDays { get; set; }
    public decimal DailyRate { get; set; }
    public decimal? TotalPrice { get; set; }

    public RentalStatus Status { get; set; }

    [NotMapped]
    public bool IsLate => ActualEndDate.HasValue && ActualEndDate.Value > ExpectedEndDate;

    [NotMapped]
    public bool IsEarly => ActualEndDate.HasValue && ActualEndDate.Value < ExpectedEndDate;

    public decimal CalculateBaseValue()
    {
        return PlanDays * DailyRate;
    }

    public int DaysUsed()
    {
        if (ActualEndDate == null)
            return 0;

        return (ActualEndDate.Value - StartDate).Days;
    }
}

namespace MotoRent.Domain.Entities;

public class Rental
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid MotoId { get; set; }
    public Guid DeliveryManId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime ExpectedEndDate { get; set; }
    public DateTime? EndDate { get; set; } // só é preenchida ao encerrar
    public int Days { get; set; }
    public decimal DailyRate { get; set; }

    public decimal CalculateBaseValue() => Days * DailyRate;
}


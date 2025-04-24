namespace MotoRent.Application.DTOs;

public class RentalResponse
{
    public Guid Id { get; set; }
    public Guid DeliverymanId { get; set; }
    public Guid MotoId { get; set; }
    public int PlanDays { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime ExpectedEndDate { get; set; }
    public DateTime? ActualEndDate { get; set; }
    public decimal? TotalPrice { get; set; }
}

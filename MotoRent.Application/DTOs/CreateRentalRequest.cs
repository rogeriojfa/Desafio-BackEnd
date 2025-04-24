namespace MotoRent.Application.DTOs;

public class CreateRentalRequest
{
    public Guid DeliverymanId { get; set; }
    public Guid MotoId { get; set; }
    public int PlanDays { get; set; } // 7, 15, 30, 45 ou 50
    public DateTime StartDate { get; set; } // Deve ser o dia seguinte à data atual
}

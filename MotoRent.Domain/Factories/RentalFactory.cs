using MotoRent.Domain.Entities;
using MotoRent.Domain.Enums;

namespace MotoRent.Domain.Factories;

public static class RentalFactory
{
    public static Rental Create(Guid motoId, DeliveryMan deliveryMan, RentalPlan plan)
    {
        if (deliveryMan.CnhType != CnhType.A && deliveryMan.CnhType != CnhType.AB)
            throw new InvalidOperationException("Entregador não habilitado com CNH tipo A.");

        var startDate = DateTime.UtcNow.Date.AddDays(1);

        var (days, rate) = plan switch
        {
            RentalPlan.Plan7Days => (7, 30m),
            RentalPlan.Plan15Days => (15, 28m),
            RentalPlan.Plan30Days => (30, 22m),
            RentalPlan.Plan45Days => (45, 20m),
            RentalPlan.Plan50Days => (50, 18m),
            _ => throw new ArgumentException("Plano de locação inválido.")
        };

        return new Rental
        {
            MotoId = motoId,
            DeliveryManId = deliveryMan.Id,
            StartDate = startDate,
            ExpectedEndDate = startDate.AddDays(days),
            Days = days,
            DailyRate = rate
        };
    }
}

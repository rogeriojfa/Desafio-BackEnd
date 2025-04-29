using MotoRent.Domain.Enums;

public static class RentalFactory
{
    public static Rental Create(Guid motoId, Deliveryman deliveryman, RentalPlan plan)
    {
        if (deliveryman.CnhType != CnhType.A && deliveryman.CnhType != CnhType.AB)
            throw new InvalidOperationException("Entregador não habilitado com CNH tipo A.");

        var startDate = DateTime.UtcNow.Date.AddDays(1);

        var (planDays, dailyRate) = GetPlanDetails(plan);

        return new Rental
        {
            Id = Guid.NewGuid(),
            MotoId = motoId,
            DeliverymanId = deliveryman.Id,
            StartDate = startDate,
            ExpectedEndDate = startDate.AddDays(planDays),
            PlanDays = planDays,
            DailyRate = dailyRate,
            Status = RentalStatus.Active // Presumo que você tenha um status inicial
        };
    }

    private static (int days, decimal rate) GetPlanDetails(RentalPlan plan)
    {
        return plan switch
        {
            RentalPlan.Plan7Days => (7, 30m),
            RentalPlan.Plan15Days => (15, 28m),
            RentalPlan.Plan30Days => (30, 22m),
            RentalPlan.Plan45Days => (45, 20m),
            RentalPlan.Plan50Days => (50, 18m),
            _ => throw new ArgumentException("Plano de locação inválido.")
        };
    }
}

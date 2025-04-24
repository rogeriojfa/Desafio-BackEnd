using MotoRent.Domain.Enums;

public class Deliveryman
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string CNPJ { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public string CNH { get; set; } = null!;
    public CnhType CnhType { get; set; }
    public string? CNHImagePath { get; set; }

    public bool IsEligibleForRental()
    {
        return CnhType == CnhType.A || CnhType == CnhType.AB;
    }
}
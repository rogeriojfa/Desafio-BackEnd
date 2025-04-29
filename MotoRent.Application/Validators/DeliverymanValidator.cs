using FluentValidation;

namespace MotoRent.Application.Validators;

public class DeliverymanValidator : AbstractValidator<Deliveryman>
{
    public DeliverymanValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O nome é obrigatório.");

        RuleFor(x => x.Cnpj)
            .NotEmpty().WithMessage("O CNPJ é obrigatório.")
            .Matches(@"^\d{14}$").WithMessage("O CNPJ deve conter 14 dígitos numéricos.");

        RuleFor(x => x.BirthDate)
            .Must(BeAtLeast18).WithMessage("O entregador deve ter pelo menos 18 anos.");

        RuleFor(x => x.Cnh)
            .NotEmpty().WithMessage("O número da CNH é obrigatório.");

        RuleFor(x => x.CnhType)
            .IsInEnum().WithMessage("Tipo de CNH inválido. Deve ser A, B ou AB.");
    }

    private bool BeAtLeast18(DateTime birthDate)
    {
        return birthDate <= DateTime.Today.AddYears(-18);
    }
}

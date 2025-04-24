using FluentValidation;

public class CreateMotoRequestValidator : AbstractValidator<CreateMotoRequest>
{
    public CreateMotoRequestValidator()
    {
        RuleFor(x => x.Year).GreaterThan(2000);
        RuleFor(x => x.Model).NotEmpty();
        RuleFor(x => x.Plate).NotEmpty();
    }
}

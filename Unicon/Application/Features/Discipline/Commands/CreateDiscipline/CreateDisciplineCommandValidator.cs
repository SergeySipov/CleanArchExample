using FluentValidation;

namespace Application.Features.Discipline.Commands.CreateDiscipline;

public class CreateDisciplineCommandValidator : AbstractValidator<CreateDisciplineCommand>
{
    public CreateDisciplineCommandValidator()
    {
        RuleFor(c => c.Description)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(c => c.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(c => c.NumberOfHours)
            .GreaterThan(0);
    }
}

using FluentValidation;

namespace Application.Features.Discipline.Queries.GetDisciplineWithPagination;

public class GetDisciplinesWithPaginationQueryValidator : AbstractValidator<GetDisciplinesWithPaginationQuery>
{
    public GetDisciplinesWithPaginationQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(int.MaxValue);

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(int.MaxValue);
    }
}

using FluentValidation;
using TimeTracker.Application.Dtos;

namespace TimeTracker.Application.Validators
{
    public class GroupValidator : AbstractValidator<GroupDto>
    {
        public GroupValidator()
        {
            RuleFor(g => g.Name).NotEmpty();
            RuleFor(g => g.Name).MaximumLength(500);
            When(g => !string.IsNullOrEmpty(g.Code), () =>
            {
                RuleFor(g => g.Code).MinimumLength(4);
                RuleFor(g => g.Code).MaximumLength(10);
            });
        }
    }
}

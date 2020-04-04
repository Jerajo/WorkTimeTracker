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
        }
    }
}

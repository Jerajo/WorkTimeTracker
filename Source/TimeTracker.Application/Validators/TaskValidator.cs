using FluentValidation;
using TimeTracker.Application.Dtos;

namespace TimeTracker.Application.Validators
{
    public class TaskValidator : AbstractValidator<TaskDto>
    {
        public TaskValidator()
        {
            RuleSet(nameof(TaskDto.Id), () =>
            {
                RuleFor(t => t.Id).GreaterThan(0);
            });
            RuleFor(t => t.Name).NotEmpty();
            RuleFor(t => t.Name).MaximumLength(500);
            When(t => t.GroupId != null, () =>
            {
                RuleFor(t => t.GroupId).GreaterThan(0);
            });
            When(t => t.DescriptionId != null, () =>
            {
                RuleFor(t => t.DescriptionId).GreaterThan(0);
            });
        }
    }
}

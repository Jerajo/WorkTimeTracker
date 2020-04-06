using FluentValidation;
using TimeTracker.Application.Dtos;

namespace TimeTracker.Application.Validators
{
    public class TemplateValidator : AbstractValidator<TaskExportTemplateDto>
    {
        public TemplateValidator()
        {
            RuleSet(nameof(TaskDto.Id), () =>
            {
                RuleFor(t => t.Id).GreaterThan(0);
            });
            RuleFor(t => t.Template).NotEmpty();
            RuleFor(t => t.Template).MaximumLength(1000);
        }
    }
}

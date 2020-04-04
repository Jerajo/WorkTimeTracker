using FluentValidation;
using System;
using TimeTracker.Application.Dtos;

namespace TimeTracker.Application.Validators
{
    public class ScheduleValidator : AbstractValidator<ScheduleDto>
    {
        public ScheduleValidator()
        {
            RuleSet(nameof(TaskDto.Id), () =>
            {
                RuleFor(t => t.Id).GreaterThan(0);
            });
            RuleFor(s => s.ScheduleDate).NotNull();
            RuleFor(s => s.ScheduleDate).LessThan(DateTimeOffset.Now);
        }
    }
}

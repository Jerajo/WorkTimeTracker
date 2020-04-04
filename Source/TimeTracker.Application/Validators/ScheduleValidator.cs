using FluentValidation;
using System;
using TimeTracker.Application.Dtos;

namespace TimeTracker.Application.Validators
{
    public class ScheduleValidator : AbstractValidator<ScheduleDto>
    {
        public ScheduleValidator()
        {
            RuleFor(s => s.ScheduleDate).NotNull();
            RuleFor(s => s.ScheduleDate).LessThan(DateTimeOffset.Now);
        }
    }
}

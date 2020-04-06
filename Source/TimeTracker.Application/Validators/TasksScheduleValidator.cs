using FluentValidation;
using System;
using TimeTracker.Application.Dtos;

namespace TimeTracker.Application.Validators
{
    public class TasksScheduleValidator : AbstractValidator<TasksScheduleDto>
    {
        public TasksScheduleValidator()
        {
            RuleSet(nameof(TaskDto.Id), () =>
            {
                RuleFor(t => t.Id).GreaterThan(0);
            });
            RuleFor(ts => ts.Duration).NotNull();
            RuleFor(ts => ts.Duration).GreaterThanOrEqualTo(TimeSpan.Zero);
            RuleFor(ts => ts.ScheduleId).NotNull();
            RuleFor(ts => ts.ScheduleId).GreaterThan(0);
            RuleFor(ts => ts.TaskId).NotNull();
            RuleFor(ts => ts.TaskId).GreaterThan(0);
        }
    }
}

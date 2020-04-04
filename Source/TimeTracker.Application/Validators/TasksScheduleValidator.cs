using FluentValidation;
using TimeTracker.Application.Dtos;

namespace TimeTracker.Application.Validators
{
    public class TasksScheduleValidator : AbstractValidator<TasksScheduleDto>
    {
        public TasksScheduleValidator()
        {
            RuleFor(ts => ts.Duration).NotNull();
            RuleFor(ts => ts.ScheduleId).NotNull();
            RuleFor(ts => ts.ScheduleId).GreaterThan(0);
            RuleFor(ts => ts.TaskId).NotNull();
            RuleFor(ts => ts.TaskId).GreaterThan(0);
        }
    }
}

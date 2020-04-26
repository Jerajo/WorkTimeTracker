using FluentValidation;
using Ninject.Modules;
using System.Windows.Input;
using TimeTracker.Application.Commands;
using TimeTracker.Application.Contracts;
using TimeTracker.Application.Factories;
using TimeTracker.Application.Queries;
using TimeTracker.Application.Validators;
using IValidatorFactory = TimeTracker.Application.Contracts.IValidatorFactory;

namespace TimeTracker.Xamarin.Configuration
{
    /// <summary>
    /// Represents the application layer dependency injection configuration.
    /// </summary>
    public class ApplicationModule : NinjectModule
    {
        /// <summary>
        /// Load all dependencies for the application layer.
        /// </summary>
        public override void Load()
        {
            // --------------- // QUERIES // --------------- //
            Bind<IQuery>().To<GetTasksQuery>();
            Bind<IQuery>().To<GetGroupsQuery>();

            // --------------- // COMMANDS // --------------- //
            Bind<ICommand>().To<CreateTaskCommand>();
            Bind<ICommand>().To<UpdateTaskCommand>();
            Bind<ICommand>().To<DeleteTaskCommand>();
            Bind<ICommand>().To<TrackWorkTimeCommand>();
            Bind<ICommand>().To<CreateGroupCommand>();
            Bind<ICommand>().To<UpdateGroupCommand>();
            Bind<ICommand>().To<DeleteGroupCommand>();

            // --------------- // VALIDATORS // --------------- //
            Bind<IValidator>().To<GroupValidator>();
            Bind<IValidator>().To<TaskValidator>();
            Bind<IValidator>().To<ScheduleValidator>();
            Bind<IValidator>().To<TasksScheduleValidator>();

            // --------------- // FACTORIES // --------------- //
            Bind<IValidatorFactory>().To<ValidatorFactory>();
            Bind<IQueryFactory>().To<QueryFactory>();
            Bind<ICommandFactory>().To<CommandFactory>();
        }
    }
}

using Murk.Command;
using Ninject;
using Prism.Commands;
using System;
using System.Threading.Tasks;
using TimeTracker.Xamarin.Contracts;

namespace TimeTracker.Xamarin.Services
{
    public class CommandFactory : ICommandFactory
    {
        private readonly IKernel _kernel;

        public CommandFactory(IKernel kernel) => _kernel = kernel;

        public DelegateCommand<TParameter> Make<TParameter, TCommand>(
            Func<TParameter, Task<bool>> canExecute)
                where TCommand : ICommandDisableAbleAsync<TParameter>
                where TParameter : class
        {
            var command = _kernel.Get<TCommand>();

            if (canExecute is null)
                canExecute = command.CanExecuteAsync;

            return new DelegateCommandAsync<TParameter>(command.ExecuteAsync, canExecute);
        }

        public DelegateCommand<TParameter> Make<TParameter, TCommand>(bool disableAble = false)
            where TCommand : ICommandDisableAbleAsync<TParameter>
            where TParameter : class
        {
            var command = _kernel.Get<TCommand>();

            if (!disableAble)
                return new DelegateCommandAsync<TParameter>(command.ExecuteAsync);
            else
                return new DelegateCommandAsync<TParameter>(command.ExecuteAsync, command.CanExecuteAsync);
        }
    }
}

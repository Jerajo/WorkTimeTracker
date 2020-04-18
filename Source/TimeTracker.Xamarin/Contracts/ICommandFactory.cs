using Murk.Command;
using Prism.Commands;
using System;
using System.Threading.Tasks;

namespace TimeTracker.Xamarin.Contracts
{
    public interface ICommandFactory
    {
        DelegateCommand<TParameter> Make<TParameter, TCommand>(Func<TParameter, Task<bool>> canExecute = null)
            where TCommand : ICommandDisableAbleAsync<TParameter>
            where TParameter : class;
    }
}

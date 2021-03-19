using Prism.Commands;
using System;
using System.Threading.Tasks;
using AsyncOperation = System.Threading.Tasks.Task;

namespace TimeTracker.Xamarin.Services
{
    public class DelegateCommandAsync<T> : DelegateCommand<T> where T : class
    {
        public DelegateCommandAsync(Func<T, AsyncOperation> executeMethod) :
            base(async param => await executeMethod(param)) {}

        public DelegateCommandAsync(Func<T, AsyncOperation> executeMethod,
            Func<T, Task<bool>> canExecuteMethod) :
            base(async p => await executeMethod(p),
                p => canExecuteMethod(p).Result) {}
    }
}

using FluentValidation;
using Ninject;

namespace TimeTracker.Application.Factories
{

    /// <summary>
    /// Create an instance of a validator.
    /// </summary>
    public class ValidatorFactory : Contracts.IValidatorFactory
    {
        private readonly IKernel _kernel;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="kernel">DI container.</param>
        public ValidatorFactory(IKernel kernel)
        {
            _kernel = kernel;
        }

        /// <inheritdoc/>
        public T GetInstance<T>() where T : IValidator
        {
            return _kernel.Get<T>();
        }
    }
}

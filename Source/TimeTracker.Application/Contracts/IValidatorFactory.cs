using FluentValidation;

namespace TimeTracker.Application.Contracts
{
    /// <summary>
    /// Create an instance of a validator.
    /// </summary>
    public interface IValidatorFactory
    {
        /// <summary>
        /// Get an instance of a validator.
        /// </summary>
        /// <typeparam name="T">Validator type.</typeparam>
        T GetInstance<T>() where T : IValidator;
    }
}

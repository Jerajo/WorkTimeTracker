using System;

namespace TimeTracker.Domain.BaseClasses
{
    /// <summary>
    /// Disposable abstract class.
    /// </summary>
    public abstract class DisposableBase : IDisposable
    {
        /// <summary>
        /// Overridable dispose operation. Override this method and call base.Dispose(isDisposing).
        /// </summary>
        /// <param name="isDisposing">Indicates whether or not is disposing.</param>
        protected virtual void Dispose(bool isDisposing) { }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

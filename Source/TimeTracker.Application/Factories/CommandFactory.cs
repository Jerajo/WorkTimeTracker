﻿using Ninject;
using System.Windows.Input;
using TimeTracker.Application.Contracts;

namespace TimeTracker.Application.Factories
{
    /// <summary>
    /// Create an instance of an async command..
    /// </summary>
    public class CommandFactory : ICommandFactory
    {
        private readonly IKernel _kernel;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="kernel">DI container.</param>
        public CommandFactory(IKernel kernel)
        {
            _kernel = kernel;
        }

        /// <inheritdoc/>
        public T GetInstance<T>() where T : ICommand
        {
            return _kernel.Get<T>();
        }
    }
}

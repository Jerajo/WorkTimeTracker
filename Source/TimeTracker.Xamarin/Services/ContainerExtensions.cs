using DryIoc;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace TimeTracker.Xamarin.Services
{
    public static class ContainerExtensions
    {
        private static readonly Dictionary<string, (Type, Type)> _navigationContainer
            = new Dictionary<string, (Type, Type)>();

        public static IContainerRegistry RegisterRegion<TView, TViewModel>(
            this IContainerRegistry containerRegistry,
            string name = null)
            where TView : BindableObject
            where TViewModel : class
        {
            if (string.IsNullOrWhiteSpace(name))
                name = typeof(TView).Name;

            _navigationContainer.Add(name, (typeof(TView), typeof(TViewModel)));
            containerRegistry.Register<TViewModel>();
            containerRegistry.Register<TView>();

            return containerRegistry;
        }

        public static View ResolveRegion(this IContainer container,
            string name)
        {
            var (viewType, viewModelType) = _navigationContainer[name];

            var view = (View)container.Resolve(viewType, IfUnresolved.Throw);
            var viewModel = container.Resolve(viewModelType, IfUnresolved.Throw);

            view.BindingContext = viewModel;
            return view;
        }
    }
}

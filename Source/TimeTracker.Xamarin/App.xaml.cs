﻿using Ninject;
using Prism;
using Prism.Ioc;
using TimeTracker.Xamarin.Configuration;
using TimeTracker.Xamarin.Contracts;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TimeTracker.Xamarin
{
    public partial class App
    {
        public static IKernel Kernel { get; private set; }

        public App() : this(null, null) {}

        public App(IPlatformInitializer platformInitializer, IKernel kernel) : base(platformInitializer)
        {
            Kernel = kernel;
            Boosts();
        }

        private void Boosts()
        {
            Initialize();
            OnInitialized();
        }

        protected override void Initialize()
        {
            if (Kernel is null) return;
            base.Initialize();
        }

        protected override async void OnInitialized()
        {
            if (Kernel is null) return;
            InitializeComponent();

            await NavigationService.NavigateAsync("MainLayout");
            var regionNavigationService = Container.Resolve<IRegionNavigationService>();
            await regionNavigationService.NavigateToAsync("Tasks");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            if (Kernel is null) return;
            Kernel.Load(new PresentationModule(), new ApplicationModule(), new DomainModule());

            containerRegistry.RegisterNavigation()
                .RegisterContainer(Kernel)
                .RegisterTypes()
                .RegisterInstance<global::Xamarin.Forms.Application>(this);
        }
    }
}

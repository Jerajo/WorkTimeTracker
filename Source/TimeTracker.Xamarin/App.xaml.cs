using Ninject;
using Prism;
using Prism.Ioc;
using TimeTracker.Xamarin.Configuration;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TimeTracker.Xamarin
{
    public partial class App
    {
        private readonly IKernel _kernel;

        public App() : this(null, null) {}

        public App(IPlatformInitializer platformInitializer, IKernel kernel) : base(platformInitializer)
        {
            _kernel = kernel;
            Boosts();
        }

        private void Boosts()
        {
            Initialize();
            OnInitialized();
        }

        protected override void Initialize()
        {
            if (_kernel is null) return;
            base.Initialize();
        }

        protected override async void OnInitialized()
        {
            if (_kernel is null) return;
            InitializeComponent();
            await NavigationService.NavigateAsync("MainShell");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            if (_kernel is null) return;
            containerRegistry.RegisterNavigation()
                .RegisterContainer(_kernel)
                .RegisterTypes();
        }
    }
}

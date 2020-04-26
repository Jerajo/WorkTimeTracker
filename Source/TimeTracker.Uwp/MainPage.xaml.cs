using Microsoft.EntityFrameworkCore;
using Ninject;
using Prism;
using Prism.Ioc;
using System.Reflection;
using TimeTracker.Core.Contracts;
using TimeTracker.Xamarin.Configuration;
using Windows.Storage;
using Windows.UI.ViewManagement;

namespace WorkTimeTracker.UWP
{
    public sealed partial class MainPage
    {
        private readonly IKernel _kernel;
        private readonly UISettings _uiSettings;

        public MainPage()
        {
            InitializeComponent();

            var assemblyName = typeof(MainPage).GetTypeInfo().Assembly.GetName().Name;
            var connectionString = $"Data Source={ApplicationData.Current.LocalFolder.Path}/WorkTimeTracker.db";
            _kernel = new StandardKernel(new PersistenceModule(options => options
                .UseSqlite(connectionString, sql => sql.MigrationsAssembly(assemblyName))
                .UseLazyLoadingProxies()
                .Options));

            var dbContext = _kernel.Get<IDbContext>();
            if (!dbContext.CanConnect())
            {
                dbContext.EnsureCreated().Wait();
                dbContext.FetchInitialData().Wait();
            }

            LoadApplication(new TimeTracker.Xamarin.App(new UwpInitializer(), _kernel));

            _uiSettings = new UISettings();
            _uiSettings.ColorValuesChanged += ColorValuesChanged;
        }

        private void ColorValuesChanged(UISettings sender, object args)
        {
            Xamarin.Essentials.MainThread.BeginInvokeOnMainThread(() =>
            {
                TimeTracker.Xamarin.App.ApplyTheme();
            });
        }
    }

    public class UwpInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Ninject;
using Prism;
using Prism.Ioc;
using System.Reflection;
using TimeTracker.Core.Contracts;
using TimeTracker.Xamarin.Configuration;
using Windows.Storage;

namespace WorkTimeTracker.UWP
{
    public sealed partial class MainPage
    {
        private readonly IKernel _kernel;

        public MainPage()
        {
            InitializeComponent();

            var assemblyName = typeof(MainPage).GetTypeInfo().Assembly.GetName().Name;
            var connectionString = $"Data Source={ApplicationData.Current.LocalFolder.Path}/WorkTimeTracker.db";
            _kernel = new StandardKernel(new NinjectDiModule(options => options
                .UseSqlite(connectionString, sql => sql.MigrationsAssembly(assemblyName))
                .UseLazyLoadingProxies()
                .Options));
            //Debug.Fail(ApplicationData.Current.LocalFolder.Path);
            var dbContext = _kernel.Get<IDbContext>();
            if (!dbContext.CanConnect())
            {
                dbContext.EnsureCreated().Wait();
                dbContext.FetchInitialData().Wait();
            }

            LoadApplication(new TimeTracker.Xamarin.App(new UwpInitializer(), _kernel));
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

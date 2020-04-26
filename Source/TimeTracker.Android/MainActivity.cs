using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Microsoft.EntityFrameworkCore;
using Ninject;
using Prism;
using Prism.Ioc;
using System.Reflection;
using TimeTracker.Core.Contracts;
using TimeTracker.Xamarin;
using TimeTracker.Xamarin.Configuration;

namespace WorkTimeTracker.Droid
{
    [Activity(Label = "WorkTimeTracker",
        Icon = "@mipmap/ic_launcher",
        Theme = "@style/MainTheme",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize |
                               ConfigChanges.Orientation |
                               ConfigChanges.UiMode)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private IKernel _kernel;

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            App.ApplyTheme();

            var assemblyName = typeof(MainActivity).GetTypeInfo().Assembly.FullName;
            var connectionString = $"Data Source={ApplicationInfo.DataDir}/WorkTimeTracker.db";

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

            Xamarin.Essentials.Platform.Init(this, bundle);
            Xamarin.Forms.Forms.SetFlags("SwipeView_Experimental");
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App(new AndroidInitializer(), _kernel));
        }

        public override void OnRequestPermissionsResult(int requestCode,
            string[] permissions,
            [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode,
                permissions,
                grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}


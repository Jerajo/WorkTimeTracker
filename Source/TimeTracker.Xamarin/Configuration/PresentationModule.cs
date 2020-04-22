using AutoMapper;
using Ninject.Modules;

namespace TimeTracker.Xamarin.Configuration
{
    /// <summary>
    /// Represents the presentation layer dependency injection configuration.
    /// </summary>
    public class PresentationModule : NinjectModule
    {
        /// <summary>
        /// Load all dependencies for the presentation layer.
        /// </summary>
        public override void Load()
        {
            // --------------- // MAPPER // --------------- //
            var mapperConfiguration = CreateConfiguration();
            Bind<MapperConfiguration>().ToConstant(mapperConfiguration).InSingletonScope();
            Bind<IMapper>().ToMethod(ctx => new Mapper(mapperConfiguration));
            //Bind<IMapper>().ToMethod(ctx =>
            //    new Mapper(mapperConfiguration, type => ctx.Kernel.Get(type)));
        }

        #region Auxiliary Methods

        /// <summary>
        /// Add all profiles in current assembly.
        /// </summary>
        private MapperConfiguration CreateConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(GetType().Assembly);
            });

            return config;
        }

        #endregion
    }
}

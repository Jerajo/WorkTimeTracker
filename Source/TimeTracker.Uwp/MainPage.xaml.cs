using Ninject;
using TimeTracker.Uwp.ViewModels;
using Windows.UI.Xaml.Controls;

namespace TimeTracker.Uwp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly IKernel _kernel;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MainPage(IKernel kernel)
        {
            this.InitializeComponent();
            this._kernel = kernel;
            this.Configure();
        }

        private void Configure()
        {
            this.DataContext = _kernel.Get<MainPageViewModel>();
        }
    }
}

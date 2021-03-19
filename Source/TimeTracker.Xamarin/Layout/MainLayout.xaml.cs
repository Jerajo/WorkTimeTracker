using TimeTracker.Xamarin.Layout.Notifications;
using Xamarin.Forms.Xaml;

namespace TimeTracker.Xamarin.Layout
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainLayout
    {
        public MainLayout() => InitializeComponent();

        public async void PushNotification(Notification notification, int? millisecondsDuration = null)
        {
            await Notifications.Push(notification, millisecondsDuration);
        }
    }
}

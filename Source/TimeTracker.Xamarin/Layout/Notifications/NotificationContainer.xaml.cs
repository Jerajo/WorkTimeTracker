using Syncfusion.XForms.Cards;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeTracker.Xamarin.Layout.Notifications
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotificationContainer
    {
        public NotificationContainer()
        {
            InitializeComponent();
        }

        public async Task Push(SfCardView notification, int? millisecondsDuration = null)
        {
            Children.Insert(0, notification);
            await InAnimation(notification);
            if (millisecondsDuration is null)
                return;

            await Task.Delay(millisecondsDuration.Value);
            await OutAnimation(notification);
            //notification.IsDismissed = true;
            Children.Remove(notification);
        }

        private Task InAnimation(View view)
        {
            view.Opacity = 0.5d;
            view.TranslationX = view.Width;
            Parallel.Invoke(
                () => view.TranslateTo(0, 0, easing: Easing.BounceOut),
                () => view.FadeTo(1)
            );
            return Task.CompletedTask;
        }

        private Task OutAnimation(View view)
        {
            Parallel.Invoke(
                () => view.TranslateTo(view.Width, 0),
                () => view.FadeTo(0.5d)
            );
            return Task.Delay(300);
        }
    }
}

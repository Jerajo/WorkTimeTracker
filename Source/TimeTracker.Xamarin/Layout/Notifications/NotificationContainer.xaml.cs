using Syncfusion.XForms.Cards;
using System;
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

        private async void Notification_Dismissed(object sender, DismissedEventArgs e)
        {
            if (!(sender is SfCardView notification))
                return;
            await RemoveNotification(notification);
        }

        public async Task Push(SfCardView notification, int? millisecondsDuration = null)
        {
            notification.Dismissed += Notification_Dismissed;
            Children.Insert(0, notification);
            await InAnimation(notification);

            if (millisecondsDuration is null)
                return;
            await Task.Delay(millisecondsDuration.Value);
            await RemoveNotification(notification);
        }

        private Task RemoveNotification(SfCardView notification)
        {
            notification.Dismissed -= Notification_Dismissed;
            OutAnimation(notification);
            Children.Remove(notification);
            GC.Collect();
            return Task.CompletedTask;
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

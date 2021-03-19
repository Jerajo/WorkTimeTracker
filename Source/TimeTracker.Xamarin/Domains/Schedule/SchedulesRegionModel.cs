using DryIoc;
using Prism.Commands;
using System;
using TimeTracker.Core.Resources;
using TimeTracker.Xamarin.Contracts;
using TimeTracker.Xamarin.Layout.Notifications;

namespace TimeTracker.Xamarin.Domains.Schedule
{
    public class SchedulesRegionModel : RegionModelBase
    {
        private readonly Random _random;
        public SchedulesRegionModel(IContainer container) : base(container)
        {
            Title = Messages.RegionTitleTrack;
            _random = new Random();
            NotificationTest();
        }

        private async void NotificationTest()
        {
            var count = 0;
            while (count++ < 5)
            {
                PushNotification(Guid.NewGuid().ToString(),
                    (NotificationType)_random.Next(3), 3000);
                await System.Threading.Tasks.Task.Delay(1000);
            }
            PushRevertNotification(new DelegateCommand<object>(o => { }), null);
        }
    }
}

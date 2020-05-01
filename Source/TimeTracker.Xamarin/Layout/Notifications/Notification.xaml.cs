using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.PowerControls.Resources.Fonts;
using Xamarin.Forms.Xaml;

namespace TimeTracker.Xamarin.Layout.Notifications
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Notification
    {
        public Notification() : this("Text", NotificationType.Info) {}
        public Notification(string message, NotificationType notificationType)
        {
            InitializeComponent();
            Message = message;
            NotificationType = notificationType;
            SetStyle();
        }

        public string Icon
        {
            get => IconLabel.Text;
            set => IconLabel.Text = value;
        }
        public string Message
        {
            get => MessageLabel.Text;
            set => MessageLabel.Text = value;
        }
        public NotificationType NotificationType { get; set; }
        public ICommand RevertCommand { get; set; }
        public object CommandParameter { get; set; }

        private void Card_Tapped(object sender, System.EventArgs e)
        {
            RevertCommand?.Execute(CommandParameter);
        }

        private void SetStyle()
        {
            switch (NotificationType)
            {
                case NotificationType.Info:
                    BackgroundColor = Color.DeepSkyBlue;
                    Icon = FontAwesomeSolid.InfoCircle;
                    break;
                case NotificationType.Warning:
                    Icon = FontAwesomeSolid.ExclamationTriangle;
                    BackgroundColor = Color.Orange;
                    break;
                case NotificationType.Error:
                    Icon = FontAwesomeSolid.Bug;
                    BackgroundColor = Color.Red;
                    break;
                case NotificationType.Revert:
                    Icon = FontAwesomeSolid.History;
                    BackgroundColor = Color.Green;
                    break;
            }
        }
    }

    public enum NotificationType
    {
        Info,
        Warning,
        Error,
        Revert
    }
}

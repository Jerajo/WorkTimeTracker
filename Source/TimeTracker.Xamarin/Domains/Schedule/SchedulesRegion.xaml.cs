using Syncfusion.XForms.Buttons;
using Xamarin.Forms.Xaml;

namespace TimeTracker.Xamarin.Domains.Schedule
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SchedulesRegion
    {
        public SchedulesRegion()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            if (!(sender is SfButton view))
                return;
        }
    }
}

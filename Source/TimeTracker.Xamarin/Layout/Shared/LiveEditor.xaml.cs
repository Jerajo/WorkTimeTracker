using System;
using System.Linq;
using Xamarin.Forms.Xaml;
using FocusEventArgs = Xamarin.Forms.FocusEventArgs;

namespace TimeTracker.Xamarin.Layout.Shared
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LiveEditor
    {
        public LiveEditor() : this(null) {}
        public LiveEditor(object bindingContext)
        {
            InitializeComponent();
            BindingContext = bindingContext;
        }

        public event EventHandler StopEditing;

        private async void Entry_Unfocused(object sender, FocusEventArgs e)
        {
            await System.Threading.Tasks.Task.Delay(100);
            if (!Children.Any(x => x.IsFocused))
                StopEditing?.Invoke(this, EventArgs.Empty);
        }

        public void FocusEntry()
        {
            CodeEntry.Focus();
        }
    }
}

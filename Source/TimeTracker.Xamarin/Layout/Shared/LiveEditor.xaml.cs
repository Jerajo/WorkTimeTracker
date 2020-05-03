using System;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms.Xaml;
using FocusEventArgs = Xamarin.Forms.FocusEventArgs;

namespace TimeTracker.Xamarin.Layout.Shared
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LiveEditor
    {
        public LiveEditor() : this(null) {}
        public LiveEditor(object bindingContext, ICommand command = null)
        {
            InitializeComponent();

            BindingContext = bindingContext;
            if (command != null)
            {
                AddButton.IsVisible = true;
                AddButton.Command = command;
                AddButton.CommandParameter = BindingContext;
            }
        }

        public event EventHandler StopEditing;

        private async void Entry_Unfocused(object sender, FocusEventArgs e)
        {
            await System.Threading.Tasks.Task.Delay(100);
            if (!Children.Any(x => x.IsFocused)){}
                StopEditing?.Invoke(this, EventArgs.Empty);
        }

        public void FocusEntry()
        {
            CodeEntry.Focus();
        }
    }
}

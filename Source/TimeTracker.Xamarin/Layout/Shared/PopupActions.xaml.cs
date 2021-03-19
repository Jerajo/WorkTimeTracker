using Syncfusion.XForms.PopupLayout;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeTracker.Xamarin.Layout.Shared
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupActions
    {
        private readonly Action _editCommand;
        private readonly Action _deleteCommand;
        //public delegate void SwipeCommand(object sender, EventArgs e);

        public PopupActions() : this(null, null) {}
        public PopupActions(Action editCommand, Action deleteCommand)
        {
            InitializeComponent();
            _editCommand = editCommand;
            _deleteCommand = deleteCommand;
        }

        private void EditButton_Clicked(object sender, System.EventArgs e)
        {
            Popup.IsOpen = false;
            _editCommand.Invoke();
        }

        private void DeleteButton_Clicked(object sender, EventArgs e)
        {
            Popup.IsOpen = false;
            _deleteCommand.Invoke();
        }

        public void Show(View view)
        {
            Popup.ShowRelativeToView(view, RelativePosition.AlignBottom);
        }
    }
}

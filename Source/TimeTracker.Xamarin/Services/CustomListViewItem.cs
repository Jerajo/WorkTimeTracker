using Syncfusion.ListView.XForms;
using System.Windows.Input;
using TimeTracker.Xamarin.Domains.Group;
using Xamarin.Forms;

namespace TimeTracker.Xamarin.Services
{
    public class CustomListViewItem : ListViewItem
    {
        private readonly ICommand _editCommand;
        private readonly ICommand _deleteCommand;

        public CustomListViewItem(ICommand editCommand,
            ICommand deleteCommand)
        {
            _editCommand = editCommand;
            _deleteCommand = deleteCommand;
        }

        protected override void OnChildAdded(Element child)
        {
            base.OnChildAdded(child);

            if (!(child is GroupCell groupCell))
                return;

            groupCell.EditCommand = _editCommand;
            groupCell.DeleteCommand = _deleteCommand;
        }
    }
}

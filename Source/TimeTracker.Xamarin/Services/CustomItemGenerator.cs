using Syncfusion.ListView.XForms;
using System.Windows.Input;

namespace TimeTracker.Xamarin.Services
{
    public class CustomItemGenerator : ItemGenerator
    {
        private SfListView _listView;
        private ICommand _editCommand;
        private ICommand _deleteCommand;

        public CustomItemGenerator(SfListView listView,
            ICommand editCommand,
            ICommand deleteCommand)
            : base(listView)
        {
            _listView = listView;
            _editCommand = editCommand;
            _deleteCommand = deleteCommand;
        }

        protected override ListViewItem OnCreateListViewItem(int itemIndex, ItemType type, object data = null)
        {
            if (type == ItemType.Record)
                return new CustomListViewItem(_editCommand, _deleteCommand);

            return base.OnCreateListViewItem(itemIndex, type, data);
        }
    }
}

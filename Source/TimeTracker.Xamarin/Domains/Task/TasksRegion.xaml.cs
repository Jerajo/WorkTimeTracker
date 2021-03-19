using Syncfusion.ListView.XForms;
using System;
using System.Linq;
using System.Windows.Input;
using TimeTracker.Xamarin.Domains.Group;
using TimeTracker.Xamarin.Layout.Shared;
using TimeTracker.Xamarin.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeTracker.Xamarin.Domains.Task
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TasksRegion
    {
        public TasksRegion()
        {
            InitializeComponent();

            this.SetBinding(IsEditingProperty, nameof(TasksRegionModel.IsEditing));
            this.SetBinding(AddGroupCommandProperty, nameof(TasksRegionModel.AddGroupCommand));
            this.SetBinding(EditGroupCommandProperty, nameof(TasksRegionModel.EditGroupCommand));
            this.SetBinding(DeleteGroupCommandProperty, nameof(TasksRegionModel.DeleteGroupCommand));
        }

        #region Properties

        public bool IsEditing
        {
            get => (bool)GetValue(IsEditingProperty);
            set => SetValue(IsEditingProperty, value);
        }

        public static readonly BindableProperty IsEditingProperty =
            BindableProperty.Create(nameof(IsEditing), typeof(bool),
                typeof(TasksRegion), false, BindingMode.TwoWay);

        public ICommand AddGroupCommand
        {
            get => (ICommand)GetValue(AddGroupCommandProperty);
            set => SetValue(AddGroupCommandProperty, value);
        }

        public static readonly BindableProperty AddGroupCommandProperty =
            BindableProperty.Create(nameof(AddGroupCommand), typeof(ICommand),
                typeof(TasksRegion), null, BindingMode.OneTime);

        public ICommand EditGroupCommand
        {
            get => (ICommand)GetValue(EditGroupCommandProperty);
            set => SetValue(EditGroupCommandProperty, value);
        }

        public static readonly BindableProperty EditGroupCommandProperty =
            BindableProperty.Create(nameof(EditGroupCommand), typeof(ICommand),
                typeof(TasksRegion), null, BindingMode.OneTime);

        public ICommand DeleteGroupCommand
        {
            get => (ICommand)GetValue(DeleteGroupCommandProperty);
            set => SetValue(DeleteGroupCommandProperty, value);
        }

        public static readonly BindableProperty DeleteGroupCommandProperty =
            BindableProperty.Create(nameof(DeleteGroupCommand), typeof(ICommand),
                typeof(TasksRegion), null, BindingMode.OneTime);

        #endregion

        #region Events

        protected override void OnBindingContextChanged()
        {
            if (EditGroupCommand is null || DeleteGroupCommand is null)
                return;

            if (GroupsContainer.ItemGenerator is CustomItemGenerator)
                return;

            GroupsContainer.ItemGenerator = new CustomItemGenerator(GroupsContainer,
                EditGroupCommand,
                DeleteGroupCommand);

            base.OnBindingContextChanged();
        }

        private void AddGroupButton_Clicked(object sender, EventArgs e)
        {
            IsEditing = true;
            AddGroupButton.IsVisible = false;
            var view = new LiveEditor(new GroupCellModel(), AddGroupCommand);
            view.EditionCompleted += LiveEditor_EditionCompleted;
            ViewContainer.Children.Add(view);
            view.FocusEntry();
        }

        private void LiveEditor_EditionCompleted(object sender, EventArgs e)
        {
            if (!(sender is LiveEditor view))
                return;
            IsEditing = false;
            view.StopEditing -= LiveEditor_EditionCompleted;
            ViewContainer.Children.Remove(view);
            AddGroupButton.IsVisible = true;
            GroupsContainer.RefreshListViewItem();
        }

        private void GroupsContainer_SwipeEnded(object sender, Syncfusion.ListView.XForms.SwipeEndedEventArgs e)
        {
            var cellItem = GetListViewItem(e.ItemData);

            if (e.SwipeDirection == Syncfusion.ListView.XForms.SwipeDirection.Left)
            {
                IsEditing = true;
                cellItem.ShowEditor();
                cellItem.OnEdited += GroupCell_Edited;
            }
            else if (e.SwipeDirection == Syncfusion.ListView.XForms.SwipeDirection.Right)
                DeleteGroupCommand.Execute(e.ItemData);

            GroupsContainer.ResetSwipe();
        }

        private void GroupCell_Edited(object sender, EventArgs e)
        {
            if (!(sender is GroupCell groupCell))
                return;

            groupCell.OnEdited -= GroupCell_Edited;
            IsEditing = false;
            GroupsContainer.RefreshListViewItem();
        }

        #endregion

        #region Auxiliary Methods

        private GroupCell GetListViewItem(object data)
        {
            if (!(GroupsContainer.Children[0] is ExtendedScrollView scrollView))
                return null;

            if (!(scrollView.Children[0] is VisualContainer visualContainer))
                return null;

            var result = visualContainer.Children.Where(x => x.BindingContext.Equals(data) &&
                                                             x is CustomListViewItem);
            var view = result.ToList()[0] as ContentView;

            return view?.Content as GroupCell;
        }

        #endregion
    }
}

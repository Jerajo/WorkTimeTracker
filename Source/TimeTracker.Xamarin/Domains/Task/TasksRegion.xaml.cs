using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using TimeTracker.Xamarin.Domains.Group;
using TimeTracker.Xamarin.Layout.Shared;
using Xamarin.Forms;
using Xamarin.Forms.PowerControls.Resources;
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
            this.SetBinding(ItemsSourceProperty, nameof(TasksRegionModel.Groups));
        }

        ~TasksRegion() => ClearCollection();

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

        public ObservableCollection<GroupCellModel> ItemsSource
        {
            get => (ObservableCollection<GroupCellModel>)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource),
                typeof(ObservableCollection<GroupCellModel>),
                typeof(TasksRegion),
                null,
                BindingMode.OneWay,
                propertyChanging: ItemsSource_PropertyChanging);

        #endregion

        #region Events

        private static void ItemsSource_PropertyChanging(global::Xamarin.Forms.BindableObject bindable,
            object oldValue,
            object newValue)
        {
            if (oldValue != null)
                throw Exceptions.Get.ReadOnlyProperty();

            if (!(newValue is ObservableCollection<GroupCellModel> newCollection))
                throw Exceptions.Get.InvalidCastException(newValue.GetType(),
                    typeof(ObservableCollection<GroupCellModel>));

            if (!(bindable is TasksRegion @this))
                return;

            AddToCollection(@this, newCollection);
            newCollection.CollectionChanged += @this.ItemsSource_CollectionChanged;
        }

        private void ItemsSource_CollectionChanged(object sender,
            NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
                AddToCollection(this, e.NewItems);
            else if (e.Action == NotifyCollectionChangedAction.Remove)
                RemoveFromCollection(e.OldItems);
        }

        private void GroupCell_Edited(object sender, EventArgs e)
        {
            if (!(sender is GroupCell view))
                return;
            EditGroupCommand.Execute(view.BindingContext);
        }

        private int _count = 0;
        private void AddGroupButton_Clicked(object sender, EventArgs e)
        {
            //AddGroupCommand?.Execute(new GroupCellModel("New group test", $"{_count++}"));

            IsEditing = true;
            AddGroupButton.IsVisible = false;
            var view = new LiveEditor(new GroupCellModel(), AddGroupCommand);
            //view.StopEditing += LiveEditor_StopEditing;
            ViewContainer.Children.Add(view);
            view.FocusEntry();
        }

        private void LiveEditor_StopEditing(object sender, EventArgs e)
        {
            if (!(sender is LiveEditor view))
                return;
            //AddGroupCommand?.Execute(view.BindingContext);
            IsEditing = false;
            view.StopEditing -= LiveEditor_StopEditing;
            ViewContainer.Children.Remove(view);
            AddGroupButton.IsVisible = true;
            GC.Collect();
        }

        #endregion

        #region Auxiliary Methods

        private void ClearCollection()
        {
            ItemsContainer.Children.Clear();
            ItemsSource.CollectionChanged -= ItemsSource_CollectionChanged;
            ItemsSource.Clear();
        }

        private static void AddToCollection(TasksRegion @this, IList newCollection)
        {
            foreach (var item in newCollection)
            {
                var view = new GroupCell(item, @this.DeleteGroupCommand);
                view.OnEdited += @this.GroupCell_Edited;
                if (item is GroupCellModel model && model.Index > 0)
                    @this.ItemsContainer.Children.Insert(model.Index, view);
                else
                    @this.ItemsContainer.Children.Insert(0, view);
            }
        }

        private void RemoveFromCollection(IList oldCollection)
        {
            foreach (var item in oldCollection)
            {
                if (!(ItemsContainer.Children
                    .First(x => x.BindingContext.Equals(item)) is GroupCell view))
                    continue;

                ((GroupCellModel)item).Index = ItemsContainer.Children.IndexOf(view);
                view.OnEdited -= GroupCell_Edited;
                ItemsContainer.Children.Remove(view);
            }
            GC.Collect();
        }

        #endregion
    }
}

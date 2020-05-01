using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using TimeTracker.Xamarin.Domains.Group;
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
            this.SetBinding(EditCommandProperty, nameof(TasksRegionModel.EditGroupCommand));
            this.SetBinding(DeleteCommandProperty, nameof(TasksRegionModel.DeleteGroupCommand));
            this.SetBinding(ItemsSourceProperty, nameof(TasksRegionModel.Groups));
        }

        ~TasksRegion() => ClearCollection();

        #region Properties

        public ICommand EditCommand
        {
            get => (ICommand)GetValue(EditCommandProperty);
            set => SetValue(EditCommandProperty, value);
        }

        public static readonly BindableProperty EditCommandProperty =
            BindableProperty.Create(nameof(EditCommand), typeof(ICommand),
                typeof(GroupCell), null, BindingMode.OneTime);

        public ICommand DeleteCommand
        {
            get => (ICommand)GetValue(DeleteCommandProperty);
            set => SetValue(DeleteCommandProperty, value);
        }

        public static readonly BindableProperty DeleteCommandProperty =
            BindableProperty.Create(nameof(DeleteCommand), typeof(ICommand),
                typeof(GroupCell), null, BindingMode.OneTime);

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
                BindingMode.OneTime,
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
            EditCommand.Execute(view.BindingContext);
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
                var view = new GroupCell(item, @this.DeleteCommand);
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

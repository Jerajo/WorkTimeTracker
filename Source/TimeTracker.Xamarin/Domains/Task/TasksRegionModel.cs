using DryIoc;
using Prism.Commands;
using Prism.Navigation;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TimeTracker.Application.Queries;
using TimeTracker.Core.Resources;
using TimeTracker.Xamarin.Contracts;
using TimeTracker.Xamarin.Domains.Group;
using TimeTracker.Xamarin.Layout.Notifications;
using Xamarin.Forms;

namespace TimeTracker.Xamarin.Domains.Task
{
    public class TasksRegionModel : RegionModelBase
    {
        private int _count = 0;
        private int _previousIndex = 0;

        public TasksRegionModel(IContainer container) : base(container)
        {
            Title = Messages.RegionTitleTask;

            AddGroupCommand = new DelegateCommand<GroupCellModel>(AddGroup);
            EditGroupCommand = new DelegateCommand<GroupCellModel>(EditGroup);
            DeleteGroupCommand = new DelegateCommand<GroupCellModel>(DeleteGroup);
            RevertCommand = new DelegateCommand<GroupCellModel>(RevertDeletedGroup);
        }

        #region Properties

        public bool IsEditing
        {
            get;
            set;
        }

        public bool IsProcessing { get; private set; }
        public ICommand RevertCommand { get; }

        public ICommand AddGroupCommand { get; }

        public ICommand EditGroupCommand { get; }

        public ICommand DeleteGroupCommand { get; }

        public ObservableCollection<GroupCellModel> Groups { get; private set; }

        #endregion

        #region Events

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (Groups != null)
                return;

            Device.BeginInvokeOnMainThread(async () =>
            {
                IsProcessing = true;
                var loadGroupsCommand = QueryFactory.GetInstance<GetGroupsQuery>();
                var groups = await loadGroupsCommand.ExecuteAsync(x => !x.IsDeleted).ConfigureAwait(true);

                Groups = Mapper.Map<ObservableCollection<GroupCellModel>>(groups);
                IsProcessing = false;
            });
        }

        #endregion

        #region Auxiliary Methods

        private void AddGroup(GroupCellModel model)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                IsProcessing = true;
                Groups.Insert(0, model);

                //TODO: implement persistence logic

                PushNotification(Messages.NotificationMessageSaveChanges, NotificationType.Info, 3000);
                IsProcessing = false;
            });
        }

        private void EditGroup(GroupCellModel model)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                IsProcessing = true;

                //TODO: implement persistence logic

                PushNotification(Messages.NotificationMessageSaveChanges, NotificationType.Info, 3000);
                IsProcessing = false;
            });
        }

        private void DeleteGroup(GroupCellModel model)
        {
            _previousIndex = Groups.IndexOf(model);

            Device.BeginInvokeOnMainThread(() =>
            {
                IsProcessing = true;
                Groups.Remove(model);

                PushRevertNotification(RevertCommand, model);
                //var taskScheduler = TaskScheduler.Current;
                //System.Threading.Tasks.Task.Run(() => { }).ContinueWith(task => { });
                System.Threading.Tasks.Task.Delay(5000).ConfigureAwait(true);

                if (model.ActionCanceled)
                    return;

                //TODO: implement persistence logic

                PushNotification(Messages.NotificationMessageSaveChanges, NotificationType.Info, 3000);
                IsProcessing = false;
            });
        }

        private void RevertDeletedGroup(GroupCellModel model)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                model.ActionCanceled = true;
                Groups.Insert(_previousIndex, model);
            });
        }

        #endregion
    }
}

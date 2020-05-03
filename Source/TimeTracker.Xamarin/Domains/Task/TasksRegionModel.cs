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
            LoadGroupsCommand = QueryFactory.GetInstance<GetGroupsQuery>();
        }

        #region Properties

        public bool IsEditing
        {
            get;
            set;
        }

        public ICommand RevertCommand { get; }

        public ICommand AddGroupCommand { get; }

        public ICommand EditGroupCommand { get; }

        public ICommand DeleteGroupCommand { get; }

        public GetGroupsQuery LoadGroupsCommand { get; }

        public ObservableCollection<GroupCellModel> Groups { get; private set; }

        #endregion

        #region Events

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            if (Groups != null)
                return;

            //TODO: load groups that are not deleted from db. (x => !x.IsDeleted)
            var groups = await LoadGroupsCommand.Run(x => true);
            Groups = Mapper.Map<ObservableCollection<GroupCellModel>>(groups);
            //Groups = new ObservableCollection<GroupCellModel>(Mapper.Map<List<GroupCellModel>>(groups));

        }

        #endregion

        #region Auxiliary Methods

        private void AddGroup(GroupCellModel model)
        {
            Groups.Add(model);
            //TODO: implement persistence logic
            PushNotification(Messages.NotificationMessageSaveChanges, NotificationType.Info, 3000);
        }

        private void EditGroup(GroupCellModel model)
        {
            //TODO: implement persistence logic
            PushNotification(Messages.NotificationMessageSaveChanges, NotificationType.Info, 3000);
        }

        private async void DeleteGroup(GroupCellModel model)
        {
            _previousIndex = Groups.IndexOf(model);
            Groups.Remove(model);
            model.ActionCanceled = false;
            PushRevertNotification(RevertCommand, model);
            await System.Threading.Tasks.Task.Delay(5000);
            if (model.ActionCanceled)
                return;
            //TODO: implement persistence logic
            PushNotification(Messages.NotificationMessageSaveChanges, NotificationType.Info, 3000);
        }

        private void RevertDeletedGroup(GroupCellModel model)
        {
            model.ActionCanceled = true;
            Groups.Insert(_previousIndex, model);
        }

        #endregion
    }
}

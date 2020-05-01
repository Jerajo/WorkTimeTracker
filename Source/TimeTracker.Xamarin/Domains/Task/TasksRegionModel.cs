using DryIoc;
using Prism.Commands;
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
            AddGroupCommand = new DelegateCommand(AddGroup);
            EditGroupCommand = new DelegateCommand<GroupCellModel>(EditGroup);
            DeleteGroupCommand = new DelegateCommand<GroupCellModel>(DeleteGroup);
            RevertCommand = new DelegateCommand<GroupCellModel>(RevertDeletedGroup);
            LoadGroupsCommand = QueryFactory.GetInstance<GetGroupsQuery>();

            //TODO: load groups that are not deleted from db. (x => !x.IsDeleted)
            //var groups = LoadGroupsCommand.Run(x => true);
            //Groups = new ObservableCollection<GroupCellModel>(Mapper.Map<List<GroupCellModel>>(groups));
            Groups = new ObservableCollection<GroupCellModel>
            {
                new GroupCellModel("Authentication", "2417"),
                new GroupCellModel("Authorization", "4642"),
                new GroupCellModel("Create user"),
                new GroupCellModel("Update user", "8954"),
                new GroupCellModel("Delete user")
            };
        }

        #region Properties

        public GetGroupsQuery LoadGroupsCommand { get; }
        public ICommand AddGroupCommand { get; }
        public ICommand EditGroupCommand { get; }
        public ICommand DeleteGroupCommand { get; }
        public ICommand RevertCommand { get; }
        public ObservableCollection<GroupCellModel> Groups { get; }

        #endregion

        #region Auxiliary Methods

        private void AddGroup()
        {
            Groups.Add(new GroupCellModel("Nuevo grupo", $"{++_count}"));
            //TODO: implement persistence logic
            PushNotification(Messages.NotificationMessageSaveChanges, NotificationType.Info, 3000);
        }

        private void EditGroup(GroupCellModel model)
        {
            model.Name = $"grupo editado {++_count}";
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

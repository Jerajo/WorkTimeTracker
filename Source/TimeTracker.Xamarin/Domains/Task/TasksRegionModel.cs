using DryIoc;
using Prism.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TimeTracker.Core.Resources;
using TimeTracker.Xamarin.Contracts;
using TimeTracker.Xamarin.Domains.Group;

namespace TimeTracker.Xamarin.Domains.Task
{
    public class TasksRegionModel : RegionModelBase
    {
        private int _count = 0;

        public TasksRegionModel(IContainer container) : base(container)
        {
            Title = Messages.RegionTitleTask;
            AddGroupCommand = new DelegateCommand(AddGroup);
            EditGroupCommand = new DelegateCommand<GroupCellModel>(EditGroup);
            DeleteGroupCommand = new DelegateCommand<GroupCellModel>(DeleteGroup);

            Groups = new ObservableCollection<GroupCellModel>
            {
                new GroupCellModel("Authentication", "2417"),
                new GroupCellModel("Authorization", "4642"),
                new GroupCellModel("Create user"),
                new GroupCellModel("Update user", "8954"),
                new GroupCellModel("Delete user")
            };
        }

        public ICommand AddGroupCommand { get; }
        public ICommand EditGroupCommand { get; }
        public ICommand DeleteGroupCommand { get; }
        public ObservableCollection<GroupCellModel> Groups { get; }

        private void AddGroup()
        {
            Groups.Add(new GroupCellModel("Nuevo grupo", $"{++_count}"));
        }

        private void DeleteGroup(GroupCellModel model)
        {
            Groups.Remove(model);
        }

        private void EditGroup(GroupCellModel model)
        {
            model.Name = $"grupo editado {++_count}";
        }
    }
}

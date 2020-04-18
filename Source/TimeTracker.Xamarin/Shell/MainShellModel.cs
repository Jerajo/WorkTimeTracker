using Prism.Navigation;
using System;
using System.Collections.Generic;
using TimeTracker.Application.Contracts;
using TimeTracker.Application.Queries;
using TimeTracker.Xamarin.Contracts;
using Xamarin.Forms;
using AsyncOperation = System.Threading.Tasks.Task;
using ICommandFactory = TimeTracker.Xamarin.Contracts.ICommandFactory;

namespace TimeTracker.Xamarin.Shell
{
    public class MainShellModel : ViewModelBase
    {
        public MainShellModel(INavigationService navigationService,
            ICommandFactory commandFactory,
            IQueryFactory queryFactory)
            : base(navigationService, commandFactory, queryFactory)
        {
            Title = "Main Page";

            //GetTasks = commandFactory.Make<TaskDto, UpdateTaskCommand>();
            GetTasks = queryFactory.GetInstance<GetTasksQuery>();

            var tasks = GetTasks.Run(x => true);
            ChangeColor();
        }

        public GetTasksQuery GetTasks { get; }

        public int Count { get; set; }

        public string Message { get; set; }
        public Color FontColor { get; set; }

        public List<Color> Colors => new List<Color>
        {
            Color.Red,
            Color.Blue,
            Color.Yellow,
            Color.Green,
            Color.White
        };

        private async void ChangeColor()
        {
            do
            {
                await AsyncOperation.Delay(1000);
                var mod = Count % 5;
                FontColor = Colors[Math.Abs(Count - (Count - (4 - (Count % 5))))];
                Message = $"Color ha cambiado {Count} beses.";
            } while (Count++ < 50);
        }
    }
}

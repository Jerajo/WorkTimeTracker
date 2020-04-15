using Prism.Navigation;
using System;
using System.Collections.Generic;
using TimeTracker.Xamarin.Contract;
using Xamarin.Forms;
using AsyncOperation = System.Threading.Tasks.Task;

namespace TimeTracker.Xamarin.Layout
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main Page";
            ChangeColor();
        }

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

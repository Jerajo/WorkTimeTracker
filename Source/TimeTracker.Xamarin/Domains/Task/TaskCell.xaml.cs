using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeTracker.Xamarin.Domains.Task
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskCell : ViewCell
    {
        public TaskCell()
        {
            InitializeComponent();
        }
    }
}
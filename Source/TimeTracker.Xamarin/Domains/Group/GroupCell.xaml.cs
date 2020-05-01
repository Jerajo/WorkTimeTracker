using Syncfusion.XForms.Accordion;
using System;
using System.Windows.Input;
using TimeTracker.Xamarin.Layout.Shared;
using Xamarin.Forms;
using Xamarin.Forms.PowerControls.Resources.Fonts;
using Xamarin.Forms.Xaml;

namespace TimeTracker.Xamarin.Domains.Group
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroupCell
    {
        //private readonly List<(Point, Point)> _actionsPositions;

        public GroupCell() : this(null, null) {}
        public GroupCell(object bindingContext, ICommand deleteCommand)
        {
            InitializeComponent();

            BindingContext = bindingContext;
            DeleteCommand = deleteCommand;

            //_actionsPositions = new List<(Point, Point)>();
            //var touchEffect = new TouchEffect();
            //touchEffect.TouchAction += Container_TouchTracking;
            //this.Effects.Add(touchEffect);
        }

        #region Properties

        public ICommand DeleteCommand { get; }

        #endregion

        #region Events

        public event EventHandler OnEdited;

        private async void ToggleAccordionButton_Tapped(object sender, System.EventArgs e)
        {
            AccordionItem.IsExpanded = !AccordionItem.IsExpanded;
            ToggleAccordionIcon.Text = AccordionItem.IsExpanded ?
                FontAwesomeSolid.AngleDown :
                FontAwesomeSolid.AngleUp;

            await System.Threading.Tasks.Task.Delay((int)Accordion.AnimationDuration);
            Accordion.BringIntoView(AccordionItem);
        }

        private void SfAccordion_Expanding(object sender, ExpandingAndCollapsingEventArgs e)
            => e.Cancel = true;

        private void SfAccordion_Collapsing(object sender, ExpandingAndCollapsingEventArgs e)
            => e.Cancel = true;

        private void ActionsMenuButton_Tapped(object sender, EventArgs e)
        {
            var popupActions = new PopupActions(OnEditSwipe_Invoked, OnDeleteSwipe_Invoked);
            popupActions.Show(sender as View);
        }

        private void OnDeleteSwipe_Invoked(object sender, EventArgs e)
        {
            DeleteCommand.Execute(BindingContext);
        }

        private void OnEditSwipe_Invoked(object sender, EventArgs e)
        {
            HeaderContent.IsVisible = false;
            var view = new LiveEditor(BindingContext);
            view.StopEditing += LiveEditor_StopEditing;
            SwipeViewContainer.Children.Add(view);
            view.FocusEntry();
        }

        private void LiveEditor_StopEditing(object sender, EventArgs e)
        {
            if (!(sender is LiveEditor view))
                return;
            OnEdited?.Invoke(this, EventArgs.Empty);
            view.StopEditing -= LiveEditor_StopEditing;
            SwipeViewContainer.Children.Remove(view);
            HeaderContent.IsVisible = true;
            GC.Collect();
        }

        #endregion

        //protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        //{
        //    var measure = base.OnMeasure(widthConstraint, heightConstraint);
        //    if (_actionsPositions.Any())
        //        return measure;

        //    _actionsPositions.Add(GetViewDimensions(ToggleAccordionButton));
        //    return measure;
        //}

        //private void Container_TouchTracking(object sender, TouchActionEventArgs e)
        //{
        //    var touchPoint = new Point(e.Location.X, e.Location.Y);
        //_canToggle = _actionsPositions.Any(a => (a.Item1.X < touchPoint.X) &&
        //                                        (a.Item2.X > touchPoint.X) &&
        //                                        (a.Item1.Y < touchPoint.Y) &&
        //                                        (a.Item2.Y > touchPoint.Y));
        //}

        //private (Point, Point) GetViewDimensions(View view) =>
        //    (new Point(view.X, view.Y), new Point(view.X + view.Width, view.Y + view.Height));
    }
}

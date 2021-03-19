using System;
using System.Collections;
using System.Linq;
using Xamarin.Forms.PowerControls.Buttons;
using Xamarin.Forms.PowerControls.Resources;
using Xamarin.Forms.Xaml;

namespace Xamarin.Forms.PowerControls.Menus
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavigationMenu
    {
        private readonly TapGestureRecognizer _tapGestureRecognizer;

        public NavigationMenu()
        {
            InitializeComponent();
            _tapGestureRecognizer = new TapGestureRecognizer();
            _tapGestureRecognizer.Tapped += OnItemSelected;
        }

        ~NavigationMenu() => ClearCollection();

        #region Properties

        public bool IsLastItemFooter
        {
            get => (bool)GetValue(IsLastItemFooterProperty);
            set => SetValue(IsLastItemFooterProperty, value);
        }

        public static readonly BindableProperty IsLastItemFooterProperty =
            BindableProperty.Create(nameof(IsLastItemFooter),
                typeof(bool),
                typeof(NavigationMenu),
                true);

        public View ItemTemplate
        {
            get => (View)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create(nameof(ItemTemplate),
                typeof(View),
                typeof(NavigationMenu),
                new NavigationButton());

        public View FooterTemplate
        {
            get => (View)GetValue(FooterTemplateProperty);
            set => SetValue(FooterTemplateProperty, value);
        }

        public static readonly BindableProperty FooterTemplateProperty =
            BindableProperty.Create(nameof(FooterTemplate),
                typeof(View),
                typeof(NavigationMenu),
                new NavigationButton());

        public object SelectedItem
        {
            get => (object)GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(NavigationMenu));

        public object ItemsSource
        {
            get => GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource),
                typeof(object),
                typeof(NavigationMenu),
                null,
                BindingMode.OneTime,
                propertyChanging: ItemsSource_PropertyChanging);

        #endregion

        #region Events

        private static void ItemsSource_PropertyChanging(Forms.BindableObject bindable,
            object oldValue,
            object newValue)
        {
            if (oldValue != null)
                throw Exceptions.Get.ReadOnlyProperty();

            if (!(newValue is IList newCollection))
                throw Exceptions.Get.InvalidCastException(newValue.GetType(), typeof(IList));

            if (!(bindable is NavigationMenu @this))
                return;

            if (@this.IsLastItemFooter)
                SetFooter(@this, newCollection);

            var itemTemplateType = @this.ItemTemplate.GetType();
            UpdateCollection(@this,
                newCollection,
                itemTemplateType);

            @this.SelectedItem = newCollection[0];
        }

        #endregion

        #region Auxiliary Methods

        private void ClearCollection()
        {
            foreach (var item in ItemsContainer.Children)
            {
                if (item is Button button)
                    button.Clicked -= OnItemSelected;
                else if (item is View view)
                    view.GestureRecognizers.Remove(_tapGestureRecognizer);
            }
            ItemsContainer.Children.Clear();
            ((IList)ItemsSource)?.Clear();
        }

        private static void UpdateCollection(NavigationMenu @this,
            IList newCollection,
            Type itemTemplateType)
        {
            foreach (var item in newCollection)
            {
                if (@this.IsLastItemFooter && item.Equals(newCollection[newCollection.Count - 1]))
                    break;

                var view = (View)Activator.CreateInstance(itemTemplateType);

                if (view is Button button)
                    button.Clicked += @this.OnItemSelected;
                else
                    view.GestureRecognizers.Add(@this._tapGestureRecognizer);

                if (@this.ItemsContainer.Children.Count == 0)
                    view.IsEnabled = false;
                view.BindingContext = item;
                @this.ItemsContainer.Children.Add(view);
            }
        }

        private static void SetFooter(NavigationMenu @this, IList newCollection)
        {
            var footer = @this.FooterTemplate;

            if (footer is Button button)
                button.Clicked += @this.OnItemSelected;
            else
                footer.GestureRecognizers.Add(@this._tapGestureRecognizer);

            footer.BindingContext = newCollection[newCollection.Count - 1];
            @this.FooterItem.Content = footer;
        }

        #endregion

        #region Virtual Methods

        protected virtual void OnItemSelected(object sender, EventArgs e)
        {
            if (!(sender is View view))
                return;

            var previousTab = GetPreviousTab();
            if (previousTab != null)
                previousTab.IsEnabled = true;

            view.IsEnabled = false;
            SelectedItem = view.BindingContext;
        }

        protected virtual View GetPreviousTab()
        {
            if (SelectedItem is null)
                return null;

            var view = ItemsContainer.Children.FirstOrDefault(
                x => x.BindingContext.Equals(SelectedItem));

            return view ?? FooterItem.Content;
        }

        #endregion
    }
}

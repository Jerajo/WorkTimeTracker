using System.Runtime.CompilerServices;
using Xamarin.Forms.Xaml;

namespace Xamarin.Forms.PowerControls.Buttons
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavigationButton
    {
        protected Style NormalStyle { get; }

        public NavigationButton()
        {
            InitializeComponent();
            NormalStyle = CreateNormalStyle();
            if (Style is null)
                Style = NormalStyle;
        }

        #region Properties

        public Style SelectedStyle
        {
            get => (Style)GetValue(SelectedStyleProperty);
            set => SetValue(SelectedStyleProperty, value);
        }

        public static readonly BindableProperty SelectedStyleProperty =
            BindableProperty.Create(nameof(SelectedStyle), typeof(Style), typeof(NavigationButton), defaultValueCreator: CreateSelectedStyle);

        #endregion

        #region Auxiliary Methods

        private static object CreateSelectedStyle(Forms.BindableObject bindable) =>
            new Style(typeof(NavigationButton))
            {
                Setters =
                {
                    new Setter{ Property = TextColorProperty, Value = Color.White },
                    new Setter{ Property = BackgroundColorProperty, Value = Color.DeepSkyBlue }
                }
            };

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(IsEnabled))
                Style = IsEnabled ? NormalStyle : SelectedStyle;
        }

        private Style CreateNormalStyle() =>
            new Style(typeof(NavigationButton))
            {
                Setters =
                {
                    new Setter{ Property = BackgroundColorProperty, Value = Color.Transparent }
                }
            };

        #endregion
    }
}

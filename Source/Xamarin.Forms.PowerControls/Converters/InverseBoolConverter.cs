using System;
using System.Globalization;
using Xamarin.Forms.Xaml;

namespace Xamarin.Forms.PowerControls.Converters
{
    public class InverseBoolConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => !(bool)value;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => !(bool)value;

        public object ProvideValue(IServiceProvider serviceProvider) => this;
    }
}

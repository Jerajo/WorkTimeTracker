using System;
using System.Globalization;
using Xamarin.Forms.Xaml;

namespace Xamarin.Forms.PowerControls.Converters
{
    public class NullToBoolConverter : IValueConverter, IMarkupExtension
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => !(value is null);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => parameter;

        public object ProvideValue(IServiceProvider serviceProvider) => this;
    }
}

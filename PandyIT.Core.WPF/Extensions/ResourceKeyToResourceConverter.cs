using System;
using System.Windows;
using System.Windows.Data;

namespace PandyIT.Core.WPF.Extensions
{
    public class ResourceKeyToResourceConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values.Length < 2)
            {
                return null;
            }

            var element = values[0] as FrameworkElement;
            var resourceKey = values[1];
            if (ResourceKeyConverter != null)
            {
                resourceKey = ResourceKeyConverter.Convert(resourceKey, targetType, ConverterParameter, culture);
            }
            else if (StringFormat != null && resourceKey is string)
            {
                resourceKey = string.Format(StringFormat, resourceKey);
            }

            var resource = element.TryFindResource(resourceKey);

            return resource;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public IValueConverter ResourceKeyConverter { get; set; }

        public object ConverterParameter { get; set; }

        public string StringFormat { get; set; }
    }
}

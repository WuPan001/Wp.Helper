using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Wp.Helpers.Converters.ControlVisibility
{
    /// <summary>
    /// 返回顶端按钮显示状态转换器
    /// </summary>
    public class ToTopperVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
            {
                return null;
            }
            else
            {
                return (double)value != 0d ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
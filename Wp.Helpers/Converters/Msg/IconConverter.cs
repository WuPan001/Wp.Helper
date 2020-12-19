using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Wp.Helpers.Enums;

namespace Wp.Helpers.Converters.Msg
{
    /// <summary>
    /// 消息弹窗Icon转换器
    /// </summary>
    public class IconConverter : IValueConverter
    {
        public Style OffLine { get; set; }
        public Style Disconnection { get; set; }
        public Style Trace { get; set; }
        public Style Alarm { get; set; }
        public Style Forbid { get; set; }
        public Style Question { get; set; }
        public Style Error { get; set; }
        public Style Info { get; set; }
        public Style Warn { get; set; }
        public Style Success { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
            {
                return Info;
            }
            else
            {
                switch ((EMsgType)value)
                {
                    case EMsgType.OffLine:
                        return OffLine;

                    case EMsgType.Disconnection:
                        return Disconnection;

                    case EMsgType.Trace:
                        return Trace;

                    case EMsgType.Alarm:
                        return Alarm;

                    case EMsgType.Question:
                        return Question;

                    case EMsgType.Error:
                        return Error;

                    case EMsgType.Info:
                        return Info;

                    case EMsgType.Warn:
                        return Warn;

                    case EMsgType.Success:
                        return Success;

                    case EMsgType.Forbid:
                        return Forbid;

                    default:

                        return Info;
                }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
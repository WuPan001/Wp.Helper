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
        /// <summary>
        /// 离线
        /// </summary>
        public Style OffLine { get; set; }

        /// <summary>
        /// 链接断开
        /// </summary>
        public Style Disconnection { get; set; }

        /// <summary>
        /// 跟踪
        /// </summary>
        public Style Trace { get; set; }

        /// <summary>
        /// 警告
        /// </summary>
        public Style Alarm { get; set; }

        /// <summary>
        /// 禁止
        /// </summary>
        public Style Forbid { get; set; }

        /// <summary>
        /// 提问
        /// </summary>
        public Style Question { get; set; }

        /// <summary>
        /// 异常
        /// </summary>
        public Style Error { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        public Style Info { get; set; }

        /// <summary>
        /// 警告
        /// </summary>
        public Style Warn { get; set; }

        /// <summary>
        /// 成功
        /// </summary>
        public Style Success { get; set; }

        /// <summary>
        /// Converter
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
            {
                return Info;
            }
            else
            {
                return ((EMsgType)value) switch
                {
                    EMsgType.OffLine => OffLine,
                    EMsgType.Disconnection => Disconnection,
                    EMsgType.Trace => Trace,
                    EMsgType.Alarm => Alarm,
                    EMsgType.Question => Question,
                    EMsgType.Error => Error,
                    EMsgType.Info => Info,
                    EMsgType.Warn => Warn,
                    EMsgType.Success => Success,
                    EMsgType.Forbid => Forbid,
                    _ => Info,
                };
            }
        }

        /// <summary>
        /// ConverterBack
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
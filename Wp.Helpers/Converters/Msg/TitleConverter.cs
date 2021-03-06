﻿using System;
using System.Globalization;
using System.Windows.Data;
using Wp.Helpers.Enums;
using Wp.Helpers.Helpers;

namespace Wp.Helpers.Converters.Msg
{
    /// <summary>
    /// 消息弹窗标题转换器
    /// </summary>
    public class TitleConverter : IValueConverter
    {
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
                return null;
            }
            else
            {
                return EnumHelper.GetEnumDescription((EMsgType)value);
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
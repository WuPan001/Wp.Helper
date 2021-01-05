using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wp.Helpers.Enums.Protocols.ModbusTcp
{
    /// <summary>
    /// ModbusTcp异常码
    /// </summary>
    public enum EErrorCode
    {
        /// <summary>
        /// 不支持的功能码
        /// </summary>
        [Description("不支持的功能码")]
        E1,

        /// <summary>
        /// 输入数量不在许可范围
        /// 0x0001≤输出数量≤0x07D0
        /// </summary>
        [Description("输入数量不在许可范围")]
        E2,

        /// <summary>
        /// 起始地址或起始地址+输出数量错误
        /// </summary>
        [Description("起始地址或起始地址+输出数量错误")]
        E3,

        /// <summary>
        /// 读取离散输出错误
        /// </summary>
        [Description("读取离散输出错误")]
        E4,
    }
}
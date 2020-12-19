using System.ComponentModel;

namespace Wp.Helpers.Enums
{
    /// <summary>
    /// 消息类型枚举
    /// </summary>
    public enum EMsgType
    {
        /// <summary>
        /// 跟踪消息
        /// </summary>
        [Description("跟踪消息")]
        Trace,

        /// <summary>
        /// 警告提示
        /// </summary>
        [Description("警告信息")]
        Alarm,

        /// <summary>
        /// 禁止提示
        /// </summary>
        [Description("警告信息")]
        Forbid,

        /// <summary>
        /// 提示消息
        /// </summary>
        [Description("提示消息")]
        Info,

        /// <summary>
        /// 警告消息
        /// </summary>
        [Description("警告消息")]
        Warn,

        /// <summary>
        /// 错误提示
        /// </summary>
        [Description("错误提示")]
        Error,

        /// <summary>
        /// 确认消息
        /// </summary>
        [Description("确认消息")]
        Question,

        /// <summary>
        /// 成功提示
        /// </summary>
        [Description("提示信息")]
        Success,

        /// <summary>
        /// 未连接到设备
        /// </summary>
        [Description("提示信息")]
        Disconnection,

        /// <summary>
        /// 未连接到网络
        /// </summary>
        [Description("提示信息")]
        OffLine,
    }
}
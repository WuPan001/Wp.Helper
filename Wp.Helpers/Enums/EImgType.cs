using System.ComponentModel;

namespace Wp.Helpers.Enums
{
    /// <summary>
    /// 图片类型
    /// </summary>
    public enum EImgType
    {
        /// <summary>
        /// png格式的图片
        /// </summary>
        [Description(".png")]
        PNG,

        /// <summary>
        /// jpg格式的图片
        /// </summary>
        [Description(".jpg")]
        JPEG,

        /// <summary>
        /// svg格式的图片
        /// </summary>
        [Description(".svg")]
        SVG,
    }
}
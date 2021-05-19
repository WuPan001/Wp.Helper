namespace Wp.Helpers.Entities.WpfStyle
{
    /// <summary>
    /// 样式文件基类信息
    /// </summary>
    public class StyleBase
    {
        /// <summary>
        /// 样式基类名
        /// </summary>
        public string BaseName { get; set; } = string.Empty;

        /// <summary>
        /// 样式基类文件路径
        /// </summary>
        public string BasePath { get; set; } = string.Empty;
    }
}
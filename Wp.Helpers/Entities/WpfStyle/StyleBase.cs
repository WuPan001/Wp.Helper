using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
        public string BaseName { get; set; }

        /// <summary>
        /// 样式基类文件路径
        /// </summary>
        public string BasePath { get; set; }

        public bool TestBool { get; set; }
    }
}
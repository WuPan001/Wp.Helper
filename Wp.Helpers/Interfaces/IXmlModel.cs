using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wp.Helpers.Interfaces
{
    /// <summary>
    /// xml帮助类对象接口
    /// </summary>
    public interface IXmlModel
    {
        /// <summary>
        /// 子节点
        /// </summary>
        public ICollection<IXmlModel> Children { get; set; }
    }
}
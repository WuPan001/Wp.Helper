using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wp.Helpers.Enums
{
    /// <summary>
    /// 大端小端
    /// </summary>
    public enum EEndian
    {
        /// <summary>
        /// 大端
        /// 低地址存放高位
        /// </summary>
        BigEndian,

        /// <summary>
        /// 小端
        /// 高地址存放低位
        /// </summary>
        LittleEndian,
    }
}
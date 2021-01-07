using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wp.Helpers.Enums
{
    /// <summary>
    /// 进制枚举
    /// </summary>
    public enum EScale
    {
        /// <summary>
        /// 转为10进制
        /// </summary>
        Decimal,

        /// <summary>
        /// 转为16进制
        /// 如10转为a
        /// </summary>
        Hex,

        /// <summary>
        /// 转为带标记的16进制
        /// 如10转为0x0a
        /// </summary>
        HexWithToken,

        /// <summary>
        /// 转为16进制
        /// 如10转为A
        /// </summary>
        HexToUp,

        /// <summary>
        /// 转为带标记的16进制
        /// 如10转为0x0A
        /// </summary>
        HexWithTokenToUp,
    }
}
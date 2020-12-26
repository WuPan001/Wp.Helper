using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wp.Helpers.Entities.Protocols.ModbusTcp
{
    /// <summary>
    /// 报文后续字节长度域
    /// </summary>
    public class SubsequenByteLengthDomain
    {
        /// <summary>
        /// 值
        /// </summary>
        public byte[] Values { get; set; } = new byte[2];

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="value"></param>
        public SubsequenByteLengthDomain(ushort value)
        {
            Values[0] = (byte)(value & 0x0F);
            Values[1] = (byte)(value & 0xF0);
        }
    }
}
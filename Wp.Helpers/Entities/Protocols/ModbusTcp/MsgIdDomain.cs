using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wp.Helpers.Entities.Protocols.ModbusTcp
{
    /// <summary>
    /// 消息号域
    /// 报文消息号，从站返回值域主站询问报文消息号相同
    /// </summary>
    public class MsgIdDomain
    {
        /// <summary>
        /// 值
        /// </summary>
        public byte[] Values { get; set; } = new byte[2];

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="value"></param>
        public MsgIdDomain(ushort value = 0x00)
        {
            Values[0] = (byte)(value & 0x0F);
            Values[1] = (byte)(value & 0xF0);
        }
    }
}
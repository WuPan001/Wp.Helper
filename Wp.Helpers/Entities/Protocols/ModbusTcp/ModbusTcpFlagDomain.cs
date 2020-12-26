using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wp.Helpers.Entities.Protocols.ModbusTcp
{
    /// <summary>
    /// Modbus标识域
    /// 强制为0
    /// </summary>
    public class ModbusTcpFlagDomain
    {
        /// <summary>
        /// 值
        /// </summary>
        public byte Value { get { return 0x00; } }
    }
}
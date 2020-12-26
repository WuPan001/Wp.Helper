using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wp.Helpers.Entities.Protocols.ModbusTcp
{
    /// <summary>
    /// 站号域
    /// 按需指定
    /// </summary>
    public class StationIdDomain
    {
        /// <summary>
        /// 值
        /// </summary>
        public byte Value { get; set; }

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="stationId"></param>
        public StationIdDomain(byte stationId = 0x00)
        {
            Value = stationId;
        }
    }
}
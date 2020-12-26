using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wp.Helpers.Enums.Protocols.ModbusTcp
{
    /// <summary>
    /// ModbusTcp协议功能码
    /// </summary>
    public enum EFunctionCode
    {
        /// <summary>
        /// 读线圈
        /// </summary>
        ReadCoil = 0x01,

        /// <summary>
        /// 读离散量输入
        /// </summary>
        ReadDiscreteInput,

        /// <summary>
        /// 读保持寄存器
        /// </summary>
        ReadHoldingRegister,

        /// <summary>
        /// 读输入寄存器
        /// </summary>
        ReadInputRegister,

        /// <summary>
        /// 写单个线圈
        /// </summary>
        WriteSingleCoil,

        /// <summary>
        /// 写单个寄存器
        /// </summary>
        WriteSingleRegister,

        /// <summary>
        /// 写多个线圈
        /// </summary>
        WriteMultipleCoils = 0x0F,

        /// <summary>
        /// 写多个寄存器
        /// </summary>
        WriteMultipleRegisters,

        /// <summary>
        /// 读文件记录
        /// </summary>
        ReadFileLog = 0x14,

        /// <summary>
        /// 写文件记录
        /// </summary>
        WriteFileLog,

        /// <summary>
        /// 屏蔽写寄存器
        /// </summary>
        MaskWriteRegister,

        /// <summary>
        /// 读写多个寄存器
        /// </summary>
        ReadAndWriteMultipleRegisters,

        /// <summary>
        /// 读设备标识码
        /// </summary>
        ReadDeviceIdentificationCode = 0x2B,
    }
}
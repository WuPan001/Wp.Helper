using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wp.Helpers.Enums.Protocols.ModbusTcp;

namespace Wp.Helpers.Helpers.ProtocolsHelper
{
    /// <summary>
    /// ModbusTcp帮助类
    /// 注意：寄存器中高低字节与计算机内存中的高低字节相反
    /// </summary>
    public class ModbusTcpHelper
    {
        #region 事件

        //

        #endregion 事件

        #region 属性、字段

        //

        #endregion 属性、字段

        #region 方法

        /// <summary>
        /// 读线圈
        /// </summary>
        /// <param name="address">起始地址</param>
        /// <param name="count">数量</param>
        /// <param name="msgId">消息号</param>
        /// <param name="stationId">站号</param>
        /// <returns></returns>
        public static byte[] ReadCoil(ushort address, ushort count, ushort msgId = 0, byte stationId = 0)
        {
            byte[] res = new byte[12];
            Buffer.BlockCopy(BitConverter.GetBytes(msgId).Reverse().ToArray(), 0, res, 0, 2);
            res[2] = 0x00;//2,3 ModbusTcp标识，强制为0
            res[3] = 0x00;
            res[4] = 0x00;//4,5后续字节长度,高字节在前,低字节在后
            res[5] = 0x06;
            res[6] = stationId;//站号
            res[7] = (byte)EFunctionCode.ReadCoil;
            Buffer.BlockCopy(BitConverter.GetBytes(address).Reverse().ToArray(), 0, res, 8, 2);//8,9起始线圈地址
            Buffer.BlockCopy(BitConverter.GetBytes(count).Reverse().ToArray(), 0, res, 10, 2);//10,11线圈数量
            return res;
        }

        /// <summary>
        /// 读离散量输入
        /// </summary>
        /// <returns></returns>
        //public static byte[] ReadDiscreteInput()
        //{
        //    return new byte[1];
        //}

        /// <summary>
        /// 读保持寄存器
        /// ok
        /// </summary>
        /// <param name="address">寄存器地址</param>
        /// <param name="count">寄存器数量</param>
        /// <param name="msgId">消息号</param>
        /// <param name="stationId">站号</param>
        /// <returns></returns>
        public static byte[] ReadHoldingRegister(ushort address, ushort count, ushort msgId = 0, byte stationId = 0)
        {
            byte[] res = new byte[12];
            Buffer.BlockCopy(BitConverter.GetBytes(msgId).Reverse().ToArray(), 0, res, 0, 2);
            res[2] = 0x00;//2,3 ModbusTcp标识，强制为0
            res[3] = 0x00;
            res[4] = 0x00;//4,5后续字节长度,高字节在前,低字节在后
            res[5] = 0x06;
            res[6] = stationId;//单元标识符
            res[7] = (byte)EFunctionCode.ReadHoldingRegister;
            Buffer.BlockCopy(BitConverter.GetBytes(address).Reverse().ToArray(), 0, res, 8, 2);//8,9寄存器地址
            Buffer.BlockCopy(BitConverter.GetBytes(count).Reverse().ToArray(), 0, res, 10, 2);//10,11寄存器数量
            return res;
        }

        /// <summary>
        /// 读输入寄存器
        /// ok
        /// </summary>
        /// <param name="address">寄存器地址</param>
        /// <param name="count">寄存器数量</param>
        /// <param name="msgId">消息号</param>
        /// <param name="stationId">站号</param>
        /// <returns></returns>
        public static byte[] ReadInputRegister(ushort address, ushort count, ushort msgId = 0, byte stationId = 0)
        {
            byte[] res = new byte[12];
            Buffer.BlockCopy(BitConverter.GetBytes(msgId).Reverse().ToArray(), 0, res, 0, 2);
            res[2] = 0x00;//2,3 ModbusTcp标识，强制为0
            res[3] = 0x00;
            res[4] = 0x00;
            res[5] = 0x06;//4,5后续字节长度,高字节在前,低字节在后
            res[6] = stationId;//单元标识符
            res[7] = (byte)EFunctionCode.ReadInputRegister;
            Buffer.BlockCopy(BitConverter.GetBytes(address).Reverse().ToArray(), 0, res, 8, 2);//8,9起始寄存器地址
            Buffer.BlockCopy(BitConverter.GetBytes(count).Reverse().ToArray(), 0, res, 10, 2);//10,11寄存器数量
            return res;
        }

        /// <summary>
        /// 写单个线圈
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="value">值，值为true，表示请求输出为 ON；值为false，表示请求输出为 OFF</param>
        /// <param name="msgId">消息号</param>
        /// <param name="stationId">站号</param>
        /// <returns></returns>
        public static byte[] WriteSingleCoil(ushort address, bool value, ushort msgId = 0, byte stationId = 0)
        {
            byte[] res = new byte[12];
            Buffer.BlockCopy(BitConverter.GetBytes(msgId).Reverse().ToArray(), 0, res, 0, 2);
            res[2] = 0x00;//2,3 ModbusTcp标识，强制为0
            res[3] = 0x00;
            res[4] = 0x00;//4,5后续字节长度,高字节在前,低字节在后
            res[5] = 0x06;
            res[6] = stationId;//站号
            res[7] = (byte)EFunctionCode.WriteSingleCoil;
            Buffer.BlockCopy(BitConverter.GetBytes(address).Reverse().ToArray(), 0, res, 8, 2);//8,9数据起始地址,高字节在前,低字节在后
            res[10] = (byte)(value ? 0xFF : 0x00);//10,11数据长度,高字节在前,低字节在后 十六进制值 FF 00 请求输出为 ON。十六进制值00 00 请求输出为 OFF。其它所有值均是非法的，并且对输出不起作用。
            res[11] = 0x00;
            return res;
        }

        /// <summary>
        /// 写单个寄存器
        /// ok
        /// </summary>
        /// <param name="address">寄存器地址</param>
        /// <param name="value">寄存器值</param>
        /// <param name="msgId">消息号</param>
        /// <param name="stationId">站号</param>
        /// <returns></returns>
        public static byte[] WriteSingleRegister(ushort address, ushort value, ushort msgId = 0, byte stationId = 0)
        {
            byte[] res = new byte[12];
            Buffer.BlockCopy(BitConverter.GetBytes(msgId).Reverse().ToArray(), 0, res, 0, 2);
            res[2] = 0x00;//2,3 ModbusTcp标识，强制为0
            res[3] = 0x00;
            res[4] = 0x00;//4,5后续字节长度,高字节在前,低字节在后
            res[5] = 0x06;
            res[6] = stationId;//单元标识符
            res[7] = (byte)EFunctionCode.WriteSingleRegister;
            Buffer.BlockCopy(BitConverter.GetBytes(address).Reverse().ToArray(), 0, res, 8, 2);//8,9寄存器地址,高字节在前,低字节在后
            Buffer.BlockCopy(BitConverter.GetBytes(value).Reverse().ToArray(), 0, res, 10, 2);//10,11寄存器值,高字节在前,低字节在后
            return res;
        }

        /// <summary>
        /// 写多个线圈
        /// </summary>
        public static byte[] WriteMultipleCoils(ushort address, ushort count, bool value, ushort msgId = 0, byte stationId = 0)
        {
            byte[] res = new byte[14];
            Buffer.BlockCopy(BitConverter.GetBytes(msgId).Reverse().ToArray(), 0, res, 0, 2);
            res[2] = 0x00;//2,3 ModbusTcp标识，强制为0
            res[3] = 0x00;
            res[4] = 0x00;
            res[5] = 0x08;//4,5后续字节长度,高字节在前,低字节在后
            res[6] = stationId;//站号
            res[7] = (byte)EFunctionCode.WriteMultipleCoils;
            Buffer.BlockCopy(BitConverter.GetBytes(address).Reverse().ToArray(), 0, res, 8, 2);//8,9数据起始地址,高字节在前,低字节在后
            Buffer.BlockCopy(BitConverter.GetBytes(address).Reverse().ToArray(), 0, res, 8, 2);//8,9数据起始地址,高字节在前,低字节在后

            res[10] = 0x00;
            res[11] = 0x04;//10,11数据长度,高字节在前,低字节在后
            res[12] = 0x01;
            res[13] = 0x0A;//8,9数据起始地址,高字节在前,低字节在后
            return res;
        }

        /// <summary>
        /// 写多个寄存器
        /// ok
        /// </summary>
        public static byte[] WriteMultipleRegisters(ushort address, ushort count, ushort[] values, ushort msgId = 0, byte stationId = 0)
        {
            byte[] res = new byte[13 + 2 * count];
            Buffer.BlockCopy(BitConverter.GetBytes(msgId).Reverse().ToArray(), 0, res, 0, 2);
            res[2] = 0x00;//2,3 ModbusTcp标识，强制为0
            res[3] = 0x00;
            Buffer.BlockCopy(BitConverter.GetBytes((ushort)(7 + 2 * count)).Reverse().ToArray(), 0, res, 4, 2);//4,5后续字节长度
            res[6] = stationId;//站号
            res[7] = (byte)EFunctionCode.WriteMultipleRegisters;
            Buffer.BlockCopy(BitConverter.GetBytes(address).Reverse().ToArray(), 0, res, 8, 2);//8,9寄存器起始地址
            Buffer.BlockCopy(BitConverter.GetBytes(count).Reverse().ToArray(), 0, res, 10, 2);//10,11寄存器数量
            res[12] = (byte)(2 * count);
            for (int i = 0; i < count; i++)
            {
                Buffer.BlockCopy(BitConverter.GetBytes(values[i]).Reverse().ToArray(), 0, res, 13 + 2 * i, 2);//寄存器值
            }
            return res;
        }

        /// <summary>
        /// 读文件记录
        /// </summary>
        //public static byte[] ReadFileLog()
        //{
        //}

        /// <summary>
        /// 写文件记录
        /// </summary>
        //public static byte[] WriteFileLog()
        //{
        //}

        /// <summary>
        /// 屏蔽写寄存器
        /// </summary>
        //public static byte[] MaskWriteRegister()
        //{
        //}

        /// <summary>
        /// 读写多个寄存器
        /// </summary>
        //public static byte[] ReadAndWriteMultipleRegisters()
        //{
        //}

        /// <summary>
        /// 读设备标识码
        /// </summary>
        //public static byte[] ReadDeviceIdentificationCode()
        //{
        //}

        #endregion 方法
    }
}
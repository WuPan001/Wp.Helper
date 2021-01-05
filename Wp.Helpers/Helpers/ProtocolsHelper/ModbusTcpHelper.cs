using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;
using Wp.Helpers.Entities;
using Wp.Helpers.Enums.Protocols.ModbusTcp;
using Wp.Helpers.ExtensionMethod;

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

        #region Send

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
        public static byte[] WriteMultipleCoils(ushort address, bool[] values, ushort msgId = 0, byte stationId = 0)
        {
            var temp = values.ToByteArray();
            byte[] res = new byte[13 + temp.Length];
            Buffer.BlockCopy(BitConverter.GetBytes(msgId).Reverse().ToArray(), 0, res, 0, 2);
            res[2] = 0x00;//2,3 ModbusTcp标识，强制为0
            res[3] = 0x00;
            Buffer.BlockCopy(BitConverter.GetBytes((ushort)(7 + temp.Length)).Reverse().ToArray(), 0, res, 4, 2);//4,5后续字节长度
            res[6] = stationId;//站号
            res[7] = (byte)EFunctionCode.WriteMultipleCoils;
            Buffer.BlockCopy(BitConverter.GetBytes(address).Reverse().ToArray(), 0, res, 8, 2);//8,9起始地址
            Buffer.BlockCopy(BitConverter.GetBytes((ushort)values.Length).Reverse().ToArray(), 0, res, 10, 2);//10,11输出数量
            res[12] = (byte)temp.Length;//后续字节数
            Buffer.BlockCopy(temp, 0, res, 13, temp.Length);//输出值
            return res;
        }

        /// <summary>
        /// 写多个寄存器
        /// </summary>
        public static byte[] WriteMultipleRegisters(ushort address, ushort[] values, ushort msgId = 0, byte stationId = 0)
        {
            byte[] res = new byte[13 + 2 * values.Length];
            Buffer.BlockCopy(BitConverter.GetBytes(msgId).Reverse().ToArray(), 0, res, 0, 2);
            res[2] = 0x00;//2,3 ModbusTcp标识，强制为0
            res[3] = 0x00;
            Buffer.BlockCopy(BitConverter.GetBytes((ushort)(7 + 2 * values.Length)).Reverse().ToArray(), 0, res, 4, 2);//4,5后续字节长度
            res[6] = stationId;//站号
            res[7] = (byte)EFunctionCode.WriteMultipleRegisters;
            Buffer.BlockCopy(BitConverter.GetBytes(address).Reverse().ToArray(), 0, res, 8, 2);//8,9寄存器起始地址
            Buffer.BlockCopy(BitConverter.GetBytes((ushort)values.Length).Reverse().ToArray(), 0, res, 10, 2);//10,11寄存器数量
            res[12] = (byte)(2 * values.Length);
            for (int i = 0; i < values.Length; i++)
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
        public static byte[] ReadAndWriteMultipleRegisters(ushort readAddress, ushort readCount, ushort writeAddress, ushort[] values, ushort msgId = 0, byte stationId = 0)
        {
            byte[] res = new byte[17 + 2 * values.Length];
            Buffer.BlockCopy(BitConverter.GetBytes(msgId).Reverse().ToArray(), 0, res, 0, 2);
            Buffer.BlockCopy(BitConverter.GetBytes((ushort)0).Reverse().ToArray(), 0, res, 2, 2);//2,3 ModbusTcp标识，强制为0
            Buffer.BlockCopy(BitConverter.GetBytes((ushort)(11 + 2 * values.Length)).Reverse().ToArray(), 0, res, 4, 2);//4,5后续字节长度
            res[6] = stationId;//站号
            res[7] = (byte)EFunctionCode.ReadAndWriteMultipleRegisters;//功能码
            Buffer.BlockCopy(BitConverter.GetBytes(readAddress).Reverse().ToArray(), 0, res, 8, 2);//8,9读寄存器起始地址
            Buffer.BlockCopy(BitConverter.GetBytes(readCount).Reverse().ToArray(), 0, res, 10, 2);//10,11读寄存器数量
            Buffer.BlockCopy(BitConverter.GetBytes(writeAddress).Reverse().ToArray(), 0, res, 12, 2);//12,13写寄存器起始地址
            Buffer.BlockCopy(BitConverter.GetBytes((ushort)values.Length).Reverse().ToArray(), 0, res, 14, 2);//14,15写寄存器数量
            res[16] = (byte)(2 * values.Length);
            for (int i = 0; i < values.Length; i++)
            {
                Buffer.BlockCopy(BitConverter.GetBytes(values[i]).Reverse().ToArray(), 0, res, 17 + 2 * i, 2);//寄存器值
            }
            return res;
        }

        /// <summary>
        /// 读设备标识码
        /// </summary>
        //public static byte[] ReadDeviceIdentificationCode()
        //{
        //}

        #endregion Send

        #region Analysis

        /// <summary>
        /// 读线圈返回报文校验及解析
        /// </summary>
        /// <param name="res">返回结果</param>
        /// <param name="data">报文</param>
        /// <param name="msgId">消息号</param>
        /// <param name="stationId">站号</param>
        /// <returns></returns>
        public static Msg AnalysisReadCoil(ref bool[] res, byte[] data, ushort msgId = 0, byte stationId = 0)
        {
            var msg = CheckMsgHead(data, EFunctionCode.ReadCoil, msgId, stationId);
            if (msg.Success)
            {
                if (data[8] == data.Length - 9)
                {
                    res = new bool[8 * data[8]];
                    for (int i = 9; i < data.Length; i++)
                    {
                        var temp = data[i].ToBooleanArray();
                        Buffer.BlockCopy(temp, 0, res, 8 * (i - 9), temp.Length);
                    }
                    msg.Message = "报文校验成功！";
                    msg.Success = true;
                }
                else
                {
                    msg.Message = "数据域.后续字节长度校验失败！";
                    msg.Success = false;
                }
            }
            return msg;
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
        /// 读保持寄存器返回报文校验及解析
        /// </summary>
        /// <param name="res">返回结果</param>
        /// <param name="data">报文</param>
        /// <param name="msgId">消息号</param>
        /// <param name="stationId">站号</param>
        /// <returns></returns>
        public static Msg AnalysisReadHoldingRegister(ref ushort[] res, byte[] data, ushort msgId = 0, byte stationId = 0)
        {
            return AnalysisReadRegister(ref res, data, EFunctionCode.ReadHoldingRegister, msgId, stationId);
        }

        /// <summary>
        /// 读保持寄存器返回报文校验及解析
        /// </summary>
        /// <param name="res">返回结果</param>
        /// <param name="data">报文</param>
        /// <param name="msgId">消息号</param>
        /// <param name="stationId">站号</param>
        /// <returns></returns>
        public static Msg AnalysisReadHoldingRegister(ref byte[] res, byte[] data, ushort msgId = 0, byte stationId = 0)
        {
            return AnalysisReadRegister(ref res, data, EFunctionCode.ReadHoldingRegister, msgId, stationId);
        }

        /// <summary>
        /// 读输入寄存器返回报文校验及解析
        /// </summary>
        /// <param name="res">返回结果</param>
        /// <param name="data">报文</param>
        /// <param name="msgId">消息号</param>
        /// <param name="stationId">站号</param>
        /// <returns></returns>
        public static Msg AnalysisReadInputRegister(ref ushort[] res, byte[] data, ushort msgId = 0, byte stationId = 0)
        {
            return AnalysisReadRegister(ref res, data, EFunctionCode.ReadInputRegister, msgId, stationId);
        }

        /// <summary>
        /// 读输入寄存器返回报文校验及解析
        /// </summary>
        /// <param name="res">返回结果</param>
        /// <param name="data">报文</param>
        /// <param name="msgId">消息号</param>
        /// <param name="stationId">站号</param>
        /// <returns></returns>
        public static Msg AnalysisReadInputRegister(ref byte[] res, byte[] data, ushort msgId = 0, byte stationId = 0)
        {
            return AnalysisReadRegister(ref res, data, EFunctionCode.ReadInputRegister, msgId, stationId);
        }

        /// <summary>
        /// 读寄存器返回报文校验及解析
        /// </summary>
        /// <param name="res">返回结果</param>
        /// <param name="data">报文</param>
        /// <param name="functionCode">功能码</param>
        /// <param name="msgId">消息号</param>
        /// <param name="stationId">站号</param>
        /// <returns></returns>
        private static Msg AnalysisReadRegister(ref ushort[] res, byte[] data, EFunctionCode functionCode, ushort msgId = 0, byte stationId = 0)
        {
            var msg = CheckMsgHead(data, functionCode, msgId, stationId);
            if (msg.Success)
            {
                if (data[8] == data.Length - 9)
                {
                    res = new ushort[data[8] / 2];
                    for (int i = 9; i < data.Length; i += 2)
                    {
                        res[(i - 9) / 2] = BitConverter.ToUInt16(data, i);
                    }
                    msg.Message = "报文校验成功！";
                    msg.Success = true;
                }
                else
                {
                    msg.Message = "数据域.后续字节长度校验失败！";
                    msg.Success = false;
                }
            }
            return msg;
        }

        /// <summary>
        /// 读寄存器返回报文校验及解析
        /// </summary>
        /// <param name="res">返回结果</param>
        /// <param name="data">报文</param>
        /// <param name="functionCode">功能码</param>
        /// <param name="msgId">消息号</param>
        /// <param name="stationId">站号</param>
        /// <returns></returns>
        private static Msg AnalysisReadRegister(ref byte[] res, byte[] data, EFunctionCode functionCode, ushort msgId = 0, byte stationId = 0)
        {
            var msg = CheckMsgHead(data, functionCode, msgId, stationId);
            if (msg.Success)
            {
                if (data[8] == data.Length - 9)
                {
                    res = new byte[data[8]];
                    Buffer.BlockCopy(data, 9, res, 0, res.Length);
                    msg.Message = "报文校验成功！";
                    msg.Success = true;
                }
                else
                {
                    msg.Message = "数据域.后续字节长度校验失败！";
                    msg.Success = false;
                }
            }
            return msg;
        }

        /// <summary>
        /// 写单个线圈报文校验及解析
        /// </summary>
        /// <param name="data">收到的报文</param>
        /// <param name="sendData">发送的报文</param>
        /// <param name="msgId">消息号</param>
        /// <param name="stationId">站号</param>
        /// <returns></returns>
        public static Msg AnalysisWriteSingleCoil(byte[] data, byte[] sendData, ushort msgId = 0, byte stationId = 0)
        {
            return AnalysisWriteSingle(data, sendData, EFunctionCode.WriteSingleCoil, msgId, stationId);
        }

        /// <summary>
        /// 写单个寄存器报文校验及解析
        /// </summary>
        /// <param name="data">收到的报文</param>
        /// <param name="sendData">发送的报文</param>
        /// <param name="msgId">消息号</param>
        /// <param name="stationId">站号</param>
        /// <returns></returns>
        public static Msg AnalysisWriteSingleRegister(byte[] data, byte[] sendData, ushort msgId = 0, byte stationId = 0)
        {
            return AnalysisWriteSingle(data, sendData, EFunctionCode.WriteSingleRegister, msgId, stationId);
        }

        /// <summary>
        /// 写单个线圈或寄存器报文校验及解析
        /// </summary>
        /// <param name="data">收到的报文</param>
        /// <param name="sendData">发送的报文</param>
        /// <param name="functionCode">功能码</param>
        /// <param name="msgId">消息号</param>
        /// <param name="stationId">站号</param>
        /// <returns></returns>
        private static Msg AnalysisWriteSingle(byte[] data, byte[] sendData, EFunctionCode functionCode, ushort msgId = 0, byte stationId = 0)
        {
            var msg = CheckMsgHead(data, functionCode, msgId, stationId);
            if (msg.Success)
            {
                if (data.Equals(8, sendData, 8, 2))
                {
                    if (data.Equals(10, sendData, 10, 2))
                    {
                        msg.Message = "报文校验成功！";
                        msg.Success = true;
                    }
                    else
                    {
                        msg.Message = "输出值校验失败！";
                        msg.Success = false;
                    }
                }
                else
                {
                    msg.Message = "输出地址校验失败！";
                    msg.Success = false;
                }
            }
            return msg;
        }

        /// <summary>
        /// 写多个线圈报文校验及解析
        /// </summary>
        /// <param name="data">收到的报文</param>
        /// <param name="sendData">发送的报文</param>
        /// <param name="msgId">消息号</param>
        /// <param name="stationId">站号</param>
        /// <returns></returns>
        public static Msg AnalysisWriteMultipleCoils(byte[] data, byte[] sendData, ushort msgId = 0, byte stationId = 0) => AnalysisWriteMultiple(data, sendData, EFunctionCode.WriteMultipleCoils, msgId, stationId);

        /// <summary>
        /// 写多个寄存器
        /// </summary>
        public static Msg AnalysisWriteMultipleRegisters(byte[] data, byte[] sendData, ushort msgId = 0, byte stationId = 0) => AnalysisWriteMultiple(data, sendData, EFunctionCode.WriteMultipleRegisters, msgId, stationId);

        /// <summary>
        /// 写多个线圈或寄存器报文校验及解析
        /// </summary>
        /// <param name="data">收到的报文</param>
        /// <param name="sendData">发送的报文</param>
        /// <param name="functionCode">功能码</param>
        /// <param name="msgId">消息号</param>
        /// <param name="stationId">站号</param>
        /// <returns></returns>
        private static Msg AnalysisWriteMultiple(byte[] data, byte[] sendData, EFunctionCode functionCode, ushort msgId = 0, byte stationId = 0)
        {
            var msg = CheckMsgHead(data, functionCode, msgId, stationId);
            if (msg.Success)
            {
                if (data.Equals(8, sendData, 8, 2))
                {
                    if (data.Equals(10, sendData, 10, 2))
                    {
                        msg.Message = "报文校验成功！";
                        msg.Success = true;
                    }
                    else
                    {
                        msg.Message = "输出数量校验失败！";
                        msg.Success = false;
                    }
                }
                else
                {
                    msg.Message = "起始地址校验失败！";
                    msg.Success = false;
                }
            }
            return msg;
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
        /// 读写多个寄存器报文校验及解析
        /// </summary>
        /// <param name="res">返回结果</param>
        /// <param name="data">报文</param>
        /// <param name="msgId">消息号</param>
        /// <param name="stationId">站号</param>
        /// <returns></returns>
        public static Msg AnalysisReadAndWriteMultipleRegisters(ref ushort[] res, byte[] data, ushort msgId = 0, byte stationId = 0)
        {
            var msg = CheckMsgHead(data, EFunctionCode.ReadAndWriteMultipleRegisters, msgId, stationId);
            if (msg.Success)
            {
                if (data[8] == data.Length - 9)
                {
                    res = new ushort[data[8] / 2];
                    for (int i = 9; i < data.Length; i += 2)
                    {
                        res[(i - 9) / 2] = BitConverter.ToUInt16(data, i);
                    }
                    msg.Message = "报文校验成功！";
                    msg.Success = true;
                }
                else
                {
                    msg.Message = "数据域.后续字节长度校验失败！";
                    msg.Success = false;
                }
            }
            return msg;
        }

        /// <summary>
        /// 读写多个寄存器报文校验及解析
        /// </summary>
        /// <param name="res">返回结果</param>
        /// <param name="data">报文</param>
        /// <param name="msgId">消息号</param>
        /// <param name="stationId">站号</param>
        /// <returns></returns>
        public static Msg AnalysisReadAndWriteMultipleRegisters(ref byte[] res, byte[] data, ushort msgId = 0, byte stationId = 0)
        {
            var msg = CheckMsgHead(data, EFunctionCode.ReadAndWriteMultipleRegisters, msgId, stationId);
            if (msg.Success)
            {
                if (data[8] == data.Length - 9)
                {
                    res = new byte[data[8]];
                    Buffer.BlockCopy(data, 9, res, 0, res.Length);
                    msg.Message = "报文校验成功！";
                    msg.Success = true;
                }
                else
                {
                    msg.Message = "数据域.后续字节长度校验失败！";
                    msg.Success = false;
                }
            }
            return msg;
        }

        /// <summary>
        /// 读设备标识码
        /// </summary>
        //public static Msg ReadDeviceIdentificationCode()
        //{
        //}

        /// <summary>
        /// 校验报文头
        /// </summary>
        /// <param name="data">报文</param>
        /// <param name="functionCode">功能码</param>
        /// <param name="msgId">消息号</param>
        /// <param name="stationId">站号</param>
        /// <returns></returns>
        private static Msg CheckMsgHead(byte[] data, EFunctionCode functionCode, ushort msgId = 0, byte stationId = 0)
        {
            var msg = new Msg();
            if (BitConverter.GetBytes(msgId).Reverse().ToArray().Equals(0, data, 0, 2))
            {
                if (BitConverter.ToInt16(data, 4) == data.Length - 6)
                {
                    if (data[6] == stationId)
                    {
                        if (data[7] == (byte)functionCode)
                        {
                            msg.Message = "报文头校验成功！";
                            msg.Success = true;
                        }
                        else
                        {
                            msg = GetErrorMsg(data[7]);
                        }
                    }
                    else
                    {
                        msg.Message = "报文头.站号校验失败！";
                        msg.Success = false;
                    }
                }
                else
                {
                    msg.Message = "报文头.后续字节长度校验失败！";
                    msg.Success = false;
                }
            }
            else
            {
                msg.Message = "报文头.消息号校验失败！";
                msg.Success = false;
            }
            return msg;
        }

        /// <summary>
        /// 获取异常码信息
        /// </summary>
        /// <param name="errorCode">异常码</param>
        /// <returns></returns>
        private static Msg GetErrorMsg(byte errorCode)
        {
            var err = errorCode - 0x80;
            var res = new Msg() { Success = false };

            switch (err)
            {
                case 0x01:

                case 0x02:

                case 0x03:

                case 0x04:
                    res.Message = $"功能码校验失败，异常码详情：{EnumHelper.GetEnumDescription((EErrorCode)err)}";
                    break;

                default:
                    res.Message = $"功能码校验失败，未知的异常码0x：{errorCode:X2}";
                    break;
            }

            return res;
        }

        #endregion Analysis

        #endregion 方法
    }
}
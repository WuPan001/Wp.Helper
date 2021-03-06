﻿#define TestXML
#define TestFile
#define TestRedis
#define TestEnum
#define TestProperty
#define TestStyle
#define TestIni
#define TestInt
#define TestPing
#define TestBool
#define TestArray
#define TestHtml

using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Wp.Helpers.Entities;
using Wp.Helpers.Entities.WpfStyle;
using Wp.Helpers.Enums;
using Wp.Helpers.ExtensionMethod;
using Wp.Helpers.Helpers;
using Wp.Helpers.Helpers.ProtocolsHelper;
using Wp.Helpers.Helpers.TemplateHelpers.VsCodeHelpers;
using Wp.Helpers.Redis;

namespace ConsoleApp1
{
    internal class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            var cmdCache = string.Empty;
            while (true)
            {
                try
                {
                    //Console.WriteLine("输入\"e\"退出程序");
                    Console.WriteLine("输入\"c\"清空记录");
                    Console.WriteLine("输入\"r\"重复上次操作");
                    Console.WriteLine("输入\"t\"进入测试程序");
                    Console.WriteLine("输入\"生成2个小数之间的随机数\"，以生成2个小数之间的随机数");
                    Console.WriteLine("输入\"保存几何样式\" ，以根据SVG文件生成并保存几何样式");
                    Console.WriteLine("输入\"保存IconFont转TextBlock样式\" ，以根据阿里IconFont生成IconFontStyle样式文件");
                    Console.WriteLine("输入\"数字转中文大写\" ，以将数字转为中文大写");
                    Console.WriteLine("输入\"保存svg转TextBlock样式\" ，以根据svg文件生成TextBlockStyle样式文件");
                    Console.WriteLine("输入\"ModbusTcp\" ，以进行ModbusTcp测试");
                    Console.WriteLine("输入\"AES256加密解密算法\" ，以进行AES256加密解密算法");
                    Console.WriteLine("输入\"VsCode UserTemplate生成\" ，以进行VsCode UserTemplate生成");
                    string cmd = Console.ReadLine();
                    Do(cmd, ref cmdCache);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message); ;
                }
                if (cmdCache != "c")
                {
                    Console.WriteLine("Done");
                    Console.WriteLine(Environment.NewLine);
                }
            }
        }

        private static void Do(string cmd, ref string cmdCache)
        {
            switch (cmd)
            {
                case "VsCode UserTemplate生成":
                    cmdCache = cmd;
                    Console.WriteLine("请输入模板名称，按回车键结束");
                    var userTemplateName = Console.ReadLine();
                    Console.WriteLine("请输入快捷键，按回车键结束");
                    var prefix = Console.ReadLine();
                    Console.WriteLine("请输入模板内容，不要包含空行，按回车键结束");
                    var body = new List<string>();
                    var temp = string.Empty;
                    while (!string.IsNullOrEmpty(temp = Console.ReadLine()))
                    {
                        body.Add(temp);
                    }
                    Console.WriteLine("请输入模板描述，按回车键结束");
                    var description = Console.ReadLine();
                    Console.WriteLine(UserTemplateHelper.GetUserTemplate(userTemplateName, prefix, body, description));
                    break;

                case "AES256加密解密算法":
                    cmdCache = cmd;
                    Console.WriteLine("请输入待加密的字符串");
                    var code = Console.ReadLine();
                    Console.WriteLine("请输入秘钥");
                    var key = Console.ReadLine();
                    var aes = EncryptionHelper.GetAESEncryptionCode(key, code);
                    Console.WriteLine(aes);
                    Console.WriteLine(EncryptionHelper.GetAESDecryptionCode(key, aes));
                    break;

                case "保存svg转TextBlock样式":
                    cmdCache = cmd;
                    SvgHelper.SaveGeometryStyle(FileHelper.GetFilesName());
                    break;

                case "c":
                    cmdCache = "c";
                    Console.Clear();
                    break;

                case "e":
                    return;

                case "r":
                    Do(cmdCache, ref cmdCache);
                    break;

                case "t":
                    cmdCache = cmd;
#if !TestFile
                    foreach (var item in FileHelper.GetFilesFullName(true))
                    {
                        Console.WriteLine(item);
                    }

                    foreach (var item in FileHelper.GetDirectoriesFullName())
                    {
                        Console.WriteLine(item);
                    }
                    foreach (var item in FileHelper.GetFileSystemInfo())
                    {
                        Console.WriteLine(item);
                    }
                    var dic = FileHelper.GetFilesName(extensions: new List<EImgType>() { EImgType.SVG });
                    foreach (var item in dic.Keys)
                    {
                        Console.WriteLine(item);
                        Console.WriteLine(dic[item]);
                        Console.WriteLine(File.ReadAllText(dic[item]));
                    }
                    SvgHelper.SaveGeometryStyle(dic);
                    FileHelper.RenameFiles(new string[] { " (" });
                    FileHelper.ClassificationFiles(new string[] { " (" });
                    FileHelper.RenameFiles(isTotalRename: true);

#elif !TestRedis

                    var redis = new RedisHelper();
                    redis.PushListRight("Test1", "2344");
                    redis.PushListRight("Test1", "2344");
                    redis.PushListRight("Test1", "2344");
                    Console.WriteLine(redis.GetString("Test"));

#elif !TestEnum

                    var test = EnumHelper.GetEnumItems<EImgType>();
                    var dic1 = EnumHelper.GetEnumValueKeyIsDescription<EImgType>();
#elif !TestProperty
                    var styleBase = new StyleBase() { BaseName = "Name" };
                    var propertyDic = PropertyHepler.GetPropertyValueKeyIsName(styleBase);
                    var stop = new Stopwatch();
                    stop.Start();
                    for (int i = 0; i < 10000; i++)
                    {
                        PropertyHepler.GetPropertyValueKeyIsName(styleBase);
                    }
                    stop.Stop();
                    Console.WriteLine(stop.ElapsedMilliseconds);
                    foreach (var item in propertyDic.Keys)
                    {
                        Console.WriteLine($"{item}    {propertyDic[item]}");
                    }

#elif !TestStyle

                    SolidColorBrushStyleHelper.SaveSolidColorBrushStyle();

#elif !TestIni

                    var styleBase = new StyleBase() { BaseName = "Name", BasePath = "Path" };
                    InitializationFileHelper.WriteAsyncPropertyNameAsKey("TestSection", styleBase, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestIni.ini")).Wait();
                    //var temp = IniHelper.ReadAsync("TestSection", "TestBool", Path.Combine(FileHelper.GetDirectory(), "TestIni.ini")).Result;
                    //var temp1 = Convert.ToBoolean(temp);
                    //Console.WriteLine(temp1);
                    //Console.WriteLine(DateTime.Now);
                    var test = InitializationFileHelper.ReadAsyncT<StyleBase>("TestSection", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestIni.ini")).Result;
                    Console.WriteLine(DateTime.Now);

#elif !TestInt

                    byte data = 4;
                    var tempInt = data.ToBooleanArray();
                    foreach (var item in tempInt)
                    {
                        Console.WriteLine(item);
                    }
                    Console.WriteLine("___________________________________");
                    ushort data1 = 4;
                    var temp1 = data1.ToBooleanArray();
                    foreach (var item in temp1)
                    {
                        Console.WriteLine(item);
                    }
                    Console.WriteLine("___________________________________");
                    uint data2 = 4;
                    var temp2 = data2.ToBooleanArray();
                    foreach (var item in temp2)
                    {
                        Console.WriteLine(item);
                    }

#elif !TestPing

                    using (var p = new Ping())
                    {
                        Console.WriteLine(p.Send("192.168.1.115").Status);
                    }

#elif !TestBool

                    bool[] test = new bool[15];
                    test[0] = true;
                    test[3] = true;
                    test[4] = true;
                    test[14] = true;
                    var res = test.ToByteArray(EEndian.LittleEndian);

                    foreach (var item in res)
                    {
                        Console.WriteLine(item);
                    }

#elif !TestArray
                    var tt = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
                    Console.WriteLine(tt.ToFormatString(EScale.Decimal));
                    Console.WriteLine(tt.ToFormatString(EScale.Hex));
                    Console.WriteLine(tt.ToFormatString(EScale.HexToUp));
                    Console.WriteLine(tt.ToFormatString(EScale.HexWithToken));
                    Console.WriteLine(tt.ToFormatString(EScale.HexWithTokenToUp, ","));
                    var bb = new byte[] { 1, 2, 3, 4, 56, 7, 8, 9 };
                    Console.WriteLine(bb.Contains((byte)1));
                    Console.WriteLine(bb.Contains((byte)10));
#elif !TestHtml
                    var htmlPath = FileHelper.GetFilesFullName(false, filter: "HTML文件(*.html) | *.html", title: "请选择demo_index.html文件").FirstOrDefault();
                    if (!string.IsNullOrEmpty(htmlPath))
                    {
                        var doc = new HtmlDocument();
                        doc.Load(htmlPath);
                        var htmlNodes = doc.DocumentNode.SelectNodes("//body/div/div[last()]/div[1]/ul[1]").FirstOrDefault();/*/ul[last()-1]*/
                        var str = new StringBuilder();
                        str.AppendLine(" <ul class=\"icon_lists dib - box\">");
                        str.AppendLine(htmlNodes.InnerHtml);
                        str.Append("</ul>");
                        Console.WriteLine(str);
                        //var children = htmlNodes.FirstOrDefault().ChildNodes.Where(li => li.Name == "li");
                        //foreach (var item in children)
                        //{
                        //    var tt = item.InnerText.Replace(" ", "").Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                        //    foreach (var t in tt)
                        //    {
                        //        Console.WriteLine(t);
                        //    }
                        //    Console.WriteLine("_______________________");
                        //}

                        //foreach (var li in ul.ChildNodes)
                        //{
                        //    Console.WriteLine(li.InnerText);
                        //    Console.WriteLine("_________________________");
                        //}
                        //foreach (var item in)
                        //{
                        //    foreach (var childern in item.ChildNodes)
                        //    {
                        //        Console.WriteLine(childern.InnerText);
                        //        Console.WriteLine("_________________________");
                        //    }
                        //}
                    }
#endif
                    TextBlockStyleHelper.GetStyleFromHtml("test");
                    break;

                case "ModbusTcp":
                    cmdCache = cmd;
                    Console.WriteLine("请输入PLC IP地址，格式为***.***.***.***，默认IP为127.0.0.1");
                    var ip = Console.ReadLine();
                    ip = string.IsNullOrEmpty(ip) ? "127.0.0.1" : ip;
                    IPEndPoint ie = new IPEndPoint(IPAddress.Parse(ip), 502);
                    Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    if (soc.Connected)
                    {
                        //
                    }
                    else
                    {
                        soc.Connect(ie);
                    }
                    Console.WriteLine("请输入\"读线圈\"，以进行读线圈测试");
                    Console.WriteLine("请输入\"读离散量输入\"，以进行读离散量输入测试");
                    Console.WriteLine("请输入\"读保持寄存器\"，以进行读保持寄存器测试");
                    Console.WriteLine("请输入\"读输入寄存器\"，以进行读输入寄存器测试");
                    Console.WriteLine("请输入\"写单个线圈\"，以进行写单个线圈测试");
                    Console.WriteLine("请输入\"写单个寄存器\"，以进行写单个寄存器测试");
                    Console.WriteLine("请输入\"写多个线圈\"，以进行写多个线圈测试");
                    Console.WriteLine("请输入\"写多个寄存器\"，以进行写多个寄存器测试");
                    Console.WriteLine("请输入\"读文件记录\"，以进行读文件记录测试");
                    Console.WriteLine("请输入\"写文件记录\"，以进行写文件记录测试");
                    Console.WriteLine("请输入\"屏蔽写寄存器\"，以进行屏蔽写寄存器测试");
                    Console.WriteLine("请输入\"读写多个寄存器\"，以进行读写多个寄存器测试");
                    Console.WriteLine("请输入\"读设备标识码\"，以进行读设备标识码测试");
                    var func = Console.ReadLine();
                    try
                    {
                        var buffer = new byte[1024];
                        var msg = new Msg();
                        var res = new ushort[1];
                        switch (func)
                        {
                            case "读线圈":
                                Console.WriteLine("请输入线圈地址：");
                                var readCoilAddress = Convert.ToUInt16(Console.ReadLine().Trim());
                                Console.WriteLine("请输入要读取的线圈的数量：");
                                var readCoilCount = Convert.ToUInt16(Console.ReadLine());
                                var readCoilByteArray = ModbusTcpHelper.ReadCoil(readCoilAddress, readCoilCount);
                                Console.WriteLine("Send:");
                                PrintByteArray(readCoilByteArray);
                                soc.Send(readCoilByteArray);
                                Thread.Sleep(100);

                                var readCoilCountTemp = soc.Receive(buffer);
                                var readCoilRec = new byte[6 + buffer[5]];
                                Buffer.BlockCopy(buffer, 0, readCoilRec, 0, readCoilRec.Length);
                                var readCoilRes = new byte[readCoilRec[8]];
                                Buffer.BlockCopy(readCoilRec, 9, readCoilRes, 0, readCoilRes.Length);
                                Console.WriteLine("Rec:");
                                PrintByteArray(readCoilRec);
                                PrintByteArray(readCoilRes);
                                var tempress = new bool[8 * readCoilRes.Length];
                                for (int i = 0; i < readCoilRes.Length; i++)
                                {
                                    Buffer.BlockCopy(readCoilRes[i].ToBooleanArray(), 0, tempress, 8 * i, 8);
                                }
                                foreach (var item in tempress)
                                {
                                    Console.Write($"{item} ");
                                }
                                Console.WriteLine();
                                var tempReadCoilRec = new bool[1];
                                msg = ModbusTcpHelper.AnalysisReadCoil(ref tempReadCoilRec, readCoilRec);
                                Console.WriteLine(msg);
                                if (msg.Success)
                                {
                                    foreach (var item in tempReadCoilRec)
                                    {
                                        Console.WriteLine(item);
                                    }
                                }
                                break;

                            case "读离散量输入":
                                break;

                            case "读保持寄存器":
                                Console.WriteLine("请输入寄存器地址：");
                                var readHoldingRegisterAddress = Convert.ToUInt16(Console.ReadLine().Trim());
                                Console.WriteLine("请输入要读取的寄存器的数量：");
                                var readHoldingRegisterCount = Convert.ToUInt16(Console.ReadLine());
                                var readHoldingRegisterByteArray = ModbusTcpHelper.ReadHoldingRegister(readHoldingRegisterAddress, readHoldingRegisterCount);
                                Console.WriteLine("Send:");
                                PrintByteArray(readHoldingRegisterByteArray);
                                soc.Send(readHoldingRegisterByteArray);
                                Thread.Sleep(100);
                                var readHoldingRegisterCountTemp = soc.Receive(buffer);
                                var readHoldingRegisterRec = new byte[6 + buffer[5]];
                                Buffer.BlockCopy(buffer, 0, readHoldingRegisterRec, 0, 6 + buffer[5]);
                                Console.WriteLine("Rec:");
                                PrintByteArray(readHoldingRegisterRec);

                                msg = ModbusTcpHelper.AnalysisReadHoldingRegister(ref res, readHoldingRegisterRec);
                                Console.WriteLine(msg);
                                foreach (var item in res)
                                {
                                    Console.WriteLine(item);
                                }
                                break;

                            case "读输入寄存器":
                                Console.WriteLine("请输入寄存器地址：");
                                var readInputRegisterAddress = Convert.ToUInt16(Console.ReadLine().Trim());
                                Console.WriteLine("请输入要读取的寄存器的数量：");
                                var readInputRegisterCount = Convert.ToUInt16(Console.ReadLine());
                                var readInputRegisterByteArray = ModbusTcpHelper.ReadInputRegister(readInputRegisterAddress, readInputRegisterCount);
                                Console.WriteLine("Send:");
                                PrintByteArray(readInputRegisterByteArray);
                                soc.Send(readInputRegisterByteArray);
                                Thread.Sleep(100);
                                var readInputRegisterCountTemp = soc.Receive(buffer);
                                var readInputRegisterRec = new byte[6 + buffer[5]];
                                Buffer.BlockCopy(buffer, 0, readInputRegisterRec, 0, 6 + buffer[5]);
                                Console.WriteLine("Rec:");
                                PrintByteArray(readInputRegisterRec);

                                msg = ModbusTcpHelper.AnalysisReadInputRegister(ref res, readInputRegisterRec);
                                Console.WriteLine(msg);
                                foreach (var item in res)
                                {
                                    Console.WriteLine(item);
                                }
                                break;

                            case "写单个线圈":
                                Console.WriteLine("请输入线圈地址：");
                                var writeSingleCoilAddress = Convert.ToUInt16(Console.ReadLine().Trim());
                                Console.WriteLine("请输入要写入的值。值为1表示将该线圈置为true；值为0表示将该线圈置为false：");
                                var writeSingleCoilValue = Console.ReadLine() == "1";
                                var writeSingleCoilByteArray = ModbusTcpHelper.WriteSingleCoil(writeSingleCoilAddress, writeSingleCoilValue);
                                Console.WriteLine("Send:");
                                PrintByteArray(writeSingleCoilByteArray);
                                soc.Send(writeSingleCoilByteArray);
                                break;

                            case "写单个寄存器":
                                Console.WriteLine("请输入寄存器地址：");
                                var writeSingleRegisterAddress = Convert.ToUInt16(Console.ReadLine().Trim());
                                Console.WriteLine("请输入要写入的值：");
                                var writeSingleRegisterValue = Convert.ToUInt16(Console.ReadLine());
                                var writeSingleRegisterByteArray = ModbusTcpHelper.WriteSingleRegister(writeSingleRegisterAddress, writeSingleRegisterValue);
                                Console.WriteLine("Send:");
                                PrintByteArray(writeSingleRegisterByteArray);
                                soc.Send(writeSingleRegisterByteArray);
                                //Thread.Sleep(100);
                                //var singleRegisterCount = soc.Receive(buffer);
                                //PrintByteArray(buffer);
                                //var singleRegisterRec = new byte[6 + buffer[5]];
                                //Buffer.BlockCopy(buffer, 0, singleRegisterRec, 0, 6 + buffer[5]);
                                //Console.WriteLine("Rec:");
                                //PrintByteArray(singleRegisterRec);
                                break;

                            case "写多个线圈":
                                Console.WriteLine("请输入起始线圈地址：");
                                var writeMultiplyColisAddress = Convert.ToUInt16(Console.ReadLine().Trim());
                                Console.WriteLine("请输入要写入的值，以空格分割：");
                                var writeMultiplyColisValuesTemp = Console.ReadLine().Split(' ').Where(s => !string.IsNullOrEmpty(s)).ToArray();
                                var writeMultiplyColisValues = new bool[writeMultiplyColisValuesTemp.Length];
                                for (int i = 0; i < writeMultiplyColisValuesTemp.Length; i++)
                                {
                                    writeMultiplyColisValues[i] = Convert.ToBoolean(writeMultiplyColisValuesTemp[i]);
                                }
                                var writeMultiplyColisByteArray = ModbusTcpHelper.WriteMultipleCoils(writeMultiplyColisAddress, writeMultiplyColisValues);
                                Console.WriteLine("Send:");
                                PrintByteArray(writeMultiplyColisByteArray);
                                soc.Send(writeMultiplyColisByteArray);
                                break;

                            case "写多个寄存器":

                                Console.WriteLine("请输入起始寄存器地址：");
                                var writeMultiplyRegisterAddress = Convert.ToUInt16(Console.ReadLine().Trim());
                                Console.WriteLine("请输入要写入的值，以空格分割：");
                                var writeMultiplyRegisterValuesTemp = Console.ReadLine().Split(' ').Where(s => !string.IsNullOrEmpty(s)).ToArray();
                                var writeMultiplyRegisterValues = new ushort[writeMultiplyRegisterValuesTemp.Length];
                                for (int i = 0; i < writeMultiplyRegisterValuesTemp.Length; i++)
                                {
                                    writeMultiplyRegisterValues[i] = Convert.ToUInt16(writeMultiplyRegisterValuesTemp[i]);
                                }
                                var writeMultiplyRegisterByteArray = ModbusTcpHelper.WriteMultipleRegisters(writeMultiplyRegisterAddress, writeMultiplyRegisterValues);
                                //var writeMultiplyRegisterByteArray = ModbusTcpHelper.WriteMultipleRegisters(writeMultiplyRegisterAddress, new byte[] { 0, 12 });
                                Console.WriteLine("Send:");
                                PrintByteArray(writeMultiplyRegisterByteArray);
                                soc.Send(writeMultiplyRegisterByteArray);
                                break;

                            case "读文件记录":
                                break;

                            case "写文件记录":
                                break;

                            case "屏蔽写寄存器":
                                break;

                            case "读写多个寄存器":

                                Console.WriteLine("请输读寄存器起始地址：");
                                var readAndWriteMultiplyRegisterAddressR = Convert.ToUInt16(Console.ReadLine().Trim());
                                Console.WriteLine("请输读寄存器数量：");
                                var readAndWriteMultiplyRegisterCountR = Convert.ToUInt16(Console.ReadLine().Trim());
                                Console.WriteLine("请输写寄存器起始地址：");
                                var readAndWriteMultiplyRegisterAddressW = Convert.ToUInt16(Console.ReadLine().Trim());
                                Console.WriteLine("请输入要写入的值，以空格分割：");
                                var readAndWriteMultiplyRegisterValuesTemp = Console.ReadLine().Split(' ').Where(s => !string.IsNullOrEmpty(s)).ToArray();
                                var readAndWriteMultiplyRegisterValues = new ushort[readAndWriteMultiplyRegisterValuesTemp.Length];
                                for (int i = 0; i < readAndWriteMultiplyRegisterValuesTemp.Length; i++)
                                {
                                    readAndWriteMultiplyRegisterValues[i] = Convert.ToUInt16(readAndWriteMultiplyRegisterValuesTemp[i]);
                                }
                                var readAndWriteMultiplyRegisterByteArray = ModbusTcpHelper.ReadAndWriteMultipleRegisters(
                                    readAndWriteMultiplyRegisterAddressR, readAndWriteMultiplyRegisterCountR,
                                    readAndWriteMultiplyRegisterAddressW, readAndWriteMultiplyRegisterValues);
                                Console.WriteLine("Send:");
                                PrintByteArray(readAndWriteMultiplyRegisterByteArray);
                                soc.Send(readAndWriteMultiplyRegisterByteArray);
                                Thread.Sleep(100);
                                var readAndWriteMultiplyRegisterCountTemp = soc.Receive(buffer);
                                var readAndWriteMultiplyRegisterRec = new byte[6 + buffer[5]];
                                Buffer.BlockCopy(buffer, 0, readAndWriteMultiplyRegisterRec, 0, 6 + buffer[5]);
                                Console.WriteLine("Rec:");
                                PrintByteArray(readAndWriteMultiplyRegisterRec);

                                msg = ModbusTcpHelper.AnalysisReadAndWriteMultipleRegisters(ref res, readAndWriteMultiplyRegisterRec);
                                Console.WriteLine(msg);
                                foreach (var item in res)
                                {
                                    Console.WriteLine(item);
                                }
                                break;

                            case "读设备标识码":
                                break;

                            default:
                                break;
                        }
                        //soc.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }

                    break;

                case "保存IconFont转TextBlock样式":
                    cmdCache = cmd;
                    Console.WriteLine("请输入样式名：");
                    var name = Console.ReadLine().Trim();
                    Console.WriteLine("请输入样式BaseName，默认为\"PrimaryIconFontTextBlockStyle\"，可回车使用默认BaseName");
                    var baseName = Console.ReadLine().Trim();
                    Console.WriteLine("请输入样式BasePath，默认为\"/Styles/TextBlockStyles.xaml\"，可回车使用默认BasePath");
                    var basePath = Console.ReadLine().Trim();
                    TextBlockStyleHelper.GetStyleFromHtml(name, new StyleBase()
                    {
                        BaseName = string.IsNullOrEmpty(baseName) ? "PrimaryIconFontTextBlockStyle" : baseName,
                        BasePath = string.IsNullOrEmpty(basePath) ? "/Styles/TextBlockStyles.xaml" : basePath
                    });
                    break;

                case "数字转中文大写":
                    cmdCache = cmd;
                    Console.WriteLine("请输入数字：");
                    Console.WriteLine(MathHelper.ToChinese(Convert.ToDecimal(Console.ReadLine())));
                    break;

                case "保存几何样式":
                    cmdCache = cmd;
                    var dicp = FileHelper.GetFilesName(extensions: new List<EImgType>() { EImgType.SVG });
                    SvgHelper.SaveGeometryStyle(dicp);
                    break;

                case "生成2个小数之间的随机数":
                    cmdCache = cmd;
                    Console.WriteLine("请输入第一个小数值：");
                    var first = Convert.ToDouble(Console.ReadLine().Trim());
                    Console.WriteLine("请输入第二个小数值：");
                    var second = Convert.ToDouble(Console.ReadLine().Trim());
                    Console.WriteLine(RandomHelper.NextDouble(first, second));
                    break;

                default:
                    break;
            }
        }

        public static void PrintByteArray(byte[] data)
        {
            foreach (var item in data)
            {
                Console.Write($"{item:X2} ");
            }
            Console.WriteLine();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace Wp.Helpers.Helpers
{
    /// <summary>
    /// 本地设备帮助类
    /// </summary>
    public class LocalMachineHelper
    {
        /// <summary>
        /// 获取CPU序列号代码
        /// </summary>
        /// <returns></returns>

        public static string GetCpuID()
        {
            var res = string.Empty;
            using var mc = new ManagementClass("Win32_Processor");
            using var moc = mc.GetInstances();
            foreach (var mo in moc)
            {
                res = mo.Properties["ProcessorId"].Value.ToString();
            }
            return res;
        }

        /// <summary>
        /// 获取网卡硬件地址
        /// </summary>
        /// <returns></returns>
        public static string GetMacAddress()
        {
            var mac = string.Empty;
            using var mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            using var moc = mc.GetInstances();
            foreach (var mo in moc)
            {
                if ((bool)mo["IPEnabled"] == true)
                {
                    mac = mo["MacAddress"].ToString();
                    break;
                }
            }
            return mac;
        }

        /// <summary>
        /// 获取硬盘ID
        /// </summary>
        /// <returns></returns>
        public static string GetDiskID()
        {
            var HDid = string.Empty;
            using var mc = new ManagementClass("Win32_DiskDrive");
            using var moc = mc.GetInstances();
            foreach (var mo in moc)
            {
                HDid = (string)mo.Properties["Model"].Value;
            }
            return HDid;
        }

        /// <summary>
        /// 获取IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetIPAddress()
        {
            var st = string.Empty;
            using var mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            using var moc = mc.GetInstances();
            foreach (var mo in moc)
            {
                if ((bool)mo["IPEnabled"] == true)
                {
                    Array ar = (Array)(mo.Properties["IpAddress"].Value);
                    st = ar.GetValue(0).ToString();
                    break;
                }
            }
            return st;
        }

        /// <summary>
        /// 操作系统的登录用户名
        /// </summary>
        /// <returns></returns>
        public static string GetUserName()
        {
            return Environment.UserName;
        }

        /// <summary>
        /// 获取计算机名
        /// </summary>
        /// <returns></returns>
        public static string GetComputerName()
        {
            return Environment.MachineName;
        }

        /// <summary>
        /// 系统类型
        /// </summary>
        /// <returns></returns>
        public static string GetSystemType()
        {
            var st = string.Empty;
            using var mc = new ManagementClass("Win32_ComputerSystem");
            using var moc = mc.GetInstances();
            foreach (var mo in moc)
            {
                st = mo["SystemType"].ToString();
            }
            return st;
        }

        /// <summary>
        /// 物理内存
        /// </summary>
        /// <returns></returns>
        public static string GetTotalPhysicalMemory()
        {
            var st = string.Empty;
            using var mc = new ManagementClass("Win32_ComputerSystem");
            using var moc = mc.GetInstances();
            foreach (var mo in moc)
            {
                st = mo["TotalPhysicalMemory"].ToString();
            }
            return st;
        }
    }
}
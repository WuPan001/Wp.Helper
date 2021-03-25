using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wp.Helpers.Helpers
{
    /// <summary>
    /// Windows系统帮助类
    /// </summary>
    public class WinHelper
    {
        /// <summary>
        /// 调用Windows屏幕键盘
        /// </summary>
        public static void RunOsk()
        {
            System.Diagnostics.Process.Start("osk.exe");
        }

        /// <summary>
        /// 查看图片
        /// </summary>
        /// <param name="path">图片文件路径</param>
        public static void RunImageView(string path)
        {
            try
            {
                //建立新的系统进程
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                //设置图片的真实路径和文件名
                process.StartInfo.FileName = path;
                //设置进程运行参数，这里以最大化窗口方法显示图片。
                process.StartInfo.Arguments = "rundl132.exe C://WINDOWS//system32//shimgvw.dll,ImageView_Fullscreen";
                //此项为是否使用Shell执行程序，因系统默认为true，此项也可不设，但若设置必须为true
                process.StartInfo.UseShellExecute = true;
                //此处可以更改进程所打开窗体的显示样式，可以不设
                process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                process.Start();
                process.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 查看图片
        /// </summary>
        /// <param name="img">图片对象</param>
        public static void RunImageView(Image img)
        {
            try
            {
                var tempFilePath = string.Empty;
                if (img != null)
                {
                    tempFilePath = System.Windows.Forms.Application.StartupPath + "\\temp.png";
                    Bitmap bm = new Bitmap(img);
                    bm.Save(tempFilePath);
                    bm.Dispose();
                }
                RunImageView(tempFilePath);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
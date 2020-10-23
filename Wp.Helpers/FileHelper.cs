using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Wp.Helpers
{
    public class FileHelper
    {
        /// <summary>
        /// 获取文件全名
        /// </summary>
        /// <param name="multiselect">是否可多选</param>
        /// <param name="filter">过滤器</param>
        /// <param name="initialDirectory">根目录</param>
        /// <param name="title">标题</param>
        /// <param name="defaultExt">文件默认扩展名</param>
        /// <returns></returns>
        public static ObservableCollection<string> GetFilesFullName(bool multiselect, string filter = "所有文件(*.*)|*.*", string initialDirectory = null, string title = "请选择文件", string defaultExt = null)
        {
            var res = new ObservableCollection<string>();
            using var open = new OpenFileDialog
            {
                Multiselect = multiselect,
                Filter = filter,
                InitialDirectory = initialDirectory,
                Title = title,
                DefaultExt = defaultExt
            };
            if (open.ShowDialog() == DialogResult.OK)
            {
                foreach (var item in open.FileNames)
                {
                    res.Add(item);
                }
            }
            else
            {
                //
            }
            return res;
        }

        /// <summary>
        /// 获取文件夹下所有文件全名
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<string> GetFilesFullName(Environment.SpecialFolder rootFolder = Environment.SpecialFolder.Desktop, string description = "请选择文件夹")
        {
            var res = new ObservableCollection<string>();
            using var open = new FolderBrowserDialog
            {
                Description = description,
                RootFolder = rootFolder
            };
            if (open.ShowDialog() == DialogResult.OK)
            {
                DirectoryInfo myDirectoryInfo = new DirectoryInfo(open.SelectedPath);
                FileInfo[] myFileInfoArray = myDirectoryInfo.GetFiles();
                foreach (var item in myFileInfoArray)
                {
                    res.Add(item.FullName);
                }
            }
            else
            {
                //
            }
            return res;
        }

        /// <summary>
        /// 获取所有文件全路径
        /// </summary>
        /// <param name="showNewFolderButton">是否显示新建文件夹按钮</param>
        /// <param name="rootFolder">根目录</param>
        /// <param name="description">描述</param>
        /// <returns></returns>
        public static ObservableCollection<string> GetDirectoriesFullName(bool showNewFolderButton = false, Environment.SpecialFolder rootFolder = Environment.SpecialFolder.Desktop, string description = "请选择文件夹")
        {
            var res = new ObservableCollection<string>();
            using var open = new FolderBrowserDialog
            {
                Description = description,
                RootFolder = rootFolder,
                ShowNewFolderButton = showNewFolderButton
            };
            if (open.ShowDialog() == DialogResult.OK)
            {
                DirectoryInfo myDirectoryInfo = new DirectoryInfo(open.SelectedPath);
                DirectoryInfo[] myDirectoryInfoArray = myDirectoryInfo.GetDirectories();
                foreach (var item in myDirectoryInfoArray)
                {
                    res.Add(item.FullName);
                }
            }
            else
            {
                //
            }
            return res;
        }

        /// <summary>
        /// 获取文件夹下文件和子文件夹全名
        /// </summary>
        /// <param name="rootFolder">根目录</param>
        /// <param name="description">描述</param>
        /// <returns></returns>
        public static ObservableCollection<string> GetFileSystemInfo(Environment.SpecialFolder rootFolder = Environment.SpecialFolder.Desktop, string description = "请选择文件夹")
        {
            var res = new ObservableCollection<string>();
            using var open = new FolderBrowserDialog
            {
                Description = description,
                RootFolder = rootFolder
            };
            if (open.ShowDialog() == DialogResult.OK)
            {
                DirectoryInfo myDirectoryInfo = new DirectoryInfo(open.SelectedPath);
                FileSystemInfo[] myFileSystemInfoArray = myDirectoryInfo.GetFileSystemInfos();
                foreach (var item in myFileSystemInfoArray)
                {
                    res.Add(item.FullName);
                }
            }
            else
            {
                //
            }
            return res;
        }
    }
}
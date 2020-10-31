using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Wp.Helpers.Enums;

namespace Wp.Helpers
{
    /// <summary>
    /// 文件帮助类
    /// </summary>
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
        /// 获取文件夹下所有文件的文件名和全名
        /// 返回的字典中的key为文件名，value为文件全名
        /// </summary>
        /// <param name="rootFolder">根目录</param>
        /// <param name="description">描述</param>
        /// <param name="extensions">文件扩展名集合，其中文件扩展名形式如.exe</param>
        /// <param name="isReturnExtension">标识返回字典中的key是否包含文件扩展名 如.exe</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetFilesName(Environment.SpecialFolder rootFolder = Environment.SpecialFolder.Desktop,
            string description = "请选择文件夹", List<EImgType> extensions = null, bool isReturnExtension = false)
        {
            var res = new Dictionary<string, string>();
            using var open = new FolderBrowserDialog
            {
                Description = description,
                RootFolder = rootFolder
            };
            if (open.ShowDialog() == DialogResult.OK)
            {
                res = GetFilesName(open.SelectedPath, extensions, isReturnExtension);
            }
            else
            {
                //
            }
            return res;
        }

        /// <summary>
        /// 获取文件夹下所有文件的文件名和全名
        /// 返回的字典中的key为文件名，value为文件全名
        /// </summary>
        /// <param name="path">文件夹路径</param>
        /// <param name="extensions">文件扩展名集合，其中文件扩展名形式如.exe</param>
        /// <param name="isReturnExtension">标识返回字典中的key是否包含文件扩展名 如.exe</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetFilesName(string path, List<EImgType> extensions = null, bool isReturnExtension = false)
        {
            var res = new Dictionary<string, string>();
            var myDirectoryInfo = new DirectoryInfo(path);
            var myFileInfoArray = myDirectoryInfo.GetFiles();
            var extension = string.Empty;
            var dic = EnumHelper.GetEnumValueKeyIsDescription<EImgType>();
            foreach (var item in myFileInfoArray)
            {
                extension = item.Extension;
                if (extensions != null)
                {
                    if (dic.Keys.Contains(extension))
                    {
                        if (isReturnExtension)
                        {
                            res.Add(item.Name, item.FullName);
                        }
                        else
                        {
                            if (extensions.Contains((EImgType)dic[extension]))
                            {
                                res.Add(item.Name.Replace(extension, ""), item.FullName);
                            }
                        }
                    }
                }
                else
                {
                    if (isReturnExtension)
                    {
                        res.Add(item.Name, item.FullName);
                    }
                    else
                    {
                        res.Add(item.Name.Replace(extension, ""), item.FullName);
                    }
                }
            }
            return res;
        }

        /// <summary>
        /// 获取文件夹下所有子文件全路径
        /// </summary>
        /// <param name="showNewFolderButton">是否显示新建文件夹按钮</param>
        /// <param name="rootFolder">根目录</param>
        /// <param name="description">描述</param>
        /// <returns></returns>
        public static ObservableCollection<string> GetDirectoriesFullName(bool showNewFolderButton = false, Environment.SpecialFolder rootFolder = Environment.SpecialFolder.Desktop,
            string description = "请选择文件夹")
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
                res = GetDirectoriesFullName(open.SelectedPath);
            }
            else
            {
                //
            }
            return res;
        }

        /// <summary>
        /// 获取文件夹下所有子文件全路径
        /// </summary>
        /// <param name="path">文件夹路径</param>
        /// <returns></returns>
        public static ObservableCollection<string> GetDirectoriesFullName(string path)
        {
            var res = new ObservableCollection<string>();
            DirectoryInfo myDirectoryInfo = new DirectoryInfo(path);
            DirectoryInfo[] myDirectoryInfoArray = myDirectoryInfo.GetDirectories();
            foreach (var item in myDirectoryInfoArray)
            {
                res.Add(item.FullName);
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
                res = GetFileSystemInfo(open.SelectedPath);
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
        /// <param name="path">文件夹路径</param>
        /// <returns></returns>
        public static ObservableCollection<string> GetFileSystemInfo(string path)
        {
            var res = new ObservableCollection<string>();
            DirectoryInfo myDirectoryInfo = new DirectoryInfo(path);
            FileSystemInfo[] myFileSystemInfoArray = myDirectoryInfo.GetFileSystemInfos();
            foreach (var item in myFileSystemInfoArray)
            {
                res.Add(item.FullName);
            }
            return res;
        }

        /// <summary>
        /// 获取文件夹完整路径
        /// </summary>
        /// <param name="rootFolder">根目录</param>
        /// <param name="description">描述</param>
        /// <returns></returns>
        public static string GetDirectory(Environment.SpecialFolder rootFolder = Environment.SpecialFolder.Desktop, string description = "请选择文件夹")
        {
            var res = string.Empty;
            using var open = new FolderBrowserDialog
            {
                Description = description,
                RootFolder = rootFolder
            };
            if (open.ShowDialog() == DialogResult.OK)
            {
                res = open.SelectedPath;
            }
            else
            {
                //
            }
            return res;
        }

        /// <summary>
        /// 重命名文件
        /// 将文件重命名后复制到根目录下以当前时间命名的文件夹下
        /// 文件重命名规则：将原文件名拆分后第nameIndex+1个元素作为新文件名；若多个文件原文件名拆分后第nameIndex+1个元素相同，则在第nameIndex+1个元素的基础上追加数字作为新文件名称
        /// </summary>
        /// <param name="spilt">文件名拆分字符串数组 若isTotalRename为true，该值为null即可  示例：如原文件名为“Test(1).svg”,则split为new string[] { "(" }即可</param>
        /// <param name="appendString">若isTotalRename为true，则将在该值作为新文件名的基础；若为false，在原文件名拆分后第nameIndex+1个元素后追加该字符串作为新文件名的基础</param>
        /// <param name="nameIndex">新文件名在原文件名拆分后的元素索引号，private 默认为第1个元素 若以在原文件名拆分后的i个元素重新命名文件，则nameIndex为i-1</param>
        /// <param name="isOpenFolder">保存完成后是否打开文件夹</param>
        /// <param name="isTotalRename">是否完全重命名文件</param>
        /// <param name="isBackward">标识重命名追加字符串追加方向 如值为true，则在文件名后追加；如值为false，则在文件名前追加</param>
        public static void RenameFiles(string[] spilt = null, string appendString = null, int nameIndex = 0, bool isOpenFolder = true, bool isTotalRename = false, bool isBackward = true)
        {
            var dic = GetFilesName(isReturnExtension: true);
            RenameFiles(dic, spilt, appendString, nameIndex, isOpenFolder, isTotalRename, isBackward);
        }

        /// <summary>
        /// 重命名文件
        /// 将文件重命名后复制到根目录下以当前时间命名的文件夹下
        /// 文件重命名规则：将原文件名拆分后第nameIndex+1个元素作为新文件名；若多个文件原文件名拆分后第nameIndex+1个元素相同，则在第nameIndex+1个元素的基础上追加数字作为新文件名称
        /// </summary>
        /// <param name="path">文件所在文件夹路径</param>
        /// <param name="spilt">文件名拆分字符串数组 若isTotalRename为true，该值为null即可  示例：如原文件名为“Test(1).svg”,则split为new string[] { "(" }即可</param>
        /// <param name="appendString">若isTotalRename为true，则将在该值作为新文件名的基础；若为false，在原文件名拆分后第nameIndex+1个元素后追加该字符串作为新文件名的基础</param>
        /// <param name="nameIndex">新文件名在原文件名拆分后的元素索引号，private 默认为第1个元素 若以在原文件名拆分后的i个元素重新命名文件，则nameIndex为i-1</param>
        /// <param name="isOpenFolder">保存完成后是否打开文件夹</param>
        /// <param name="isTotalRename">是否完全重命名文件</param>
        /// <param name="isBackward">标识重命名追加字符串追加方向 如值为true，则在文件名后追加；如值为false，则在文件名前追加</param>
        public static void RenameFiles(string path, string[] spilt = null, string appendString = null, int nameIndex = 0, bool isOpenFolder = true, bool isTotalRename = false, bool isBackward = true)
        {
            var dic = GetFilesName(path, isReturnExtension: true);
            RenameFiles(dic, spilt, appendString, nameIndex, isOpenFolder, isTotalRename, isBackward);
        }

        /// <summary>
        /// 重命名文件
        /// </summary>
        /// <param name="dic">文件夹下文件名和文件全名</param>
        /// <param name="spilt">文件名拆分字符串数组 若isTotalRename为true，该值为null即可  示例：如原文件名为“Test(1).svg”,则split为new string[] { "(" }即可</param>
        /// <param name="appendString">若isTotalRename为true，则将在该值作为新文件名的基础；若为false，在原文件名拆分后第nameIndex+1个元素后追加该字符串作为新文件名的基础</param>
        /// <param name="nameIndex">新文件名在原文件名拆分后的元素索引号，private 默认为第1个元素 若以在原文件名拆分后的i个元素重新命名文件，则nameIndex为i-1</param>
        /// <param name="isOpenFolder">保存完成后是否打开文件夹</param>
        /// <param name="isTotalRename">是否完全重命名文件</param>
        /// <param name="isBackward">标识重命名追加字符串追加方向 如值为true，则在文件名后追加；如值为false，则在文件名前追加</param>
        private static void RenameFiles(Dictionary<string, string> dic, string[] spilt = null, string appendString = null, int nameIndex = 0, bool isOpenFolder = true, bool isTotalRename = false, bool isBackward = true)
        {
            if (dic.Count > 0)
            {
                var dateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                var directoryPath = $"{dic[dic.Keys.FirstOrDefault()].Replace(dic.Keys.FirstOrDefault(), string.Empty)}//{dateTime}";
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                foreach (var item in dic.Keys)
                {
                    var tempName = item.Split(spilt, StringSplitOptions.RemoveEmptyEntries)[nameIndex].Replace($".{item.Split('.').LastOrDefault()}", string.Empty);
                    string rename = isTotalRename ? appendString : isBackward ? tempName + appendString : appendString + tempName;
                    FileInfo fileInfo = new FileInfo(dic[item]);
                    if (File.Exists(dic[item].Replace(item, $"{dateTime}\\{rename}.{item.Split('.').LastOrDefault()}")))
                    {
                        for (int i = 0; i < dic.Keys.Count; i++)
                        {
                            if (!File.Exists(dic[item].Replace(item, $"{dateTime}\\{rename}{i}.{item.Split('.').LastOrDefault()}")))
                            {
                                fileInfo.CopyTo(dic[item].Replace(item, $"{dateTime}\\{rename}{i}.{item.Split('.').LastOrDefault()}"));
                                break;
                            }
                        }
                    }
                    else
                    {
                        fileInfo.CopyTo(dic[item].Replace(item, $"{dateTime}\\{rename}.{item.Split('.').LastOrDefault()}"));
                    }
                }
                if (isOpenFolder)
                {
                    System.Diagnostics.Process.Start(directoryPath);
                }
            }
            else
            {
                Console.WriteLine("文件夹下无任何文件！");
            }
        }

        /// <summary>
        /// 整理文件
        /// </summary>
        /// <param name="spilt">文件名拆分字符串数组 若isTotalRename为true，该值为null即可  示例：如原文件名为“Test(1).svg”,则split为new string[] { "(" }即可</param>
        /// <param name="appendString">若isTotalRename为true，则将在该值作为新文件名的基础；若为false，在原文件名拆分后第nameIndex+1个元素后追加该字符串作为新文件名的基础</param>
        /// <param name="nameIndex">新文件名在原文件名拆分后的元素索引号，private 默认为第1个元素 若以在原文件名拆分后的i个元素重新命名文件，则nameIndex为i-1</param>
        /// <param name="isOpenFolder">保存完成后是否打开文件夹</param>
        /// <param name="isTotalRename">是否完全重命名文件</param>
        /// <param name="isBackward">标识重命名追加字符串追加方向 如值为true，则在文件名后追加；如值为false，则在文件名前追加</param>
        public static void ClassificationFiles(string[] spilt = null, string appendString = null, int nameIndex = 0, bool isOpenFolder = true, bool isTotalRename = false, bool isBackward = true)
        {
            var dic = GetFilesName(isReturnExtension: true);
            ClassificationFiles(dic, spilt, appendString, nameIndex, isOpenFolder, isTotalRename, isBackward);
        }

        /// <summary>
        /// 整理文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="spilt">文件名拆分字符串数组 若isTotalRename为true，该值为null即可  示例：如原文件名为“Test(1).svg”,则split为new string[] { "(" }即可</param>
        /// <param name="appendString">若isTotalRename为true，则将在该值作为新文件名的基础；若为false，在原文件名拆分后第nameIndex+1个元素后追加该字符串作为新文件名的基础</param>
        /// <param name="nameIndex">新文件名在原文件名拆分后的元素索引号，private 默认为第1个元素 若以在原文件名拆分后的i个元素重新命名文件，则nameIndex为i-1</param>
        /// <param name="isOpenFolder">保存完成后是否打开文件夹</param>
        /// <param name="isTotalRename">是否完全重命名文件</param>
        /// <param name="isBackward">标识重命名追加字符串追加方向 如值为true，则在文件名后追加；如值为false，则在文件名前追加</param>
        public static void ClassificationFiles(string path, string[] spilt = null, string appendString = null, int nameIndex = 0, bool isOpenFolder = true, bool isTotalRename = false, bool isBackward = true)
        {
            var dic = GetFilesName(path, isReturnExtension: true);
            ClassificationFiles(dic, spilt, appendString, nameIndex, isOpenFolder, isTotalRename, isBackward);
        }

        /// <summary>
        /// 整理文件
        /// </summary>
        /// <param name="dic">文件夹下文件名和文件全名</param>
        /// <param name="spilt">文件名拆分字符串数组 若isTotalRename为true，该值为null即可  示例：如原文件名为“Test(1).svg”,则split为new string[] { "(" }即可</param>
        /// <param name="appendString">若isTotalRename为true，则将在该值作为新文件名的基础；若为false，在原文件名拆分后第nameIndex+1个元素后追加该字符串作为新文件名的基础</param>
        /// <param name="nameIndex">新文件名在原文件名拆分后的元素索引号，private 默认为第1个元素 若以在原文件名拆分后的i个元素重新命名文件，则nameIndex为i-1</param>
        /// <param name="isOpenFolder">保存完成后是否打开文件夹</param>
        /// <param name="isTotalRename">是否完全重命名文件</param>
        /// <param name="isBackward">标识重命名追加字符串追加方向 如值为true，则在文件名后追加；如值为false，则在文件名前追加</param>
        private static void ClassificationFiles(Dictionary<string, string> dic, string[] spilt = null, string appendString = null, int nameIndex = 0, bool isOpenFolder = true, bool isTotalRename = false, bool isBackward = true)
        {
            if (dic.Count > 0)
            {
                var directoryPath = dic[dic.Keys.FirstOrDefault()].Replace(dic.Keys.FirstOrDefault(), string.Empty);
                foreach (var item in dic.Keys)
                {
                    try
                    {
                        var tempName = item.Split(spilt, StringSplitOptions.RemoveEmptyEntries)[nameIndex];
                        string directoryName = isTotalRename ? appendString : isBackward ? tempName + appendString : appendString + tempName;
                        if (!Directory.Exists($"{directoryPath}\\{directoryName }"))
                        {
                            Directory.CreateDirectory($"{directoryPath}\\{directoryName }");
                        }
                        FileInfo fileInfo = new FileInfo(dic[item]);
                        fileInfo.CopyTo($"{directoryPath}\\{directoryName}\\{item}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
                if (isOpenFolder)
                {
                    System.Diagnostics.Process.Start(directoryPath);
                }
            }
            else
            {
                Console.WriteLine("文件夹下无任何文件！");
            }
        }
    }
}
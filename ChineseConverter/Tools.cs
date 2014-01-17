using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ChineseConverter
{
    /// <summary>
    /// 工具类
    /// </summary>
    public static class Tools
    {
        //获取程序所在文件夹绝对路径
        public static string AppDirPath
        {
            get { return System.Windows.Forms.Application.StartupPath + "\\"; }
        }

        /// <summary>
        /// 取消指定文件只读属性
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>是否修改成功</returns>
        public static bool SetFileNotReadOnly(string path)
        {
            if (!File.Exists(path)) return true;
            try
            {
                FileInfo fileInfo = new FileInfo(path);
                if (fileInfo.IsReadOnly)
                {
                    fileInfo.IsReadOnly = false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 检测换行格式
        /// </summary>
        /// <param name="content">文本内容</param>
        /// <returns>换行格式 (-1: 无换行, 0: Unix/Mac, 1: Windows)</returns>
        public static int CheckNewLineType(string content)
        {
            if (content.IndexOf('\n') == -1) return -1;
            if (content.IndexOf("\r\n") >= 0) return 1;
            else return 0;
        }

        /// <summary>
        /// 检查控件的DataSource是否处于准备中
        /// </summary>
        /// <param name="tag">控件标签</param>
        /// <returns>是否处于准备中</returns>
        public static bool CheckControlDataSourceIsPreparing(object tag)
        {
            try
            {
                return Convert.ToBoolean(tag);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

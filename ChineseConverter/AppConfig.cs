using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;

namespace SimplifiedTraditionalConverter
{
    /// <summary>
    /// 程序配置类
    /// </summary>
    public static class AppConfig
    {
        /// <summary>配置文件根节点名称</summary>
        public const string RootElementName = "Application";
        /// <summary>配置文件名</summary>
        public static string ConfigFileName = "AppConfig.xml";
        /// <summary>XML文档类</summary>
        private static XmlDocument AppDocument = null;

        /// <summary>
        /// 配置信息结构
        /// </summary>
        public struct ConfigInfo
        {
            /// <summary>转换类型配置项名称</summary>
            public const string ConvertType = "ConvertType";
            /// <summary>转换方式配置项名称</summary>
            public const string ConvertMethod = "ConvertMethod";
            /// <summary>保存编码配置项名称</summary>
            public const string SaveEncoding = "SaveEncoding";
            /// <summary>文件后缀名类型配置项名称</summary>
            public const string FileExt = "FileExt";
            /// <summary>是否排除(正则表达式)配置项名称</summary>
            public const string IsExclude = "IsExclude";
            /// <summary>是否总在最前配置项名称</summary>
            public const string IsTopMost = "IsTopMost";
            /// <summary>是否转换文件名配置项名称</summary>
            public const string IsConvertFileName = "IsConvertFileName";
        }

        /// <summary>
        /// 获取配置文件绝对路径
        /// </summary>
        public static string ConfigPath
        {
            get
            {
                return System.Windows.Forms.Application.StartupPath + "\\" + ConfigFileName;
            }
        }

        /// <summary>
        /// 打开配置文件
        /// </summary>
        /// <returns>是否打开成功</returns>
        public static bool Open()
        {
            AppDocument = new XmlDocument();
            try
            {
                AppDocument.Load(ConfigPath);
            }
            catch(Exception)
            {
                if (!CreateConfigFile())
                {
                    MessageBox.Show("配置文件创建失败！", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else
                {
                    AppDocument.Load(ConfigPath);
                }
            }
            return true;
        }

        /// <summary>
        /// 保存配置文件
        /// </summary>
        /// <returns>是否保存成功</returns>
        public static bool Save()
        {
            try
            {
                AppDocument.Save(ConfigPath);
            }
            catch(Exception)
            {
                //MessageBox.Show("配置文件保存失败！", "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取指定元素中的指定属性的值
        /// </summary>
        /// <param name="name">配置项</param>
        /// <returns>配置值</returns>
        public static string GetValue(string name, string defaultValue)
        {
            if (AppDocument == null) return defaultValue;
            try
            {
                XmlNode node = AppDocument.SelectSingleNode(RootElementName).SelectSingleNode(name);
                if (node == null) return defaultValue;
                else return node.InnerText.Trim();
            }
            catch(Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 获取指定元素中的指定属性的值
        /// </summary>
        /// <param name="name">配置项</param>
        /// <returns>配置值</returns>
        public static int GetValue(string name, int defaultValue)
        {
            try
            {
                return Convert.ToInt32(GetValue(name, defaultValue.ToString()));
            }
            catch(Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 获取指定元素中的指定属性的值
        /// </summary>
        /// <param name="name">配置项</param>
        /// <returns>配置值</returns>
        public static bool GetValue(string name, bool defaultValue)
        {
            try
            {
                return Convert.ToBoolean(GetValue(name, defaultValue.ToString()));
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 设置指定元素中的指定属性的值
        /// </summary>
        /// <param name="name">配置项</param>
        /// <param name="value">配置值</param>
        public static bool SetValue(string name, string value)
        {
            if (AppDocument == null)
            {
                if (!Open()) return false;
            }
            try
            {
                XmlNode root = AppDocument.SelectSingleNode(RootElementName);
                XmlNode node = root.SelectSingleNode(name);
                if (node != null)
                {
                    node.InnerText = value;
                }
                else
                {
                    XmlElement elem = AppDocument.CreateElement(name);
                    elem.InnerText = value;
                    root.AppendChild(elem);
                }
            }
            catch(Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 设置指定元素中的指定属性的值
        /// </summary>
        /// <param name="name">配置项</param>
        /// <param name="value">配置值</param>
        public static bool SetValue(string name, int value)
        {
            return SetValue(name, value.ToString());
        }

        /// <summary>
        /// 设置指定元素中的指定属性的值
        /// </summary>
        /// <param name="name">配置项</param>
        /// <param name="value">配置值</param>
        public static bool SetValue(string name, bool value)
        {
            return SetValue(name, value.ToString());
        }

        /// <summary>
        /// 创建配置文件
        /// </summary>
        /// <returns>是否创建成功</returns>
        private static bool CreateConfigFile()
        {
            XmlTextWriter xmlWriter = null;
            try
            {
                xmlWriter = new XmlTextWriter(ConfigPath, Encoding.UTF8);
                xmlWriter.Formatting = Formatting.Indented;
                xmlWriter.WriteProcessingInstruction("xml", "version='1.0' encoding='UTF-8'");
                xmlWriter.WriteStartElement(RootElementName);
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (xmlWriter != null) xmlWriter.Close();
            }
            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChineseConverter
{
    /// <summary>
    /// 程序配置类
    /// </summary>
    public static class AppConfig
    {
        /// <summary>默认文件后缀名</summary>
        public const string DefaultFileExt = ".convert";
        /// <summary>配置文件名</summary>
        private const string ConfigFileName = "AppConfig.xml";

        /// <summary>当前<see cref="XmlConfig"/>对象</summary>
        private static XmlConfig Config = null;
        private static int _convertType = 0;
        private static int _convertMethod = 0;
        private static int _saveEncoding = 0;
        private static string _fileExt = DefaultFileExt;
        private static bool _isExclude = false;
        private static bool _isTopMost = false;
        private static bool _isConvertFileName = false;

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
            /// <summary>文件后缀名配置项名称</summary>
            public const string FileExt = "FileExt";
            /// <summary>是否排除配置项名称</summary>
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
        /// 获取或设置转换类型
        /// </summary>
        public static int ConvertType
        {
            get { return _convertType; }
            set
            {
                if (Config != null) Config.SetValue(ConfigInfo.ConvertType, value);
            }
        }

        /// <summary>
        /// 获取或设置转换方式
        /// </summary>
        public static int ConvertMethod
        {
            get { return _convertMethod; }
            set
            {
                if (Config != null) Config.SetValue(ConfigInfo.ConvertMethod, value);
            }
        }

        /// <summary>
        /// 获取或设置保存编码
        /// </summary>
        public static int SaveEncoding
        {
            get { return _saveEncoding; }
            set
            {
                if (Config != null) Config.SetValue(ConfigInfo.SaveEncoding, value);
            }
        }

        /// <summary>
        /// 获取或设置文件后缀名
        /// </summary>
        public static string FileExt
        {
            get { return _fileExt; }
            set
            {
                if (Config != null) Config.SetValue(ConfigInfo.FileExt, value);
            }
        }

        /// <summary>
        /// 获取或设置是否排除
        /// </summary>
        public static bool IsExclude
        {
            get { return _isExclude; }
            set
            {
                if (Config != null) Config.SetValue(ConfigInfo.IsExclude, value);
            }
        }

        /// <summary>
        /// 获取或设置是否总在最前
        /// </summary>
        public static bool IsTopMost
        {
            get { return _isTopMost; }
            set
            {
                if (Config != null) Config.SetValue(ConfigInfo.IsTopMost, value);
            }
        }

        /// <summary>
        /// 获取或设置是否转换文件名
        /// </summary>
        public static bool IsConvertFileName
        {
            get { return _isConvertFileName; }
            set
            {
                if (Config != null) Config.SetValue(ConfigInfo.IsConvertFileName, value);
            }
        }

        /// <summary>
        /// 读取配置文件
        /// </summary>
        /// <returns>是否读取成功</returns>
        public static bool Read()
        {
            try
            {
                Config = new XmlConfig(ConfigPath);
            }
            catch (ApplicationException)
            {
                return false;
            }
            _convertType = Config.GetValue(ConfigInfo.ConvertType, 0);
            _convertMethod = Config.GetValue(ConfigInfo.ConvertMethod, 0);
            _saveEncoding = Config.GetValue(ConfigInfo.SaveEncoding, 0);
            _fileExt = Config.GetValue(ConfigInfo.FileExt, DefaultFileExt);
            _isExclude = Config.GetValue(ConfigInfo.IsExclude, false);
            _isTopMost = Config.GetValue(ConfigInfo.IsTopMost, false);
            _isConvertFileName = Config.GetValue(ConfigInfo.IsConvertFileName, false);
            return true;
        }

        /// <summary>
        /// 保存配置文件
        /// </summary>
        /// <returns>是否保存成功</returns>
        public static bool Save()
        {
            if (Config != null) return Config.Save();
            else return false;
        }

    }
}

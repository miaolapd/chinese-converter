using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

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
        private static string _converterMapsPath = "";
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
            /// <summary>字符对照表配置文件绝对路径元素名</summary>
            public const string ConverterMapsPath = "ConverterMapsPath";
            /// <summary>转换类型元素名</summary>
            public const string ConverterType = "ConverterType";
            /// <summary>转换方式元素名</summary>
            public const string ConvertMethod = "ConvertMethod";
            /// <summary>保存编码元素名</summary>
            public const string SaveEncoding = "SaveEncoding";
            /// <summary>文件后缀名元素名</summary>
            public const string FileExt = "FileExt";
            /// <summary>是否排除元素名</summary>
            public const string IsExclude = "IsExclude";
            /// <summary>是否总在最前元素名</summary>
            public const string IsTopMost = "IsTopMost";
            /// <summary>是否转换文件名元素名</summary>
            public const string IsConvertFileName = "IsConvertFileName";
        }

        /// <summary>
        /// 获取配置文件绝对路径
        /// </summary>
        public static string ConfigPath
        {
            get
            {
                return Tools.AppDirPath + ConfigFileName;
            }
        }

        /// <summary>
        /// 获取或设置字符对照表配置文件绝对路径
        /// </summary>
        public static string ConverterMapsPath
        {
            get { return _converterMapsPath; }
            set
            {
                _converterMapsPath = value;
                if (Config != null) Config.SetValue(ConfigInfo.ConverterMapsPath, Path.GetFileName(value));
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
                _convertType = value;
                if (Config != null) Config.SetValue(ConfigInfo.ConverterType, value);
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
                _convertMethod = value;
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
                _saveEncoding = value;
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
                _fileExt = value;
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
                _isExclude = value;
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
                _isTopMost = value;
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
                _isConvertFileName = value;
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
                Config = new XmlConfig(ConfigPath, true);
            }
            catch (ApplicationException)
            {
                return false;
            }
            _converterMapsPath = Config.GetValue(ConfigInfo.ConverterMapsPath, "");
            _convertType = Config.GetValue(ConfigInfo.ConverterType, 0);
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

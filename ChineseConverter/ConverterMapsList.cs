using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;

namespace ChineseConverter
{
    #region 字符对照表配置列表类

    /// <summary>
    /// 字符对照表配置列表类
    /// </summary>
    public static class ConverterMapsList
    {
        /// <summary>字符对照表配置文件夹名称</summary>
        public const string ConverterMapsListDirName = "ConverterMaps";
        /// <summary>字符对照表配置文件扩展名</summary>
        public const string ConverterMapFileExt = ".xml";

        /// <summary>
        /// 配置信息结构
        /// </summary>
        public struct ConfigInfo
        {
            /// <summary>字符对照表配置文件名称元素名</summary>
            public const string Name = "Name";
            /// <summary>转换器列表元素名</summary>
            public const string Converters = "Converters";
            /// <summary>转换器元素名</summary>
            public const string Converter = "Converter";
            /// <summary>字符对照表列表元素名</summary>
            public const string Maps = "Maps";
            /// <summary>字符对照表元素名</summary>
            public const string Map = "Map";

            /// <summary>
            /// 转换器属性配置信息结构
            /// </summary>
            public struct ConverterAttribute
            {
                /// <summary>转换器ID属性名</summary>
                public const string Id = "Id";
                /// <summary>转换器名称属性名</summary>
                public const string Name = "Name";
                /// <summary>源字符对照表ID属性名</summary>
                public const string SourceMapId = "SourceMapId";
                /// <summary>目标字符对照表ID属性名</summary>
                public const string DestMapId = "DestMapId";
            }

            /// <summary>
            /// 字符对照表属性配置信息结构
            /// </summary>
            public struct MapAttribute
            {
                /// <summary>字符对照表ID属性名</summary>
                public const string Id = "Id";
            }
        }

        private static List<ConverterMaps> _mapsList = null;

        /// <summary>
        /// 获取字符对照表配置列表
        /// </summary>
        public static List<ConverterMaps> MapsList
        {
            get { return _mapsList; }
        }

        /// <summary>
        /// 获取字符对照表配置文件夹绝对路径
        /// </summary>
        public static string ConverterMapsListDirPath
        {
            get { return Tools.AppDirPath + ConverterMapsListDirName + "\\"; }
        }

        /// <summary>
        /// 读取字符对照表配置列表
        /// </summary>
        /// <returns>是否读取成功</returns>
        public static bool ReadConverterMapsList()
        {
            _mapsList = new List<ConverterMaps>();
            if (!Directory.Exists(ConverterMapsListDirPath)) return false;
            string[] fileList = null;
            try
            {
                fileList = Directory.GetFiles(ConverterMapsListDirPath, "*" + ConverterMapFileExt);
            }
            catch (Exception)
            {
                return false;
            }
            if (fileList.Length == 0) return false;
            foreach (string path in fileList)
            {
                try
                {
                    MapsList.Add(ReadConverterMapsConfig(path));
                }
                catch(Exception)
                {
                    continue;
                }
            }
            if (MapsList.Count == 0) return false;
            else return true;
        }

        /// <summary>
        /// 读取字符对照表配置
        /// </summary>
        /// <param name="path">字符对照表配置文件绝对路径</param>
        /// <returns><see cref="ConverterMaps"/>类新实例</returns>
        /// <exception cref="System.ApplicationException"></exception>
        public static ConverterMaps ReadConverterMapsConfig(string path)
        {
            XmlConfig config = null;
            try
            {
                config = new XmlConfig(path);
            }
            catch (ApplicationException)
            {
                throw new ApplicationException("配置文件加载失败！");
            }
            string name = config.GetValue(ConfigInfo.Name, "");
            if (name == String.Empty) throw new ApplicationException("字符对照表名字为空！");
            Dictionary<string, Map> maps = new Dictionary<string, Map>();
            foreach (XElement element in config.GetElements(ConfigInfo.Maps))
            {
                string mapId = config.GetAttributeValue(element, ConfigInfo.MapAttribute.Id, "");
                if (mapId == String.Empty) continue;
                string mapValue = element.Value;
                if (mapValue == null || mapValue.Trim() == String.Empty) continue;
                try
                {
                    maps.Add(mapId, new Map(mapId, mapValue));
                }
                catch (Exception)
                {
                    continue;
                }
            }
            if (maps.Count == 0) throw new ApplicationException("字符对照表序列未找到！");
            SortedDictionary<int, Converter> converters = new SortedDictionary<int, Converter>();
            foreach (XElement element in config.GetElements(ConfigInfo.Converters))
            {
                int converterId = config.GetAttributeValue(element, ConfigInfo.ConverterAttribute.Id, -1);
                if (converterId == -1) continue;
                string converterName = config.GetAttributeValue(element, ConfigInfo.ConverterAttribute.Name, "");
                if (converterName == String.Empty) continue;
                string sourceMapId = config.GetAttributeValue(element, ConfigInfo.ConverterAttribute.SourceMapId, "");
                if (sourceMapId == String.Empty) continue;
                string destMapId = config.GetAttributeValue(element, ConfigInfo.ConverterAttribute.DestMapId, "");
                if (destMapId == String.Empty) continue;
                if (sourceMapId == destMapId) continue;
                if (!CheckConverter(maps, sourceMapId, destMapId)) continue;
                try
                {
                    converters.Add(converterId, new Converter(converterId, converterName, sourceMapId, destMapId));
                }
                catch (Exception)
                {
                    continue;
                }
            }
            if (converters.Count == 0) throw new ApplicationException("转换器未找到！");
            return new ConverterMaps(path, name, maps, converters);
        }

        /// <summary>
        /// 检查转换器是否正确
        /// </summary>
        /// <param name="maps">字符对照表列表</param>
        /// <param name="sourceMapId">源字符对照表ID</param>
        /// <param name="destMapId">目标字符对照表ID</param>
        /// <returns>是否正确</returns>
        public static bool CheckConverter(Dictionary<string, Map> maps, string sourceMapId, string destMapId)
        {
            if (!maps.ContainsKey(sourceMapId)) return false;
            if (!maps.ContainsKey(destMapId)) return false;
            if (maps[sourceMapId].Value.Length != maps[destMapId].Value.Length) return false;
            return true;
        }

        /// <summary>
        /// 根据指定字符对照表列表、源字符对照表ID和目标字符对照表ID对指定文本进行字符转换
        /// </summary>
        /// <param name="content">文本内容</param>
        /// <param name="maps">字符对照表列表</param>
        /// <param name="sourceMapId">源字符对照表ID</param>
        /// <param name="destMapId">目标字符对照表ID</param>
        /// <returns>转换后的文本内容</returns>
        public static string ConvertText(string content, Dictionary<string, Map> maps, string sourceMapId, string destMapId)
        {
            StringBuilder strBuilder = new StringBuilder(content);
            try
            {
                for (int i = 0; i < strBuilder.Length; i++)
                {
                    int index = maps[sourceMapId].Value.IndexOf(strBuilder[i]);
                    if (index >= 0)
                    {
                        strBuilder[i] = maps[destMapId].Value[index];
                    }
                }
            }
            catch (Exception)
            {
                return content;
            }
            return strBuilder.ToString();
        }
    }

    #endregion

    #region 字符对照表配置类

    /// <summary>
    /// 字符对照表配置类
    /// </summary>
    public class ConverterMaps
    {
        private readonly string _configPath;
        private readonly string _name;
        private readonly Dictionary<string, Map> _maps;
        private readonly SortedDictionary<int, Converter> _converters;

        /// <summary>
        /// 获取配置文件绝对路径
        /// </summary>
        public string ConfigPath
        {
            get { return _configPath; }
        }

        /// <summary>
        /// 获取字符对照表配置文件名称
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// 获取字符对照表列表
        /// </summary>
        public Dictionary<string, Map> Maps
        {
            get { return _maps; }
        }

        /// <summary>
        /// 获取转换器列表
        /// </summary>
        public SortedDictionary<int, Converter> Converters
        {
            get { return _converters; }
        }

        /// <summary>
        /// 根据指定配置文件绝对路径、字符对照表配置文件名称、字符对照表列表和转换器列表创建<see cref="ConverterMaps"/>类新实例
        /// </summary>
        /// <param name="configPath">配置文件绝对路径</param>
        /// <param name="name">字符对照表配置文件名称</param>
        /// <param name="maps">字符对照表列表</param>
        /// <param name="converters">转换器列表</param>
        public ConverterMaps(string configPath, string name, Dictionary<string, Map> maps, SortedDictionary<int, Converter> converters)
        {
            _configPath = configPath;
            _name = name;
            _converters = converters;
            _maps = maps;
        }
    }

    #endregion

    #region 字符对照表类

    /// <summary>
    /// 字符对照表类
    /// </summary>
    public class Map
    {
        private readonly string _id;
        private readonly string _value;

        /// <summary>
        /// 获取字符对照表ID
        /// </summary>
        public string Id
        {
            get { return _id; }
        }

        /// <summary>
        /// 获取字符列表
        /// </summary>
        public string Value
        {
            get { return _value; }
        }

        /// <summary>
        /// 根据指定字符对照表ID和字符列表创建<see cref="Map"/>类新实例
        /// </summary>
        /// <param name="id">字符对照表ID</param>
        /// <param name="value">字符列表</param>
        public Map(string id, string value)
        {
            _id = id;
            _value = value;
        }
    }

    #endregion

    #region 转换器类

    /// <summary>
    /// 转换器类
    /// </summary>
    public class Converter
    {
        private readonly int _id;
        private readonly string _name;
        private readonly string _sourceMapId;
        private readonly string _destMapId;

        /// <summary>
        /// 获取转换器ID
        /// </summary>
        public int Id
        {
            get { return _id; }
        }

        /// <summary>
        /// 获取转换器名称
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// 获取源字符对照表ID
        /// </summary>
        public string SourceMapId
        {
            get { return _sourceMapId; }
        }

        /// <summary>
        /// 获取目标字符对照表ID
        /// </summary>
        public string DestMapId
        {
            get { return _destMapId; }
        }

        /// <summary>
        /// 根据指定转换器ID、转换器名称、源字符对照表ID和目标字符对照表ID创建<see cref="Converter"/>类新实例
        /// </summary>
        /// <param name="id">转换器ID</param>
        /// <param name="name">指定的转换器名称</param>
        /// <param name="sourceMapId">源字符对照表ID</param>
        /// <param name="destMapId">目标字符对照表ID</param>
        public Converter(int id, string name, string sourceMapId, string destMapId)
        {
            _id = id;
            _name = name;
            _sourceMapId = sourceMapId;
            _destMapId = destMapId;
        }
    }

    #endregion
}

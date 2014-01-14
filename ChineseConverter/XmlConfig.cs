using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace ChineseConverter
{
    /// <summary>
    /// XML配置类
    /// </summary>
    public class XmlConfig
    {
        /// <summary>默认根元素名称</summary>
        public const string DefaultRootName = "Config";

        private readonly string _configPath;
        private readonly string _rootName;
        /// <summary>当前<see cref="XDocument"/>对象</summary>
        private XDocument Document = null;

        /// <summary>
        /// 获取当前配置文件绝对路径
        /// </summary>
        public string ConfigPath
        {
            get { return _configPath; }
        }

        /// <summary>
        /// 获取根元素
        /// </summary>
        public XElement Root
        {
            get { return Document.Root; }
        }

        /// <summary>
        /// 获取根元素名称
        /// </summary>
        public string RootName
        {
            get { return _rootName; }
        }

        /// <summary>
        /// 根据指定配置文件路径创建带有默认根元素名称的<see cref="XmlConfig"/>类新实例
        /// </summary>
        /// <param name="configPath">配置文件绝对路径</param>
        /// <exception cref="System.ApplicationException"></exception>
        public XmlConfig(string configPath)
            : this(configPath, DefaultRootName)
        {
            
        }

        /// <summary>
        /// 根据指定配置文件路径和指定根元素名称创建<see cref="XmlConfig"/>类新实例
        /// </summary>
        /// <param name="configPath">配置文件绝对路径</param>
        /// <param name="rootName">根元素名称</param>
        /// <exception cref="System.ApplicationException"></exception>
        public XmlConfig(string configPath, string rootName)
        {
            _configPath = configPath;
            _rootName = rootName;
            try
            {
                Document = XDocument.Load(ConfigPath);
            }
            catch (Exception)
            {
                if (!CreateConfigFile()) throw new ApplicationException("创建配置文件失败！");
                try
                {
                    Document = XDocument.Load(ConfigPath);
                }
                catch (Exception)
                {
                    throw new ApplicationException("加载配置文件失败！");
                }
            }
        }

        /// <summary>
        /// 保存配置文件
        /// </summary>
        /// <returns>是否保存成功</returns>
        public bool Save()
        {
            try
            {
                Document.Save(ConfigPath);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 创建配置文件
        /// </summary>
        /// <returns>是否创建成功</returns>
        private bool CreateConfigFile()
        {
            try
            {
                XDocument document = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), new XElement(RootName));
                document.Save(ConfigPath);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取指定元素的值
        /// </summary>
        /// <param name="elementName">元素名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>元素的值</returns>
        public string GetValue(string elementName, string defaultValue)
        {
            try
            {
                XElement element = Root.Element(elementName);
                if (element == null) return defaultValue;
                else return element.Value.Trim();
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 获取指定元素的值
        /// </summary>
        /// <param name="elementName">元素名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>元素的值</returns>
        public int GetValue(string elementName, int defaultValue)
        {
            try
            {
                return Convert.ToInt32(GetValue(elementName, defaultValue.ToString()));
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 获取指定元素的值
        /// </summary>
        /// <param name="elementName">元素名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>元素的值</returns>
        public bool GetValue(string elementName, bool defaultValue)
        {
            try
            {
                return Convert.ToBoolean(GetValue(elementName, defaultValue.ToString()));
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 设置指定元素的值
        /// </summary>
        /// <param name="elementName">元素名称</param>
        /// <param name="value">元素的值</param>
        /// <returns>是否设置成功</returns>
        public bool SetValue(string elementName, string value)
        {
            try
            {
                XElement element = Root.Element(elementName);
                if (element != null)
                {
                    element.SetValue(value);
                }
                else
                {
                    element = new XElement(elementName, value);
                    Root.Add(element);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 设置指定元素的值
        /// </summary>
        /// <param name="elementName">元素名称</param>
        /// <param name="value">元素的值</param>
        /// <returns>是否设置成功</returns>
        public bool SetValue(string elementName, int value)
        {
            return SetValue(elementName, value.ToString());
        }

        /// <summary>
        /// 设置指定元素的值
        /// </summary>
        /// <param name="elementName">元素名称</param>
        /// <param name="value">元素的值</param>
        /// <returns>是否设置成功</returns>
        public bool SetValue(string elementName, bool value)
        {
            return SetValue(elementName, value.ToString());
        }

        /// <summary>
        /// 获取指定元素的指定属性的值
        /// </summary>
        /// <param name="elementName">元素名称</param>
        /// <param name="attributeName">属性名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>属性的值</returns>
        public string GetAttributeValue(string elementName, string attributeName, string defaultValue)
        {
            try
            {
                XElement element = Root.Element(elementName);
                if (element != null)
                {
                    XAttribute attribute = element.Attribute(attributeName);
                    if (attribute != null) return attribute.Value;
                    else return defaultValue;
                }
                else
                {
                    return defaultValue;
                }
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 获取指定元素的指定属性的值
        /// </summary>
        /// <param name="elementName">元素名称</param>
        /// <param name="attributeName">属性名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>属性的值</returns>
        public int GetAttributeValue(string elementName, string attributeName, int defaultValue)
        {
            try
            {
                return Convert.ToInt32(GetAttributeValue(elementName, attributeName, defaultValue.ToString()));
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 获取指定元素的指定属性的值
        /// </summary>
        /// <param name="elementName">元素名称</param>
        /// <param name="attributeName">属性名称</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>属性的值</returns>
        public bool GetAttributeValue(string elementName, string attributeName, bool defaultValue)
        {
            try
            {
                return Convert.ToBoolean(GetAttributeValue(elementName, attributeName, defaultValue.ToString()));
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 设置指定元素的指定属性的值
        /// </summary>
        /// <param name="elementName">元素名称</param>
        /// <param name="attributeName">属性名称</param>
        /// <param name="value">属性的值</param>
        /// <returns>是否设置成功</returns>
        public bool SetAttributeValue(string elementName, string attributeName, string value)
        {
            try
            {
                XElement element = Root.Element(elementName);
                if (element != null)
                {
                    element.SetAttributeValue(attributeName, value);
                }
                else
                {
                    element = new XElement(elementName, new XAttribute(attributeName, value));
                    Root.Add(element);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 设置指定元素的指定属性的值
        /// </summary>
        /// <param name="elementName">元素名称</param>
        /// <param name="attributeName">属性名称</param>
        /// <param name="value">属性的值</param>
        /// <returns>是否设置成功</returns>
        public bool SetAttributeValue(string elementName, string attributeName, int value)
        {
            return SetAttributeValue(elementName, attributeName, value.ToString());
        }

        /// <summary>
        /// 设置指定元素的指定属性的值
        /// </summary>
        /// <param name="elementName">元素名称</param>
        /// <param name="attributeName">属性名称</param>
        /// <param name="value">属性的值</param>
        /// <returns>是否设置成功</returns>
        public bool SetAttributeValue(string elementName, string attributeName, bool value)
        {
            return SetAttributeValue(elementName, attributeName, value.ToString());
        }

        /// <summary>
        /// 获取指定元素的经过筛选的子元素集合
        /// </summary>
        /// <param name="elementName">元素名称</param>
        /// <param name="subElementName">子元素名称</param>
        /// <returns>子元素集合</returns>
        public IEnumerable<XElement> GetElements(string elementName, string subElementName)
        {
            try
            {
                return Root.Element(elementName).Elements(subElementName);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取指定元素的全部子元素集合
        /// </summary>
        /// <param name="elementName">元素名称</param>
        /// <returns>子元素集合</returns>
        public IEnumerable<XElement> GetElements(string elementName)
        {
            try
            {
                return Root.Element(elementName).Elements();
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取指定元素的经过筛选的属性集合
        /// </summary>
        /// <param name="elementName">元素名称</param>
        /// <param name="attributeName">属性名称</param>
        /// <returns>属性集合</returns>
        public IEnumerable<XAttribute> GetAttributes(string elementName, string attributeName)
        {
            try
            {
                return Root.Element(elementName).Attributes(attributeName);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取指定元素的全部属性集合
        /// </summary>
        /// <param name="elementName">元素名称</param>
        /// <returns>属性集合</returns>
        public IEnumerable<XAttribute> GetAttributes(string elementName)
        {
            try
            {
                return Root.Element(elementName).Attributes();
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}

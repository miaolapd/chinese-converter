using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace ChineseConverter
{
    /// <summary>
    /// 主窗口类
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>预设正则表达式列表文件名</summary>
        public const string DefaultRegexListFileName = "DefaultRegexList.txt";
        /// <summary>字符转换对照表文件名</summary>
        public const string ConvertListFileName = "ConvertList.txt";
        /// <summary>默认文件后缀名</summary>
        public const string DefaultFileExt = ".convert";
        /// <summary>转换类型枚举</summary>
        public enum ConvertType { Simplified, Traditional, HuoXingWen };
        /// <summary>转换方式枚举</summary>
        public enum ConvertMethod { ConvertFile, ConvertText };

        private string _sourceFilePath = "";
        private string[] _convertList = null;
        private IdentifyEncoding _identifyEncoding = null;

        /// <summary>获取或设置源文件绝对路径</summary>
        private string SourceFilePath
        {
            get { return _sourceFilePath; }
            set { _sourceFilePath = value; }
        }

        /// <summary>获取字符转换对照表</summary>
        private string[] ConvertList
        {
            get { return _convertList; }
            set { _convertList = value; }
        }

        /// <summary>获取或设置检测字符编码类</summary>
        private IdentifyEncoding IdentifyEncoding
        {
            get { return _identifyEncoding; }
            set { _identifyEncoding = value; }
        }

        /// <summary>
        /// 读取源文件
        /// </summary>
        /// <param name="path">源文件绝对路径</param>
        /// <returns>是否读取成功</returns>
        private bool ReadSourceFile(string path)
        {
            if (path == String.Empty || !File.Exists(path)) return false;
            SourceFilePath = path;
            if (IdentifyEncoding == null) IdentifyEncoding = new IdentifyEncoding();
            string content = "";
            string encodingName = "";
            try
            {
                if (cboReadEncoding.SelectedIndex == 0)
                {
                    encodingName = IdentifyEncoding.GetEncodingName(new FileInfo(SourceFilePath));
                    content = File.ReadAllText(SourceFilePath, GetEncodingByName(encodingName));
                }
                else
                {
                    encodingName = cboReadEncoding.SelectedItem.ToString();
                    content = File.ReadAllText(SourceFilePath, GetReadEncoding(cboReadEncoding.SelectedIndex));
                }
            }
            catch (Exception)
            {
                SourceFilePath = "";
                return false;
            }
            if (CheckNewLineType(content) == 0)
            {
                content = content.Replace("\n", "\r\n");
            }
            txtSource.Text = content;
            lblFilePath.Text = "文件路径：" + SourceFilePath;
            lblFileEncoding.Text = "文件编码：" + encodingName;
            return true;
        }

        /// <summary>
        /// 写入目标文件
        /// </summary>
        /// <param name="path">目标文件绝对路径</param>
        /// <returns>是否写入成功</returns>
        private bool WriteDestFile(string path)
        {
            if (path == String.Empty) return false;
            if (File.Exists(path))
            {
                if (!SetFileNotReadOnly(path)) return false;
            }
            try
            {
                File.WriteAllText(path, txtDest.Text, GetSaveEncoding(cboSaveEncoding.SelectedIndex));
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 按照编码名称获取指定的编码类型
        /// </summary>
        /// <param name="name">编码名称</param>
        /// <returns>编码类型</returns>
        private Encoding GetEncodingByName(string name)
        {
            switch (name.ToLower())
            {
                case "utf-8": return Encoding.UTF8;
                case "unicode": return Encoding.Unicode;
                case "gb2312":
                case "gbk": return Encoding.GetEncoding("gb2312");
                case "big5": return Encoding.GetEncoding("big5");
                case "hz": return Encoding.GetEncoding("hz-gb-2312");
                default: return Encoding.Default;
            }
        }

        /// <summary>
        /// 按照保存编码下拉列表的索引号获取保存编码的编码类型
        /// </summary>
        /// <param name="index">保存编码下拉列表的索引号</param>
        /// <returns>编码类型</returns>
        private Encoding GetSaveEncoding(int index)
        {
            switch (index)
            {
                case 0: return Encoding.UTF8;
                case 1: return Encoding.Unicode;
                case 2: return Encoding.GetEncoding("gb2312");
                case 3: return Encoding.GetEncoding("big5");
                default: return Encoding.Default;
            }
        }

        /// <summary>
        /// 按照读取编码下拉列表的索引号获取保存编码的编码类型
        /// </summary>
        /// <param name="index">读取编码下拉列表的索引号</param>
        /// <returns>编码类型</returns>
        private Encoding GetReadEncoding(int index)
        {
            switch (index)
            {
                case 1: return Encoding.UTF8;
                case 2: return Encoding.Unicode;
                case 3: return Encoding.GetEncoding("gb2312");
                case 4: return Encoding.GetEncoding("big5");
                case 5: return Encoding.GetEncoding("hz-gb-2312");
                case 6: return Encoding.GetEncoding("shift_jis");
                case 7: return Encoding.GetEncoding("euc-kr");
                default: return Encoding.Default;
            }
        }

        /// <summary>
        /// 转换文本
        /// </summary>
        private void ConvertText()
        {
            string sourceContent = txtSource.Text;
            if (sourceContent == String.Empty)
            {
                txtDest.Text = "";
                return;
            }
            string destContent = "";
            ConvertType convertType = (ConvertType)cboConvertType.SelectedIndex;
            if (cboRegex.Text.Trim() != String.Empty)
            {
                Regex regex = null;
                try
                {
                    regex = new Regex(cboRegex.Text.Trim(), RegexOptions.IgnoreCase | RegexOptions.Multiline);
                }
                catch (Exception)
                {
                    ShowErrorMsg("正则表达式错误！");
                    cboRegex.SelectAll();
                    cboRegex.Focus();
                    return;
                }
                if (chkIsExclude.Checked)
                {
                    StringBuilder strBuilder = new StringBuilder(ConvertChinese(sourceContent, convertType));
                    foreach (Match match in regex.Matches(sourceContent))
                    {
                        strBuilder.Replace(ConvertChinese(match.Value, convertType), match.Value);
                    }
                    destContent = strBuilder.ToString();
                }
                else
                {
                    destContent = regex.Replace(sourceContent, match => ConvertChinese(match.Value, convertType));
                }
            }
            else
            {
                destContent = ConvertChinese(sourceContent, convertType);
            }
            txtDest.Text = destContent;
        }

        /// <summary>
        /// 将文本转换为指定的文字类型
        /// </summary>
        /// <param name="content">要转换的文本</param>
        /// <param name="convertType">转换类型</param>
        /// <returns>转换后的文本</returns>
        private string ConvertChinese(string content, ConvertType convertType)
        {
            StringBuilder strBuilder = new StringBuilder(content);
            for (int i = 0; i < ConvertList[(int)convertType].Length; i++)
            {
                if (convertType == ConvertType.HuoXingWen)
                {
                    strBuilder.Replace(ConvertList[(int)ConvertType.Simplified][i], ConvertList[(int)ConvertType.HuoXingWen][i]);
                    strBuilder.Replace(ConvertList[(int)ConvertType.Traditional][i], ConvertList[(int)ConvertType.HuoXingWen][i]);
                }
                else if (convertType == ConvertType.Traditional)
                {
                    strBuilder.Replace(ConvertList[(int)ConvertType.Simplified][i], ConvertList[(int)ConvertType.Traditional][i]);
                }
                else
                {
                    strBuilder.Replace(ConvertList[(int)ConvertType.Traditional][i], ConvertList[(int)ConvertType.Simplified][i]);
                }
            }
            return strBuilder.ToString();
        }

        /// <summary>
        /// 读取转换列表
        /// </summary>
        /// <returns>是否读取成功</returns>
        private bool ReadConvertList()
        {
            string path = Application.StartupPath + "\\" + ConvertListFileName;
            if (!File.Exists(path)) return false;
            string content = "";
            try
            {
                content = File.ReadAllText(path, Encoding.UTF8);
            }
            catch (Exception)
            {
                return false;
            }
            ConvertList = content.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (ConvertList.Length < 2) return false;
            if (ConvertList[0].Length != ConvertList[1].Length) return false;
            if (ConvertList.Length == 3)
            {
                if (ConvertList[0].Length != ConvertList[2].Length) return false;
            }
            else
            {
                cboConvertType.Items.RemoveAt((int)ConvertType.HuoXingWen);
            }
            return true;
        }

        /// <summary>
        /// 转换文件名
        /// </summary>
        /// <returns>是否转换成功</returns>
        private bool ConvertFileName()
        {
            if (SourceFilePath == String.Empty || !File.Exists(SourceFilePath)) return false;
            string destFilePath = ConvertChinese(SourceFilePath, (ConvertType)cboConvertType.SelectedIndex);
            if (SourceFilePath == destFilePath)
            {
                return true;
            }
            else
            {
                try
                {
                    File.Copy(SourceFilePath, destFilePath, true);
                    File.Delete(SourceFilePath);
                }
                catch (Exception)
                {
                    return false;
                }
                SourceFilePath = destFilePath;
                lblFilePath.Text = SourceFilePath;
                return true;
            }
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
        /// 读取预设正则表达式列表
        /// </summary>
        private void ReadDefaultRegexList()
        {
            string[] regexList = null;
            try
            {
                regexList = File.ReadAllText(Application.StartupPath + "\\" + DefaultRegexListFileName, Encoding.UTF8)
                    .Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            }
            catch (Exception) { }
            if (regexList != null) cboRegex.Items.AddRange(regexList);
        }

        /// <summary>
        /// 读取程序配置
        /// </summary>
        private void ReadConfig()
        {
            AppConfig.Open();
            try
            {
                cboConvertType.SelectedIndex = AppConfig.GetValue(AppConfig.ConfigInfo.ConvertType, 0);
            }
            catch (ArgumentOutOfRangeException)
            {
                cboConvertType.SelectedIndex = 0;
            }
            try
            {
                cboConvertMethod.SelectedIndex = AppConfig.GetValue(AppConfig.ConfigInfo.ConvertMethod, 0);
            }
            catch (ArgumentOutOfRangeException)
            {
                cboConvertMethod.SelectedIndex = 0;
            }
            try
            {
                cboSaveEncoding.SelectedIndex = AppConfig.GetValue(AppConfig.ConfigInfo.SaveEncoding, 0);
            }
            catch (ArgumentOutOfRangeException)
            {
                cboSaveEncoding.SelectedIndex = 0;
            }
            chkIsConvertFileName.Checked = AppConfig.GetValue(AppConfig.ConfigInfo.IsConvertFileName, false);
            chkIsExclude.Checked = AppConfig.GetValue(AppConfig.ConfigInfo.IsExclude, false);
            txtFileExt.Text = AppConfig.GetValue(AppConfig.ConfigInfo.FileExt, DefaultFileExt);
            chkIsTopMost.Checked = AppConfig.GetValue(AppConfig.ConfigInfo.IsTopMost, false);
        }

        /// <summary>
        /// 检测换行格式
        /// </summary>
        /// <param name="content">文本内容</param>
        /// <returns>换行格式 (-1: 无换行, 0: Unix/Mac, 1: Windows)</returns>
        private int CheckNewLineType(string content)
        {
            if (content.IndexOf('\n') == -1) return -1;
            if (content.IndexOf("\r\n") >= 0) return 1;
            else return 0;
        }

        /// <summary>
        /// 显示操作成功消息
        /// </summary>
        /// <param name="msg">消息</param>
        private void ShowSuccessMsg(string msg)
        {
            MessageBox.Show(msg, "操作成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 显示操作失败消息
        /// </summary>
        /// <param name="msg">消息</param>
        private void ShowErrorMsg(string msg)
        {
            MessageBox.Show(msg, "操作失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 当窗口加载时发生
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
            cboReadEncoding.SelectedIndex = 0;
            if (!ReadConvertList())
            {
                ShowErrorMsg("字符转换对照表读取失败！\r\n可能原因：文件未找到、各行字符数不一致");
                Close();
                return;
            }
            ReadDefaultRegexList();
            ReadConfig();
            if (Environment.GetCommandLineArgs().Length > 1)
            {
                if (ReadSourceFile(Environment.GetCommandLineArgs()[1]))
                {
                    ConvertText();
                }
            }
            else
            {
                try
                {
                    Thread identifyEncodingThread = new Thread(obj => IdentifyEncoding = new IdentifyEncoding());
                    identifyEncodingThread.IsBackground = true;
                    identifyEncodingThread.Start();
                }
                catch (Exception) { }
            }
        }

        /// <summary>
        /// 当窗口将要关闭时发生
        /// </summary>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            AppConfig.Save();
        }

        /// <summary>
        /// 当文件被拖拽到窗口上时发生
        /// </summary>
        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            if (cboConvertMethod.SelectedIndex == (int)ConvertMethod.ConvertText) cboConvertMethod.SelectedIndex = (int)ConvertMethod.ConvertFile;
            if (ReadSourceFile(((string[])e.Data.GetData(DataFormats.FileDrop))[0]))
            {
                ConvertText();
            }
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        /// <summary>
        /// 当在窗口中按下按键时发生
        /// </summary>
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case (int)Keys.S:
                    if (e.Control)
                    {
                        if (e.Shift)
                        {
                            if (btnSaveAs.Enabled) btnSaveAs_Click(null, null);
                        }
                        else
                        {
                            if (btnSave.Enabled) btnSave_Click(null, null);
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// 当在窗口调整大小时发生
        /// </summary>
        private void MainForm_Resize(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 当单击重新载入按钮时发生
        /// </summary>
        private void btnReload_Click(object sender, EventArgs e)
        {
            if (ReadSourceFile(SourceFilePath))
            {
                ConvertText();
            }
        }

        /// <summary>
        /// 当单击保存按钮时发生
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SourceFilePath == String.Empty) return;
            if (!WriteDestFile(SourceFilePath))
            {
                ShowErrorMsg("文件保存失败！");
                return;
            }
            else
            {
                if (chkIsConvertFileName.Checked)
                {
                    if (!ConvertFileName())
                    {
                        ShowErrorMsg("文件名转换失败！");
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 当单击另存为按钮时发生
        /// </summary>
        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            if (SourceFilePath == String.Empty) return;
            if (txtFileExt.Text.Trim() == String.Empty)
            {
                ShowErrorMsg("请填写后缀名！");
                txtFileExt.SelectAll();
                txtFileExt.Focus();
            }
            string path = Path.GetDirectoryName(SourceFilePath) + "\\" + Path.GetFileNameWithoutExtension(SourceFilePath) +
                txtFileExt.Text.Trim() + Path.GetExtension(SourceFilePath);
            if (!WriteDestFile(path))
            {
                ShowErrorMsg("文件保存失败！");
            }
            AppConfig.SetValue(AppConfig.ConfigInfo.FileExt, txtFileExt.Text.Trim());
        }

        /// <summary>
        /// 当单击转换按钮时发生
        /// </summary>
        private void btnConvert_Click(object sender, EventArgs e)
        {
            if (cboConvertMethod.SelectedIndex == (int)ConvertMethod.ConvertFile && SourceFilePath == String.Empty)
                cboConvertMethod.SelectedIndex = (int)ConvertMethod.ConvertText;
            ConvertText();
        }

        /// <summary>
        /// 当转换类型下拉列表选择项改变时发生
        /// </summary>
        private void cboConvertType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConvertText();
            AppConfig.SetValue(AppConfig.ConfigInfo.ConvertType, cboConvertType.SelectedIndex);
        }

        /// <summary>
        /// 当转换方式下拉列表选择项改变时发生
        /// </summary>
        private void cboConvertMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboConvertMethod.SelectedIndex == (int)ConvertMethod.ConvertText)
            {
                SourceFilePath = "";
                btnReload.Enabled = false;
                btnSave.Enabled = false;
                btnSaveAs.Enabled = false;
                txtFileExt.Enabled = false;
                lblFilePath.Text = "";
                lblFileEncoding.Text = "";
                cboReadEncoding.Enabled = false;
            }
            else
            {
                btnReload.Enabled = true;
                btnSave.Enabled = true;
                btnSaveAs.Enabled = true;
                txtFileExt.Enabled = true;
                txtSource.Clear();
                txtDest.Clear();
                cboReadEncoding.Enabled = true;
            }
            AppConfig.SetValue(AppConfig.ConfigInfo.ConvertMethod, cboConvertMethod.SelectedIndex);
        }

        /// <summary>
        /// 当是否转换文件名复选框选中状态改变时发生
        /// </summary>
        private void chkIsConvertFileName_CheckedChanged(object sender, EventArgs e)
        {
            AppConfig.SetValue(AppConfig.ConfigInfo.IsConvertFileName, chkIsConvertFileName.Checked);
        }

        /// <summary>
        /// 当保存编码下拉列表选择项改变时发生
        /// </summary>
        private void cboSaveEncoding_SelectedIndexChanged(object sender, EventArgs e)
        {
            AppConfig.SetValue(AppConfig.ConfigInfo.SaveEncoding, cboSaveEncoding.SelectedIndex);
        }

        /// <summary>
        /// 当单击清除正则表达式按钮时发生
        /// </summary>
        private void btnRegexClear_Click(object sender, EventArgs e)
        {
            cboRegex.Text = "";
            cboRegex.Focus();
            btnConvert_Click(null, null);
        }

        /// <summary>
        /// 当单击查看正则表达式帮助按钮时发生
        /// </summary>
        private void btnRegexHelp_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.ProcessStartInfo proInfo =
                    new System.Diagnostics.ProcessStartInfo("http://zh.wikipedia.org/wiki/%E6%AD%A3%E5%88%99%E8%A1%A8%E8%BE%BE%E5%BC%8F");
                System.Diagnostics.Process pro = System.Diagnostics.Process.Start(proInfo);
            }
            catch (Exception) { }
        }

        /// <summary>
        /// 当是否排除复选框选中状态改变时发生
        /// </summary>
        private void chkIsExclude_CheckedChanged(object sender, EventArgs e)
        {
            if (cboRegex.Text.Trim() != String.Empty) btnConvert_Click(null, null);
            AppConfig.SetValue(AppConfig.ConfigInfo.IsExclude, chkIsExclude.Checked);
        }

        /// <summary>
        /// 当总在最前复选框选中状态改变时发生
        /// </summary>
        private void chkIsTopMost_CheckedChanged(object sender, EventArgs e)
        {
            TopMost = chkIsTopMost.Checked;
            AppConfig.SetValue(AppConfig.ConfigInfo.IsTopMost, chkIsTopMost.Checked);
        }

        /// <summary>
        /// 当在文本框按下按键时发生
        /// </summary>
        private void txtContent_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            switch (e.KeyValue)
            {
                case (int)Keys.A:
                    if (e.Control) textBox.SelectAll();
                    break;
                case (int)Keys.T:
                    if (e.Control) btnConvert_Click(null, null);
                    break;
            }
        }

        /// <summary>
        /// 当在正则表达式下拉列表按下按键时发生
        /// </summary>
        private void cboRegex_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case (int)Keys.Enter:
                    btnConvert_Click(null, null);
                    break;
            }
        }

        /// <summary>
        /// 当在正则表达式下拉列表选择项改变时发生
        /// </summary>
        private void cboRegex_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnConvert_Click(null, null);
        }

        /// <summary>
        /// 当读取编码下拉列表选择项改变时发生
        /// </summary>
        private void cboReadEncoding_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SourceFilePath != String.Empty)
            {
                if (ReadSourceFile(SourceFilePath)) ConvertText();
            }
        }

        #region 主菜单事件

        /// <summary>
        /// 当单击打开菜单项时发生
        /// </summary>
        private void tsmiOpenFile_Click(object sender, EventArgs e)
        {
            if (ofdOpenFile.ShowDialog() == DialogResult.OK)
            {
                cboConvertMethod.SelectedIndex = (int)ConvertMethod.ConvertFile;
                if (!ReadSourceFile(ofdOpenFile.FileName))
                {
                    ShowErrorMsg("文件读取失败！");
                    return;
                }
                else
                {
                    ConvertText();
                }
            }
        }

        /// <summary>
        /// 当单击保存结果菜单项时发生
        /// </summary>
        private void tsmiSaveFile_Click(object sender, EventArgs e)
        {
            if (txtDest.Text == String.Empty) return;
            if (SourceFilePath != String.Empty)
            {
                sfdSaveFile.InitialDirectory = Path.GetDirectoryName(SourceFilePath);
                sfdSaveFile.FileName = "*" + Path.GetExtension(SourceFilePath);
            }
            if (sfdSaveFile.ShowDialog() == DialogResult.OK)
            {
                if (!WriteDestFile(sfdSaveFile.FileName))
                {
                    ShowErrorMsg("文件保存失败！");
                    return;
                }
            }
        }

        /// <summary>
        /// 当单击恢复默认设置菜单项时发生
        /// </summary>
        private void tsmiRestoreConfig_Click(object sender, EventArgs e)
        {
            try
            {
                cboReadEncoding.SelectedIndex = 0;
                File.Delete(AppConfig.ConfigPath);
                ReadConfig();
            }
            catch (Exception) { }
        }

        /// <summary>
        /// 当单击退出菜单项时发生
        /// </summary>
        private void tsmiQuit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 当单击关于菜单项时发生
        /// </summary>
        private void tsmiAbout_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }

        #endregion

    }
}

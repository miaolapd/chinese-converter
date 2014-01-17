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
        /// <summary>转换方式枚举</summary>
        public enum ConvertMethod { ConvertFile, ConvertText };

        private string _sourceFilePath = "";
        private IdentifyEncoding _identifyEncoding = null;

        /// <summary>获取或设置源文件绝对路径</summary>
        private string SourceFilePath
        {
            get { return _sourceFilePath; }
            set { _sourceFilePath = value; }
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
            if (cboConvertMethod.SelectedIndex == (int)ConvertMethod.ConvertText)
            {
                cboConvertMethod.SelectedIndex = (int)ConvertMethod.ConvertFile;
            }
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
            if (Tools.CheckNewLineType(content) == 0)
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
                if (!Tools.SetFileNotReadOnly(path)) return false;
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
        private void ConvertSourceToDestText()
        {
            if (cboConverterType.SelectedIndex == -1) return;
            string sourceContent = txtSource.Text;
            string destContent = "";
            ConverterMaps converterMaps = (ConverterMaps)cboConverterMapsList.SelectedItem;
            Converter converter = (Converter)cboConverterType.SelectedItem;
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
                    StringBuilder strBuilder = new StringBuilder(ConverterMapsList.ConvertText(sourceContent, converterMaps.Maps,
                        converter.SourceMapId, converter.DestMapId));
                    foreach (Match match in regex.Matches(sourceContent))
                    {
                        strBuilder.Replace(ConverterMapsList.ConvertText(match.Value, converterMaps.Maps, converter.SourceMapId, converter.DestMapId),
                            match.Value);
                    }
                    destContent = strBuilder.ToString();
                }
                else
                {
                    destContent = regex.Replace(sourceContent, match =>
                        ConverterMapsList.ConvertText(match.Value, converterMaps.Maps, converter.SourceMapId, converter.DestMapId));
                }
            }
            else
            {
                destContent = ConverterMapsList.ConvertText(sourceContent, converterMaps.Maps, converter.SourceMapId, converter.DestMapId);
            }
            txtDest.Text = destContent;
        }

        /// <summary>
        /// 读取字符对照表配置列表
        /// </summary>
        private void ReadConverterMapsList()
        {
            if (!ConverterMapsList.ReadConverterMapsList())
            {
                ShowErrorMsg("字符对照表读取失败！");
                return;
            }
            cboConverterMapsList.Tag = true;
            BindingSource bs = new BindingSource();
            bs.DataSource = ConverterMapsList.MapsList;
            cboConverterMapsList.DataSource = bs;
            cboConverterMapsList.DisplayMember = "Name";
            cboConverterMapsList.ValueMember = "ConfigPath";
            cboConverterMapsList.SelectedIndex = 0;
            cboConverterMapsList.Tag = false;
            cboConverterMapsList_SelectedIndexChanged(null, null);
        }

        /// <summary>
        /// 转换文件名
        /// </summary>
        /// <returns>是否转换成功</returns>
        private bool ConvertFileName()
        {
            if (cboConverterType.SelectedIndex == -1) return false;
            if (SourceFilePath == String.Empty || !File.Exists(SourceFilePath)) return false;
            ConverterMaps converterMaps = (ConverterMaps)cboConverterMapsList.SelectedItem;
            Converter converter = (Converter)cboConverterType.SelectedItem;
            string destFilePath = ConverterMapsList.ConvertText(SourceFilePath, converterMaps.Maps, converter.SourceMapId, converter.DestMapId);
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
        /// 读取预设正则表达式列表
        /// </summary>
        private void ReadDefaultRegexList()
        {
            string[] regexList = null;
            try
            {
                regexList = File.ReadAllText(Tools.AppDirPath + DefaultRegexListFileName, Encoding.UTF8)
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
            if(!AppConfig.Read())
            {
                ShowErrorMsg("读取配置文件失败！");
            }
            for (int i = 0; i < cboConverterMapsList.Items.Count; i++)
            {
                ConverterMaps converterMaps = (ConverterMaps)cboConverterMapsList.Items[i];
                if (AppConfig.ConverterMapsPath.ToLower() == Path.GetFileName(converterMaps.ConfigPath.ToLower()))
                {
                    cboConverterMapsList.SelectedIndex = i;
                    break;
                }
            }
            try
            {
                cboConverterType.SelectedIndex = AppConfig.ConvertType;
            }
            catch (ArgumentOutOfRangeException)
            {
                cboConverterType.SelectedIndex = -1;
            }
            try
            {
                cboConvertMethod.SelectedIndex = AppConfig.ConvertMethod;
            }
            catch (ArgumentOutOfRangeException)
            {
                cboConvertMethod.SelectedIndex = 0;
            }
            try
            {
                cboSaveEncoding.SelectedIndex = AppConfig.SaveEncoding;
            }
            catch (ArgumentOutOfRangeException)
            {
                cboSaveEncoding.SelectedIndex = 0;
            }
            chkIsConvertFileName.Checked = AppConfig.IsConvertFileName;
            chkIsExclude.Checked = AppConfig.IsExclude;
            txtFileExt.Text = AppConfig.FileExt;
            chkIsTopMost.Checked = AppConfig.IsTopMost;
            cboReadEncoding.SelectedIndex = 0;
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
            Tag = Size;
            ReadConverterMapsList();
            ReadDefaultRegexList();
            ReadConfig();
            if (Environment.GetCommandLineArgs().Length > 1)
            {
                if (ReadSourceFile(Environment.GetCommandLineArgs()[1]))
                {
                    ConvertSourceToDestText();
                    txtSource.Select(0, 0);
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
            if (ReadSourceFile(((string[])e.Data.GetData(DataFormats.FileDrop))[0]))
            {
                ConvertSourceToDestText();
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
                case (int)Keys.R:
                    if (e.Control)
                    {
                        if (btnReload.Enabled) btnReload_Click(null, null);
                    }
                    break;
                case (int)Keys.T:
                    if (e.Control)
                    {
                        if (btnConvert.Enabled) btnConvert_Click(null, null);
                    }
                    break;
                case (int)Keys.D:
                    if (e.Control)
                    {
                        if (btnDestToSource.Enabled) btnDestToSource_Click(null, null);
                    }
                    break;
            }
        }

        /// <summary>
        /// 当在窗口调整大小时发生
        /// </summary>
        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (!(Tag is Size)) return;
            Size size = Size - (Size)Tag;
            size.Width /= 2;
            grpSource.Size += size;
            grpDest.Location = new Point(grpDest.Location.X + size.Width, grpDest.Location.Y);
            grpDest.Size += size;
            btnDestToSource.Location = new Point(btnDestToSource.Location.X + size.Width, btnDestToSource.Location.Y + size.Height / 2);
            btnConvert.Location = new Point(btnConvert.Location.X + size.Width, btnConvert.Location.Y + size.Height / 2);
            Tag = Size;
        }

        /// <summary>
        /// 当单击重新载入按钮时发生
        /// </summary>
        private void btnReload_Click(object sender, EventArgs e)
        {
            if (ReadSourceFile(SourceFilePath))
            {
                btnConvert_Click(null, null);
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
            AppConfig.FileExt = txtFileExt.Text.Trim();
        }

        /// <summary>
        /// 当单击转换按钮时发生
        /// </summary>
        private void btnConvert_Click(object sender, EventArgs e)
        {
            if (cboConvertMethod.SelectedIndex == (int)ConvertMethod.ConvertFile && SourceFilePath == String.Empty)
                cboConvertMethod.SelectedIndex = (int)ConvertMethod.ConvertText;
            ConvertSourceToDestText();
        }

        /// <summary>
        /// 当单击转换文本To源文本按钮时发生
        /// </summary>
        private void btnDestToSource_Click(object sender, EventArgs e)
        {
            txtSource.Text = txtDest.Text;
            btnConvert_Click(null, null);
        }

        /// <summary>
        /// 当转换类型下拉列表选择项改变时发生
        /// </summary>
        private void cboConverterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboConverterType.SelectedIndex == -1 ||
                Tools.CheckControlDataSourceIsPreparing(cboConverterType.Tag))
            {
                return;
            }
            ConvertSourceToDestText();
            AppConfig.ConvertType = cboConverterType.SelectedIndex;
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
                chkIsConvertFileName.Enabled = false;
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
                chkIsConvertFileName.Enabled = true;
            }
            AppConfig.ConvertMethod = cboConvertMethod.SelectedIndex;
        }

        /// <summary>
        /// 当是否转换文件名复选框选中状态改变时发生
        /// </summary>
        private void chkIsConvertFileName_CheckedChanged(object sender, EventArgs e)
        {
            AppConfig.IsConvertFileName = chkIsConvertFileName.Checked;
        }

        /// <summary>
        /// 当保存编码下拉列表选择项改变时发生
        /// </summary>
        private void cboSaveEncoding_SelectedIndexChanged(object sender, EventArgs e)
        {
            AppConfig.SaveEncoding = cboSaveEncoding.SelectedIndex;
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
        /// 当单击加入正则表达式按钮时发生
        /// </summary>
        private void btnRegexAdd_Click(object sender, EventArgs e)
        {
            string regex = cboRegex.Text.Trim();
            if (regex == String.Empty) return;
            cboRegex.Items.Add(regex);
            string path = Tools.AppDirPath + DefaultRegexListFileName;
            if (!File.Exists(path)) return;
            try
            {
                string content = File.ReadAllText(path, Encoding.UTF8);
                if (!content.EndsWith("\r\n")) content += "\r\n";
                content += regex + "\r\n";
                File.WriteAllText(path, content, Encoding.UTF8);
            }
            catch (Exception)
            {
                ShowErrorMsg("预设正则表达式列表文件写入失败！");
                return;
            }
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
            AppConfig.IsExclude = chkIsExclude.Checked;
        }

        /// <summary>
        /// 当总在最前复选框选中状态改变时发生
        /// </summary>
        private void chkIsTopMost_CheckedChanged(object sender, EventArgs e)
        {
            TopMost = chkIsTopMost.Checked;
            AppConfig.IsTopMost = chkIsTopMost.Checked;
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
                if (ReadSourceFile(SourceFilePath)) btnConvert_Click(null, null);
            }
        }

        /// <summary>
        /// 当字符对照表下拉列表选择项改变时发生
        /// </summary>
        private void cboConverterMapsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboConverterMapsList.SelectedIndex == -1 ||
                Tools.CheckControlDataSourceIsPreparing(cboConverterMapsList.Tag))
            {
                return;
            }
            ConverterMaps converterMaps = (ConverterMaps)cboConverterMapsList.SelectedItem;
            cboConverterType.Tag = true;
            BindingSource bs = new BindingSource();
            bs.DataSource = converterMaps.Converters.Values;
            cboConverterType.DataSource = bs;
            cboConverterType.DisplayMember = "Name";
            cboConverterType.ValueMember = "Id";
            cboConverterType.SelectedIndex = 0;
            cboConverterType.Tag = false;
            cboConverterType_SelectedIndexChanged(null, null);
            AppConfig.ConverterMapsPath = cboConverterMapsList.SelectedValue.ToString();
        }

        #region 主菜单事件

        /// <summary>
        /// 当单击打开菜单项时发生
        /// </summary>
        private void tsmiOpenFile_Click(object sender, EventArgs e)
        {
            if (ofdOpenFile.ShowDialog() == DialogResult.OK)
            {
                if (!ReadSourceFile(ofdOpenFile.FileName))
                {
                    ShowErrorMsg("文件读取失败！");
                    return;
                }
                else
                {
                    ConvertSourceToDestText();
                }
            }
        }

        /// <summary>
        /// 当单击保存结果菜单项时发生
        /// </summary>
        private void tsmiSaveResult_Click(object sender, EventArgs e)
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
                File.Delete(AppConfig.ConfigPath);
            }
            catch (Exception) { }
            ReadConfig();
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

namespace ChineseConverter
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.grpConvertMethod = new System.Windows.Forms.GroupBox();
            this.chkIsConvertFileName = new System.Windows.Forms.CheckBox();
            this.cboConvertMethod = new System.Windows.Forms.ComboBox();
            this.grpConvertType = new System.Windows.Forms.GroupBox();
            this.cboConverterType = new System.Windows.Forms.ComboBox();
            this.lblFilePath = new System.Windows.Forms.Label();
            this.txtFileExt = new System.Windows.Forms.TextBox();
            this.lblFileExt = new System.Windows.Forms.Label();
            this.btnSaveAs = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnConvert = new System.Windows.Forms.Button();
            this.grpDest = new System.Windows.Forms.GroupBox();
            this.txtDest = new System.Windows.Forms.TextBox();
            this.grpSource = new System.Windows.Forms.GroupBox();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.tsmiFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSaveResult = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRestoreConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.ofdOpenFile = new System.Windows.Forms.OpenFileDialog();
            this.sfdSaveFile = new System.Windows.Forms.SaveFileDialog();
            this.grpSaveEncoding = new System.Windows.Forms.GroupBox();
            this.cboSaveEncoding = new System.Windows.Forms.ComboBox();
            this.chkIsTopMost = new System.Windows.Forms.CheckBox();
            this.chkIsExclude = new System.Windows.Forms.CheckBox();
            this.grpRegex = new System.Windows.Forms.GroupBox();
            this.btnRegexAdd = new System.Windows.Forms.Button();
            this.btnRegexClear = new System.Windows.Forms.Button();
            this.cboRegex = new System.Windows.Forms.ComboBox();
            this.btnRegexHelp = new System.Windows.Forms.Button();
            this.ttpCommon = new System.Windows.Forms.ToolTip(this.components);
            this.cboReadEncoding = new System.Windows.Forms.ComboBox();
            this.btnDestToSource = new System.Windows.Forms.Button();
            this.btnReload = new System.Windows.Forms.Button();
            this.cboConverterMapsList = new System.Windows.Forms.ComboBox();
            this.lblFileEncoding = new System.Windows.Forms.Label();
            this.grpReadEncoding = new System.Windows.Forms.GroupBox();
            this.grpConvertMaps = new System.Windows.Forms.GroupBox();
            this.grpConvertMethod.SuspendLayout();
            this.grpConvertType.SuspendLayout();
            this.grpDest.SuspendLayout();
            this.grpSource.SuspendLayout();
            this.menuMain.SuspendLayout();
            this.grpSaveEncoding.SuspendLayout();
            this.grpRegex.SuspendLayout();
            this.grpReadEncoding.SuspendLayout();
            this.grpConvertMaps.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpConvertMethod
            // 
            this.grpConvertMethod.Controls.Add(this.chkIsConvertFileName);
            this.grpConvertMethod.Controls.Add(this.cboConvertMethod);
            this.grpConvertMethod.Location = new System.Drawing.Point(315, 28);
            this.grpConvertMethod.Name = "grpConvertMethod";
            this.grpConvertMethod.Size = new System.Drawing.Size(178, 44);
            this.grpConvertMethod.TabIndex = 9;
            this.grpConvertMethod.TabStop = false;
            this.grpConvertMethod.Text = "转换方式";
            // 
            // chkIsConvertFileName
            // 
            this.chkIsConvertFileName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkIsConvertFileName.AutoSize = true;
            this.chkIsConvertFileName.Location = new System.Drawing.Point(88, 19);
            this.chkIsConvertFileName.Name = "chkIsConvertFileName";
            this.chkIsConvertFileName.Size = new System.Drawing.Size(84, 16);
            this.chkIsConvertFileName.TabIndex = 1;
            this.chkIsConvertFileName.Text = "转换文件名";
            this.ttpCommon.SetToolTip(this.chkIsConvertFileName, "设置是否同时对文件名进行字符转换");
            this.chkIsConvertFileName.UseVisualStyleBackColor = true;
            this.chkIsConvertFileName.CheckedChanged += new System.EventHandler(this.chkIsConvertFileName_CheckedChanged);
            // 
            // cboConvertMethod
            // 
            this.cboConvertMethod.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboConvertMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboConvertMethod.FormattingEnabled = true;
            this.cboConvertMethod.Items.AddRange(new object[] {
            "转换文档",
            "转换文本"});
            this.cboConvertMethod.Location = new System.Drawing.Point(6, 17);
            this.cboConvertMethod.Name = "cboConvertMethod";
            this.cboConvertMethod.Size = new System.Drawing.Size(76, 20);
            this.cboConvertMethod.TabIndex = 0;
            this.ttpCommon.SetToolTip(this.cboConvertMethod, "设置转换方式");
            this.cboConvertMethod.SelectedIndexChanged += new System.EventHandler(this.cboConvertMethod_SelectedIndexChanged);
            // 
            // grpConvertType
            // 
            this.grpConvertType.Controls.Add(this.cboConverterType);
            this.grpConvertType.Location = new System.Drawing.Point(12, 28);
            this.grpConvertType.Name = "grpConvertType";
            this.grpConvertType.Size = new System.Drawing.Size(136, 44);
            this.grpConvertType.TabIndex = 7;
            this.grpConvertType.TabStop = false;
            this.grpConvertType.Text = "转换类型";
            // 
            // cboConverterType
            // 
            this.cboConverterType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboConverterType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboConverterType.FormattingEnabled = true;
            this.cboConverterType.Location = new System.Drawing.Point(6, 17);
            this.cboConverterType.Name = "cboConverterType";
            this.cboConverterType.Size = new System.Drawing.Size(124, 20);
            this.cboConverterType.TabIndex = 0;
            this.ttpCommon.SetToolTip(this.cboConverterType, "设置转换类型");
            this.cboConverterType.SelectedIndexChanged += new System.EventHandler(this.cboConverterType_SelectedIndexChanged);
            // 
            // lblFilePath
            // 
            this.lblFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFilePath.AutoEllipsis = true;
            this.lblFilePath.Location = new System.Drawing.Point(12, 582);
            this.lblFilePath.Name = "lblFilePath";
            this.lblFilePath.Size = new System.Drawing.Size(761, 12);
            this.lblFilePath.TabIndex = 4;
            this.lblFilePath.UseMnemonic = false;
            // 
            // txtFileExt
            // 
            this.txtFileExt.Location = new System.Drawing.Point(693, 95);
            this.txtFileExt.MaxLength = 255;
            this.txtFileExt.Name = "txtFileExt";
            this.txtFileExt.Size = new System.Drawing.Size(80, 21);
            this.txtFileExt.TabIndex = 17;
            this.ttpCommon.SetToolTip(this.txtFileExt, "另存为时，在原文件名和扩展名之间添加指定的后缀名");
            // 
            // lblFileExt
            // 
            this.lblFileExt.AutoSize = true;
            this.lblFileExt.Location = new System.Drawing.Point(634, 98);
            this.lblFileExt.Name = "lblFileExt";
            this.lblFileExt.Size = new System.Drawing.Size(53, 12);
            this.lblFileExt.TabIndex = 16;
            this.lblFileExt.Text = "后缀名：";
            this.ttpCommon.SetToolTip(this.lblFileExt, "另存为时，在原文件名和扩展名之间添加指定的后缀名");
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.Location = new System.Drawing.Point(545, 92);
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new System.Drawing.Size(83, 25);
            this.btnSaveAs.TabIndex = 15;
            this.btnSaveAs.Text = "另存为";
            this.ttpCommon.SetToolTip(this.btnSaveAs, "快捷键：Ctrl+Shift+S");
            this.btnSaveAs.UseVisualStyleBackColor = true;
            this.btnSaveAs.Click += new System.EventHandler(this.btnSaveAs_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(456, 92);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(83, 25);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "保存(覆盖)";
            this.ttpCommon.SetToolTip(this.btnSave, "快捷键：Ctrl+S");
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnConvert
            // 
            this.btnConvert.Location = new System.Drawing.Point(378, 347);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(29, 45);
            this.btnConvert.TabIndex = 3;
            this.btnConvert.Text = ">>";
            this.ttpCommon.SetToolTip(this.btnConvert, "转换文本，快捷键：Ctrl+T");
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // grpDest
            // 
            this.grpDest.Controls.Add(this.txtDest);
            this.grpDest.Location = new System.Drawing.Point(413, 128);
            this.grpDest.Name = "grpDest";
            this.grpDest.Size = new System.Drawing.Size(360, 447);
            this.grpDest.TabIndex = 1;
            this.grpDest.TabStop = false;
            this.grpDest.Text = "转换文本";
            // 
            // txtDest
            // 
            this.txtDest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDest.Location = new System.Drawing.Point(3, 17);
            this.txtDest.MaxLength = 0;
            this.txtDest.Multiline = true;
            this.txtDest.Name = "txtDest";
            this.txtDest.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtDest.Size = new System.Drawing.Size(354, 427);
            this.txtDest.TabIndex = 0;
            this.txtDest.WordWrap = false;
            this.txtDest.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtContent_KeyDown);
            // 
            // grpSource
            // 
            this.grpSource.Controls.Add(this.txtSource);
            this.grpSource.Location = new System.Drawing.Point(12, 128);
            this.grpSource.Name = "grpSource";
            this.grpSource.Size = new System.Drawing.Size(360, 447);
            this.grpSource.TabIndex = 0;
            this.grpSource.TabStop = false;
            this.grpSource.Text = "源文本";
            // 
            // txtSource
            // 
            this.txtSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSource.Location = new System.Drawing.Point(3, 17);
            this.txtSource.MaxLength = 0;
            this.txtSource.Multiline = true;
            this.txtSource.Name = "txtSource";
            this.txtSource.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSource.Size = new System.Drawing.Size(354, 427);
            this.txtSource.TabIndex = 0;
            this.txtSource.WordWrap = false;
            this.txtSource.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtContent_KeyDown);
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFile,
            this.tsmiHelp});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(785, 25);
            this.menuMain.TabIndex = 0;
            this.menuMain.Text = "menuStrip1";
            // 
            // tsmiFile
            // 
            this.tsmiFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOpenFile,
            this.tsmiSaveResult,
            this.tsmiRestoreConfig,
            this.tsmiQuit});
            this.tsmiFile.Name = "tsmiFile";
            this.tsmiFile.Size = new System.Drawing.Size(58, 21);
            this.tsmiFile.Text = "文件(&F)";
            // 
            // tsmiOpenFile
            // 
            this.tsmiOpenFile.Name = "tsmiOpenFile";
            this.tsmiOpenFile.Size = new System.Drawing.Size(164, 22);
            this.tsmiOpenFile.Text = "打开(&O)";
            this.tsmiOpenFile.Click += new System.EventHandler(this.tsmiOpenFile_Click);
            // 
            // tsmiSaveResult
            // 
            this.tsmiSaveResult.Name = "tsmiSaveResult";
            this.tsmiSaveResult.Size = new System.Drawing.Size(164, 22);
            this.tsmiSaveResult.Text = "保存结果(&S)";
            this.tsmiSaveResult.Click += new System.EventHandler(this.tsmiSaveResult_Click);
            // 
            // tsmiRestoreConfig
            // 
            this.tsmiRestoreConfig.Name = "tsmiRestoreConfig";
            this.tsmiRestoreConfig.Size = new System.Drawing.Size(164, 22);
            this.tsmiRestoreConfig.Text = "恢复默认设置(&R)";
            this.tsmiRestoreConfig.Click += new System.EventHandler(this.tsmiRestoreConfig_Click);
            // 
            // tsmiQuit
            // 
            this.tsmiQuit.Name = "tsmiQuit";
            this.tsmiQuit.Size = new System.Drawing.Size(164, 22);
            this.tsmiQuit.Text = "退出(&Q)";
            this.tsmiQuit.Click += new System.EventHandler(this.tsmiQuit_Click);
            // 
            // tsmiHelp
            // 
            this.tsmiHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAbout});
            this.tsmiHelp.Name = "tsmiHelp";
            this.tsmiHelp.Size = new System.Drawing.Size(61, 21);
            this.tsmiHelp.Text = "帮助(&H)";
            // 
            // tsmiAbout
            // 
            this.tsmiAbout.Name = "tsmiAbout";
            this.tsmiAbout.Size = new System.Drawing.Size(116, 22);
            this.tsmiAbout.Text = "关于(&A)";
            this.tsmiAbout.Click += new System.EventHandler(this.tsmiAbout_Click);
            // 
            // ofdOpenFile
            // 
            this.ofdOpenFile.Filter = "文本文件|*.txt;*.log;*.lrc;*.cue|所有文件|*.*";
            // 
            // sfdSaveFile
            // 
            this.sfdSaveFile.Filter = "文本文件|*.txt;*.log;*.lrc;*.cue|所有文件|*.*";
            // 
            // grpSaveEncoding
            // 
            this.grpSaveEncoding.Controls.Add(this.cboSaveEncoding);
            this.grpSaveEncoding.Location = new System.Drawing.Point(140, 78);
            this.grpSaveEncoding.Name = "grpSaveEncoding";
            this.grpSaveEncoding.Size = new System.Drawing.Size(122, 44);
            this.grpSaveEncoding.TabIndex = 12;
            this.grpSaveEncoding.TabStop = false;
            this.grpSaveEncoding.Text = "保存编码";
            // 
            // cboSaveEncoding
            // 
            this.cboSaveEncoding.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSaveEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSaveEncoding.FormattingEnabled = true;
            this.cboSaveEncoding.Items.AddRange(new object[] {
            "UTF-8",
            "Unicode",
            "GBK",
            "BIG5",
            "Default"});
            this.cboSaveEncoding.Location = new System.Drawing.Point(6, 17);
            this.cboSaveEncoding.Name = "cboSaveEncoding";
            this.cboSaveEncoding.Size = new System.Drawing.Size(110, 20);
            this.cboSaveEncoding.TabIndex = 0;
            this.ttpCommon.SetToolTip(this.cboSaveEncoding, "设置保存文件时使用的编码 (建议使用UTF-8编码)");
            this.cboSaveEncoding.SelectedIndexChanged += new System.EventHandler(this.cboSaveEncoding_SelectedIndexChanged);
            // 
            // chkIsTopMost
            // 
            this.chkIsTopMost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkIsTopMost.AutoSize = true;
            this.chkIsTopMost.Location = new System.Drawing.Point(701, 601);
            this.chkIsTopMost.Name = "chkIsTopMost";
            this.chkIsTopMost.Size = new System.Drawing.Size(72, 16);
            this.chkIsTopMost.TabIndex = 6;
            this.chkIsTopMost.Text = "总在最前";
            this.ttpCommon.SetToolTip(this.chkIsTopMost, "窗口是否保持最前");
            this.chkIsTopMost.UseVisualStyleBackColor = true;
            this.chkIsTopMost.CheckedChanged += new System.EventHandler(this.chkIsTopMost_CheckedChanged);
            // 
            // chkIsExclude
            // 
            this.chkIsExclude.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkIsExclude.AutoSize = true;
            this.chkIsExclude.Location = new System.Drawing.Point(220, 19);
            this.chkIsExclude.Name = "chkIsExclude";
            this.chkIsExclude.Size = new System.Drawing.Size(48, 16);
            this.chkIsExclude.TabIndex = 4;
            this.chkIsExclude.Text = "排除";
            this.ttpCommon.SetToolTip(this.chkIsExclude, "设置符合指定模式的字符串是否不参与字符转换");
            this.chkIsExclude.UseVisualStyleBackColor = true;
            this.chkIsExclude.CheckedChanged += new System.EventHandler(this.chkIsExclude_CheckedChanged);
            // 
            // grpRegex
            // 
            this.grpRegex.Controls.Add(this.btnRegexAdd);
            this.grpRegex.Controls.Add(this.btnRegexClear);
            this.grpRegex.Controls.Add(this.cboRegex);
            this.grpRegex.Controls.Add(this.btnRegexHelp);
            this.grpRegex.Controls.Add(this.chkIsExclude);
            this.grpRegex.Location = new System.Drawing.Point(499, 28);
            this.grpRegex.Name = "grpRegex";
            this.grpRegex.Size = new System.Drawing.Size(274, 44);
            this.grpRegex.TabIndex = 10;
            this.grpRegex.TabStop = false;
            this.grpRegex.Text = "正则表达式";
            // 
            // btnRegexAdd
            // 
            this.btnRegexAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRegexAdd.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegexAdd.Location = new System.Drawing.Point(166, 15);
            this.btnRegexAdd.Name = "btnRegexAdd";
            this.btnRegexAdd.Size = new System.Drawing.Size(23, 23);
            this.btnRegexAdd.TabIndex = 2;
            this.btnRegexAdd.Text = "+";
            this.ttpCommon.SetToolTip(this.btnRegexAdd, "添加当前正则表达式到预设列表中");
            this.btnRegexAdd.UseVisualStyleBackColor = true;
            this.btnRegexAdd.Click += new System.EventHandler(this.btnRegexAdd_Click);
            // 
            // btnRegexClear
            // 
            this.btnRegexClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRegexClear.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegexClear.Location = new System.Drawing.Point(141, 15);
            this.btnRegexClear.Name = "btnRegexClear";
            this.btnRegexClear.Size = new System.Drawing.Size(23, 23);
            this.btnRegexClear.TabIndex = 1;
            this.btnRegexClear.Text = "×";
            this.ttpCommon.SetToolTip(this.btnRegexClear, "清除正则表达式文本");
            this.btnRegexClear.UseVisualStyleBackColor = true;
            this.btnRegexClear.Click += new System.EventHandler(this.btnRegexClear_Click);
            // 
            // cboRegex
            // 
            this.cboRegex.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboRegex.FormattingEnabled = true;
            this.cboRegex.Location = new System.Drawing.Point(6, 17);
            this.cboRegex.Name = "cboRegex";
            this.cboRegex.Size = new System.Drawing.Size(129, 20);
            this.cboRegex.TabIndex = 0;
            this.ttpCommon.SetToolTip(this.cboRegex, "使用正则表达式，可在符合指定模式的字符串中(或之外)进行字符转换");
            this.cboRegex.SelectedIndexChanged += new System.EventHandler(this.cboRegex_SelectedIndexChanged);
            this.cboRegex.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cboRegex_KeyDown);
            // 
            // btnRegexHelp
            // 
            this.btnRegexHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRegexHelp.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegexHelp.Location = new System.Drawing.Point(191, 15);
            this.btnRegexHelp.Name = "btnRegexHelp";
            this.btnRegexHelp.Size = new System.Drawing.Size(23, 23);
            this.btnRegexHelp.TabIndex = 3;
            this.btnRegexHelp.Text = "?";
            this.ttpCommon.SetToolTip(this.btnRegexHelp, "查看正则表达式帮助");
            this.btnRegexHelp.UseVisualStyleBackColor = true;
            this.btnRegexHelp.Click += new System.EventHandler(this.btnRegexHelp_Click);
            // 
            // cboReadEncoding
            // 
            this.cboReadEncoding.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboReadEncoding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboReadEncoding.FormattingEnabled = true;
            this.cboReadEncoding.Items.AddRange(new object[] {
            "自动选择",
            "UTF-8",
            "Unicode",
            "GBK",
            "BIG5",
            "HZ",
            "Shift-JIS",
            "EUC-KR(Korean)",
            "Default"});
            this.cboReadEncoding.Location = new System.Drawing.Point(6, 17);
            this.cboReadEncoding.Name = "cboReadEncoding";
            this.cboReadEncoding.Size = new System.Drawing.Size(110, 20);
            this.cboReadEncoding.TabIndex = 0;
            this.ttpCommon.SetToolTip(this.cboReadEncoding, "设置读取文件时使用的编码 (自动选择可识别前四种编码)");
            this.cboReadEncoding.SelectedIndexChanged += new System.EventHandler(this.cboReadEncoding_SelectedIndexChanged);
            // 
            // btnDestToSource
            // 
            this.btnDestToSource.Location = new System.Drawing.Point(378, 296);
            this.btnDestToSource.Name = "btnDestToSource";
            this.btnDestToSource.Size = new System.Drawing.Size(29, 45);
            this.btnDestToSource.TabIndex = 2;
            this.btnDestToSource.Text = "<<";
            this.ttpCommon.SetToolTip(this.btnDestToSource, "将转换文本放入源文本中，快捷键：Ctrl+D");
            this.btnDestToSource.UseVisualStyleBackColor = true;
            this.btnDestToSource.Click += new System.EventHandler(this.btnDestToSource_Click);
            // 
            // btnReload
            // 
            this.btnReload.Location = new System.Drawing.Point(268, 92);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(75, 25);
            this.btnReload.TabIndex = 13;
            this.btnReload.Text = "重新载入";
            this.ttpCommon.SetToolTip(this.btnReload, "重新载入当前文件内容，快捷键：Ctrl+R");
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // cboConverterMapsList
            // 
            this.cboConverterMapsList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboConverterMapsList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboConverterMapsList.FormattingEnabled = true;
            this.cboConverterMapsList.Location = new System.Drawing.Point(6, 17);
            this.cboConverterMapsList.Name = "cboConverterMapsList";
            this.cboConverterMapsList.Size = new System.Drawing.Size(143, 20);
            this.cboConverterMapsList.TabIndex = 0;
            this.ttpCommon.SetToolTip(this.cboConverterMapsList, "选择字符对照表");
            this.cboConverterMapsList.SelectedIndexChanged += new System.EventHandler(this.cboConverterMapsList_SelectedIndexChanged);
            // 
            // lblFileEncoding
            // 
            this.lblFileEncoding.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblFileEncoding.Location = new System.Drawing.Point(12, 602);
            this.lblFileEncoding.Name = "lblFileEncoding";
            this.lblFileEncoding.Size = new System.Drawing.Size(360, 12);
            this.lblFileEncoding.TabIndex = 5;
            this.lblFileEncoding.UseMnemonic = false;
            // 
            // grpReadEncoding
            // 
            this.grpReadEncoding.Controls.Add(this.cboReadEncoding);
            this.grpReadEncoding.Location = new System.Drawing.Point(12, 78);
            this.grpReadEncoding.Name = "grpReadEncoding";
            this.grpReadEncoding.Size = new System.Drawing.Size(122, 44);
            this.grpReadEncoding.TabIndex = 11;
            this.grpReadEncoding.TabStop = false;
            this.grpReadEncoding.Text = "读取编码";
            // 
            // grpConvertMaps
            // 
            this.grpConvertMaps.Controls.Add(this.cboConverterMapsList);
            this.grpConvertMaps.Location = new System.Drawing.Point(154, 28);
            this.grpConvertMaps.Name = "grpConvertMaps";
            this.grpConvertMaps.Size = new System.Drawing.Size(155, 44);
            this.grpConvertMaps.TabIndex = 8;
            this.grpConvertMaps.TabStop = false;
            this.grpConvertMaps.Text = "字符对照表";
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 622);
            this.Controls.Add(this.btnDestToSource);
            this.Controls.Add(this.grpConvertMaps);
            this.Controls.Add(this.grpReadEncoding);
            this.Controls.Add(this.btnReload);
            this.Controls.Add(this.lblFileEncoding);
            this.Controls.Add(this.grpRegex);
            this.Controls.Add(this.chkIsTopMost);
            this.Controls.Add(this.grpSaveEncoding);
            this.Controls.Add(this.grpConvertMethod);
            this.Controls.Add(this.grpConvertType);
            this.Controls.Add(this.lblFilePath);
            this.Controls.Add(this.txtFileExt);
            this.Controls.Add(this.lblFileExt);
            this.Controls.Add(this.btnSaveAs);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.grpDest);
            this.Controls.Add(this.grpSource);
            this.Controls.Add(this.menuMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(801, 661);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "简繁转换器";
            this.ttpCommon.SetToolTip(this, "可将文件拖拽到窗口上");
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.grpConvertMethod.ResumeLayout(false);
            this.grpConvertMethod.PerformLayout();
            this.grpConvertType.ResumeLayout(false);
            this.grpDest.ResumeLayout(false);
            this.grpDest.PerformLayout();
            this.grpSource.ResumeLayout(false);
            this.grpSource.PerformLayout();
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.grpSaveEncoding.ResumeLayout(false);
            this.grpRegex.ResumeLayout(false);
            this.grpRegex.PerformLayout();
            this.grpReadEncoding.ResumeLayout(false);
            this.grpConvertMaps.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        /// <summary>转换方式组合框</summary>
        private System.Windows.Forms.GroupBox grpConvertMethod;
        /// <summary>转换类型组合框</summary>
        private System.Windows.Forms.GroupBox grpConvertType;
        /// <summary>文件路径标签</summary>
        private System.Windows.Forms.Label lblFilePath;
        /// <summary>文件后缀名文本框</summary>
        private System.Windows.Forms.TextBox txtFileExt;
        /// <summary>文件后缀名标签</summary>
        private System.Windows.Forms.Label lblFileExt;
        /// <summary>另存为按钮</summary>
        private System.Windows.Forms.Button btnSaveAs;
        /// <summary>保存按钮</summary>
        private System.Windows.Forms.Button btnSave;
        /// <summary>转换文本按钮</summary>
        private System.Windows.Forms.Button btnConvert;
        /// <summary>转换文本组合框</summary>
        private System.Windows.Forms.GroupBox grpDest;
        /// <summary>转换文本文本框</summary>
        private System.Windows.Forms.TextBox txtDest;
        /// <summary>源文本组合框</summary>
        private System.Windows.Forms.GroupBox grpSource;
        /// <summary>源文本文本框</summary>
        private System.Windows.Forms.TextBox txtSource;
        /// <summary>主菜单</summary>
        private System.Windows.Forms.MenuStrip menuMain;
        /// <summary>文件菜单项</summary>
        private System.Windows.Forms.ToolStripMenuItem tsmiFile;
        /// <summary>帮助菜单项</summary>
        private System.Windows.Forms.ToolStripMenuItem tsmiHelp;
        /// <summary>打开菜单项</summary>
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenFile;
        /// <summary>保存结果菜单项</summary>
        private System.Windows.Forms.ToolStripMenuItem tsmiSaveResult;
        /// <summary>退出菜单项</summary>
        private System.Windows.Forms.ToolStripMenuItem tsmiQuit;
        /// <summary>关于菜单项</summary>
        private System.Windows.Forms.ToolStripMenuItem tsmiAbout;
        /// <summary>打开文件对话框</summary>
        private System.Windows.Forms.OpenFileDialog ofdOpenFile;
        /// <summary>保存文件对话框</summary>
        private System.Windows.Forms.SaveFileDialog sfdSaveFile;
        /// <summary>保存编码组合框</summary>
        private System.Windows.Forms.GroupBox grpSaveEncoding;
        /// <summary>保存编码下拉列表</summary>
        private System.Windows.Forms.ComboBox cboSaveEncoding;
        /// <summary>是否总在最前复选框</summary>
        private System.Windows.Forms.CheckBox chkIsTopMost;
        /// <summary>是否排除复选框</summary>
        private System.Windows.Forms.CheckBox chkIsExclude;
        /// <summary>正则表达式组合框</summary>
        private System.Windows.Forms.GroupBox grpRegex;
        /// <summary>公共消息提醒控件</summary>
        private System.Windows.Forms.ToolTip ttpCommon;
        /// <summary>查看正则表达式帮助按钮</summary>
        private System.Windows.Forms.Button btnRegexHelp;
        /// <summary>正则表达式下拉列表</summary>
        private System.Windows.Forms.ComboBox cboRegex;
        /// <summary>文件编码标签</summary>
        private System.Windows.Forms.Label lblFileEncoding;
        /// <summary>读取编码下拉列表</summary>
        private System.Windows.Forms.ComboBox cboReadEncoding;
        /// <summary>恢复默认设置菜单项</summary>
        private System.Windows.Forms.ToolStripMenuItem tsmiRestoreConfig;
        /// <summary>重新载入按钮</summary>
        private System.Windows.Forms.Button btnReload;
        /// <summary>是否转换文件名复选框</summary>
        private System.Windows.Forms.CheckBox chkIsConvertFileName;
        /// <summary>转换方式下拉列表</summary>
        private System.Windows.Forms.ComboBox cboConvertMethod;
        /// <summary>转换类型下拉列表</summary>
        private System.Windows.Forms.ComboBox cboConverterType;
        /// <summary>清除正则表达式按钮</summary>
        private System.Windows.Forms.Button btnRegexClear;
        /// <summary>读取编码组合框</summary>
        private System.Windows.Forms.GroupBox grpReadEncoding;
        /// <summary>字符对照表组合框</summary>
        private System.Windows.Forms.GroupBox grpConvertMaps;
        /// <summary>字符对照表下拉列表</summary>
        private System.Windows.Forms.ComboBox cboConverterMapsList;
        /// <summary>转换文本To源文本按钮</summary>
        private System.Windows.Forms.Button btnDestToSource;
        private System.Windows.Forms.Button btnRegexAdd;
    }
}


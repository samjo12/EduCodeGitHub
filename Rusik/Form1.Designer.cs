
namespace Rusik
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            Prev_btn = new System.Windows.Forms.Button();
            Next_btn = new System.Windows.Forms.Button();
            nudRecord = new System.Windows.Forms.NumericUpDown();
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            File_tsmi = new System.Windows.Forms.ToolStripMenuItem();
            OpenFile_tsmi = new System.Windows.Forms.ToolStripMenuItem();
            OpenTranslatedFile_tsmi = new System.Windows.Forms.ToolStripMenuItem();
            CloseFilesClear_tsmi = new System.Windows.Forms.ToolStripMenuItem();
            SaveFile_tsmi = new System.Windows.Forms.ToolStripMenuItem();
            Quit_tsmi = new System.Windows.Forms.ToolStripMenuItem();
            About_tsmi = new System.Windows.Forms.ToolStripMenuItem();
            SignatureIN_tb = new System.Windows.Forms.TextBox();
            SignatureIN_lb = new System.Windows.Forms.Label();
            Records_lb = new System.Windows.Forms.Label();
            Translate_btn = new System.Windows.Forms.Button();
            progressBar1 = new System.Windows.Forms.ProgressBar();
            SignatureOUT_lb = new System.Windows.Forms.Label();
            SignatureOUT_tb = new System.Windows.Forms.TextBox();
            Start_btn = new System.Windows.Forms.Button();
            progressBar1_lb = new System.Windows.Forms.Label();
            comboBox1 = new System.Windows.Forms.ComboBox();
            comboBox2 = new System.Windows.Forms.ComboBox();
            BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            label1 = new System.Windows.Forms.Label();
            SourceFile_tb = new System.Windows.Forms.Label();
            TranslatedFile_tb = new System.Windows.Forms.Label();
            Delete_btn = new System.Windows.Forms.Button();
            Search_ts = new System.Windows.Forms.ToolStrip();
            toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            Search_tstb = new System.Windows.Forms.ToolStripTextBox();
            SourceSearch_tsb = new System.Windows.Forms.ToolStripButton();
            SourceFirst_tsb = new System.Windows.Forms.ToolStripButton();
            SourcePrev_tsb = new System.Windows.Forms.ToolStripButton();
            SourceNext_tsb = new System.Windows.Forms.ToolStripButton();
            SourceLast_tsb = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            SearchStat_tslb = new System.Windows.Forms.ToolStripLabel();
            TabClose_tsb = new System.Windows.Forms.ToolStripButton();
            miniToolStrip = new System.Windows.Forms.StatusStrip();
            toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            lbSource = new System.Windows.Forms.ToolStripStatusLabel();
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            lbTranslated = new System.Windows.Forms.ToolStripStatusLabel();
            statusStrip2 = new System.Windows.Forms.StatusStrip();
            Save = new System.Windows.Forms.Button();
            textBox1 = new System.Windows.Forms.TextBox();
            OffsetOUT_lb = new System.Windows.Forms.Label();
            OffsetIN_lb = new System.Windows.Forms.Label();
            textBox2 = new System.Windows.Forms.TextBox();
            OffsetMode512_rb = new System.Windows.Forms.RadioButton();
            OffsetModeInt32_rb = new System.Windows.Forms.RadioButton();
            SignatureModeString_rb = new System.Windows.Forms.RadioButton();
            SignatureModeHEX_rb = new System.Windows.Forms.RadioButton();
            OffsetgroupBox = new System.Windows.Forms.GroupBox();
            Signature_groupBox = new System.Windows.Forms.GroupBox();
            Records_groupBox = new System.Windows.Forms.GroupBox();
            groupBox1 = new System.Windows.Forms.GroupBox();
            ccpanel = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)nudRecord).BeginInit();
            menuStrip1.SuspendLayout();
            Search_ts.SuspendLayout();
            statusStrip1.SuspendLayout();
            statusStrip2.SuspendLayout();
            OffsetgroupBox.SuspendLayout();
            Signature_groupBox.SuspendLayout();
            Records_groupBox.SuspendLayout();
            groupBox1.SuspendLayout();
            ccpanel.SuspendLayout();
            SuspendLayout();
            // 
            // Prev_btn
            // 
            Prev_btn.BackColor = System.Drawing.Color.FromArgb(255, 192, 192);
            Prev_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            Prev_btn.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            Prev_btn.Location = new System.Drawing.Point(6, 19);
            Prev_btn.Name = "Prev_btn";
            Prev_btn.Size = new System.Drawing.Size(90, 72);
            Prev_btn.TabIndex = 3;
            Prev_btn.Text = "Prev";
            Prev_btn.UseVisualStyleBackColor = false;
            Prev_btn.Click += Prev_btn_Click;
            // 
            // Next_btn
            // 
            Next_btn.BackColor = System.Drawing.Color.FromArgb(255, 192, 192);
            Next_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            Next_btn.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            Next_btn.Location = new System.Drawing.Point(116, 18);
            Next_btn.Name = "Next_btn";
            Next_btn.Size = new System.Drawing.Size(90, 72);
            Next_btn.TabIndex = 4;
            Next_btn.Text = "Next";
            Next_btn.UseVisualStyleBackColor = false;
            Next_btn.Click += Next_btn_Click;
            // 
            // nudRecord
            // 
            nudRecord.BackColor = System.Drawing.SystemColors.Control;
            nudRecord.Location = new System.Drawing.Point(221, 18);
            nudRecord.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            nudRecord.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudRecord.Name = "nudRecord";
            nudRecord.ReadOnly = true;
            nudRecord.Size = new System.Drawing.Size(106, 23);
            nudRecord.TabIndex = 8;
            nudRecord.Value = new decimal(new int[] { 1, 0, 0, 0 });
            nudRecord.ValueChanged += nudRecord_ValueChanged;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { File_tsmi, About_tsmi });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new System.Drawing.Size(1008, 24);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // File_tsmi
            // 
            File_tsmi.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { OpenFile_tsmi, OpenTranslatedFile_tsmi, CloseFilesClear_tsmi, SaveFile_tsmi, Quit_tsmi });
            File_tsmi.Name = "File_tsmi";
            File_tsmi.Size = new System.Drawing.Size(37, 20);
            File_tsmi.Text = "File";
            // 
            // OpenFile_tsmi
            // 
            OpenFile_tsmi.Name = "OpenFile_tsmi";
            OpenFile_tsmi.Size = new System.Drawing.Size(204, 22);
            OpenFile_tsmi.Text = "Open Binary File";
            OpenFile_tsmi.Click += OpenFile_tsmi_Click;
            // 
            // OpenTranslatedFile_tsmi
            // 
            OpenTranslatedFile_tsmi.Name = "OpenTranslatedFile_tsmi";
            OpenTranslatedFile_tsmi.Size = new System.Drawing.Size(204, 22);
            OpenTranslatedFile_tsmi.Text = "Open Translated Text File";
            OpenTranslatedFile_tsmi.Click += OpenTranslatedFile_tsmi_Click;
            // 
            // CloseFilesClear_tsmi
            // 
            CloseFilesClear_tsmi.Name = "CloseFilesClear_tsmi";
            CloseFilesClear_tsmi.Size = new System.Drawing.Size(204, 22);
            CloseFilesClear_tsmi.Text = "Close Files/Clear";
            CloseFilesClear_tsmi.Click += CloseFilesClear_Click;
            // 
            // SaveFile_tsmi
            // 
            SaveFile_tsmi.Name = "SaveFile_tsmi";
            SaveFile_tsmi.Size = new System.Drawing.Size(204, 22);
            SaveFile_tsmi.Text = "Save File As...";
            SaveFile_tsmi.Click += SaveFile_tsmi_Click;
            // 
            // Quit_tsmi
            // 
            Quit_tsmi.Name = "Quit_tsmi";
            Quit_tsmi.Size = new System.Drawing.Size(204, 22);
            Quit_tsmi.Text = "Quit";
            Quit_tsmi.Click += Quit_tsmi_Click;
            // 
            // About_tsmi
            // 
            About_tsmi.Name = "About_tsmi";
            About_tsmi.Size = new System.Drawing.Size(52, 20);
            About_tsmi.Text = "About";
            About_tsmi.Click += About_tsmi_Click;
            // 
            // SignatureIN_tb
            // 
            SignatureIN_tb.Location = new System.Drawing.Point(45, 20);
            SignatureIN_tb.MaxLength = 14;
            SignatureIN_tb.Name = "SignatureIN_tb";
            SignatureIN_tb.PlaceholderText = "HEX number";
            SignatureIN_tb.ReadOnly = true;
            SignatureIN_tb.Size = new System.Drawing.Size(105, 23);
            SignatureIN_tb.TabIndex = 12;
            SignatureIN_tb.TextChanged += Signature_tb_TextChanged;
            SignatureIN_tb.KeyPress += Signature_tb_KeyPress;
            // 
            // SignatureIN_lb
            // 
            SignatureIN_lb.AutoSize = true;
            SignatureIN_lb.Location = new System.Drawing.Point(6, 23);
            SignatureIN_lb.Name = "SignatureIN_lb";
            SignatureIN_lb.Size = new System.Drawing.Size(19, 15);
            SignatureIN_lb.TabIndex = 13;
            SignatureIN_lb.Text = "IN";
            // 
            // Records_lb
            // 
            Records_lb.AutoSize = true;
            Records_lb.Location = new System.Drawing.Point(114, 0);
            Records_lb.Name = "Records_lb";
            Records_lb.Size = new System.Drawing.Size(95, 15);
            Records_lb.TabIndex = 14;
            Records_lb.Text = "Found: 0 records";
            // 
            // Translate_btn
            // 
            Translate_btn.BackColor = System.Drawing.Color.FromArgb(192, 255, 255);
            Translate_btn.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            Translate_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            Translate_btn.Font = new System.Drawing.Font("Segoe UI Semibold", 14.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            Translate_btn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            Translate_btn.Image = Properties.Resources.Google48;
            Translate_btn.Location = new System.Drawing.Point(152, 17);
            Translate_btn.Name = "Translate_btn";
            Translate_btn.Size = new System.Drawing.Size(72, 72);
            Translate_btn.TabIndex = 15;
            Translate_btn.UseVisualStyleBackColor = false;
            Translate_btn.Click += Translate_btn_Click;
            // 
            // progressBar1
            // 
            progressBar1.ForeColor = System.Drawing.Color.FromArgb(0, 192, 0);
            progressBar1.Location = new System.Drawing.Point(6, 114);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new System.Drawing.Size(930, 22);
            progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            progressBar1.TabIndex = 19;
            // 
            // SignatureOUT_lb
            // 
            SignatureOUT_lb.AutoSize = true;
            SignatureOUT_lb.Location = new System.Drawing.Point(6, 47);
            SignatureOUT_lb.Name = "SignatureOUT_lb";
            SignatureOUT_lb.Size = new System.Drawing.Size(30, 15);
            SignatureOUT_lb.TabIndex = 26;
            SignatureOUT_lb.Text = "OUT";
            // 
            // SignatureOUT_tb
            // 
            SignatureOUT_tb.Location = new System.Drawing.Point(44, 43);
            SignatureOUT_tb.MaxLength = 14;
            SignatureOUT_tb.Name = "SignatureOUT_tb";
            SignatureOUT_tb.PlaceholderText = "HEX number";
            SignatureOUT_tb.ReadOnly = true;
            SignatureOUT_tb.Size = new System.Drawing.Size(106, 23);
            SignatureOUT_tb.TabIndex = 27;
            SignatureOUT_tb.TextChanged += Signature_tb_TextChanged;
            SignatureOUT_tb.KeyPress += Signature_tb_KeyPress;
            // 
            // Start_btn
            // 
            Start_btn.BackColor = System.Drawing.SystemColors.ButtonFace;
            Start_btn.DialogResult = System.Windows.Forms.DialogResult.Retry;
            Start_btn.Font = new System.Drawing.Font("Segoe UI Black", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            Start_btn.Location = new System.Drawing.Point(109, 12);
            Start_btn.Name = "Start_btn";
            Start_btn.Size = new System.Drawing.Size(103, 55);
            Start_btn.TabIndex = 29;
            Start_btn.Text = "Search by signature";
            Start_btn.UseVisualStyleBackColor = false;
            Start_btn.Visible = false;
            Start_btn.Click += SearchBinaryBtn_Click;
            // 
            // progressBar1_lb
            // 
            progressBar1_lb.AccessibleRole = System.Windows.Forms.AccessibleRole.ProgressBar;
            progressBar1_lb.BackColor = System.Drawing.SystemColors.Control;
            progressBar1_lb.FlatStyle = System.Windows.Forms.FlatStyle.System;
            progressBar1_lb.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            progressBar1_lb.Location = new System.Drawing.Point(942, 115);
            progressBar1_lb.Name = "progressBar1_lb";
            progressBar1_lb.Size = new System.Drawing.Size(46, 20);
            progressBar1_lb.TabIndex = 30;
            progressBar1_lb.Text = "0 %";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "{ \"Afrikaans\",\"af\"},{ \"Albanian\",\"sq\"},{ \"Arabic\",\"ar\"},{ \"Armenian\",\"hy\"},{ \"Azerbaijani\",\"az\"},{ \"Basque\",\"eu\"},{ \"Belarusian\",\"be\"},", "{ \"Bulgarian\",\"bg\"},{ \"Catalan\",\"ca\"},{ \"Chinese(Simplified)\",\"zh-CN\"},{ \"Chinese(Traditional)\",\"zh-TW\"},{ \"Croatian\",\"hr\"},", "{ \"Czech\",\"cs\"},{ \"Danish\",\"da\"},{ \"Dutch\",\"nl\"},{ \"English\",\"en\"},{ \"Estonian\",\"et\"},{ \"Filipino\",\"tl\"},{ \"Finnish\",\"fi\"},", "{ \"French\",\"fr\"},{ \"Galician\",\"gl\"},{ \"Georgian\",\"ka\"},{ \"German\",\"de\"},{ \"Greek\",\"el\"},{ \"Haitian\",\"ht\"},{ \"Hebrew\",\"iw\"},", "{ \"Hindi\",\"hi\"},{ \"Hungarian\",\"hu\"},{ \"Icelandic\",\"is\"},{ \"Indonesian\",\"id\"},{ \"Irish\", \"ga\"},{ \"Italian\",\"it\"},{ \"Japanese\",\"ja\"},", "{ \"Korean\",\"ko\"},{ \"Latvian\",\"lv\"},{ \"Lithuanian\",\"lt\"},{ \"Macedonian\",\"mk\"},{ \"Malay\",\"ms\"},{ \"Maltese\",\"mt\"},{ \"Norwegian\",\"no\"},", "{ \"Persian\",\"fa\"},{ \"Polish\",\"pl\"},{ \"Portuguese\",\"pt\"},{ \"Romanian\",\"ro\"},{ \"Russian\",\"ru\"},{ \"Serbian\",\"sr\"},{ \"Slovak\",\"sk\"},", "{ \"Slovenian\",\"sl\"},{ \"Spanish\",\"es\"},{ \"Swahili\",\"sw\"},{ \"Swedish\",\"sv\"},{ \"Thai\",\"th\"},{ \"Turkish\",\"tr\"},{ \"Ukrainian\",\"uk\"},", "{ \"Urdu\",\"ur\"},{ \"Vietnamese\",\"vi\"},{ \"Welsh\",\"cy\"},{ \"Yiddish\",\"yi\"} " });
            comboBox1.Location = new System.Drawing.Point(6, 18);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new System.Drawing.Size(129, 23);
            comboBox1.TabIndex = 32;
            comboBox1.SelectionChangeCommitted += comboBox1_SelectedIndexChanged;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new System.Drawing.Point(6, 66);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new System.Drawing.Size(129, 23);
            comboBox2.TabIndex = 33;
            comboBox2.SelectionChangeCommitted += comboBox2_SelectedIndexChanged;
            // 
            // BottomToolStripPanel
            // 
            BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
            BottomToolStripPanel.Name = "BottomToolStripPanel";
            BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // TopToolStripPanel
            // 
            TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
            TopToolStripPanel.Name = "TopToolStripPanel";
            TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // RightToolStripPanel
            // 
            RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
            RightToolStripPanel.Name = "RightToolStripPanel";
            RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // LeftToolStripPanel
            // 
            LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
            LeftToolStripPanel.Name = "LeftToolStripPanel";
            LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // ContentPanel
            // 
            ContentPanel.Size = new System.Drawing.Size(150, 130);
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Wingdings 3", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label1.Location = new System.Drawing.Point(60, 47);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(28, 13);
            label1.TabIndex = 35;
            label1.Text = "->";
            // 
            // SourceFile_tb
            // 
            SourceFile_tb.AutoSize = true;
            SourceFile_tb.BackColor = System.Drawing.Color.ForestGreen;
            SourceFile_tb.Location = new System.Drawing.Point(7, 118);
            SourceFile_tb.Name = "SourceFile_tb";
            SourceFile_tb.Size = new System.Drawing.Size(0, 15);
            SourceFile_tb.TabIndex = 37;
            // 
            // TranslatedFile_tb
            // 
            TranslatedFile_tb.AutoSize = true;
            TranslatedFile_tb.BackColor = System.Drawing.Color.Lime;
            TranslatedFile_tb.Location = new System.Drawing.Point(10, 118);
            TranslatedFile_tb.Name = "TranslatedFile_tb";
            TranslatedFile_tb.Size = new System.Drawing.Size(0, 15);
            TranslatedFile_tb.TabIndex = 38;
            // 
            // Delete_btn
            // 
            Delete_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            Delete_btn.Image = Properties.Resources.Basket;
            Delete_btn.Location = new System.Drawing.Point(280, 43);
            Delete_btn.Name = "Delete_btn";
            Delete_btn.Size = new System.Drawing.Size(47, 47);
            Delete_btn.TabIndex = 39;
            Delete_btn.UseVisualStyleBackColor = true;
            Delete_btn.Click += Delete_btn_Click;
            // 
            // Search_ts
            // 
            Search_ts.BackColor = System.Drawing.SystemColors.Info;
            Search_ts.Dock = System.Windows.Forms.DockStyle.None;
            Search_ts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripLabel1, Search_tstb, SourceSearch_tsb, SourceFirst_tsb, SourcePrev_tsb, SourceNext_tsb, SourceLast_tsb, toolStripSeparator3, SearchStat_tslb, TabClose_tsb });
            Search_ts.Location = new System.Drawing.Point(12, 585);
            Search_ts.Name = "Search_ts";
            Search_ts.Size = new System.Drawing.Size(293, 25);
            Search_ts.TabIndex = 37;
            Search_ts.Text = "toolStrip1";
            Search_ts.Visible = false;
            // 
            // toolStripLabel1
            // 
            toolStripLabel1.Name = "toolStripLabel1";
            toolStripLabel1.Size = new System.Drawing.Size(0, 22);
            toolStripLabel1.ToolTipText = "Type your string for searching here...";
            // 
            // Search_tstb
            // 
            Search_tstb.AcceptsReturn = true;
            Search_tstb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            Search_tstb.Name = "Search_tstb";
            Search_tstb.ReadOnly = true;
            Search_tstb.Size = new System.Drawing.Size(250, 25);
            Search_tstb.ToolTipText = "Type text for seaching here...";
            Search_tstb.KeyDown += SourceSearch_tstb_KeyDown;
            // 
            // SourceSearch_tsb
            // 
            SourceSearch_tsb.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            SourceSearch_tsb.Image = Properties.Resources.Search;
            SourceSearch_tsb.ImageTransparentColor = System.Drawing.Color.Magenta;
            SourceSearch_tsb.Name = "SourceSearch_tsb";
            SourceSearch_tsb.Size = new System.Drawing.Size(23, 22);
            SourceSearch_tsb.Text = "Search";
            SourceSearch_tsb.ToolTipText = "Go search";
            SourceSearch_tsb.Click += NewTab_Click;
            // 
            // SourceFirst_tsb
            // 
            SourceFirst_tsb.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            SourceFirst_tsb.Image = Properties.Resources.ToFirst;
            SourceFirst_tsb.ImageTransparentColor = System.Drawing.Color.Magenta;
            SourceFirst_tsb.Name = "SourceFirst_tsb";
            SourceFirst_tsb.Size = new System.Drawing.Size(23, 22);
            SourceFirst_tsb.Text = "First";
            SourceFirst_tsb.Visible = false;
            SourceFirst_tsb.Click += SourceFirst_tsb_Click;
            // 
            // SourcePrev_tsb
            // 
            SourcePrev_tsb.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            SourcePrev_tsb.Image = Properties.Resources.Prev;
            SourcePrev_tsb.ImageTransparentColor = System.Drawing.Color.Magenta;
            SourcePrev_tsb.Name = "SourcePrev_tsb";
            SourcePrev_tsb.Size = new System.Drawing.Size(23, 22);
            SourcePrev_tsb.Text = "Previous";
            SourcePrev_tsb.Visible = false;
            SourcePrev_tsb.Click += Search_Prev_btn_Click;
            // 
            // SourceNext_tsb
            // 
            SourceNext_tsb.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            SourceNext_tsb.Image = Properties.Resources.Next;
            SourceNext_tsb.ImageTransparentColor = System.Drawing.Color.Magenta;
            SourceNext_tsb.Name = "SourceNext_tsb";
            SourceNext_tsb.Size = new System.Drawing.Size(23, 22);
            SourceNext_tsb.Text = "Next";
            SourceNext_tsb.Visible = false;
            SourceNext_tsb.Click += Search_Next_btn_Click;
            // 
            // SourceLast_tsb
            // 
            SourceLast_tsb.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            SourceLast_tsb.Image = Properties.Resources.ToLast;
            SourceLast_tsb.ImageTransparentColor = System.Drawing.Color.Magenta;
            SourceLast_tsb.Name = "SourceLast_tsb";
            SourceLast_tsb.Size = new System.Drawing.Size(23, 22);
            SourceLast_tsb.Text = "Last";
            SourceLast_tsb.Visible = false;
            SourceLast_tsb.Click += SourceLast_tsb_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // SearchStat_tslb
            // 
            SearchStat_tslb.Name = "SearchStat_tslb";
            SearchStat_tslb.Size = new System.Drawing.Size(0, 22);
            // 
            // TabClose_tsb
            // 
            TabClose_tsb.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            TabClose_tsb.Image = Properties.Resources.cross;
            TabClose_tsb.ImageTransparentColor = System.Drawing.Color.Magenta;
            TabClose_tsb.Name = "TabClose_tsb";
            TabClose_tsb.Size = new System.Drawing.Size(23, 22);
            TabClose_tsb.Text = "Close";
            TabClose_tsb.ToolTipText = "Close";
            TabClose_tsb.Visible = false;
            TabClose_tsb.Click += TabClose_tsb_Click;
            // 
            // miniToolStrip
            // 
            miniToolStrip.AccessibleName = "New item selection";
            miniToolStrip.AccessibleRole = System.Windows.Forms.AccessibleRole.ButtonDropDown;
            miniToolStrip.AutoSize = false;
            miniToolStrip.BackColor = System.Drawing.SystemColors.Info;
            miniToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            miniToolStrip.Location = new System.Drawing.Point(143, 1);
            miniToolStrip.Name = "miniToolStrip";
            miniToolStrip.Size = new System.Drawing.Size(159, 22);
            miniToolStrip.TabIndex = 19;
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            toolStripStatusLabel2.Size = new System.Drawing.Size(142, 17);
            toolStripStatusLabel2.Text = "Source message symbols:";
            // 
            // lbSource
            // 
            lbSource.Name = "lbSource";
            lbSource.Size = new System.Drawing.Size(0, 17);
            // 
            // statusStrip1
            // 
            statusStrip1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            statusStrip1.AutoSize = false;
            statusStrip1.BackColor = System.Drawing.SystemColors.Info;
            statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripStatusLabel1, lbTranslated });
            statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            statusStrip1.Location = new System.Drawing.Point(508, 548);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new System.Drawing.Size(207, 22);
            statusStrip1.TabIndex = 0;
            statusStrip1.Text = "statusStrip1";
            statusStrip1.Visible = false;
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new System.Drawing.Size(159, 15);
            toolStripStatusLabel1.Text = "Translated message symbols:";
            // 
            // lbTranslated
            // 
            lbTranslated.Name = "lbTranslated";
            lbTranslated.Size = new System.Drawing.Size(0, 0);
            // 
            // statusStrip2
            // 
            statusStrip2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            statusStrip2.BackColor = System.Drawing.SystemColors.Info;
            statusStrip2.Dock = System.Windows.Forms.DockStyle.None;
            statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripStatusLabel2, lbSource });
            statusStrip2.Location = new System.Drawing.Point(12, 548);
            statusStrip2.Name = "statusStrip2";
            statusStrip2.Size = new System.Drawing.Size(159, 22);
            statusStrip2.TabIndex = 19;
            statusStrip2.Text = "statusStrip2";
            statusStrip2.Visible = false;
            // 
            // Save
            // 
            Save.BackgroundImage = Properties.Resources.save;
            Save.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            Save.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Save.Location = new System.Drawing.Point(221, 44);
            Save.Name = "Save";
            Save.Size = new System.Drawing.Size(50, 47);
            Save.TabIndex = 40;
            Save.UseVisualStyleBackColor = true;
            Save.Click += Save_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new System.Drawing.Point(65, 42);
            textBox1.MaxLength = 14;
            textBox1.Name = "textBox1";
            textBox1.PlaceholderText = "HEX number";
            textBox1.ReadOnly = true;
            textBox1.Size = new System.Drawing.Size(41, 23);
            textBox1.TabIndex = 44;
            // 
            // OffsetOUT_lb
            // 
            OffsetOUT_lb.AutoSize = true;
            OffsetOUT_lb.Location = new System.Drawing.Point(6, 46);
            OffsetOUT_lb.Name = "OffsetOUT_lb";
            OffsetOUT_lb.Size = new System.Drawing.Size(62, 15);
            OffsetOUT_lb.TabIndex = 43;
            OffsetOUT_lb.Text = "OffsetOUT";
            // 
            // OffsetIN_lb
            // 
            OffsetIN_lb.AutoSize = true;
            OffsetIN_lb.Location = new System.Drawing.Point(6, 22);
            OffsetIN_lb.Name = "OffsetIN_lb";
            OffsetIN_lb.Size = new System.Drawing.Size(51, 15);
            OffsetIN_lb.TabIndex = 42;
            OffsetIN_lb.Text = "OffsetIN";
            // 
            // textBox2
            // 
            textBox2.Location = new System.Drawing.Point(65, 19);
            textBox2.MaxLength = 14;
            textBox2.Name = "textBox2";
            textBox2.PlaceholderText = "HEX number";
            textBox2.ReadOnly = true;
            textBox2.Size = new System.Drawing.Size(41, 23);
            textBox2.TabIndex = 41;
            // 
            // OffsetMode512_rb
            // 
            OffsetMode512_rb.AutoSize = true;
            OffsetMode512_rb.Location = new System.Drawing.Point(138, 72);
            OffsetMode512_rb.Name = "OffsetMode512_rb";
            OffsetMode512_rb.Size = new System.Drawing.Size(74, 19);
            OffsetMode512_rb.TabIndex = 45;
            OffsetMode512_rb.Text = "512 bytes";
            OffsetMode512_rb.UseVisualStyleBackColor = true;
            // 
            // OffsetModeInt32_rb
            // 
            OffsetModeInt32_rb.AutoSize = true;
            OffsetModeInt32_rb.Checked = true;
            OffsetModeInt32_rb.Location = new System.Drawing.Point(9, 72);
            OffsetModeInt32_rb.Name = "OffsetModeInt32_rb";
            OffsetModeInt32_rb.Size = new System.Drawing.Size(126, 19);
            OffsetModeInt32_rb.TabIndex = 46;
            OffsetModeInt32_rb.TabStop = true;
            OffsetModeInt32_rb.Text = "Int32 in from offset";
            OffsetModeInt32_rb.UseVisualStyleBackColor = true;
            // 
            // SignatureModeString_rb
            // 
            SignatureModeString_rb.AutoSize = true;
            SignatureModeString_rb.Location = new System.Drawing.Point(109, 72);
            SignatureModeString_rb.Name = "SignatureModeString_rb";
            SignatureModeString_rb.Size = new System.Drawing.Size(71, 19);
            SignatureModeString_rb.TabIndex = 47;
            SignatureModeString_rb.Text = "by string";
            SignatureModeString_rb.UseVisualStyleBackColor = true;
            SignatureModeString_rb.CheckedChanged += SignatureModeString_rb_CheckedChanged;
            // 
            // SignatureModeHEX_rb
            // 
            SignatureModeHEX_rb.AutoSize = true;
            SignatureModeHEX_rb.Checked = true;
            SignatureModeHEX_rb.Location = new System.Drawing.Point(6, 72);
            SignatureModeHEX_rb.Name = "SignatureModeHEX_rb";
            SignatureModeHEX_rb.Size = new System.Drawing.Size(92, 19);
            SignatureModeHEX_rb.TabIndex = 48;
            SignatureModeHEX_rb.TabStop = true;
            SignatureModeHEX_rb.Text = "by HEX code";
            SignatureModeHEX_rb.UseVisualStyleBackColor = true;
            SignatureModeHEX_rb.CheckedChanged += SignatureModeHEX_rb_CheckedChanged;
            // 
            // OffsetgroupBox
            // 
            OffsetgroupBox.Controls.Add(OffsetIN_lb);
            OffsetgroupBox.Controls.Add(Start_btn);
            OffsetgroupBox.Controls.Add(textBox2);
            OffsetgroupBox.Controls.Add(OffsetModeInt32_rb);
            OffsetgroupBox.Controls.Add(OffsetOUT_lb);
            OffsetgroupBox.Controls.Add(OffsetMode512_rb);
            OffsetgroupBox.Controls.Add(textBox1);
            OffsetgroupBox.Location = new System.Drawing.Point(197, 13);
            OffsetgroupBox.Name = "OffsetgroupBox";
            OffsetgroupBox.Size = new System.Drawing.Size(223, 95);
            OffsetgroupBox.TabIndex = 49;
            OffsetgroupBox.TabStop = false;
            OffsetgroupBox.Text = "Offset";
            // 
            // Signature_groupBox
            // 
            Signature_groupBox.Controls.Add(SignatureIN_lb);
            Signature_groupBox.Controls.Add(SignatureIN_tb);
            Signature_groupBox.Controls.Add(SignatureModeHEX_rb);
            Signature_groupBox.Controls.Add(SignatureOUT_lb);
            Signature_groupBox.Controls.Add(SignatureModeString_rb);
            Signature_groupBox.Controls.Add(SignatureOUT_tb);
            Signature_groupBox.Location = new System.Drawing.Point(6, 13);
            Signature_groupBox.Name = "Signature_groupBox";
            Signature_groupBox.Size = new System.Drawing.Size(185, 95);
            Signature_groupBox.TabIndex = 50;
            Signature_groupBox.TabStop = false;
            Signature_groupBox.Text = "Signatures";
            // 
            // Records_groupBox
            // 
            Records_groupBox.Controls.Add(Prev_btn);
            Records_groupBox.Controls.Add(Next_btn);
            Records_groupBox.Controls.Add(Delete_btn);
            Records_groupBox.Controls.Add(nudRecord);
            Records_groupBox.Controls.Add(Records_lb);
            Records_groupBox.Controls.Add(Save);
            Records_groupBox.Location = new System.Drawing.Point(426, 13);
            Records_groupBox.Name = "Records_groupBox";
            Records_groupBox.Size = new System.Drawing.Size(333, 95);
            Records_groupBox.TabIndex = 51;
            Records_groupBox.TabStop = false;
            Records_groupBox.Text = "Records controls";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(Translate_btn);
            groupBox1.Controls.Add(comboBox1);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(comboBox2);
            groupBox1.Location = new System.Drawing.Point(765, 13);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(231, 95);
            groupBox1.TabIndex = 52;
            groupBox1.TabStop = false;
            groupBox1.Text = "Translator";
            // 
            // ccpanel
            // 
            ccpanel.Controls.Add(Signature_groupBox);
            ccpanel.Controls.Add(groupBox1);
            ccpanel.Controls.Add(OffsetgroupBox);
            ccpanel.Controls.Add(Records_groupBox);
            ccpanel.Controls.Add(TranslatedFile_tb);
            ccpanel.Controls.Add(progressBar1);
            ccpanel.Controls.Add(SourceFile_tb);
            ccpanel.Controls.Add(progressBar1_lb);
            ccpanel.Location = new System.Drawing.Point(0, 588);
            ccpanel.Name = "ccpanel";
            ccpanel.Size = new System.Drawing.Size(1008, 141);
            ccpanel.TabIndex = 54;
            ccpanel.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1008, 729);
            Controls.Add(ccpanel);
            Controls.Add(statusStrip1);
            Controls.Add(statusStrip2);
            Controls.Add(Search_ts);
            Controls.Add(menuStrip1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Custom Translator v.0.8beta by Samjo";
            Load += Form1_Load;
            KeyDown += Form1_KeyDown;
            ((System.ComponentModel.ISupportInitialize)nudRecord).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            Search_ts.ResumeLayout(false);
            Search_ts.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            statusStrip2.ResumeLayout(false);
            statusStrip2.PerformLayout();
            OffsetgroupBox.ResumeLayout(false);
            OffsetgroupBox.PerformLayout();
            Signature_groupBox.ResumeLayout(false);
            Signature_groupBox.PerformLayout();
            Records_groupBox.ResumeLayout(false);
            Records_groupBox.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ccpanel.ResumeLayout(false);
            ccpanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Button Prev_btn;
        private System.Windows.Forms.Button Next_btn;
        private System.Windows.Forms.NumericUpDown nudRecord;
        /// private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem File_tsmi;
        private System.Windows.Forms.ToolStripMenuItem OpenFile_tsmi;
        private System.Windows.Forms.ToolStripMenuItem SaveFile_tsmi;
        private System.Windows.Forms.ToolStripMenuItem Quit_tsmi;
        private System.Windows.Forms.ToolStripMenuItem About_tsmi;
        private System.Windows.Forms.TextBox SignatureIN_tb;
        private System.Windows.Forms.Label SignatureIN_lb;
        private System.Windows.Forms.Label Records_lb;
        private System.Windows.Forms.Button Translate_btn;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label SignatureOUT_lb;
        private System.Windows.Forms.TextBox SignatureOUT_tb;
        private System.Windows.Forms.Button Start_btn;
        private System.Windows.Forms.Label progressBar1_lb;
        private System.Windows.Forms.ToolStripMenuItem OpenTranslatedFile_tsmi;
        private System.Windows.Forms.ToolStripMenuItem CloseFilesClear_tsmi;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label SourceFile_tb;
        private System.Windows.Forms.Label TranslatedFile_tb;
        private System.Windows.Forms.Button Delete_btn;
        private System.Windows.Forms.ToolStrip Search_ts;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox Search_tstb;
        private System.Windows.Forms.ToolStripButton SourceSearch_tsb;
        private System.Windows.Forms.ToolStripButton SourceFirst_tsb;
        private System.Windows.Forms.ToolStripButton SourcePrev_tsb;
        private System.Windows.Forms.ToolStripButton SourceNext_tsb;
        private System.Windows.Forms.ToolStripButton SourceLast_tsb;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel SearchStat_tslb;
        private System.Windows.Forms.ToolStripButton TabClose_tsb;
        private System.Windows.Forms.StatusStrip miniToolStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel lbSource;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lbTranslated;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label OffsetOUT_lb;
        private System.Windows.Forms.Label OffsetIN_lb;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.RadioButton OffsetMode512_rb;
        private System.Windows.Forms.RadioButton OffsetModeInt32_rb;
        private System.Windows.Forms.RadioButton SignatureModeString_rb;
        private System.Windows.Forms.RadioButton SignatureModeHEX_rb;
        private System.Windows.Forms.GroupBox OffsetgroupBox;
        private System.Windows.Forms.GroupBox Signature_groupBox;
        private System.Windows.Forms.GroupBox Records_groupBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox ccpanel;
    }
}


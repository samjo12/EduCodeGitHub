
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
        internal void InitializeComponent()
        {
            Prev_btn = new System.Windows.Forms.Button();
            Next_btn = new System.Windows.Forms.Button();
            nudRecord = new System.Windows.Forms.NumericUpDown();
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            File_tsmi = new System.Windows.Forms.ToolStripMenuItem();
            OpenFile_tsmi = new System.Windows.Forms.ToolStripMenuItem();
            ImportText_tsmi = new System.Windows.Forms.ToolStripMenuItem();
            ImportTextST_tsmi = new System.Windows.Forms.ToolStripMenuItem();
            SaveProject_tsmi = new System.Windows.Forms.ToolStripMenuItem();
            ExportTextFileST_tsmi = new System.Windows.Forms.ToolStripMenuItem();
            CloseFilesClear_tsmi = new System.Windows.Forms.ToolStripMenuItem();
            Quit_tsmi = new System.Windows.Forms.ToolStripMenuItem();
            About_tsmi = new System.Windows.Forms.ToolStripMenuItem();
            Offset_tb = new System.Windows.Forms.TextBox();
            Offset_lb = new System.Windows.Forms.Label();
            Records_lb = new System.Windows.Forms.Label();
            Translate_btn = new System.Windows.Forms.Button();
            progressBar1 = new System.Windows.Forms.ProgressBar();
            Signature_lb = new System.Windows.Forms.Label();
            Signature_tb = new System.Windows.Forms.TextBox();
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
            label2 = new System.Windows.Forms.Label();
            PerformingFile_tb = new System.Windows.Forms.Label();
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
            UNDO = new System.Windows.Forms.Button();
            REDO = new System.Windows.Forms.Button();
            OpenProjectFile_tsmi = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)nudRecord).BeginInit();
            menuStrip1.SuspendLayout();
            Search_ts.SuspendLayout();
            statusStrip1.SuspendLayout();
            statusStrip2.SuspendLayout();
            SuspendLayout();
            // 
            // Prev_btn
            // 
            Prev_btn.BackColor = System.Drawing.Color.FromArgb(255, 192, 192);
            Prev_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            Prev_btn.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold);
            Prev_btn.Location = new System.Drawing.Point(500, 609);
            Prev_btn.Name = "Prev_btn";
            Prev_btn.Size = new System.Drawing.Size(75, 56);
            Prev_btn.TabIndex = 3;
            Prev_btn.Text = "Prev";
            Prev_btn.UseVisualStyleBackColor = false;
            Prev_btn.Click += Prev_btn_Click;
            // 
            // Next_btn
            // 
            Next_btn.BackColor = System.Drawing.Color.FromArgb(255, 192, 192);
            Next_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            Next_btn.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold);
            Next_btn.Location = new System.Drawing.Point(581, 609);
            Next_btn.Name = "Next_btn";
            Next_btn.Size = new System.Drawing.Size(75, 56);
            Next_btn.TabIndex = 4;
            Next_btn.Text = "Next";
            Next_btn.UseVisualStyleBackColor = false;
            Next_btn.Click += Next_btn_Click;
            // 
            // nudRecord
            // 
            nudRecord.BackColor = System.Drawing.SystemColors.Control;
            nudRecord.Location = new System.Drawing.Point(677, 611);
            nudRecord.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudRecord.Name = "nudRecord";
            nudRecord.ReadOnly = true;
            nudRecord.Size = new System.Drawing.Size(56, 23);
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
            File_tsmi.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { OpenFile_tsmi, ImportText_tsmi, ImportTextST_tsmi, SaveProject_tsmi, OpenProjectFile_tsmi, ExportTextFileST_tsmi, CloseFilesClear_tsmi, Quit_tsmi });
            File_tsmi.Name = "File_tsmi";
            File_tsmi.Size = new System.Drawing.Size(37, 20);
            File_tsmi.Text = "File";
            File_tsmi.Click += File_tsmi_Click;
            // 
            // OpenFile_tsmi
            // 
            OpenFile_tsmi.Name = "OpenFile_tsmi";
            OpenFile_tsmi.Size = new System.Drawing.Size(197, 22);
            OpenFile_tsmi.Text = "Open Binary DataFile";
            OpenFile_tsmi.Click += OpenBinaryFile_tsmi_Click;
            // 
            // ImportText_tsmi
            // 
            ImportText_tsmi.Name = "ImportText_tsmi";
            ImportText_tsmi.Size = new System.Drawing.Size(197, 22);
            ImportText_tsmi.Text = "Import TextFile";
            ImportText_tsmi.Click += ImportText_Click;
            // 
            // ImportTextST_tsmi
            // 
            ImportTextST_tsmi.Name = "ImportTextST_tsmi";
            ImportTextST_tsmi.Size = new System.Drawing.Size(197, 22);
            ImportTextST_tsmi.Text = "Import TextFile \"S=T\"";
            ImportTextST_tsmi.Click += OpenTranslatedFileST_tsmi_Click;
            // 
            // SaveProject_tsmi
            // 
            SaveProject_tsmi.Enabled = false;
            SaveProject_tsmi.Name = "SaveProject_tsmi";
            SaveProject_tsmi.Size = new System.Drawing.Size(197, 22);
            SaveProject_tsmi.Text = "Save Project";
            SaveProject_tsmi.Click += SaveProject_Click;
            // 
            // ExportTextFileST_tsmi
            // 
            ExportTextFileST_tsmi.Enabled = false;
            ExportTextFileST_tsmi.Name = "ExportTextFileST_tsmi";
            ExportTextFileST_tsmi.Size = new System.Drawing.Size(197, 22);
            ExportTextFileST_tsmi.Text = "Export TextFile as \"S=T\"";
            ExportTextFileST_tsmi.Click += ExportTextFileST_tsmi_Click;
            // 
            // CloseFilesClear_tsmi
            // 
            CloseFilesClear_tsmi.Enabled = false;
            CloseFilesClear_tsmi.Name = "CloseFilesClear_tsmi";
            CloseFilesClear_tsmi.Size = new System.Drawing.Size(197, 22);
            CloseFilesClear_tsmi.Text = "Close Files/Clear";
            CloseFilesClear_tsmi.Click += CloseFilesClear_Click;
            // 
            // Quit_tsmi
            // 
            Quit_tsmi.Name = "Quit_tsmi";
            Quit_tsmi.Size = new System.Drawing.Size(197, 22);
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
            // Offset_tb
            // 
            Offset_tb.Location = new System.Drawing.Point(69, 673);
            Offset_tb.Name = "Offset_tb";
            Offset_tb.PlaceholderText = "HEX number";
            Offset_tb.ReadOnly = true;
            Offset_tb.Size = new System.Drawing.Size(115, 23);
            Offset_tb.TabIndex = 12;
            // 
            // Offset_lb
            // 
            Offset_lb.AutoSize = true;
            Offset_lb.Location = new System.Drawing.Point(15, 678);
            Offset_lb.Name = "Offset_lb";
            Offset_lb.Size = new System.Drawing.Size(54, 15);
            Offset_lb.TabIndex = 13;
            Offset_lb.Text = "Offset 0x";
            // 
            // Records_lb
            // 
            Records_lb.AutoSize = true;
            Records_lb.Location = new System.Drawing.Point(740, 614);
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
            Translate_btn.Font = new System.Drawing.Font("Segoe UI Semibold", 14.75F, System.Drawing.FontStyle.Bold);
            Translate_btn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            Translate_btn.Image = Properties.Resources.Google48;
            Translate_btn.Location = new System.Drawing.Point(922, 609);
            Translate_btn.Name = "Translate_btn";
            Translate_btn.Size = new System.Drawing.Size(75, 56);
            Translate_btn.TabIndex = 15;
            Translate_btn.UseVisualStyleBackColor = false;
            Translate_btn.Click += Translate_btn_Click;
            // 
            // progressBar1
            // 
            progressBar1.ForeColor = System.Drawing.Color.FromArgb(0, 192, 0);
            progressBar1.Location = new System.Drawing.Point(15, 705);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new System.Drawing.Size(930, 22);
            progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            progressBar1.TabIndex = 19;
            // 
            // Signature_lb
            // 
            Signature_lb.AutoSize = true;
            Signature_lb.Location = new System.Drawing.Point(190, 678);
            Signature_lb.Name = "Signature_lb";
            Signature_lb.Size = new System.Drawing.Size(72, 15);
            Signature_lb.TabIndex = 26;
            Signature_lb.Text = "Signature 0x";
            // 
            // Signature_tb
            // 
            Signature_tb.Location = new System.Drawing.Point(264, 673);
            Signature_tb.Name = "Signature_tb";
            Signature_tb.PlaceholderText = "HEX number";
            Signature_tb.ReadOnly = true;
            Signature_tb.Size = new System.Drawing.Size(162, 23);
            Signature_tb.TabIndex = 27;
            // 
            // Start_btn
            // 
            Start_btn.BackColor = System.Drawing.SystemColors.ButtonFace;
            Start_btn.DialogResult = System.Windows.Forms.DialogResult.Retry;
            Start_btn.Font = new System.Drawing.Font("Segoe UI Black", 9F, System.Drawing.FontStyle.Bold);
            Start_btn.Location = new System.Drawing.Point(425, 673);
            Start_btn.Name = "Start_btn";
            Start_btn.Size = new System.Drawing.Size(164, 23);
            Start_btn.TabIndex = 29;
            Start_btn.Text = "Search Binary";
            Start_btn.UseVisualStyleBackColor = false;
            Start_btn.Visible = false;
            Start_btn.Click += Start_btn_Click;
            // 
            // progressBar1_lb
            // 
            progressBar1_lb.AccessibleRole = System.Windows.Forms.AccessibleRole.ProgressBar;
            progressBar1_lb.BackColor = System.Drawing.SystemColors.Control;
            progressBar1_lb.FlatStyle = System.Windows.Forms.FlatStyle.System;
            progressBar1_lb.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            progressBar1_lb.Location = new System.Drawing.Point(951, 707);
            progressBar1_lb.Name = "progressBar1_lb";
            progressBar1_lb.Size = new System.Drawing.Size(46, 20);
            progressBar1_lb.TabIndex = 30;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "{ \"Afrikaans\",\"af\"},{ \"Albanian\",\"sq\"},{ \"Arabic\",\"ar\"},{ \"Armenian\",\"hy\"},{ \"Azerbaijani\",\"az\"},{ \"Basque\",\"eu\"},{ \"Belarusian\",\"be\"},", "{ \"Bulgarian\",\"bg\"},{ \"Catalan\",\"ca\"},{ \"Chinese(Simplified)\",\"zh-CN\"},{ \"Chinese(Traditional)\",\"zh-TW\"},{ \"Croatian\",\"hr\"},", "{ \"Czech\",\"cs\"},{ \"Danish\",\"da\"},{ \"Dutch\",\"nl\"},{ \"English\",\"en\"},{ \"Estonian\",\"et\"},{ \"Filipino\",\"tl\"},{ \"Finnish\",\"fi\"},", "{ \"French\",\"fr\"},{ \"Galician\",\"gl\"},{ \"Georgian\",\"ka\"},{ \"German\",\"de\"},{ \"Greek\",\"el\"},{ \"Haitian\",\"ht\"},{ \"Hebrew\",\"iw\"},", "{ \"Hindi\",\"hi\"},{ \"Hungarian\",\"hu\"},{ \"Icelandic\",\"is\"},{ \"Indonesian\",\"id\"},{ \"Irish\", \"ga\"},{ \"Italian\",\"it\"},{ \"Japanese\",\"ja\"},", "{ \"Korean\",\"ko\"},{ \"Latvian\",\"lv\"},{ \"Lithuanian\",\"lt\"},{ \"Macedonian\",\"mk\"},{ \"Malay\",\"ms\"},{ \"Maltese\",\"mt\"},{ \"Norwegian\",\"no\"},", "{ \"Persian\",\"fa\"},{ \"Polish\",\"pl\"},{ \"Portuguese\",\"pt\"},{ \"Romanian\",\"ro\"},{ \"Russian\",\"ru\"},{ \"Serbian\",\"sr\"},{ \"Slovak\",\"sk\"},", "{ \"Slovenian\",\"sl\"},{ \"Spanish\",\"es\"},{ \"Swahili\",\"sw\"},{ \"Swedish\",\"sv\"},{ \"Thai\",\"th\"},{ \"Turkish\",\"tr\"},{ \"Ukrainian\",\"uk\"},", "{ \"Urdu\",\"ur\"},{ \"Vietnamese\",\"vi\"},{ \"Welsh\",\"cy\"},{ \"Yiddish\",\"yi\"} " });
            comboBox1.Location = new System.Drawing.Point(740, 642);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new System.Drawing.Size(73, 23);
            comboBox1.TabIndex = 32;
            comboBox1.SelectionChangeCommitted += comboBox1_SelectedIndexChanged;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new System.Drawing.Point(839, 642);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new System.Drawing.Size(73, 23);
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
            label1.Font = new System.Drawing.Font("Wingdings 3", 9F, System.Drawing.FontStyle.Bold);
            label1.Location = new System.Drawing.Point(813, 646);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(17, 13);
            label1.TabIndex = 35;
            label1.Text = "->";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(677, 640);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(57, 30);
            label2.TabIndex = 36;
            label2.Text = "Direction\r\ntranslator";
            // 
            // PerformingFile_tb
            // 
            PerformingFile_tb.AutoSize = true;
            PerformingFile_tb.BackColor = System.Drawing.Color.ForestGreen;
            PerformingFile_tb.Location = new System.Drawing.Point(16, 709);
            PerformingFile_tb.Name = "PerformingFile_tb";
            PerformingFile_tb.Size = new System.Drawing.Size(0, 15);
            PerformingFile_tb.TabIndex = 37;
            // 
            // TranslatedFile_tb
            // 
            TranslatedFile_tb.AutoSize = true;
            TranslatedFile_tb.BackColor = System.Drawing.Color.Lime;
            TranslatedFile_tb.Location = new System.Drawing.Point(19, 709);
            TranslatedFile_tb.Name = "TranslatedFile_tb";
            TranslatedFile_tb.Size = new System.Drawing.Size(0, 15);
            TranslatedFile_tb.TabIndex = 38;
            // 
            // Delete_btn
            // 
            Delete_btn.Image = Properties.Resources.Basket;
            Delete_btn.Location = new System.Drawing.Point(12, 611);
            Delete_btn.Name = "Delete_btn";
            Delete_btn.Size = new System.Drawing.Size(41, 40);
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
            toolStripStatusLabel2.Size = new System.Drawing.Size(126, 17);
            toolStripStatusLabel2.Text = "Source message bytes:";
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
            toolStripStatusLabel1.Size = new System.Drawing.Size(143, 15);
            toolStripStatusLabel1.Text = "Translated message bytes:";
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
            statusStrip2.Size = new System.Drawing.Size(143, 22);
            statusStrip2.TabIndex = 19;
            statusStrip2.Text = "statusStrip2";
            statusStrip2.Visible = false;
            // 
            // Save
            // 
            Save.BackgroundImage = Properties.Resources.save;
            Save.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            Save.Location = new System.Drawing.Point(69, 612);
            Save.Name = "Save";
            Save.Size = new System.Drawing.Size(36, 37);
            Save.TabIndex = 40;
            Save.UseVisualStyleBackColor = true;
            Save.Click += Save_Click;
            // 
            // UNDO
            // 
            UNDO.Location = new System.Drawing.Point(124, 610);
            UNDO.Name = "UNDO";
            UNDO.Size = new System.Drawing.Size(47, 41);
            UNDO.TabIndex = 41;
            UNDO.Text = "Undo";
            UNDO.UseVisualStyleBackColor = true;
            UNDO.Click += UNDO_Click;
            // 
            // REDO
            // 
            REDO.Location = new System.Drawing.Point(177, 610);
            REDO.Name = "REDO";
            REDO.Size = new System.Drawing.Size(47, 41);
            REDO.TabIndex = 42;
            REDO.Text = "Redo";
            REDO.UseVisualStyleBackColor = true;
            // 
            // OpenProjectFile_tsmi
            // 
            OpenProjectFile_tsmi.Name = "OpenProjectFile_tsmi";
            OpenProjectFile_tsmi.Size = new System.Drawing.Size(197, 22);
            OpenProjectFile_tsmi.Text = "Open Project";
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1008, 729);
            Controls.Add(REDO);
            Controls.Add(UNDO);
            Controls.Add(Save);
            Controls.Add(statusStrip1);
            Controls.Add(statusStrip2);
            Controls.Add(Delete_btn);
            Controls.Add(TranslatedFile_tb);
            Controls.Add(PerformingFile_tb);
            Controls.Add(Search_ts);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(progressBar1_lb);
            Controls.Add(comboBox2);
            Controls.Add(comboBox1);
            Controls.Add(Start_btn);
            Controls.Add(Signature_tb);
            Controls.Add(Signature_lb);
            Controls.Add(progressBar1);
            Controls.Add(Translate_btn);
            Controls.Add(Records_lb);
            Controls.Add(Offset_lb);
            Controls.Add(Offset_tb);
            Controls.Add(nudRecord);
            Controls.Add(Next_btn);
            Controls.Add(Prev_btn);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Help Custom Translator v.0.76 alfa by Samjo";
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
        private System.Windows.Forms.ToolStripMenuItem ExportTextFileST_tsmi;
        private System.Windows.Forms.ToolStripMenuItem Quit_tsmi;
        private System.Windows.Forms.ToolStripMenuItem About_tsmi;
        private System.Windows.Forms.TextBox Offset_tb;
        private System.Windows.Forms.Label Offset_lb;
        private System.Windows.Forms.Label Records_lb;
        private System.Windows.Forms.Button Translate_btn;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label Signature_lb;
        private System.Windows.Forms.TextBox Signature_tb;
        private System.Windows.Forms.Button Start_btn;
        private System.Windows.Forms.Label progressBar1_lb;
        private System.Windows.Forms.ToolStripMenuItem ImportTextST_tsmi;
        private System.Windows.Forms.ToolStripMenuItem CloseFilesClear_tsmi;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label PerformingFile_tb;
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
        private System.Windows.Forms.Button UNDO;
        private System.Windows.Forms.Button REDO;
        private System.Windows.Forms.ToolStripMenuItem SaveProject_tsmi;
        private System.Windows.Forms.ToolStripMenuItem ImportText_tsmi;
        private System.Windows.Forms.ToolStripMenuItem OpenProjectFile_tsmi;
    }
}


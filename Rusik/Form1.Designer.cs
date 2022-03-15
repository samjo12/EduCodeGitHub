
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
            this.Prev_btn = new System.Windows.Forms.Button();
            this.Next_btn = new System.Windows.Forms.Button();
            this.nudRecord = new System.Windows.Forms.NumericUpDown();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.File_tsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFile_tsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenTranslatedFile_tsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.CloseFilesClear_tsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveFile_tsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.Quit_tsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.About_tsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.Offset_tb = new System.Windows.Forms.TextBox();
            this.Offset_lb = new System.Windows.Forms.Label();
            this.Records_lb = new System.Windows.Forms.Label();
            this.Translate_btn = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.Signature_lb = new System.Windows.Forms.Label();
            this.Signature_tb = new System.Windows.Forms.TextBox();
            this.Start_btn = new System.Windows.Forms.Button();
            this.progressBar1_lb = new System.Windows.Forms.Label();
            this.Source_tc = new System.Windows.Forms.TabControl();
            this.HomeSource = new System.Windows.Forms.TabPage();
            this.Source_ts = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.SearchSource_tstb = new System.Windows.Forms.ToolStripTextBox();
            this.SearchSource_tsb = new System.Windows.Forms.ToolStripButton();
            this.SourceFirst_tsb = new System.Windows.Forms.ToolStripButton();
            this.SourcePrev_tsb = new System.Windows.Forms.ToolStripButton();
            this.SourceNext_tsb = new System.Windows.Forms.ToolStripButton();
            this.SourceLast_tsb = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.SearchStat_tslb = new System.Windows.Forms.ToolStripLabel();
            this.Close = new System.Windows.Forms.ToolStripButton();
            this.Source_tb = new System.Windows.Forms.TextBox();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbSource = new System.Windows.Forms.ToolStripStatusLabel();
            this.Translated_tb = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.Translated_tc = new System.Windows.Forms.TabControl();
            this.TranslatedHome = new System.Windows.Forms.TabPage();
            this.Translated_ts = new System.Windows.Forms.ToolStrip();
            this.SearchTranslated_tstb = new System.Windows.Forms.ToolStripTextBox();
            this.SearchTranslated_tsb = new System.Windows.Forms.ToolStripButton();
            this.TranslatedPrev_tsb = new System.Windows.Forms.ToolStripButton();
            this.TranslatedNext_tsb = new System.Windows.Forms.ToolStripButton();
            this.TranslatedLast_tsb = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.CloseTS_tsb = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbTranslated = new System.Windows.Forms.ToolStripStatusLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SourceFile_tb = new System.Windows.Forms.Label();
            this.TranslatedFile_tb = new System.Windows.Forms.Label();
            this.Delete_btn = new System.Windows.Forms.Button();
            this.TranslatedFirst_tsb = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.nudRecord)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.Source_tc.SuspendLayout();
            this.HomeSource.SuspendLayout();
            this.Source_ts.SuspendLayout();
            this.statusStrip2.SuspendLayout();
            this.Translated_tc.SuspendLayout();
            this.TranslatedHome.SuspendLayout();
            this.Translated_ts.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Prev_btn
            // 
            this.Prev_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.Prev_btn.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Prev_btn.Location = new System.Drawing.Point(500, 609);
            this.Prev_btn.Name = "Prev_btn";
            this.Prev_btn.Size = new System.Drawing.Size(75, 56);
            this.Prev_btn.TabIndex = 3;
            this.Prev_btn.Text = "Prev";
            this.Prev_btn.UseVisualStyleBackColor = false;
            this.Prev_btn.Click += new System.EventHandler(this.Prev_btn_Click);
            // 
            // Next_btn
            // 
            this.Next_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.Next_btn.Font = new System.Drawing.Font("Segoe UI Black", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Next_btn.Location = new System.Drawing.Point(581, 609);
            this.Next_btn.Name = "Next_btn";
            this.Next_btn.Size = new System.Drawing.Size(75, 56);
            this.Next_btn.TabIndex = 4;
            this.Next_btn.Text = "Next";
            this.Next_btn.UseVisualStyleBackColor = false;
            this.Next_btn.Click += new System.EventHandler(this.Next_btn_Click);
            // 
            // nudRecord
            // 
            this.nudRecord.Location = new System.Drawing.Point(677, 611);
            this.nudRecord.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudRecord.Name = "nudRecord";
            this.nudRecord.ReadOnly = true;
            this.nudRecord.Size = new System.Drawing.Size(56, 23);
            this.nudRecord.TabIndex = 8;
            this.nudRecord.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudRecord.ValueChanged += new System.EventHandler(this.nudRecord_ValueChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.File_tsmi,
            this.About_tsmi});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1008, 24);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // File_tsmi
            // 
            this.File_tsmi.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenFile_tsmi,
            this.OpenTranslatedFile_tsmi,
            this.CloseFilesClear_tsmi,
            this.SaveFile_tsmi,
            this.Quit_tsmi});
            this.File_tsmi.Name = "File_tsmi";
            this.File_tsmi.Size = new System.Drawing.Size(37, 20);
            this.File_tsmi.Text = "File";
            // 
            // OpenFile_tsmi
            // 
            this.OpenFile_tsmi.Name = "OpenFile_tsmi";
            this.OpenFile_tsmi.Size = new System.Drawing.Size(207, 22);
            this.OpenFile_tsmi.Text = "Open Binary File";
            this.OpenFile_tsmi.Click += new System.EventHandler(this.OpenFile_tsmi_Click);
            // 
            // OpenTranslatedFile_tsmi
            // 
            this.OpenTranslatedFile_tsmi.Name = "OpenTranslatedFile_tsmi";
            this.OpenTranslatedFile_tsmi.Size = new System.Drawing.Size(207, 22);
            this.OpenTranslatedFile_tsmi.Text = "Open Translated Text File";
            this.OpenTranslatedFile_tsmi.Click += new System.EventHandler(this.OpenTranslatedFile_tsmi_Click);
            // 
            // CloseFilesClear_tsmi
            // 
            this.CloseFilesClear_tsmi.Name = "CloseFilesClear_tsmi";
            this.CloseFilesClear_tsmi.Size = new System.Drawing.Size(207, 22);
            this.CloseFilesClear_tsmi.Text = "Close Files/Clear";
            this.CloseFilesClear_tsmi.Click += new System.EventHandler(this.CloseFilesClear_Click);
            // 
            // SaveFile_tsmi
            // 
            this.SaveFile_tsmi.Name = "SaveFile_tsmi";
            this.SaveFile_tsmi.Size = new System.Drawing.Size(207, 22);
            this.SaveFile_tsmi.Text = "Save File As...";
            this.SaveFile_tsmi.Click += new System.EventHandler(this.SaveFile_tsmi_Click);
            // 
            // Quit_tsmi
            // 
            this.Quit_tsmi.Name = "Quit_tsmi";
            this.Quit_tsmi.Size = new System.Drawing.Size(207, 22);
            this.Quit_tsmi.Text = "Quit";
            this.Quit_tsmi.Click += new System.EventHandler(this.Quit_tsmi_Click);
            // 
            // About_tsmi
            // 
            this.About_tsmi.Name = "About_tsmi";
            this.About_tsmi.Size = new System.Drawing.Size(52, 20);
            this.About_tsmi.Text = "About";
            this.About_tsmi.Click += new System.EventHandler(this.About_tsmi_Click);
            // 
            // Offset_tb
            // 
            this.Offset_tb.Location = new System.Drawing.Point(69, 673);
            this.Offset_tb.Name = "Offset_tb";
            this.Offset_tb.PlaceholderText = "HEX number";
            this.Offset_tb.ReadOnly = true;
            this.Offset_tb.Size = new System.Drawing.Size(115, 23);
            this.Offset_tb.TabIndex = 12;
            // 
            // Offset_lb
            // 
            this.Offset_lb.AutoSize = true;
            this.Offset_lb.Location = new System.Drawing.Point(15, 678);
            this.Offset_lb.Name = "Offset_lb";
            this.Offset_lb.Size = new System.Drawing.Size(53, 15);
            this.Offset_lb.TabIndex = 13;
            this.Offset_lb.Text = "Offset 0x";
            // 
            // Records_lb
            // 
            this.Records_lb.AutoSize = true;
            this.Records_lb.Location = new System.Drawing.Point(740, 614);
            this.Records_lb.Name = "Records_lb";
            this.Records_lb.Size = new System.Drawing.Size(95, 15);
            this.Records_lb.TabIndex = 14;
            this.Records_lb.Text = "Found: 0 records";
            // 
            // Translate_btn
            // 
            this.Translate_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.Translate_btn.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.Translate_btn.Font = new System.Drawing.Font("Segoe UI Semibold", 14.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Translate_btn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Translate_btn.Image = global::Rusik.Properties.Resources.Google48;
            this.Translate_btn.Location = new System.Drawing.Point(922, 609);
            this.Translate_btn.Name = "Translate_btn";
            this.Translate_btn.Size = new System.Drawing.Size(75, 56);
            this.Translate_btn.TabIndex = 15;
            this.Translate_btn.UseVisualStyleBackColor = false;
            this.Translate_btn.Click += new System.EventHandler(this.Translate_btn_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.progressBar1.Location = new System.Drawing.Point(15, 705);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(930, 22);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 19;
            // 
            // Signature_lb
            // 
            this.Signature_lb.AutoSize = true;
            this.Signature_lb.Location = new System.Drawing.Point(190, 678);
            this.Signature_lb.Name = "Signature_lb";
            this.Signature_lb.Size = new System.Drawing.Size(71, 15);
            this.Signature_lb.TabIndex = 26;
            this.Signature_lb.Text = "Signature 0x";
            // 
            // Signature_tb
            // 
            this.Signature_tb.Location = new System.Drawing.Point(264, 673);
            this.Signature_tb.Name = "Signature_tb";
            this.Signature_tb.PlaceholderText = "HEX number";
            this.Signature_tb.ReadOnly = true;
            this.Signature_tb.Size = new System.Drawing.Size(162, 23);
            this.Signature_tb.TabIndex = 27;
            // 
            // Start_btn
            // 
            this.Start_btn.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Start_btn.DialogResult = System.Windows.Forms.DialogResult.Retry;
            this.Start_btn.Font = new System.Drawing.Font("Segoe UI Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Start_btn.Location = new System.Drawing.Point(425, 673);
            this.Start_btn.Name = "Start_btn";
            this.Start_btn.Size = new System.Drawing.Size(164, 23);
            this.Start_btn.TabIndex = 29;
            this.Start_btn.Text = "Search Binary";
            this.Start_btn.UseVisualStyleBackColor = false;
            this.Start_btn.Visible = false;
            this.Start_btn.Click += new System.EventHandler(this.Start_btn_Click);
            // 
            // progressBar1_lb
            // 
            this.progressBar1_lb.AccessibleRole = System.Windows.Forms.AccessibleRole.ProgressBar;
            this.progressBar1_lb.BackColor = System.Drawing.SystemColors.Control;
            this.progressBar1_lb.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.progressBar1_lb.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.progressBar1_lb.Location = new System.Drawing.Point(951, 707);
            this.progressBar1_lb.Name = "progressBar1_lb";
            this.progressBar1_lb.Size = new System.Drawing.Size(46, 20);
            this.progressBar1_lb.TabIndex = 30;
            // 
            // Source_tc
            // 
            this.Source_tc.Controls.Add(this.HomeSource);
            this.Source_tc.Location = new System.Drawing.Point(11, 39);
            this.Source_tc.Name = "Source_tc";
            this.Source_tc.SelectedIndex = 0;
            this.Source_tc.Size = new System.Drawing.Size(486, 565);
            this.Source_tc.TabIndex = 31;
            this.Source_tc.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.Source_tc_Selecting);
            // 
            // HomeSource
            // 
            this.HomeSource.BackColor = System.Drawing.SystemColors.Control;
            this.HomeSource.Controls.Add(this.Source_ts);
            this.HomeSource.Controls.Add(this.Source_tb);
            this.HomeSource.Controls.Add(this.statusStrip2);
            this.HomeSource.Location = new System.Drawing.Point(4, 24);
            this.HomeSource.Name = "HomeSource";
            this.HomeSource.Size = new System.Drawing.Size(478, 537);
            this.HomeSource.TabIndex = 0;
            this.HomeSource.Text = "Home";
            // 
            // Source_ts
            // 
            this.Source_ts.BackColor = System.Drawing.SystemColors.Info;
            this.Source_ts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.SearchSource_tstb,
            this.SearchSource_tsb,
            this.SourceFirst_tsb,
            this.SourcePrev_tsb,
            this.SourceNext_tsb,
            this.SourceLast_tsb,
            this.toolStripSeparator3,
            this.SearchStat_tslb,
            this.Close});
            this.Source_ts.Location = new System.Drawing.Point(0, 0);
            this.Source_ts.Name = "Source_ts";
            this.Source_ts.Size = new System.Drawing.Size(478, 25);
            this.Source_ts.TabIndex = 37;
            this.Source_ts.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(0, 22);
            this.toolStripLabel1.ToolTipText = "Type your string for searching here...";
            // 
            // SearchSource_tstb
            // 
            this.SearchSource_tstb.AcceptsReturn = true;
            this.SearchSource_tstb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SearchSource_tstb.Name = "SearchSource_tstb";
            this.SearchSource_tstb.ReadOnly = true;
            this.SearchSource_tstb.Size = new System.Drawing.Size(250, 25);
            this.SearchSource_tstb.ToolTipText = "Type text for seaching here...";
            // 
            // SearchSource_tsb
            // 
            this.SearchSource_tsb.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SearchSource_tsb.Image = global::Rusik.Properties.Resources.Search;
            this.SearchSource_tsb.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SearchSource_tsb.Name = "SearchSource_tsb";
            this.SearchSource_tsb.Size = new System.Drawing.Size(23, 22);
            this.SearchSource_tsb.Text = "Search";
            this.SearchSource_tsb.ToolTipText = "Go search";
            this.SearchSource_tsb.Click += new System.EventHandler(this.SearchSource_Click);
            // 
            // SourceFirst_tsb
            // 
            this.SourceFirst_tsb.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SourceFirst_tsb.Image = global::Rusik.Properties.Resources.ToFirst;
            this.SourceFirst_tsb.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SourceFirst_tsb.Name = "SourceFirst_tsb";
            this.SourceFirst_tsb.Size = new System.Drawing.Size(23, 22);
            this.SourceFirst_tsb.Text = "First";
            this.SourceFirst_tsb.Click += new System.EventHandler(this.SourceFirst_tsb_Click);
            // 
            // SourcePrev_tsb
            // 
            this.SourcePrev_tsb.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SourcePrev_tsb.Image = global::Rusik.Properties.Resources.Prev;
            this.SourcePrev_tsb.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SourcePrev_tsb.Name = "SourcePrev_tsb";
            this.SourcePrev_tsb.Size = new System.Drawing.Size(23, 22);
            this.SourcePrev_tsb.Text = "Previous";
            this.SourcePrev_tsb.Click += new System.EventHandler(this.Search_Prev_btn_Click);
            // 
            // SourceNext_tsb
            // 
            this.SourceNext_tsb.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SourceNext_tsb.Image = global::Rusik.Properties.Resources.Next;
            this.SourceNext_tsb.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SourceNext_tsb.Name = "SourceNext_tsb";
            this.SourceNext_tsb.Size = new System.Drawing.Size(23, 22);
            this.SourceNext_tsb.Text = "Next";
            this.SourceNext_tsb.Click += new System.EventHandler(this.Search_Next_btn_Click);
            // 
            // SourceLast_tsb
            // 
            this.SourceLast_tsb.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SourceLast_tsb.Image = global::Rusik.Properties.Resources.ToLast;
            this.SourceLast_tsb.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SourceLast_tsb.Name = "SourceLast_tsb";
            this.SourceLast_tsb.Size = new System.Drawing.Size(23, 22);
            this.SourceLast_tsb.Text = "Last";
            this.SourceLast_tsb.Click += new System.EventHandler(this.SourceLast_tsb_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // SearchStat_tslb
            // 
            this.SearchStat_tslb.Name = "SearchStat_tslb";
            this.SearchStat_tslb.Size = new System.Drawing.Size(0, 22);
            // 
            // Close
            // 
            this.Close.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Close.Image = global::Rusik.Properties.Resources.cross;
            this.Close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Close.Name = "Close";
            this.Close.Size = new System.Drawing.Size(23, 22);
            this.Close.Text = "toolStripButton1";
            this.Close.ToolTipText = "Close Search";
            this.Close.Click += new System.EventHandler(this.SourceClose_tsb_Click);
            // 
            // Source_tb
            // 
            this.Source_tb.AllowDrop = true;
            this.Source_tb.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.Source_tb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Source_tb.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Source_tb.Location = new System.Drawing.Point(0, 25);
            this.Source_tb.Multiline = true;
            this.Source_tb.Name = "Source_tb";
            this.Source_tb.ReadOnly = true;
            this.Source_tb.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Source_tb.Size = new System.Drawing.Size(472, 485);
            this.Source_tb.TabIndex = 36;
            this.Source_tb.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Source_tb_MouseClick);
            this.Source_tb.LocationChanged += new System.EventHandler(this.Source_tb_TextChanged);
            // 
            // statusStrip2
            // 
            this.statusStrip2.BackColor = System.Drawing.SystemColors.Info;
            this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel2,
            this.lbSource});
            this.statusStrip2.Location = new System.Drawing.Point(0, 515);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.Size = new System.Drawing.Size(478, 22);
            this.statusStrip2.TabIndex = 19;
            this.statusStrip2.Text = "statusStrip2";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(142, 17);
            this.toolStripStatusLabel2.Text = "Source message symbols:";
            // 
            // lbSource
            // 
            this.lbSource.Name = "lbSource";
            this.lbSource.Size = new System.Drawing.Size(0, 17);
            // 
            // Translated_tb
            // 
            this.Translated_tb.AllowDrop = true;
            this.Translated_tb.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Translated_tb.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.Translated_tb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Translated_tb.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Translated_tb.Location = new System.Drawing.Point(3, 28);
            this.Translated_tb.Multiline = true;
            this.Translated_tb.Name = "Translated_tb";
            this.Translated_tb.ReadOnly = true;
            this.Translated_tb.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Translated_tb.Size = new System.Drawing.Size(483, 485);
            this.Translated_tb.TabIndex = 37;
            this.Translated_tb.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Translated_tb_KeyUp);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "{ \"Afrikaans\",\"af\"},{ \"Albanian\",\"sq\"},{ \"Arabic\",\"ar\"},{ \"Armenian\",\"hy\"},{ \"Aze" +
                "rbaijani\",\"az\"},{ \"Basque\",\"eu\"},{ \"Belarusian\",\"be\"},",
            "{ \"Bulgarian\",\"bg\"},{ \"Catalan\",\"ca\"},{ \"Chinese(Simplified)\",\"zh-CN\"},{ \"Chinese" +
                "(Traditional)\",\"zh-TW\"},{ \"Croatian\",\"hr\"},",
            "{ \"Czech\",\"cs\"},{ \"Danish\",\"da\"},{ \"Dutch\",\"nl\"},{ \"English\",\"en\"},{ \"Estonian\",\"" +
                "et\"},{ \"Filipino\",\"tl\"},{ \"Finnish\",\"fi\"},",
            "{ \"French\",\"fr\"},{ \"Galician\",\"gl\"},{ \"Georgian\",\"ka\"},{ \"German\",\"de\"},{ \"Greek\"" +
                ",\"el\"},{ \"Haitian\",\"ht\"},{ \"Hebrew\",\"iw\"},",
            "{ \"Hindi\",\"hi\"},{ \"Hungarian\",\"hu\"},{ \"Icelandic\",\"is\"},{ \"Indonesian\",\"id\"},{ \"I" +
                "rish\", \"ga\"},{ \"Italian\",\"it\"},{ \"Japanese\",\"ja\"},",
            "{ \"Korean\",\"ko\"},{ \"Latvian\",\"lv\"},{ \"Lithuanian\",\"lt\"},{ \"Macedonian\",\"mk\"},{ \"M" +
                "alay\",\"ms\"},{ \"Maltese\",\"mt\"},{ \"Norwegian\",\"no\"},",
            "{ \"Persian\",\"fa\"},{ \"Polish\",\"pl\"},{ \"Portuguese\",\"pt\"},{ \"Romanian\",\"ro\"},{ \"Rus" +
                "sian\",\"ru\"},{ \"Serbian\",\"sr\"},{ \"Slovak\",\"sk\"},",
            "{ \"Slovenian\",\"sl\"},{ \"Spanish\",\"es\"},{ \"Swahili\",\"sw\"},{ \"Swedish\",\"sv\"},{ \"Thai" +
                "\",\"th\"},{ \"Turkish\",\"tr\"},{ \"Ukrainian\",\"uk\"},",
            "{ \"Urdu\",\"ur\"},{ \"Vietnamese\",\"vi\"},{ \"Welsh\",\"cy\"},{ \"Yiddish\",\"yi\"} "});
            this.comboBox1.Location = new System.Drawing.Point(740, 642);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(73, 23);
            this.comboBox1.TabIndex = 32;
            this.comboBox1.SelectionChangeCommitted += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(839, 642);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(73, 23);
            this.comboBox2.TabIndex = 33;
            this.comboBox2.SelectionChangeCommitted += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // BottomToolStripPanel
            // 
            this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // TopToolStripPanel
            // 
            this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.TopToolStripPanel.Name = "TopToolStripPanel";
            this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // RightToolStripPanel
            // 
            this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // LeftToolStripPanel
            // 
            this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // ContentPanel
            // 
            this.ContentPanel.Size = new System.Drawing.Size(150, 130);
            // 
            // Translated_tc
            // 
            this.Translated_tc.Controls.Add(this.TranslatedHome);
            this.Translated_tc.Location = new System.Drawing.Point(500, 36);
            this.Translated_tc.Name = "Translated_tc";
            this.Translated_tc.SelectedIndex = 0;
            this.Translated_tc.Size = new System.Drawing.Size(497, 568);
            this.Translated_tc.TabIndex = 34;
            // 
            // TranslatedHome
            // 
            this.TranslatedHome.Controls.Add(this.Translated_ts);
            this.TranslatedHome.Controls.Add(this.Translated_tb);
            this.TranslatedHome.Controls.Add(this.statusStrip1);
            this.TranslatedHome.Location = new System.Drawing.Point(4, 24);
            this.TranslatedHome.Name = "TranslatedHome";
            this.TranslatedHome.Padding = new System.Windows.Forms.Padding(3);
            this.TranslatedHome.Size = new System.Drawing.Size(489, 540);
            this.TranslatedHome.TabIndex = 0;
            this.TranslatedHome.Text = "Home";
            // 
            // Translated_ts
            // 
            this.Translated_ts.BackColor = System.Drawing.SystemColors.Info;
            this.Translated_ts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SearchTranslated_tstb,
            this.SearchTranslated_tsb,
            this.TranslatedFirst_tsb,
            this.TranslatedPrev_tsb,
            this.TranslatedNext_tsb,
            this.TranslatedLast_tsb,
            this.toolStripSeparator2,
            this.toolStripLabel2,
            this.CloseTS_tsb});
            this.Translated_ts.Location = new System.Drawing.Point(3, 3);
            this.Translated_ts.Name = "Translated_ts";
            this.Translated_ts.Size = new System.Drawing.Size(483, 25);
            this.Translated_ts.TabIndex = 38;
            this.Translated_ts.Text = "toolStrip1";
            // 
            // SearchTranslated_tstb
            // 
            this.SearchTranslated_tstb.AcceptsReturn = true;
            this.SearchTranslated_tstb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SearchTranslated_tstb.Name = "SearchTranslated_tstb";
            this.SearchTranslated_tstb.ReadOnly = true;
            this.SearchTranslated_tstb.Size = new System.Drawing.Size(250, 25);
            this.SearchTranslated_tstb.ToolTipText = "Type text for searching here...";
            // 
            // SearchTranslated_tsb
            // 
            this.SearchTranslated_tsb.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SearchTranslated_tsb.Image = global::Rusik.Properties.Resources.Search;
            this.SearchTranslated_tsb.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SearchTranslated_tsb.Name = "SearchTranslated_tsb";
            this.SearchTranslated_tsb.Size = new System.Drawing.Size(23, 22);
            this.SearchTranslated_tsb.Text = "Search";
            this.SearchTranslated_tsb.ToolTipText = "Go search!";
            // 
            // TranslatedPrev_tsb
            // 
            this.TranslatedPrev_tsb.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TranslatedPrev_tsb.Image = global::Rusik.Properties.Resources.Prev;
            this.TranslatedPrev_tsb.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TranslatedPrev_tsb.Name = "TranslatedPrev_tsb";
            this.TranslatedPrev_tsb.Size = new System.Drawing.Size(23, 22);
            this.TranslatedPrev_tsb.Text = "Previous";
            this.TranslatedPrev_tsb.ToolTipText = "Previous";
            // 
            // TranslatedNext_tsb
            // 
            this.TranslatedNext_tsb.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TranslatedNext_tsb.Image = global::Rusik.Properties.Resources.Next;
            this.TranslatedNext_tsb.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TranslatedNext_tsb.Name = "TranslatedNext_tsb";
            this.TranslatedNext_tsb.Size = new System.Drawing.Size(23, 22);
            this.TranslatedNext_tsb.Text = "Next";
            this.TranslatedNext_tsb.ToolTipText = "Next";
            // 
            // TranslatedLast_tsb
            // 
            this.TranslatedLast_tsb.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TranslatedLast_tsb.Image = global::Rusik.Properties.Resources.ToLast;
            this.TranslatedLast_tsb.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TranslatedLast_tsb.Name = "TranslatedLast_tsb";
            this.TranslatedLast_tsb.Size = new System.Drawing.Size(23, 22);
            this.TranslatedLast_tsb.Text = "Last";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(0, 22);
            // 
            // CloseTS_tsb
            // 
            this.CloseTS_tsb.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CloseTS_tsb.Image = global::Rusik.Properties.Resources.cross;
            this.CloseTS_tsb.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CloseTS_tsb.Name = "CloseTS_tsb";
            this.CloseTS_tsb.Size = new System.Drawing.Size(23, 22);
            this.CloseTS_tsb.Text = "Close";
            this.CloseTS_tsb.ToolTipText = "Close Search";
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.Info;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.lbTranslated});
            this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.statusStrip1.Location = new System.Drawing.Point(3, 517);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(483, 20);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(161, 15);
            this.toolStripStatusLabel1.Text = "Translated message symbols:";
            // 
            // lbTranslated
            // 
            this.lbTranslated.Name = "lbTranslated";
            this.lbTranslated.Size = new System.Drawing.Size(0, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Wingdings 3", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(813, 646);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 35;
            this.label1.Text = "->";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(677, 640);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 30);
            this.label2.TabIndex = 36;
            this.label2.Text = "Direction\r\ntranslator";
            // 
            // SourceFile_tb
            // 
            this.SourceFile_tb.AutoSize = true;
            this.SourceFile_tb.BackColor = System.Drawing.Color.ForestGreen;
            this.SourceFile_tb.Location = new System.Drawing.Point(16, 709);
            this.SourceFile_tb.Name = "SourceFile_tb";
            this.SourceFile_tb.Size = new System.Drawing.Size(0, 15);
            this.SourceFile_tb.TabIndex = 37;
            // 
            // TranslatedFile_tb
            // 
            this.TranslatedFile_tb.AutoSize = true;
            this.TranslatedFile_tb.BackColor = System.Drawing.Color.Lime;
            this.TranslatedFile_tb.Location = new System.Drawing.Point(19, 709);
            this.TranslatedFile_tb.Name = "TranslatedFile_tb";
            this.TranslatedFile_tb.Size = new System.Drawing.Size(0, 15);
            this.TranslatedFile_tb.TabIndex = 38;
            // 
            // Delete_btn
            // 
            this.Delete_btn.Image = global::Rusik.Properties.Resources.Basket;
            this.Delete_btn.Location = new System.Drawing.Point(12, 611);
            this.Delete_btn.Name = "Delete_btn";
            this.Delete_btn.Size = new System.Drawing.Size(41, 40);
            this.Delete_btn.TabIndex = 39;
            this.Delete_btn.UseVisualStyleBackColor = true;
            this.Delete_btn.Click += new System.EventHandler(this.Delete_btn_Click);
            // 
            // TranslatedFirst_tsb
            // 
            this.TranslatedFirst_tsb.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.TranslatedFirst_tsb.Image = global::Rusik.Properties.Resources.ToFirst;
            this.TranslatedFirst_tsb.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TranslatedFirst_tsb.Name = "TranslatedFirst_tsb";
            this.TranslatedFirst_tsb.Size = new System.Drawing.Size(23, 22);
            this.TranslatedFirst_tsb.Text = "First";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.Delete_btn);
            this.Controls.Add(this.TranslatedFile_tb);
            this.Controls.Add(this.SourceFile_tb);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Translated_tc);
            this.Controls.Add(this.progressBar1_lb);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.Start_btn);
            this.Controls.Add(this.Signature_tb);
            this.Controls.Add(this.Signature_lb);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.Translate_btn);
            this.Controls.Add(this.Records_lb);
            this.Controls.Add(this.Offset_lb);
            this.Controls.Add(this.Offset_tb);
            this.Controls.Add(this.nudRecord);
            this.Controls.Add(this.Next_btn);
            this.Controls.Add(this.Prev_btn);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.Source_tc);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Help Custom Translator";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudRecord)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.Source_tc.ResumeLayout(false);
            this.HomeSource.ResumeLayout(false);
            this.HomeSource.PerformLayout();
            this.Source_ts.ResumeLayout(false);
            this.Source_ts.PerformLayout();
            this.statusStrip2.ResumeLayout(false);
            this.statusStrip2.PerformLayout();
            this.Translated_tc.ResumeLayout(false);
            this.TranslatedHome.ResumeLayout(false);
            this.TranslatedHome.PerformLayout();
            this.Translated_ts.ResumeLayout(false);
            this.Translated_ts.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.TextBox Offset_tb;
        private System.Windows.Forms.Label Offset_lb;
        private System.Windows.Forms.Label Records_lb;
        private System.Windows.Forms.Button Translate_btn;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label Signature_lb;
        private System.Windows.Forms.TextBox Signature_tb;
        private System.Windows.Forms.Button Start_btn;
        private System.Windows.Forms.Label progressBar1_lb;
        private System.Windows.Forms.TabControl Source_tc;
        private System.Windows.Forms.TextBox Source_tb;
        private System.Windows.Forms.ToolStripMenuItem OpenTranslatedFile_tsmi;
        private System.Windows.Forms.ToolStripMenuItem CloseFilesClear_tsmi;
        private System.Windows.Forms.TextBox Translated_tb;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel lbSource;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.TabPage HomeSource;
        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel;
        private System.Windows.Forms.TabControl Translated_tc;
        private System.Windows.Forms.TabPage TranslatedHome;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lbTranslated;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStrip Source_ts;
        private System.Windows.Forms.ToolStrip Translated_ts;
        private System.Windows.Forms.Label SourceFile_tb;
        private System.Windows.Forms.Label TranslatedFile_tb;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox SearchSource_tstb;
        private System.Windows.Forms.ToolStripButton SearchSource_tsb;
        private System.Windows.Forms.ToolStripButton SourcePrev_tsb;
        private System.Windows.Forms.ToolStripButton SourceNext_tsb;
        private System.Windows.Forms.ToolStripButton SourceLast_tsb;
        private System.Windows.Forms.ToolStripLabel SearchStat_tslb;
        private System.Windows.Forms.ToolStripTextBox SearchTranslated_tstb;
        private System.Windows.Forms.ToolStripButton SearchTranslated_tsb;
        private System.Windows.Forms.ToolStripButton TranslatedPrev_tsb;
        private System.Windows.Forms.ToolStripButton TranslatedNext_tsb;
        private System.Windows.Forms.ToolStripButton TranslatedLast_tsb;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Button Delete_btn;
        private System.Windows.Forms.ToolStripButton Close;
        private System.Windows.Forms.ToolStripButton CloseTS_tsb;
        private System.Windows.Forms.ToolStripButton SourceFirst_tsb;
        private System.Windows.Forms.ToolStripButton TranslatedFirst_tsb;
    }
}


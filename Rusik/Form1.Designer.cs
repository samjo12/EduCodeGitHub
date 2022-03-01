
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
            this.lbTranslated = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.File_tsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFile_tsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenTranslatedFile_tsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.closeFilesClearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveFile_tsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.Quit_tsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.About_tsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.Offset_tb = new System.Windows.Forms.TextBox();
            this.Offset_lb = new System.Windows.Forms.Label();
            this.Offset_hp = new System.Windows.Forms.HelpProvider();
            this.Records_lb = new System.Windows.Forms.Label();
            this.Translate_btn = new System.Windows.Forms.Button();
            this.Translated_tb = new System.Windows.Forms.TextBox();
            this.SourceFile_lb = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SearchTranslated_tb = new System.Windows.Forms.TextBox();
            this.SearchTranslated_btn = new System.Windows.Forms.Button();
            this.SearchSource_btn = new System.Windows.Forms.Button();
            this.SearchSource_tb = new System.Windows.Forms.TextBox();
            this.OutputFile_lb = new System.Windows.Forms.Label();
            this.SourceFile_tb = new System.Windows.Forms.TextBox();
            this.Signature_lb = new System.Windows.Forms.Label();
            this.Signature_tb = new System.Windows.Forms.TextBox();
            this.TranslatedFile_tb = new System.Windows.Forms.TextBox();
            this.Start_btn = new System.Windows.Forms.Button();
            this.progressBar1_lb = new System.Windows.Forms.Label();
            this.Source_tc = new System.Windows.Forms.TabControl();
            this.Source_tab = new System.Windows.Forms.TabPage();
            this.Source_tb = new System.Windows.Forms.TextBox();
            this.lbSource = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudRecord)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.Source_tc.SuspendLayout();
            this.Source_tab.SuspendLayout();
            this.SuspendLayout();
            // 
            // Prev_btn
            // 
            this.Prev_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.Prev_btn.Location = new System.Drawing.Point(422, 631);
            this.Prev_btn.Name = "Prev_btn";
            this.Prev_btn.Size = new System.Drawing.Size(75, 23);
            this.Prev_btn.TabIndex = 3;
            this.Prev_btn.Text = "Previous";
            this.Prev_btn.UseVisualStyleBackColor = false;
            this.Prev_btn.Click += new System.EventHandler(this.Prev_btn_Click);
            // 
            // Next_btn
            // 
            this.Next_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.Next_btn.Location = new System.Drawing.Point(511, 631);
            this.Next_btn.Name = "Next_btn";
            this.Next_btn.Size = new System.Drawing.Size(75, 23);
            this.Next_btn.TabIndex = 4;
            this.Next_btn.Text = "Next";
            this.Next_btn.UseVisualStyleBackColor = false;
            this.Next_btn.Click += new System.EventHandler(this.Next_btn_Click);
            // 
            // nudRecord
            // 
            this.nudRecord.Location = new System.Drawing.Point(592, 631);
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
            // lbTranslated
            // 
            this.lbTranslated.AutoSize = true;
            this.lbTranslated.Location = new System.Drawing.Point(511, 606);
            this.lbTranslated.Name = "lbTranslated";
            this.lbTranslated.Size = new System.Drawing.Size(170, 15);
            this.lbTranslated.TabIndex = 10;
            this.lbTranslated.Text = "Translated Message: 0 symbols";
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
            this.closeFilesClearToolStripMenuItem,
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
            // closeFilesClearToolStripMenuItem
            // 
            this.closeFilesClearToolStripMenuItem.Name = "closeFilesClearToolStripMenuItem";
            this.closeFilesClearToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.closeFilesClearToolStripMenuItem.Text = "Close Files/Clear";
            this.closeFilesClearToolStripMenuItem.Click += new System.EventHandler(this.closeFilesClearToolStripMenuItem_Click);
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
            this.Offset_tb.Location = new System.Drawing.Point(66, 662);
            this.Offset_tb.Name = "Offset_tb";
            this.Offset_tb.PlaceholderText = "HEX number";
            this.Offset_tb.ReadOnly = true;
            this.Offset_tb.Size = new System.Drawing.Size(115, 23);
            this.Offset_tb.TabIndex = 12;
            // 
            // Offset_lb
            // 
            this.Offset_lb.AutoSize = true;
            this.Offset_lb.Location = new System.Drawing.Point(12, 667);
            this.Offset_lb.Name = "Offset_lb";
            this.Offset_lb.Size = new System.Drawing.Size(53, 15);
            this.Offset_lb.TabIndex = 13;
            this.Offset_lb.Text = "Offset 0x";
            // 
            // Records_lb
            // 
            this.Records_lb.AutoSize = true;
            this.Records_lb.Location = new System.Drawing.Point(651, 635);
            this.Records_lb.Name = "Records_lb";
            this.Records_lb.Size = new System.Drawing.Size(95, 15);
            this.Records_lb.TabIndex = 14;
            this.Records_lb.Text = "Found: 0 records";
            // 
            // Translate_btn
            // 
            this.Translate_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.Translate_btn.Location = new System.Drawing.Point(777, 631);
            this.Translate_btn.Name = "Translate_btn";
            this.Translate_btn.Size = new System.Drawing.Size(75, 23);
            this.Translate_btn.TabIndex = 15;
            this.Translate_btn.Text = "Translate";
            this.Translate_btn.UseVisualStyleBackColor = false;
            this.Translate_btn.Click += new System.EventHandler(this.Translate_btn_Click);
            // 
            // Translated_tb
            // 
            this.Translated_tb.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Translated_tb.Location = new System.Drawing.Point(495, 0);
            this.Translated_tb.Multiline = true;
            this.Translated_tb.Name = "Translated_tb";
            this.Translated_tb.ReadOnly = true;
            this.Translated_tb.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Translated_tb.Size = new System.Drawing.Size(481, 522);
            this.Translated_tb.TabIndex = 17;
            // 
            // SourceFile_lb
            // 
            this.SourceFile_lb.AutoSize = true;
            this.SourceFile_lb.Location = new System.Drawing.Point(15, 28);
            this.SourceFile_lb.Name = "SourceFile_lb";
            this.SourceFile_lb.Size = new System.Drawing.Size(67, 15);
            this.SourceFile_lb.TabIndex = 18;
            this.SourceFile_lb.Text = "Binary File: ";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(651, 660);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(345, 26);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 19;
            // 
            // SearchTranslated_tb
            // 
            this.SearchTranslated_tb.Location = new System.Drawing.Point(511, 694);
            this.SearchTranslated_tb.Name = "SearchTranslated_tb";
            this.SearchTranslated_tb.PlaceholderText = "Some text for searching ...";
            this.SearchTranslated_tb.ReadOnly = true;
            this.SearchTranslated_tb.Size = new System.Drawing.Size(430, 23);
            this.SearchTranslated_tb.TabIndex = 20;
            // 
            // SearchTranslated_btn
            // 
            this.SearchTranslated_btn.Location = new System.Drawing.Point(939, 694);
            this.SearchTranslated_btn.Name = "SearchTranslated_btn";
            this.SearchTranslated_btn.Size = new System.Drawing.Size(57, 23);
            this.SearchTranslated_btn.TabIndex = 21;
            this.SearchTranslated_btn.Text = "Search";
            this.SearchTranslated_btn.UseVisualStyleBackColor = true;
            this.SearchTranslated_btn.Click += new System.EventHandler(this.SearchTranslated_btn_Click);
            // 
            // SearchSource_btn
            // 
            this.SearchSource_btn.Location = new System.Drawing.Point(422, 694);
            this.SearchSource_btn.Name = "SearchSource_btn";
            this.SearchSource_btn.Size = new System.Drawing.Size(75, 23);
            this.SearchSource_btn.TabIndex = 23;
            this.SearchSource_btn.Text = "Search";
            this.SearchSource_btn.UseVisualStyleBackColor = true;
            this.SearchSource_btn.Click += new System.EventHandler(this.SearchSource_Click);
            // 
            // SearchSource_tb
            // 
            this.SearchSource_tb.Location = new System.Drawing.Point(12, 694);
            this.SearchSource_tb.Name = "SearchSource_tb";
            this.SearchSource_tb.PlaceholderText = "Some text for searching ...";
            this.SearchSource_tb.ReadOnly = true;
            this.SearchSource_tb.Size = new System.Drawing.Size(411, 23);
            this.SearchSource_tb.TabIndex = 22;
            // 
            // OutputFile_lb
            // 
            this.OutputFile_lb.AutoSize = true;
            this.OutputFile_lb.Location = new System.Drawing.Point(511, 28);
            this.OutputFile_lb.Name = "OutputFile_lb";
            this.OutputFile_lb.Size = new System.Drawing.Size(114, 15);
            this.OutputFile_lb.TabIndex = 24;
            this.OutputFile_lb.Text = "Translated Text File: ";
            // 
            // SourceFile_tb
            // 
            this.SourceFile_tb.Location = new System.Drawing.Point(88, 24);
            this.SourceFile_tb.Name = "SourceFile_tb";
            this.SourceFile_tb.ReadOnly = true;
            this.SourceFile_tb.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.SourceFile_tb.Size = new System.Drawing.Size(409, 23);
            this.SourceFile_tb.TabIndex = 25;
            // 
            // Signature_lb
            // 
            this.Signature_lb.AutoSize = true;
            this.Signature_lb.Location = new System.Drawing.Point(187, 667);
            this.Signature_lb.Name = "Signature_lb";
            this.Signature_lb.Size = new System.Drawing.Size(71, 15);
            this.Signature_lb.TabIndex = 26;
            this.Signature_lb.Text = "Signature 0x";
            // 
            // Signature_tb
            // 
            this.Signature_tb.Location = new System.Drawing.Point(261, 662);
            this.Signature_tb.Name = "Signature_tb";
            this.Signature_tb.PlaceholderText = "HEX number";
            this.Signature_tb.ReadOnly = true;
            this.Signature_tb.Size = new System.Drawing.Size(162, 23);
            this.Signature_tb.TabIndex = 27;
            // 
            // TranslatedFile_tb
            // 
            this.TranslatedFile_tb.Location = new System.Drawing.Point(628, 24);
            this.TranslatedFile_tb.Name = "TranslatedFile_tb";
            this.TranslatedFile_tb.ReadOnly = true;
            this.TranslatedFile_tb.Size = new System.Drawing.Size(364, 23);
            this.TranslatedFile_tb.TabIndex = 28;
            // 
            // Start_btn
            // 
            this.Start_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Start_btn.Font = new System.Drawing.Font("Segoe UI Black", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Start_btn.Location = new System.Drawing.Point(422, 662);
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
            this.progressBar1_lb.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.progressBar1_lb.Location = new System.Drawing.Point(605, 666);
            this.progressBar1_lb.Name = "progressBar1_lb";
            this.progressBar1_lb.Size = new System.Drawing.Size(40, 15);
            this.progressBar1_lb.TabIndex = 30;
            // 
            // Source_tc
            // 
            this.Source_tc.Controls.Add(this.Source_tab);
            this.Source_tc.Location = new System.Drawing.Point(12, 53);
            this.Source_tc.Name = "Source_tc";
            this.Source_tc.SelectedIndex = 0;
            this.Source_tc.Size = new System.Drawing.Size(984, 550);
            this.Source_tc.TabIndex = 31;
            // 
            // Source_tab
            // 
            this.Source_tab.Controls.Add(this.Source_tb);
            this.Source_tab.Controls.Add(this.Translated_tb);
            this.Source_tab.Location = new System.Drawing.Point(4, 24);
            this.Source_tab.Name = "Source_tab";
            this.Source_tab.Padding = new System.Windows.Forms.Padding(3);
            this.Source_tab.Size = new System.Drawing.Size(976, 522);
            this.Source_tab.TabIndex = 0;
            this.Source_tab.Text = "All";
            this.Source_tab.UseVisualStyleBackColor = true;
            // 
            // Source_tb
            // 
            this.Source_tb.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Source_tb.Location = new System.Drawing.Point(-3, 0);
            this.Source_tb.Multiline = true;
            this.Source_tb.Name = "Source_tb";
            this.Source_tb.ReadOnly = true;
            this.Source_tb.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Source_tb.Size = new System.Drawing.Size(484, 522);
            this.Source_tb.TabIndex = 18;
            // 
            // lbSource
            // 
            this.lbSource.AutoSize = true;
            this.lbSource.Location = new System.Drawing.Point(16, 606);
            this.lbSource.Name = "lbSource";
            this.lbSource.Size = new System.Drawing.Size(151, 15);
            this.lbSource.TabIndex = 9;
            this.lbSource.Text = "Source message: 0 symbols";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.progressBar1_lb);
            this.Controls.Add(this.Source_tc);
            this.Controls.Add(this.Start_btn);
            this.Controls.Add(this.TranslatedFile_tb);
            this.Controls.Add(this.Signature_tb);
            this.Controls.Add(this.Signature_lb);
            this.Controls.Add(this.SourceFile_tb);
            this.Controls.Add(this.OutputFile_lb);
            this.Controls.Add(this.SearchSource_btn);
            this.Controls.Add(this.SearchSource_tb);
            this.Controls.Add(this.SearchTranslated_btn);
            this.Controls.Add(this.SearchTranslated_tb);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.SourceFile_lb);
            this.Controls.Add(this.Translate_btn);
            this.Controls.Add(this.Records_lb);
            this.Controls.Add(this.Offset_lb);
            this.Controls.Add(this.Offset_tb);
            this.Controls.Add(this.lbTranslated);
            this.Controls.Add(this.lbSource);
            this.Controls.Add(this.nudRecord);
            this.Controls.Add(this.Next_btn);
            this.Controls.Add(this.Prev_btn);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Binary Files Text Translator";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudRecord)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.Source_tc.ResumeLayout(false);
            this.Source_tab.ResumeLayout(false);
            this.Source_tab.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Prev_btn;
        private System.Windows.Forms.Button Next_btn;
        private System.Windows.Forms.NumericUpDown nudRecord;
        private System.Windows.Forms.Label lbTranslated;
       /// private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem File_tsmi;
        private System.Windows.Forms.ToolStripMenuItem OpenFile_tsmi;
        private System.Windows.Forms.ToolStripMenuItem SaveFile_tsmi;
        private System.Windows.Forms.ToolStripMenuItem Quit_tsmi;
        private System.Windows.Forms.ToolStripMenuItem About_tsmi;
        private System.Windows.Forms.TextBox Offset_tb;
        private System.Windows.Forms.Label Offset_lb;
        private System.Windows.Forms.HelpProvider Offset_hp;
        private System.Windows.Forms.Label Records_lb;
        private System.Windows.Forms.Button Translate_btn;
        private System.Windows.Forms.TextBox Translated_tb;
        private System.Windows.Forms.Label SourceFile_lb;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox SearchTranslated_tb;
        private System.Windows.Forms.Button SearchTranslated_btn;
        private System.Windows.Forms.Button SearchSource_btn;
        private System.Windows.Forms.TextBox SearchSource_tb;
        private System.Windows.Forms.Label OutputFile_lb;
        private System.Windows.Forms.TextBox SourceFile_tb;
        private System.Windows.Forms.Label Signature_lb;
        private System.Windows.Forms.TextBox Signature_tb;
        private System.Windows.Forms.TextBox TranslatedFile_tb;
        private System.Windows.Forms.Button Start_btn;
        private System.Windows.Forms.Label progressBar1_lb;
        private System.Windows.Forms.TabControl Source_tc;
        private System.Windows.Forms.TabPage Source_tab;
        private System.Windows.Forms.TextBox Source_tb;
        private System.Windows.Forms.ToolStripMenuItem OpenTranslatedFile_tsmi;
        private System.Windows.Forms.ToolStripMenuItem closeFilesClearToolStripMenuItem;
        private System.Windows.Forms.Label lbSource;
    }
}


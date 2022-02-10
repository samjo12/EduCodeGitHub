
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
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.nudRecord = new System.Windows.Forms.NumericUpDown();
            this.lbSource = new System.Windows.Forms.Label();
            this.lbTranslated = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.File_tsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFile_tsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveFile_tsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.Quit_tsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.About_tsmi = new System.Windows.Forms.ToolStripMenuItem();
            this.FileOffset_tb = new System.Windows.Forms.TextBox();
            this.Offset_lb = new System.Windows.Forms.Label();
            this.Offset_hp = new System.Windows.Forms.HelpProvider();
            this.Records_ld = new System.Windows.Forms.Label();
            this.Translate_btn = new System.Windows.Forms.Button();
            this.Source_tb = new System.Windows.Forms.TextBox();
            this.Translated_tb = new System.Windows.Forms.TextBox();
            this.Fileopened_lb = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudRecord)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPrev
            // 
            this.btnPrev.Location = new System.Drawing.Point(278, 414);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(75, 23);
            this.btnPrev.TabIndex = 3;
            this.btnPrev.Text = "Prev";
            this.btnPrev.UseVisualStyleBackColor = true;
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(437, 414);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 4;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            // 
            // nudRecord
            // 
            this.nudRecord.Location = new System.Drawing.Point(368, 414);
            this.nudRecord.Name = "nudRecord";
            this.nudRecord.Size = new System.Drawing.Size(56, 23);
            this.nudRecord.TabIndex = 8;
            // 
            // lbSource
            // 
            this.lbSource.AutoSize = true;
            this.lbSource.Location = new System.Drawing.Point(13, 28);
            this.lbSource.Name = "lbSource";
            this.lbSource.Size = new System.Drawing.Size(92, 15);
            this.lbSource.TabIndex = 9;
            this.lbSource.Text = "Source message";
            // 
            // lbTranslated
            // 
            this.lbTranslated.AutoSize = true;
            this.lbTranslated.Location = new System.Drawing.Point(676, 27);
            this.lbTranslated.Name = "lbTranslated";
            this.lbTranslated.Size = new System.Drawing.Size(111, 15);
            this.lbTranslated.TabIndex = 10;
            this.lbTranslated.Text = "Translated Message";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.File_tsmi,
            this.About_tsmi});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // File_tsmi
            // 
            this.File_tsmi.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenFile_tsmi,
            this.SaveFile_tsmi,
            this.Quit_tsmi});
            this.File_tsmi.Name = "File_tsmi";
            this.File_tsmi.Size = new System.Drawing.Size(37, 20);
            this.File_tsmi.Text = "File";
            // 
            // OpenFile_tsmi
            // 
            this.OpenFile_tsmi.Name = "OpenFile_tsmi";
            this.OpenFile_tsmi.Size = new System.Drawing.Size(144, 22);
            this.OpenFile_tsmi.Text = "Open File";
            this.OpenFile_tsmi.Click += new System.EventHandler(this.OpenFile_tsmi_Click);
            // 
            // SaveFile_tsmi
            // 
            this.SaveFile_tsmi.Name = "SaveFile_tsmi";
            this.SaveFile_tsmi.Size = new System.Drawing.Size(144, 22);
            this.SaveFile_tsmi.Text = "Save File As...";
            // 
            // Quit_tsmi
            // 
            this.Quit_tsmi.Name = "Quit_tsmi";
            this.Quit_tsmi.Size = new System.Drawing.Size(144, 22);
            this.Quit_tsmi.Text = "Quit";
            // 
            // About_tsmi
            // 
            this.About_tsmi.Name = "About_tsmi";
            this.About_tsmi.Size = new System.Drawing.Size(52, 20);
            this.About_tsmi.Text = "About";
            this.About_tsmi.Click += new System.EventHandler(this.About_tsmi_Click);
            // 
            // FileOffset_tb
            // 
            this.FileOffset_tb.Location = new System.Drawing.Point(144, 415);
            this.FileOffset_tb.Name = "FileOffset_tb";
            this.FileOffset_tb.Size = new System.Drawing.Size(115, 23);
            this.FileOffset_tb.TabIndex = 12;
            // 
            // Offset_lb
            // 
            this.Offset_lb.AutoSize = true;
            this.Offset_lb.Location = new System.Drawing.Point(13, 420);
            this.Offset_lb.Name = "Offset_lb";
            this.Offset_lb.Size = new System.Drawing.Size(128, 15);
            this.Offset_lb.TabIndex = 13;
            this.Offset_lb.Text = "Offset from beginning:";
            // 
            // Records_ld
            // 
            this.Records_ld.AutoSize = true;
            this.Records_ld.Location = new System.Drawing.Point(624, 418);
            this.Records_ld.Name = "Records_ld";
            this.Records_ld.Size = new System.Drawing.Size(95, 15);
            this.Records_ld.TabIndex = 14;
            this.Records_ld.Text = "Found: 0 records";
            // 
            // Translate_btn
            // 
            this.Translate_btn.Location = new System.Drawing.Point(526, 414);
            this.Translate_btn.Name = "Translate_btn";
            this.Translate_btn.Size = new System.Drawing.Size(75, 23);
            this.Translate_btn.TabIndex = 15;
            this.Translate_btn.Text = "Translate";
            this.Translate_btn.UseVisualStyleBackColor = true;
            // 
            // Source_tb
            // 
            this.Source_tb.Location = new System.Drawing.Point(12, 48);
            this.Source_tb.Multiline = true;
            this.Source_tb.Name = "Source_tb";
            this.Source_tb.Size = new System.Drawing.Size(376, 360);
            this.Source_tb.TabIndex = 16;
            // 
            // Translated_tb
            // 
            this.Translated_tb.Location = new System.Drawing.Point(406, 48);
            this.Translated_tb.Multiline = true;
            this.Translated_tb.Name = "Translated_tb";
            this.Translated_tb.Size = new System.Drawing.Size(382, 360);
            this.Translated_tb.TabIndex = 17;
            // 
            // Fileopened_lb
            // 
            this.Fileopened_lb.AutoSize = true;
            this.Fileopened_lb.Location = new System.Drawing.Point(144, 28);
            this.Fileopened_lb.Name = "Fileopened_lb";
            this.Fileopened_lb.Size = new System.Drawing.Size(31, 15);
            this.Fileopened_lb.TabIndex = 18;
            this.Fileopened_lb.Text = "File: ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Fileopened_lb);
            this.Controls.Add(this.Translated_tb);
            this.Controls.Add(this.Source_tb);
            this.Controls.Add(this.Translate_btn);
            this.Controls.Add(this.Records_ld);
            this.Controls.Add(this.Offset_lb);
            this.Controls.Add(this.FileOffset_tb);
            this.Controls.Add(this.lbTranslated);
            this.Controls.Add(this.lbSource);
            this.Controls.Add(this.nudRecord);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrev);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Binary Files Text Translator";
            ((System.ComponentModel.ISupportInitialize)(this.nudRecord)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.NumericUpDown nudRecord;
        private System.Windows.Forms.Label lbSource;
        private System.Windows.Forms.Label lbTranslated;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem File_tsmi;
        private System.Windows.Forms.ToolStripMenuItem OpenFile_tsmi;
        private System.Windows.Forms.ToolStripMenuItem SaveFile_tsmi;
        private System.Windows.Forms.ToolStripMenuItem Quit_tsmi;
        private System.Windows.Forms.ToolStripMenuItem About_tsmi;
        private System.Windows.Forms.TextBox FileOffset_tb;
        private System.Windows.Forms.Label Offset_lb;
        private System.Windows.Forms.HelpProvider Offset_hp;
        private System.Windows.Forms.Label Records_ld;
        private System.Windows.Forms.Button Translate_btn;
        private System.Windows.Forms.TextBox Source_tb;
        private System.Windows.Forms.TextBox Translated_tb;
        private System.Windows.Forms.Label Fileopened_lb;
    }
}


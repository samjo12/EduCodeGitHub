
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
            this.rtbSource = new System.Windows.Forms.RichTextBox();
            this.rtbTranslated = new System.Windows.Forms.RichTextBox();
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
            ((System.ComponentModel.ISupportInitialize)(this.nudRecord)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtbSource
            // 
            this.rtbSource.Location = new System.Drawing.Point(13, 48);
            this.rtbSource.Name = "rtbSource";
            this.rtbSource.ReadOnly = true;
            this.rtbSource.Size = new System.Drawing.Size(374, 361);
            this.rtbSource.TabIndex = 1;
            this.rtbSource.Text = "";
            // 
            // rtbTranslated
            // 
            this.rtbTranslated.Location = new System.Drawing.Point(405, 48);
            this.rtbTranslated.Name = "rtbTranslated";
            this.rtbTranslated.Size = new System.Drawing.Size(383, 361);
            this.rtbTranslated.TabIndex = 2;
            this.rtbTranslated.Text = "";
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
            this.lbTranslated.Size = new System.Drawing.Size(109, 15);
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
            this.OpenFile_tsmi.Size = new System.Drawing.Size(180, 22);
            this.OpenFile_tsmi.Text = "Open File";
            this.OpenFile_tsmi.Click += new System.EventHandler(this.OpenFile_tsmi_Click);
            // 
            // SaveFile_tsmi
            // 
            this.SaveFile_tsmi.Name = "SaveFile_tsmi";
            this.SaveFile_tsmi.Size = new System.Drawing.Size(180, 22);
            this.SaveFile_tsmi.Text = "Save File As...";
            // 
            // Quit_tsmi
            // 
            this.Quit_tsmi.Name = "Quit_tsmi";
            this.Quit_tsmi.Size = new System.Drawing.Size(180, 22);
            this.Quit_tsmi.Text = "Quit";
            // 
            // About_tsmi
            // 
            this.About_tsmi.Name = "About_tsmi";
            this.About_tsmi.Size = new System.Drawing.Size(52, 20);
            this.About_tsmi.Text = "About";
            this.About_tsmi.Click += new System.EventHandler(this.About_tsmi_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lbTranslated);
            this.Controls.Add(this.lbSource);
            this.Controls.Add(this.nudRecord);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrev);
            this.Controls.Add(this.rtbTranslated);
            this.Controls.Add(this.rtbSource);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.nudRecord)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbSource;
        private System.Windows.Forms.RichTextBox rtbTranslated;
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
    }
}


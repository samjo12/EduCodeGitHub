
namespace MyUtils
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.помощьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.блокнотToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiInsertDate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiInsertTime = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiNotepadClear = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiNotepadSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNotepadLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.MyTabs = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.chkboxGenerator1 = new System.Windows.Forms.CheckBox();
            this.btnGeneratorCopy = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.tbGenerator = new System.Windows.Forms.TextBox();
            this.lblGenerator = new System.Windows.Forms.Label();
            this.btnGenerator = new System.Windows.Forms.Button();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblCount = new System.Windows.Forms.Label();
            this.btnRst = new System.Windows.Forms.Button();
            this.btnMinus = new System.Windows.Forms.Button();
            this.btnPlus = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.MyTabs.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.помощьToolStripMenuItem,
            this.блокнотToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(586, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_Exit});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // ToolStripMenuItem_Exit
            // 
            this.ToolStripMenuItem_Exit.Name = "ToolStripMenuItem_Exit";
            this.ToolStripMenuItem_Exit.Size = new System.Drawing.Size(108, 22);
            this.ToolStripMenuItem_Exit.Text = "Выход";
            this.ToolStripMenuItem_Exit.Click += new System.EventHandler(this.ToolStripMenuItem_Exit_Click);
            // 
            // помощьToolStripMenuItem
            // 
            this.помощьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAbout});
            this.помощьToolStripMenuItem.Name = "помощьToolStripMenuItem";
            this.помощьToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.помощьToolStripMenuItem.Text = "Помощь";
            // 
            // tsmiAbout
            // 
            this.tsmiAbout.Name = "tsmiAbout";
            this.tsmiAbout.Size = new System.Drawing.Size(149, 22);
            this.tsmiAbout.Text = "О программе";
            this.tsmiAbout.Click += new System.EventHandler(this.tsmiAbout_Click);
            // 
            // блокнотToolStripMenuItem
            // 
            this.блокнотToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiInsertDate,
            this.tsmiInsertTime,
            this.toolStripMenuItem1,
            this.tsmiNotepadClear,
            this.toolStripMenuItem2,
            this.tsmiNotepadSave,
            this.tsmiNotepadLoad});
            this.блокнотToolStripMenuItem.Name = "блокнотToolStripMenuItem";
            this.блокнотToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.блокнотToolStripMenuItem.Text = "Блокнот";
            // 
            // tsmiInsertDate
            // 
            this.tsmiInsertDate.Name = "tsmiInsertDate";
            this.tsmiInsertDate.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.D)));
            this.tsmiInsertDate.Size = new System.Drawing.Size(232, 22);
            this.tsmiInsertDate.Text = "Вставить дату";
            this.tsmiInsertDate.Click += new System.EventHandler(this.tsmiInsertDate_Click);
            // 
            // tsmiInsertTime
            // 
            this.tsmiInsertTime.Name = "tsmiInsertTime";
            this.tsmiInsertTime.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.T)));
            this.tsmiInsertTime.Size = new System.Drawing.Size(232, 22);
            this.tsmiInsertTime.Text = "Вставить время";
            this.tsmiInsertTime.Click += new System.EventHandler(this.tsmiInsertTime_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(229, 6);
            // 
            // tsmiNotepadClear
            // 
            this.tsmiNotepadClear.Name = "tsmiNotepadClear";
            this.tsmiNotepadClear.Size = new System.Drawing.Size(232, 22);
            this.tsmiNotepadClear.Text = "Очистить";
            this.tsmiNotepadClear.Click += new System.EventHandler(this.tsmiNotepadClear_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(229, 6);
            // 
            // tsmiNotepadSave
            // 
            this.tsmiNotepadSave.Name = "tsmiNotepadSave";
            this.tsmiNotepadSave.Size = new System.Drawing.Size(232, 22);
            this.tsmiNotepadSave.Text = "Сохранить";
            this.tsmiNotepadSave.Click += new System.EventHandler(this.tsmiNotepadSave_Click);
            // 
            // tsmiNotepadLoad
            // 
            this.tsmiNotepadLoad.Name = "tsmiNotepadLoad";
            this.tsmiNotepadLoad.Size = new System.Drawing.Size(232, 22);
            this.tsmiNotepadLoad.Text = "Загрузить";
            this.tsmiNotepadLoad.Click += new System.EventHandler(this.tsmiNotepadLoad_Click);
            // 
            // MyTabs
            // 
            this.MyTabs.Controls.Add(this.tabPage3);
            this.MyTabs.Controls.Add(this.tabPage2);
            this.MyTabs.Controls.Add(this.tabPage1);
            this.MyTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MyTabs.Location = new System.Drawing.Point(0, 24);
            this.MyTabs.Multiline = true;
            this.MyTabs.Name = "MyTabs";
            this.MyTabs.SelectedIndex = 0;
            this.MyTabs.Size = new System.Drawing.Size(586, 469);
            this.MyTabs.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.MyTabs.TabIndex = 1;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.richTextBox1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(578, 443);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Блокнот";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.Info;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(3, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(572, 437);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.chkboxGenerator1);
            this.tabPage2.Controls.Add(this.btnGeneratorCopy);
            this.tabPage2.Controls.Add(this.btnClear);
            this.tabPage2.Controls.Add(this.tbGenerator);
            this.tabPage2.Controls.Add(this.lblGenerator);
            this.tabPage2.Controls.Add(this.btnGenerator);
            this.tabPage2.Controls.Add(this.numericUpDown2);
            this.tabPage2.Controls.Add(this.numericUpDown1);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(578, 443);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Генератор";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // chkboxGenerator1
            // 
            this.chkboxGenerator1.AutoSize = true;
            this.chkboxGenerator1.Location = new System.Drawing.Point(239, 31);
            this.chkboxGenerator1.Name = "chkboxGenerator1";
            this.chkboxGenerator1.Size = new System.Drawing.Size(95, 17);
            this.chkboxGenerator1.TabIndex = 10;
            this.chkboxGenerator1.Text = "Без повторов";
            this.chkboxGenerator1.UseVisualStyleBackColor = true;
            this.chkboxGenerator1.TextChanged += new System.EventHandler(this.ToolStripMenuItem_Exit_Click);
            // 
            // btnGeneratorCopy
            // 
            this.btnGeneratorCopy.Location = new System.Drawing.Point(46, 151);
            this.btnGeneratorCopy.Name = "btnGeneratorCopy";
            this.btnGeneratorCopy.Size = new System.Drawing.Size(62, 23);
            this.btnGeneratorCopy.TabIndex = 9;
            this.btnGeneratorCopy.Text = "Copy";
            this.btnGeneratorCopy.UseVisualStyleBackColor = true;
            this.btnGeneratorCopy.Click += new System.EventHandler(this.btnGeneratorCopy_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(140, 65);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 8;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // tbGenerator
            // 
            this.tbGenerator.Location = new System.Drawing.Point(140, 99);
            this.tbGenerator.Multiline = true;
            this.tbGenerator.Name = "tbGenerator";
            this.tbGenerator.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbGenerator.Size = new System.Drawing.Size(75, 145);
            this.tbGenerator.TabIndex = 7;
            // 
            // lblGenerator
            // 
            this.lblGenerator.AutoSize = true;
            this.lblGenerator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblGenerator.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblGenerator.Location = new System.Drawing.Point(49, 108);
            this.lblGenerator.Name = "lblGenerator";
            this.lblGenerator.Size = new System.Drawing.Size(21, 22);
            this.lblGenerator.TabIndex = 6;
            this.lblGenerator.Text = "0";
            // 
            // btnGenerator
            // 
            this.btnGenerator.Location = new System.Drawing.Point(140, 29);
            this.btnGenerator.Name = "btnGenerator";
            this.btnGenerator.Size = new System.Drawing.Size(75, 23);
            this.btnGenerator.TabIndex = 4;
            this.btnGenerator.Text = "Сгенить";
            this.btnGenerator.UseVisualStyleBackColor = true;
            this.btnGenerator.Click += new System.EventHandler(this.btnGenerator_Click);
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(46, 65);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(58, 20);
            this.numericUpDown2.TabIndex = 3;
            this.numericUpDown2.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(46, 29);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(58, 20);
            this.numericUpDown1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "До";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "От";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lblCount);
            this.tabPage1.Controls.Add(this.btnRst);
            this.tabPage1.Controls.Add(this.btnMinus);
            this.tabPage1.Controls.Add(this.btnPlus);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(578, 443);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Счетчик";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCount.Location = new System.Drawing.Point(141, 45);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(18, 20);
            this.lblCount.TabIndex = 3;
            this.lblCount.Text = "0";
            // 
            // btnRst
            // 
            this.btnRst.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnRst.Location = new System.Drawing.Point(8, 69);
            this.btnRst.Name = "btnRst";
            this.btnRst.Size = new System.Drawing.Size(75, 29);
            this.btnRst.TabIndex = 2;
            this.btnRst.Text = "Reset";
            this.btnRst.UseVisualStyleBackColor = true;
            this.btnRst.Click += new System.EventHandler(this.btnRst_Click);
            // 
            // btnMinus
            // 
            this.btnMinus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnMinus.Location = new System.Drawing.Point(8, 40);
            this.btnMinus.Name = "btnMinus";
            this.btnMinus.Size = new System.Drawing.Size(75, 29);
            this.btnMinus.TabIndex = 1;
            this.btnMinus.Text = "-";
            this.btnMinus.UseVisualStyleBackColor = true;
            this.btnMinus.Click += new System.EventHandler(this.btnMinus_Click);
            // 
            // btnPlus
            // 
            this.btnPlus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnPlus.Location = new System.Drawing.Point(8, 11);
            this.btnPlus.Name = "btnPlus";
            this.btnPlus.Size = new System.Drawing.Size(75, 29);
            this.btnPlus.TabIndex = 0;
            this.btnPlus.Text = "+";
            this.btnPlus.UseVisualStyleBackColor = true;
            this.btnPlus.Click += new System.EventHandler(this.btnPlus_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 493);
            this.Controls.Add(this.MyTabs);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Мои утилиты";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.MyTabs.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Exit;
        private System.Windows.Forms.ToolStripMenuItem помощьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiAbout;
        private System.Windows.Forms.TabControl MyTabs;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Button btnRst;
        private System.Windows.Forms.Button btnMinus;
        private System.Windows.Forms.Button btnPlus;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnGenerator;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblGenerator;
        private System.Windows.Forms.TextBox tbGenerator;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnGeneratorCopy;
        private System.Windows.Forms.CheckBox chkboxGenerator1;
        private System.Windows.Forms.ToolStripMenuItem блокнотToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiInsertDate;
        private System.Windows.Forms.ToolStripMenuItem tsmiInsertTime;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ToolStripMenuItem tsmiNotepadClear;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem tsmiNotepadSave;
        private System.Windows.Forms.ToolStripMenuItem tsmiNotepadLoad;
    }
}


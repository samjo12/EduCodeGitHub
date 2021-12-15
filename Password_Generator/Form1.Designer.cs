
namespace Password_Generator
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
            this.tcPassGen = new System.Windows.Forms.TabControl();
            this.tabConverter = new System.Windows.Forms.TabPage();
            this.btnConverterSwap = new System.Windows.Forms.Button();
            this.tbConverterTo = new System.Windows.Forms.TextBox();
            this.tbConverterFrom = new System.Windows.Forms.TextBox();
            this.btnConvert = new System.Windows.Forms.Button();
            this.cbConverterTo = new System.Windows.Forms.ComboBox();
            this.cbConverterFrom = new System.Windows.Forms.ComboBox();
            this.tabPassword = new System.Windows.Forms.TabPage();
            this.btnCopyPass = new System.Windows.Forms.Button();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.btnCreatePass = new System.Windows.Forms.Button();
            this.nudPassLength = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.clbPassSymbols = new System.Windows.Forms.CheckedListBox();
            this.cbConverterMetrica = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tcPassGen.SuspendLayout();
            this.tabConverter.SuspendLayout();
            this.tabPassword.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPassLength)).BeginInit();
            this.SuspendLayout();
            // 
            // tcPassGen
            // 
            this.tcPassGen.Controls.Add(this.tabConverter);
            this.tcPassGen.Controls.Add(this.tabPassword);
            this.tcPassGen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcPassGen.Location = new System.Drawing.Point(0, 0);
            this.tcPassGen.Name = "tcPassGen";
            this.tcPassGen.SelectedIndex = 0;
            this.tcPassGen.Size = new System.Drawing.Size(800, 637);
            this.tcPassGen.TabIndex = 0;
            // 
            // tabConverter
            // 
            this.tabConverter.BackColor = System.Drawing.Color.Honeydew;
            this.tabConverter.Controls.Add(this.label2);
            this.tabConverter.Controls.Add(this.cbConverterMetrica);
            this.tabConverter.Controls.Add(this.btnConverterSwap);
            this.tabConverter.Controls.Add(this.tbConverterTo);
            this.tabConverter.Controls.Add(this.tbConverterFrom);
            this.tabConverter.Controls.Add(this.btnConvert);
            this.tabConverter.Controls.Add(this.cbConverterTo);
            this.tabConverter.Controls.Add(this.cbConverterFrom);
            this.tabConverter.Location = new System.Drawing.Point(4, 24);
            this.tabConverter.Name = "tabConverter";
            this.tabConverter.Padding = new System.Windows.Forms.Padding(3);
            this.tabConverter.Size = new System.Drawing.Size(792, 609);
            this.tabConverter.TabIndex = 1;
            this.tabConverter.Text = "Конвертер";
            // 
            // btnConverterSwap
            // 
            this.btnConverterSwap.Location = new System.Drawing.Point(156, 32);
            this.btnConverterSwap.Name = "btnConverterSwap";
            this.btnConverterSwap.Size = new System.Drawing.Size(114, 23);
            this.btnConverterSwap.TabIndex = 5;
            this.btnConverterSwap.Text = "< Обменять >";
            this.btnConverterSwap.UseVisualStyleBackColor = true;
            this.btnConverterSwap.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbConverterTo
            // 
            this.tbConverterTo.Location = new System.Drawing.Point(290, 61);
            this.tbConverterTo.Name = "tbConverterTo";
            this.tbConverterTo.ReadOnly = true;
            this.tbConverterTo.Size = new System.Drawing.Size(121, 23);
            this.tbConverterTo.TabIndex = 4;
            // 
            // tbConverterFrom
            // 
            this.tbConverterFrom.Location = new System.Drawing.Point(18, 62);
            this.tbConverterFrom.Name = "tbConverterFrom";
            this.tbConverterFrom.Size = new System.Drawing.Size(121, 23);
            this.tbConverterFrom.TabIndex = 3;
            this.tbConverterFrom.Text = "1";
            // 
            // btnConvert
            // 
            this.btnConvert.Location = new System.Drawing.Point(156, 62);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(114, 23);
            this.btnConvert.TabIndex = 2;
            this.btnConvert.Text = "Конвертировать";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // cbConverterTo
            // 
            this.cbConverterTo.FormattingEnabled = true;
            this.cbConverterTo.Items.AddRange(new object[] {
            "mm - millimeters",
            "cm - cantimeters",
            "dm - decimeters",
            "m - meters",
            "km - kilometers",
            "ml - miles"});
            this.cbConverterTo.Location = new System.Drawing.Point(290, 32);
            this.cbConverterTo.Name = "cbConverterTo";
            this.cbConverterTo.Size = new System.Drawing.Size(121, 23);
            this.cbConverterTo.TabIndex = 1;
            this.cbConverterTo.Text = "mm - millimeters";
            // 
            // cbConverterFrom
            // 
            this.cbConverterFrom.FormattingEnabled = true;
            this.cbConverterFrom.Items.AddRange(new object[] {
            "mm - millimeters",
            "cm - cantimeters",
            "dm - decimeters",
            "m - meters",
            "km - kilometers",
            "ml - miles"});
            this.cbConverterFrom.Location = new System.Drawing.Point(18, 32);
            this.cbConverterFrom.Name = "cbConverterFrom";
            this.cbConverterFrom.Size = new System.Drawing.Size(121, 23);
            this.cbConverterFrom.TabIndex = 0;
            this.cbConverterFrom.Text = "mm - millimeters";
            // 
            // tabPassword
            // 
            this.tabPassword.BackColor = System.Drawing.Color.Transparent;
            this.tabPassword.Controls.Add(this.btnCopyPass);
            this.tabPassword.Controls.Add(this.tbPassword);
            this.tabPassword.Controls.Add(this.btnCreatePass);
            this.tabPassword.Controls.Add(this.nudPassLength);
            this.tabPassword.Controls.Add(this.label1);
            this.tabPassword.Controls.Add(this.clbPassSymbols);
            this.tabPassword.Location = new System.Drawing.Point(4, 24);
            this.tabPassword.Name = "tabPassword";
            this.tabPassword.Padding = new System.Windows.Forms.Padding(3);
            this.tabPassword.Size = new System.Drawing.Size(792, 609);
            this.tabPassword.TabIndex = 0;
            this.tabPassword.Text = "Генератор паролей";
            // 
            // btnCopyPass
            // 
            this.btnCopyPass.Location = new System.Drawing.Point(64, 214);
            this.btnCopyPass.Name = "btnCopyPass";
            this.btnCopyPass.Size = new System.Drawing.Size(94, 23);
            this.btnCopyPass.TabIndex = 5;
            this.btnCopyPass.Text = "Скопировать";
            this.btnCopyPass.UseVisualStyleBackColor = true;
            // 
            // tbPassword
            // 
            this.tbPassword.BackColor = System.Drawing.SystemColors.Info;
            this.tbPassword.Location = new System.Drawing.Point(9, 185);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(203, 23);
            this.tbPassword.TabIndex = 4;
            // 
            // btnCreatePass
            // 
            this.btnCreatePass.Location = new System.Drawing.Point(48, 144);
            this.btnCreatePass.Name = "btnCreatePass";
            this.btnCreatePass.Size = new System.Drawing.Size(140, 23);
            this.btnCreatePass.TabIndex = 3;
            this.btnCreatePass.Text = "Создать пароль";
            this.btnCreatePass.UseVisualStyleBackColor = true;
            this.btnCreatePass.Click += new System.EventHandler(this.btnCreatePass_Click);
            // 
            // nudPassLength
            // 
            this.nudPassLength.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.nudPassLength.Location = new System.Drawing.Point(102, 106);
            this.nudPassLength.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudPassLength.Minimum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.nudPassLength.Name = "nudPassLength";
            this.nudPassLength.Size = new System.Drawing.Size(47, 23);
            this.nudPassLength.TabIndex = 2;
            this.nudPassLength.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Длина пароля:";
            // 
            // clbPassSymbols
            // 
            this.clbPassSymbols.BackColor = System.Drawing.SystemColors.Info;
            this.clbPassSymbols.CheckOnClick = true;
            this.clbPassSymbols.FormattingEnabled = true;
            this.clbPassSymbols.Items.AddRange(new object[] {
            "Цифры [0..9]",
            "Прописные буквы [a..z]",
            "Строчные буквы [A..Z]",
            "Спец.символы %,*,),?,#,$,^,&,^"});
            this.clbPassSymbols.Location = new System.Drawing.Point(8, 17);
            this.clbPassSymbols.Name = "clbPassSymbols";
            this.clbPassSymbols.Size = new System.Drawing.Size(204, 76);
            this.clbPassSymbols.TabIndex = 0;
            // 
            // cbConverterMetrica
            // 
            this.cbConverterMetrica.FormattingEnabled = true;
            this.cbConverterMetrica.Items.AddRange(new object[] {
            "Длины",
            "Веса"});
            this.cbConverterMetrica.Location = new System.Drawing.Point(118, 3);
            this.cbConverterMetrica.Name = "cbConverterMetrica";
            this.cbConverterMetrica.Size = new System.Drawing.Size(152, 23);
            this.cbConverterMetrica.TabIndex = 6;
            this.cbConverterMetrica.Text = "Длины";
            this.cbConverterMetrica.SelectedIndexChanged += new System.EventHandler(this.cbConverterMetrica_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "Выбор метрики";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 637);
            this.Controls.Add(this.tcPassGen);
            this.Name = "Form1";
            this.Text = "Мои утилиты";
            this.tcPassGen.ResumeLayout(false);
            this.tabConverter.ResumeLayout(false);
            this.tabConverter.PerformLayout();
            this.tabPassword.ResumeLayout(false);
            this.tabPassword.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPassLength)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcPassGen;
        private System.Windows.Forms.TabPage tabPassword;
        private System.Windows.Forms.TabPage tabConverter;
        private System.Windows.Forms.CheckedListBox clbPassSymbols;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudPassLength;
        private System.Windows.Forms.Button btnCreatePass;
        private System.Windows.Forms.Button btnCopyPass;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.TextBox tbConverterTo;
        private System.Windows.Forms.TextBox tbConverterFrom;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.ComboBox cbConverterTo;
        private System.Windows.Forms.ComboBox cbConverterFrom;
        private System.Windows.Forms.Button btnConverterSwap;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbConverterMetrica;
    }
}


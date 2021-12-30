
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
            this.tabPassword = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.tbMyStr = new System.Windows.Forms.TextBox();
            this.pb1 = new System.Windows.Forms.ProgressBar();
            this.tbPassForce = new System.Windows.Forms.TextBox();
            this.tbSymIgnor = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbDisableUnderline = new System.Windows.Forms.CheckBox();
            this.cbDisableMinus = new System.Windows.Forms.CheckBox();
            this.cbDisableLetter_O_I = new System.Windows.Forms.CheckBox();
            this.cbDisableLetter_o = new System.Windows.Forms.CheckBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.btnCreatePass = new System.Windows.Forms.Button();
            this.nudPassLength = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.clbPassSymbols = new System.Windows.Forms.CheckedListBox();
            this.tabConverter = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.cbConverterMetrica = new System.Windows.Forms.ComboBox();
            this.btnConverterSwap = new System.Windows.Forms.Button();
            this.tbConverterTo = new System.Windows.Forms.TextBox();
            this.tbConverterFrom = new System.Windows.Forms.TextBox();
            this.btnConvert = new System.Windows.Forms.Button();
            this.cbConverterTo = new System.Windows.Forms.ComboBox();
            this.cbConverterFrom = new System.Windows.Forms.ComboBox();
            this.tcPassGen.SuspendLayout();
            this.tabPassword.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPassLength)).BeginInit();
            this.tabConverter.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcPassGen
            // 
            this.tcPassGen.Controls.Add(this.tabPassword);
            this.tcPassGen.Controls.Add(this.tabConverter);
            this.tcPassGen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcPassGen.Location = new System.Drawing.Point(0, 0);
            this.tcPassGen.Name = "tcPassGen";
            this.tcPassGen.SelectedIndex = 0;
            this.tcPassGen.Size = new System.Drawing.Size(547, 409);
            this.tcPassGen.TabIndex = 0;
            // 
            // tabPassword
            // 
            this.tabPassword.BackColor = System.Drawing.Color.Transparent;
            this.tabPassword.Controls.Add(this.label4);
            this.tabPassword.Controls.Add(this.tbMyStr);
            this.tabPassword.Controls.Add(this.pb1);
            this.tabPassword.Controls.Add(this.tbPassForce);
            this.tabPassword.Controls.Add(this.tbSymIgnor);
            this.tabPassword.Controls.Add(this.label3);
            this.tabPassword.Controls.Add(this.cbDisableUnderline);
            this.tabPassword.Controls.Add(this.cbDisableMinus);
            this.tabPassword.Controls.Add(this.cbDisableLetter_O_I);
            this.tabPassword.Controls.Add(this.cbDisableLetter_o);
            this.tabPassword.Controls.Add(this.tbPassword);
            this.tabPassword.Controls.Add(this.btnCreatePass);
            this.tabPassword.Controls.Add(this.nudPassLength);
            this.tabPassword.Controls.Add(this.label1);
            this.tabPassword.Controls.Add(this.clbPassSymbols);
            this.tabPassword.Location = new System.Drawing.Point(4, 24);
            this.tabPassword.Name = "tabPassword";
            this.tabPassword.Padding = new System.Windows.Forms.Padding(3);
            this.tabPassword.Size = new System.Drawing.Size(539, 381);
            this.tabPassword.TabIndex = 0;
            this.tabPassword.Text = "Генератор паролей";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(9, 216);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(507, 15);
            this.label4.TabIndex = 16;
            this.label4.Text = "Использовать в пароле свои последовательности символов, можно ввести через  ; ,\" " +
    "\" : ";
            // 
            // tbMyStr
            // 
            this.tbMyStr.Location = new System.Drawing.Point(9, 234);
            this.tbMyStr.MaxLength = 70;
            this.tbMyStr.Name = "tbMyStr";
            this.tbMyStr.Size = new System.Drawing.Size(521, 23);
            this.tbMyStr.TabIndex = 15;
            // 
            // pb1
            // 
            this.pb1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.pb1.ForeColor = System.Drawing.SystemColors.Control;
            this.pb1.Location = new System.Drawing.Point(9, 350);
            this.pb1.Name = "pb1";
            this.pb1.Size = new System.Drawing.Size(521, 23);
            this.pb1.TabIndex = 14;
            // 
            // tbPassForce
            // 
            this.tbPassForce.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.tbPassForce.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.tbPassForce.Location = new System.Drawing.Point(9, 324);
            this.tbPassForce.Name = "tbPassForce";
            this.tbPassForce.Size = new System.Drawing.Size(521, 29);
            this.tbPassForce.TabIndex = 13;
            this.tbPassForce.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbSymIgnor
            // 
            this.tbSymIgnor.Location = new System.Drawing.Point(9, 186);
            this.tbSymIgnor.MaxLength = 70;
            this.tbSymIgnor.Name = "tbSymIgnor";
            this.tbSymIgnor.Size = new System.Drawing.Size(521, 23);
            this.tbSymIgnor.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(8, 168);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(466, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "Не использовать в пароле следующие символы (можно ввести до 70 символов):";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // cbDisableUnderline
            // 
            this.cbDisableUnderline.AutoSize = true;
            this.cbDisableUnderline.Location = new System.Drawing.Point(242, 74);
            this.cbDisableUnderline.Name = "cbDisableUnderline";
            this.cbDisableUnderline.Size = new System.Drawing.Size(276, 19);
            this.cbDisableUnderline.TabIndex = 10;
            this.cbDisableUnderline.Text = "Не использовать символ \" _ \" подчеркивания";
            this.cbDisableUnderline.UseVisualStyleBackColor = true;
            // 
            // cbDisableMinus
            // 
            this.cbDisableMinus.AutoSize = true;
            this.cbDisableMinus.Location = new System.Drawing.Point(242, 56);
            this.cbDisableMinus.Name = "cbDisableMinus";
            this.cbDisableMinus.Size = new System.Drawing.Size(181, 19);
            this.cbDisableMinus.TabIndex = 9;
            this.cbDisableMinus.Text = "Не использовать \" - \" минус";
            this.cbDisableMinus.UseVisualStyleBackColor = true;
            // 
            // cbDisableLetter_O_I
            // 
            this.cbDisableLetter_O_I.AutoSize = true;
            this.cbDisableLetter_O_I.Location = new System.Drawing.Point(242, 38);
            this.cbDisableLetter_O_I.Name = "cbDisableLetter_O_I";
            this.cbDisableLetter_O_I.Size = new System.Drawing.Size(207, 19);
            this.cbDisableLetter_O_I.TabIndex = 8;
            this.cbDisableLetter_O_I.Text = "Не импользовать буквы \"O\" и \"I\"";
            this.cbDisableLetter_O_I.UseVisualStyleBackColor = true;
            // 
            // cbDisableLetter_o
            // 
            this.cbDisableLetter_o.AutoSize = true;
            this.cbDisableLetter_o.Location = new System.Drawing.Point(242, 20);
            this.cbDisableLetter_o.Name = "cbDisableLetter_o";
            this.cbDisableLetter_o.Size = new System.Drawing.Size(173, 19);
            this.cbDisableLetter_o.TabIndex = 7;
            this.cbDisableLetter_o.Text = "Не использовать букву \"о\"";
            this.cbDisableLetter_o.UseVisualStyleBackColor = true;
            // 
            // tbPassword
            // 
            this.tbPassword.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.tbPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.tbPassword.Location = new System.Drawing.Point(9, 291);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.tbPassword.Size = new System.Drawing.Size(521, 33);
            this.tbPassword.TabIndex = 4;
            // 
            // btnCreatePass
            // 
            this.btnCreatePass.Location = new System.Drawing.Point(198, 262);
            this.btnCreatePass.Name = "btnCreatePass";
            this.btnCreatePass.Size = new System.Drawing.Size(140, 23);
            this.btnCreatePass.TabIndex = 3;
            this.btnCreatePass.Text = "Создать пароль";
            this.btnCreatePass.UseVisualStyleBackColor = true;
            this.btnCreatePass.Click += new System.EventHandler(this.btnCreatePass_Click);
            // 
            // nudPassLength
            // 
            this.nudPassLength.Cursor = System.Windows.Forms.Cursors.Default;
            this.nudPassLength.Location = new System.Drawing.Point(102, 263);
            this.nudPassLength.Maximum = new decimal(new int[] {
            80,
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
            12,
            0,
            0,
            0});
            this.nudPassLength.ValueChanged += new System.EventHandler(this.nudPassLength_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 265);
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
            "Прописные буквы [A..Z]",
            "Строчные буквы [a..z]",
            "Спец.символы ! @ # $ _ / |",
            "Скобки [ ] { } ( ) < >",
            "Математ.знаки % ^ & * - + = ~",
            "Знаки препинания  ; : , . ? кавычки",
            "Символ пробела"});
            this.clbPassSymbols.Location = new System.Drawing.Point(8, 17);
            this.clbPassSymbols.Name = "clbPassSymbols";
            this.clbPassSymbols.Size = new System.Drawing.Size(215, 148);
            this.clbPassSymbols.TabIndex = 0;
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
            this.tabConverter.Size = new System.Drawing.Size(539, 381);
            this.tabConverter.TabIndex = 1;
            this.tabConverter.Text = "Конвертер";
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(547, 409);
            this.Controls.Add(this.tcPassGen);
            this.Name = "Form1";
            this.Text = "Мои утилиты";
            this.tcPassGen.ResumeLayout(false);
            this.tabPassword.ResumeLayout(false);
            this.tabPassword.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPassLength)).EndInit();
            this.tabConverter.ResumeLayout(false);
            this.tabConverter.PerformLayout();
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
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.TextBox tbConverterTo;
        private System.Windows.Forms.TextBox tbConverterFrom;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.ComboBox cbConverterTo;
        private System.Windows.Forms.ComboBox cbConverterFrom;
        private System.Windows.Forms.Button btnConverterSwap;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbConverterMetrica;
        private System.Windows.Forms.CheckBox cbDisableLetter_O_I;
        private System.Windows.Forms.CheckBox cbDisableLetter_o;
        private System.Windows.Forms.CheckBox cbDisableMinus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbDisableUnderline;
        private System.Windows.Forms.TextBox tbSymIgnor;
        private System.Windows.Forms.TextBox tbPassForce;
        private System.Windows.Forms.ProgressBar pb1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbMyStr;
    }
}


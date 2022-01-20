
namespace Password_Generator
{
    partial class Miner1
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.MenuStartGame = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuGameLevel = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuGameLevelEasy = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuGameLevelMedium = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuGameLevelHard = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuGameLevelNightmare = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuSizePlayfield = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuSizePlayfield10 = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuSizePlayfield20 = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuSizePlayfield30 = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuSizePlayfieldCustom = new System.Windows.Forms.ToolStripMenuItem();
            this.XSizePlayfield = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.YSizePlayfield = new System.Windows.Forms.ToolStripTextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuStartGame,
            this.MenuGameLevel,
            this.MenuSizePlayfield});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1025, 29);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // MenuStartGame
            // 
            this.MenuStartGame.CheckOnClick = true;
            this.MenuStartGame.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MenuStartGame.ForeColor = System.Drawing.Color.Green;
            this.MenuStartGame.Name = "MenuStartGame";
            this.MenuStartGame.Size = new System.Drawing.Size(95, 25);
            this.MenuStartGame.Text = "StartGame";
            this.MenuStartGame.Click += new System.EventHandler(this.MenuStartGame_Click);
            // 
            // MenuGameLevel
            // 
            this.MenuGameLevel.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuGameLevelEasy,
            this.MenuGameLevelMedium,
            this.MenuGameLevelHard,
            this.MenuGameLevelNightmare});
            this.MenuGameLevel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MenuGameLevel.Name = "MenuGameLevel";
            this.MenuGameLevel.Size = new System.Drawing.Size(99, 25);
            this.MenuGameLevel.Text = "GameLevel";
            // 
            // MenuGameLevelEasy
            // 
            this.MenuGameLevelEasy.Checked = true;
            this.MenuGameLevelEasy.CheckOnClick = true;
            this.MenuGameLevelEasy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MenuGameLevelEasy.Name = "MenuGameLevelEasy";
            this.MenuGameLevelEasy.Size = new System.Drawing.Size(155, 26);
            this.MenuGameLevelEasy.Tag = "1";
            this.MenuGameLevelEasy.Text = "Easy";
            this.MenuGameLevelEasy.Click += new System.EventHandler(this.GameLevelChanging);
            // 
            // MenuGameLevelMedium
            // 
            this.MenuGameLevelMedium.CheckOnClick = true;
            this.MenuGameLevelMedium.Name = "MenuGameLevelMedium";
            this.MenuGameLevelMedium.Size = new System.Drawing.Size(155, 26);
            this.MenuGameLevelMedium.Tag = "2";
            this.MenuGameLevelMedium.Text = "Medium";
            this.MenuGameLevelMedium.Click += new System.EventHandler(this.GameLevelChanging);
            // 
            // MenuGameLevelHard
            // 
            this.MenuGameLevelHard.CheckOnClick = true;
            this.MenuGameLevelHard.Name = "MenuGameLevelHard";
            this.MenuGameLevelHard.Size = new System.Drawing.Size(155, 26);
            this.MenuGameLevelHard.Tag = "3";
            this.MenuGameLevelHard.Text = "Hard";
            this.MenuGameLevelHard.Click += new System.EventHandler(this.GameLevelChanging);
            // 
            // MenuGameLevelNightmare
            // 
            this.MenuGameLevelNightmare.CheckOnClick = true;
            this.MenuGameLevelNightmare.Name = "MenuGameLevelNightmare";
            this.MenuGameLevelNightmare.Size = new System.Drawing.Size(155, 26);
            this.MenuGameLevelNightmare.Tag = "4";
            this.MenuGameLevelNightmare.Text = "Nightmare";
            this.MenuGameLevelNightmare.Click += new System.EventHandler(this.GameLevelChanging);
            // 
            // MenuSizePlayfield
            // 
            this.MenuSizePlayfield.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuSizePlayfield10,
            this.MenuSizePlayfield20,
            this.MenuSizePlayfield30,
            this.MenuSizePlayfieldCustom});
            this.MenuSizePlayfield.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MenuSizePlayfield.Name = "MenuSizePlayfield";
            this.MenuSizePlayfield.Size = new System.Drawing.Size(109, 25);
            this.MenuSizePlayfield.Text = "SizePlayfield";
            // 
            // MenuSizePlayfield10
            // 
            this.MenuSizePlayfield10.Checked = true;
            this.MenuSizePlayfield10.CheckOnClick = true;
            this.MenuSizePlayfield10.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MenuSizePlayfield10.Name = "MenuSizePlayfield10";
            this.MenuSizePlayfield10.Size = new System.Drawing.Size(134, 26);
            this.MenuSizePlayfield10.Tag = "10";
            this.MenuSizePlayfield10.Text = "10 x 10";
            this.MenuSizePlayfield10.Click += new System.EventHandler(this.SizePlayfieldChanging);
            // 
            // MenuSizePlayfield20
            // 
            this.MenuSizePlayfield20.CheckOnClick = true;
            this.MenuSizePlayfield20.Name = "MenuSizePlayfield20";
            this.MenuSizePlayfield20.Size = new System.Drawing.Size(134, 26);
            this.MenuSizePlayfield20.Tag = "20";
            this.MenuSizePlayfield20.Text = "20 x 20";
            this.MenuSizePlayfield20.Click += new System.EventHandler(this.SizePlayfieldChanging);
            // 
            // MenuSizePlayfield30
            // 
            this.MenuSizePlayfield30.CheckOnClick = true;
            this.MenuSizePlayfield30.Name = "MenuSizePlayfield30";
            this.MenuSizePlayfield30.Size = new System.Drawing.Size(134, 26);
            this.MenuSizePlayfield30.Tag = "30";
            this.MenuSizePlayfield30.Text = "30 x 30";
            this.MenuSizePlayfield30.Click += new System.EventHandler(this.SizePlayfieldChanging);
            // 
            // MenuSizePlayfieldCustom
            // 
            this.MenuSizePlayfieldCustom.CheckOnClick = true;
            this.MenuSizePlayfieldCustom.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.XSizePlayfield,
            this.toolStripSeparator1,
            this.YSizePlayfield});
            this.MenuSizePlayfieldCustom.Name = "MenuSizePlayfieldCustom";
            this.MenuSizePlayfieldCustom.Size = new System.Drawing.Size(134, 26);
            this.MenuSizePlayfieldCustom.Text = "Custom";
            this.MenuSizePlayfieldCustom.Click += new System.EventHandler(this.SizePlayfieldChanging);
            // 
            // XSizePlayfield
            // 
            this.XSizePlayfield.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.XSizePlayfield.HideSelection = false;
            this.XSizePlayfield.MaxLength = 32000;
            this.XSizePlayfield.MergeAction = System.Windows.Forms.MergeAction.Replace;
            this.XSizePlayfield.Name = "XSizePlayfield";
            this.XSizePlayfield.Size = new System.Drawing.Size(120, 25);
            this.XSizePlayfield.Text = "Size X:";
            this.XSizePlayfield.ToolTipText = "Enter value in range 5..30";
            this.XSizePlayfield.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbSizePlayfield_KeyDown);
            this.XSizePlayfield.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbSizePlayfield_KeyPress);
            this.XSizePlayfield.Click += new System.EventHandler(this.SizePlayfieldChanging);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // YSizePlayfield
            // 
            this.YSizePlayfield.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.YSizePlayfield.HideSelection = false;
            this.YSizePlayfield.MaxLength = 32000;
            this.YSizePlayfield.MergeAction = System.Windows.Forms.MergeAction.Replace;
            this.YSizePlayfield.Name = "YSizePlayfield";
            this.YSizePlayfield.Size = new System.Drawing.Size(120, 25);
            this.YSizePlayfield.Text = "Size Y:";
            this.YSizePlayfield.ToolTipText = "Enter value in range 5..30";
            this.YSizePlayfield.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbSizePlayfield_KeyDown);
            this.YSizePlayfield.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbSizePlayfield_KeyPress);
            this.YSizePlayfield.Click += new System.EventHandler(this.SizePlayfieldChanging);
            // 
            // Miner1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1025, 512);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Miner1";
            this.Text = "Minesweeper from Samjo v.1.0";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MenuStartGame;
        private System.Windows.Forms.ToolStripMenuItem MenuGameLevel;
        private System.Windows.Forms.ToolStripMenuItem MenuGameLevelEasy;
        private System.Windows.Forms.ToolStripMenuItem MenuGameLevelMedium;
        private System.Windows.Forms.ToolStripMenuItem MenuGameLevelHard;
        private System.Windows.Forms.ToolStripMenuItem MenuGameLevelNightmare;
        private System.Windows.Forms.ToolStripMenuItem MenuSizePlayfield;
        private System.Windows.Forms.ToolStripMenuItem MenuSizePlayfield10;
        private System.Windows.Forms.ToolStripMenuItem MenuSizePlayfield20;
        private System.Windows.Forms.ToolStripMenuItem MenuSizePlayfield30;
        private System.Windows.Forms.ToolStripMenuItem MenuSizePlayfieldCustom;
        private System.Windows.Forms.ToolStripTextBox YSizePlayfield;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripTextBox XSizePlayfield;
    }
}


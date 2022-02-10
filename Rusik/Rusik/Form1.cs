using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rusik
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void tsmiQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsmiSaveFile_Click(object sender, EventArgs e)
        {
            SaveFile();
        }
        private void SaveFile()
        {
            try
            {
                rtbTranslated.SaveFile("notepad.rtf");
            }
            catch
            {
                MessageBox.Show("Файл был перезаписан notepad.rtf");
            }
        }
        private void LoadFile()
        {
            try
            {
                rtbSource.LoadFile("notepad.rtf");
            }
            catch
            {
                MessageBox.Show("Создан новый файл notepad.rtf");
                SaveFile();
            }
        }
        private void OpenFile_tsmi_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.ShowDialog();
            LoadFile();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
           // LoadFile();
        }
        private void About_tsmi_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This program may be useful for translation text in some binary files.");
        }
    }
}

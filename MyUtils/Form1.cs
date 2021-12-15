using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyUtils
{
    public partial class MainForm : Form
    {
        int count = 0;
        Random rnd=new Random();
       
        

        public MainForm()
        {
            InitializeComponent();
        }

        private void ToolStripMenuItem_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsmiAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("My 2nd program on C#");
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            count++;
            lblCount.Text = count.ToString(); 
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            if (count != 0)
            {
                count--;
                lblCount.Text = count.ToString(); 
            }
        }

        private void btnRst_Click(object sender, EventArgs e)
        {
            count = 0; lblCount.Text = Convert.ToString (count);
        }

        private void btnGenerator_Click(object sender, EventArgs e)
        {
            int n, n_counter= Convert.ToInt32(numericUpDown2.Value- numericUpDown1.Value-1);
            n = rnd.Next(Convert.ToInt32(numericUpDown1.Value), Convert.ToInt32(numericUpDown2.Value));
            lblGenerator.Text = Convert.ToString(n);
            if (chkboxGenerator1.Checked)
            {
                while (tbGenerator.Text.IndexOf(n.ToString()) != -1)
                {
                    if (n_counter == 0) break;
                    n = rnd.Next(Convert.ToInt32(numericUpDown1.Value), Convert.ToInt32(numericUpDown2.Value));
                    n_counter--; 
                }
                if(n_counter!=0)tbGenerator.AppendText(n + "\n");
            }
            else tbGenerator.AppendText(n + "\n");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //tbGenerator.Text = ""; lblGenerator.Text = "";
            tbGenerator.Clear();
        }

        private void btnGeneratorCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(tbGenerator.Text);
        }

        private void tsmiInsertDate_Click(object sender, EventArgs e)
        {
            richTextBox1.AppendText("\n" + DateTime.Now.ToShortDateString());
        }

        private void tsmiNotepadClear_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        private void tsmiInsertTime_Click(object sender, EventArgs e)
        {
            richTextBox1.AppendText("\n"+DateTime.Now.ToShortTimeString());
        }

        private void tsmiNotepadSave_Click(object sender, EventArgs e)
        {
            NotepadSaveFile();
        }
        private void NotepadSaveFile()
        {
            try
            {
                richTextBox1.SaveFile("notepad.rtf");
            }
            catch
            {
                
                MessageBox.Show("Файл был перезаписан notepad.rtf");
            }
        }
        private void NotepadLoadFile()
        {
            try
            {
                richTextBox1.LoadFile("notepad.rtf");
            }
            catch
            {
                MessageBox.Show("Создан новый файл notepad.rtf");
                NotepadSaveFile();
            }
        }
        private void tsmiNotepadLoad_Click(object sender, EventArgs e)
        {
            NotepadLoadFile();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            NotepadLoadFile();
        }
    }
}

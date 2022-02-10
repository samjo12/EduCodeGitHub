using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Rusik
{
    public partial class Form1 : Form
    {
        public string SourceFile;
        public string OutputFile;
        public Form1()
        {
            InitializeComponent();
        }

        private void Quit_tsmi_Click(object sender, EventArgs e)
        {
            // если исходный файл открыт, то предлагаем сохранить выходной файл
            this.Close();
        }

        private void SaveFile_tsmi_Click(object sender, EventArgs e)
        {
            //SaveFile();
        }
        private void SaveFileAs(string s)
        {
            try
            {
               // 1 - выбираем в диалоговом окне имя нового выходного файла
               // 2 - перемещаем позицию входного файла на 0
               // 3- переписываем весь входной файл в новыый файл учитываю измененные Records
                
            }
            catch
            {
                MessageBox.Show("Error writing to file: "+s);
            }
        }


        private void OpenFile_tsmi_Click(object sender, EventArgs e)
        {
          OpenFileDialog openFileDialog1 = new();
          openFileDialog1.ShowDialog();
          SourceFile = openFileDialog1.FileName;
          Fileopened_lb.Text = SourceFile; // пишем в метку имя открываемого файла
          BinaryReader reader = new BinaryReader(File.Open(SourceFile, FileMode.Open));
            // создаем объект BinaryWriter
            if (File.Exists(SourceFile + ".new"))
            {
                MessageBox.Show("Output file with standard name is already exists:\n" + SourceFile + ".new"
                    + "\n Please choose new output FileName.");
                SaveFileDialog saveFileDialog1 = new();
                saveFileDialog1.OverwritePrompt = true; //предупреждение о перезаписи
                saveFileDialog1.ShowDialog();
                OutputFile = saveFileDialog1.FileName;
            }
            else
            {
                OutputFile = openFileDialog1.FileName + ".new";
            }


            using (BinaryWriter writer = new BinaryWriter(File.Open(OutputFile, FileMode.OpenOrCreate)))
            {
                FileInfo src = new FileInfo(SourceFile);
                var l = src.Length;
                
                while (l>0)
                { // посимвольно читаем исходный файл
                    var b = reader.ReadByte();
                    ///if (EndOfStreamException.) break;
                    writer.Write(b);
                    l--;
                }

            }
            


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

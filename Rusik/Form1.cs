using System;
using System.Collections;
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
        public DoublyLinkedList<string> linkedListSF = new DoublyLinkedList<string>(); // связный список для исходного файла
        public DoublyLinkedList<string> linkedListOF = new DoublyLinkedList<string>(); // связный список для выходного файла
        public struct DataNode 
        {
           public string str;
           public long pos;
        }
        public byte [] Signature= {0x00,0x06,0x00 };  // Сигнатура из байт
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
            SaveFileDialog saveFileDialog1 = new();
            saveFileDialog1.OverwritePrompt = true; //предупреждение о перезаписи
            saveFileDialog1.ShowDialog();
            FileInfo src = new FileInfo(OutputFile);
            OutputFile = saveFileDialog1.FileName;

            if (src.Exists)
            {
                src.MoveTo(OutputFile);
            }
            else { MessageBox.Show("Error writing to file: " + OutputFile); }
        }

        private void OpenFile_tsmi_Click(object sender, EventArgs e)
        {
          OpenFileDialog openFileDialog1 = new();
          openFileDialog1.ShowDialog();
          SourceFile = openFileDialog1.FileName;
          OutputFile = SourceFile + ".tmp$$";
          //BinaryReader reader = new BinaryReader(File.Open(SourceFile, FileMode.Open));
            // создаем объект BinaryWriter
            if (!File.Exists(OutputFile))
            {   // Если выходной файл еще не существует, копируем выбранный файл во временный
                FileInfo srcF = new FileInfo(SourceFile);
                srcF.CopyTo(OutputFile);
                FileInfo outF = new FileInfo(SourceFile); // ставим атрибуты на копию hidden
                outF.Attributes |= FileAttributes.Hidden;
                //outF.Attributes |= FileAttributes.ReadOnly;
            }
            // разблокируем поля SourceFile_tb TranslatedFile_tb
            SourceFile_tb.Text = SourceFile;
            TranslatedFile_tb.Text = OutputFile;


            //разблокируем поля Смещения, Поиска Сигнатуры и Поиска строк текста во входном и выходном файлах
            Offset_tb.ReadOnly = false; // textbox Offset
            Signature_tb.ReadOnly = false; // texbox Signature
            SearchSource_tb.ReadOnly = false;
            SearchTranslated_tb.ReadOnly = false;
            /*
             *  using (BinaryWriter writer = new BinaryWriter(File.Open(OutputFile, FileMode.OpenOrCreate)))
                {
                    FileInfo src = new FileInfo(SourceFile);
                    var l = src.Length;
                    long onepercent = l / 100-1, percent = onepercent;
                    progressBar1.Value=0;
                    for (int i=0; i<l; i++)
                    { // посимвольно читаем исходный файл
                        var b = reader.ReadByte();
                        //if (EndOfStreamException.) break;
                        writer.Write(b);
                        if (i == percent) { progressBar1.Value++; percent += onepercent; }
                    }
                    progressBar1.Value=100;
                }
              */

            /* //Работа со списком
            DoublyLinkedList<string> linkedList = new DoublyLinkedList<string>();
// добавление элементов
linkedList.Add("Bob");
linkedList.Add("Bill");
linkedList.Add("Tom");
linkedList.AddFirst("Kate");
foreach (var item in linkedList)
{
    Console.WriteLine(item);
}
// удаление
linkedList.Remove("Bill");
 
// перебор с последнего элемента
foreach (var t in linkedList.BackEnumerator())
{
    Console.WriteLine(t);
} 
             */

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
           // LoadFile();
        }
        private void About_tsmi_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This program may be useful for translation text in some binary files.");
        }

        private void Start_btn_Click(object sender, EventArgs e)
        {
            //if (Signature_tb.TextLength == 0) { MessageBox.Show("You must setup Signature to start."); return; }
            // начиная со смещения Offset начнем по байтам искать сигнатуру Signature.
            // Если найдем, то будем добавлять текст в список DoubleNode. Строим списки параллельно у обоих файлов.
            // создаем объект BinaryReader
            using (BinaryReader readerSF = new BinaryReader(File.Open(SourceFile, FileMode.Open)))
            { // откроем файл Source на чтение
                FileInfo src = new FileInfo(SourceFile);
                long l = src.Length;
                int sign_pointer = 0; // 0-сигнатура еще не встречена, 1..N - номер символа в сигнатуре
                long onepercent = l / 100 - 1, percent = onepercent;
                progressBar1.Value = 0;
                for (long i = 0; i < l; i++)
                { // посимвольно читаем исходный файл
                    var b = readerSF.ReadByte();
                    if (Signature[sign_pointer] == b) //найден символ из сигнатуры
                    {
                        sign_pointer++;
                        if (Signature.Length == sign_pointer)
                        { // Сигнатура найдена. Сбросим указатель сигнатуры
                            sign_pointer = 0;
                            if (i + 4 >= l) break; // конец файла достигнут - сигнатура ошибочна
                            Int32 lentxt = readerSF.ReadInt32(); // читаем число int - 4 байта -длина текстовых данных
                            i += 4;
                            // читаем текст длиной lentxt
                            string message = "";
                            if (i + lentxt >= l) break; // конец файла достигнут - сигнатура ошибочна
                            for (int j = 0; j < lentxt; j++) { message+= (char)readerSF.ReadByte(); i++; }
                            // создаем элемент списка с новой записью
                            linkedListSF.Add(message); // создаем новый элемент списка
                            
                        }
                    }
                    else { sign_pointer = 0; }// сигнатура не подтвердилась 
                    // ищем сигнатуру

                    if (i >= percent) 
                    { if (progressBar1.Value < 100) { progressBar1.Value++; }
                      percent += onepercent; progressBar1_lb.Text = progressBar1.Value.ToString();
                    }
                }
                progressBar1.Value = 100;
                progressBar1_lb.Text = "100%";
            }

            // создаем объект BinaryReader для OutputFile
           /* using (BinaryReader readerOF = new BinaryReader(File.Open(OutputFile, FileMode.Open)))
            { // откроем файл .tmp$$ на Чтение

            }*/
            //Выведем в SourceFile_tb первый элемент списка
            foreach (var item in linkedListSF)
            {
                Source_tb.Text = (string) item;
                Source_tb.Text = (string) item;
                break;
            }
            

        }
    }

    public class DoublyNode <T>
    {
        public DoublyNode(T data)
        {                   //Класс DoubleNode является обобщенным, поэтому может хранить данные любого типа. 
            Data = data;    //Для хранения данных предназначено свойство Data.
        }
        public T Data { get; set; }
        public DoublyNode<T> Previous { get; set; } // предыдущий узел
        public DoublyNode<T> Next { get; set; }     // следующий узел
    }
    public class DoublyLinkedList<T> : IEnumerable<T>  // класс - двусвязный список
    {
        DoublyNode<T> head; // головной/первый элемент
        DoublyNode<T> tail; // последний/хвостовой элемент
        int count;  // количество элементов в списке

        // добавление элемента
        public void Add(T data)
        {
            DoublyNode<T> node = new DoublyNode<T>(data);

            if (head == null)
                head = node;
            else
            {
                tail.Next = node;
                node.Previous = tail;
            }
            tail = node;
            count++;
        }
        public void AddFirst(T data)
        {
            DoublyNode<T> node = new DoublyNode<T>(data);
            DoublyNode<T> temp = head;
            node.Next = temp;
            head = node;
            if (count == 0)
                tail = head;
            else
                temp.Previous = node;
            count++;
        }
        // удаление
        public bool Remove(T data)
        {
            DoublyNode<T> current = head;

            // поиск удаляемого узла
            while (current != null)
            {
                if (current.Data.Equals(data))
                {
                    break;
                }
                current = current.Next;
            }
            if (current != null)
            {
                // если узел не последний
                if (current.Next != null)
                {
                    current.Next.Previous = current.Previous;
                }
                else
                {
                    // если последний, переустанавливаем tail
                    tail = current.Previous;
                }

                // если узел не первый
                if (current.Previous != null)
                {
                    current.Previous.Next = current.Next;
                }
                else
                {
                    // если первый, переустанавливаем head
                    head = current.Next;
                }
                count--;
                return true;
            }
            return false;
        }

        public int Count { get { return count; } }
        public long FilePosition { get { return FilePosition; } }

        public bool IsEmpty { get { return count == 0; } }

        public void Clear()
        {
            head = null;
            tail = null;
            count = 0;
        }

        public bool Contains(T data)
        {
            DoublyNode<T> current = head;
            while (current != null)
            {
                if (current.Data.Equals(data))
                    return true;
                current = current.Next;
            }
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            DoublyNode<T> current = head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }
        public IEnumerable<T> BackEnumerator()
        {
            DoublyNode<T> current = tail;
            while (current != null)
            {
                yield return current.Data;
                current = current.Previous;
            }
        }
    }
}

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
        static readonly int MaxBytesMessage = 2200;
        public long SourceNodeCounter = 0; // счетчик-указатель на текущую запись списка
        public string SourceFile; // бинарный файл
        public string TranslatedFile; //Текстовый файл частично переведенный ранее со знаком разделителем "="
        public string OutputFile; // Выходной текстовый файл с текущим рабочим переводом
        public DoublyLinkedList<string> linkedListSF = new DoublyLinkedList<string>(); // связный список для исходного файла
        public DoublyLinkedList<string> linkedListOF = new DoublyLinkedList<string>(); // связный список для выходного файла
        public struct DataNode 
        {
           public string str;
           public long pos;
        }
        public byte [] Signature= {0x04,0x00,0x06,0x00 };  // Сигнатура из байт
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
          if (SourceFile == null || SourceFile=="") return; //без имени файла дальше нечего делать
          OutputFile = SourceFile + ".tmp$$";
          if (File.Exists(SourceFile +".txt")) TranslatedFile= SourceFile + ".txt"; // определяем вспомогательный файл с переводом по-умолчанию
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
        private void OpenTranslatedFile_tsmi_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new();
            openFileDialog1.ShowDialog();
            TranslatedFile = openFileDialog1.FileName;
            if (TranslatedFile == null || TranslatedFile == "") return; //без имени файла дальше нечего делать

            //BinaryReader reader = new BinaryReader(File.Open(SourceFile, FileMode.Open));
            // создаем объект BinaryWriter
            
        }

        private void OpenTranslatedFile()
        {       
            if (!File.Exists(TranslatedFile)) return; // файл не существует, открывать нечего
                using (BinaryReader readerTF = new BinaryReader(File.Open(TranslatedFile, FileMode.Open)))
                { // откроем файл Translated Text File на чтение
                    FileInfo src = new FileInfo(TranslatedFile);
                    byte[] buf1 = new byte[MaxBytesMessage * 3]; //Буффер для чтения из файла строки текста до знака =
                    byte[] buf2 = new byte[MaxBytesMessage * 3]; //Буффер для чтения из файла строки текста после знака =
                    long l = src.Length; //размер исходного файла в байтах
                    long onepercent = l / 100 - 1, percent = onepercent;
                    byte b;
                    bool sourcePart = true;
                    int bufcounter = 0;
                    progressBar1.Value = 0; 
                    progressBar1_lb.Text = "0 %";
                    for (long i = 0; i < l; i++)
                    { // посимвольно читаем исходный файл в буффер
                        b = readerTF.ReadByte(); bufcounter++;
                        if (b==0x3d && sourcePart == true) //найден символ =
                        { //теперь строку из буфера нужно проверить на соответствие строке из списка linkedListSF
                            bufcounter--;
                            if (sourcePart is true) { CompareStrings(buf1, bufcounter, linkedListSF); sourcePart = false; }
                            bufcounter = 0; continue;
                        }
                        // Если встретили в оригинальной части фразы, то просто пропускаем
                        if (sourcePart == true && (b == 0xa || b == 0xd )) { bufcounter--; continue; }
                        // если втретили в переводе, то будем ожидать новой фразы пеервода
                        if (sourcePart == false && (b == 0xa || b == 0xd)) 
                        {   
                            CompareStrings(buf2, bufcounter, linkedListOF); //вносим перевод или заменяем имеющийся
                            bufcounter=0; 
                            sourcePart = true; 
                            continue; 
                        } 
                        if (bufcounter > MaxBytesMessage){ bufcounter = 0; continue; }// размер сообщения превышен - урезаем его
                        
                        if (sourcePart == true) buf1[bufcounter - 1] = b; else buf2[bufcounter] = b;

                        // играем с прогрессбаром
                        if (i >= percent)
                        {
                            if (progressBar1.Value < 100) { progressBar1.Value++; }
                            percent += onepercent; progressBar1_lb.Text = progressBar1.Value.ToString();
                        }
                    }
                    progressBar1.Value = 100;
                    progressBar1_lb.Text = "100%";
                    if (progressBar1.Value == 0)
                    { // После поиска по сигнатуре - ничего не найдено.
                        progressBar1_lb.Text = "";
                    }

                }
        }
        private int CompareStrings( byte [] buffer, long length, DoublyLinkedList<string> linkedList)
        { //возвращает 0 если найдена такая же запись как в списке, либо число расхождений
          // Если число расхождений больше 20% создаем новую запись в списке
            int uneq=0; //число несовпадений
            string str1="";
            byte []buf1=new byte[length];
            for(int i=0;i<length;i++)
            {
                buf1[i] = buffer[i];
                str1 += (char)buf1[i];
            }

            foreach (var item in linkedList)
            {
                Source_tb.Text = item;
                
            }
            return uneq;
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
            if (SourceFile == null) return; // файл еще не открыт
            using (BinaryWriter writer = new BinaryWriter(File.Open(OutputFile + ".txt", FileMode.OpenOrCreate)))
            {
                using (BinaryReader readerSF = new BinaryReader(File.Open(SourceFile, FileMode.Open)))
                { // откроем файл Source на чтение
                    FileInfo src = new FileInfo(SourceFile);
                    byte[] buf = new byte[MaxBytesMessage]; //Буффер для чтения из файла строки текста
                    long l = src.Length; //размер исходного файла в байтах
                    int sign_pointer = 0; // 0-сигнатура еще не встречена, 1..N - номер символа в сигнатуре
                    long onepercent = l / 100 - 1, percent = onepercent;
                    byte b;
                    progressBar1.Value = 0; 
                    progressBar1_lb.Text = "0 %";
                    for (long i = 0; i < l; i++)
                    { // посимвольно читаем исходный файл
                        b = readerSF.ReadByte();
                        if (Signature[sign_pointer] == b) //найден символ из сигнатуры
                        {
                            sign_pointer++; //счетчик найденных символов из сигнатуры
                            if (Signature.Length == sign_pointer) //Сигнатура найдена. Сбросим указатель сигнатуры
                            { 
                                sign_pointer = 0; 
                                if ((i + 4) >= l) break; // конец файла достигнут - сигнатура ошибочна
                                Int32 lentxt = readerSF.ReadInt32(); // читаем число int - 4 байта -длина текстовых данных
                                i += 4;
                                if (lentxt > MaxBytesMessage || lentxt<4) continue; // не может быть такое длинное предложение
                                
                                // читаем текст длиной lentxt
                                string message = "";
                                if ((i + lentxt) >= l) break; // конец файла достигнут - сигнатура ошибочна
                                for (int j = 0; j < lentxt; j++) { buf[j] = readerSF.ReadByte(); message += (char)buf[j]; i++; }
                                // создаем элемент списка с новой записью
                                linkedListSF.Add(message, i + 1); // создаем новый элемент списка, с файловым указателем на начало строки
                                buf[lentxt+1] = 0xd; buf[lentxt+2] = 0xa; buf[lentxt] = 0x3d; // добавляем к концу строки "=0xd0xa"
                                writer.Write(buf,0,lentxt+2); // записываем строку текста в выходной файл
                            }
                        }
                        else { sign_pointer = 0; }// сигнатура не подтвердилась 
                                                  // ищем сигнатуру

                        if (i >= percent)
                        {
                            if (progressBar1.Value < 100) { progressBar1.Value++; }
                            percent += onepercent; progressBar1_lb.Text = progressBar1.Value.ToString();
                        }
                    }
                    progressBar1.Value = 100;
                    progressBar1_lb.Text = "100%";
                    if (progressBar1.Value == 0)
                    { // После поиска по сигнатуре - ничего не найдено.
                        progressBar1_lb.Text = "";
                    }
                    nudRecord.Maximum = linkedListSF.Count;
                    nudRecord.Minimum = 1;

                    Records_lb.Text = "Found " + linkedListSF.Count + " records.";
                    Translated_tb.ReadOnly = false;
                    
                }

                //Выведем в SourceFile_tb первый элемент списка
                foreach (var item in linkedListSF)
                {
                    Source_tb.Text = item;
                    break;
                }
                nudRecord.Value = 1;
                nudRecord.ReadOnly = false;
            }
            OpenTranslatedFile();
            /*
            using (BinaryWriter writer = new BinaryWriter(File.Open(OutputFile+".txt", FileMode.OpenOrCreate)))
            {
                foreach (var item in linkedListSF)
                {                  
                    writer.Write(item);
                }
            }*/

        }
        private void Next_btn_Click(object sender, EventArgs e)
        {
            nudRecord.ReadOnly = true;
            if (nudRecord.Value == linkedListSF.Count) { nudRecord.Value = 1; } else { nudRecord.Value++;}
            Source_tb.Text = (string)linkedListSF.NextNode();
            nudRecord.ReadOnly = false;
        }
        private void Prev_btn_Click(object sender, EventArgs e)
        {
            nudRecord.ReadOnly = true;
            if (nudRecord.Value == 1) { nudRecord.Value = linkedListSF.Count; } else {nudRecord.Value--; }
            Source_tb.Text = (string)linkedListSF.PrevNode();
            nudRecord.ReadOnly = false;
        }
        private void nudRecord_ValueChanged(object sender, EventArgs e)
        {
            if (nudRecord.ReadOnly==true) return; // это пришел афтершок из функций Next_btn_Click И Prev_btn_click
            long counter = (long)nudRecord.Value;
            foreach (var item in linkedListSF)
            {
                counter--;
                if (counter == 0) { Source_tb.Text = (string)item; break; }
            }
        }
        private void SearchSource_Click(object sender, EventArgs e)
        {   // поиск по тексту из входящего файла
            if (SearchSource_tb.Text.Length == 0) return; // не задана строка поиска
            foreach (var item in linkedListSF)
            {
                var str1 = (string)item;
                if (str1 == SearchSource_tb.Text) { Source_tb.Text = (string)item; break; }
            }
        }

        private void SearchTranslated_btn_Click(object sender, EventArgs e)
        {

        }


    }

    public class DoublyNode <T>
    {
        public DoublyNode(T data)
        {                   //Класс DoubleNode является обобщенным, поэтому может хранить данные любого типа. 
            Data = data;    //Для хранения данных предназначено свойство Data.
        }
        public T Data { get; set; }
        public long Fileposition { get; set; } 
        public DoublyNode<T> Previous { get; set; } // предыдущий узел
        public DoublyNode<T> Next { get; set; }     // следующий узел
    }
    public class DoublyLinkedList<T> : IEnumerable<T>  // класс - двусвязный список
    {
        DoublyNode<T> curr; //текущий элемент
        DoublyNode<T> head; // головной/первый элемент
        DoublyNode<T> tail; // последний/хвостовой элемент
        int count;  // количество элементов в списке
 //       DoublyNode<T> curr; // текущий элемент

        // добавление элемента
        public void Add(T data, long Fileposition)
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
            node.Fileposition = Fileposition;
        }
        public void AddFirst(T data, long Fileposition)
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
            node.Fileposition = Fileposition;
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
        public long FilePosition { get { return curr.Fileposition; } }

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
                curr = current;
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
                curr = current;
            }
        }

        public IEnumerable<T> BackEnumerator()
        {
            DoublyNode<T> current = tail;
            while (current != null)
            {
                yield return current.Data;
                current = current.Previous;
                curr = current;
            }
        }

        public object NextNode()
        {
            DoublyNode<T> current;
            if (count == 0) return "Text records were not found !"; // список пуст
            if (curr == null) curr = head;
            current = curr;
            if (current.Next == null) { current = head; }
            else { current = current.Next; }
            curr = current;
            return current.Data;
        }
        public object PrevNode()
        {
            DoublyNode<T> current;
            if (count==0) return "Text records were not found !"; // список пуст
            if (curr == null) curr = head;
            current = curr;
            if (current.Previous == null) { current = tail; }
            else { current = current.Previous; }
            curr = current;
            return current.Data;
        }
    }
}

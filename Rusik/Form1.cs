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
using System.Text.RegularExpressions;


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
        /*public struct DataNode 
        {
           public string str;
           public long pos;
        }*/
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

        private void SaveFile(string filename)
        {
            /* FileInfo src = new FileInfo(filename);
             if (src.Exists)
             {
                 src.MoveTo(OutputFile);
             }
             else { MessageBox.Show("Error writing to file:",OutputFile, MessageBoxButtons.OK); }*/
            using (BinaryWriter writer = new BinaryWriter(File.Open(filename, FileMode.OpenOrCreate)))
            {
                // записываем в файл значение каждого свойства объекта
                foreach (var item in linkedListSF)
                {
                    byte[] bytes = System.Text.Encoding.UTF8.GetBytes(item+"=");
                    writer.Write(bytes);

                    writer.Write(System.Text.Encoding.UTF8.GetBytes(linkedListSF.Twin.Data+"\n"));

                    string tmp = linkedListSF.Twin.Data;
                  //  writer.Write(System.Text.Encoding.UTF8.GetBytes("\n"));                   

                }
            }
        }
        private void SaveFile_tsmi_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new();
            saveFileDialog1.OverwritePrompt = true; //предупреждение о перезаписи
            saveFileDialog1.ShowDialog();
            string tmpOutputFile = saveFileDialog1.FileName;
            if (tmpOutputFile == null || tmpOutputFile == "") return;
            SaveFile(tmpOutputFile);
        }

        private void OpenFile_tsmi_Click(object sender, EventArgs e)
        {
          OpenFileDialog openFileDialog1 = new();
          openFileDialog1.ShowDialog();
          string tmpSourceFile = openFileDialog1.FileName;
            if (tmpSourceFile == "") return; // 
            else SourceFile = tmpSourceFile;
          if (SourceFile == null || SourceFile=="") return; //без имени файла дальше нечего делать
          OutputFile = SourceFile + ".tmp$$";
          if (File.Exists(SourceFile +".txt")) TranslatedFile= SourceFile + ".txt"; // определяем вспомогательный файл с переводом по-умолчанию
          //BinaryReader reader = new BinaryReader(File.Open(SourceFile, FileMode.Open));
          // создаем объект BinaryWriter
          if (!File.Exists(OutputFile))
          {   // Если выходной файл еще не существует, копируем выбранный файл во временный
              FileInfo srcF = new FileInfo(SourceFile);
              srcF.CopyTo(OutputFile);
              FileInfo outF = new FileInfo(OutputFile); // ставим атрибуты на копию hidden
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
        }

        private void OpenTranslatedFile_tsmi_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new();
            openFileDialog1.ShowDialog();
            TranslatedFile = openFileDialog1.FileName;
            if (TranslatedFile == null || TranslatedFile == "") return; //без имени файла дальше нечего делать
            if(SourceFile=="" || SourceFile==null)OpenTranslatedFile();
        }

        private void OpenTranslatedFile()
        {       
            if (!File.Exists(TranslatedFile)) return; // файл не существует, открывать нечего
            using (BinaryReader readerTF = new BinaryReader(File.Open(TranslatedFile, FileMode.Open)))
            { // откроем файл Translated Text File на чтение
                FileInfo src = new FileInfo(TranslatedFile);
                byte[] buf1 = new byte[MaxBytesMessage * 3]; //Буффер для чтения из файла строки текста до знака =
                byte[] buf2 = new byte[MaxBytesMessage * 3]; //Буффер для чтения из файла строки текста после знака =
                string str1 = "", str2 = "";
                long l = src.Length; //размер исходного файла в байтах
                long onepercent = l / 100 - 1, percent = onepercent;
                byte b;
                bool sourcePart = true;
                bool skipTranslateFlag = false;
                int bufcounter = 0;

                for (long i = 0; i < l; i++) ////////////!!!!!!
                { // посимвольно читаем исходный файл в буффер
                    b = readerTF.ReadByte(); bufcounter++;
                    if (b==0x3d && sourcePart == true) //найден символ =  
                    { //теперь строку из буфера нужно проверить на наличие в списке linkedListSF оригинальных строк
                        bufcounter--;
                        byte[] tmp_bytes1 = new byte[bufcounter];
                        for (int j = 0; j < bufcounter; j++) tmp_bytes1[j] = buf1[j];
                        
                        str1 = Encoding.UTF8.GetString(tmp_bytes1);
                        if (str1 == "Accomplishing side missions can result in achievements or the discovery of rare files.") 
                        {
                            bufcounter = 0;
                        }
                        var maxK=FindSameString(str1, linkedListSF); // curr указывает куда и unq
                        if (maxK < 0.95) // строка из доп файла не похожа ни на одну строку из бинарного файла,
                        {               // либо бинарный файл не был открыт
                            linkedListSF.Add(str1, 0); // создаем новый элемент списка, с файловым указателем на начало строки
                        }
                        else skipTranslateFlag = true; //такая строка отсутствует в оригинальном списке
                        sourcePart = false; 
                        bufcounter = 0;
                        continue;
                    }
                    // Если встретили символы в оригинальной части фразы, то просто пропускаем
                    // if (sourcePart == true && (b == 0xa || b == 0xd )) { bufcounter--; continue; }
                    // если "0d 0a" втретили в переводе, то это конец строки и будем ожидать новой фразы пеервода
                    if (sourcePart == false && (b == 0xa && (buf2[bufcounter-2] == 0xd)||(i>=l-1)))
                    {
                        bufcounter--; //удаляем последниe символы 0d и 0a
                        byte[] tmp_bytes2=new byte[bufcounter];
                        for (int j = 0; j < bufcounter; j++) tmp_bytes2[j] = buf2[j];
                        str2= Encoding.UTF8.GetString(tmp_bytes2);
                        bufcounter =0; 
                        sourcePart = true;
                        if (skipTranslateFlag != true) //вносим перевод или заменяем имеющийся
                        {
                            if (linkedListSF.curr.Twin == null)
                            {
                                linkedListOF.Add(str2, 0); // создаем запись в списке с переводом
                                linkedListSF.SetTwin(linkedListOF.curr); //связываем ссылками исходную строку со строкой перевода в списках
                                linkedListOF.SetTwin(linkedListSF.curr);
                            }
                            else
                            {
                                linkedListOF.SetCurrent(linkedListSF.Twin);
                                if (linkedListOF.curr != null) linkedListOF.ReplaceData(str2); else MessageBox.Show("Shit happen. Err Replacing!");
                            }
                        }
                        else skipTranslateFlag = false;
                        continue; 
                    } 
                    if (bufcounter > MaxBytesMessage){ bufcounter = 0; continue; }// размер сообщения превышен - урезаем его

                    if (sourcePart == true){ buf1[bufcounter - 1] = b; } 
                    else { buf2[bufcounter-1] = b; }

                    // играем с прогрессбаром
                    if (i >= percent)
                    {
                        if (progressBar1.Value < 100) { progressBar1.Value++; }
                        percent += onepercent; progressBar1_lb.Text = progressBar1.Value.ToString()+" %";
                    }
                }
                //progressBar1.Value = 0;
                //progressBar1_lb.Text = "";
                nudRecord.Maximum = linkedListSF.Count;
                nudRecord.Minimum = 1;

                Records_lb.Text = "Found " + linkedListSF.Count + " records.";
                Translated_tb.ReadOnly = false;
                //Выведем в SourceFile_tb первый элемент списка
                foreach (var item in linkedListSF) { Source_tb.Text = item; break; }
                foreach (var item in linkedListOF) { Translated_tb.Text = item; break; }
                nudRecord.Value = 1;
                nudRecord.ReadOnly = false;
            }
        }

        private float FindSameString( string str1, DoublyLinkedList<string> linkedList)
        { //возвращает 0 если найдена такая же запись как в заданном списке, либо число расхождений
          // Если число расхождений больше 3% создаем новую запись в списке
            float kTanimoto=0, kTanimoto_tmp; // степень схожести строк [0..1]. 0-Несхожие. 1-идентичные
            long kMaxfilePosition=0;

            foreach (var item in linkedList) // прямое сравнение строк на точное совпадение
                if (str1 == item)// фразы в linkedlistSF и доп.файле с переводом - совпали
                  return 1;  // строка есть в списке
            
            // Расчет коэфф.Танимото схожести для всех строк списка и поиска максимального

            foreach (var item in linkedList) 
            {
               // Найдем строку в списке linkedlist максимально схожую (по алгоритму Танимото) с входной строкой
               kTanimoto_tmp = Tanimoto(str1,item);
               if (kTanimoto < kTanimoto_tmp) { kTanimoto = kTanimoto_tmp; kMaxfilePosition = linkedList.FilePosition; }
            }

            foreach (var item in linkedList) //Найдем запись из списка с макс.коэфф.Танимото и заменим в нем перевод
            {
                if (linkedList.FilePosition== kMaxfilePosition){  break; }
            }
            return kTanimoto;
        }// MessageBox.Show(str1,"Обнаружены непечатные символы в строке:", MessageBoxButtons.OK); 

        private float Tanimoto(string str1, string str2)
        { //Коэффициент Танимото – описывает степень схожести двух множеств. 
          // при использовании строк с русскими буквами, их лучше подавать сюда в Unicode
            float kTanimoto = 0;
            //str1.ToLower(); str2.ToLower(); //переводим обе строки в нижний регистр - в этой проге, точнее так не делать
            string str1out = Regex.Replace(str1, @"^[A-Za-z0-9]+ ", String.Empty); // Оставим Англ.буквы цифры и пробел
            string str2out = Regex.Replace(str2, @"^[A-Za-z0-9]+ ", String.Empty); // Оставим Англ.буквы цифры и пробел
            int a = 0; // кол-во элементов в 1-ом множестве
            int b = 0; //кол-во элементов во 2-ом множестве
            int c = 0; //кол-во общих элементов  в двух множествах
            if (Math.Abs(str1out.Length - str2out.Length) != 0) return 0; // отличие в длине вычищенных строк -строки точно не совпадают
            if (str1out == str2out) return 1; // строки идентичны
            Dictionary<char, int> dictionarys1 = str1out.GroupBy(x => x)
                .ToDictionary(x => x.Key, x => x.Count());
            Dictionary<char, int> dictionarys2 = str2out.GroupBy(x => x)
                .ToDictionary(x => x.Key, x => x.Count());
            a = dictionarys1.Count;
            b = dictionarys2.Count;
            // пересечение последовательностей
            var result = dictionarys1.Intersect(dictionarys2);
            c = result.Count();

            kTanimoto =(float)c / (a + b - c);
            return kTanimoto; // Чем ближе к 1 , тем достовернее сходство. 0.85 - уже вполне достоверно
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
           // LoadFile();
        }
        private void About_tsmi_Click(object sender, EventArgs e)
        {
            string str1="This program may be useful for translation text in some binary files.";
            MessageBox.Show(str1, "About program ...", MessageBoxButtons.OK);
        }

        private void Start_btn_Click(object sender, EventArgs e)
        {
            //if (Signature_tb.TextLength == 0) { MessageBox.Show("You must setup Signature to start."); return; }
            // начиная со смещения Offset начнем по байтам искать сигнатуру Signature.
            // Если найдем, то будем добавлять текст в список DoubleNode. Строим списки параллельно у обоих файлов.
            // создаем объект BinaryReader
            if (SourceFile == "" || SourceFile==null) return; // файл еще не открыт
            Start_btn.Visible = false;
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
                                if ((lentxt > MaxBytesMessage) || lentxt < 3) continue; // не может быть такое длинное/короткое предложение
                                if ((i + lentxt) >= l) break; // конец файла достигнут - сигнатура ошибочна

                                for (int j = 0; j < lentxt; j++) //читаем lentxt байт сообщения
                                {
                                    buf[j] = readerSF.ReadByte(); i++;
                                    if (j > 2 && buf[j] == 0xa6)//заменяем символ UTF-8 E280A6 на три точки 2E2E2E .
                                        if (buf[j - 1] == 0x80)
                                          if (buf[j - 2] == 0xe2) { buf[j] = 0x2e; buf[j - 1] = 0x2e; buf[j - 2] = 0x2e;}

                                    if (j > 2 && buf[j] == 0x90)//заменяем символ UTF-8 E38090 на скобку 0x5b [
                                        if (buf[j - 1] == 0x80)
                                            if (buf[j - 2] == 0xe3) { buf[j - 2] = 0x5b; j -= 2; lentxt -= 2; }

                                    if (j > 2 && buf[j] == 0x91)//заменяем символ UTF-8 E38090 на скобку 0x5b [
                                        if (buf[j - 1] == 0x80)
                                            if (buf[j - 2] == 0xe3) { buf[j - 2] = 0x5d; j -= 2; lentxt -= 2; }

                                    if (j > 3 && buf[j] == 0x3e) //3c, 62||42, 52||72, 3E // <br> или <BR> заменяем на \n  0x5c,0x6e
                                        if (buf[j-1]==0x72 || buf[j-1]==0x52)
                                            if (buf[j - 2] == 0x62 || buf[j - 2] == 0x42)
                                                if(buf[j - 3]== 0x3c) { buf[j - 3]=0x5c; buf[j - 2] =0x6e; lentxt -= 2; j -= 2; }
                                    // 2e 3d 0d e3 80 90  на space+[ ; e3 80 91 3d 0d
                                }
                                string message = "";  // преобразованиЕ массива byte в строку string
                                for (int j = 0; j < lentxt; j++) message += (char)buf[j];
                                if (message == "..." || message=="") continue; // пустые строки не переводим
                                // создаем элемент списка с новой записью                                                           
                                linkedListSF.Add(message, i + 1); // создаем новый элемент списка, с файловым указателем на начало строки
                                linkedListOF.Add("", i + 1); //одновременно создаем список с переводом
                                linkedListSF.SetTwin(linkedListOF.curr); //связываю ссылками исходную строку со строкой перевода
                                linkedListOF.SetTwin(linkedListSF.curr);
                                buf[lentxt + 1] = 0xd; buf[lentxt + 2] = 0xa; buf[lentxt] = 0x3d; // добавляем к концу строки "=0xd0xa"
                                writer.Write(buf, 0, lentxt + 2); // записываем строку текста в выходной файл
                            }
                        }
                        else { sign_pointer = 0; }// сигнатура не подтвердилась
                        if (i >= percent) // проверяем нужно ли двигать прогресс бар на 1%
                        {
                            if (progressBar1.Value < 100) { progressBar1.Value++; }
                            percent += onepercent; progressBar1_lb.Text = Convert.ToString(progressBar1.Value)+" %";
                        }
                    }
                    progressBar1.Value = 0;
                    progressBar1_lb.Text = "";
                    nudRecord.Maximum = linkedListSF.Count;
                    nudRecord.Minimum = 1;

                    Records_lb.Text = "Found " + linkedListSF.Count + " records.";
                    Translated_tb.ReadOnly = false;
                    //Выведем в SourceFile_tb первый элемент списка
                    foreach (var item in linkedListSF) { Source_tb.Text = item; break; }
                    foreach (var item in linkedListOF) { Translated_tb.Text = item; break; }
                    nudRecord.Value = 1;
                    nudRecord.ReadOnly = false;
                }
                OpenTranslatedFile();
            }
        }

        private void Next_btn_Click(object sender, EventArgs e)
        {
            nudRecord.ReadOnly = true;
            if (linkedListSF.Count == 0) return; // список пуст перемещение вперед невозможно
            if (nudRecord.Value == linkedListSF.Count) { nudRecord.Value = 1; } else { nudRecord.Value++;}
            //проверим, если TextBox изменился - сохраним его
            linkedListOF.SetCurrent(linkedListSF.Twin);
            if ((string)linkedListOF.Current != Translated_tb.Text) 
                if(linkedListSF.Twin==linkedListOF.curr)linkedListOF.ReplaceData(Translated_tb.Text);

            byte[] bytes = Encoding.Default.GetBytes((string)linkedListSF.NextNode());
            Source_tb.Text = Encoding.UTF8.GetString(bytes);
            bytes = Encoding.Default.GetBytes((string)linkedListOF.NextNode());
            Translated_tb.Text = Encoding.UTF8.GetString(bytes);
            nudRecord.ReadOnly = false;
        }
        private void Prev_btn_Click(object sender, EventArgs e)
        {
            nudRecord.ReadOnly = true;
            if (linkedListSF.Count == 0) return; // список пуст перемещение назад невозможно
            if (nudRecord.Value == 1) { nudRecord.Value = linkedListSF.Count; } else {nudRecord.Value--; }
            //проверим, если TextBox изменился - сохраним его
            linkedListOF.SetCurrent(linkedListSF.Twin);
            if ((string)linkedListOF.Current != Translated_tb.Text)
                if (linkedListSF.Twin == linkedListOF.curr) linkedListOF.ReplaceData(Translated_tb.Text);

            byte[] bytes = Encoding.Default.GetBytes((string)linkedListSF.PrevNode());
            Source_tb.Text = Encoding.UTF8.GetString(bytes);
            bytes = Encoding.Default.GetBytes((string)linkedListOF.NextNode());
            Translated_tb.Text = Encoding.UTF8.GetString(bytes);
            nudRecord.ReadOnly = false;
        }
        private void nudRecord_ValueChanged(object sender, EventArgs e)
        {
            if (nudRecord.ReadOnly==true) return; // это пришел афтершок из функций Next_btn_Click И Prev_btn_click
            long counter = (long)nudRecord.Value;
            foreach (var item in linkedListSF)
            {
                counter--;
                if (counter == 0) 
                { 
                    byte[] bytes = Encoding.Default.GetBytes(item);
                    Source_tb.Text = Encoding.UTF8.GetString(bytes);
                    linkedListOF.curr = linkedListSF.Twin;
                    bytes = Encoding.Default.GetBytes(linkedListOF.curr.Data);
                    Translated_tb.Text = Encoding.UTF8.GetString(bytes);
                    break; 
                }
            }
        }
        private void SearchSource_Click(object sender, EventArgs e)
        {   // поиск по тексту из входящего файла
            if (SearchSource_tb.Text.Length == 0) return; // не задана строка поиска
            //нужно открыть новую вкладку, и создать новый список со всеми включениями подстроки
            foreach (var item in linkedListSF)
            { 
                var str1 = (string)item;
                if (str1 == SearchSource_tb.Text) 
                {
                    byte[] bytes = Encoding.Default.GetBytes(item);
                    Source_tb.Text = Encoding.UTF8.GetString(bytes);
                    bytes = Encoding.Default.GetBytes(linkedListSF.Twin.Data);
                    Translated_tb.Text = Encoding.UTF8.GetString(bytes);
                    break; 
                }
            }
        }

        private void SearchTranslated_btn_Click(object sender, EventArgs e)
        {

        }

        private void closeFilesClearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // сохранение файлов/очистка списков
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
        public DoublyNode<T> Twin { get; set; }     // ссылка на связанный элемент из оригинального/переведенного списка
    }
    public class DoublyLinkedList<T> : IEnumerable<T>  // класс - двусвязный список
    {
        public DoublyNode<T> curr; //текущий элемент
        DoublyNode<T> head; // головной/первый элемент
        DoublyNode<T> tail; // последний/хвостовой элемент
        int count;  // количество элементов в списке
        
        public void Add(T data, long Fileposition)// добавление элемента
        {
            DoublyNode<T> node = new DoublyNode<T>(data);
            curr = node;    // добавляемый элемент становится текущим
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
            curr = node; // добавляемый элемент становится текущим
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
            curr = current; // элемент становится текущим
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
        public long FilePosition { get { if (curr != null) return curr.Fileposition;  else return -1; } }
        public DoublyNode<T> Twin { get { return curr.Twin; } }

        public bool IsEmpty { get { return count == 0; } }
        public object Current { get { return curr.Data; } }
        
        public void ReplaceData(T data)
        {
            if (curr == null) return;
            curr.Data = data;
        }
        public void SetTwin(DoublyNode<T> twin)
        {
            if (curr == null) return;
            curr.Twin = twin;
        }
        public void SetCurrent(DoublyNode<T> current)
        {
            if (curr == null) return;
            curr =current;
        }

        public void Clear()
        {
            head = null;
            tail = null;
            curr = null;
            count = 0;
        }

        public bool Contains(T data)
        {
            DoublyNode<T> current = head;
            while (current != null)
            {
                if (current.Data.Equals(data)) { curr = current; return true; }
                current = current.Next;
            }
            curr = current;
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
                curr = current;
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
                curr = current;
                yield return current.Data;
                current = current.Previous;
                curr = current;
            }
        }
        public object NextNode()
        {
            DoublyNode<T> current;
            if (count == 0) return null; // список пуст
            if (curr == null) { if (head == null) return null; curr = head; }
            current = curr;
            if (current.Next == null) { current = head; }
            else { current = current.Next; }
            curr = current;
            return current.Data;
        }
        public object PrevNode()
        {
            DoublyNode<T> current;
            if (count == 0) return null; // список пуст
            if (curr == null) { if (head == null) return null; curr = head; }
            current = curr;
            if (current.Previous == null) { current = tail; }
            else { current = current.Previous; }
            curr = current;
            return current.Data;
        }
    }
}

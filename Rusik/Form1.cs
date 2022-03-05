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
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Web;
using System.Text.RegularExpressions;


namespace Rusik
{
    public partial class Form1 : Form
    {
        static readonly int MaxBytesMessage = 7000; // Максимальный размер сообщения в байтах
        public long SourceNodeCounter = 0; // счетчик-указатель на текущую запись списка
        public string SourceFile; // бинарный файл
        public string TranslatedFile; //Текстовый файл частично переведенный ранее со знаком разделителем "="
        public string OutputFile; // Выходной текстовый файл с текущим рабочим переводом
        public DoublyLinkedList<string> linkedListSF = new DoublyLinkedList<string>(); // связный список для исходного файла
        public DoublyLinkedList<string> linkedListOF = new DoublyLinkedList<string>(); // связный список для выходного файла
        public DoublyLinkedList<string> linkedListSS = new DoublyLinkedList<string>(); // связный список для поиска по исходому тексту
        public DoublyLinkedList<string> linkedListOS = new DoublyLinkedList<string>(); // связный список для поиска по пепреводу
        public bool NotSavedYet = false;
        public byte[] Signature = { 0x04, 0x00, 0x06, 0x00 };  // Сигнатура из байт
        BackgroundWorker bgWorker=new();
        public Timer timer1 = new System.Windows.Forms.Timer { Interval = 100 };
        public string languageEn = "en"; //из модуля google-переводчика
        public string languageRu = "ru";
        public string InterfaceLanguage = "ru";
        public long LastRecordNumber=1; // это сохраненный в ini файле nudRecord 
        public Form1()
        {
            InitializeComponent();
            bgWorker.WorkerReportsProgress = true;
            bgWorker .WorkerSupportsCancellation = true;
            this.FormClosing += new FormClosingEventHandler(this.Form1_FormClosing);// обработчик закрытия окна по крестику
            timer1.Tick += new EventHandler(Timer_Tick);
        }
        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Quit_tsmi_Click(sender, e);
            bgWorker.Dispose();
        }

        private void StartBackgroundWork()
        {
            timer1.Enabled = true; bgWorker.RunWorkerAsync();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            /* if (progressBar1.Value < progressBar1.Maximum)
                 progressBar1.Increment(5);
             else
                 progressBar1.Value = progressBar1.Minimum;*/
            progressBar1.Invalidate();

            progressBar1_lb.Text = Convert.ToString(progressBar1.Value);
            progressBar1_lb.Invalidate();
            
        }

        private void Quit_tsmi_Click(object sender, EventArgs e) // МЕНЮ Quit
        {
            // если исходный файл открыт, то предлагаем сохранить выходной файл
            if (linkedListOF.Curr != null) // если в памяти есть список - то предлагаем сохраниться
            {
                if (NotSavedYet == true)
                    do
                    {
                        DialogResult result = MessageBox.Show(
                        "Do yo want to Save \rthe result of your work?",
                        "Message",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly);
                        if (result == DialogResult.No) return; // пользователь отказался от сохранения
                    } while (SaveFile() == false); // согласился сохраниться , но что-то пошло не так. Даем еще одну попытку...
                linkedListOF.Clear();
                linkedListOS.Clear();
                linkedListSF.Clear();
                linkedListSS.Clear();
            }
        }

        private bool SaveFile() // сохраняем список из памяти в файл с разделителем =
        {
            SaveFileDialog saveFileDialog1 = new();
            saveFileDialog1.OverwritePrompt = true; //предупреждение о перезаписи
            saveFileDialog1.ShowDialog();
            string tmpOutputFile = saveFileDialog1.FileName;
            if (tmpOutputFile == null || tmpOutputFile == "") return false;
            long onepercent = linkedListSF.Count / 100 - 1, percent = onepercent;
            progressBar1.Value = 0;
            progressBar1_lb.Text = "";
            using (BinaryWriter writer = new BinaryWriter(File.Open(tmpOutputFile, FileMode.OpenOrCreate)))
            {
                long counter = 0;
                // записываем в файл содержимое списков с данными
                foreach (var item in linkedListSF)
                {
                    if (item == null) break;
                    byte[] bytes = System.Text.Encoding.UTF8.GetBytes(item + "=");
                    writer.Write(bytes);
                    var item2 = linkedListSF.Twin.Data;
                    if (item2 == null) item2 = String.Empty;
                    item2 += "\r\n";
                    bytes = System.Text.Encoding.UTF8.GetBytes(item2);
                    writer.Write(bytes);
                    counter++;
                    if (counter >= percent)
                    {
                        if (progressBar1.Value < 100) 
                        { progressBar1.Value++; }
                        percent += onepercent; 
                    }
                }
            }
            progressBar1.Value = 0;
            //progressBar1_lb.Text = "";
            NotSavedYet = false; // актуальная версия сохранена
            return true;
        }
        private void SaveFile_tsmi_Click(object sender, EventArgs e) // Меню Save File As
        {  SaveFile();
        }

        private void OpenFile_tsmi_Click(object sender, EventArgs e) //MENU Open Binary File
        {
            OpenFileDialog openFileDialog1 = new();
            openFileDialog1.ShowDialog();
            string tmpSourceFile = openFileDialog1.FileName;
            if (tmpSourceFile == "") return; // 
            else SourceFile = tmpSourceFile;
            if (SourceFile == null || SourceFile == string.Empty) return; //без имени файла дальше нечего делать
            OutputFile = SourceFile + ".tmp$$";
            if (File.Exists(SourceFile + ".txt")) TranslatedFile = SourceFile + ".txt"; // определяем вспомогательный файл с переводом по-умолчанию

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

            //разблокируем поля Смещения, Поиска Сигнатуры и Поиска строк текста во входном и выходном файлах
            Offset_tb.ReadOnly = false; // textbox Offset
            Signature_tb.ReadOnly = false; // texbox Signature
            SearchSource_tb.ReadOnly = false;
            SearchTranslated_tb.ReadOnly = false;
            Start_btn.Visible = true;
        }

        private void OpenTranslatedFile_tsmi_Click(object sender, EventArgs e)//MENU Open Text File
        {
            OpenFileDialog openFileDialog1 = new();
            openFileDialog1.ShowDialog();
            TranslatedFile = openFileDialog1.FileName;
            if (TranslatedFile == null || TranslatedFile == string.Empty) return; //без имени файла дальше нечего делать
                                                                                  // if(SourceFile == string.Empty || SourceFile == null) 
            OpenTranslatedFile();
        }

        private void OpenTranslatedFile() // Чтение и разбор текстового переведенного файла
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
                bool sourcePart = true; // флаг 
                bool flag_ReplaceData = false; // флаг
                bool flag_Skipdialog = false; //флаг пропуска пользовательских диалоговых окон
                int bufcounter = 0;
                object maxK_Node = null; // ссылка на потенциально дублирующуюся строку из списка
                object maxK_NodeOF = null; //ссылка на перевод заменяемой строки


        TranslatedFile_tb.Text = TranslatedFile;
               // StartBackgroundWork();

                for (long i = 0; i < l; i++)
                { // посимвольно читаем исходный файл в буффер
                    b = readerTF.ReadByte(); bufcounter++;
                    //Пустые строки не будем принимать за отдельные сообщения
                    if (b == 0xa && bufcounter==2 && sourcePart == true && buf1[bufcounter - 2] == 0xd) 
                    { bufcounter = 0; continue; }
                    
                    if (b == 0x3d && sourcePart == true) //найден символ =  
                    { //теперь строку из буфера нужно проверить на наличие в списке linkedListSF оригинальных строк
                        bufcounter-- ;
                        byte[] tmp_bytes1 = new byte[bufcounter];
                        for (int j = 0; j < bufcounter; j++) tmp_bytes1[j] = buf1[j];
                        str1 = Encoding.UTF8.GetString(tmp_bytes1); // Создаем из буфера с бaйтами строку в UTF8
                        //Если строка пустая, менее 3 символов или содержит символ ENTER, то такое сообщение - пропускаем
                        if (str1 == "" || str1.Length <= 3) { bufcounter = 0; continue; }
                        float maxK;

                        if (flag_Skipdialog == false) //если включен режим пропуска диалога, то поиск совпадающих строк-отключаем
                        { maxK = FindSameString(str1, linkedListSF, out maxK_Node); } // проверим на наличие совпадений в списке
                        else { maxK = 0; }
                        if (maxK_Node == null) { maxK = 0; }
                        maxK_NodeOF = linkedListSF.TwinFrom((DoublyNode<string>)maxK_Node); // получаем ссылку на ячейку с переводом
                        if ((maxK <= 1) && (maxK >= 0.96)) // совпадение от 96 до 100% - это та же самая строка
                        {
                            linkedListSF.ReplaceData(str1, (DoublyNode<string>)maxK_Node);
                            flag_ReplaceData = true;
                        }
                        else if (maxK < 0.96 && maxK > 0.85)// строка очень Похожа на одну из строк в списке,
                        {                                   // спросим пользователя
                            var str2_tmp = linkedListSF.DataFrom((DoublyNode<string>)maxK_Node);
                            DialogResult result = MessageBox.Show(
                            "There were detected couple similar strings! Similarity is " + Convert.ToString(maxK * 100) + "%\nAre it the same?" +
                            "\n1:(" + str1.Length + "): " + str1 +
                            "\n2:(" + ((string)str2_tmp).Length + "): " + str2_tmp +
                            "\n\nCancel will skip this dialog and adding all strings as new.",
                            "Please attention !",
                            MessageBoxButtons.YesNoCancel,
                            MessageBoxIcon.Question,
                            MessageBoxDefaultButton.Button1,
                            MessageBoxOptions.DefaultDesktopOnly);
                            if (result == DialogResult.No) { linkedListSF.Add(str1, 0); }// создаем новый элемент списка
                            else if (result == DialogResult.Yes)// пользователь сказал что строки одинаковые, тогда заменим старую строку новой
                            {
                                linkedListSF.ReplaceData(str1, (DoublyNode<string>)maxK_Node);
                                flag_ReplaceData = true;
                            }
                            else //result == DialogResult.Cancel
                            { flag_Skipdialog = true; }

                   }
                   else // maxK <0.75 можно не спрашивать строка точно уникальная
                   {
                       linkedListSF.Add(str1, 0);// создаем новый элемент списка
                   }
                   sourcePart = false;
                   bufcounter = 0;
                   continue;
                    }
                    // Если встретили символы в оригинальной части фразы, то просто пропускаем
                    // если "0d 0a" втретили в переводе, то это конец строки и будем ожидать новой фразы перeвода
                    if (sourcePart == false && ((b == 0xa && buf2[bufcounter - 2] == 0xd) || (i == l - 1)))
                    {
                        if (i < l - 1) bufcounter -= 2; //удаляем последниe символы 0d и 0a
                        else buf2[bufcounter - 1] = b; // дописываем последний символ в файле
                        byte[] tmp_bytes2 = new byte[bufcounter];
                        for (int j = 0; j < bufcounter; j++) tmp_bytes2[j] = buf2[j];
                        str2 = Encoding.UTF8.GetString(tmp_bytes2);
                        bufcounter = 0;
                        sourcePart = true;

                        if (flag_ReplaceData == false || flag_Skipdialog==true)// запишем новую строку в список
                        {
                            linkedListOF.Add(str2, 0); // создаем запись в списке с переводом
                            linkedListSF.SetTwin(linkedListOF.curr); //связываем ссылками исходную строку со строкой перевода в списках
                            linkedListOF.SetTwin(linkedListSF.curr);
                        }
                        else //Делаем замену перевода т.к. flag_ReplaceData == true
                        {
                            var old_data = linkedListOF.DataFrom((DoublyNode<string>)maxK_NodeOF);
                            // перевод не меняем т.к. новое значение - пустое , ИЛИ новая строка идентична старой
                            if (str2 == "" || str2== (string)old_data) { flag_ReplaceData = false; continue; }
                            // Если старое значение не пустое ИЛИ новая строка не пустая - спрашиваем пользователя о замене
                            if (((string)old_data != "") && (str2 != "" ))
                            {   
                                DialogResult result = MessageBox.Show(
                                "Do you really wants to replace string 1 with string 2 ?" +
                                "\n1:(" + ((string)old_data).Length + "): " + (string)old_data +
                                "\n2:(" + str2.Length + "): " + str2+
                                "\nIf you say \"No\"  - string \"2:\" will be lost!"+
                                "\n\nCancel will skipiing this dialog and adding all records as new.",
                                "Please attention !",
                                MessageBoxButtons.YesNoCancel,
                                MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button1,
                                MessageBoxOptions.DefaultDesktopOnly);
                                if (result == DialogResult.No) { flag_ReplaceData = false; continue; }// перевод не меняем
                                else if (result == DialogResult.Cancel) { flag_Skipdialog = true; }
                            }
                            linkedListOF.ReplaceData(str2, (DoublyNode<string>)maxK_NodeOF);//меняем перевод
                            flag_ReplaceData = false; // отработал - сбросили
                        }
                        continue;
                    }
                    if (bufcounter > MaxBytesMessage) { bufcounter = 0; continue; }// размер сообщения превышен - урезаем его

                    if (sourcePart == true) { buf1[bufcounter - 1] = b;  }
                    else {  buf2[bufcounter - 1] = b; }

                    // играем с прогрессбаром

                    if (i >= percent) // проверяем нужно ли двигать прогресс бар на 1%
                    {
                        if (progressBar1.Value < 100) { progressBar1.Value++; }
                        percent += onepercent; progressBar1_lb.Text = Convert.ToString(progressBar1.Value) + " %";
                    }
                }
                //bgWorker.CancelAsync();
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
                timer1.Enabled = false;
            }
        }

        private float FindSameString(string str1, DoublyLinkedList<string> linkedList, out object maxK_node)
        { //возвращает 1 если найдена такая же запись как в заданном списке, 0-если такой строки нет в списке, либо 
          // коэффициент Танимото указывающий степень схожести 
            float kTanimoto = 0, kTanimoto_tmp; // степень схожести строк [0..1]. 0-Несхожие. 1-идентичные

            maxK_node = null;
            foreach (var item in linkedList) // прямое сравнение строк на точное совпадение
                if (str1 == item)// фразы в linkedlistSF и доп.файле с переводом - совпали
                {
                    maxK_node = linkedList.Curr;
                    return 1; // строка есть в списке
                }

            // Расчет коэфф.Танимото схожести для всех строк списка и поиска максимального
            foreach (var item in linkedList)
            {
                // Найдем строку в списке linkedlist максимально схожую (по алгоритму Танимото) с входной строкой
                kTanimoto_tmp = Tanimoto(str1, item);
                if (kTanimoto_tmp > kTanimoto)
                { kTanimoto = kTanimoto_tmp; maxK_node = linkedList.Curr; } //ссылка на запись с максимальным коэфф. Танимото
            }
            return kTanimoto;
        }

        private float Tanimoto(string str1, string str2)
        { //Коэффициент Танимото – описывает степень схожести двух множеств. 
          // при использовании строк с русскими буквами, их лучше подавать сюда в Unicode
            float kTanimoto = 0;
            //str1.ToLower(); str2.ToLower(); //переводим обе строки в нижний регистр - в этой проге, точнее так не делать
            if (str1 == String.Empty || str2 == String.Empty) return 0; // одна из строк нулевой длинны - строки неравны

            string str1out = Regex.Replace(str1, @"\\r", String.Empty); //уберем символ \r
            string str2out = Regex.Replace(str2, @"\\r", String.Empty);
            str1out = Regex.Replace(str1out, @"\\n", String.Empty); // уберем символ \n
            str2out = Regex.Replace(str2out, @"\\n", String.Empty);
            str1out = Regex.Replace(str1out, @"[^A-Za-z0-9,.!?]+", String.Empty); // Оставим Англ.буквы цифры и знаки ,.?! 
            str2out = Regex.Replace(str2out, @"[^A-Za-z0-9,.!?]+", String.Empty); // Оставим Англ.буквы цифры и знаки ,.?! 
            if (str1out == str2out)// строки идентичны, но есть расхождение в знаках - нужно спросить у пользователя
                return kTanimoto = (float)0.85;
            if (str1out == String.Empty || str2out == String.Empty) return 0; //одна из строк стала нулевой длинны
            if (str1out.Length != str2out.Length) return 0; //вычищенные строки не совпадают по длинне
            //Длины строк совпадают, а содержимое нет. Сравним методом Танимото
            //if (str1out.Length == str2out.Length && str1out != str2out) return 0; 

            // найдем отличие между строками используя алгоритм Танимото
            int a = 0; // кол-во элементов в 1-ом множестве
            int b = 0; //кол-во элементов во 2-ом множестве
            int c = 0; //кол-во общих элементов  в двух множествах
            // Найдем множество - количество уникальных символов в каждой из строк
            Dictionary<char, int> dictionarys1 = str1out.GroupBy(x => x)
                .ToDictionary(x => x.Key, x => x.Count());
            a = dictionarys1.Count;
            Dictionary<char, int> dictionarys2 = str2out.GroupBy(x => x)
                .ToDictionary(x => x.Key, x => x.Count());
            b = dictionarys2.Count;

            // Найдем пересечение множеств
            var result = dictionarys1.Intersect(dictionarys2);
            c = result.Count();
            // Расчет коэффициента Танимото
            kTanimoto = (float)c / (a + b - (float)c);
            return kTanimoto; // Чем ближе к 1 , тем достовернее сходство. обычно 0.85 - уже вполне достоверно */
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // InterfaceLanguage={EN,RU};
            // TranslatedFile= {Full Path to last editing file}; полный путь
            // LastRecordNumber= {integer} -номер последней редактируемой записи с прошлой сессии
            // SourceLanguage={EN...} - указание для переводчика Google
            // TranslationLanguage ={RU...} - указание для переводчика Google
            // 
            // Загрузим первоначальные настройки программы. Ini файл
            string[] commands = { "InterfaceLanguage", "TranslatedFile", "LastRecordNumber", "SourceLanguage", "TranslationLanguage" };
            string[] IL = { "en","ru"}; // возможные языки интерфейса
            string[] SL = { "en","ru"}; string[] TL = { "ru","en"}; //направления перевода
            string IniFile = Environment.GetCommandLineArgs()[0]; //получаем имя запущенного файла
            string message="", command="", command_value="";
           // bool flag_Newfile = true;
            int cm_start=0; // command_value_start
            IniFile = IniFile.Substring(0, IniFile.Length - 3); IniFile += "ini";
            FileInfo src = new FileInfo(IniFile);
            if (src == null) return; // ini - файл отсутствует
            byte[] buf = new byte[1024 * 3]; //Буффер для чтения из файла строки текста
            long l = src.Length; //размер исходного файла в байтах
            if (l > 1024) return; //дефективный ini - игнорируем
            //ЧИТАЕМ ФАЙЛ В БУФЕР
            using (BinaryReader readerSF = new BinaryReader(File.Open(IniFile, FileMode.Open)))
            {  //for (long i = 0; i < l; i++) buf[i] = readerSF.ReadByte(); 
               while((message=readerSF.ReadString())!=null)
                {
                    for (int i = 0; i < message.Length; i++) 
                    {
                        if (message[i] == '=')
                        {
                            command = message.Substring(0, i - 1);
                            cm_start = i + 1;
                        }
                        else if(message[i] == 0xa && message[i-1]==0xd) //конец строки
                        {
                            command_value = message.Substring(cm_start,i-2-cm_start);
                            foreach(var item in commands)
                            {
                                if (item == command) //есть поддерживаемая команда INI !
                                {   
                                    FileInfo src1;
                                    switch (command)
                                    {   
                                        case "InterfaceLanguage":
                                            foreach (var item1 in IL)
                                                if(item1==command_value.ToLower())InterfaceLanguage=command_value;
                                            break;
                                        case "TranslatedFile":
                                            if((src1 = new FileInfo(command_value))!=null)TranslatedFile=command_value;
                                            break;
                                        case "LastRecordNumber":
                                            LastRecordNumber=Convert.ToInt64(command_value);
                                            break;
                                        case "SourceLanguage":
                                            foreach (var item1 in SL)
                                                if (item1 == command_value.ToLower()) languageEn = command_value;
                                            break;
                                        case "TranslationLanguage":
                                            foreach (var item1 in TL)
                                                if (item1 == command_value.ToLower()) languageRu = command_value;
                                            break;
                                    }
                                   break;
                                }


                            }
                            
                        }

                    }
                }
            }
            //Ищем названия параметров
         /*   message = Encoding.UTF8.GetString(buf);//получили файл как строку текста в UTF8 кодировке
            for (int i = 0; i < l; i++)
            {
                if (message[i] == '=')
                {
                    command = message.Substring(pointer1, i - pointer2);
                    switch (command)
                    {
                       // case "InterfaceLanguage": InterfaceLanguage=;
                       //     break;
                    }

                }
            }*/



        }
        private void About_tsmi_Click(object sender, EventArgs e)
        {
            string str1 = "This program may be useful for translating text" +
                          " in some binary or text files.\n" +
                          "You can search & catch strings of text in binary files using HEX-coded signatures.\n" +
                          "Supports Google translating service for limited translations.\n" +
                          "Program can operate with text files that consist of two parts:\n" +
                          "original sentence and translation sentense compared with symbol =";
            MessageBox.Show(str1, "About program ...", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Start_btn_Click(object sender, EventArgs e)
        {
            //if (Signature_tb.TextLength == 0) { MessageBox.Show("You must setup Signature to start."); return; }
            // начиная со смещения Offset начнем по байтам искать сигнатуру Signature.
            // Если найдем, то будем добавлять текст в список DoubleNode. Строим списки параллельно у обоих файлов.
            // создаем объект BinaryReader
            if (SourceFile == "" || SourceFile == null) return; // Binary файл еще не открыт
            Start_btn.Visible = false;
            using (BinaryWriter writer = new BinaryWriter(File.Open(OutputFile + ".txt", FileMode.OpenOrCreate)))
            {
                using (BinaryReader readerSF = new BinaryReader(File.Open(SourceFile, FileMode.Open)))
                { // откроем файл Source на чтение
                    FileInfo src = new FileInfo(SourceFile);
                    byte[] buf = new byte[MaxBytesMessage * 3]; //Буффер для чтения из файла строки текста
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

                                for (int j = 0; j < lentxt; j++)//читаем lentxt байт сообщения
                                {
                                    buf[j] = readerSF.ReadByte(); i++;
                                    if (j > 1 && buf[j] == 0xa0)//заменяем символ UTF-8 C2A0 на пробел .
                                        if (buf[j - 1] == 0xc2){ buf[j-1] = 0x20; j -= 1; lentxt -= 1; }

                                    if (j > 2 && buf[j] == 0xa6)//заменяем символ UTF-8 E280A6 на три точки 2E2E2E .
                                        if (buf[j - 1] == 0x80)
                                            if (buf[j - 2] == 0xe2) { buf[j] = 0x2e; buf[j - 1] = 0x2e; buf[j - 2] = 0x2e; }

                                    if (j >= 2 && buf[j] == 0x90)//заменяем символ UTF-8 E38090 на скобку 0x5b [
                                        if (buf[j - 1] == 0x80)
                                            if (buf[j - 2] == 0xe3) { buf[j - 2] = 0x5b; j -= 2; lentxt -= 2; }

                                    if (j >= 2 && buf[j] == 0x91)//заменяем символ UTF-8 E38091 на скобку 0x5b ]
                                        if (buf[j - 1] == 0x80)
                                            if (buf[j - 2] == 0xe3) { buf[j - 2] = 0x5d; j -= 2; lentxt -= 2; }

                                    if (j > 2 && buf[j] == 0x9f)//заменяем символ UTF-8 EFBC9F на вопрос  0x3f ?
                                         if (buf[j - 1] == 0xbc)
                                             if (buf[j - 2] == 0xef) { buf[j - 2] = 0x5d; j -= 2; lentxt -= 2; }

                                    if (j > 3 && buf[j] == 0x3e)//3c, 62||42, 52||72, 3E // <br> или <BR> заменяем на \n  0x5c,0x6e
                                        if (buf[j - 1] == 0x72 || buf[j - 1] == 0x52)
                                            if (buf[j - 2] == 0x62 || buf[j - 2] == 0x42)
                                                if (buf[j - 3] == 0x3c) { buf[j - 3] = 0x5c; buf[j - 2] = 0x6e; lentxt -= 2; j -= 2; }

                                    if (buf[j] == 0x3d) // символ "=" заменяем на 3 символа "%3D" = 0x25,0x33,0x44
                                    { buf[j] = 0x25; buf[j + 1] = 0x33; buf[j + 2] = 0x44; lentxt += 2; j += 2; }
                                    // 2e 3d 0d e3 80 90  на space+[ ; e3 80 91 3d 0d
                                }
                                string message;  // преобразованиЕ массива byte в строку string
                                byte[] tmp_bytes = new byte[lentxt];
                                for (int j = 0; j < lentxt; j++) tmp_bytes[j] = buf[j];
                                message = Encoding.UTF8.GetString(tmp_bytes);

                                if (message == "..." || message == "") continue; // пустые строки не переводим
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
                            percent += onepercent; progressBar1_lb.Text = Convert.ToString(progressBar1.Value) + " %";
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
                //OpenTranslatedFile(); отдельно запускаем через меню
            }
        }

        private void Next_btn_Click(object sender, EventArgs e)
        {
            nudRecord.ReadOnly = true;
            if (linkedListSF.Count == 0) return; // список пуст перемещение вперед невозможно
            if (nudRecord.Value == linkedListSF.Count) { nudRecord.Value = 1; } else { nudRecord.Value++; }
            //проверим, если TextBox изменился - сохраним его
            //linkedListOF.SetCurrent(linkedListSF.Twin);
            if ((string)linkedListOF.CurrentData != Translated_tb.Text)
                if (linkedListSF.Twin == linkedListOF.curr) { linkedListOF.ReplaceData(Translated_tb.Text); NotSavedYet = true; }

            byte[] bytes = Encoding.Default.GetBytes((string)linkedListSF.NextNode());
            Source_tb.Text = Encoding.UTF8.GetString(bytes);
            bytes = Encoding.Default.GetBytes((string)linkedListOF.NextNode());
            Translated_tb.Text = Encoding.UTF8.GetString(bytes);
            // выводим сообщения о количестве символов в записях исходника и перевода
            lbTranslated.Text = "Translated Message: " + Convert.ToString(Translated_tb.Text.Length) + " symbols";
            lbSource.Text = "Source Message: " + Convert.ToString(Source_tb.Text.Length) + " symbols";
            nudRecord.ReadOnly = false;
        }
        private void Prev_btn_Click(object sender, EventArgs e)
        {
            if (linkedListSF.Count == 0) return; // список пуст перемещение назад невозможно
            nudRecord.ReadOnly = true;
            if (nudRecord.Value == 1) { nudRecord.Value = linkedListSF.Count; } else { nudRecord.Value--; }
            //проверим, если TextBox изменился - сохраним его
            //linkedListOF.SetCurrent(linkedListSF.Twin);
            if ((string)linkedListOF.CurrentData != Translated_tb.Text)
                if (linkedListSF.Twin == linkedListOF.curr) { linkedListOF.ReplaceData(Translated_tb.Text); NotSavedYet = true; }

            byte[] bytes = Encoding.Default.GetBytes((string)linkedListSF.PrevNode());
            Source_tb.Text = Encoding.UTF8.GetString(bytes);
            bytes = Encoding.Default.GetBytes((string)linkedListOF.PrevNode());
            Translated_tb.Text = Encoding.UTF8.GetString(bytes);
            // выводим сообщения о количестве символов в записях исходника и перевода
            lbTranslated.Text = "Translated Message: " + Convert.ToString(Translated_tb.Text.Length) + " symbols";
            lbSource.Text = "Source Message: " + Convert.ToString(Source_tb.Text.Length) + " symbols";
            nudRecord.ReadOnly = false;
        }
        private void nudRecord_ValueChanged(object sender, EventArgs e)
        {
            if (nudRecord.ReadOnly == true) return; // это пришел афтершок из функций Next_btn_Click И Prev_btn_click
            long counter = (long)nudRecord.Value;
            //проверим, если TextBox изменился - сохраним его
            if ((string)linkedListOF.CurrentData != Translated_tb.Text)
                if (linkedListSF.Twin == linkedListOF.curr) { linkedListOF.ReplaceData(Translated_tb.Text); NotSavedYet = true; }
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
                    // выводим сообщения о количестве символов в записях исходника и перевода
                    lbTranslated.Text = "Translated Message: " + Convert.ToString(Translated_tb.Text.Length) + " symbols";
                    lbSource.Text = "Source Message: " + Convert.ToString(Source_tb.Text.Length) + " symbols";
                    break;
                }
            }
        }
        private void SearchSource_Click(object sender, EventArgs e)
        {   // поиск по тексту из входящего файла
            string str = SearchSource_tb.Text; 
            if (SearchSource_tb.Text.Length == 0) return; // не задана строка поиска
            
            if (linkedListSS.Count != 0) linkedListSS.Clear(); //очищаем список если был ранее создан
            //Начинаем поиск подстроки по всем элементам списка linkListSF
            foreach (var item in linkedListSF)
            {
                if (item.Contains(str))
                {
                    // Вхождения найдены
                    linkedListSS.Add(item, 0);
                }
            }
            if (linkedListSS.Count == 0) return;
            // а иначе нужно открыть новую вкладку, и переключить обзор функций Next Prev на новый список
            // и вывести из него первый элемент.
            //добавление вкладки
            TabPage newTabPage = new TabPage();
            int len = str.Length < 50 ? str.Length : 50;
            newTabPage.Text = str.Substring(0,len);
            Source_tc.TabPages.Add(newTabPage);
            Source_tc.SelectedTab=newTabPage;

            this.Source_tc.SelectedTab.Controls.Add(this.Source_tb);
            this.Source_tc.SelectedTab.Controls.Add(this.Translated_tb);

        }

        private void SearchTranslated_btn_Click(object sender, EventArgs e)
        {
            //создаем новый TAB
            //на нем открываем новый TEXTBOX
            // Проходимся по всему списку и ищем подстроку
        }

        private void CloseFilesClear_Click(object sender, EventArgs e)
        {
            Quit_tsmi_Click(null,null); //сохраним работу в файл
            nudRecord.ReadOnly = true; 
            nudRecord.Value = 1; 
            Records_lb.Text = "Found: 0 records";
            
            TranslatedFile_tb.Text = ""; TranslatedFile = "";
            SourceFile_lb.Text = ""; SourceFile = "";

            Source_tb.Text = ""; Source_tb.ReadOnly = true;
            Translated_tb.Text = ""; Translated_tb.ReadOnly = true;
            lbSource.Text = "Source Message: 0 symbols"; 
            lbTranslated.Text = "Translated Message: 0 symbols";
            Offset_tb.Text = ""; Offset_tb.ReadOnly = true;
            Signature_tb.Text = "";Signature_tb.ReadOnly = true;
            SearchSource_tb.Text = ""; SearchTranslated_tb.Text = "";
            SearchSource_tb.ReadOnly = true; SearchTranslated_tb.ReadOnly = true;
            NotSavedYet = false;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Translate_btn_Click(object sender, EventArgs e)
        {
            if (Source_tb.Text != string.Empty) Translated_tb.Text += "\n"+Translate_Google(Source_tb.Text);
            // выводим сообщения о количестве символов в переводe
            lbTranslated.Text = "Translated Message: " + Convert.ToString(Translated_tb.Text.Length) + " symbols";
        }
        private string Translate_Google(string source)
        { 
            string mathod = "GET";
            string userAgent = "Mozilla/5.0 (Windows NT 10.0; rv:91.0) Gecko/20100101 Firefox/91.0";
            string urlFormat = "https://translate.googleapis.com/translate_a/single?client=gtx&sl={0}&tl={1}&dt=t&q={2}";
            //string languageEn = "en";// обьявлены глобально в классе F0rm1
            //string languageRu = "ru";
            string text = source;
            string url = string.Format(urlFormat, languageEn, languageRu, Uri.EscapeUriString(text));
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = mathod;
            request.UserAgent = userAgent;
            string response = new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd();      
            // Parse json data
            return Parse_Google_JSON(response);
        }
        private string Parse_Google_JSON(string str)
        {
            int openbr = 0; //счетчик скобок []
            int openbrTR = 0; // номер скобы перед переводом
            int openbrTR1st = 0; //количество открытых скоб перед первой строкой
            int startST=0; //позиция начала подстроки с текстом перевода
            string result = string.Empty; // строка с переводом :)
            bool flag_sent = false; //флаг пропуска всех символов кроме [ ]
            bool flag_begin = false;//Флаг того , что перевод при разборе еще не встречался
            int len = str.Length; //длина сообщения от GOOGLE
            for(int i = 0; i < len; i++)
            {
                if (str[i] == '[') { openbr++; continue; }
                if (str[i] == ']') 
                {   
                    openbr--;
                    if (openbr < openbrTR){ flag_sent = false; }
                    if (openbr < openbrTR1st) flag_sent =true;
                    continue; 
                }
                if (flag_sent == true) continue; 
                if (str[i] == '\"' && str[i - 1] == '[') 
                {   
                    if (flag_begin == false) { flag_begin = true; openbrTR1st = openbr-1; }
                    startST = i + 1; openbrTR = openbr; 
                    continue; 
                }
                if (str[i] == '\"' && str[i + 1] == ',' && ( i + 1 ) < len) 
                { 
                    result += str.Substring(startST,i-1-startST);
                    //result += "\n";
                    flag_sent = true;
                }
            }
            return ("\n"+result);
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
        public object CurrentData { get { return curr.Data; } }
        public object Curr { get { return curr; } }
        public object DataFrom (DoublyNode<T> Node)
        {
            if (Node != null) return Node.Data; else return null;
        }
        public object TwinFrom(DoublyNode<T> Node)
        {
            if (Node != null) return Node.Twin; else return null;
        }

        public void ReplaceData(T data, DoublyNode<T> directNode=null) // Заменяет поле даты текущего элемента, либо иного
        {                                           // элемента, ссылка на который указана в необязательном поле directNode
            if (directNode != null) { directNode.Data = data; return; }
            if (curr == null) return;
            curr.Data = data; 
        }
        public void SetTwin(DoublyNode<T> twin)
        {
            if (curr == null) return;
            curr.Twin = twin;
        }
        public void SetCurrent(object current)//DoublyNode<T>
        {
            if (curr == null) return;
            curr =(DoublyNode<T>)current;
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

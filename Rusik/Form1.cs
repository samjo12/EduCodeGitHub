﻿using System;
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
        public long CurrentnudRecord; // переменная для сохранения номера текущей записи списка при запуске поиска
        public int numSearchTabS = 0; // кол-во открытых вкладок с поиском по Source message
        public int numSearchTabT = 0; // кол-во открытых вкладок с поиском по Translated message
        public int currentTabS = 0; // номер текущей вкладки в окне с Source
        public int currentTabT = 0; // номер текущей вкладки в окне с Translated
        public TabPage ActiveTab = null;
        public long SourceNodeCounter = 0; // счетчик-указатель на текущую запись списка
        public string SourceFile=""; // бинарный файл
        public string TranslatedFile=""; //Текстовый файл частично переведенный ранее со знаком разделителем "="
        public string OutputFile=""; // Выходной текстовый файл с текущим рабочим переводом
        string IniFile = ""; //полный путь к INI файлу
        public DoublyLinkedList<string> linkedListSF = new(); // связный список для исходного файла
        public DoublyLinkedList<string> linkedListOF = new(); // связный список для выходного файла

        //public DoublyLinkedList<string> linkedListHS = new(); // связный список c историей открытия файлов
        public bool flag_NotSavedYet = false;
        public bool flag_Skipdialog = false; //флаг пропуска пользовательских диалоговых окон
        //флаг наличие доп.данных о смещениях/позициях текстовых строк внутри бинарного файла :
        public bool flag_ExtraDataforBinary = false; 
        public byte[] Signature = { 0x04, 0x00, 0x06, 0x00 };  // Сигнатура из байт

        public string languageEn = "en"; //из модуля google-переводчика
        public string languageRu = "ru";
        public string InterfaceLanguage = "en"; //английский язык по-умолчанию
        public long LastRecordNumber=1; // это сохраненный в ini файле параметр nudRecord последнего открытого файла
        public Dictionary<string, string> GoogleLangs = new Dictionary<string, string>(){
{ "Afrikaans","af"},{ "Albanian","sq"},{ "Arabic","ar"},{ "Armenian","hy"},{ "Azerbaijani","az"},{ "Basque","eu"},{ "Belarusian","be"},
{ "Bulgarian","bg"},{ "Catalan","ca"},{ "Chinese(Simplified)","zh-CN"},{ "Chinese(Traditional)","zh-TW"},{ "Croatian","hr"},
{ "Czech","cs"},{ "Danish","da"},{ "Dutch","nl"},{ "English","en"},{ "Estonian","et"},{ "Filipino","tl"},{ "Finnish","fi"},
{ "French","fr"},{ "Galician","gl"},{ "Georgian","ka"},{ "German","de"},{ "Greek","el"},{ "Haitian","ht"},{ "Hebrew","iw"},
{ "Hindi","hi"},{ "Hungarian","hu"},{ "Icelandic","is"},{ "Indonesian","id"},{ "Irish", "ga"},{ "Italian","it"},{ "Japanese","ja"},
{ "Korean","ko"},{ "Latvian","lv"},{ "Lithuanian","lt"},{ "Macedonian","mk"},{ "Malay","ms"},{ "Maltese","mt"},{ "Norwegian","no"},
{ "Persian","fa"},{ "Polish","pl"},{ "Portuguese","pt"},{ "Romanian","ro"},{ "Russian","ru"},{ "Serbian","sr"},{ "Slovak","sk"},
{ "Slovenian","sl"},{ "Spanish","es"},{ "Swahili","sw"},{ "Swedish","sv"},{ "Thai","th"},{ "Turkish","tr"},{ "Ukrainian","uk"},
{ "Urdu","ur"},{ "Vietnamese","vi"},{ "Welsh","cy"},{ "Yiddish","yi"} };
        // объявление начальной вкладки
        public TabControl Tabs;
        public TabPage Home;//=new();
        //public SplitContainer splitContainer1 =new();
        public Form1()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(this.Form1_FormClosing);// обработчик закрытия окна по крестику
            // подключаем коллекцию со списком языков 
            comboBox1.DataSource = new BindingSource(GoogleLangs, null);
            comboBox2.DataSource = new BindingSource(GoogleLangs, null);
            comboBox1.DisplayMember = "Key"; comboBox2.DisplayMember = "Key";
            comboBox1.ValueMember = "Value"; comboBox2.ValueMember = "Value";
            /*string usage_message = "This program can help you with some unofficial " +
            "localization of other programs or games.";
            TextBox mess_tb = new();
            mess_tb.Location = new Point(0, 25);
            mess_tb.Name = "UsageMessage";
            mess_tb.Size = new Size(977, 500);
            mess_tb.Text = usage_message;
            mess_tb.Multiline = true;
            mess_tb.ReadOnly = true;
            Controls.Add(mess_tb);
            */
            Load_INI(); // читаем ini- файл
                        // формируем первую вкладку
                        
        }

        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Quit_tsmi_Click(sender, e);
        }

        private void Quit_tsmi_Click(object sender, EventArgs e) // МЕНЮ Quit
        {
            // если исходный файл открыт, то предлагаем сохранить выходной файл
            if (linkedListOF.Curr != null) // если в памяти есть список - то предлагаем сохраниться
            {
                if (flag_NotSavedYet == true)
                    do
                    {
                        DialogResult result = MessageBox.Show(
                        "Do yo want to Save file?\n"+TranslatedFile,
                        "Attention",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly);
                        if (result == DialogResult.No){ Save_INI(); return; }// пользователь отказался от сохранения
                    } while (SaveFile() == false); // согласился сохраниться , но что-то пошло не так. Даем еще одну попытку...
                linkedListOF.Clear();
                linkedListSF.Clear();
            }
            Save_INI();
        }

        private bool SaveFile(string outfile="") // сохраняем список из памяти в файл с разделителем =
        {
            String tmpOutputFile;
            if (outfile == "") tmpOutputFile = TranslatedFile;
            else tmpOutputFile = outfile;
            long onepercent = linkedListSF.Count / 100 - 1, percent = onepercent;
            progressBar1.Value = 0;
            progressBar1_lb.Text = "%";
            using (BinaryWriter writer = new BinaryWriter(File.Open(tmpOutputFile, FileMode.OpenOrCreate)))
            {
                long counter = 0;
                // записываем в файл содержимое списков с данными
                var tmp_curr = linkedListSF.curr;
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
                        { progressBar1.Value++; progressBar1_lb.Text = Convert.ToString(progressBar1.Value)+"%"; }
                        percent += onepercent; 
                    }
                }
                linkedListSF.curr=tmp_curr; //восстановим curr
            }
            Save_INI();
            return true;
        }
        private void SaveFile_tsmi_Click(object sender, EventArgs e) // Меню Save File As
        {
            SaveFileDialog saveFileDialog1 = new();
            saveFileDialog1.OverwritePrompt = true; //предупреждение о перезаписи
            saveFileDialog1.ShowDialog();
            string tmpOutputFile = saveFileDialog1.FileName;
            if (tmpOutputFile == null || tmpOutputFile == "") return;
            else SaveFile(tmpOutputFile);  
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
            Search_tstb.ReadOnly = false;
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
                long l = src.Length; //размер исходного файла в байтах
                byte[] buf1 = new byte[MaxBytesMessage * 3]; //Буффер для чтения из файла строки текста до знака =
                byte[] buf2 = new byte[MaxBytesMessage * 3]; //Буффер для чтения из файла строки текста после знака =
                string str1 = "", str2 = "";
                long onepercent = l / 100 - 1, percent = onepercent;
                byte b;
                bool sourcePart = true; // флаг 
                bool flag_ReplaceData = false; // флаг
                //bool flag_Skipdialog = false; //флаг пропуска пользовательских диалоговых окон перенес в заголовок класса
                int bufcounter = 0;
                object maxK_Node = null; // ссылка на потенциально дублирующуюся строку из списка
                object maxK_NodeOF = null; //ссылка на перевод заменяемой строки

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
                        { linkedListSF.Add(str1, 0);// создаем новый элемент списка
                        }
                        sourcePart = false;
                        bufcounter = 0;
                        continue;
                    }
                    // Если встретили символы в оригинальной части фразы, то просто пропускаем
                    // если "0d 0a" втретили в переводе, то это конец строки и будем ожидать новой фразы перeвода
                    if(bufcounter>=2)
                    if ((sourcePart == false && b == 0xa && buf2[bufcounter - 2] == 0xd) || (i == (l - 1)))
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
                        percent += onepercent; progressBar1_lb.Text = Convert.ToString(progressBar1.Value) + "%";
                    }
                }
                nudRecord.Maximum = linkedListSF.Count;
                nudRecord.Minimum = 1;
                if (linkedListSF.Count > 1)
                {
                    Records_lb.Text = "Found " + linkedListSF.Count + " records.";
                    //Выведем в SourceFile_tb первый элемент списка
                    //foreach (var item in linkedListSF) { Source_tb.Text = item; break; }
                    //foreach (var item in linkedListOF) { Translated_tb.Text = item; break; }
                    nudRecord.Value = 1;
                    nudRecord.ReadOnly = false;
                    //lbSource.Text = Convert.ToString(Source_tb.Text.Length); // указываем кол-во символов в исходном сообщении
                    NewTab_Click(null, null); //пытаемся открыть основную вкладку HOME
                    Translated_tb_KeyUp(null, null); //обновляем число символов в переводе
                    // разблокируем строки поиска
                    Search_tstb.ReadOnly = false;
                    TranslatedFile_tb.Text = TranslatedFile;
                }
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
        private void Load_INI()
        {
            // InterfaceLanguage={EN,RU};
            // TranslatedFile= {Full Path to last editing file}; полный путь
            // LastRecordNumber= {integer} -номер последней редактируемой записи с прошлой сессии
            // SourceLanguage={EN...} - указание для переводчика Google
            // TranslationLanguage ={RU...} - указание для переводчика Google
            // OpenFileHistory={полный путь к файлу,LastNumber}
            // Загрузим первоначальные настройки программы. Ini файл
            string[] commands = { "InterfaceLanguage", "TranslatedFile", "LastRecordNumber", "SourceLanguage", "TranslationLanguage", "OpenFileHistory" };
            string[] IL = { "en", "ru" }; // возможные языки интерфейса
            IniFile = Environment.GetCommandLineArgs()[0]; //получаем имя запущенного файла
            string message = "", command = "", command_value = "";
            IniFile = IniFile[0..^3]; IniFile += "ini";

            if (!File.Exists(IniFile)) return; // ini - файл отсутствует
            //ЧИТАЕМ ФАЙЛ построчно
            using (StreamReader readerSF = new(File.Open(IniFile, FileMode.Open)))
            {
                while ((message = readerSF.ReadLine()) != null)
                {
                    for (int i = 0; i < message.Length; i++)
                    {
                        if (message[i] == '=')
                        {
                            command = message.Substring(0, i);
                            //Убираем все пробелы
                            command = Regex.Replace(command, @"\s", ""); //удаляем пробелы
                            command = Regex.Replace(command, @"\t", ""); // удаляем табуляцию

                            command_value = message.Substring(i + 1, message.Length - i - 1);
                            command_value = Regex.Replace(command_value, @"\t", "");// удаляем табуляцию
                            foreach (var item in commands) //проверяем, команда ли это?
                            {
                                if (item == command) //есть поддерживаемая команда INI !
                                {
                                    switch (command)
                                    {
                                        case "InterfaceLanguage":
                                            command_value = Regex.Replace(command_value, @"\s", "");
                                            command_value = command_value.ToLower();
                                            foreach (var item1 in IL)
                                                if (item1 == command_value) InterfaceLanguage = command_value;
                                            break;
                                        case "TranslatedFile":
                                            if (command_value != "" && command_value != null)
                                                if (File.Exists(command_value))
                                                { TranslatedFile = command_value; }
                                                else LastRecordNumber = 1;
                                            break;
                                        case "LastRecordNumber":
                                            command_value = Regex.Replace(command_value, @"\s", "");
                                            LastRecordNumber = Convert.ToInt64(command_value);
                                            break;
                                        case "SourceLanguage":
                                            command_value = Regex.Replace(command_value, @"\s", "");
                                            command_value = command_value.ToLower();
                                            foreach (var item1 in GoogleLangs.Values)
                                                if (item1 == command_value) languageEn = command_value;
                                            comboBox1.SelectedValue = languageEn;
                                            foreach (var item1 in GoogleLangs.Keys)
                                                if (GoogleLangs[item1] == languageEn)
                                                { comboBox1.Text = GoogleLangs[item1]; break; }
                                            break;
                                        case "TranslationLanguage":
                                            command_value = Regex.Replace(command_value, @"\s", "");
                                            command_value = command_value.ToLower();
                                            foreach (var item1 in GoogleLangs.Values)
                                                if (item1 == command_value) languageRu = command_value;
                                            comboBox2.SelectedValue = languageRu;
                                            foreach (var item1 in GoogleLangs.Keys)
                                                if (GoogleLangs[item1] == languageRu)
                                                { comboBox2.Text = GoogleLangs[item1]; break; }
                                            break;
                                            /*   case "OpenFileHistory": // делим строку на параметры по запятой
                                                   var result = new Regex(@"^.").Matches(command_value);
                                                   foreach (Match item1 in result)
                                                   {
                                                       //Console.WriteLine(item1);
                                                   }
                                                   break;*/
                                    }
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }//читаем следующую строку
            }//закрываем файл
             //Ищем названия параметров
             //   message = Encoding.UTF8.GetString(buf);//получили файл как строку текста в UTF8 кодировке
            if (TranslatedFile == "" || TranslatedFile == null) LastRecordNumber = 1;
            if (TranslatedFile != "" && TranslatedFile != null) { flag_Skipdialog = true; OpenTranslatedFile(); }
            if (LastRecordNumber != 1)
            {
                nudRecord.Value = LastRecordNumber;
                nudRecord_ValueChanged(null, null);
            }
            if (comboBox1.Text == null) comboBox1.Text = "en";
            if (comboBox1.Text == null) comboBox1.Text = "ru";
            // проверим наличие файла с экстра данными
            IniFile = IniFile[0..^3]; IniFile += "ext";

            if (!File.Exists(IniFile)) return; // extra - файл отсутствует
            //ЧИТАЕМ ФАЙЛ Hash,offset,len
        /*    using (StreamReader readerSF = new(File.Open(IniFile, FileMode.Open)))
            {
                while ((message = readerSF.ReadLine()) != null)
                {
                    for (int i = 0; i < message.Length; i++)
                    {
                    }
                }
            }*/
        
        }
        public static string CreateMD5(string input) //расчет Хэша по строке
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes); // .NET 5 +

                // Convert the byte array to hexadecimal string prior to .NET 5
                // StringBuilder sb = new System.Text.StringBuilder();
                // for (int i = 0; i < hashBytes.Length; i++)
                // {
                //     sb.Append(hashBytes[i].ToString("X2"));
                // }
                // return sb.ToString();
            }
        }
        private void Save_INI()
        {
            /*string[] commands = { "InterfaceLanguage", "TranslatedFile", "LastRecordNumber", "SourceLanguage", "TranslationLanguage", "OpenFileHistory" };
            string[] IL = { "en", "ru" }; // возможные языки интерфейса
            string[] SL = { "en", "ru" }; string[] TL = { "ru", "en" }; //направления перевода
            string message = "", command = "", command_value = "";*/
            //записываем файл Ini заново, поверх старого
            using (StreamWriter writer = new StreamWriter(File.Open(IniFile, FileMode.Create)))
            {
                writer.WriteLine("[Last session Section]");
                writer.WriteLine("InterfaceLanguage="+ InterfaceLanguage);
                writer.WriteLine("SourceLanguage=" + languageEn);
                writer.WriteLine("TranslationLanguage=" + languageRu);
                writer.WriteLine("LastRecordNumber=" + Convert.ToString(nudRecord.Value));
                writer.WriteLine("TranslatedFile=" + TranslatedFile);
                writer.WriteLine("[History Section - don't change anything below this line !]");
                //далее нужно сохранять ранее считанный список истории linkedListHS
         /*       if (linkedListHS.curr != null) // список есть, выгружаем
                {
                    foreach (var item in linkedListHS)
                    {
                        if (item == null) break;
                        writer.WriteLine(item);
                    }
                }*/
            }
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
           
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
                            percent += onepercent; progressBar1_lb.Text = Convert.ToString(progressBar1.Value) + "%";
                        }
                    }
                    nudRecord.Maximum = linkedListSF.Count;
                    nudRecord.Minimum = 1;

                    Records_lb.Text = "Found " + linkedListSF.Count + " records.";
                    //Translated_tb.ReadOnly = false;
                    //Выведем в SourceFile_tb первый элемент списка
                    //foreach (var item in linkedListSF) { Source_tb.Text = item; break; }
                    //foreach (var item in linkedListOF) { Translated_tb.Text = item; break; }
                    nudRecord.Value = 1; 
                    nudRecord.ReadOnly = false; 
                }
                //OpenTranslatedFile(); отдельно запускаем через меню
                NewTab_Click(null, null); //пытаемся открыть основную вкладку HOME
            }
        }


        private void Next_btn_Click(object sender, EventArgs e)
        {
            if (Tabs == null) return;
            if (linkedListSF.Count == 0) return; // список пуст перемещение вперед невозможно
           // if (linkedListOF.curr == null  || linkedListSF.curr == null) return;
            
            SplitContainer sc = (SplitContainer)Tabs.SelectedTab.Tag;
            SearchTabs tabSearch = (SearchTabs)sc.Tag;

            tabSearch.Next(); nudRecord.Value = tabSearch.linkedListSS.curr.Twin.N_Record;
        }
        private void Prev_btn_Click(object sender, EventArgs e)
        {
            if (Tabs == null) return;
            if (linkedListSF.Count == 0) return; // список пуст перемещение назад невозможно
            //if (linkedListOF.curr == null || linkedListSF.curr == null) return;

            SplitContainer sc = (SplitContainer)Tabs.SelectedTab.Tag;
            SearchTabs tabSearch = (SearchTabs)sc.Tag;

            tabSearch.Prev(); nudRecord.Value = tabSearch.linkedListSS.curr.Twin.N_Record; //вкладки с поиском
        }
        private void nudRecord_ValueChanged(object sender, EventArgs e)
        {
            if (nudRecord.ReadOnly == true) return; // это пришел афтершок из функций Next_btn_Click И Prev_btn_click
            if (Tabs == null) return;
            SplitContainer sc = (SplitContainer)Tabs.SelectedTab.Tag;
            SearchTabs tabSearch = (SearchTabs)sc.Tag;

            if(nudRecord.Value!=tabSearch.linkedListSS.curr.Twin.N_Record) //Если nudRecord не совпал с номером
                tabSearch.toDefined((long)nudRecord.Value);//элемента списка, то двигаем curr на новый nudRecord

        }

        public void Refresh_Search_ts(TabPage ts)
        {
            if (ts == Home) 
            { //заблокируем лишние кнопки на панели
                SourceFirst_tsb.Visible = false;
                SourcePrev_tsb.Visible = false;
                SourceNext_tsb.Visible = false;
                SourceLast_tsb.Visible = false;
                TabClose_tsb.Visible = false;
                SearchStat_tslb.Visible = false;
                nudRecord.ReadOnly = false;
                nudRecord.Increment = 1;
                nudRecord.BackColor = Color.White;
            }
            else 
            { //разблокируем кнопки поиска на панели
                SourceFirst_tsb.Visible = true;
                SourcePrev_tsb.Visible = true;
                SourceNext_tsb.Visible = true;
                SourceLast_tsb.Visible = true;
                TabClose_tsb.Visible = true;
                SearchStat_tslb.Visible = true;
                nudRecord.ReadOnly = true;
                nudRecord.Increment = 0;
                nudRecord.BackColor = Color.LightGray;
            }
        }
 
        private void NewTab_Click(object sender, EventArgs e)
        {   // поиск по тексту из входящего файла
            /*        
            public long CurrentnudRecord; // переменная для сохранения номера текущей записи списка при запуске поиска
            public int numSearchTabS = 0; // кол-во открытых вкладок с поиском по Source message
            public int numSearchTabT = 0; // кол-во открытых вкладок с поиском по Translated message
            public int currentTabS = 0; // номер текущей вкладки в окне с Source
            public int currentTabT = 0; // номер текущей вкладки в окне с Translated*/
            string str = Search_tstb.Text; //строка поиска
            SearchTabs NewTab = new();
            TabPage newTabPage=new();
            

            if (Tabs == null) // делаем главную вкладку HOME
            {
                if (linkedListSF.Count() == 0) return; // нет смысла открывать вкладку, список пуст
                NewTab.SetlinkedListHome(linkedListSF);
                Tabs = new();
                Tabs.Name = "Tabs";
                Tabs.Size = new Size(985, 550);
                Tabs.Location = new Point(12, 35);
                //Tabs.ItemSize = new Size(61, 20);
                Tabs.SelectedIndex = 0;
                Tabs.TabIndex = 40;
                this.Controls.Add(Tabs);
                Tabs.Selecting += new TabControlCancelEventHandler(Tabs_Selecting);
                
                Home = newTabPage; 
                
                Home.Name = "Home";
                Home.Text = "Home";
                /*Home.Size = new Size(900, 498);
                Home.TabIndex = 0;*/
                Tabs.TabPages.Add(Home); //добавим новую вкладку Home
                Tabs.SelectedTab = Home;
                Home.Controls.Add(Search_ts);
                Search_ts.Dock = DockStyle.Top;
                //разблокируем элементы интерфейса
                Search_ts.Visible =true;
                statusStrip1.Visible = true;
                statusStrip2.Visible = true;

            }
            else// открывается точно не главная вкладка
            {   
                if (Search_tstb.Text.Length == 0) { NewTab.Clear(); newTabPage.Dispose(); return; } //пустая строка поиска 
                NewTab.SetlinkedListSF(linkedListSF, str); //ищем строку str в списке SF
                if (NewTab.Count() == 0) { NewTab.Clear(); newTabPage.Dispose(); return; } // поиск ничего не дал }
                
                int len = str.Length < 50 ? str.Length : 50;
                newTabPage.Text = str.Substring(0, len);
                Tabs.TabPages.Add(newTabPage); //добавим новую вкладку Home
                Tabs.SelectedTab = newTabPage; //переключимся на новую вкладку
            }
            
            currentTabS++; 


            Font font = new Font("Segoe UI", 14.03f);//, FontStyle.Bold | FontStyle.Italic | FontStyle.Underline);
            TextBox newSource_tb = new();
            newSource_tb.Location = new Point(0, 58);
            newSource_tb.Width = 484; newSource_tb.Height = 475;
            newSource_tb.Font = font;
            newSource_tb.BackColor = SystemColors.GradientInactiveCaption;
            newSource_tb.Name = "newSource_tb" + Convert.ToString(currentTabS);
            newSource_tb.Multiline = true;
            newSource_tb.ScrollBars = ScrollBars.Vertical;
            newSource_tb.KeyDown += Form1_KeyDown; // подключим горячие клавиши

            TextBox newTranslated_tb = new();
            newTranslated_tb.Location = new Point(500, 58);
            newTranslated_tb.Width = 488;  newTranslated_tb.Height = 475;
            newTranslated_tb.Font = font;
            newTranslated_tb.BackColor = SystemColors.InactiveBorder;
            newTranslated_tb.Name = "newTranslated_tb" + Convert.ToString(currentTabS);
            newTranslated_tb.Multiline = true;
            newTranslated_tb.ScrollBars = ScrollBars.Vertical;
            newTranslated_tb.KeyUp += Translated_tb_KeyUp;// ставим контрол на нажатие клавиш для отслеживания счетчика введенных символов
            //newTranslated_tb.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            newTranslated_tb.KeyDown += Form1_KeyDown; // подключим горячие клавиши
            NewTab.TabPage = newTabPage; // сохраним адрес Таба в экземпляре класса
            NewTab.tabSource_tb = newSource_tb;
            NewTab.tabTranslated_tb = newTranslated_tb;
            NewTab.tabSource_lb = lbSource;

            SplitContainer newSplitContainer = new();
            newSplitContainer.Location = new Point(0, 25);
            newSplitContainer.Name = "splitContainer"+ Convert.ToString(currentTabS); 
            newSplitContainer.Size = new Size(977, 500);
            newSplitContainer.SplitterDistance = 484;
            newSplitContainer.Orientation = Orientation.Vertical;
//newSource_tb.Anchor = AnchorStyles.Left | AnchorStyles.Top;
//newTranslated_tb.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            NewTab.splitContainer = newSplitContainer;
            newSplitContainer.Panel1.Controls.Add(newSource_tb);
            newSplitContainer.Panel2.Controls.Add(newTranslated_tb);
            
            statusStrip2.Dock = DockStyle.Bottom; newSplitContainer.Panel1.Controls.Add(statusStrip2);
            //statusStrip2.Anchor = AnchorStyles.Bottom;
            
            statusStrip1.Dock = DockStyle.Bottom; newSplitContainer.Panel2.Controls.Add(statusStrip1);
            //statusStrip1.Anchor = AnchorStyles.Bottom;
            Tabs.SelectedTab.Controls.Add(Search_ts);
            newSource_tb.Dock = DockStyle.Top;
            newTranslated_tb.Dock = DockStyle.Top;

            Tabs.SelectedTab.Tag = newSplitContainer;
            newSplitContainer.Tag=(object)NewTab;  //сохраним класс поиска в закладку

            newTabPage.Controls.Add(newSplitContainer);
            newTabPage.Controls.Add(Search_ts);

            //newTranslated_tb.BringToFront();
            NewTab.tabTranslated_lb = lbTranslated;
            //обновляем визуальную информацию
            SearchStat_tslb.Text = "1 of " + Convert.ToString(NewTab.Count());
            NewTab.tabSearchStat_tslb = SearchStat_tslb;

            Refresh_Search_ts(Tabs.SelectedTab);
            NewTab.RefreshCurrent();
            nudRecord.Value = NewTab.linkedListSS.curr.Twin.N_Record;
        }
   
        private void Tabs_Selecting(object sender, TabControlCancelEventArgs e) // перетыкиваем вкладку мышью на панели Tabs
        {// если выбрана основная вкладка, то вынесем неперед основные текстбоксы
            if (Tabs.SelectedTab == null) { Tabs.Dispose(); Tabs = null; return; }//нет открытых вкладок
            SplitContainer sc= (SplitContainer)Tabs.SelectedTab.Tag;
            if (sc == null) return;
            SearchTabs tabSearch = (SearchTabs) sc.Tag;

            // прячем лишние tc 
            tabSearch.RefreshCurrent();
            if (tabSearch.curnum() < 1)
            {
                TabClose_tsb_Click(null, null); // список пуст-закроем вкладку
                nudRecord_ValueChanged(null, null);
                return;
            }
            else SearchStat_tslb.Text = Convert.ToString(tabSearch.curnum()) + " of " + Convert.ToString(tabSearch.Count());
            nudRecord.Value = tabSearch.linkedListSS.curr.Twin.N_Record;
            nudRecord_ValueChanged(null, null);
            Refresh_Search_ts(Tabs.SelectedTab);
            Tabs.SelectedTab.Controls.Add(Search_ts);
            sc.Panel1.Controls.Add(statusStrip2);
            sc.Panel2.Controls.Add(statusStrip1);
        }
        private void Search_Next_btn_Click(object sender, EventArgs e)
        {
            SplitContainer sc = (SplitContainer)Tabs.SelectedTab.Tag;
            SearchTabs tabSearch = (SearchTabs)sc.Tag;
            if (tabSearch == null) return;
            if (Tabs.SelectedTab == Home) return;
            tabSearch.Next();
        }
        private void Search_Prev_btn_Click(object sender, EventArgs e)
        {
            SplitContainer sc = (SplitContainer)Tabs.SelectedTab.Tag;
            SearchTabs tabSearch = (SearchTabs)sc.Tag;
            if (tabSearch == null) return;
            if (Tabs.SelectedTab == Home) return;
            tabSearch.Prev();
        }
        private void SourceLast_tsb_Click(object sender, EventArgs e)
        {
            SplitContainer sc = (SplitContainer)Tabs.SelectedTab.Tag;
            SearchTabs tabSearch = (SearchTabs)sc.Tag;
            if (tabSearch == null) return;
            if (Tabs.SelectedTab == Home) return;
            tabSearch.toLast();
        }
        private void SourceFirst_tsb_Click(object sender, EventArgs e)
        {
            SplitContainer sc = (SplitContainer)Tabs.SelectedTab.Tag;
            SearchTabs tabSearch = (SearchTabs)sc.Tag;
            if (tabSearch == null) return;
            if (Tabs.SelectedTab == Home) return;
            tabSearch.toFirst();
        }
        private void TabClose_tsb_Click(object sender, EventArgs e)
        {
            SplitContainer sc = (SplitContainer)Tabs.SelectedTab.Tag;
            SearchTabs tabSearch = (SearchTabs)sc.Tag;
            TabPage temptab;
            if (tabSearch == null) return;
            if (Tabs.SelectedTab == Home)  // заккрываем вкладку HOME - 
            {   //открепляем от вкладок и прячем эл-ты управления
                this.Controls.Add(Search_ts); Search_tstb.Text = ""; Search_ts.Visible = false; 
                this.Controls.Add(statusStrip1); statusStrip1.Visible = false;
                this.Controls.Add(statusStrip2); statusStrip2.Visible = false;
            }
            tabSearch.Clear();
            temptab=Tabs.SelectedTab;
            
            Tabs.SelectedTab = Home; //переход на главную вкладку
            sc = (SplitContainer)Tabs.SelectedTab.Tag;
            Tabs.SelectedTab.Controls.Add(Search_ts);
            sc.Panel1.Controls.Add(statusStrip2);
            sc.Panel2.Controls.Add(statusStrip1);
            Refresh_Search_ts(Tabs.SelectedTab);
            temptab.Dispose();
            //nudRecord_ValueChanged(null,null);
        }
  
        private void Delete_btn_Click(object sender, EventArgs e)
        { 
            //long num;
            string data;
            if (Tabs == null) return;
            SplitContainer sc = (SplitContainer)Tabs.SelectedTab.Tag;
            SearchTabs tabSearch = (SearchTabs)sc.Tag;
            if (tabSearch == null) //определимся что будем удалять
            { // открыта вкладка HOME
                data = linkedListSF.curr.Data;
            }
            else // удаляем из вкладки поиска
            {   
                data = tabSearch.tabSource_tb.Text;
            }
            DialogResult result = MessageBox.Show(
                                    "Do you really wants delete message:" +
                                    "\n" + data + "?",
                                    "Please attention !",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question,
                                    MessageBoxDefaultButton.Button1,
                                    MessageBoxOptions.DefaultDesktopOnly);
            if (result == DialogResult.No) { return; }// перевод не меняем
            else // Пользователь подтвердил удаление
            {
                flag_NotSavedYet = true;
                    //удаляем элемент из основного списка OF с переводом
                    linkedListOF.DeleteNode(tabSearch.linkedListSS.curr.Twin.Twin); 
                    //удаляем элемент из основного списка SF на который ссылается элемент из списка поиска SS
                    linkedListSF.DeleteNode(tabSearch.linkedListSS.curr.Twin); 
                    tabSearch.Remove(); //удаляем текущий элемент из списка поиска SS
                    //обновляем вкладку для актуализации видимой инфы
                    if (tabSearch.linkedListSS.Count == 0) TabClose_tsb_Click(null, null);
                    else Tabs_Selecting(null,null);
            }
        }

        private void CloseFilesClear_Click(object sender, EventArgs e)
        {
               
            nudRecord.ReadOnly = true; // устанавливаем номер записи в 1
            nudRecord.Increment = 0;
            nudRecord.BackColor = Color.LightGray;
            nudRecord.Value = 1; 
            Records_lb.Text = "Found: 0 records";
            
            TranslatedFile_tb.Text = ""; TranslatedFile = "";
            SourceFile_tb.Text = ""; SourceFile = "";

            //закроем все вкладки
            while(Tabs!=null) TabClose_tsb_Click(null, null); // список пуст-закроем вкладку

            lbSource.Text = ""; 
            lbTranslated.Text = "";
            Offset_tb.Text = ""; Offset_tb.ReadOnly = true;
            Signature_tb.Text = ""; Signature_tb.ReadOnly = true;
            //Search_tstb.Text = ""; 
            //Search_tstb.ReadOnly = true; 
            flag_NotSavedYet = false; // флаг -сохранение не требуется
            flag_Skipdialog = false; //по-умолчанию - не пропускать диалоги
            progressBar1.Value = 0; progressBar1_lb.Text = "%";
        }

      private void Translate_btn_Click(object sender, EventArgs e)
        {
            SplitContainer sc = (SplitContainer)Tabs.SelectedTab.Tag;
            if (sc == null) return;
            SearchTabs tabSearch = (SearchTabs)sc.Tag;

            tabSearch.tabTranslated_tb.Text += "\n" + Translate_Google(tabSearch.tabSource_tb.Text);
            tabSearch.Translated_KeyUp();
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
        private string Parse_Google_JSON(string str) //распарсиваем ответ Google в JSON
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
                    result += str[startST..(i - 1)];
                    //result += "\n";
                    flag_sent = true;
                }
            }
            return ("\n"+result);
        }

        private void Translated_tb_KeyUp(object sender, KeyEventArgs e)
        {
            if (Tabs.SelectedTab == null) return;
            SplitContainer sc = (SplitContainer)Tabs.SelectedTab.Tag;
            SearchTabs tabSearch = (SearchTabs)sc.Tag;
            tabSearch.Translated_KeyUp();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            languageRu =(string)comboBox1.SelectedValue;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            languageRu = (string)comboBox2.SelectedValue;
        }
        private void SourceSearch_tstb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // символЫ Delete и BackSpace
            {
                if(sender== Search_tstb)NewTab_Click(sender,e);
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void SaveState(DoublyLinkedList<string>linkedList, string str)
        { 
        
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e) 
        { // переход Ctrl+стрелка на новую запись
            if (e.Control && e.KeyCode == Keys.Right)
            {
                Next_btn_Click(null, null);
            }
            else if (e.Control && e.KeyCode == Keys.Left)
            {
                Prev_btn_Click(null, null);
            }
            else if (e.Control && e.KeyCode == Keys.Down)
            {
                Translate_btn_Click(null, null);
            }
            else if (e.Control && e.KeyCode == Keys.S)
            { SaveFile(); }
            else if (e.Control && e.KeyCode == Keys.Z)
            { UNDO_textbox(); }/*
            else if ((e.Control & e.Shift) == Keys.V)
            {
                MessageBox.Show("Ctrl+shift+V detected");
            }*/
        }
        private void UNDO_textbox()
        {
            if (Tabs == null) return;
            if (Tabs.SelectedTab == null) return; // нет открытых файлов
            SplitContainer sc = (SplitContainer)Tabs.SelectedTab.Tag;
            SearchTabs tabSearch = (SearchTabs)sc.Tag;
            if (tabSearch == null) // вызов с главного TABa
            {
                if (linkedListOF.curr.UNDO.curr != null)
                { 
                  linkedListOF.curr.Data = linkedListOF.curr.UNDO.curr.Data; 
                  linkedListOF.curr.UNDO.NextNode(); 
                  nudRecord_ValueChanged(null, null);
                }
            }
            else //UNDO на вкладке поиска 
            {
                if (tabSearch.linkedListSS.curr.UNDO.curr != null)
                {
                    tabSearch.linkedListSS.curr.Data = tabSearch.linkedListSS.curr.UNDO.curr.Data;
                    tabSearch.linkedListSS.curr.UNDO.NextNode();
                    tabSearch.RefreshCurrent();
                }
            }
        }

        private void UNDO_Click(object sender, EventArgs e)
        {
            if (Tabs == null) return;
            UNDO_textbox();
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
        public long N_Record { get; set; } // номер по-порядку 
        public DoublyLinkedList<T> UNDO = new(); // список с отменой
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
            count++; curr.N_Record = count;
            node.Fileposition = Fileposition;
        }
        public void AddFirst(T data, long Fileposition) //использую в UNDO как стек отмены
        {
            DoublyNode<T> node = new DoublyNode<T>(data); // добавить первым в список
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

        // удаление по ссылке на элемент
        public DoublyNode<T> DeleteNode(DoublyNode<T> node1=null) //null- УДАЛИТЬ текущий элемент списка или по ссылке
        {   
            bool flag_isnodecurrent= true; //удаляется текущий элемент
            DoublyNode<T> tempcurr = curr, node = node1;
//если node1 не передан,то удаляем текущий элемент,
//иначе если переданный элемент не текущий, то в конце восстановим curr
            if (node == null) node=curr; else if(node!=curr) flag_isnodecurrent = false; 

            node.Twin = null; //ставим метку, что данный элемент ни на что не ссылается и его можно удалять
            if (node.Next != null) { curr = node.Next; node.Next.Previous = node.Previous; }
            else { tail = node.Previous; curr = node.Previous; }
            // если узел не первый
            if (node.Previous != null) { node.Previous.Next = node.Next; }
            else { head = node.Next; }
            if(count>0)count--;
            if (flag_isnodecurrent == false) curr = tempcurr; // восстанавливаем curr 
            return curr;
        }
        
        public bool RemoveData(T data)// удаление элемента с поиском по строке
        {
            DoublyNode<T> current = head;
            DoublyNode<T> temp = curr;
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
                    head = current.Next;// если первый, переустанавливаем head
                }
                count--;
                curr = temp;
                return true;
            }
            curr = temp;
            return false;
        }

        public int Count { get { return count; } }
        public long FilePosition { get { if (curr != null) return curr.Fileposition;  else return -1; } }
        public DoublyNode<T> Twin { get { if (curr != null) return curr.Twin; else return head; } }

        public bool IsEmpty { get { return count == 0; } }
        public object CurrentData { get { return curr.Data; } } //возвращает данные из текущего элемента списка
        public object Curr { get { return curr; } } //возвращает указатель на текущий элемент списка
        public object DataFrom (DoublyNode<T> Node) ////возвращает данные из произвольного элемента списка
        {
            if (Node != null) return Node.Data; else return null;
        }
        public object TwinFrom(DoublyNode<T> Node) //возвращает указатель на Twin произвольного элемента списка
        {
            if (Node != null) return Node.Twin; else return null;
        }

        public void ReplaceData(T data, DoublyNode<T> directNode=null) // Заменяет поле даты текущего элемента, либо иного
        {                                           // элемента, ссылка на который указана в необязательном поле directNode
            if (directNode == null) directNode = curr;
            if (directNode != null) 
            {
                if(directNode.UNDO.curr==null) directNode.UNDO.AddFirst(directNode.Data, directNode.Fileposition);
                else
                    if (!directNode.UNDO.curr.Data.Equals(data)) // Если данные изменились создадим эл-нт UNDO
                        directNode.UNDO.AddFirst(directNode.Data,directNode.Fileposition);
                if (directNode.UNDO.Count() > 100) //Обрезаем хвост UNDO - лимит не более 100 откатов
                    directNode.UNDO.tail = directNode.UNDO.tail.Previous;
                directNode.Data = data; 
                return; 
            }
        }
        public void SetTwin(DoublyNode<T> twin)
        {
            if (curr == null) return;
            curr.Twin = twin;
        }

        public long GetCurrentNum() 
        {
            DoublyNode<T> item=head,current=curr;
            long num=0,num1=0; //
            if (item == null) return 0; //список пуст
            while(item!=null)
            {
                item = item.Next; num1++;
                if (item == curr) break;
            }
            curr = current; //восстановим curr
            if (item == null) return 0;
            return num;
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
        public DoublyNode<T> GetHead()
        {
            return head;
        }
        public DoublyNode<T> GetTail()
        {
            return tail;
        }
    }

    public class SearchTabs
    {
        public TabPage TabPage;
        public DoublyLinkedList<string> linkedListSS = new(); //создaдим список с результатами поиска

        public TextBox tabSource_tb { get; set; }//поля для хранения текстбоксов на вкладках
        public TextBox tabTranslated_tb { get; set; }
        public SplitContainer splitContainer { get; set; }
        public ToolStripLabel tabSearchStat_tslb { get; set; }
        public ToolStripStatusLabel tabSource_lb { get; set; }//кол-во символов в сообщении текстбокса
        public ToolStripStatusLabel tabTranslated_lb{ get; set; }//кол-во символов в сообщении текстбокса

        public int currnum=1; //номер текущего элемента списка
        public bool TranslatedSource = false;

        public int Count() => linkedListSS.Count;
        public int curnum()
        {
            return currnum;
        }

        public void SetlinkedListHome(DoublyLinkedList<string> linkedList)
        {
            foreach (var item in linkedList)
            {
                linkedListSS.Add(item, linkedList.curr.Fileposition); //создаем в списке поиска новый элемент
                linkedListSS.SetTwin(linkedList.curr); // помещаем в его поле Twin указатель на запись из списка SF
            }
            foreach (var item in linkedListSS) break; // ставим curr на head
        }
        public void SetlinkedListSF(DoublyLinkedList <string> linkedList, string str)
        {
            if (linkedList == null || str =="") return; //если передана пустая строка или непередан список
            //Начинаем поиск подстроки по всем элементам списка linkListSF
            
            var temp=linkedList.curr;
            foreach (var item in linkedList)
            {
                if (item.Contains(str))// Вхождения найдены
                {
                    linkedListSS.Add(item, 0); //создаем в списке поиска новый элемент
                    linkedListSS.SetTwin(linkedList.curr); // помещаем в его поле Twin указатель на запись из списка SF
                    continue; //пропускаем поиск по переводу, чтобы не сделать дубликат в найденном
                }
                if (linkedList.curr.Twin.Data.Contains(str))// проверяем на совпадение и список с переводом
                {
                        linkedListSS.Add(linkedList.curr.Twin.Data, 0);
                        linkedListSS.SetTwin(linkedList.curr);
                }
            }
            foreach (var item in linkedListSS) break; // ставим curr на head
            linkedList.curr = temp;
           // if (linkedListSS.Count == 0) return; //ничего не найдено
                                                 // вот что-то найдено, если вкладка не создавалась - то создадим
        }

        public void Next() //перемещение по результатам поиска
        {
            if (linkedListSS.curr == null) return; // проверим что список не пустой
            if (linkedListSS.curr.Next != null)
            {
                if (linkedListSS.curr.Twin == null) // похоже открытая запись в поиске уже была удалена из основного списка
                { // Удалим ее из списка поиска
                    linkedListSS.DeleteNode(linkedListSS.curr); //удаляем ее без сохранения
                    if (currnum > linkedListSS.Count) currnum--;
                }
                else 
                {
                    linkedListSS.ReplaceData(tabTranslated_tb.Text, linkedListSS.curr.Twin.Twin);
                    //linkedListSS.curr.Twin.Twin.Data = tabTranslated_tb.Text; //сохраняем содержимое текстбокса
                    currnum++; 
                }
                linkedListSS.curr = linkedListSS.curr.Next;
            }
            else // мы в конце списка. перемотаем на начало
            {
                currnum = 1;
                while (linkedListSS.curr.Previous != null)
                { linkedListSS.curr = linkedListSS.curr.Previous; }
            }
            RefreshCurrent();
            tabSearchStat_tslb.Text =Convert.ToString(currnum)+" of "+ linkedListSS.Count;
        }
        public void Prev()//перемещение по результатам поиска
        {
            if (linkedListSS.curr == null) return; // проверим что список не пустой
            if (linkedListSS.curr.Previous != null) 
            {
                if (linkedListSS.curr.Twin == null) // похоже открытая запись в поиске уже была удалена из основного списка
                { // Удалим ее из списка поиска
                    linkedListSS.DeleteNode(linkedListSS.curr); //удаляем ее без сохранения
                    if (currnum > linkedListSS.Count) currnum--;
                }
                else
                {
                    linkedListSS.ReplaceData(tabTranslated_tb.Text, linkedListSS.curr.Twin.Twin);
                    //linkedListSS.curr.Twin.Twin.Data = tabTranslated_tb.Text; //сохраняем содержимое текстбокса
                    currnum--;
                }
                linkedListSS.curr = linkedListSS.curr.Previous;
            }
            else// мы в начале списка. перемотаем в конец
            {
                currnum = linkedListSS.Count;
                while (linkedListSS.curr.Next != null)
                { linkedListSS.curr = linkedListSS.curr.Next; }
            }
            RefreshCurrent();
        }
        public void toFirst()
        {
            if(linkedListSS.curr==null || linkedListSS.Count <= 0) return; // проверим что список не пустой
            currnum = 1;
            while (linkedListSS.curr.Previous != null)
            { linkedListSS.curr = linkedListSS.curr.Previous; }
            RefreshCurrent();
        }
        public void toLast()
        {
            if (linkedListSS.curr == null || linkedListSS.Count <= 0) return; // проверим что список не пустой
            currnum = linkedListSS.Count;
            while (linkedListSS.curr.Next != null)
            { linkedListSS.curr = linkedListSS.curr.Next; }
            RefreshCurrent();
        }
        public void toDefined(long pos)
        {
            if (pos > linkedListSS.Count || pos<=0) return; //запрос на нереальную позицию
            currnum = 1; 
            linkedListSS.curr = linkedListSS.GetHead();
            while (currnum != pos)
            {
                if (linkedListSS.curr.Next != null) { linkedListSS.curr = linkedListSS.curr.Next; currnum++; }
            }
            RefreshCurrent();
        }
        public void RefreshCurrent() //обновим видимую запись во вкладке поиска
        {
            if (linkedListSS.curr == null) return; // вдруг список пуст
            // Проверим, не удалена ли открытая запись в поиске из основного списка
            while (linkedListSS.curr.Twin == null || linkedListSS.curr.Twin.Twin==null) 
            { // Удалим ее из списка поиска
                linkedListSS.DeleteNode(linkedListSS.curr); //удаляем ее без сохранения из списка поиска
                if (currnum > linkedListSS.Count) currnum--;
                if (linkedListSS.curr == null) return; //список стал пуст ?
            }

            tabSource_tb.BringToFront();
            tabTranslated_tb.BringToFront();
            if (TranslatedSource == false)
            {
                tabSource_tb.Text = linkedListSS.curr.Twin.Data;
                tabTranslated_tb.Text = linkedListSS.curr.Twin.Twin.Data;
            }
            else 
            {
                tabSource_tb.Text = linkedListSS.curr.Twin.Twin.Data;
                tabTranslated_tb.Text = linkedListSS.curr.Twin.Data;
            }
            tabSearchStat_tslb.Text = Convert.ToString(currnum) + " of " + linkedListSS.Count;
            Translated_KeyUp();
        }
        public void Translated_KeyUp() // обновляем счетчики длинны и сохраняем перевод
        {   
            int tabSource_lb_len = tabSource_tb.Text.Length; //длины текстбоксов
            int tabTranslated_tb_len = tabTranslated_tb.Text.Length;

            if (tabTranslated_tb_len > tabSource_lb_len) tabTranslated_lb.ForeColor = Color.DarkRed;
            else tabTranslated_lb.ForeColor = Color.Black;
            tabTranslated_lb.Text = Convert.ToString(tabTranslated_tb_len);
            tabSource_lb.Text = Convert.ToString(tabSource_lb_len);
            if(linkedListSS.curr!=null) // защищаемся от случайного пизд-ца с null
                if(linkedListSS.curr.Twin!=null) 
                    linkedListSS.ReplaceData(tabTranslated_tb.Text, linkedListSS.curr.Twin.Twin);
        }

        public void Remove() //удаляем запись из списка SF по ссылке из SS.Twin
        {
            if (linkedListSS.curr != null) linkedListSS.curr.Twin = null; else return;
            linkedListSS.DeleteNode(linkedListSS.curr);
            currnum--;
        }
        public void Clear()
        {
            if (linkedListSS.Count() > 0)
            {
                Translated_KeyUp(); //сохраняем текстбокс
                linkedListSS.Clear();
            }
            if(tabSource_tb!=null) tabSource_tb.Dispose();
            if(tabTranslated_tb!=null) tabTranslated_tb.Dispose();
            if(linkedListSS!=null) linkedListSS = null;
        }
    }
}


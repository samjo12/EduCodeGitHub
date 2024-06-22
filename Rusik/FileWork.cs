using Rusik;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Rusik
{        

    public partial class FileWork
    {
        protected static System.Windows.Forms.ProgressBar progressBar1 = CommonData.progressBar1;
        protected static System.Windows.Forms.Label PerformingFile_tb = CommonData.PerformingFile_tb;
        protected static System.Windows.Forms.Label progressBar1_lb = CommonData.progressBar1_lb;
        protected static System.Windows.Forms.ComboBox comboBox1 = CommonData.comboBox1;
        protected static System.Windows.Forms.ComboBox comboBox2 = CommonData.comboBox2;
        protected static System.Windows.Forms.NumericUpDown nudRecord = CommonData.nudRecord;
        protected static System.Windows.Forms.Label TranslatedFile_tb = CommonData.TranslatedFile_tb;
        protected static System.Windows.Forms.ToolStripTextBox Search_tstb = CommonData.Search_tstb;
        protected static System.Windows.Forms.Label Records_lb = CommonData.Records_lb;
        protected static System.Windows.Forms.Button Start_btn = CommonData.Start_btn;
       // protected static NewTab_Click(null,
       // protected static 

        public static bool ExportTextFileST(string outfile = "") // сохраняем список из памяти в файл с разделителем =
        {

            //if (CommonData.TranslatedFileST == "" || CommonData.TranslatedFileST == null) return false;
            String tmpOutputFile;
            if (outfile == "") tmpOutputFile = CommonData.TranslatedFileST;
            else tmpOutputFile = outfile;
            long onepercent = CommonData.linkedList.Count / 100 - 1, percent = onepercent;
            progressBar1.Value = 0;
            progressBar1_lb.Text = "%";
            using (BinaryWriter writer = new BinaryWriter(File.Open(tmpOutputFile, FileMode.OpenOrCreate)))
            {
                long counter = 0;
                // записываем в файл содержимое списков с данными
                //var tmp_curr = linkedList.curr;
                DoublyNode<string> tmp_link = CommonData.linkedList.Head();
                while (tmp_link != null)
                {
                    var item = tmp_link.OriginalData;
                    byte[] bytes = System.Text.Encoding.UTF8.GetBytes(item + "=");
                    writer.Write(bytes);
                    var item2 = tmp_link.TranslatedData;
                    if (item2 == null) item2 = String.Empty;
                    bytes = System.Text.Encoding.UTF8.GetBytes(item2+ "\r\n");
                    writer.Write(bytes);
                    counter++;
                    if (counter >= percent)
                    {
                        if (progressBar1.Value < 100)
                        { progressBar1.Value++; progressBar1_lb.Text = Convert.ToString(progressBar1.Value) + "%"; }
                        percent += onepercent;
                    }
                    tmp_link = tmp_link.Next;
                }
                //linkedList.curr=tmp_curr; //восстановим curr
            }
            SaveINI();
            return true;
        }

        public static bool LoadINI()
        {
            // InterfaceLanguage={EN,RU};
            // TranslatedFile= {Full Path to last editing file}; полный путь
            // LastRecordNumber= {integer} -номер последней редактируемой записи с прошлой сессии
            // SourceLanguage={EN...} - указание для переводчика Google
            // TranslationLanguage ={RU...} - указание для переводчика Google
            // OpenFileHistory={полный путь к файлу,LastNumber}
            // Загрузим первоначальные настройки программы. Ini файл
            string[] commands = { "InterfaceLanguage", "ProjectFile", "BinaryDataFile", "TranslatedFileST", "LastRecordNumber", "SourceLanguage", "TranslationLanguage" };
            string[] IL = { "en", "ru" }; // возможные языки интерфейса
            CommonData.IniFile = Environment.GetCommandLineArgs()[0]; //получаем имя запущенного файла
            string message = "", command = "", command_value = "";
            CommonData.IniFile = CommonData.IniFile[0..^3]; CommonData.IniFile += "ini";

            if (!File.Exists(CommonData.IniFile)) return false; // ini - файл отсутствует
            //ЧИТАЕМ ФАЙЛ построчно
            using (StreamReader readerSF = new(File.Open(CommonData.IniFile, FileMode.Open)))
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
                                                if (item1 == command_value) CommonData.InterfaceLanguage = command_value;
                                            break;                                        
                                        case "ProjectFile":
                                            if (command_value != "" && command_value != null)
                                                if (File.Exists(command_value))
                                                { CommonData.ProjectFile = command_value; }
                                            break;
                                        case "BinaryDataFile":
                                            if (command_value != "" && command_value != null)
                                                if (File.Exists(command_value))
                                                { CommonData.OriginalBinaryFile = command_value; }
                                            break;
                                        case "TranslatedFileST":
                                            if (command_value != "" && command_value != null)
                                                if (File.Exists(command_value))
                                                { CommonData.TranslatedFileST = command_value; }
                                            break;
                                        case "LastRecordNumber":
                                            command_value = Regex.Replace(command_value, @"\s", "");
                                            CommonData.LastRecordNumber = Convert.ToInt64(command_value);
                                            break;
                                        case "SourceLanguage":
                                            command_value = Regex.Replace(command_value, @"\s", "");
                                            command_value = command_value.ToLower();
                                            foreach (var item1 in CommonData.GoogleLangs.Values)
                                                if (item1 == command_value) CommonData.languageIN = command_value;
                                            comboBox1.SelectedValue = CommonData.languageIN;
                                            foreach (var item1 in CommonData.GoogleLangs.Keys)
                                                if (CommonData.GoogleLangs[item1] == CommonData.languageIN)
                                                { comboBox1.Text = CommonData.GoogleLangs[item1]; break; }
                                            break;
                                        case "TranslationLanguage":
                                            command_value = Regex.Replace(command_value, @"\s", "");
                                            command_value = command_value.ToLower();
                                            foreach (var item1 in CommonData.GoogleLangs.Values)
                                                if (item1 == command_value) CommonData.languageOUT = command_value;
                                            comboBox2.SelectedValue = CommonData.languageOUT;
                                            foreach (var item1 in CommonData.GoogleLangs.Keys)
                                                if (CommonData.GoogleLangs[item1] == CommonData.languageOUT)
                                                { comboBox2.Text = CommonData.GoogleLangs[item1]; break; }
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
            CommonData.mess_tb.Visible = false;
            //if (comboBox1.Text == null) comboBox1.Text = "en";
            //if (comboBox1.Text == null) comboBox1.Text = "ru";
            if (CommonData.ProjectFile != "" && CommonData.ProjectFile != null) 
            { 
                if(LoadProject()) return true;

            }
            if (CommonData.OriginalBinaryFile != "" && CommonData.OriginalBinaryFile != null)
            {
                if(LoadBinary()) return true;
            }
            if (CommonData.TranslatedFileST != "" || CommonData.TranslatedFileST != null)
            {
                if(LoadTextTranslatedST()) return true;
            }
            return false;

        }

        public static void SaveINI()
        {
            /*string[] commands = { "InterfaceLanguage", "TranslatedFile", "LastRecordNumber", "SourceLanguage", "TranslationLanguage", "OpenFileHistory" };
            string[] IL = { "en", "ru" }; // возможные языки интерфейса
            string[] SL = { "en", "ru" }; string[] TL = { "ru", "en" }; //направления перевода
            string message = "", command = "", command_value = "";*/
            //записываем файл Ini заново, поверх старого
            using (StreamWriter writer = new StreamWriter(File.Open(CommonData.IniFile, FileMode.Create)))
            {
                writer.WriteLine("[Last Session]");
                writer.WriteLine("InterfaceLanguage=" + CommonData.InterfaceLanguage);
                writer.WriteLine("SourceLanguage=" + CommonData.languageIN);
                writer.WriteLine("TranslationLanguage=" + CommonData.languageOUT);
                writer.WriteLine("LastRecordNumber=" + Convert.ToString(nudRecord.Value));
                if (CommonData.ProjectFile != null && CommonData.ProjectFile != "")
                { writer.WriteLine("ProjectFile=" + Convert.ToString(CommonData.ProjectFile)); }
                else if (CommonData.OriginalBinaryFile != null && CommonData.OriginalBinaryFile != "")
                { writer.WriteLine("BinaryDataFile=" + CommonData.OriginalBinaryFile); }
                else if (CommonData.TranslatedFileST != null && CommonData.TranslatedFileST != "") 
                  writer.WriteLine("TranslatedFileST=" + CommonData.TranslatedFileST);

            }
        }

        public static bool SaveProject()
        {
            string outfile = CommonData.ProjectFile;
            //Проверим что какой то файл был открыт перед сохранением проекта
            if ((CommonData.TranslatedFileST == "" || CommonData.TranslatedFileST == null)
                && (CommonData.OriginalBinaryFile == "" || CommonData.OriginalBinaryFile == null)) return false;
            String tmpOutputFile;
            if (outfile == "") tmpOutputFile = CommonData.TranslatedFileST;
            else tmpOutputFile = outfile;
            long onepercent = CommonData.linkedList.Count / 100 - 1, percent = onepercent;
            progressBar1.Value = 0;
            progressBar1_lb.Text = "%";
            string ProjectFile = CommonData.ProjectFile;
            long counter = 0;

            using (BinaryWriter writer = new BinaryWriter(File.Open(ProjectFile, FileMode.OpenOrCreate)))
            {
                DoublyNode<string> tmp_link = CommonData.linkedList.Head();
                while (tmp_link != null)
                {   // сохраняем очердную запись списка в файл проекта
                    var item = tmp_link.OriginalData;
                    byte[] bytes = Encoding.UTF8.GetBytes(item+"");
                    Int16 bytes_len = (Int16)bytes.Length;
                    writer.Write((Int64)tmp_link.Fileposition);// записываем позицию строки в файле оригинале
                    writer.Write((Int16)bytes_len);
                    writer.Write(bytes,0,bytes_len);
                    var item2 = tmp_link.TranslatedData;
                    bytes = Encoding.UTF8.GetBytes(item2+"");
                    bytes_len = (Int16)bytes.Length;
                    writer.Write(bytes_len);
                    if(bytes_len>0)writer.Write(bytes);
                    //writer.Write(0xFFFFFFFF);
                    counter++;
                    if (counter >= percent)
                    {
                        if (progressBar1.Value < 100)
                        { progressBar1.Value++; progressBar1_lb.Text = Convert.ToString(progressBar1.Value) + "%"; }
                        percent += onepercent;
                    }
                    tmp_link = tmp_link.Next;
                }
            }
            return true;
        }
        public static bool LoadProject()
        {
            byte sym;

            CommonData.PerformingFile_tb.Text=CommonData.ProjectFile;
            using (BinaryReader reader = new BinaryReader(File.Open(CommonData.ProjectFile, FileMode.Open)))
            { // откроем файл Source на чтение
                long CurrFileposition=reader.BaseStream.Position;
                long MaxFileposition = reader.BaseStream.Length;
                long onepercent = MaxFileposition / 100 - 1, percent = 0, ipercent=0;
                long position = CurrFileposition;
                while (CurrFileposition + 10 <= MaxFileposition)
                {
                    Int64 Fileposition = reader.ReadInt64();// читаем позицию строки в файле оригинале 8 bytes
                    Int16 message_len1 = reader.ReadInt16(); // читаем размер сообщения1 в байтах 2 bytes
                    byte[] message1 = new byte[message_len1];

                    string str1, str2;
                    for (int i = 0; i < message_len1; i++)
                    {
                        sym = reader.ReadByte();
                        message1[i] = sym;
                    }
                    str1 = Encoding.UTF8.GetString(message1);
                    Int16 message_len2 = reader.ReadInt16(); // читаем размер сообщения2 в байтах 2 bytes

                    if (message_len2 > 0)
                    {
                        byte[] message2 = new byte[message_len2];
                        for (int i = 0; i < message_len2; i++)
                        {
                            sym = reader.ReadByte();
                            message2[i] = sym;
                        }
                        str2 = Encoding.UTF8.GetString(message2);
                    }
                    else
                    {
                        str2 = "";
                    }
                    if (str1 == null || str2 == null || str1 == "") continue; // пустые строки не добавляем
                    CommonData.linkedList.Add(str1, str2, Fileposition); // создаем новый элемент списка, с файловым указателем на начало строки
                    CurrFileposition = reader.BaseStream.Position;
                    position = CurrFileposition; percent=CurrFileposition%onepercent;
                    if (ipercent != percent) // проверяем нужно ли двигать прогресс бар на 1%
                    {
                        ipercent = percent;
                        if (progressBar1.Value < 100) { progressBar1.Value++; }
                        progressBar1_lb.Text = Convert.ToString(progressBar1.Value) + "%";
                    }
                }
            }
            nudRecord.Maximum = CommonData.linkedList.Count;
            nudRecord.Minimum = 1;
            Records_lb.Text = "Found " + CommonData.linkedList.Count + " records.";
            if (CommonData.LastRecordNumber != 1) nudRecord.Value = CommonData.LastRecordNumber;
            else nudRecord.Value = 1;
            nudRecord.ReadOnly = false;
            return true;
        }

        public static bool LoadBinary()
        { 
            //if (Signature_tb.TextLength == 0) { MessageBox.Show("You must setup Signature to start."); return; }
            // начиная со смещения Offset начнем по байтам искать сигнатуру Signature.
            // Если найдем, то будем добавлять текст в список DoubleNode. Строим списки параллельно у обоих файлов.
            // создаем объект BinaryReader

            string SourceFile = CommonData.OriginalBinaryFile;
            //           string OutputFile = CommonData.OutputFile;
            CommonData.PerformingFile_tb.Text = CommonData.OriginalBinaryFile;
            int MaxBytesMessage = CommonData.MaxBytesMessage;
            byte[] Signature = CommonData.Signature;

            if (SourceFile == "" || SourceFile == null) return false; // имя Binary файла еще не задано
            Start_btn.Visible = false;

            using (BinaryReader readerSF = new BinaryReader(File.Open(SourceFile, FileMode.Open)))
            { // откроем файл Source на чтение
                FileInfo src = new FileInfo(SourceFile);
                byte[] buf = new byte[MaxBytesMessage * 3]; //Буффер для чтения из файла строки текста
                long l = src.Length; //размер исходного файла в байтах
                int sign_pointer = 0; // 0-сигнатура еще не встречена, 1..N - номер символа в сигнатуре
                long onepercent = l / 100 - 1, percent = onepercent;
                long position;
                byte b;

                for (long i = 0; i<l; i++)
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

                            position = i+1; // сохраняем позицию начала сообщения
                            for (int j = 0; j<lentxt; j++)//читаем lentxt байт сообщения
                            {
                                buf[j] = readerSF.ReadByte(); i++;
                            }
                            string message;  
                            byte[] tmp_bytes = new byte[lentxt];
                            // преобразованиЕ массива byte в строку string
                            for (int j = 0; j<lentxt; j++) tmp_bytes[j] = buf[j];
                            message = Encoding.UTF8.GetString(tmp_bytes);
                            if (message == "") continue; // пустые строки не перехватываем
                            // создаем элемент списка с новой записью                                                           
                            CommonData.linkedList.Add(message, "", position); // создаем новый элемент списка, с файловым указателем на начало строки
                        }
                    }
                    else { sign_pointer = 0; }// сигнатура не подтвердилась
                    if (i >= percent) // проверяем нужно ли двигать прогресс бар на 1%
                    {
                        if (progressBar1.Value < 100) { progressBar1.Value++; }
                        percent += onepercent; progressBar1_lb.Text = Convert.ToString(progressBar1.Value) + "%";
                    }
                }
                nudRecord.Maximum = CommonData.linkedList.Count;
                nudRecord.Minimum = 1;
                
                Records_lb.Text = "Found " + CommonData.linkedList.Count + " records.";
                nudRecord.Value = CommonData.linkedList.Count;
                nudRecord.ReadOnly = false;
            }
            return true;
        }

        public static bool LoadTextTranslatedST() // Чтение и разбор текстового переведенного файла
        {
            if (!File.Exists(CommonData.TranslatedFileST)) return false; // файл не существует, открывать нечего

            using (BinaryReader readerTF = new BinaryReader(File.Open(CommonData.TranslatedFileST, FileMode.Open)))
            { // откроем файл Translated Text File на чтение
                FileInfo src = new FileInfo(CommonData.TranslatedFileST);
                long l = src.Length; //размер исходного файла в байтах
                byte[] buf1 = new byte[CommonData.MaxBytesMessage * 3]; //Буффер для чтения из файла строки текста до знака =
                byte[] buf2 = new byte[CommonData.MaxBytesMessage * 3]; //Буффер для чтения из файла строки текста после знака =
                string str1 = "", str2 = "";
                long onepercent = l / 100 - 1, percent = onepercent;
                byte b;
                bool sourcePart = true; // флаг 
                bool flag_ReplaceData = false; // флаг
                //bool flag_Skipdialog = false; //флаг пропуска пользовательских диалоговых окон перенес в заголовок класса
                int bufcounter = 0;
                object maxK_Node = null; // ссылка на потенциально дублирующуюся запись в списке
                                         //object maxK_NodeOF = null; //ссылка на перевод заменяемой строки

                for (long i = 0; i < l; i++)
                { // посимвольно читаем исходный файл в буффер
                    b = readerTF.ReadByte(); bufcounter++;
                    //Пустые строки не будем принимать за отдельные сообщения
                    if (b == 0xa && bufcounter == 2 && sourcePart == true && buf1[bufcounter - 2] == 0xd)
                    { bufcounter = 0; continue; }

                    if (b == 0x3d && sourcePart == true) //найден символ =  
                    { //теперь строку из буфера нужно проверить на наличие в списке linkedList оригинальных строк
                        bufcounter--;
                        byte[] tmp_bytes1 = new byte[bufcounter];
                        for (int j = 0; j < bufcounter; j++) tmp_bytes1[j] = buf1[j];
                        str1 = Encoding.UTF8.GetString(tmp_bytes1); // Создаем из буфера с бaйтами строку в UTF8
                        //Если строка пустая, менее 3 символов или содержит символ ENTER, то такое сообщение - пропускаем
                        if (str1 == "" || str1.Length < 2) { bufcounter = 0; continue; }
                        float maxK; // коэфф. схожести строк по Танимото

                        if (CommonData.flag_Skipdialog == false) //если включен режим пропуска диалога, то поиск совпадающих строк-отключаем
                        { maxK = FindSameString(str1, CommonData.linkedList, out maxK_Node); } // проверим на наличие совпадений в списке
                        else { maxK = 0; }
                        if (maxK_Node == null) { maxK = 0; }
                        //maxK_NodeOF = linkedList.TwinFrom((DoublyNode<string>)maxK_Node); // получаем ссылку на ячейку с переводом
                        if ((maxK <= 1) && (maxK >= 0.96)) // совпадение от 96 до 100% - это та же самая строка
                        {
                            CommonData.linkedList.ReplaceData(str1, (DoublyNode<string>)maxK_Node);
                            flag_ReplaceData = true;
                        }
                        else if (maxK < 0.96 && maxK > 0.91)// строка очень Похожа на одну из строк в списке,
                        {                                   // спросим пользователя
                            //var str2_tmp = linkedList.DataFrom((DoublyNode<string>)maxK_Node);
                            var str2_tmp = ((DoublyNode<string>)maxK_Node).DataOriginal;
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
                            if (result == DialogResult.No) { CommonData.linkedList.Add(str1, "", 0); }// создаем новый элемент списка
                            else if (result == DialogResult.Yes)// пользователь сказал что строки одинаковые, тогда заменим старую строку новой
                            {
                                CommonData.linkedList.ReplaceData(str1, (DoublyNode<string>)maxK_Node);
                                flag_ReplaceData = true;
                            }
                            else //result == DialogResult.Cancel
                            { CommonData.flag_Skipdialog = true; }

                        }
                        else // maxK <0.75 можно не спрашивать строка точно уникальная
                        {
                            CommonData.linkedList.Add(str1, "", 0);// создаем новый элемент списка
                        }
                        sourcePart = false;
                        bufcounter = 0;
                        continue;
                    }
                    // Если встретили символы в оригинальной части фразы, то просто пропускаем
                    // если "0d 0a" втретили в переводе, то это конец строки и будем ожидать новой фразы перeвода
                    if (bufcounter >= 2)
                        if ((sourcePart == false && b == 0xa && buf2[bufcounter - 2] == 0xd) || (i == (l - 1)))
                        {
                            if (i < l - 1) bufcounter -= 2; //удаляем последниe символы 0d и 0a
                            else buf2[bufcounter - 1] = b; // дописываем последний символ в файле
                            byte[] tmp_bytes2 = new byte[bufcounter];
                            for (int j = 0; j < bufcounter; j++) tmp_bytes2[j] = buf2[j];
                            str2 = Encoding.UTF8.GetString(tmp_bytes2);
                            bufcounter = 0;
                            sourcePart = true;

                            if (flag_ReplaceData == false || CommonData.flag_Skipdialog == true)// запишем новую строку в список
                            {
                                //linkedListOF.Add(str2, 0); // создаем запись в списке с переводом
                                CommonData.linkedList.ReplaceData(str2);
                                // linkedList.SetTwin(linkedListOF.curr); //связываем ссылками исходную строку со строкой перевода в списках
                                // linkedListOF.SetTwin(linkedList.curr);
                            }
                            else //Делаем замену перевода т.к. flag_ReplaceData == true
                            {
                                var old_data = ((DoublyNode<string>)maxK_Node).DataTranslated;

                                // перевод не меняем т.к. новое значение - пустое , ИЛИ новая строка идентична старой
                                if (str2 == "" || str2 == (string)old_data) { flag_ReplaceData = false; continue; }
                                // Если старое значение не пустое ИЛИ новая строка не пустая - спрашиваем пользователя о замене
                                if (((string)old_data != "") && (str2 != ""))
                                {
                                    DialogResult result = MessageBox.Show(
                                    "Do you really wants to replace string 1 with string 2 ?" +
                                    "\n1:(" + ((string)old_data).Length + "): " + (string)old_data +
                                    "\n2:(" + str2.Length + "): " + str2 +
                                    "\nIf you say \"No\"  - string \"2:\" will be lost!" +
                                    "\n\nCancel will skipiing this dialog and adding all records as new.",
                                    "Please attention !",
                                    MessageBoxButtons.YesNoCancel,
                                    MessageBoxIcon.Question,
                                    MessageBoxDefaultButton.Button1,
                                    MessageBoxOptions.DefaultDesktopOnly);
                                    if (result == DialogResult.No) { flag_ReplaceData = false; continue; }// перевод не меняем
                                    else if (result == DialogResult.Cancel) { CommonData.flag_Skipdialog = true; }
                                }
                                CommonData.linkedList.ReplaceData(str2, (DoublyNode<string>)maxK_Node);//меняем перевод
                                flag_ReplaceData = false; // отработал - сбросили
                            }
                            continue;
                        }
                    if (bufcounter > CommonData.MaxBytesMessage) { bufcounter = 0; continue; }// размер сообщения превышен - урезаем его

                    if (sourcePart == true) { buf1[bufcounter - 1] = b; }
                    else { buf2[bufcounter - 1] = b; }

                    // играем с прогрессбаром

                    if (i >= percent) // проверяем нужно ли двигать прогресс бар на 1%
                    {
                        if (progressBar1.Value < 100) { progressBar1.Value++; }
                        percent += onepercent; progressBar1_lb.Text = Convert.ToString(progressBar1.Value) + "%";
                    }
                }


            }
            nudRecord.Maximum = CommonData.linkedList.Count;
            nudRecord.Minimum = 1;
            if (CommonData.linkedList.Count > 1)
            {
                nudRecord.Value = 1;
                nudRecord.ReadOnly = false;
                //lbSource.Text = Convert.ToString(Source_tb.Text.Length); // указываем кол-во символов в исходном сообщении
                return true;
            }
            else return false;
        }
        private static float FindSameString(string str1, DoublyLinkedList<string> linkedList, out object maxK_node)
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

        private static float Tanimoto(string str1, string str2)
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
            int c = 0; //кол-во общих элементов в двух множествах a & b
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
            return kTanimoto; // Чем ближе к 1 , тем достовернее сходство
        }

    }
}

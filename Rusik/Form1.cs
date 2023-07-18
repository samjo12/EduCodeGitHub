using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Web;
using System.Text.RegularExpressions;
using System.Runtime.ConstrainedExecution;
using System.Diagnostics.Eventing.Reader;
using Rusik.Properties;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using TextBox = System.Windows.Forms.TextBox;
using static System.Runtime.InteropServices.JavaScript.JSType;
using String = System.String;
using System.Security.Cryptography.Xml;
using System.Xml.Linq;
using System.Xml;
///using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Rusik
{
    public partial class Form1 : Form
    {
        static readonly int MaxBytesMessage = 27000; // Максимальный размер сообщения в байтах
        public long CurrentnudRecord; // номер текущей записи списка при запуске поиска
        // номер текущей вкладки в окне с Source, использую для динамич. именования контролов
        public int currentTabS = 0;

        //public Dictionary <TabPage, SearchTabs> OpenedTabs; //коллекция открытых вкладок
        public long SourceNodeCounter = 0; // счетчик-указатель на текущую запись списка
        public string BinaryFile = ""; // бинарный файл
        public string TextFile = ""; //Текстовый файл частично переведенный ранее со знаком разделителем "="
        //public string OutputTextFile = ""; // Выходной текстовый файл с текущим рабочим переводом
        string IniFile = ""; //полный путь к INI файлу , типа последний открытый файл
        string IniFileSpec = ""; // полный путь к ini файлу который открывается программой

        public DoublyLinkedList<string>[] linkedList = new DoublyLinkedList<string>[10]; // связный список c переводом


        public bool flag_NotSavedYet = false;//флаг - требуется сохранение, данные были изменены
        public bool flag_Skipdialog = false; //флаг пропуска пользовательских диалоговых окон
        //флаг наличие доп.данных о смещениях/позициях текстовых строк внутри бинарного файла :
        //public bool flag_ExtraDataforBinary = false; //наличие спец.файла с картой рипнутого бинарного файла

        public List<byte> SignatureINList = new List<byte>();
        public List<byte> SignatureOUTList = new List<byte>();
        public byte[] signatureIN = null;//{ 04-00-06-00 };  // Сигнатура из байт Английский
        public byte[] signatureOUT = null;//{ 0C-00-06-00 }; //сигнатура русский
        public long OffsetIN = 0; // Смещение от конца сигнатуры до счетчика текста
        public long OffsetOUT = 0;

        public string languageIN = "en"; //из модуля google-переводчика входной/выходной языки
        public string languageOUT = "ru";
        public string InterfaceLanguage = "en"; //английский язык по-умолчанию
        public long LastRecordNumber = 1; // это сохраненный в ini файле параметр nudRecord касательно последнего открытого файла
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
        // объявление имен элементов эправления
        public TabControl Tabs; //таб-контрол
        public TabPage Home; // главная вкладка
        public TextBox mess_tb; //стартовый help
        public System.Windows.Forms.Button SkipCheck_btn; //кнопка пропуска диалогов

        public byte[] SignatureIN { get => signatureIN; set => signatureIN = value; }
        public byte[] SignatureOUT { get => signatureOUT; set => signatureOUT = value; }
        public Form1()
        {
            InitializeComponent();
            // вешаем обработчик закрытия окна по крестику
            this.FormClosing += new FormClosingEventHandler(this.Form1_FormClosing);
            // подключаем коллекцию со списком языков перевода к комбобоксу 
            comboBox1.DataSource = new BindingSource(GoogleLangs, null);
            comboBox2.DataSource = new BindingSource(GoogleLangs, null);
            comboBox1.DisplayMember = "Key"; comboBox2.DisplayMember = "Key";
            comboBox1.ValueMember = "Value"; comboBox2.ValueMember = "Value";

            //стартовое сообщение о работе программы

            mess_tb = new();
            Font newFont = new Font(FontFamily.GenericMonospace, 14);
            mess_tb.Location = new Point(16, 50);
            mess_tb.Name = Resources.usageMessageTitle;
            mess_tb.Size = new Size(977, 500);
            mess_tb.Font = newFont;
            mess_tb.Text = @Resources.usageMessage;
            mess_tb.Multiline = true;
            mess_tb.ReadOnly = true;
            base.Controls.Add(mess_tb);

            //linkedList[0] = new DoublyLinkedList<string>();


            Load_INI(); // читаем ini- файл
                        // формируем первую вкладку

        }
        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Quit_tsmi_Click(sender, e);
        }

        private void Quit_tsmi_Click(object sender, EventArgs e) // МЕНЮ Quit
        {   // если sender==null ,то из программы не выйдем
            // если исходный файл открыт, то предлагаем сохранить выходной файл
            if (linkedList != null && linkedList[0].Count != 0) // если в памяти есть список - то предлагаем сохраниться
            {
                if (flag_NotSavedYet == true)
                    do
                    {
                        DialogResult result = MessageBox.Show(
                        "Do you want to Save file?\n" + TextFile,
                        "Attention",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button1);
                        if (result == DialogResult.No) { Save_INI(); return; }// пользователь отказался от сохранения
                    } while (SaveFile() == false); // согласился сохраниться , но что-то пошло не так. Даем еще одну попытку...
                linkedList[0].Clear();
                Save_INI();
            }

            if (sender != null) Environment.Exit(0);

        }
        private void Save_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private bool SaveFile(string outfile = "") // сохраняем список из памяти в файл с разделителем =
        {
            if (TextFile == "" || TextFile == null) return false;
            System.String tmpOutputFile;
            if (outfile == "") tmpOutputFile = TextFile;
            else tmpOutputFile = outfile;
            long onepercent = linkedList[0].Count / 100 - 1, percent = onepercent;
            progressBar1.Value = 0;
            progressBar1_lb.Text = "%";
            using (BinaryWriter writer = new BinaryWriter(File.Open(tmpOutputFile, FileMode.OpenOrCreate)))
            {
                long counter = 0;
                // записываем в файл содержимое списков с данными
                var tmp_curr = linkedList[0].curr;
                foreach (var item in linkedList)
                {
                    if (item == null) break;
                    byte[] bytes = System.Text.Encoding.UTF8.GetBytes(item + "=");
                    writer.Write(bytes);
                    var item2 = linkedList[0].Twin.Data;
                    if (item2 == null) item2 = "";
                    item2 += "\r\n";
                    bytes = System.Text.Encoding.UTF8.GetBytes(item2);
                    writer.Write(bytes);
                    counter++;
                    if (counter >= percent)
                    {
                        if (progressBar1.Value < 100)
                        {
                            progressBar1.Value++;
                            progressBar1_lb.Text = Convert.ToString(progressBar1.Value) + "%";
                        }
                        percent += onepercent;
                    }
                }
                linkedList[0].curr = tmp_curr; //восстановим curr
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
            string OutputBinaryFile; // Выходной  файл с текущим рабочим переводом
            if (tmpSourceFile == "" || tmpSourceFile == null) return; // пользователь не выбрал файла
            else BinaryFile = tmpSourceFile;
            OutputBinaryFile = BinaryFile + ".RusikBin";

            if (File.Exists(OutputBinaryFile))//такой файл уже есть, т.е. работа с ним велась ранее поищем ini - шку
            {
                DialogResult result = MessageBox.Show(String.Format(Resources.questionNewSession, BinaryFile),
                    Resources.questionTitle,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1);
                if (result == DialogResult.No)// пользователь выбрал удалить старый файл и начать новую сессию
                {
                    File.Delete(OutputBinaryFile);
                    FileInfo srcF = new FileInfo(BinaryFile);

                    srcF.CopyTo(OutputBinaryFile);
                    //FileInfo outF = new FileInfo(OutputFile); // ставим атрибуты на копию hidden
                    //outF.Attributes |= FileAttributes.Hidden;
                    //outF.Attributes |= FileAttributes.ReadOnly;
                }
            }
            else
            {// Если выходной файл еще не существует, копируем выбранный файл во временный
                FileInfo srcF = new FileInfo(BinaryFile);
                srcF.CopyTo(OutputBinaryFile);
            }
            BinaryFile = OutputBinaryFile; //все изменения будем вносить в копию бинарного файла
            // выведем имя открытого файла
            //SourceFile_tb.ForeColor = Color.White;
            SourceFile_tb.BackColor = Color.White;
            SourceFile_tb.Text = BinaryFile;

            //разблокируем поля Смещения, Поиска Сигнатуры и Поиска строк текста во входном и выходном файлах
            SignatureIN_tb.ReadOnly = false; // textbox Offset
            SignatureOUT_tb.ReadOnly = false; // texbox Signature
            OffsetIN_tb.ReadOnly = false;
            OffsetOUT_tb.ReadOnly = false;
            Search_tstb.ReadOnly = false;
            Start_btn.Visible = true;
        }

        private protected void OpenTextFile_tsmi_Click(object sender, EventArgs e)//MENU Open Text File
        {
            OpenFileDialog openFileDialog1 = new();
            openFileDialog1.ShowDialog();
            TextFile = openFileDialog1.FileName;
            if (TextFile == null || TextFile == string.Empty) return; //без имени файла дальше нечего делать

            //создадим кнопку пропуска диалога
            SkipCheck_btn = new();
            SkipCheck_btn.Location = new Point(839, 678);
            SkipCheck_btn.Size = new Size(104, 23);
            SkipCheck_btn.Text = "SkipChecking";
            this.Controls.Add(SkipCheck_btn);
            SkipCheck_btn.Click += SkipCheck_btn_Click;


            /*
            Thread tr1 = new Thread(() => OpenTranslatedFile());
            tr1.SetApartmentState(ApartmentState.STA);
            tr1.Start();
            tr1.Join();*/

            OpenTextFile();

            SkipCheck_btn.Dispose(); //удалим кнопку
        }
        private void SkipCheck_btn_Click(object sender, EventArgs e)
        {
            flag_Skipdialog = true; // пропустить диалоги и проверку на дубликаты строк в файле
        }

        private void OpenTextFile() // Чтение и разбор текстового переведенного файла
        {
            string message;
            string full_message = "";
            DoublyLinkedList<string> LList;
            int LLRang;
         //   object maxK_Node = null; // ссылка на потенциально дублирующуюся запись в списке

            if (!File.Exists(TextFile)) return; // файл не существует, открывать нечего
                                                //ЧИТАЕМ ФАЙЛ построчно

            if (linkedList == null || linkedList[0]==null || linkedList[0].Count == 0)
            {
                linkedList[0] = new DoublyLinkedList<string>();
                LList = linkedList[0];
            }
            else
            {
                LLRang = linkedList.Length;
                linkedList[LLRang] = new DoublyLinkedList<string>();
                LList = linkedList[LLRang];
            }
           /* using (StreamReader readerSF = new(File.Open(TextFile, FileMode.Open)))
            {
                while ((message = readerSF.ReadLine()) != null)
                {
                    full_message=string.Concat(full_message,message);
                    string[] records = full_message.Split('=');

                    if (records.Length >= 2) // количество подстрок
                    {
                        if (records[0].Length > 2) //длина строки более 2 символов
                        {
                            float maxK; // коэфф. схожести строк по Танимото

                            if (flag_Skipdialog == false) //если включен режим пропуска диалога, то поиск совпадающих строк-отключаем
                            { maxK = FindSameString(full_message, LList, out maxK_Node); } // проверим на наличие совпадений в списке
                            else { maxK = 0; }
                            if (maxK_Node == null) { maxK = 0; }
                            //maxK_NodeOF = linkedList.TwinFrom((DoublyNode<string>)maxK_Node); // получаем ссылку на ячейку с переводом
                            if ((maxK <= 1) && (maxK >= 0.96)) // совпадение от 96 до 100% - это та же самая строка
                            {
                                //linkedList.ReplaceData(records[0], (DoublyNode<string>)maxK_Node);

                                LList.AddInPart(full_message); maxK_Node = LList.Curr; 
                            }
                            else if (maxK < 0.96 && maxK > 0.91)// строка очень Похожа на одну из строк в списке,
                            {                                   // спросим пользователя
                                var str2_tmp = LList.GetTData((DoublyNode<string>)maxK_Node);
                                DialogResult result = MessageBox.Show(
                                "There were detected couple similar strings! Similarity is " + Convert.ToString(maxK * 100) + "%\nAre it the same?" +
                                "\n1:(" + full_message.Length + "): " + full_message +
                                "\n2:(" + ((string)str2_tmp).Length + "): " + str2_tmp +
                                "\n\nCancel will skip this dialog and adding all strings as new.",
                                "Please attention !",
                                MessageBoxButtons.YesNoCancel,
                                MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button1);
                                if (result == DialogResult.No) { LList.AddInPart(full_message); }// создаем новый элемент списка
                                else if (result == DialogResult.Yes)// пользователь сказал что строки одинаковые, то дополним ее еще одним переводом
                                {
                                    LList.ReplaceData(records[1], (DoublyNode<string>)maxK_Node);
                                }
                                else //result == DialogResult.Cancel
                                { flag_Skipdialog = true; }

                            }
                            else // maxK <0.75 можно не спрашивать строка точно уникальная
                            {
                                LList.Add(full_message, records[1]);// создаем новый элемент списка
                            }
                        }
                        full_message = "";
                    }
                    else if (records.Length < 2) // символа разделителя '=' не было, считаем еще одну строку впридачу к предыдущей
                    { continue; }
                }//читаем следующую строку
            }//закрываем файл*/
           using (BinaryReader readerTF = new BinaryReader(File.Open(TextFile, FileMode.Open)))
            { // откроем файл Translated Text File на чтение
                FileInfo src = new FileInfo(TextFile);
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
                object maxK_Node = null; // ссылка на потенциально дублирующуюся запись в списке


                for (long i = 0; i < l; i++)
                { // посимвольно читаем исходный файл в буффер
                    b = readerTF.ReadByte(); bufcounter++;
                    //Пустые строки не будем принимать за отдельные сообщения
                    if (b == 0xa && bufcounter == 2 && sourcePart == true && buf1[bufcounter - 2] == 0xd)
                    { bufcounter = 0; continue; }

                    if (b == 0x3d && sourcePart == true) //найден символ =  
                    { //теперь строку из буфера нужно проверить на наличие в списке linkedListSF оригинальных строк
                        bufcounter--;
                        byte[] tmp_bytes1 = new byte[bufcounter];
                        for (int j = 0; j < bufcounter; j++) tmp_bytes1[j] = buf1[j];
                        str1 = Encoding.UTF8.GetString(tmp_bytes1); // Создаем из буфера с бaйтами строку в UTF8
                        //Если строка пустая, менее 3 символов или содержит символ ENTER, то такое сообщение - пропускаем
                        if (str1 == "" || str1.Length < 2) { bufcounter = 0; continue; }
                        float maxK; // коэфф. схожести строк по Танимото

                        if (flag_Skipdialog == false) //если включен режим пропуска диалога, то поиск совпадающих строк-отключаем
                        { maxK = FindSameString(str1, LList, out maxK_Node); } // проверим на наличие совпадений в списке
                        else { maxK = 0; }
                        if (maxK_Node == null) { maxK = 0; }
                        //maxK_NodeOF = linkedList.TwinFrom((DoublyNode<string>)maxK_Node); // получаем ссылку на ячейку с переводом
                        if ((maxK <= 1) && (maxK >= 0.96)) // совпадение от 96 до 100% - это та же самая строка
                        {
                            //linkedList.ReplaceData(str1, (DoublyNode<string>)maxK_Node);
                            LList.AddInPart(full_message); maxK_Node = LList.Curr;
                            flag_ReplaceData = true;
                        }
                        else if (maxK < 0.96 && maxK > 0.91)// строка очень Похожа на одну из строк в списке,
                        {                                   // спросим пользователя
                            var str2_tmp = LList.GetTData((DoublyNode<string>)maxK_Node);
                            DialogResult result = MessageBox.Show(
                            "There were detected couple similar strings! Similarity is " + Convert.ToString(maxK * 100) + "%\nAre it the same?" +
                            "\n1:(" + str1.Length + "): " + str1 +
                            "\n2:(" + ((string)str2_tmp).Length + "): " + str2_tmp +
                            "\n\nCancel will skip this dialog and adding all strings as new.",
                            "Please attention !",
                            MessageBoxButtons.YesNoCancel,
                            MessageBoxIcon.Question,
                            MessageBoxDefaultButton.Button1);
                            if (result == DialogResult.No) { LList.AddInPart(str1); }// создаем новый элемент списка
                            else if (result == DialogResult.Yes)// пользователь сказал что строки одинаковые, тогда заменим старую строку новой
                            {
                                //LList.ReplaceData(str1, (DoublyNode<string>)maxK_Node);
                                flag_ReplaceData = true;
                            }
                            else //result == DialogResult.Cancel
                            { flag_Skipdialog = true; }

                        }
                        else // maxK <0.75 можно не спрашивать строка точно уникальная
                        {
                            LList.AddInPart(str1);// создаем новый элемент списка
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

                            if (flag_ReplaceData == false || flag_Skipdialog == true)// запишем новую строку в список
                            {
                                LList.AddOutPart(str2); // создаем запись в списке с переводом
                                                             //         linkedListSF.SetTwin(linkedListOF.curr); //связываем ссылками исходную строку со строкой перевода в списках
                                                             //        linkedListOF.SetTwin(linkedListSF.curr);
                            }
                            else //Делаем замену перевода т.к. flag_ReplaceData == true
                            {
                                var old_data = LList.GetTData1((DoublyNode<string>)maxK_Node);
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
                                    MessageBoxDefaultButton.Button1);
                                    if (result == DialogResult.No) { flag_ReplaceData = false; continue; }// перевод не меняем
                                    else if (result == DialogResult.Cancel) { flag_Skipdialog = true; }
                                }
                                LList.ReplaceData(str2, (DoublyNode<string>)maxK_Node);//меняем перевод
                                flag_ReplaceData = false; // отработал - сбросили
                            }
                            continue;
                        }
                    if (bufcounter > MaxBytesMessage) { bufcounter = 0; continue; }// размер сообщения превышен - урезаем его

                    if (sourcePart == true) { buf1[bufcounter - 1] = b; }
                    else { buf2[bufcounter - 1] = b; }

                    // играем с прогрессбаром

                    if (i >= percent) // проверяем нужно ли двигать прогресс бар на 1%
                    {
                        
                        percent += onepercent; progressBar1_lb.Text = Convert.ToString(progressBar1.Value) + "%";
                    }
                }


            }
            nudRecord.Maximum = LList.Count;
            nudRecord.Minimum = 1;
            if (LList.Count > 1)
            {

                //Records_lb.Text = "Found " + linkedListSF.Count + " records.";
                //Выведем в SourceFile_tb первый элемент списка
                //foreach (var item in linkedListSF) { Source_tb.Text = item; break; }
                //foreach (var item in linkedListOF) { Translated_tb.Text = item; break; }
                nudRecord.Value = 1;
                nudRecord.ReadOnly = false;
                //lbSource.Text = Convert.ToString(Source_tb.Text.Length); // указываем кол-во символов в исходном сообщении
                NewTab_Click(null, null); //пытаемся открыть основную вкладку HOME
                Translated_tb_KeyUp(null, null); //обновляем число символов в переводе
                Records_lb.Text = "Found " + LList.Count + " records.";
                // разблокируем строку поиска
                Search_tstb.ReadOnly = false;
                TranslatedFile_tb.Text = TextFile;

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
            if (str1out.Length != str2out.Length) return 0; //вычищенные строки не совпадают по длине
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
        private void Load_INI()
        {
            // InterfaceLanguage={EN,RU};
            // TextFile= {Full Path to last editing file}; полный путь
            // LastRecordNumber= {integer} -номер последней редактируемой записи с прошлой сессии
            // SourceLanguage={EN...} - указание для переводчика Google
            // TranslationLanguage ={RU...} - указание для переводчика Google
            // OpenFileHistory={полный путь к файлу,LastNumber}
            // Загрузим первоначальные настройки программы. Ini файл
            string[] commands = { "InterfaceLanguage", "TextFile", "BinaryFile",
                "LastRecordNumber", "SourceLanguage", "TranslationLanguage", "OpenFileHistory",
                "SignatureIN", "SignatureOUT", "OffsetIN", "OffsetOUT" };
            string[] IL = { "en", "ru" }; // возможные языки интерфейса
            IniFile = Environment.GetCommandLineArgs()[0]; //получаем имя запущенного файла
            string message = "", command = "", command_value = "";
            IniFile = IniFile[0..^3]; IniFile += "ini";
            long LastRecordNumber = 1;
            int len;

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
                                        case "TextFile":
                                            if (command_value != "" && command_value != null)
                                                if (File.Exists(command_value))
                                                { TextFile = command_value; }
                                                else LastRecordNumber = 1;
                                            break;
                                        case "BinaryFile":
                                            if (command_value != "" && command_value != null)
                                                if (File.Exists(command_value))
                                                { BinaryFile = command_value; }
                                                else LastRecordNumber = 1;
                                            break;
                                        case "LastRecordNumber":
                                            command_value = Regex.Replace(command_value, @"[^0123456789]", "");
                                            LastRecordNumber = Convert.ToInt64(command_value);
                                            break;
                                        case "SourceLanguage":
                                            command_value = Regex.Replace(command_value, @"\s", "");
                                            command_value = command_value.ToLower();
                                            foreach (var item1 in GoogleLangs.Values)
                                                if (item1 == command_value) languageIN = command_value;
                                            comboBox1.SelectedValue = languageIN;
                                            foreach (var item1 in GoogleLangs.Keys)
                                                if (GoogleLangs[item1] == languageIN)
                                                { comboBox1.Text = GoogleLangs[item1]; break; }
                                            break;
                                        case "TranslationLanguage":
                                            command_value = Regex.Replace(command_value, @"\s", "");
                                            command_value = command_value.ToLower();
                                            foreach (var item1 in GoogleLangs.Values)
                                                if (item1 == command_value) languageOUT = command_value;
                                            comboBox2.SelectedValue = languageOUT;
                                            foreach (var item1 in GoogleLangs.Keys)
                                                if (GoogleLangs[item1] == languageOUT)
                                                { comboBox2.Text = GoogleLangs[item1]; break; }
                                            break;
                                        case "SignatureIN":
                                            SignatureIN_tb.Text = GetHexString(command_value, "IN");
                                            break;
                                        case "SignatureOUT":
                                            SignatureOUT_tb.Text = GetHexString(command_value, "OUT");
                                            break;
                                        case "OffsetIN":
                                            OffsetIN_tb.Text = GetHexString(command_value);
                                            break;
                                        case "OffsetOUT":
                                            OffsetOUT_tb.Text = GetHexString(command_value);
                                            break;
                                            /* *   case "OpenFileHistory": // делим строку на параметры по запятой
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
             //if (TextFile == "" || TextFile == null) LastRecordNumber = 1;
            if (BinaryFile != "" && BinaryFile != null)
            {
                OffsetIN_tb.Text = OffsetIN.ToString();
                OffsetIN_tb.Text = OffsetOUT.ToString();
                SearchBinaryBtn_Click(null, null); // открываем binary файл
                if (LastRecordNumber <= nudRecord.Maximum) nudRecord.Value = LastRecordNumber;
            }

            if (TextFile != "" && TextFile != null)
            {
                flag_Skipdialog = true; OpenTextFile(); mess_tb.Visible = false;
                if (LastRecordNumber != 1 && nudRecord.Value == 1)
                {
                    //nudRecord.Maximum = linkedListSF.Count;
                    if (LastRecordNumber <= nudRecord.Maximum) nudRecord.Value = LastRecordNumber;

                    //nudRecord_ValueChanged(this,null);
                }
            }

            if (comboBox1.Text == null) comboBox1.Text = "en";
            if (comboBox1.Text == null) comboBox1.Text = "ru";
        }

        public string GetHexString(string value, string OP = "")
        { //второй параметр для копирования данных во внешнюю переменную
            if (value == null || value == "") return null;
            byte[] Signature;
            value = value.ToUpper();
            value = Regex.Replace(value, @"[^0123456789ABCDEF]", "");
            int len = value.Length;
            if (len > 0 && len % 2 == 1)
                value = value.Substring(0, len - 1);
            Signature = Enumerable.Range(0, value.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(value.Substring(x, 2), 16))
                .ToArray();
            if (OP == "IN") SignatureIN = Signature;
            else if (OP == "OUT") SignatureOUT = Signature;
            return BitConverter.ToString(Signature);
        }
        public byte[] ConvertStringToByteArray(string value)
        {
            if (value == null || value == "") return null;

            return System.Text.Encoding.ASCII.GetBytes(value);
        }
        private long ConvertHEX2Long(String value)
        {
            if (value == null || value == "") return 0;
            value = value.ToUpper();
            value = Regex.Replace(value, @"[^0123456789ABCDEF]", "");
            return Int64.Parse(value, System.Globalization.NumberStyles.HexNumber);
        }

        private string HexStringToString(string hexString)
        {
            if (hexString == null || hexString == "")
            {
                //throw new ArgumentException();
                return "";
            }
            hexString = hexString.ToUpper();
            hexString = Regex.Replace(hexString, @"[^0123456789ABCDEF]", "");
            if ((hexString.Length & 1) == 1) hexString = hexString.Substring(0, hexString.Length - 1);
            var sb = new StringBuilder();
            for (var i = 0; i < hexString.Length; i += 2)
            {
                var hexChar = hexString.Substring(i, 2);
                sb.Append((char)Convert.ToByte(hexChar, 16));
            }
            return sb.ToString();
        }
        public string StringToHexString(string value)
        {
            if (value == null || value == "") return value;
            StringBuilder sb = new StringBuilder();
            foreach (char c in value)
                sb.AppendFormat("{0:X2}", (int)c);
            return sb.ToString().Trim();
        }

        public static string CreateMD5(string input) //расчет Хэша по строке
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes); // .NET 5 +
            }
        }
        private void WriteINI(string tofile)
        {

            using (StreamWriter writer = new StreamWriter(File.Open(tofile, FileMode.Create)))
            {
                writer.WriteLine("[Last session saved state]");
                writer.WriteLine("[Common section]");
                writer.WriteLine("InterfaceLanguage=" + InterfaceLanguage);
                writer.WriteLine("SourceLanguage=" + languageIN);
                writer.WriteLine("TranslationLanguage=" + languageOUT);
                writer.WriteLine("LastRecordNumber=" + Convert.ToString(nudRecord.Value));
                if (TextFile != "" || TextFile != null)
                {
                    writer.WriteLine("[Additional text file section]");
                    writer.WriteLine("TextFile=" + TextFile);
                }
                if (BinaryFile != "" || BinaryFile != null)
                {
                    writer.WriteLine("[Binary file Section]");
                    writer.WriteLine("BinaryFile=" + BinaryFile);
                    writer.WriteLine("SignatureIN=" + SignatureIN_tb.Text);
                    writer.WriteLine("SignatureOUT=" + SignatureOUT_tb.Text);
                    writer.WriteLine("OffsetIN=" + OffsetIN_tb.Text);
                    writer.WriteLine("OffsetOUT=" + OffsetOUT_tb.Text);
                }
            }
        }
        private void Save_INI()
        {
            //сохранять нужно сразу два ini файла
            //1ый - один ini с именем  файла(с которым велась работа) в его же каталоге
            //2ой - второй ini с именем программы в рабочей папкепрограммы , как последний редактируемый файл
            if (IniFile != "") WriteINI(IniFile);
            if (IniFileSpec != "") WriteINI(IniFileSpec);

        }
        private void About_tsmi_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Resources.aboutProgram, @Resources.aboutProgramTitle,
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void SearchBinaryBtn_fail()
        {            //заблокируем поля Смещения, Поиска Сигнатуры и Поиска строк текста во входном и выходном файлах
            SignatureIN_tb.ReadOnly = true; // textbox Offset
            SignatureOUT_tb.ReadOnly = true; // texbox Signature
            OffsetIN_tb.ReadOnly = true;
            OffsetOUT_tb.ReadOnly = true;
            Search_tstb.ReadOnly = true;
            Start_btn.Visible = true;
            MessageBox.Show("Can't operate with this file", "Caution", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        private void SearchBinaryBtn_Click(object sender, EventArgs e)
        {
            //if (Signature_tb.TextLength == 0) { MessageBox.Show("You must setup Signature to start."); return; }
            // начиная со смещения Offset начнем по байтам искать сигнатуру Signature.
            // Если найдем, то будем добавлять текст в список DoubleNode. Строим списки параллельно у обоих файлов.
            // создаем объект BinaryReader
            if (BinaryFile == "" || BinaryFile == null) return; // Binary файл еще не открыт
            Start_btn.Visible = false;

            DoublyLinkedList<string> LList;
            int LLRang = linkedList.Length;
            if (linkedList[0].Count == 0) LList = linkedList[0];
            else
            {
                // Сообщение об ошибке. Первым должен быть открыт бинарный файл. Закройте все открытые файлы и повторите попытку
                return;
            }
            using (BinaryReader readerSF = new BinaryReader(File.Open(BinaryFile, FileMode.Open)))
            { // откроем файл Source на чтение
                FileInfo src = new FileInfo(BinaryFile);
                byte[] buf = new byte[MaxBytesMessage * 3]; //Буффер для чтения из файла строки текста
                long l = src.Length; //размер исходного файла в байтах
                int sign_pointer = 0; // 0-сигнатура еще не встречена, 1..N - номер символа в сигнатуре
                long onepercent = l / 100 - 1, percent = onepercent;
                byte b;

                SetSignature(); // получаем сигнатуры из форм текстбоксов

                if ((SignatureIN == null) ||
                    (SignatureIN == null && SignatureOUT == null)) { SearchBinaryBtn_fail(); return; }

                int signatureInLength;
                int signatureOutLength;
                if (SignatureOUT == null) signatureOutLength = 0;
                else signatureOutLength = SignatureOUT.Length;

                if (SignatureIN.Length <= 0) { SearchBinaryBtn_fail(); return; }
                else signatureInLength = SignatureIN.Length;

                // создаем буфер для подгрузки сигнатуры из файла. Размер берем наибольший из двух сигнатур
                //byte[] sigbuf = new byte[signatureInLength>signatureOutLength?signatureInLength:signatureOutLength];
                byte[] sigbufIn = new byte[signatureInLength];
                byte[] sigbufOut = new byte[signatureOutLength];
                //  bool iWaitLinkedListSF = false; // флаг ожидания текста перевода

                //проверим первые символы из файла, не сигнатура ли?
                if (l <= signatureInLength + 4) { SearchBinaryBtn_fail(); return; }// файл слишком мал для работы

                for (long i = 0; i < l; i++) //читаем файл
                {
                    b = readerSF.ReadByte(); //читаем 1 байт
                    //двигаем очередь FIFO буфера входной сигнатуры
                    for (int ii = 1; ii < signatureInLength; ii++) sigbufIn[ii - 1] = sigbufIn[ii];
                    sigbufIn[signatureInLength - 1] = b;
                    //двигаем очередь FIFO буфера вЫходной сигнатуры
                    if (signatureOutLength > 0)
                    {
                        for (int ii = 1; ii < signatureOutLength; ii++) sigbufOut[ii - 1] = sigbufOut[ii];
                        sigbufOut[signatureOutLength - 1] = b;
                    }

                    bool SignatureInFlag = sigbufIn.SequenceEqual(SignatureIN); /*флаг совпадения с сигнатурой IN*/
                    bool SignatureOutFlag;
                    if (signatureOutLength > 0) SignatureOutFlag = sigbufOut.SequenceEqual(SignatureOUT); else SignatureOutFlag = false; // out
                    if (SignatureInFlag || SignatureOutFlag)// !СОВПАЛО! сравниваем сигнатуру через lynq
                    {
                        Int32 lentxt;
                        if (OffsetModeInt32_rb.Checked == true)
                        {
                            // пропускаем смещение
                            if (SignatureInFlag && i + OffsetIN < l)
                            {
                                for (int ii = 0; ii < OffsetIN; ii++) readerSF.ReadByte(); //читаем по 1 байту
                                i += OffsetIN;
                            }
                            else
                            if (SignatureOutFlag && i + OffsetOUT < l)
                            {
                                for (int ii = 0; ii < OffsetOUT; ii++) readerSF.ReadByte(); //читаем по 1 байту
                                i += OffsetOUT;
                            }
                            lentxt = readerSF.ReadInt32(); // читаем число int - 4 байта -длина текстовых данных                                                          //
                        }
                        else { if (i + 512 < l) lentxt = 512; else lentxt = (int)(l - i); }
                        i += 4;
                        if ((i + lentxt) >= l || lentxt < 0) continue; // сигнатура ошибочна, размер сообщения превышает размер файла

                        if ((lentxt > MaxBytesMessage) || lentxt < 3) // не может быть такое длинное/короткое предложение
                        {
                            for (int ii = 0; ii < lentxt; ii++) readerSF.ReadByte(); //читаем в никуда, так-как эти даные не нужны
                            i += lentxt;
                            continue;
                        }

                        long message_position_in_file = i; // смещение начала_сообщения относительно начала файла
                        byte[] tmp_bytes = new byte[lentxt]; //буфер для сообщения
                        for (int j = 0; j < lentxt; j++)
                        {//читаем lentxt байт сообщения в буфер
                            tmp_bytes[j] = readerSF.ReadByte(); i++;
                        }
                        // преобразованиЕ массива byte в строку string
                        string message = Encoding.UTF8.GetString(tmp_bytes);

                        if (SignatureInFlag)// создаем новый элемент списка, с файловым указателем на начало строки
                        {   // встретилась сигнатура1
                            LList.AddInPart(message, message_position_in_file, lentxt);  //создадим новый узел списка
                            /* buf[lentxt] = 0x3d;  // добавляем к концу строки "=0xd0xa"
                              writer.Write(buf, 0, lentxt); // записываем строку текста в выходной файл*/
                        }
                        else if (SignatureOutFlag)// встретилась сигнатура2
                        {//создаем список с переводом
                            LList.AddOutPart(message, message_position_in_file, lentxt);
                            //writer.Write(buf, 0, lentxt - 1); // записываем строку текста в выходной файл
                        }
                    }
                    if (i >= percent) // проверяем нужно ли двигать прогресс бар на 1%
                    {
                        if (progressBar1.Value < 100) { progressBar1.Value++; }
                        percent += onepercent; progressBar1_lb.Text = Convert.ToString(progressBar1.Value) + "%";
                    }
                }

                nudRecord.Maximum = LList.Count;
                if (LList.Count == 0) BinaryFile = null;
                else { IniFileSpec = BinaryFile; IniFileSpec += "ini"; }
                nudRecord.Minimum = 1;

                Records_lb.Text = "Found " + LList.Count + " records.";
                //Translated_tb.ReadOnly = false;
                nudRecord.Value = 1;
                nudRecord.ReadOnly = false;
            }

            NewTab_Click(null, null); //пытаемся открыть основную вкладку HOME
                                      //progressBar1.Value = 0;  // убeрем зеленый цвет закраса прогресбара
        }

        private void Next_btn_Click(object sender, EventArgs e)
        {
            if (Tabs == null) return;
            if (linkedList[0].Count == 0) return; // список пуст перемещение вперед невозможно

            SplitContainer sc = (SplitContainer)Tabs.SelectedTab.Tag;
            SearchTabs tabSearch = (SearchTabs)sc.Tag;

            tabSearch.Next(); nudRecord.Value = tabSearch.linkedListSS.curr.Twin.N_Record;
        }
        private void Prev_btn_Click(object sender, EventArgs e)
        {
            if (Tabs == null) return;
            if (linkedList[0].Count == 0) return; // список пуст перемещение назад невозможно

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

            if (nudRecord.Value != tabSearch.linkedListSS.curr.Twin.N_Record) //Если nudRecord не совпал с номером
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
            public int currentTabS = 0; // номер текущей вкладки в окне с Source
            */
            string str;
            if (Search_ts.Visible == false)
            {
                Search_ts.Visible = true;
                Search_tstb.ReadOnly = false;
                Search_tstb.Text = "";
                mess_tb.Visible = false;
            }
            str = Search_tstb.Text; //строка поиска
            SearchTabs NewTab = new();
            TabPage newTabPage = new();


            if (Tabs == null) // делаем главную вкладку HOME
            {
                if (linkedList.Count() == 0) return; // нет смысла открывать вкладку, список пуст
                NewTab.SetlinkedListHome(linkedList[0]);
                Tabs = new();
                Tabs.Name = "Tabs";
                Tabs.Size = new Size(985, 550);
                //Tabs.Size = new Size(this.Width, this.Height/4*3);
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
                Search_ts.Visible = true;
                statusStrip1.Visible = true;
                statusStrip2.Visible = true;

            }
            else// открывается точно не главная вкладка
            {
                if (Search_tstb.Text.Length == 0) { NewTab.Clear(); newTabPage.Dispose(); return; } //пустая строка поиска 
                NewTab.SearchInTab(linkedList[0], str); //ищем строку str в списке SF
                if (NewTab.Count() == 0) { NewTab.Clear(); newTabPage.Dispose(); return; } // поиск ничего не дал }
                // Формируем связи новой вкладки с соседними, т.е. с родительской
                NewTab.PrevTabPage = Tabs.SelectedTab; // сохраняем в новой вкладке указатель на родительскую вкладку 
                SplitContainer sc = (SplitContainer)Tabs.SelectedTab.Tag;
                SearchTabs tabSearch = (SearchTabs)sc.Tag; //извлекаем класс данных текущей вкладки
                tabSearch.NextTabPage = newTabPage; // запоминаем в родительской вкладке указатель на новую вкладку
                //Даем имя новой вкладке
                int len = str.Length < 50 ? str.Length : 50;
                newTabPage.Text = str.Substring(0, len);
                Tabs.TabPages.Add(newTabPage); //добавим новую вкладку на закладки
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
            newTranslated_tb.Width = 488; newTranslated_tb.Height = 475;
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
            newSplitContainer.Name = "splitContainer" + Convert.ToString(currentTabS);
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
            newSplitContainer.Tag = (object)NewTab;  //сохраним класс поиска в закладку

            newTabPage.Controls.Add(newSplitContainer);
            newTabPage.Controls.Add(Search_ts);

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
            SplitContainer sc = (SplitContainer)Tabs.SelectedTab.Tag;
            if (sc == null) return;
            SearchTabs tabSearch = (SearchTabs)sc.Tag;

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
            SearchTabs tabSearch = (SearchTabs)sc.Tag; // класс-данных закрываемой вкладки
            TabPage temptab;
            if (tabSearch == null) return; //вкладок вообще нет
            if (Tabs.SelectedTab == Home)  // закрываем вкладку HOME - 
            {   //открепляем от вкладок и прячем эл-ты управления
                Search_tstb.Text = ""; base.Controls.Add(Search_ts); Search_ts.Visible = false;
                this.Controls.Add(statusStrip1); statusStrip1.Visible = false;
                this.Controls.Add(statusStrip2); statusStrip2.Visible = false;
                Home.Dispose();
                tabSearch.Clear(); Home = null;
            }
            else // закрываем вкладку поиска
            {
                SearchTabs tabSearch_prev = null, tabSearch_next = null;
                temptab = Tabs.SelectedTab; //удаляемая TabPage
                // извлечем классы данных возможных родителькой и дочерней вкладок
                if (tabSearch.PrevTabPage != null)
                {
                    SplitContainer sc1 = (SplitContainer)tabSearch.PrevTabPage.Tag;
                    tabSearch_prev = (SearchTabs)sc1.Tag; // класс-данных родительской вкладки
                }
                if (tabSearch.NextTabPage != null)
                {
                    SplitContainer sc1 = (SplitContainer)tabSearch.NextTabPage.Tag;
                    tabSearch_next = (SearchTabs)sc1.Tag; // класс-данных дочерней вкладки
                }
                if (tabSearch.PrevTabPage != null)
                {
                    tabSearch_prev.NextTabPage = tabSearch.NextTabPage;
                }
                if (tabSearch.NextTabPage != null)
                {
                    tabSearch_next.PrevTabPage = tabSearch.PrevTabPage;
                }
                if (tabSearch.PrevTabPage != null) { Tabs.SelectedTab = tabSearch.PrevTabPage; }//переход на родительскую вкладку
                else { Tabs.SelectedTab = Home; }//или на главную, если родительская была удалена


                //очищаем класс данных по удаляемой вкладке
                tabSearch.PrevTabPage = null; tabSearch.NextTabPage = null;
                tabSearch.TabPage = null; tabSearch.Clear();
                // Переносим все нужные контролы на сплит-контейнер вкладки, на которую произошел переход
                Tabs.SelectedTab.Controls.Add(Search_ts); // Панель поиска
                sc = (SplitContainer)Tabs.SelectedTab.Tag;
                sc.Panel1.Controls.Add(statusStrip2); //панель статуса слева
                sc.Panel2.Controls.Add(statusStrip1); //панель статуса справа
                Refresh_Search_ts(Tabs.SelectedTab); // отображаем набор функционала на панели в соответствии
                // с тем открыта ли вкладка поиска или Home
                temptab.Dispose(); // ликвидируем сам удаляемый Таб
            }
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
                data = linkedList[0].curr.Data;
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
                 MessageBoxDefaultButton.Button1);
            if (result == DialogResult.No) { return; }// перевод не меняем
            else // Пользователь подтвердил удаление
            {
                flag_NotSavedYet = true;
                //удаляем элемент из основного списка SF на который ссылается элемент из списка поиска SS
                linkedList[0].DeleteNode(tabSearch.linkedListSS.curr.Twin);// вроде без Twin надо
                tabSearch.Remove(); //удаляем текущий элемент из списка поиска SS
                //обновляем вкладку для актуализации видимой инфы
                if (tabSearch.linkedListSS.Count == 0) TabClose_tsb_Click(null, null);
                else Tabs_Selecting(null, null);
            }
        }

        private void CloseFilesClear_Click(object sender, EventArgs e)
        {
            Quit_tsmi_Click(null, null); //очистим списки
            nudRecord.ReadOnly = true; // устанавливаем номер записи в 1
            nudRecord.Increment = 0;
            nudRecord.BackColor = Color.LightGray;
            nudRecord.Value = 1;
            Records_lb.Text = "Found: 0 records";


            TranslatedFile_tb.Text = ""; TextFile = "";
            SourceFile_tb.Text = ""; BinaryFile = "";
            //выведем элементы интерфейса за пределы закрываемого окна
            //закроем все вкладки
            while (Tabs != null) TabClose_tsb_Click(null, null); // список пуст-закроем вкладку

            lbSource.Text = "";
            lbTranslated.Text = "";
            SignatureIN_tb.Text = ""; SignatureIN_tb.ReadOnly = true;
            SignatureOUT_tb.Text = ""; SignatureOUT_tb.ReadOnly = true;
            //Search_tstb.Text = ""; 
            //Search_tstb.ReadOnly = true; 
            flag_NotSavedYet = false; // флаг -сохранение не требуется
            flag_Skipdialog = false; //по-умолчанию - не пропускать диалоги
            progressBar1.Value = 0; progressBar1_lb.Text = "%";
            mess_tb.Visible = true; // восстановим текстовое окно с дисклеймером
            // УДАЛИМ INI-ФАЙЛ
            if (File.Exists(IniFile)) File.Delete(IniFile);

        }

        private void Translate_btn_Click(object sender, EventArgs e)
        {
            if (Tabs == null) return; // кнопка нажата, а окна еще не созданы
            SplitContainer sc = (SplitContainer)Tabs.SelectedTab.Tag;
            if (sc == null) return;
            SearchTabs tabSearch = (SearchTabs)sc.Tag;

            tabSearch.tabTranslated_tb.Text += Translate_Google(tabSearch.tabSource_tb.Text);
            tabSearch.Translated_KeyUp();
        }
        private string Translate_Google(string source)
        {
            string mathod = "GET";
            string userAgent = "Mozilla/5.0 (Windows NT 10.0; rv:91.0) Gecko/20100101 Firefox/91.0";
            string urlFormat = "https://translate.googleapis.com/translate_a/single?client=gtx&sl={0}&tl={1}&dt=t&q={2}";
            //string languageIN = "en";// обьявлены глобально в классе F0rm1
            //string languageOUT = "ru";
            string text = source;
            string url = string.Format(urlFormat, languageIN, languageOUT, Uri.EscapeUriString(text));
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = mathod;
            request.UserAgent = userAgent;
            string response = new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd();
            // Parse json data
            return Parse_Google_JSON(response);
        }
        private string Parse_Google_JSON(string str) //распарсиваем ответ Google в JSON
        {
            int openbr = 0;   //счетчик скобок []
            int openbrTR = 0; // номер скобы перед переводом
            int openbrTR1st = 0; //количество открытых скоб перед первой строкой
            int startST = 0; //позиция начала подстроки с текстом перевода
            string result = string.Empty; // строка с переводом :)
            bool flag_sent = false; //флаг пропуска всех символов кроме [ ]
            bool flag_begin = false;//Флаг того , что перевод при разборе еще не встречался
            int len = str.Length; //длина сообщения от GOOGLE
            for (int i = 0; i < len; i++)
            {
                if (str[i] == '[') { openbr++; continue; }
                if (str[i] == ']')
                {
                    openbr--;
                    if (openbr < openbrTR) { flag_sent = false; }
                    if (openbr < openbrTR1st) flag_sent = true;
                    continue;
                }
                if (flag_sent == true) continue;
                if (str[i] == '\"' && str[i - 1] == '[')
                {
                    if (flag_begin == false) { flag_begin = true; openbrTR1st = openbr - 1; }
                    startST = i + 1; openbrTR = openbr;
                    continue;
                }
                if (str[i] == '\"' && str[i + 1] == ',' && (i + 1) < len)
                {
                    result += str[startST..i];
                    flag_sent = true;
                }
            }
            return (result);
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
            languageOUT = (string)comboBox1.SelectedValue;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            languageOUT = (string)comboBox2.SelectedValue;
        }
        private void SourceSearch_tstb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // символЫ Delete и BackSpace
            {
                if (sender == Search_tstb) NewTab_Click(sender, e);
            }
        }

        private void SaveState(DoublyLinkedList<string> linkedList, string str)
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
            /*else if (e.Control && e.KeyCode == Keys.Z)
            { UNDO_textbox(); }/*
            else if ((e.Control & e.Shift) == Keys.V)
            {
                MessageBox.Show("Ctrl+shift+V detected");
            }*/
        }
        /*      private void UNDO_textbox()
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


              private void UNDO_Click(object sender, EventArgs e)
              {
                  if (Tabs == null) return;
                  UNDO_textbox();
              }
              */
        private void Signature_tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (SignatureModeHEX_rb.Checked == false) return; //установлен режим ввода HEX

            if (e.KeyChar == 'a') e.KeyChar = 'A';
            else if (e.KeyChar == 'b') e.KeyChar = 'B';
            else if (e.KeyChar == 'c') e.KeyChar = 'C';
            else if (e.KeyChar == 'd') e.KeyChar = 'D';
            else if (e.KeyChar == 'e') e.KeyChar = 'E';
            else if (e.KeyChar == 'f') e.KeyChar = 'F';
            if (!char.IsNumber(e.KeyChar)
                && e.KeyChar != 'A'
                && e.KeyChar != 'B'
                && e.KeyChar != 'C'
                && e.KeyChar != 'D'
                && e.KeyChar != 'E'
                && e.KeyChar != 'F'
                && e.KeyChar != '\b')
                e.Handled = true;

        }

        private void Signature_tb_TextChanged(object sender, EventArgs e)
        {
            if (SignatureModeHEX_rb.Checked == false) // вводиться строка
            {
                return;
            }

            // вводиться HEX:

            TextBox tb = (TextBox)sender;// as TextBox;
            string current_text = tb.Text;
            current_text = Regex.Replace(current_text, @"[^0123456789ABCDEF]", "");
            int len = current_text.Length;

            string new_text = "";
            if (len > 2 && len < 14)
            {
                for (var i = 0; i < len; i += 2)
                {
                    if (len - 1 - i > 0)
                    {
                        new_text += current_text.Substring(i, 2);
                        if (i < len - 2) new_text += "-";
                    }
                }

                if (len % 2 != 0) { new_text += current_text.Substring(len - 1, 1); }
            }
            else { return; }


            tb.Text = new_text;
            tb.SelectionStart = tb.Text.Length;
            tb.SelectionLength = 0;
        }

        private void SignatureModeString_rb_CheckedChanged(object sender, EventArgs e)
        { //  радиокнопка бинарный режим поиск по строке
            SignatureIN_tb.PlaceholderText = "Text";
            SignatureOUT_tb.PlaceholderText = "Text";
            SignatureIN_tb.Text = HexStringToString(SignatureIN_tb.Text);
            SignatureOUT_tb.Text = HexStringToString(SignatureOUT_tb.Text);
            //Signature_tb_TextChanged(sender, e); //для верного отображения текста
            OffsetMode512_rb.Checked = true;

        }

        private void SignatureModeHEX_rb_CheckedChanged(object sender, EventArgs e)
        {
            SignatureIN_tb.PlaceholderText = "HEX...";
            SignatureOUT_tb.PlaceholderText = "HEX...";
            SignatureIN_tb.Text = StringToHexString(SignatureIN_tb.Text);
            SignatureOUT_tb.Text = StringToHexString(SignatureOUT_tb.Text);
            //Signature_tb_TextChanged(sender, e);

        }
        private void SetSignature()
        {
            if (SignatureModeHEX_rb.Checked == true) // вводиться строка
            {
                GetHexString(SignatureIN_tb.Text, "IN"); // Запись сигнатур из HEX-строки в массив байт
                GetHexString(SignatureOUT_tb.Text, "OUT");
            }
            else
            {
                SignatureIN = ConvertStringToByteArray(SignatureIN_tb.Text);
                SignatureOUT = ConvertStringToByteArray(SignatureOUT_tb.Text);
            }
            if (OffsetIN_tb.Text == null || OffsetIN_tb.Text == "") OffsetIN = 0;
            else OffsetIN = ConvertHEX2Long(OffsetIN_tb.Text);
            if (OffsetOUT_tb.Text == null || OffsetOUT_tb.Text == "") OffsetOUT = 0;
            else OffsetOUT = ConvertHEX2Long(OffsetIN_tb.Text);

        }
    }

    public class DoublyNode<T>
    {
        public DoublyNode(T data, T data1)
        {                   //Класс DoubleNode является обобщенным, поэтому может хранить данные любого типа. 
            Data = data;    //Для хранения данных предназначено свойство Data.
            Data1 = data1;
        }
        public T Data { get; set; }
        public T Data1 { get; set; }
        public long Fileposition { get; set; } // позиция начала текста IN в файле
        public long Fileposition1 { get; set; }// позиция начала текста OUT в файле
        public int messageLength { get; set; }
        public int messageLength1 { get; set; }
        public DoublyNode<T> Previous { get; set; } // предыдущий узел
        public DoublyNode<T> Next { get; set; }     // следующий узел
        public DoublyNode<T> Twin { get; set; }     // ссылка на доп. перевод из txt файла
        public long N_Record { get; set; } // номер по-порядку 
                                           //     public DoublyLinkedList<T> UNDO = new(); // список с отменой
    }
    public class DoublyLinkedList<T> : IEnumerable<T>  // класс - двусвязный список
    {
        public DoublyNode<T> curr; //текущий элемент
        DoublyNode<T> head; // головной/первый элемент
        DoublyNode<T> tail; // последний/хвостовой элемент
        int count;  // количество элементов в списке

        public void Add(T data, T data1,
            long Fileposition = -1, long Fileposition1 = -1, int messlen = 0, int messlen1 = 0)// добавление элемента
        {
            DoublyNode<T> node = new DoublyNode<T>(data, data1);
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
        }
        public void AddInPart(T data, long Fileposition = -1, int messlen = 0)// добавление элемента
        {
            DoublyNode<T> node = new DoublyNode<T>(data, data);
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
            node.messageLength = messlen;

        }
        public void AddOutPart(T data1, long Fileposition1 = -1, int messlen1 = 0)//добавление перевода в текущий
        {
            DoublyNode<T> node = curr;
            ReplaceData(data1, node);
            node.Fileposition1 = Fileposition1;
            node.messageLength1 = messlen1;

        }

        public void AddFirst(T data, T data1)
        {
            DoublyNode<T> node = new DoublyNode<T>(data, data1); // добавить первым в список
            DoublyNode<T> temp = head;
            curr = node; // добавляемый элемент становится текущим
            node.Next = temp;
            head = node;
            if (count == 0) tail = head;
            else temp.Previous = node;
            count++;

        }
        public void ExpandData(long Fileposition, int originalMessageLength,
            long Fileposition1 = -1, int originalMessageLength1 = 0)
        {
            DoublyNode<T> node = curr;
            node.Fileposition = Fileposition;
            node.messageLength = originalMessageLength;
            node.Fileposition1 = Fileposition1;
            node.messageLength1 = originalMessageLength1;
        }

        public void AddBeforeCurrent(T data, T data1, long Fileposition = -1, int originalMessageLength = 0)
        {
            DoublyNode<T> node = new DoublyNode<T>(data, data1); // создаем элемент
            node.Previous = curr.Previous;
            if (curr != head) curr.Previous.Next = node; else head = node;
            node.Next = curr;
            curr.Previous = node;
            count++;
            node.Fileposition = Fileposition;
            node.messageLength = originalMessageLength;
        }


        // удаление по ссылке на элемент
        public DoublyNode<T> DeleteNode(DoublyNode<T> node1 = null) //null- УДАЛИТЬ текущий элемент списка или по ссылке
        {
            bool flag_isnodecurrent = true; //удаляется текущий элемент
            DoublyNode<T> tempcurr = curr, node = node1;
            //если node1 не передан,то удаляем текущий элемент,
            //иначе если переданный элемент не текущий, то в конце восстановим curr
            if (node == null) node = curr; else if (node != curr) flag_isnodecurrent = false;

            node.Twin = null; //ставим метку, что данный элемент ни на что не ссылается и его можно удалять
            if (node.Next != null) { curr = node.Next; node.Next.Previous = node.Previous; }
            else { tail = node.Previous; curr = node.Previous; }
            // если узел не первый
            if (node.Previous != null) { node.Previous.Next = node.Next; }
            else { head = node.Next; }
            if (count > 0) count--;
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
                if (current.Data.Equals(data) || current.Data1.Equals(data)) break;
                current = current.Next;
            }
            if (current == null) return false; // ничего не нашли и не удалили
            curr = current; // элемент становится текущим

            // если узел не последний
            if (current.Next != null) current.Next.Previous = current.Previous;
            else tail = current.Previous;// если последний, переустанавливаем tail

            // если узел не первый
            if (current.Previous != null) current.Previous.Next = current.Next;
            else head = current.Next;// если первый, переустанавливаем head

            count--;
            curr = temp;
            return true;
        }

        public int Count { get { return count; } }
        public long FilePosition { get { if (curr != null) return curr.Fileposition; else return -1; } }
        public DoublyNode<T> Twin { get { if (curr != null) return curr.Twin; else return head; } }

        public bool IsEmpty { get { return count == 0; } }
        public object CurrentInData { get { return curr.Data; } } //возвращает данные из текущего элемента списка поле data
        public object CurrentOutData { get { return curr.Data1; } } //возвращает данные из текущего элемента списка поле data1
        public object Curr { get { return curr; } } //возвращает указатель на текущий элемент списка
        public object GetTData(DoublyNode<T> Node) ////возвращает данные из произвольного элемента списка
        {
            if (Node != null) return Node.Data; else return null;
        }
        public object GetTData1(DoublyNode<T> Node) ////возвращает данные из произвольного элемента списка
        {
            if (Node != null) return Node.Data1; else return null;
        }
        public object TwinFrom(DoublyNode<T> Node) //возвращает указатель на Twin определенного элемента списка
        {
            if (Node != null) return Node.Twin; else return null;
        }

        public void ReplaceData(T data1, DoublyNode<T> directNode = null) // Заменяет поле данных текущего узла,
        {  // либо иного узла списка, ссылка на который указана в необязательном поле directNode
            if (directNode == null) directNode = curr;
            if (directNode != null)
            {
                /* if (directNode.UNDO.curr == null) directNode.UNDO.AddFirst(directNode.Data, directNode.Fileposition, directNode.messageLength);
                 else
                     if (!directNode.UNDO.curr.Data.Equals(data)) // Если данные изменились создадим эл-нт UNDO
                     directNode.UNDO.AddFirst(directNode.Data, directNode.Fileposition, directNode.messageLength);
                     if (directNode.UNDO.Count() > 100) //Обрезаем хвост UNDO - лимит не более 100 откатов
                     directNode.UNDO.tail = directNode.UNDO.tail.Previous;*/

                directNode.Data1 = data1;
                return;
            }
        }
        public void SetTwin(DoublyNode<T> twin)
        {
            //if (twin == null) return;
            if (curr == null) return;
            curr.Twin = twin;
        }

        public long GetCurrentNum()
        {
            DoublyNode<T> item = head, current = curr;
            long num = 0, num1 = 0; //
            if (item == null) return 0; //список пуст
            while (item != null)
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
            curr = head;
            return head;
        }
        public DoublyNode<T> GetTail()
        {
            curr = tail;
            return tail;
        }
    }

    public class SearchTabs
    {
        public TabPage TabPage; //вкладка связанная с данной копией класса
        public TabPage PrevTabPage = null; //родительская вкладка TabPage
        public TabPage NextTabPage = null; //ссылка на вкладку возможного потомка
        public DoublyLinkedList<string> linkedListSS = new(); //создaдим список с результатами поиска

        public TextBox tabSource_tb { get; set; }//поля для хранения текстбоксов на вкладках
        public TextBox tabTranslated_tb { get; set; }
        public SplitContainer splitContainer { get; set; }
        public ToolStripLabel tabSearchStat_tslb { get; set; }
        public ToolStripStatusLabel tabSource_lb { get; set; }//кол-во символов в сообщении текстбокса
        public ToolStripStatusLabel tabTranslated_lb { get; set; }//кол-во символов в сообщении текстбокса

        public int currnum = 1; //номер текущего элемента списка
        public bool TranslatedSource = false;

        public int Count() => linkedListSS.Count;
        public int curnum()
        {
            return currnum;
        }

        /*public SearchTabs() //конструктор
        { 
            linkedListSS=new(); //создaдим список с результатами поиска
        }*/

        public void SetlinkedListHome(DoublyLinkedList<string> linkedList)
        { //скопировали 
            //linkedListSS = linkedList; //вкладка HOME тупо ссылается  на список с данными
            linkedList.GetHead();
            while (linkedList.curr.Next != null)
            {
                linkedListSS.Add(null, null);
                linkedListSS.curr.Twin = linkedList.curr;
                linkedList.curr = linkedList.curr.Next;
            }
            linkedList.GetHead();
            linkedListSS.GetHead();
        }
        public void SearchInTab(DoublyLinkedList<string> linkedList, string str)
        {
            if (linkedList == null || str == "") return; //если передана пустая строка или непередан список
                                                         //Начинаем поиск подстроки по всем элементам списка linkList
            var temp = linkedList.curr;
            foreach (var item in linkedList)
            {
                if (item.Contains(str))// Вхождения из Data найдены
                {
                    linkedListSS.Add(null, null); //создаем в списке поиска новый элемент
                    linkedListSS.SetTwin(linkedList.curr); // помещаем в его поле Twin указатель на запись из списка SF
                }
                else if (linkedList.curr.Data1.Contains(str))// проверяем на вхождения из Data1
                {
                    linkedListSS.Add(null, null);
                    linkedListSS.SetTwin(linkedList.curr);
                }
            }
            linkedListSS.GetHead(); // ставим для SS - curr на head
            linkedList.curr = temp;
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
                    linkedListSS.ReplaceData(tabTranslated_tb.Text, linkedListSS.curr.Twin);
                    currnum++;
                }
                linkedListSS.curr = linkedListSS.curr.Next;
            }
            else // мы в конце списка. перемотаем на начало
            {
                currnum = 1;
                linkedListSS.GetHead();
            }
            RefreshCurrent();
            tabSearchStat_tslb.Text = Convert.ToString(currnum) + " of " + linkedListSS.Count;
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
                    linkedListSS.ReplaceData(tabTranslated_tb.Text, linkedListSS.curr.Twin);
                    currnum--;
                }
                linkedListSS.curr = linkedListSS.curr.Previous;
            }
            else// мы в начале списка. перемотаем в конец
            {
                currnum = linkedListSS.Count;
                linkedListSS.GetTail();
            }
            RefreshCurrent();
        }
        public void toFirst()
        {
            if (linkedListSS.curr == null || linkedListSS.Count <= 0) return; // проверим что список не пустой
            currnum = 1;
            linkedListSS.GetHead();
            RefreshCurrent();
        }
        public void toLast()
        {
            if (linkedListSS.curr == null || linkedListSS.Count <= 0) return; // проверим что список не пустой
            currnum = linkedListSS.Count;
            linkedListSS.GetTail();
            RefreshCurrent();
        }
        public void toDefined(long pos) //перемещение на заданную позицию
        {
            if (pos > linkedListSS.Count || pos <= 0) return; //запрос по перемещению на нереальную позицию
            currnum = 1;
            linkedListSS.GetHead();
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
            while (linkedListSS.curr.Twin == null)
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
                tabTranslated_tb.Text = linkedListSS.curr.Twin.Data1;
            }
            else
            {
                tabSource_tb.Text = linkedListSS.curr.Twin.Data;
                tabTranslated_tb.Text = linkedListSS.curr.Twin.Data1;
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
            if (linkedListSS.curr != null) // защищаемся от случайного пизд-ца с null
                if (linkedListSS.curr.Twin != null)
                    linkedListSS.ReplaceData(tabTranslated_tb.Text, linkedListSS.curr.Twin);
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
            if (tabSource_tb != null) { tabSource_tb.Dispose(); tabSource_tb = null; }
            if (tabTranslated_tb != null) { tabTranslated_tb.Dispose(); tabTranslated_tb = null; }
            if (linkedListSS != null) linkedListSS = null;
        }
    }
}


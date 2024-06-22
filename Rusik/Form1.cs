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
using System.Drawing.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using TextBox = System.Windows.Forms.TextBox;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Reflection;


namespace Rusik
{

    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            // вешаем обработчик закрытия окна по крестику
            this.FormClosing += new FormClosingEventHandler(this.Form1_FormClosing);
            // подключаем коллекцию со списком языков перевода к комбобоксу 
            comboBox1.DataSource = new BindingSource(CommonData.GoogleLangs, null);
            comboBox2.DataSource = new BindingSource(CommonData.GoogleLangs, null);
            comboBox1.DisplayMember = "Key"; comboBox2.DisplayMember = "Key";
            comboBox1.ValueMember = "Value"; comboBox2.ValueMember = "Value";

            string usage_message = @"
This program can be useful for unofficial localizations some programs or games.
The main goal of this program is searching and capturing text phrases (unicode UTF-8 supported) on any language in binary data files with user defined parameters (offset and starting signature).
After this capturing you can save and work with own project file, or/and import/export text files with parts of translation work.
Program have comfortable functional for editing, searching, navigating, Google-translating feature and even primary binary file UPDATING.

Author ask to all, Do Not Violance digital rights and licensies of 
any Companies, thier Products or Trademarks!
Peace to all.";
            CommonData.mess_tb = new();
            Font newFont = new Font(FontFamily.GenericMonospace, 14);
            CommonData.mess_tb.Location = new Point(16, 50);
            CommonData.mess_tb.Name = "UsageMessage";
            CommonData.mess_tb.Size = new Size(977, 500);
            CommonData.mess_tb.Font = newFont;
            CommonData.mess_tb.Text = usage_message;
            CommonData.mess_tb.Multiline = true;
            CommonData.mess_tb.ReadOnly = true;
            Controls.Add(CommonData.mess_tb);

            CommonData.progressBar1 = progressBar1;
            CommonData.PerformingFile_tb = PerformingFile_tb;
            CommonData.progressBar1_lb = progressBar1_lb;
            CommonData.comboBox1 = comboBox1;
            CommonData.comboBox2 = comboBox2;
            CommonData.nudRecord = nudRecord;
            CommonData.TranslatedFile_tb = TranslatedFile_tb;
            CommonData.Search_tstb = Search_tstb;
            CommonData.Records_lb = Records_lb;
            CommonData.Start_btn = Start_btn;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (FileWork.LoadINI())
            {
                CloseFilesClear_tsmi.Enabled = true;
                Start_btn.Visible = false;
            }
            NewTab_Click(null, null); //пытаемся открыть основную вкладку HOME;
            nudRecord_ValueChanged(null, null);
        }

        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Quit_tsmi_Click(sender, e);
        }

        private void Quit_tsmi_Click(object sender, EventArgs e) // МЕНЮ Quit
        {
            // если исходный файл открыт, то предлагаем сохранить выходной файл
            if (CommonData.linkedList.Curr != null) // если в памяти есть список - то предлагаем сохраниться
            {
                if (CommonData.flag_NotSavedYet == true)
                    do
                    {
                        DialogResult result = MessageBox.Show(
                        "Do yo want to Save file?\n" + CommonData.TranslatedFileST,
                        "Attention",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly);
                        if (result == DialogResult.No) { FileWork.SaveINI(); return; }// пользователь отказался от сохранения
                    } while (FileWork.ExportTextFileST() == false); // согласился сохраниться , но что-то пошло не так. Даем еще одну попытку...
                CommonData.linkedList.Clear();

            }
            FileWork.SaveINI();
        }
        private void Save_Click(object sender, EventArgs e)
        {
            //FileWork.ExportTextFileST();
            SaveProject_Click(sender, e);
        }

        private void ExportTextFileST_tsmi_Click(object sender, EventArgs e) // Меню Save File As
        {
            SaveFileDialog saveFileDialog1 = new();
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            saveFileDialog1.DefaultExt = "*.txt";
            saveFileDialog1.CreatePrompt = true;
            saveFileDialog1.OverwritePrompt = true; //предупреждение о перезаписи
            saveFileDialog1.ShowDialog();
            string tmpOutputFile = saveFileDialog1.FileName;
            if (tmpOutputFile == null || tmpOutputFile == "") return;
            else FileWork.ExportTextFileST(tmpOutputFile);
        }

        private void SaveProject_Click(object sender, EventArgs e)
        {
            /* SaveFileDialog saveFileDialog1 = new();
             saveFileDialog1.OverwritePrompt = true; //предупреждение о перезаписи
             saveFileDialog1.ShowDialog();*/

            if (CommonData.ProjectFile == "" || CommonData.ProjectFile == null)
            {
                if (CommonData.OriginalBinaryFile == "" || CommonData.OriginalBinaryFile == null)
                {
                    MessageBox.Show("Attention !",
                        "For Saving the file\n"+
                        "You must Open Binary Data file\n"+
                        "or save as project at first",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information, MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly);
                    return; 
                }
                else CommonData.ProjectFile = CommonData.OriginalBinaryFile + ".project";
            }
            
            FileWork.SaveProject();
        }

        private void OpenBinaryFile_tsmi_Click(object sender, EventArgs e) //MENU Open Binary File это только выбираем название бинарного файла 
        {

            do
            {
                OpenFileDialog openFileDialog1 = new();
                openFileDialog1.Filter = "All files(*.*)|*.*";
                openFileDialog1.ShowDialog();
                string OpenBinaryFile = string.Empty;
                OpenBinaryFile = openFileDialog1.FileName;
                if (OpenBinaryFile == "" || OpenBinaryFile == null) return; // не выбрано никакого файла
                else CommonData.OriginalBinaryFile = OpenBinaryFile;
            } while (!File.Exists(CommonData.OriginalBinaryFile));

            CloseFilesClear_tsmi.Enabled = true;

            //разблокируем поля Смещения, Поиска Сигнатуры и Поиска строк текста во входном и выходном файлах
            Offset_tb.ReadOnly = false; // textbox Offset
            Signature_tb.ReadOnly = false; // texbox Signature
            Search_tstb.ReadOnly = false;
            Start_btn.Visible = true;
        }

        private protected void OpenTranslatedFileST_tsmi_Click(object sender, EventArgs e)//MENU Open Text File
        {
            do
            {
                OpenFileDialog openFileDialog1 = new();
                openFileDialog1.ShowDialog();
                string OpenTextFile = string.Empty;
                OpenTextFile = openFileDialog1.FileName;
                if (OpenTextFile == "" || OpenTextFile == null) return;
                else CommonData.TranslatedFileST = OpenTextFile;
            } while (!File.Exists(CommonData.TranslatedFileST));
            /* 
                //создадим кнопку пропуска диалога
                SkipCheck_btn = new();
                SkipCheck_btn.Location = new Point(839, 678);
                SkipCheck_btn.Size = new Size(104, 23);
                SkipCheck_btn.Text = "SkipChecking";
                this.Controls.Add(SkipCheck_btn);
                SkipCheck_btn.Click += SkipCheck_btn_Click;*/

            if (FileWork.LoadTextTranslatedST())
            {
                NewTab_Click(null, null); //пытаемся открыть основную вкладку HOME
                Translated_tb_KeyUp(null, null); //обновляем число символов в переводе
                Records_lb.Text = "Found " + CommonData.linkedList.Count + " records.";
                // разблокируем строку поиска
                Search_tstb.ReadOnly = false;
                CloseFilesClear_tsmi.Enabled = true;
                TranslatedFile_tb.Text = CommonData.TranslatedFileST;
            }
            CloseFilesClear_tsmi.Enabled = true;


            //new System.Threading.Thread(() => OpenTranslatedFile()).Start(); //
            //SkipCheck_btn.Dispose(); //удалим кнопку
        }
        private void SkipCheck_btn_Click(object sender, EventArgs e)
        {
            CommonData.flag_Skipdialog = true; // пропустить диалоги и проверку на дубликаты строк в файле
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

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
        private void About_tsmi_Click(object sender, EventArgs e)
        {
            string str1 = @"
This program may be useful for changing and translating text
in some binary data files For example in Unity games.
Also, program can operate with text files that consist of two parts:
original sentence and translation sentense compared with symbol =";
            MessageBox.Show(str1, "About info ...", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Start_btn_Click(object sender, EventArgs e)
        {
            string SourceFile = CommonData.OriginalBinaryFile;
            int MaxBytesMessage = CommonData.MaxBytesMessage;
            byte[] Signature = CommonData.Signature;

            if (SourceFile == "" || SourceFile == null) return; // имя Binary файла еще не задано
            Start_btn.Visible = false; // выключаем кнопку СТАРТ
            PerformingFile_tb.Text = CommonData.OriginalBinaryFile; // выводим имя файла на панель прогрессбара
            if (FileWork.LoadBinary()) // бинарный файл успешно открыт и загружен
            {
                NewTab_Click(null, null);
                SaveProject_tsmi.Enabled = true; // разрешаем меню сохраненик проекта
                CloseFilesClear_tsmi.Enabled = true;
                ExportTextFileST_tsmi.Enabled = true;

                // выведем имя открытого файла на прогрессбаре загрузки

            }//пытаемся открыть основную вкладку HOME;
            else
            {
                Start_btn.Visible = true;
                progressBar1.Value = 0;
            }
        }

        private void Next_btn_Click(object sender, EventArgs e)
        {
            if (CommonData.Tabs == null) return;
            if (CommonData.linkedList.Count == 0) return; // список пуст перемещение вперед невозможно
                                                          // if (linkedListOF.curr == null  || linkedList.curr == null) return;

            SplitContainer sc = (SplitContainer)CommonData.Tabs.SelectedTab.Tag;
            SearchTabs tabSearch = (SearchTabs)sc.Tag;

            tabSearch.Next();
            nudRecord.Value = tabSearch.linkedListSS.curr.Twin.N_Record;
            nudRecord_ValueChanged(null, null);

        }

        private void Prev_btn_Click(object sender, EventArgs e)
        {
            if (CommonData.Tabs == null) return;
            if (CommonData.linkedList.Count == 0) return; // список пуст перемещение назад невозможно

            SplitContainer sc = (SplitContainer)CommonData.Tabs.SelectedTab.Tag;
            SearchTabs tabSearch = (SearchTabs)sc.Tag;

            tabSearch.Prev();
            nudRecord.Value = tabSearch.linkedListSS.curr.Twin.N_Record; //вкладки с поиском
            nudRecord_ValueChanged(null, null);
        }

        public void nudRecord_ValueChanged(object sender, EventArgs e)
        {
            if (nudRecord.ReadOnly == true) return; // это пришел афтершок из функций Next_btn_Click И Prev_btn_click
            if (CommonData.Tabs == null) return;
            SplitContainer sc = (SplitContainer)CommonData.Tabs.SelectedTab.Tag;
            SearchTabs tabSearch = (SearchTabs)sc.Tag;

            if (nudRecord.Value != tabSearch.linkedListSS.curr.Twin.N_Record) //Если nudRecord не совпал с номером
                tabSearch.toDefined((long)nudRecord.Value);//элемента списка, то двигаем curr на новый nudRecord
        }

        public void Refresh_Search_ts(TabPage ts)
        {
            if (ts == CommonData.Home)
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
        {   // поиск по тексту из входящего файла Открываем новую вкладку
            string str;
            if (Search_ts.Visible == false)
            {
                Search_ts.Visible = true;
                Search_tstb.ReadOnly = false;
                Search_tstb.Text = "";
                CommonData.mess_tb.Visible = false;
            }
            str = Search_tstb.Text; //строка поиска
            SearchTabs NewTab = new();
            TabPage newTabPage = new();

            if (CommonData.Tabs == null) // делаем главную вкладку HOME
            {
                if (CommonData.linkedList.Count() == 0) return; // нет смысла открывать вкладку, список пуст
                NewTab.SetSearchList(CommonData.linkedList, str, true); //домашняя вкладка
                CommonData.Tabs = new();
                CommonData.Tabs.Name = "Tabs";
                CommonData.Tabs.Size = new Size(985, 550);
                CommonData.Tabs.Location = new Point(12, 35);
                //Tabs.ItemSize = new Size(61, 20);
                CommonData.Tabs.SelectedIndex = 0;
                CommonData.Tabs.TabIndex = 40;
                this.Controls.Add(CommonData.Tabs);
                CommonData.Tabs.Selecting += new TabControlCancelEventHandler(Tabs_Selecting);

                CommonData.Home = newTabPage;

                CommonData.Home.Name = "Home";
                CommonData.Home.Text = "Home";
                /*Home.Size = new Size(900, 498);
                Home.TabIndex = 0;*/
                CommonData.Tabs.TabPages.Add(CommonData.Home); //добавим новую вкладку Home
                CommonData.Tabs.SelectedTab = CommonData.Home;
                CommonData.Home.Controls.Add(Search_ts);
                Search_ts.Dock = DockStyle.Top;
                //разблокируем элементы интерфейса
                Search_ts.Visible = true;
                statusStrip1.Visible = true;
                statusStrip2.Visible = true;

                SaveProject_tsmi.Enabled = true;
                ExportTextFileST_tsmi.Enabled = true;
            }
            else// открывается точно не главная вкладка
            {
                if (Search_tstb.Text.Length == 0) { NewTab.Clear(); newTabPage.Dispose(); return; } //пустая строка поиска 
                NewTab.SetSearchList(CommonData.linkedList, str); //ищем строку str в списке SF
                if (NewTab.Count() == 0) { NewTab.Clear(); newTabPage.Dispose(); return; } // поиск ничего не дал }
                // Формируем связи новой вкладки с соседними, т.е. с родительской
                NewTab.PrevTabPage = CommonData.Tabs.SelectedTab; // сохраняем в новой вкладке указатель на родительскую вкладку 
                SplitContainer sc = (SplitContainer)CommonData.Tabs.SelectedTab.Tag;
                SearchTabs tabSearch = (SearchTabs)sc.Tag; //извлекаем класс данных текущей вкладки
                tabSearch.NextTabPage = newTabPage; // запоминаем в родительской вкладке указатель на новую вкладку
                //Даем имя новой вкладке
                int len = str.Length < 50 ? str.Length : 50;
                newTabPage.Text = str.Substring(0, len);
                CommonData.Tabs.TabPages.Add(newTabPage); //добавим новую вкладку на закладки
                CommonData.Tabs.SelectedTab = newTabPage; //переключимся на новую вкладку
            }

            CommonData.currentTabS++;

            Font font = new Font("Segoe UI", 14.03f);//, FontStyle.Bold | FontStyle.Italic | FontStyle.Underline);
            TextBox newSource_tb = new();
            newSource_tb.Location = new Point(0, 58);
            newSource_tb.Width = 484; newSource_tb.Height = 475;
            newSource_tb.Font = font;
            newSource_tb.BackColor = SystemColors.GradientInactiveCaption;
            newSource_tb.Name = "newSource_tb" + Convert.ToString(CommonData.currentTabS);
            newSource_tb.Multiline = true;
            newSource_tb.ScrollBars = ScrollBars.Vertical;
            newSource_tb.KeyDown += Form1_KeyDown; // подключим горячие клавиши

            TextBox newTranslated_tb = new();
            newTranslated_tb.Location = new Point(500, 58);
            newTranslated_tb.Width = 488; newTranslated_tb.Height = 475;
            newTranslated_tb.Font = font;
            newTranslated_tb.BackColor = SystemColors.InactiveBorder;
            newTranslated_tb.Name = "newTranslated_tb" + Convert.ToString(CommonData.currentTabS);
            newTranslated_tb.Multiline = true;
            newTranslated_tb.ScrollBars = ScrollBars.Vertical;
            newTranslated_tb.KeyUp += Translated_tb_KeyUp;// ставим контрол на нажатие клавиш для отслеживания счетчика введенных символов
            newTranslated_tb.KeyDown += Form1_KeyDown; // подключим горячие клавиши
            NewTab.TabPage = newTabPage; // сохраним адрес Таба в экземпляре класса
            NewTab.tabSource_tb = newSource_tb;
            NewTab.tabTranslated_tb = newTranslated_tb;
            NewTab.tabSource_lb = lbSource;

            SplitContainer newSplitContainer = new();
            newSplitContainer.Location = new Point(0, 25);
            newSplitContainer.Name = "splitContainer" + Convert.ToString(CommonData.currentTabS);
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
            CommonData.Tabs.SelectedTab.Controls.Add(Search_ts);
            newSource_tb.Dock = DockStyle.Top;
            newTranslated_tb.Dock = DockStyle.Top;

            CommonData.Tabs.SelectedTab.Tag = newSplitContainer;
            newSplitContainer.Tag = (object)NewTab;  //сохраним класс поиска в закладку

            newTabPage.Controls.Add(newSplitContainer);
            newTabPage.Controls.Add(Search_ts);

            NewTab.tabTranslated_lb = lbTranslated;
            //обновляем визуальную информацию
            SearchStat_tslb.Text = "1 of " + Convert.ToString(NewTab.Count());
            NewTab.tabSearchStat_tslb = SearchStat_tslb;


            /*if (NewTab.linkedListSS.curr != null) // 17/06/2024  && NewTab.TabPage.Name=="Home"
            {
                /*if (CommonData.LastRecordNumber > NewTab.linkedListSS.curr.Twin.N_Record)
                    nudRecord.Value = NewTab.linkedListSS.curr.Twin.N_Record;
                else nudRecord.Value = CommonData.LastRecordNumber;
                nudRecord.Value = NewTab.linkedListSS.curr.Twin.N_Record;
            }
            else nudRecord.Value = CommonData.LastRecordNumber;*/

            nudRecord.Value = CommonData.LastRecordNumber;
            Refresh_Search_ts(CommonData.Tabs.SelectedTab);
            NewTab.RefreshCurrent(); nudRecord_ValueChanged(null, null);

        }

        private void Tabs_Selecting(object sender, TabControlCancelEventArgs e) // перетыкиваем вкладку мышью на панели Tabs
        {// если выбрана основная вкладка, то вынесем неперед основные текстбоксы
            if (CommonData.Tabs.SelectedTab == null) { CommonData.Tabs.Dispose(); CommonData.Tabs = null; return; }//нет открытых вкладок
            SplitContainer sc = (SplitContainer)CommonData.Tabs.SelectedTab.Tag;
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
            Refresh_Search_ts(CommonData.Tabs.SelectedTab);
            CommonData.Tabs.SelectedTab.Controls.Add(Search_ts);
            sc.Panel1.Controls.Add(statusStrip2);
            sc.Panel2.Controls.Add(statusStrip1);
        }
        private void Search_Next_btn_Click(object sender, EventArgs e)
        {
            SplitContainer sc = (SplitContainer)CommonData.Tabs.SelectedTab.Tag;
            SearchTabs tabSearch = (SearchTabs)sc.Tag;
            if (tabSearch == null) return;
            if (CommonData.Tabs.SelectedTab == CommonData.Home) return;
            tabSearch.Next();
            nudRecord.Value = tabSearch.linkedListSS.curr.Twin.N_Record;
            nudRecord_ValueChanged(null, null);
        }
        private void Search_Prev_btn_Click(object sender, EventArgs e)
        {
            SplitContainer sc = (SplitContainer)CommonData.Tabs.SelectedTab.Tag;
            SearchTabs tabSearch = (SearchTabs)sc.Tag;
            if (tabSearch == null) return;
            if (CommonData.Tabs.SelectedTab == CommonData.Home) return;
            tabSearch.Prev();
            nudRecord.Value = tabSearch.linkedListSS.curr.Twin.N_Record;
            nudRecord_ValueChanged(null, null);
        }
        private void SourceLast_tsb_Click(object sender, EventArgs e)
        {
            SplitContainer sc = (SplitContainer)CommonData.Tabs.SelectedTab.Tag;
            SearchTabs tabSearch = (SearchTabs)sc.Tag;
            if (tabSearch == null) return;
            if (CommonData.Tabs.SelectedTab == CommonData.Home) return;
            tabSearch.toLast();
        }
        private void SourceFirst_tsb_Click(object sender, EventArgs e)
        {
            SplitContainer sc = (SplitContainer)CommonData.Tabs.SelectedTab.Tag;
            SearchTabs tabSearch = (SearchTabs)sc.Tag;
            if (tabSearch == null) return;
            if (CommonData.Tabs.SelectedTab == CommonData.Home) return;
            tabSearch.toFirst();
        }
        private void TabClose_tsb_Click(object sender, EventArgs e)
        {
            SplitContainer sc = (SplitContainer)CommonData.Tabs.SelectedTab.Tag;
            SearchTabs tabSearch = (SearchTabs)sc.Tag; // класс-данных закрываемой вкладки
            TabPage temptab;
            if (tabSearch == null) return; //вкладок вообще нет
            if (CommonData.Tabs.SelectedTab == CommonData.Home)  // закрываем вкладку HOME - 
            {   //открепляем от вкладок и прячем эл-ты управления
                Search_tstb.Text = ""; Controls.Add(Search_ts); Search_ts.Visible = false;
                this.Controls.Add(statusStrip1); statusStrip1.Visible = false;
                this.Controls.Add(statusStrip2); statusStrip2.Visible = false;
                CommonData.Home.Dispose();
                tabSearch.Clear(); CommonData.Home = null;
            }
            else // закрываем вкладку поиска
            {
                SearchTabs tabSearch_prev = null, tabSearch_next = null;
                temptab = CommonData.Tabs.SelectedTab; //удаляемая TabPage
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
                if (tabSearch.PrevTabPage != null) { CommonData.Tabs.SelectedTab = tabSearch.PrevTabPage; }//переход на родительскую вкладку
                else { CommonData.Tabs.SelectedTab = CommonData.Home; }//или на главную, если родительская была удалена


                //очищаем класс данных по удаляемой вкладке
                tabSearch.PrevTabPage = null; tabSearch.NextTabPage = null;
                tabSearch.TabPage = null; tabSearch.Clear();
                // Переносим все нужные контролы на сплит-контейнер вкладки, на которую произошел переход
                CommonData.Tabs.SelectedTab.Controls.Add(Search_ts); // Панель поиска
                sc = (SplitContainer)CommonData.Tabs.SelectedTab.Tag;
                sc.Panel1.Controls.Add(statusStrip2); //панель статуса слева
                sc.Panel2.Controls.Add(statusStrip1); //панель статуса справа
                Refresh_Search_ts(CommonData.Tabs.SelectedTab); // отображаем набор функционала на панели в соответствии
                // с тем открыта ли вкладка поиска или Home
                temptab.Dispose(); // ликвидируем сам удаляемый Таб
            }
        }
        private void Delete_btn_Click(object sender, EventArgs e)
        {
            //long num;
            string data;
            if (CommonData.Tabs == null) return;
            SplitContainer sc = (SplitContainer)CommonData.Tabs.SelectedTab.Tag;
            SearchTabs tabSearch = (SearchTabs)sc.Tag;
            if (tabSearch == null) //определимся что будем удалять
            { // открыта вкладка HOME
                data = CommonData.linkedList.curr.DataTranslated;
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
                CommonData.flag_NotSavedYet = true;
                //удаляем элемент из  списка linkedList на который ссылается элемент из списка поиска SS
                CommonData.linkedList.DeleteNode(tabSearch.linkedListSS.curr.Twin);
                tabSearch.Remove(); //удаляем текущий элемент из списка поиска SS
                //обновляем вкладку для актуализации видимой инфы
                if (tabSearch.linkedListSS.Count == 0) TabClose_tsb_Click(null, null);
                else Tabs_Selecting(null, null);
            }
        }

        private void CloseFilesClear_Click(object sender, EventArgs e)
        {
            nudRecord.ReadOnly = true; // устанавливаем номер записи в 1
            nudRecord.Increment = 0;
            nudRecord.BackColor = Color.LightGray;
            nudRecord.Value = 1;
            Records_lb.Text = "Found: 0 records";

            TranslatedFile_tb.Text = ""; CommonData.TranslatedFileST = "";
            PerformingFile_tb.Text = ""; CommonData.OriginalBinaryFile = "";
            //выведем элементы интерфейса за пределы закрываемого окна
            //закроем все вкладки
            while (CommonData.Tabs != null) TabClose_tsb_Click(null, null); // список пуст-закроем вкладку

            lbSource.Text = "";
            lbTranslated.Text = "";
            Offset_tb.Text = ""; Offset_tb.ReadOnly = true;
            Signature_tb.Text = ""; Signature_tb.ReadOnly = true;
            //Search_tstb.Text = ""; 
            //Search_tstb.ReadOnly = true; 
            CommonData.flag_NotSavedYet = false; // флаг -сохранение не требуется
            CommonData.flag_Skipdialog = false; //по-умолчанию - не пропускать диалоги
            progressBar1.Value = 0; progressBar1_lb.Text = "%";
            CommonData.mess_tb.Visible = true; // восстановим текстовое окно с дисклеймером
            SaveProject_tsmi.Enabled = false;
            CloseFilesClear_tsmi.Enabled = false;
        }

        private void Translate_btn_Click(object sender, EventArgs e)
        {
            if (CommonData.Tabs == null) return; // кнопка нажата, а окна еще не созданы
            SplitContainer sc = (SplitContainer)CommonData.Tabs.SelectedTab.Tag;
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
            string url = string.Format(urlFormat, CommonData.languageIN, CommonData.languageOUT, Uri.EscapeUriString(text));
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
            if (CommonData.Tabs.SelectedTab == null) return;
            SplitContainer sc = (SplitContainer)CommonData.Tabs.SelectedTab.Tag;
            SearchTabs tabSearch = (SearchTabs)sc.Tag;
            tabSearch.Translated_KeyUp();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CommonData.languageOUT = (string)comboBox1.SelectedValue;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            CommonData.languageOUT = (string)comboBox2.SelectedValue;
        }
        private void SourceSearch_tstb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // символЫ Delete и BackSpace
            {
                if (sender == Search_tstb) NewTab_Click(sender, e);
            }
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
            else if (e.Control && e.KeyCode == Keys.S) { FileWork.ExportTextFileST(); }
            else if (e.Control && e.KeyCode == Keys.Z) { UNDO_textbox(); }
            /*
            else if ((e.Control & e.Shift) == Keys.V)
            {
                MessageBox.Show("Ctrl+shift+V detected");
            }*/
        }
        private void UNDO_textbox()
        {
            if (CommonData.Tabs == null) return;
            if (CommonData.Tabs.SelectedTab == null) return; // нет открытых файлов
            SplitContainer sc = (SplitContainer)CommonData.Tabs.SelectedTab.Tag;
            SearchTabs tabSearch = (SearchTabs)sc.Tag;
            if (tabSearch == null) // вызов с главного TABa
            {
                if (CommonData.linkedList.curr.UNDO.curr != null)
                {
                    CommonData.linkedList.curr.DataTranslated = CommonData.linkedList.curr.UNDO.curr.DataTranslated;
                    CommonData.linkedList.curr.UNDO.NextNode();
                    nudRecord_ValueChanged(null, null);
                }
            }
            else //UNDO на вкладке поиска 
            {
                if (tabSearch.linkedListSS.curr.UNDO.curr != null)
                {
                    tabSearch.linkedListSS.curr.DataTranslated = tabSearch.linkedListSS.curr.UNDO.curr.DataTranslated;
                    tabSearch.linkedListSS.curr.UNDO.NextNode();
                    tabSearch.RefreshCurrent();
                }
            }
        }

        private void UNDO_Click(object sender, EventArgs e)
        {
            /* if (CommonData.Tabs == null) return;
             UNDO_textbox();*/
        }




        private void ImportText_Click(object sender, EventArgs e)
        {

        }

        private void File_tsmi_Click(object sender, EventArgs e)
        {

        }
    }

    public class DoublyNode<T>
    {
        public DoublyNode(T data1, T data2)
        {                   //Класс DoubleNode является обобщенным, поэтому может хранить данные любого типа. 
            DataOriginal = data1;    //Для хранения данных предназначено свойство Data.
            DataTranslated = data2;
        }
        public T DataOriginal { get; set; }
        public T DataTranslated { get; set; }
        public long Fileposition { get; set; }
        public DoublyNode<T> Previous { get; set; } // предыдущий узел
        public DoublyNode<T> Next { get; set; }     // следующий узел
        public DoublyNode<T> Twin { get; set; }     // ссылка на связанный элемент из оригинального/переведенного списка
        public long N_Record { get; set; } // номер по-порядку 
        public object OriginalData { get { return DataOriginal; } }//internal set; }
        public object TranslatedData { get; internal set; }

        public DoublyLinkedList<T> UNDO = new(); // список с историей элемента UNDO
    }
    public class DoublyLinkedList<T> : IEnumerable<T>  // класс - двусвязный список
    {
        public DoublyNode<T> curr; //текущий элемент
        DoublyNode<T> head; // головной/первый элемент
        DoublyNode<T> tail; // последний/хвостовой элемент
        int count = 0;  // количество элементов в списке
        public DoublyNode<T> Head()
        { return head; }

        public void Add(T data1, T data2, long Fileposition)// добавление элемента
        {
            DoublyNode<T> node = new DoublyNode<T>(data1, data2);
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

        public DoublyNode<T> DeleteNode(DoublyNode<T> node1 = null) //null- УДАЛИТЬ текущий элемент списка или по ссылке
        {
            //           bool flag_isnodecurrent= true; //удаляется текущий элемент
            DoublyNode<T> tempcurr = curr, node = node1;
            //если node1 не передан,то удаляем текущий элемент,
            //иначе если переданный элемент не текущий, то в конце восстановим curr
            if (node == null) node = curr;// else if(node!=curr) flag_isnodecurrent = false; 

            if (node.Next != null) { curr = node.Next; node.Next.Previous = node.Previous; }
            else { tail = node.Previous; curr = node.Previous; }
            // если узел не первый
            if (node.Previous != null) { node.Previous.Next = node.Next; }
            else { head = node.Next; }
            if (count > 0) count--;
            if (node1 == null) curr = tempcurr; // восстанавливаем curr 
            node.UNDO = null; node = null;

            return curr;
        }

        public bool RemoveData(T data)// поиск элементов из списка по строкам данных, и их удаление 
        {
            DoublyNode<T> curr1 = head;
            bool flag_action = false;

            // поиск удаляемого узла
            while (curr1 != null) //ищем данные в списке
            {
                if (curr1.DataOriginal.Equals(data) || curr1.DataTranslated.Equals(data))
                {
                    DeleteNode(curr1); //удаляем элемент
                    flag_action = true;
                }
                curr1 = curr1.Next;

            }
            return flag_action;
        }

        public int Count { get { return count; } }
        public long FilePosition { get { if (curr != null) return curr.Fileposition; else return -1; } }

        public bool IsEmpty { get { return count == 0; } }
        public object GetCurrentDataOriginal { get { return curr.DataOriginal; } } //возвращает данные из текущего элемента списка
        public object Curr { get { return curr; } } //возвращает указатель на текущий элемент списка
        public object GetDataOriginal(DoublyNode<T> Node) ////возвращает данные из произвольного элемента списка
        {
            if (Node != null) return Node.DataOriginal; else return null;
        }
        public object TranslatedData(DoublyNode<T> Node) ////возвращает данные из произвольного элемента списка
        {
            if (Node != null) return Node.DataTranslated; else return null;
        }

        public void ReplaceData(T dataTranslated, DoublyNode<T> directNode = null) // Заменяет поле даты текущего элемента, либо иного
        {                                           // элемента, ссылка на который указана в необязательном поле directNode
            if (directNode == null) directNode = curr;
            if (directNode != null)
            {
                if (directNode.UNDO.curr == null || directNode.UNDO == null)
                    directNode.UNDO.Add(directNode.DataOriginal, directNode.DataTranslated, directNode.Fileposition); //сохраним элемент первым в список UNDO
                else
                { // Список UNDO уже не пуст, добавим к нему элемент в случае выявления измененений
                    if (!directNode.UNDO.curr.DataTranslated.Equals(dataTranslated)) // Если данные изменились создадим эл-нт UNDO
                        directNode.UNDO.Add(directNode.DataOriginal, directNode.DataTranslated, directNode.Fileposition);

                    if (directNode.UNDO.Count() > 100) //Обрезаем хвост UNDO - лимит не более 100 откатов
                        directNode.UNDO.DeleteNode(directNode.UNDO.head.Next); //удаляем второй элемент из самых давних изменений
                }
                directNode.DataTranslated = dataTranslated;
                return;
            }
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

        public bool Contains(T dataOriginal, T dataTranslated, DoublyNode<T> node1 = null) //возвращает true, если любое из полей данных совпало
        {                                           //при отсутствии ссылки на проверяемый элемент, проверяется текущий
            DoublyNode<T> current = node1;
            if (current == null) current = curr;

            if (current.DataOriginal.Equals(dataOriginal) || current.DataTranslated.Equals(dataTranslated)) { return true; }

            return false;
        }
        public void ListContains(DoublyLinkedList<T> outputlist, string str1) //возвращает список с совпадениями, иначе null
        {
            DoublyNode<T> node = head;

            while (node != null)
            {
                if ((node.DataOriginal as string).Contains(str1) || (node.DataTranslated as string).Contains(str1))
                {
                    outputlist.Add(node.DataOriginal, node.DataTranslated, node.Fileposition);
                    outputlist.curr.Twin = node; //сохраняем ссылку на оригинал
                }
                node = node.Next;
            }
            outputlist.curr = outputlist.Head();
            return;
        }

        public void ListCopy(DoublyLinkedList<T> outputlist) //возвращает список с совпадениями, иначе null
        {
            DoublyNode<T> node = head;

            while (node != null)
            {
                outputlist.Add(node.DataOriginal, node.DataTranslated, node.Fileposition);
                outputlist.curr.Twin = node; //сохраняем ссылку на оригинал
                node = node.Next;
            }

            return;
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
                yield return current.DataTranslated;
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
                yield return current.DataTranslated;
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
            return current;
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
            return current;
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


        public void SetSearchList(DoublyLinkedList<string> linkedList, string str, bool home=false) //SetlinkedList
        { 
            if (linkedList == null) return; //если передана пустая строка или непередан список
            if (home == true) { linkedList.ListCopy(linkedListSS); return; } //Если вкладка home - то используем CommonData.linkedList
            else if (str == "") return;                                           //Начинаем поиск подстроки по всем элементам списка linkList
            linkedList.ListContains(linkedListSS, str); //заполняем список linkedListSS, из узлов linkedlist,
                                                        //содержащих подстроку str
        }

        public void Next() //перемещение по результатам поиска
        {
            if (linkedListSS.curr == null) return; // проверим что список не пустой
            if (linkedListSS.curr.Next != null) // есть ли последущий элемент?
            {
                if (linkedListSS.curr.Twin == null) // похоже открытая запись в поиске уже была удалена из основного списка
                { // Удалим ее из списка поиска
                    linkedListSS.DeleteNode(linkedListSS.curr); //удаляем ее без сохранения
                    if (currnum > linkedListSS.Count) currnum--;
                }
                else
                {
                    linkedListSS.ReplaceData(tabTranslated_tb.Text, linkedListSS.curr.Twin);
                    //linkedListSS.curr.Twin.Twin.Data = tabTranslated_tb.Text; //сохраняем содержимое текстбокса
                    currnum++;
                }
                //linkedListSS.curr = linkedListSS.curr.Next;
                linkedListSS.NextNode();
            }
            else // мы в конце списка. перемотаем на начало
            {
                /*currnum = 1;
                while (linkedListSS.curr.Previous != null)
                { linkedListSS.curr = linkedListSS.curr.Previous; }*/
                toFirst();
            }
            RefreshCurrent();
            tabSearchStat_tslb.Text = Convert.ToString(currnum) + " of " + linkedListSS.Count;
        }

        public void Prev()//перемещение по результатам поиска
        {
            if (linkedListSS.curr == null) return; // проверим что список не пустой
            if (linkedListSS.curr.Previous != null) // есть ли предыдущий элемент?
            {
                if (linkedListSS.curr.Twin == null) // похоже открытая запись в поиске уже была удалена из основного списка
                { // Удалим ее из списка поиска
                    linkedListSS.DeleteNode(linkedListSS.curr); //удаляем ее без сохранения
                }
                else // открытая запись действительна.
                {
                    linkedListSS.ReplaceData(tabTranslated_tb.Text, linkedListSS.curr.Twin); //сохраняем содержимое текстбокса

                }
                /*currnum--;
                linkedListSS.curr = linkedListSS.curr.Previous;*/
                currnum--;  linkedListSS.PrevNode();
            }
            else// мы в начале списка. перемотаем в конец
            {
                /*currnum = linkedListSS.Count;
                while (linkedListSS.curr.Next != null)
                { linkedListSS.curr = linkedListSS.curr.Next; }*/
                if (linkedListSS.curr != null) toLast();

            }
            RefreshCurrent();
            tabSearchStat_tslb.Text = Convert.ToString(currnum) + " of " + linkedListSS.Count;
            

        }

        public void toFirst()
        {
            if (linkedListSS.curr == null || linkedListSS.Count <= 0) return; // проверим что список не пустой
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
        public void toDefined(long pos) // переход на заданный числом номер записи
        {
            if (pos > linkedListSS.Count || pos <= 0) return; //запрос на нереальную позицию
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
                tabSource_tb.Text = linkedListSS.curr.Twin.DataOriginal;
                tabTranslated_tb.Text = linkedListSS.curr.Twin.DataTranslated;
            }
            else
            {
                tabSource_tb.Text = linkedListSS.curr.Twin.DataOriginal;
                tabTranslated_tb.Text = linkedListSS.curr.Twin.DataTranslated;
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


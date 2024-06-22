using Rusik;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rusik
{
    public static class CommonData  //Статический класс, который выступает в качестве буфера для обмена данными между формами
    {
        //класс для обмена данными
        public static System.Windows.Forms.ProgressBar progressBar1;
        public static System.Windows.Forms.Label PerformingFile_tb;
        public static System.Windows.Forms.Label progressBar1_lb;
        public static System.Windows.Forms.ComboBox comboBox1;
        public static System.Windows.Forms.ComboBox comboBox2;
        public static System.Windows.Forms.NumericUpDown nudRecord;
        public static System.Windows.Forms.Label TranslatedFile_tb;
        public static System.Windows.Forms.ToolStripTextBox Search_tstb;
        public static System.Windows.Forms.Label Records_lb;
        public static System.Windows.Forms.Button Start_btn;

        public static int W = 30; //ширина кнопки

        public static string Form2text;
        public static DateTime currt = new DateTime(DateTime.Now.Year, DateTime .Now.Month, DateTime.Now.Day, 0, 0, 0);
        public static DateTime currt0 = currt;

        public static readonly int MaxBytesMessage = 7000; // Максимальный размер сообщения в байтах
        public static long CurrentnudRecord; // переменная для сохранения номера текущей записи списка при запуске поиска
        // номер текущей вкладки в окне с Source, использую для динамич. именования контролов
        public static int currentTabS = 0;

        //public Dictionary <TabPage, SearchTabs> OpenedTabs; //коллекция открытых вкладок
        public static long SourceNodeCounter = 0; // счетчик-указатель на текущую запись списка
        public static string OriginalBinaryFile = ""; // бинарный OriginalBinaryFile
        public static string ProjectFile = ""; // бинарный со своей разметкой вида
                                               // {Nposition}{Nbyte-original}{data-original}{Nbytes-changed}{data-changed}... - обязательная часть
                                               // {Nbytes-alter1}{data-alter1}... {Nbytes-alterM}{data-alterM}                - необязательная часть
                                               // 0xFFFFlong //                                                               - конец  элемента
        public static string TranslatedFileST = ""; //Текстовый файл частично переведенный ранее со знаком разделителем "="
        public static string IniFile = ""; //полный путь к INI файлу
        public static DoublyLinkedList<string> linkedList = new();
        public static bool flag_NotSavedYet = false;//флаг - требуется сохранение, данные были изменены
        public static bool flag_Skipdialog = false; //флаг пропуска пользовательских диалоговых окон
        //флаг наличие доп.данных о смещениях/позициях текстовых строк внутри бинарного файла :
        public static bool flag_ExtraDataforBinary = false; //наличие спец.файла с картой рипнутого бинарного файла
        public static byte[] Signature = { 0x04, 0x00, 0x06, 0x00 };  // Сигнатура из байт

        public static string languageIN = "en"; //из модуля google-переводчика входной/выходной языки
        public static string languageOUT = "ru";
        public static string InterfaceLanguage = "en"; //английский язык по-умолчанию
        public static long LastRecordNumber = 1; // это сохраненный в ini файле параметр nudRecord последнего открытого файла
        public static Dictionary<string, string> GoogleLangs = new Dictionary<string, string>(){
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
        public static TabControl Tabs; //таб-контрол
        public static TabPage Home; // главная вкладка
        public static TextBox mess_tb; //стартовый help
        public static Button SkipCheck_btn; //кнопка пропуска диалогов



    }
}

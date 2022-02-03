using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Resources;
using System.Runtime.InteropServices;
using System.Drawing.Text;
using System.IO;
using System.Windows;
using System.Drawing.Drawing2D;

namespace Password_Generator
{ 
    public partial class Miner1 : Form
    {
        Random rnd = new Random();
          int nudMinerX = 10;
          int nudMinerY = 10;
          int nudComplicate = 12; //12% мин EASY mode
          

        public Miner1()
        {
            InitializeComponent();
            this.CenterToScreen(); // устанавливаем положение формы  по центру экрана
        }

        public void GameLevelChanging(object sender, EventArgs e)
        {
         // Determine if clicked menu item is the GameLevel menu item.
            if (sender == MenuGameLevelEasy)
            {
                // Set the checkmark
                MenuGameLevelEasy.Checked = true;
                // Uncheck other GameLevel menu items.
                MenuGameLevelMedium.Checked = false;
                MenuGameLevelHard.Checked = false;
                MenuGameLevelNightmare.Checked = false;
                // Set the color of the text in the TextBox control to Blue.
                nudComplicate = 12; //% мин
                StaticData.Form2text = "Take It Easy ...";
            }
            else if (sender == MenuGameLevelMedium)
            {
                // Set the checkmark
                MenuGameLevelMedium.Checked = true;
                // Uncheck others menu items.
                MenuGameLevelEasy.Checked = false;
                MenuGameLevelHard.Checked = false;
                MenuGameLevelNightmare.Checked = false;
                nudComplicate = 17; //%мин
                StaticData.Form2text = "It Was Very Long Day ...";
            }
            else if (sender == MenuGameLevelHard)
            {
                // Set the checkmark
                MenuGameLevelHard.Checked = true;
                // Uncheck the menuItemRed and menuItemBlue menu items.
                MenuGameLevelEasy.Checked = false;
                MenuGameLevelMedium.Checked = false;
                MenuGameLevelNightmare.Checked = false;
                // Set the color of the text in the TextBox control to Blue.
                nudComplicate = 22; // %
                StaticData.Form2text = "Hard Time For You ...";
            }
            else //nightmare
            {
                MenuGameLevelEasy.Checked = false;
                MenuGameLevelMedium.Checked = false;
                MenuGameLevelHard.Checked = false;
                MenuGameLevelNightmare.Checked = true;
                nudComplicate = 25; // %
                StaticData.Form2text = "Time To Die !!!";
            }
    }

    public void SizePlayfieldChanging(object sender, EventArgs e)
    {
        // Determine if clicked menu item is the Playfield menu item.
        if (sender == MenuSizePlayfield10)  //easy
        {
            // Set the checkmark
            MenuSizePlayfield10.Checked = true;
            // Uncheck other Playfield menu items.
            MenuSizePlayfield20.Checked = false;
            MenuSizePlayfield30.Checked = false;
            MenuSizePlayfieldCustom.Checked = false;
            nudMinerX = 10; nudMinerY = 10;
            XSizePlayfield.Text = "Size X:"; YSizePlayfield.Text = "Size Y:"; // заполняем поля с подсказками
        }
        else if (sender == MenuSizePlayfield20)
        {
            MenuSizePlayfield20.Checked = true;
            MenuSizePlayfield10.Checked = false;
            MenuSizePlayfield30.Checked = false;
            MenuSizePlayfieldCustom.Checked = false;
            nudMinerX = 20; nudMinerY = 20;
            XSizePlayfield.Text = "Size X:"; YSizePlayfield.Text = "Size Y:"; // заполняем поля с подсказками
        }
            else if (sender == MenuSizePlayfield30)
        {
            MenuSizePlayfield30.Checked = true;
            MenuSizePlayfield10.Checked = false;
            MenuSizePlayfield20.Checked = false;
            MenuSizePlayfieldCustom.Checked = false;
            nudMinerX = 30; nudMinerY = 30;
            XSizePlayfield.Text = "Size X:"; YSizePlayfield.Text = "Size Y:"; // заполняем поля с подсказками

        }
        else if (sender == MenuSizePlayfieldCustom) 
        {
          MenuSizePlayfield10.Checked = false;
          MenuSizePlayfield20.Checked = false;
          MenuSizePlayfield30.Checked = false;
          MenuSizePlayfieldCustom.Checked = true;
          if (XSizePlayfield.Text is "") { XSizePlayfield.Text = "Size X:"; nudMinerX = 0; }
          if (YSizePlayfield.Text is "") { YSizePlayfield.Text = "Size Y:"; nudMinerY = 0; }
        } 
        else if (sender == XSizePlayfield)
        {
          MenuSizePlayfield10.Checked = false;
          MenuSizePlayfield20.Checked = false;
          MenuSizePlayfield30.Checked = false;
          MenuSizePlayfieldCustom.Checked = true;
          XSizePlayfield.Text = ""; nudMinerX = 0;
          if (nudMinerY == 0) YSizePlayfield.Text = "Size Y:"; 
        }
        else // sender ==YSizePlayfield
        {
           MenuSizePlayfield10.Checked = false;
           MenuSizePlayfield20.Checked = false;
           MenuSizePlayfield30.Checked = false;
           MenuSizePlayfieldCustom.Checked = true;
           YSizePlayfield.Text = ""; nudMinerY = 0;
           if (nudMinerX == 0) XSizePlayfield.Text = "Size X:"; 
        }
    }
        void MenuStartGame_Click(object sender, EventArgs e)
        {
          // проверяем что переменные nudMinerX и nudMinerY находятся в диапазоне 5..30
            if (nudMinerX < 5) nudMinerX = 5; 
            if (nudMinerX > 30) nudMinerX = 30;
            if (nudMinerY < 5) nudMinerY = 5;
            if (nudMinerY > 30) nudMinerY = 30;
            XSizePlayfield.Text = nudMinerX.ToString();
            YSizePlayfield.Text = nudMinerY.ToString(); 

            StaticData.X = nudMinerX;
            StaticData.Y = nudMinerY;
            StaticData.S = nudComplicate;

            /*
            * Чтобы изменить что-то у Формы1, нужно к ней обратиться
            * Чтобы к ней обратиться, нужна ссылка на неё. У нашей Формы2 нет ссылки на Форму1. Никакой.
            * Для этого, при создании экземпляра Формы2, мы передаём ей ссылку на Форму1 (this - ссылка на себя).
            * А в Форме2, мы принимаем эту ссылку (в Конструкторе) и записываем в переменную типа Form1.
            * Теперь мы можем обратиться к Форме1 через эту новую переменную, которая хранит ссылку на Форму1, 
            * и поменять в ней всё, что мы могли бы поменять из самой Формы1.
            * https://www.youtube.com/watch?v=Q9lBJ_q6eCA
                        namespace App1;
                        {
                            public partial class Form1:Form
                            {
                               public Form1();
                               void Button1_Click(object sender,EventArgs e)
                               {
                                    new Form2(this).Show();
                                }
                            }
                            public partial class Form2:Form
                            {
                                readonly Form1 form1;
                                public Form2(Form1 owner)
                                {
                                    form1=owner;
                                    InitializeComponent();
                                }
                                void Button1_Click(object sender, EventArgs e)
                                {
                                    form1.button1.Text="Uraaaaaa!!!";
                                }
                            }
                        }
            */
            this.Visible = false; //отключаем основное окно
            new Miner2(this).Show();//открываем дочернюю форму с полем игры
        }

        private void tbSizePlayfield_KeyDown(object sender, KeyEventArgs e)///object sender, KeyPressEventArgs e
        {
            if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back) // символЫ Delete и BackSpace
            {
                if (sender == XSizePlayfield) { XSizePlayfield.Text = ""; nudMinerX = 0; }
                if (sender == YSizePlayfield) { YSizePlayfield.Text = ""; nudMinerY = 0; }
            }
        }

        private void tbSizePlayfield_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;

            if (!Char.IsDigit(number)) //Если не цифры , то
            {
               e.Handled = true; // не пропускать символ
                return;
            }
            
            if (sender == XSizePlayfield) //пришла цифра из текстбокса XSizePlayfield
            {
                if (XSizePlayfield.Text.Length > 3) XSizePlayfield.Text = ""; // если на вход залетело слово вместо цифр
                string tmpstr = XSizePlayfield.Text + number;
                switch(tmpstr.Length) //проверим новую длину текстбокса включая цифру number
                {
                  case 3:// вводится уже третья цифра, например 29+3 или 11+1
                         // тогда, отбросим первую цифру и проверим не превысит ли новое число 30
                        string ts2 = tmpstr.Substring(1, 2); // берем 2 последние цифры, включая новую
                        if (Convert.ToInt32(ts2) > 30) // если превышает, то пишем в текстбокс 0 + number
                        {
                            XSizePlayfield.Text = "0" + number;
                            e.Handled = true; // не пропускать символ, т.к. мы строку отредактировали уже здесь.
                            nudMinerX = Convert.ToInt32(XSizePlayfield.Text);
                            return;
                        } 
                        //иначе оставляем в текстбоксе два последних символа
                        XSizePlayfield.Text = ts2;
                        e.Handled = true; // не пропускать символ, т.к. мы строку отредактировали уже здесь.
                        nudMinerX = Convert.ToInt32(ts2);
                        return;
                  case 2:// вводится второй символ ... например 4+1
                        if (Convert.ToInt32(tmpstr) > 30)//Если число становиться больше 30
                        {
                         XSizePlayfield.Text = "0" + number; // первую цифру заменяем на 0, а вторую оставляем новую
                         e.Handled = true; // не пропускать символ, т.к. мы строку отредактировали уже здесь.
                         nudMinerX = Convert.ToInt32(XSizePlayfield.Text);
                         return;
                        }
                        nudMinerX = Convert.ToInt32(tmpstr);
                        break;
                  default:
                        //XSizePlayfield.Text = tmpstr; это делает textbox на автомате :)
                        nudMinerX = Convert.ToInt32(tmpstr);// вводится первый символ
                        break;
                }
                return;
            }

            if (sender == YSizePlayfield) //пришла цифра из текстбокса YSizePlayfield
            {
                if (YSizePlayfield.Text.Length > 3) YSizePlayfield.Text = ""; // если на вход залетело слово вместо цифр
                string tmpstr = YSizePlayfield.Text + number;
                switch (tmpstr.Length) //проверим новую длину текстбокса включая символ number
                {
                    case 3:// вводится уже третий символ, например 29+3 или 11+1
                           // тогда, отбросим первый символ и проверим не превысит ли новое число 30
                        string ts2 = tmpstr.Substring(1, 2);
                        if (Convert.ToInt32(ts2) > 30) // если превышает, то пишем в текстбокс 0 + number
                        {
                            YSizePlayfield.Text = "0" + number;
                            e.Handled = true; // не пропускать символ, т.к. мы строку отредактировали уже здесь.
                            nudMinerY = Convert.ToInt32(YSizePlayfield.Text);
                            return;
                        } //иначе оставляем в текстбоксе два последних символа
                        YSizePlayfield.Text = ts2;
                        e.Handled = true; // не пропускать символ, т.к. мы отредактировали строку  уже здесь.
                        nudMinerY = Convert.ToInt32(ts2);
                        return;
                    case 2:// вводится второй символ ... например 4+1
                        if (Convert.ToInt32(tmpstr) > 30)//Если число становиться больше 30
                        {
                            YSizePlayfield.Text = "0" + number; // первую цифру заменяем на 0, а вторую оставляем новую
                            e.Handled = true; // не пропускать символ, т.к. мы строку отредактировали уже здесь.
                            nudMinerY = Convert.ToInt32(YSizePlayfield.Text);
                            return;
                        }
                        nudMinerY = Convert.ToInt32(tmpstr);
                        break;
                    default:
                        //YSizePlayfield.Text = tmpstr; это делает textbox на автомате :)
                        nudMinerY = Convert.ToInt32(tmpstr);
                        break;
                }
                //if (YSizePlayfield.Text.Length > 0) nudMinerY = Convert.ToInt32(tmpstr); else nudMinerY = 0;
                return;
            }
        }
    }

    public static class StaticData  //Статический класс, который выступает в качестве буфера для обмена данными между формами
    {
        //класс для обмена данными
        public static int X = 0;
        public static int Y = 0;
        public static decimal S = 0; 

        public static string Form2text;
        public static DateTime currt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
        public static DateTime currt0 = currt;
        public static bool stoptimer_flag = false;
        public static bool NightColorMode = false;
        //      public static Color colorschem1 = Color.FromArgb(35, 30, 31); // синий индикатор
        //      public static Color colorschem2 = Color.FromArgb(2, 226, 232);// темно-серый неактивный индикатор
        public static Color colorschem1 = Color.White; // синий индикатор
        public static Color colorschem2 = Color.Red;// темно-серый неактивный индикатор
        
    }
    public partial class Miner2 : Form // класс дочерней формы игрового поля
    {
        readonly Miner1 miner1; //создаем переменную со ссылкой на адрес класса1
        int X;
        int Y;
        int S;
        PrivateFontCollection private_fonts = new PrivateFontCollection();

        public GifImage gifImage = null;
        public GifImage gifMine = null;

        public string filePath = @"C:\Users\usr\source\repos\Miner\mine30l.gif"; //взрыв
        public string filePath2 = @"C:\Users\usr\source\repos\Miner\blackbomb30.gif"; //мина на взводе
        public string filePath3 = @"C:\Users\usr\source\repos\Miner\redbomb30.gif";
        public string filePath4 = @"C:\Users\usr\source\repos\Miner\flag_red30.png";
        public string filePath5 = @"C:\Users\usr\source\repos\Miner\flag_yellow30.png";
        /*public string filePath = @"C:\Users\amsad\source\EduCodeGitHub\Miner\mine30l.gif"; //взрыв
        public string filePath2 = @"C:\Users\amsad\source\EduCodeGitHub\Miner\blackbomb30.gif"; //мина на взводе
        public string filePath3 = @"C:\Users\amsad\source\EduCodeGitHub\Miner\redbomb30.gif";
        public string filePath4 = @"C:\Users\amsad\source\EduCodeGitHub\Miner\flag_red30.png";
        public string filePath5 = @"C:\Users\amsad\source\EduCodeGitHub\Miner\flag_yellow30.png";*/

        DateTime date1 = new DateTime(0, 0);
        Timer timer1 = new Timer();
        Timer timer2 = new Timer();

        public Clock gametime; // индикатор игрового таймера
        public Clock gamecont; // индикатор количества мин
        public Color LabelBackColour =Color.Azure;
   
        Label WIN = new Label();
        int W = 30; //размеры кнопок в пикселях -ширина
        int H = 30; //размеры кнопок в пикселях -высота
        int Z;      // исходное количество мин. изначально равно S
        Button[,] _buttons = new Button[30, 30];
        bool[,] buttonflags = new Boolean[30, 30];
        bool[,] buttonopened = new Boolean[30, 30];

        public Label[,] LButtons = new Label[30, 30];
        Boolean flag_detonation = false;
        bool flag_restart = false;
        int[,] minespole = new int[30,30];
        int explodeX;
        int explodeY;
        Button Startbtn = new Button(); //кнопка рестарт
        Button Restartbtn = new Button();// После окончания игры создадим эту кнопку на все окно дря рестарта
        Button FlagSWbtn = new Button();// кнопка смена режимов клавищ мыши, меняем местами левую и правую кнопки мыши
        bool FlagSwitch = false; // по умолчанию, левая кнопка тыкает, а правая открывает ячейки
        public const int BySide_padding = 40;//45
        public const int Top_padding = 45; //90
        Random rnd = new Random();

        public Miner2(Miner1 owner)
        {
            miner1 = owner;
            // Загружаем встроенный в ресурсы свой шрифт
            EnableDoubleBuffering();
            
            LoadFont();
            var labelFontDigi = new Font(private_fonts.Families[0], 20);

            gifImage = new GifImage(filePath); //2
            gifImage.ReverseAtEnd = false; // 2 dont reverse at end

            gifMine = new GifImage(filePath2);
            this.FormClosing += new FormClosingEventHandler(this.Miner2_FormClosing);// обработчик закрытия окна по крестику

            this.Text = StaticData.Form2text;// "Take it Easy ...";
            //this.BackColor= Color.Black;

            X = StaticData.X;
            Y = StaticData.Y;
            S = Convert.ToInt32(Math.Round(StaticData.S*X*Y/100)); /*количество мин исходя из уровня сложности S%*(*X*Y)/100% */
            Z = S; // количество мин
            // вычисляем размер окна
            // константы размеров
            
            this.Width = Convert.ToInt32(BySide_padding * 2 + X*W + W/2 + 2); // 
            this.Height = Convert.ToInt32(Top_padding*2 + Y*H + H/2 + 2); // 
            this.CenterToScreen(); // выводим форму с окном по центру экрана
                                   //Формируем экранный буфер
                                   //public BufferedGraphicsContext();


            //Создаем кнопку переключения нажатий/режимы тык/флаг
            var button_rst = new Button();
            System.Drawing.Drawing2D.GraphicsPath myPath = new System.Drawing.Drawing2D.GraphicsPath();
            myPath.AddEllipse(3*W, 5, W, W);
            //Graphics g = CreateGraphics();

            Region myRegion = new Region(myPath);
            button_rst.Bounds = new Rectangle(3 * W, 5, W, W);
            button_rst.Region = myRegion;
            
            // рисуем окантовоку для круглой кнопки 
            /*g.DrawEllipse(new Pen(Color.Gray, 2),
            button_rst.Left + 1,
            button_rst.Top + 1,
            button_rst.Width - 3,
            button_rst.Height - 3);*/
            button_rst.BackColor = Color.Blue;
            //button_rst.Width = W;
            //button_rst.Height = H;
            //button_rst.Location = new Point(this.Width/2-W, 5); //20 и 35 - отступы слева и сверху
            button_rst.Tag = 1000; // специальная кнопка
            button_rst.Visible = true;
            button_rst.BringToFront();

            Restartbtn = button_rst;
            this.Controls.Add(Restartbtn); //выводим кнопку с заданными ранее параметрами
            this.Restartbtn.MouseDown += new MouseEventHandler(this.button1_MouseDown); // вешаем на кнопку  обработчик нажатий  кнопок мыши
            this.Restartbtn.Focus();                                                                      // this._buttons[i, j].Click += new System.EventHandler(this.button1_Click); // вешаем обработчик событий
                                                                                                          ///////// А теперь зададим то, что под кнопкой



            var labelfont = new Font("Arial", 16, FontStyle.Bold); // задаем шрифт -красный текст на черном фоне с центровкой

            gamecont = new Clock();
            gamecont.bombcounter=Z;
            gamecont.Bounds = new Rectangle(W+10, 5, 2*W, 25); // задаем область контрола с часами
            Controls.Add(gamecont);

            gametime = new Clock();
            gametime.Bounds = new Rectangle(this.Width - 4*W, 5, 2*W+5, 25); // задаем область контрола с часами
            Controls.Add(gametime);
 
            //надпись о победе/проигрыше
            WIN.Font = new Font("Arial", 16, FontStyle.Bold);
            WIN.ForeColor = Color.Red;
            
            WIN.BackColor = Color.LightGreen;
            WIN.Visible = false;
            WIN.Location = new Point(BySide_padding, this.Height-70);
            WIN.Height = 25;
            WIN.Width = H * X;
            WIN.TextAlign = ContentAlignment.MiddleCenter;
            WIN.Text = "Victory !";

            Controls.Add(WIN);
            this.WIN.MouseDown += new MouseEventHandler(this.button2_MouseDown);





            for (int i = 0; i < X; i++)
                for (int j = 0; j < Y; j++)
                {
                    minespole[i, j] = 0; //инициализируем минное поле
                    buttonflags[i, j] = false; // инициаализацция массива флагов на кнопках
                    buttonopened[i, j] = false;//ini массива нажатых кнопок
                }

            for (int i = 0; i < S; i++) // минируем поле, где S - кол-во мин
            {
                int x, y;
                do {
                    x = rnd.Next(0,X-1); 
                    y = rnd.Next(0,Y-1);
                } while (minespole[x, y]==10);
                minespole[x, y] = 10; //мина
            }
            var font14 = new Font("Arial", 14, FontStyle.Bold);  

            for (int i = 0; i < X; i++)
                for (int j = 0; j < Y; j++)
                {   // создание и вывод кнопок 
                    var button = new Button();
                    button.Width = W;
                    button.Height = H;
                    button.Location = new Point((button.Width) * i + 1 + BySide_padding, (button.Height) * j + 1 + 35); //20 и 35 - отступы слева и сверху
                    button.Text = "";
                    //button.FlatStyle = FlatStyle.Popup;
                    button.BackColor = Color.AntiqueWhite;

                    button.Font = font14;

                    button.Tag = j * X + i;// НЕ заминировано, записываем только порядковый номер кнопки
                    //button.Text = (j * X + i).ToString();
                    //button.Tag = 0; 

                    button.Visible = true;
                    //button.BringToFront();

                    _buttons[i, j] = button;
                    this.Controls.Add(_buttons[i, j]); //выводим кнопку с заданными ранее параметрами
                    this.DoubleBuffered = true;

                    this._buttons[i, j].MouseDown += new MouseEventHandler(this.button1_MouseDown); // вешаем на кнопку  обработчик нажатий  кнопок мыши
                                                                                                    // this._buttons[i, j].Click += new System.EventHandler(this.button1_Click); // вешаем обработчик событий
                    ///////// А теперь зададим то, что под кнопкой
                    int around = 0; //Посчитаем количество мин вокруг ячейки и создадим их лейблы
                    if (minespole[i, j] == 10) { around = 10; } //Ячейка с миной, окружение можно не просчитывать
                    else
                    {
                        if (i > 0 && minespole[i - 1, j] == 10) around++; //WEST
                        if (i > 0 && j > 0 && minespole[i - 1, j - 1] == 10) around++; //NW
                        if (j > 0 && minespole[i, j - 1] == 10) around++; // Nord
                        if (i < (X - 1) && j > 0 && minespole[i + 1, j - 1] == 10) around++; //NordEast
                        if (i < (X - 1) && minespole[i + 1, j] == 10) around++; //East
                        if (i < (X - 1) && j < (Y - 1) && minespole[i + 1, j + 1] == 10) around++; //SouthEast
                        if (j < (Y - 1) && minespole[i, j + 1] == 10) around++; //South
                        if (i > 0 && j < (Y - 1) && minespole[i - 1, j + 1] == 10) around++; //SouthWest
                        minespole[i, j] = around; // указываем количество мин вокруг от 0 до 9
                    }
          
                  
                    var labelbutton = new Label(); // счетчик мин
                                                   
                    labelbutton.Text = around.ToString();
                    switch (around)
                    {
                        case 0:
                            labelbutton.Text = "";  //  пустая метка ячейки поля. 0 - не будем показывать
                            break;
                        //цвет шрифта числа мин вокруг
                        case 1: labelbutton.ForeColor = Color.Blue; break;
                        case 2: labelbutton.ForeColor = Color.Green; break;
                        case 3: labelbutton.ForeColor = Color.Red; break;
                        case 4: labelbutton.ForeColor = Color.Navy; break;
                        case 5: labelbutton.ForeColor = Color.DeepPink; break;
                        case 6: labelbutton.ForeColor = Color.Brown; break;
                        case 7: labelbutton.ForeColor = Color.OrangeRed; break;
                        case 8: labelbutton.ForeColor = Color.Indigo; break;
                        case 9: labelbutton.ForeColor = Color.Yellow; break;
                        case 10: //labelbutton.ForeColor = Color.Black; //рисуем мину
                                 labelbutton.Text = "";
                                 labelbutton.Image = gifMine.GetFrame(0); 
                                 break;
                        default: break; //это очищенная пустая область

                    }
                    // Создаем прототип лейбла с элементом минного поля
                   
                    labelbutton.Width = W;
                    labelbutton.Height = H;
                    labelbutton.Visible = true;
                    //labelbutton.ForeColor = Color.Red; //цвет шрифта
                    labelbutton.BackColor = LabelBackColour; // цвет фона
                    labelbutton.TextAlign = ContentAlignment.MiddleCenter;
                    labelbutton.BorderStyle = BorderStyle.Fixed3D;
                    labelbutton.Font = labelfont;

                    labelbutton.Location = new Point(W * i + BySide_padding, H * j + 35);
                    labelbutton.Visible = true;

                    LButtons[i, j] = labelbutton;
                    this.Controls.Add(labelbutton);
                    this.DoubleBuffered = true;

                    this.timer2.Interval = 1500;// настраиваем интервал таймера
                    this.timer2.Tick += new System.EventHandler(this.timer2_Tick); // 2 создаем таймер для gif - анимации

                    this.LButtons[i, j].MouseDown += new MouseEventHandler(this.button2_MouseDown); // вешаем на лейблы  обработчик нажатий  кнопок мыши

                }
        }
        public void EnableDoubleBuffering()
        {
            // Set the value of the double-buffering style bits to true.
            this.SetStyle(ControlStyles.DoubleBuffer |
               ControlStyles.UserPaint |
               ControlStyles.AllPaintingInWmPaint,
               true);
            this.UpdateStyles();
        }
        public sealed class BufferedGraphicsManager
        {
            private static BufferedGraphicsContext bufferedGraphicsContext;
            public static BufferedGraphicsContext Current
            {
                get { return BufferedGraphicsManager.bufferedGraphicsContext; }
            }

            static BufferedGraphicsManager()
            {
                BufferedGraphicsManager.bufferedGraphicsContext = new BufferedGraphicsContext();

                AppDomain.CurrentDomain.ProcessExit += new EventHandler(BufferedGraphicsManager.OnShutdown);
                AppDomain.CurrentDomain.DomainUnload += new EventHandler(BufferedGraphicsManager.OnShutdown);
            }
            private static void OnShutdown(object sender, EventArgs e)
            {
                BufferedGraphicsManager.Current.Invalidate();
            }
        }
       /* public BufferedGraphics Allocate(Graphics targetGraphics, Rectangle targetRectangle)
        {
            if (targetRectangle.Width * targetRectangle.Height > this.MaximumBuffer.Width * this.MaximumBuffer.Height)
                return this.AllocBufferInTempManager(targetGraphics, IntPtr.Zero, targetRectangle);
            else
                return this.AllocBuffer(targetGraphics, IntPtr.Zero, targetRectangle);
        }*/
        private void LoadFont()
        {

            using (MemoryStream fontStream = new MemoryStream(Minesweeper.Properties.Resources.DS_DIGI))
            {
                // create an unsafe memory block for the font data
                System.IntPtr data = Marshal.AllocCoTaskMem((int)fontStream.Length);
                // create a buffer to read in to
                byte[] fontdata = new byte[fontStream.Length];
                // read the font data from the resource
                fontStream.Read(fontdata, 0, (int)fontStream.Length);
                // copy the bytes to the unsafe memory block
                Marshal.Copy(fontdata, 0, data, (int)fontStream.Length);
                // pass the font to the font collection
                private_fonts.AddMemoryFont(data, (int)fontStream.Length);
                // close the resource stream
                fontStream.Close();
                // free the unsafe memory
                Marshal.FreeCoTaskMem(data);

            }

        }
        void Miner2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
                e.Cancel = true;
            StaticData.currt = StaticData.currt0; //сбросим время
            if (timer2 != null) timer2.Dispose(); 
            if (timer2 != null) timer2 = null; //убиваем таймер, чтобы избежать артефактов при перезапуске

            miner1.Visible = Enabled;
            this.Hide();
        }
        private void NewGameINI ()
        {
            S=Z; // количество мин
            flag_detonation = false;
            flag_restart = false;
            timer2.Stop();
            LButtons[explodeX, explodeY].Image = null;
            explodeX = 0; explodeY = 0;
            if(WIN.Visible is true)WIN.Visible=false; //выключаем сообщение о победе

            gametime.currenttime=StaticData.currt0;
            gametime.Invalidate();
            gamecont.bombcounter = S; //выводим на счетчик кол-во неоткрытых мин
            gamecont.Invalidate();
            var font14 = new Font("Arial", 14, FontStyle.Bold);
            for (int i = 0; i < X; i++)
                for (int j = 0; j < Y; j++)
                {
                    minespole[i, j] = 0; //инициализируем минное поле
                    buttonflags[i, j] = false; // инициализация массива флагов на кнопках
                    buttonopened[i, j] = false;//ini массива нажатых кнопок
                }
            for (int i = 0; i < S; i++) // минируем поле, где S - кол-во мин
            {
                int x, y;
                do
                {
                  x = rnd.Next(0, X - 1);
                  y = rnd.Next(0, Y - 1);
                } while (minespole[x, y] == 10);
                minespole[x, y] = 10; //мина
            }
            for (int i = 0; i < X; i++)
                for (int j = 0; j < Y; j++)
                {
                    //Если  кнопка включена , а надпись - нет
                    //_buttons[i, j].Visible = false;// на случай, если кнопка видна -выключаем
                    LButtons[i, j].Visible = true; //иначе изменения в Лейбл не отработаются ;
                   
                    LButtons[i, j].Text = "";
                    LButtons[i, j].Image = null;
                    LButtons[i, j].BackColor = LabelBackColour;
                    LButtons[i, j].Font = font14;             
                   int around = 0; //Посчитаем количество мин вокруг ячейки и создадим их лейблы
                    if (minespole[i, j] == 10) 
                    {  around = 10; LButtons[i, j].Image = gifMine.GetFrame(0);} //Ячейка с миной, окружение можно не просчитывать
                    else
                    {
                        if (i > 0 && minespole[i - 1, j] == 10) around++; //WEST
                        if (i > 0 && j > 0 && minespole[i - 1, j - 1] == 10) around++; //NW
                        if (j > 0 && minespole[i, j - 1] == 10) around++; // Nord
                        if (i < (X - 1) && j > 0 && minespole[i + 1, j - 1] == 10) around++; //NordEast
                        if (i < (X - 1) && minespole[i + 1, j] == 10) around++; //East
                        if (i < (X - 1) && j < (Y - 1) && minespole[i + 1, j + 1] == 10) around++; //SouthEast
                        if (j < (Y - 1) && minespole[i, j + 1] == 10) around++; //South
                        if (i > 0 && j < (Y - 1) && minespole[i - 1, j + 1] == 10) around++; //SouthWest
                        minespole[i, j] = around; // указываем количество мин вокруг от 0 до 9
                        LButtons[i,j].Text = around.ToString();
                        switch (around)
                        {
                           case 0:
                               LButtons[i, j].Text = "";  //  пустая метка ячейки поля. 0 - не будем показывать
                               break;
                           //цвет шрифта числа мин вокруг
                           case 1: LButtons[i, j].ForeColor = Color.Blue; break;
                           case 2: LButtons[i, j].ForeColor = Color.Green; break;
                           case 3: LButtons[i, j].ForeColor = Color.Red; break;
                           case 4: LButtons[i, j].ForeColor = Color.Navy; break;
                           case 5: LButtons[i, j].ForeColor = Color.DeepPink; break;
                           case 6: LButtons[i, j].ForeColor = Color.Brown; break;
                           case 7: LButtons[i, j].ForeColor = Color.OrangeRed; break;
                           case 8: LButtons[i, j].ForeColor = Color.Indigo; break;
                           case 9: LButtons[i, j].ForeColor = Color.Yellow; break;
                           //case 10: break;
                           default: break; //это очищенная пустая область
                        }
                    }

                    // Создаем прототип лейбла с элементом минного поля
                    _buttons[i, j].BringToFront();
                    _buttons[i, j].Image = null;

                }
        }
 /*       private void timer1_Tick(object sender, EventArgs e)
        {
          date1 = date1.AddSeconds(1);
        }
 */
        private void timer2_Tick(object sender, EventArgs e)
        {
            LButtons[explodeX, explodeY].Image=(Bitmap)gifImage.GetNextFrame();
            if (LButtons[explodeX, explodeY].Image is null) 
            { 
                timer2.Stop(); 
                LButtons[explodeX, explodeY].Image = Image.FromFile(filePath3); 
                return; 
            }
        }
        private void dispose_button(Button b) //Нажатие ... отключить кнопку и вывести вместо нее label
        {
            int t = (int)b.Tag;
            int x = t % X;
            int y = t / X;
            
            if (buttonflags[x, y] is true) return; // На клетке стоит флаг, ничего отключать не нужно
            if (buttonopened[x, y] is true) return; //Эта кнопка уже открыта .... выходим
            else buttonopened[x, y] = true; // отметим, что эта кнопка была нажата
            //b.Visible=false; //прячем кнопку
            LButtons[x, y].BringToFront(); //показываем надпись под кнопкой
            switch (minespole[x, y])
            {
                case 0:  // нажата пустая клетка -отключаем кнопку и
                                                // очищаем также все соседние клетки вокруг
                        if (x > 0 && minespole[x - 1, y] < 10)
                        {
                            minespole[x, y] = 100; //теперь это очищенная пустая область
                            dispose_button(_buttons[x - 1, y]);
                        }//WEST 1
                        if (x > 0 && y > 0 && minespole[x - 1, y - 1] < 10)
                        {
                            minespole[x, y] = 100;//теперь это очищенная пустая область \
                            dispose_button(_buttons[x - 1, y - 1]);
                        } //NW 2
                        if (y > 0 && minespole[x, y - 1] < 10)
                        {
                            minespole[x, y] = 100;//теперь это очищенная пустая область
                            dispose_button(_buttons[x, y - 1]);
                        } // Nord 3
                        if (x < (X - 1) && y > 0 && minespole[x + 1, y - 1] < 10)
                        {
                            minespole[x, y] = 100;//теперь это очищенная пустая область
                            dispose_button(_buttons[x + 1, y - 1]);
                        }//NordEast 4
                        if (x < (X - 1) && minespole[x + 1, y] < 10)
                        {
                            minespole[x, y] = 100;//теперь это очищенная пустая область
                            dispose_button(_buttons[x + 1, y]);
                        }  //East 5
                        if (x < (X - 1) && y < (Y - 1) && minespole[x + 1, y + 1] < 10)
                        {
                            minespole[x, y] = 100;//теперь это очищенная пустая область
                            dispose_button(_buttons[x + 1, y + 1]);
                        }//SouthEast 6
                        if (y < (Y - 1) && minespole[x, y + 1] < 10)
                        {
                            minespole[x, y] = 100;//теперь это очищенная пустая область
                            dispose_button(_buttons[x, y + 1]);
                        }//South 7
                        if (x > 0 && y < (Y - 1) && minespole[x - 1, y + 1] < 10)
                        {
                            minespole[x, y] = 100;//теперь это очищенная пустая область
                            dispose_button(_buttons[x - 1, y + 1]);
                        }//SouthWest 8
                        break;
                //цвет шрифта числа мин вокруг
                case 1 - 9: break;
                case 10:// флаг, что нажал прямо в эту мину .... :(
                    if (flag_detonation is true) 
                    {
                      gifImage.ReverseAtEnd = false; // 2 dont reverse at end
                      gifImage.Repeat = false; // 2 dont repeat playback
                      explodeX = x; explodeY = y;
                      LButtons[x, y].Image = (Image)gifImage.GetFrame(0);
                      timer2.Start(); //начнем анимацию взрыва
                    } 
                    break;
                default: /*GameOver_check();*/ return; //это очищенная пустая область
            }
            if (S == 0 || flag_detonation is true) GameOver_check(); // Если число флагов и мин совпадает -проверим достигнута ли победа
        }

        private void GameOver_check() //проверяем на достижение конца игры и чистим хвосты
        {
            int z = X * Y;
            for (int i = 0; i < X; i++)
                for (int j = 0; j < Y; j++)
                    if (buttonopened[i, j] is true) z--;
            if(flag_detonation is true) //взорвалась мина
            {
                gametime.mTimer.Stop();
                flag_detonation = false; // сбрасываем флаг взрыва
                for (int i = 0; i < X; i++)
                    for (int j = 0; j < Y; j++)
                    { if (buttonflags[i,j] is true && minespole[i,j]!=10) _buttons[i,j].Image= Image.FromFile(filePath5); //false flag
                        //if (_buttons[i, j].Visible is true) dispose_button(_buttons[i, j]); // открываем только неоткрытые кнопки
                        LButtons[i, j].BringToFront();
                    }
                flag_restart = true; // игра окончена
                return;
            }

            if (S == 0 && z == Z) // стоит максимальное кол-во флажков и количесво неоткрытых кнопок соответствует числу мин
            {
                gametime.mTimer.Stop();
                flag_restart = true; // игра окончена
                WIN.Visible = true;
                return;
            }
            if (S != 0 && z == Z) //Выиграли, т.к. все пустые кнопки открыты, хотя флажки не все стоят
            {
                gametime.mTimer.Stop();
                flag_restart = true; // игра окончена
                //поставим недоставленные флаги
                for (int i = 0; i < X; i++)
                    for (int j = 0; j < Y; j++)
                        if (buttonflags[i, j] is false && minespole[i, j] == 10) _buttons[i, j].Image = Image.FromFile(filePath4); //flag
                WIN.Visible = true;
                return;
            }
            if (S==0) //проверка на тот случай, когда все мины совпадают с проставленными флажками, а кнопки открыты еще не все
            {   //проверим совпадут ли мины c флажками
                int counter = 0;
                for (int i = 0; i < X; i++)
                    for (int j = 0; j < Y; j++)
                    {
                        if (buttonflags[i, j] is true && minespole[i, j] == 10) counter++;
                    }
                if (counter == Z) // мины c флажками совпали
                {
                    gametime.mTimer.Stop();
                    flag_restart = true; // игра окончена
                                         //откроем все неоткрытые кнопки
                    for (int i = 0; i < X; i++)
                        for (int j = 0; j < Y; j++)
                            if (_buttons[i, j].Visible is true) dispose_button(_buttons[i, j]); // открываем только неоткрытые кнопки
                    WIN.Visible = true;
                }
            }
        }
        private void WIN_message() // включить/выключить сообщение  статуса
        {
            if (WIN.Visible is false) WIN.Visible = true; else WIN.Visible = false;
        }
        private void setflag(int x, int y) // устанавливает/снимает флаг с кнопки
        {
            if (buttonflags[x, y] is false)
            {
                if (S == 0) return;// нельзя ставить флажков больше, чем есть мин по счетчику
                buttonflags[x, y] = true; _buttons[x, y].Image = Image.FromFile(filePath4);
                S--;
                gamecont.bombcounter = S; gamecont.Invalidate();
                if (S == 0) {  GameOver_check();  return; } //проверим - не достигнут ли конец игры
            }
            else 
            {  //убираем флаг, и прибавляем значение счетчика неотмеченных мин
                S++; gamecont.bombcounter = S; gamecont.Invalidate();
                _buttons[x, y].Image = null;
                buttonflags[x, y] = false;// массив с флагами на кнопках 
            }
        }

        private void button2_MouseDown(object sender, MouseEventArgs e) // обработка нажатий на лейблы
        {
            if (flag_restart is true) { NewGameINI(); flag_restart = false; }
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            if (flag_restart is true) { NewGameINI(); flag_restart = false; return; }
            int t,x,y;
            Button button = (Button)sender;
            t = (int)button.Tag;
            if (t == 1000) 
            {
                if (gametime.mTimer.Enabled is true) gametime.mTimer.Stop(); flag_restart = true; NewGameINI(); return;
            }
            x = t % X;
            y = t / X;
            if (e.Button==MouseButtons.Left) //нажата левая кнопка мыши
            {
                if(gametime.mTimer.Enabled is false)//timer1.Start(); //запускаем таймер,если еще этого не сделали
                {
                    gametime.mTimer.Start();
                }
                if (buttonflags[x, y] is true) return;  // на этой кнопке стоит флажек - не обрабатываем этот клик
                if (minespole[x, y] == 10) //ой, наступили на мину!
                { /*Игра окончена*/
                    //Controls.Remove(gametime);
                    gametime.mTimer.Stop();
                    //timer1.Stop();//останавливаем таймер - взрыв
                    flag_detonation = true; //ставим флаг, что нажата кнопка с миной
                }
                dispose_button(_buttons[x, y]);
                return;
            }
                    
            if (e.Button == MouseButtons.Right) //нажата правая кнопка мыши
                setflag(x, y);
        }
    }
    public class GifImage
    {   // we need the  System.Drawing.Imaging namespace.
        private Image gifImage;
        private FrameDimension dimension;
        private int frameCount;
        private int currentFrame = -1;
        private bool reverse;
        private bool repeat; // dont repeat playback
        private int step = 1;

        public GifImage(string path)
        {
            gifImage = Image.FromFile(path);
            //initialize
            dimension = new FrameDimension(gifImage.FrameDimensionsList[0]);
            //gets the GUID
            frameCount = gifImage.GetFrameCount(dimension);//всего кадров в анимации
        }
        public bool ReverseAtEnd
        {
            //whether the gif should play backwards when it reaches the end
            get { return reverse; }
            set { reverse = value; }
        }
        public bool Repeat
        {
            //whether the gif should play backwards when it reaches the end
            get { return repeat; }
            set { repeat = value; }
        }

        public Image GetNextFrame()
        {
            currentFrame += step;
            //if (currentFrame > frameCount) return null;//типа нехочу начинать с начала
            //if the animation reaches a boundary...
            if (currentFrame >= frameCount || currentFrame < 0)
            {
                if (reverse)
                {
                    step *= -1;
                    //...reverse the count
                    //apply it
                    currentFrame += step; 
                }
                else
                {
                    if (repeat == false)
                    {
                      return null;
                    }
                    currentFrame = 0;//...or start over
                }
            }
            return GetFrame(currentFrame);
        }

        public Image GetFrame(int index)
        {
            gifImage.SelectActiveFrame(dimension, index);
            //find the frame
            return (Image)gifImage.Clone();
            //return a copy of it
        }
    }
    public partial class Clock : UserControl ////////////////////////////////////////////////
    {
        public int bombcounter=1000; // Число бомб для индикатора/ Если=1000 -выводим таймер, иначе счетчик 
        public Timer mTimer;
        public DateTime currenttime = StaticData.currt; 
 
        bool showSlasher = true;
        public bool NightColorMode = StaticData.NightColorMode;
        public Clock()
        {
             mTimer = new Timer();
             mTimer.Interval = 1000;
             mTimer.Tick += new EventHandler(TimerTick);
             //mTimer.Start(); //Управляю таймером извне
            
        }
        private void TimerTick(object sender, EventArgs e)
        {
            Invalidate();
        }

        public int DigitWidth
        {
            get { return (width + 2 * Thickness); }
            set { width = value - 2 * Thickness; }
        }

        public int Thickness { get; set; } = 2;  // указываем  толщину сегмента
        int width = 8;                          //указываем  длину/высоту сегмента 

        Brush bkBrush = new SolidBrush(StaticData.colorschem1);     //цвет цифр
        Brush activeBrush = new SolidBrush(StaticData.colorschem2);// цвет неактивных индикаторов

        void DrawVerticalUpLeft(int x, int y, bool active, Graphics g)
        {
            Brush brush = (active ? activeBrush : bkBrush);
            int Th1 = Thickness / 2;
            int Th2 = Th1;
            if (Th1 * 2 < Thickness) Th1++;
            for (int i = 0; i < Th1; i++)//делаем заоваленные края
            {
                g.FillRectangle(brush, x + i, y + i - Th1 - (int)(Thickness / 4), 1, width + Th1 - (int)(Thickness / 4));
            }
            for (int i = 0; i < Th2; i++)
            {
                g.FillRectangle(brush, x + Th1 + i, y + i - (int)(Thickness / 4), 1, width - 2 * i + Th1 - (int)(Thickness / 4));
            }
        }
        void DrawVerticalUpRight(int x, int y, bool active, Graphics g)
        {
            Brush brush = (active ? activeBrush : bkBrush);
            int Th1 = Thickness / 2;
            int Th2 = Th1;
            if (Th1 * 2 < Thickness) Th1++;
            for (int i = 0; i < Th1; i++)//делаем заоваленные края
            {
                g.FillRectangle(brush, x + i, y - i + (int)(Thickness / 4), 1, width + 2 * i - Th1);
            }
            for (int i = 0; i < Th2; i++)
            {
                g.FillRectangle(brush, x + i + Th1, y - i - Th1 + (int)(Thickness / 4), 1, width + Th1);
            }
        }
        void DrawVerticalDownLeft(int x, int y, bool active, Graphics g)
        {
            Brush brush = (active ? activeBrush : bkBrush);
            int Th1 = Thickness / 2;
            int Th2 = Th1;
            if (Th1 * 2 < Thickness) Th1++;
            for (int i = 0; i < Th1; i++)//делаем заоваленные края
            {
                g.FillRectangle(brush, x + i, y - i + Th1, 1, width + (int)(Thickness / 4));
            }
            for (int i = 0; i < Th2; i++)
            {
                g.FillRectangle(brush, x + i + Th1, y + i, 1, width - 2 * i + (int)(Thickness / 4));
            }
        }
        void DrawVerticalDownRight(int x, int y, bool active, Graphics g)
        {
            Brush brush = (active ? activeBrush : bkBrush);
            int Th1 = Thickness / 2;
            int Th2 = Th1;
            if (Th1 * 2 < Thickness) Th1++;
            for (int i = 0; i < Th1; i++)//делаем заоваленные края
            {
                g.FillRectangle(brush, x + i, y - i + Th1, 1, width - Th1 + 2 * i - (int)(Thickness / 4));
            }
            for (int i = 0; i < Th2; i++)
            {
                g.FillRectangle(brush, x + i + Th1, y + i, 1, width + Th1 - (int)(Thickness / 4));
            }
        }

        void DrawHorizontalUp(int x, int y, bool active, Graphics g)
        {
            Brush brush = (active ? activeBrush : bkBrush);

            for (int i = 0; i < Thickness; i++) //делаем заоваленные края
            {
                g.FillRectangle(brush, x - (int)(Thickness / 2) + i, y + i, width + Thickness - 2 * i, 1);
            }
        }
        void DrawHorizontalDown(int x, int y, bool active, Graphics g)
        {
            Brush brush = (active ? activeBrush : bkBrush);

            for (int i = 0; i < Thickness; i++) //делаем заоваленные края
            {
                g.FillRectangle(brush, x - i + (int)(Thickness / 2), y + i, width - Thickness + 2 * i, 1);
            }
        }
        void DrawHorizontalMiddle(int x, int y, bool active, Graphics g)
        {
            Brush brush = (active ? activeBrush : bkBrush);
            int Th1 = Thickness / 2;
            int Th2 = Th1;
            if (Th1 * 2 < Thickness) Th1++;
            for (int i = 0; i < Th1; i++) //делаем заоваленные края
            {
                g.FillRectangle(brush, x - i + Th1, y + i, width - Thickness + 2 * i, 1);
            }
            for (int i = 0; i < Th2; i++)
            {
                g.FillRectangle(brush, x + i, y + Th1 + i, width - i * 2, 1);
            }
        }
        void DrawSlasher(bool activ, int x, int y, Graphics g)
        {
            Brush brush = (activ ? activeBrush : bkBrush);
            g.FillRectangle(brush, x, y + width, Thickness, Thickness); // верхняя точка
            g.FillRectangle(brush, x, y + width * 2 + Thickness, Thickness, Thickness); // нижняя точка
        }


        // a b c d e f g segments for digits from 0 to 9
        bool[] activeSegments = {
            true, true, true, true, true, true, false,
            false, true, true, false, false, false, false,
            true, true, false, true, true, false, true,
            true, true, true, true, false, false, true,
            false, true, true, false, false, true, true, //4
            true, false, true, true, false, true, true, //5
            true, false, true, true, true, true, true,
            true, true, true, false, false, false, false, //7
            true, true, true, true, true, true, true,
            true, true, true, false, false, true, true,
        };

        void DrawDigit(int digit, int x, int y, Graphics g)
        {
            //a -верхний горизонтальный                                                                         
            DrawHorizontalUp(x + Thickness, y, activeSegments[7 * digit + 0], g);
            //b - верхний правый вертикальный
            DrawVerticalUpRight(x + Thickness + width, y + Thickness, activeSegments[7 * digit + 1], g);
            //c - нижний правый вертикальный
            DrawVerticalDownRight(x + Thickness + width, y + 2 * Thickness + width, activeSegments[7 * digit + 2], g);
            //d - нижний горизонтальный
            DrawHorizontalDown(x + Thickness, y + 2 * width + 2 * Thickness, activeSegments[7 * digit + 3], g);
            //e -нижний левый вертикальный
            DrawVerticalDownLeft(x, y + 2 * Thickness + width, activeSegments[7 * digit + 4], g);
            //f -верхний левый вертикальный
            DrawVerticalUpLeft(x, y + Thickness, activeSegments[7 * digit + 5], g);
            //g - средний горизонтальный
            DrawHorizontalMiddle(x + Thickness, y + Thickness + width, activeSegments[7 * digit + 6], g);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            int x = 0, y = 0;
            if (bombcounter == 1000)// показываем время
            {
                DrawDigit(currenttime.Minute / 10, x, y, g);
                x += DigitWidth + Thickness;
                DrawDigit(currenttime.Minute % 10, x, y, g);
                x += DigitWidth + Thickness;
                x += DigitWidth / 4;
                DrawSlasher(showSlasher ? showSlasher = false : showSlasher = true, x, y, g); //мигающее двоеточие
                x += DigitWidth / 2;
                DrawDigit(currenttime.Second / 10, x, y, g);
                x += DigitWidth + Thickness;
                DrawDigit(currenttime.Second % 10, x, y, g);
                currenttime = currenttime.AddSeconds(1);//DateTime.Now;
                StaticData.currt = currenttime;
            }
            else //Показываем счетчик бомб
            {
                int d1 = (int)(bombcounter / 100);
                int d2 = (int)((bombcounter - d1*100) / 10);
                int d3 = bombcounter-d1*100-d2*10;
                DrawDigit(d1, x, y, g); // первый знак
                x += DigitWidth + Thickness;
                DrawDigit(d2, x, y, g); // второй знак
                x += DigitWidth + Thickness;
                DrawDigit(d3, x, y, g);
            }
        }
    }

    public class RoundButton : Control
    {
        public Color BackColor2 { get; set; }
        public Color ButtonBorderColor { get; set; }
        public int ButtonRoundRadius { get; set; }

        public Color ButtonHighlightColor { get; set; }
        public Color ButtonHighlightColor2 { get; set; }
        public Color ButtonHighlightForeColor { get; set; }

        public Color ButtonPressedColor { get; set; }
        public Color ButtonPressedColor2 { get; set; }
        public Color ButtonPressedForeColor { get; set; }

        private bool IsHighlighted;
        private bool IsPressed;

        public RoundButton()
        {
            Size = new Size(100, 40);
            ButtonRoundRadius = 30;
            BackColor = Color.Gainsboro;
            BackColor2 = Color.Silver;
            ButtonBorderColor = Color.Black;
            ButtonHighlightColor = Color.Orange;
            ButtonHighlightColor2 = Color.OrangeRed;
            ButtonHighlightForeColor = Color.Black;

            ButtonPressedColor = Color.Red;
            ButtonPressedColor2 = Color.Maroon;
            ButtonPressedForeColor = Color.White;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;
                createParams.ExStyle |= 0x00000020; // WS_EX_TRANSPARENT
                return createParams;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            var foreColor = IsPressed ? ButtonPressedForeColor : IsHighlighted ? ButtonHighlightForeColor : ForeColor;
            var backColor = IsPressed ? ButtonPressedColor : IsHighlighted ? ButtonHighlightColor : BackColor;
            var backColor2 = IsPressed ? ButtonPressedColor2 : IsHighlighted ? ButtonHighlightColor2 : BackColor2;


            using (var pen = new Pen(ButtonBorderColor, 1))
                e.Graphics.DrawPath(pen, Path);

            using (var brush = new LinearGradientBrush(ClientRectangle, backColor, backColor2, LinearGradientMode.Vertical))
                e.Graphics.FillPath(brush, Path);

            using (var brush = new SolidBrush(foreColor))
            {
                var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                var rect = ClientRectangle;
                rect.Inflate(-4, -4);
                e.Graphics.DrawString(Text, Font, brush, rect, sf);
            }

            base.OnPaint(e);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            IsHighlighted = true;
            Parent.Invalidate(Bounds, false);
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            IsHighlighted = false;
            IsPressed = false;
            Parent.Invalidate(Bounds, false);
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Parent.Invalidate(Bounds, false);
            Invalidate();
            IsPressed = true;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            Parent.Invalidate(Bounds, false);
            Invalidate();
            IsPressed = false;
        }

        protected GraphicsPath Path
        {
            get
            {
                var rect = ClientRectangle;
                rect.Inflate(-1, -1);
                return GetRoundedRectangle(rect, ButtonRoundRadius);
            }
        }

        public static GraphicsPath GetRoundedRectangle(Rectangle rect, int d)
        {
            var gp = new GraphicsPath();

            gp.AddArc(rect.X, rect.Y, d, d, 180, 90);
            gp.AddArc(rect.X + rect.Width - d, rect.Y, d, d, 270, 90);
            gp.AddArc(rect.X + rect.Width - d, rect.Y + rect.Height - d, d, d, 0, 90);
            gp.AddArc(rect.X, rect.Y + rect.Height - d, d, d, 90, 90);
            gp.CloseFigure();

            return gp;
        }
    }
}

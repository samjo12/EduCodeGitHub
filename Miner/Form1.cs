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


namespace Password_Generator
{ 
    public partial class Miner1 : Form
    {
        Random rnd = new Random();
          int nudMinerX = 10;
          int nudMinerY = 10;
          int nudComplicate = 12; //12% мин
          

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
            }
            else if (sender == MenuGameLevelMedium)
            {
                // Set the checkmark
                MenuGameLevelMedium.Checked = true;
                // Uncheck others menu items.
                MenuGameLevelEasy.Checked = false;
                MenuGameLevelHard.Checked = false;
                MenuGameLevelNightmare.Checked = false;
                nudComplicate = 19; //%мин
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
                nudComplicate = 26; // %
            }
            else //nightmare
            {
                MenuGameLevelEasy.Checked = false;
                MenuGameLevelMedium.Checked = false;
                MenuGameLevelHard.Checked = false;
                MenuGameLevelNightmare.Checked = true;
                nudComplicate = 34; // %
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
            // nudMinerX = 0; nudMinerY = 0; XSizePlayfield.Text = "Size X:"; YSizePlayfield.Text = "Size Y:";
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
            //
            //Пишем в переменную InboxData данные.
            //ВНИМАНИЕ! Сначала нужно записать данные в переменную, а затем вызывать метод загрузки данных (Show()). 
            //В противном случае мы не получим данные в дочерней форме
            //SF.X = nudMinerX.Value;
            //SF.Y = nudMinerY.Value;
            //this.BackColor = Color.Aquamarine;
            
          // проверяем что переменные nudMinerX и nudMinerY находятся в диапазоне 5..30
            if (nudMinerX < 5) nudMinerX = 5; 
            if (nudMinerX > 30) nudMinerX = 30;
            if (nudMinerY < 5) nudMinerY = 5;
            if (nudMinerY > 30) nudMinerY = 30;
            XSizePlayfield.Text = nudMinerX.ToString();
            YSizePlayfield.Text = nudMinerY.ToString(); 

          //  int buttonsCount = nudMinerX * nudMinerY;
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
                
               // if (XSizePlayfield.Text.Length > 0) nudMinerX = Convert.ToInt32(tmpstr); else nudMinerX = 0;
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
        private void tbSizePlayfield_FocusDown(object sender, EventArgs e) //если поля остаются полсле редактирования пустыми
        {
        }

    }

    public static class StaticData  //Статический класс, который выступает в качестве буфера для обмена данными между формами
    {
        //класс для обмена данными
        public static Decimal X = 0;
        public static Decimal Y = 0;
        public static Decimal S = 0;
    }
    public partial class Miner2 : Form // класс дочерней формы игрового поля
    {
        readonly Miner1 miner1; //создаем переменную со ссылкой на адрес класса1
        int X;
        int Y;
        int S;
        //Image pictureBox1 = Image.FromFile("C:/Users/usr/source/repos/Miner/mine55.gif");

       // Image pictureBox1 = Image.FromFile("C:/Users/amsad/source/EduCodeGitHub/Miner/mine55.gif");
       // Image pictureBox2 = Image.FromFile("C:/Users/amsad/source/EduCodeGitHub/Miner/mine55.gif");
     //   PictureBox pb1 = new PictureBox();
          
      private GifImage gifImage = null;
      private string filePath = @"C:\Users\usr\source\repos\Miner\mine55.gif";//
      private string path = @"C:\Users\amsad\source\EduCodeGitHub\Miner\mine55.gif";// C:\Users\usr\source\repos\Miner\mine55.gif";
      int index;


        DateTime date1 = new DateTime(0, 0);
        Timer timer1 = new Timer();
        Timer timerpic = new Timer();
        public Color LabelBackColour =Color.Azure;
        Label labelcont = new Label(); // счетчик мин
        Label labeltime = new Label();
        int W = 30; //размеры кнопок в пикселях -ширина
        int H = 30; //размеры кнопок в пикселях -высота
        int Z;      // исходное количество мин. ихначально равно S
        Button[,] _buttons = new Button[30, 30];
        Boolean[,] buttonflags = new Boolean[30, 30];
        Boolean[,] buttonopened = new Boolean[30, 30];

        public Label[,] LButtons = new Label[30, 30];
        Boolean flag_detonation = false;
        bool flag_restart = false;
        int[,] minespole = new int[30,30];
        Button Startbtn = new Button(); //кнопка рестарт
        Button Restartbtn = new Button();// После окончания игры создадим эту кнопку на все окно дря рестарта
        Button FlagSWbtn = new Button();// кнопка смена режимов клавищ мыши, меняем местами левую и правую кнопки мыши
        bool FlagSwitch = false; // по умолчанию, левая кнопка тыкает, а правая открывает ячейки
        Random rnd = new Random();

        public Miner2(Miner1 owner)
        {
            miner1 = owner;
            this.FormClosing += new FormClosingEventHandler(this.Miner2_FormClosing);// обработчик закрытия окна по крестику


            this.Text = "Take it Easy ...";

       

        X = Convert.ToInt32(StaticData.X);
            Y = Convert.ToInt32(StaticData.Y);
            S = Convert.ToInt32(Math.Round(StaticData.S*X*Y/100)); /*количество мин исходя из уровня сложности S%*(*X*Y)/100% */
            Z = S; // количество мин
            // вычисляем размер окна
            this.Width =40+Convert.ToInt32(X)*(W+1);
            this.Height = 70+Convert.ToInt32(Y)*(H+2);
            //this.Location = new Point();
            this.CenterToScreen(); // выводим форму с окном по центру экрана
            // оформим кнопки управления и счетчики мин и времени
            var labelfont = new Font("Arial", 16, FontStyle.Bold); // задаем шрифт -красный текст на черном фоне с центровкой
            
            labelcont.Width = 50;
            labelcont.Height = 25;
            labelcont.Visible = true;
           
            labelcont.ForeColor = Color.Red; //цвет шрифта
            labelcont.BackColor = Color.Black; // цвет фона
            labelcont.TextAlign = ContentAlignment.MiddleCenter;
            labelcont.Font = labelfont;
            labelcont.Text = S.ToString("000");
     
            labelcont.Location = new Point(20, 5);
            Controls.Add(labelcont);
            
            labeltime.Width = 70;
            labeltime.Height = 25;
            labeltime.Visible = true;

            labeltime.ForeColor = Color.Red; //цвет шрифта
            labeltime.BackColor = Color.Black; // цвет фона
            labeltime.TextAlign = ContentAlignment.MiddleCenter;
            labeltime.Font = labelfont;
            labeltime.Text = date1.ToString("mm:ss");
            labeltime.Location = new Point(this.Width-labeltime.Width-30,5);
            Controls.Add(labeltime);
            //Создаем кнопку переключения нажатий/режимы тык/флаг

            //BtnFocus.Focus();
            for (int i = 0; i < X; i++)
                for (int j = 0; j < Y; j++)
                {
                    var button = new Button();

                    button.Width = W;
                    button.Height = H; 
                    button.Location = new Point((button.Width) * i+1 + 20, (button.Height) * j+1 + 35); //20 и 35 - отступы слева и сверху
                    button.Text = "";
                    //button.FlatStyle = FlatStyle.Popup;
                    button.BackColor = Color.AntiqueWhite;
                    var buttonfont = new Font("Arial", 14, FontStyle.Bold);
                    button.Font = buttonfont;

                    button.Tag = j * X + i;// НЕ заминировано, записываем только порядковый номер кнопки
                    //button.Text = (j * X + i).ToString();
                    //button.Tag = 0; 
                    minespole[i, j] = 0; //инициализируем минное поле
                    buttonflags[i, j] = false; // инициаализацция массива флагов на кнопках
                    buttonopened[i, j] = false;//ini массива нажатых кнопок
                    //button.Visible = true;

                    _buttons[i, j] = button;
                    this.Controls.Add(_buttons[i, j]); //выводим кнопку с заданными ранее параметрами
                    this._buttons[i, j].MouseDown += new MouseEventHandler(this.button1_MouseDown); // вешаем на наши кнопки  обработчик нажатий  кнопок мыши
                   // this._buttons[i, j].Click += new System.EventHandler(this.button1_Click); // вешаем обработчик событий
                    this.timer1.Tick += new System.EventHandler(this.timer1_Tick); // создаем таймер
                    
                }

            for (int i =0; i<S; i++) // минируем поле, где S - кол-во мин
            {
                int x, y;
                do {
                    x = rnd.Next(0,X-1); 
                    y = rnd.Next(0,Y-1);
                } while (minespole[x, y]==10);
                minespole[x, y] = 10; //мина
            }
            for (int i = 0; i < X; i++)
                for (int j = 0; j < Y; j++)
                {
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
          
                    var labelfont1 = new Font("Arial", 14, FontStyle.Bold); // задаем шрифт -красный текст на черном фоне с центровкой
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
                        case 10: labelbutton.ForeColor = Color.Black; //рисуем мину
                                 labelbutton.Text = "*"; break;
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

                    labelbutton.Location = new Point(W * i + 20, H * j + 35);
                    labelbutton.Visible = true;

                    LButtons[i, j] = labelbutton;
                    this.Controls.Add(labelbutton);
                    
                    //labelbutton.BringToFront(); // вытащим на передний план

                    this.LButtons[i, j].MouseDown += new MouseEventHandler(this.button2_MouseDown); // вешаем на лейблы  обработчик нажатий  кнопок мыши
                }
            
        }
        void Miner2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
                e.Cancel = true;
            if(timer1!=null)timer1.Dispose(); if (timer1 != null) timer1 = null; //убиваем таймер, чтобы избежать артефактов при перезапуске
           for (int i = 0; i < X; i++)
                for (int j = 0; j<Y; j++)
                {
                    if (_buttons[i, j] != null) _buttons[i, j].Dispose();
                    if (LButtons[i, j] != null) LButtons[i, j].Dispose();
                }
            miner1.Visible = Enabled;
            this.Hide();
        }
        private void NewGameINI ()
        {
            S=Z; // количество мин
            flag_detonation = false;
            flag_restart = false;
            //timer1.Dispose(); timer1 = null; timer1 = new Timer();

            // this.timer1.Tick += new System.EventHandler(this.timer1_Tick); // создаем новый таймер
            date1 = new DateTime(0, 0);
            labelcont.Text = S.ToString("000"); //выводим на счетчик кол-во неоткрытых мин
            labeltime.Text = "00:00";//date1.ToString("mm:ss");
            var labelfont1 = new Font("Arial", 14, FontStyle.Bold);
            for (int i = 0; i < X; i++)
                for (int j = 0; j < Y; j++)
                {
                   _buttons[i,j].Visible = true; //показать кнопки
                    _buttons[i, j].Text = "";
                    minespole[i, j] = 0; //инициализируем минное поле
                    buttonflags[i, j] = false; // инициаализацция массива флагов на кнопках
                    buttonopened[i, j] = false;//ini массива нажатых кнопок
                    LButtons[i, j].Visible = true;
                    LButtons[i, j].Text = "";
                    LButtons[i, j].BackColor = LabelBackColour;
                    LButtons[i, j].Font = labelfont1;
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
                        case 10:
                            LButtons[i, j].ForeColor = Color.Black; //рисуем мину
                            LButtons[i, j].Text = "*"; break;
                        default: break; //это очищенная пустая область
                    }
                    // Создаем прототип лейбла с элементом минного поля
                }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            date1 = date1.AddMilliseconds(1);
            labeltime.Text = date1.ToString("mm:ss");
           // pictureBox1.Image = gifImage.GetNextFrame();
        }
        private void Explode_mine()
        {
            //PictureBox(,);
        }
        private void dispose_button(Button b) //Нажатие ... отключить кнопку и вывести вместо нее label
        {
            int t = (int)b.Tag;
            int x = t % X;
            int y = t / X;
            
            if (buttonflags[x, y] is true) return; // На клетке стоит флаг, ничего отключать не нужно
            if (buttonopened[x, y] is true) return; //Эта кнопка уже открыта .... выходим
            else buttonopened[x, y] = true; // отметим, что эта кнопка была нажата
            b.Visible = false;
            LButtons[x, y].Visible = true;
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
                case 1 - 9: /*LButtons[x,y].ForeColor = Color.Blue; */break;
                /*case 2: LButtons[x, y].ForeColor = Color.Green; break;
                case 3: LButtons[x, y].ForeColor = Color.Red; break;
                case 4: LButtons[x, y].ForeColor = Color.Navy; break;
                case 5: LButtons[x, y].ForeColor = Color.DeepPink; break;
                case 6: LButtons[x, y].ForeColor = Color.Brown; break;
                case 7: LButtons[x, y].ForeColor = Color.OrangeRed; break;
                case 8: LButtons[x, y].ForeColor = Color.Indigo; break;
                case 9: LButtons[x, y].ForeColor = Color.Yellow; break;*/
                case 10:LButtons[x, y].ForeColor = Color.Black; //цвет мины*/
                    
                    if (flag_detonation is true) // флаг, что нажал прямо в эту мину .... :(
                    {
                        LButtons[x, y].BackColor = Color.Red;// цвет фона разорвавшейся мины
                      LButtons[x, y].Font = new Font("Arial", 18, FontStyle.Bold);
                        LButtons[x,y].Text = "*";

                        //LButtons[x, y].Image = pictureBox1;
                        //for (int l=0;l<30;l++)for(int k=0;k<10;k++)LButtons[x, y].Image = gifImage.GetFrame(k);
                        Image gifImage = Image.FromFile(path);
                        FrameDimension dimension = new FrameDimension(gifImage.FrameDimensionsList[0]);
                        int frameCount = gifImage.GetFrameCount(dimension);
                        gifImage.SelectActiveFrame(dimension, index);//index);
                        //gifImage.GetFrame(10);
                        for (int k = 0; k < frameCount; k++)
                        { gifImage.SelectActiveFrame(dimension, k); LButtons[x, y].Image = gifImage; }// = pictureBox1;

                        /* SelectActiveFrame will return an integer that you do not necessarily need. 
                         * The important part is it will transform the image into only the selected frame.
                        Conclusion

                         There are a few things to keep in mind.The SelectActivateFrame C# function modifies 
                        the same Image object, in which case you need to call the Clone() method before returning the new frame.

                         The above C# class for displaying animated GIFs also shows a small sample of the possibities 
                        of manually extracting frames from a GIF. One being that the frames are displayed backwards 
                        when the animation reaches the end. The GetFrame C# function allows direct access to any frame, 
                        which opens to door to custom FPS (frames per second) display...*/
                    } 
                    break;
                default: GameOver_check(); return; //это очищенная пустая область
                
            }
            /*Control control = (Control)sender;
    var name = control.Name;
    MessageBox.Show(string.Format("I pressed this {0} and showed me this messagebox",name));*/
                        if (S == 0 || flag_detonation is true) GameOver_check(); // Если число флагов и мин совпадает -проверим достигнута ли победа
            //b.Visible=false;
            //LButtons[x, y].Visible = true;
        }


        private void GameOver_check() //проверяем на достижение конца игры и чистим хвосты
        {
            int z = X * Y;
            for (int i = 0; i < X; i++)
                for (int j = 0; j < Y; j++)
                    if (buttonopened[i, j] is true) z--;
            if(flag_detonation is true) //взорвалась мина
            {
                timer1.Stop(); 
                flag_detonation = false; // сбрасываем флаг взрыва
                for (int i = 0; i < X; i++)
                    for (int j = 0; j < Y; j++)
                        if (_buttons[i, j].Visible is true) dispose_button(_buttons[i, j]); // открываем только неоткрытые кнопки
                flag_restart = true; // игра окончена
                return;
            }

            if (S == 0 && z == Z) // стоит максимальное кол-во флажков и количесво неоткрытых кнопок соответствует числу мин
            {
                timer1.Stop();
                flag_restart = true; // игра окончена
                return;
            }
            if (S != 0 && z == Z) //Проигрыш
            {
                flag_restart = true; // игра окончена
                timer1.Stop();
                return;
            }
        }
        private void setflag(int x, int y, Boolean flag) // устанавливает/снимает флаг с кнопки
        {
            if (flag is true)
            {
                if (S == 0) return;// нельзя ставить флажков больше, чем есть мин по счетчику
                buttonflags[x, y] = flag;
                S--;_buttons[x, y].Text = "?";labelcont.Text = S.ToString("000");
                if (S == 0) {  GameOver_check();  return; } //проверим - не достигнут ли конец игры
                
            }
            else 
            {  //убираем флаг, и прибавляем значение счетчика неотмеченных мин
                _buttons[x, y].Text = ""; S++; labelcont.Text = S.ToString("000"); 
                buttonflags[x, y] = flag;// массив с флагами на кнопках 
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
            x = t % X;
            y = t / X;
            if (e.Button==MouseButtons.Left) //нажата левая кнопка мыши
            {
                //if (flag_detonation is true) { MessageBox.Show("GameOver_check"); }///////////// DEL ME
                if(timer1.Enabled is false)timer1.Start(); //запускаем таймер,если еще этого не сделали
                if (buttonflags[x, y] is true) return;  // на этой кнопке стоит флажек - не обрабатываем этот клик
                if (minespole[x, y] == 10) //ой, наступили на мину!
                { /*Игра окончена*/
                    timer1.Stop();//останавливаем таймер - взрыв
                    flag_detonation = true; //ставим флаг, что нажата кнопка с миной
                }
                dispose_button(_buttons[x, y]);
                //if (S == 0) { GameOver_check(); return; }
                return;
            }
                    
            if (e.Button == MouseButtons.Right) //нажата правая кнопка мыши
            { 
                if (buttonflags[x, y] is false)setflag(x, y, true); // ставим или снимаем флажек с кнопки
                else setflag(x, y, false);
                return;
            }
        }
    }
    public class GifImage
    {   // we need the  System.Drawing.Imaging namespace.
        private Image gifImage;
        private FrameDimension dimension;
        private int frameCount;
        private int currentFrame = -1;
        private bool reverse;
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

        public Image GetNextFrame()
        {

            currentFrame += step;

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
                    currentFrame = 0;
                    //...or start over
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
}

        
    



/*
            Clock my = new Clock();
            
            my.Bounds = new Rectangle(100, 100, 1000, 1000); // задаем область контрола с часами
            Controls.Add(my);
 */


    
    /*public static class StaticData
    {
        public static DateTime currt= new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,0,0,0 );
        public static bool stoptimer_flag = false;
    } */
    public partial class Clock : UserControl
    {
        private Timer mTimer;
        public DateTime currenttime = StaticData.currt;//new DateTime(0,0);
        // = new DateTime(0, 0);//DateTime.Now;
        bool showSlasher = true;
        public Clock()
        {
            mTimer = new Timer();
            mTimer.Interval = 1000;
            mTimer.Tick += new EventHandler(TimerTick);
            mTimer.Start();
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

        public int Thickness { get; set; } = 16;  // указываем высоту сегмента и толщину линий
        int width = 50; //length or height of segment

        Brush bkBrush = new SolidBrush(Color.FromArgb(35,30,31)); //White); 
        Brush activeBrush = new SolidBrush(Color.FromArgb(2,226,232));//Color.Red);

        void DrawVerticalUpLeft(int x, int y, bool active, Graphics g)
        {
            Brush brush = (active ? activeBrush : bkBrush);
            int Th1 = Thickness / 2;
            int Th2 = Th1;
            if (Th1 * 2 < Thickness) Th1++;
            for (int i = 0; i < Th1; i++)//делаем заоваленные края
            {
                g.FillRectangle(brush, x + i, y + i - Th1 - (int)(Thickness / 4), 1, width + Th1 -(int)(Thickness / 4));
            }
            for (int i = 0; i < Th2; i++)
            {
                g.FillRectangle(brush, x + Th1 + i, y + i - (int)(Thickness / 4), 1, width - 2*i+ Th1 - (int)(Thickness / 4));
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
                g.FillRectangle(brush, x + i + Th1, y - i -Th1 + (int)(Thickness / 4), 1, width + Th1 );
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
                g.FillRectangle(brush, x + i, y - i+Th1, 1, width + (int)(Thickness / 4));
            }
            for (int i = 0; i < Th2; i++)
            {
                g.FillRectangle(brush, x  + i + Th1, y + i, 1, width - 2 * i + (int)(Thickness / 4));
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
                g.FillRectangle(brush, x + i, y - i + Th1, 1, width -Th1 +2 * i - (int)(Thickness / 4));
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
                g.FillRectangle(brush, x -(int)(Thickness/2) + i , y + i, width +Thickness - 2*i, 1);
            }
        }
        void DrawHorizontalDown(int x, int y, bool active, Graphics g)
        {
            Brush brush = (active ? activeBrush : bkBrush);

            for (int i = 0; i < Thickness; i++) //делаем заоваленные края
            {
                g.FillRectangle(brush, x - i+ (int)(Thickness/2) , y + i, width-Thickness + 2 * i, 1);
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
                g.FillRectangle(brush, x-i+Th1 , y+i, width-Thickness+2*i, 1);
            }
            for (int i = 0; i < Th2; i++)
            {
                g.FillRectangle(brush, x+i, y+Th1+i, width-i*2, 1);
            }
        }
        void DrawSlasher(bool activ, int x, int y, Graphics g)
        {
            Brush brush = (activ ? activeBrush : bkBrush);
            g.FillRectangle(brush, x, y + width, Thickness, Thickness); // верхняя точка
            g.FillRectangle(brush, x, y + width*2 + Thickness, Thickness, Thickness); // нижняя точка
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
            if (StaticData.stoptimer_flag) { mTimer.Stop(); }
            currenttime = currenttime.AddSeconds(1);//DateTime.Now;
            StaticData.currt = currenttime;
            
            int x = 0, y = 0;

            DrawDigit(currenttime.Minute / 10, x, y, g);
            x += DigitWidth + Thickness;
            DrawDigit(currenttime.Minute % 10, x, y, g);
            x += DigitWidth + Thickness;
            x += DigitWidth / 4;
            DrawSlasher(showSlasher ? showSlasher=false:showSlasher=true, x, y, g); //мигающее двоеточие
            x += DigitWidth / 2;
            DrawDigit(currenttime.Second / 10, x, y, g);
            x += DigitWidth + Thickness;
            DrawDigit(currenttime.Second % 10, x, y, g);
        }
    }
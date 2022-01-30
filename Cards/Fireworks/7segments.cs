public partial class Clock : UserControl
    {
        private Timer mTimer;
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
 
        public int DigitWidth {
            get { return (width + 2 * Thickness);  }
            set { width = value - 2 * Thickness; }
            }
 
        public int Thickness { get; set; } = 5;
        int width = 35; //length or height of segment
 
        Brush bkBrush = new SolidBrush(Color.White);
        Brush activeBrush = new SolidBrush(Color.Red);
 
        void DrawVertical(int x, int y, bool active, Graphics g)
        {
            Brush brush = (active ? activeBrush : bkBrush);
            g.FillRectangle(brush, x, y, Thickness, width);
        }
        void DrawHorizontal(int x, int y, bool active, Graphics g)
        {
            Brush brush = (active ? activeBrush : bkBrush);
            g.FillRectangle(brush, x, y, width, Thickness);
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
            //a
            DrawHorizontal(x + Thickness, y, activeSegments[7 * digit + 0], g);
            //b
            DrawVertical(x + Thickness+width, y + Thickness, activeSegments[7 * digit + 1], g);
            //c
            DrawVertical(x + Thickness + width, y + 2*Thickness + width, activeSegments[7 * digit + 2], g);
            //d
            DrawHorizontal(x + Thickness, y + 2*width + 2*Thickness, activeSegments[7 * digit + 3], g);
            //e
            DrawVertical(x, y + 2 * Thickness + width, activeSegments[7 * digit + 4], g);
            //f
            DrawVertical(x, y + Thickness, activeSegments[7 * digit + 5], g);
            //g
            DrawHorizontal(x + Thickness, y + Thickness + width, activeSegments[7 * digit + 6], g);
 
        }
 
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
 
            DateTime currenttime = DateTime.Now;
 
            int x = 0, y = 0;
 
            DrawDigit(currenttime.Minute / 10, x, y, g);
            x += DigitWidth + Thickness;
            DrawDigit(currenttime.Minute % 10, x, y, g);
            x += DigitWidth + DigitWidth/2;
            DrawDigit(currenttime.Second / 10, x, y, g);
            x += DigitWidth + Thickness;
            DrawDigit(currenttime.Second % 10, x, y, g);
        }
    }
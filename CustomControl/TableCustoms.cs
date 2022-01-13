using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using System.Drawing.Drawing2D;


namespace CustomControl
{
    public class TableCustoms : Control
    {
        #region Перемененные
        public ObservableCollection<Column> Columns { get; set; } = new ObservableCollection<Column>();//Список столбцов таблицы
        private ObservableCollection<Row> rows = new ObservableCollection<Row>();//Список строк
        private int countRow = 0;//количество строк
        #endregion
        #region Свойства
        public int CountRow // гетер и сетер при увеличении переменной на N раз
        {
            get { return countRow; }
            set
            {
                //При увеличении добавляем 
                if (value > countRow)
                {
                    int iteration = value - countRow;
                    for (int i = 0; i < iteration; i++)
                    {
                        rows.Add(new Row());
                    }
                }
                //при уменьшении удаляем с конца
                if (value < countRow)
                {
                    int iteration = countRow - value;
                    for (int i = 0; i < iteration; i++)
                    {
                        rows.Remove(rows[rows.Count - 1]);
                    }
                }

                countRow = value;
            }
        }
        //гетер и сетер для списка строк, будет использоваться позже
        public ObservableCollection<Row> Rows
        {
            get { return rows; }
            set { }
        }

        public int ColumnHeaderHeigth { get; set; } = 20;//высота шапки таблицы
        public int RowHeaderWidth { get; set; } = 20;//высота заголовков строк
        public Color ColumnHeaderBack { get; set; } = SystemColors.Control;//Основной цвет фона заголовков таблицы
        public Color BorderColor { get; set; } = Color.Black;//Стандартный цвет границ таблицы
        public bool NumerableRows { get; set; } = false;//Флаг автоматической нумерации
        #endregion
        //Метода изменения столбцов, будет использоваться в следующем уроке
        private void EditColumn()
        {

        }
        //Метод изменения строк
        private void EditRows()
        {
            if (countRow < rows.Count)//Увеличение количества строк
            {
                rows[rows.Count - 1].Cells = CreatCells(Columns.Count);//Добавление пустых ячеек в строку
                countRow++;
            }
            if (CountRow > rows.Count)//уменьшение количества строк
            {
                countRow--;
            }

        }
        //метод создания N количества ячеек
        private List<Cell> CreatCells(int Count)
        {
            // return Enumerable.Repeat(new Cell(), Count).ToList();
            List<Cell> result = new List<Cell>();
            for (int i = 0; i < Count; i++)
            {
                result.Add(new Cell());
            }
            return result;
        }
        public TableCustoms()
        {
            rows.CollectionChanged += (e, v) => EditRows();//проверка изменения списка
            Columns.CollectionChanged += (e, v) => EditColumn();//проверка изменения списка
            BackColor = SystemColors.AppWorkspace;//Стандартный фон
            PanelTable panelTable = new PanelTable(this);//Создание основной панели
            panelTable.Dock = DockStyle.Fill;//Растягиваем основную панель по Control
            Controls.Add(panelTable);//Добавление панели на Control
        }
    }
    public class Column
    {
        public string Name { get; set; } = "NameColumn";//Наименование Столбца 
        public string Caption { get; set; } = "CaptionColumn";//Текст заголовка
        public int Width { get; set; } = 100;//Стандартная ширина
        public Color Back { get; set; } = Color.White;//Цвет фона

        public Column()
        {

        }
    }
    internal class PanelTable : ScrollableControl//Control со ScrolLbar
    {
        private TableCustoms BParent;//переменная основного класса, для работы с свойствами
        public PanelTable(TableCustoms bParent)
        {
            HScroll = true;//Отображение ползунка по горизонтали
            VScroll = true;//Отображение ползунка по вертикали
            AutoScroll = true;//Автоматическое появление полос прокрутки
            BParent = bParent;
        }
        //переопределение метода
        protected override void OnPaint(PaintEventArgs e)
        {
            Matrix m = new Matrix();
            m.Translate(this.AutoScrollPosition.X, this.AutoScrollPosition.Y, MatrixOrder.Append);
            e.Graphics.Transform = m;
            Graphics graf = e.Graphics;
            int maxWidth = 0;//Высота AutoScrollMinSize
            int maxHeight = 0;//Ширина AutoScrollMinSize
                              //расчитываем ширину
            foreach (Column item in BParent.Columns)
            {
                maxWidth += item.Width;
            }
            //расчитываем высоту
            foreach (Row item in BParent.Rows)
            {
                maxHeight += item.Heigth;
            }
            AutoScrollMinSize = new Size(maxWidth + 100, maxHeight + 100);//назначаем AutoScrollMinSize относительно этого будут появляться полосы прокрутки
            graf.Clear(BParent.BackColor);
            DrawHeaderColumns(graf);//Отрисовка заголовков столбцов таблицы
            DrawHeaderRows(graf);//Отрисовка заголовков строк таблицы
            DrawCells(graf);//Отрисовка ячеек
            base.OnPaint(e);
        }
        /// <summary>
        /// Отрисока заголовков столбцов
        /// </summary>
        /// <param name="graf"></param>
        private void DrawHeaderColumns(Graphics graf)
        {
            int x = 2;
            Rectangle rect;
            rect = new Rectangle(x, 1, BParent.RowHeaderWidth, BParent.ColumnHeaderHeigth);
            graf.DrawRectangle(new Pen(BParent.BorderColor), rect);
            graf.FillRectangle(new SolidBrush(BParent.ColumnHeaderBack), rect);
            x += BParent.RowHeaderWidth + 1;
            foreach (Column item in BParent.Columns)
            {
                rect = new Rectangle(x, 1, item.Width, BParent.ColumnHeaderHeigth);
                graf.DrawRectangle(new Pen(BParent.BorderColor), rect);
                graf.FillRectangle(new SolidBrush(BParent.ColumnHeaderBack), rect);
                if (item.Caption.Length != 0)
                {
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    graf.DrawString(item.Caption, new Font("Times", 9), Brushes.Black, rect, sf);
                }
                x += item.Width + 1;
            }
        }
        //Отрисовка заголовков строк
        private void DrawHeaderRows(Graphics graf)
        {
            int y = 1;
            int i = 0;
            Rectangle rect;
            y += BParent.RowHeaderWidth + 1;
            foreach (Row item in BParent.Rows)
            {
                rect = new Rectangle(2, y, BParent.RowHeaderWidth, item.Heigth);
                graf.DrawRectangle(new Pen(BParent.BorderColor), rect);
                graf.FillRectangle(new SolidBrush(BParent.ColumnHeaderBack), rect);
                if (BParent.NumerableRows)
                {
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    graf.DrawString(i.ToString(), new Font("Times", 9), Brushes.Black, rect, sf);
                }
                i++;
                y += item.Heigth + 1;
            }
        }
        //отрисовка ячеек
        private void DrawCells(Graphics graf)
        {

            int x = 2 + BParent.RowHeaderWidth + 1;
            int y = 2 + BParent.ColumnHeaderHeigth;
            Rectangle rect;
            int i = 0;
            foreach (Row itemRow in BParent.Rows)
            {
                foreach (Column itemColumn in BParent.Columns)
                {
                    rect = new Rectangle(x, y, itemColumn.Width, itemRow.Heigth);
                    graf.DrawRectangle(new Pen(BParent.BorderColor), rect);
                    graf.FillRectangle(new SolidBrush(Color.White), rect);
                    if (itemRow.Cells[i].Value != null)
                    {
                        StringFormat sf = new StringFormat();
                        sf.Alignment = StringAlignment.Center;
                        sf.LineAlignment = StringAlignment.Center;
                        graf.DrawString(itemRow.Cells[i].Value.ToString(), new Font("Times", 9), Brushes.Black, rect, sf);
                    }
                    x += itemColumn.Width + 1;
                    i++;
                }
                i = 0;
                y += itemRow.Heigth + 1;
                x = 2 + BParent.RowHeaderWidth + 1;
            }
        }
    }
    [Serializable]
    public class Row
    {
        public int Heigth { get; set; } = 20;// Высота строки
        public List<Cell> Cells { get; set; } = new List<Cell>();//список ячеек
        public Row()
        {

        }
        public Row(List<Cell> cells)
        {
            Cells = cells;
        }

    }
    [Serializable]
    public class Cell : ICloneable
    {
        public object Value { get; set; } = null;//значение ячейки 
        public Cell()
        {
        }
        public object Clone()
        {
            return MemberwiseClone();
        }
    }

}

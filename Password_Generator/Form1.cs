using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Password_Generator
{
    public partial class Form1 : Form
    {
        Random rnd = new Random();
        /*
Цифры[0..9]
Прописные буквы[a..z]
Строчные буквы[A..Z]
Спец.символы ! @ # $ _ / \ |
Скобки [ ] { } ( ) < >
Математ.знаки % ^ & * - + = ~
Знаки препинания ; : , . ` " ' ?
Пробел " " */
        int[,] cifer_symbols = new int[2,10];//набор кодов символов Ascii для цифр
        int[,] small_letters = new int[2, 26]; //набор кодов символов Ascii для маленьких букв
        int[,] big_letters =  new int[2,26];//набор кодов символов Ascii для больших букв
        int[,] special_symbols = new int[] { 33, 35, 36, 47, 64, 92, 95, 124 }; //! @ # $ _ / \ |
        int[,] bracket_symbols = new int[] { 40, 41, 60, 62, 91, 93, 123, 125 };
        int[,] math_symbols = new int[] { 37, 38, 42, 43, 45, 61, 94, 126 };
        int[,] special_symbols2 = new int[] { 34, 39, 44, 46, 58, 59, 63, 96 };
        
        Dictionary<string,double> metrica;
        
        public Form1()
        {
            InitializeComponent();
            clbPassSymbols.SetItemChecked(0,true);
            clbPassSymbols.SetItemChecked(1, true);
            clbPassSymbols.SetItemChecked(2, true);
            metrica = new Dictionary<string, double> ();
            metrica.Add("mm - millimeters",1);
            metrica.Add("cm - cantimeters", 10);
            metrica.Add("dm - decimeters", 100);
            metrica.Add("m - meters", 1000);
            metrica.Add("km - kilometers", 1000000);
            metrica.Add("ml - miles", 1609344);
        }

        private void btnCreatePass_Click(object sender, EventArgs e)
        {
            int passletter, nsymbs=0,i,j,unused_groups,rnd_tmp;
            string str_entropy;
            double entropy = 0;
            Boolean flag_SymIgnor;
            Boolean[] group_sym = new Boolean[clbPassSymbols.CheckedItems.Count]; //используемые в пароле группы символов

            for (i = 0; i < clbPassSymbols.CheckedItems.Count; i++) group_sym[i] = false; // initial
            pb1.Value = 0;// обнуляем прогрессбар
            string password = "";

            if (clbPassSymbols.CheckedItems.Count == 0) return; // никаких наборов символов для пароля не выбрано
                                                                // проверим , есть ли игнорируемые символы
             //список игнорируемых символов не пустой
                                             // Инициализация выбранных наборов символов
                for (i = 0; i < tbSymIgnor.Text.Length; i++)// переберем все запрещенные символы 
                {                                           // и отметим их в массивах символов
                    passletter = Convert.ToInt32(tbSymIgnor.Text[i]);
                    if (passletter >= 48 && passletter <=57)// встречен запрещенный символ в цифрах
                    { cifer_symbols[1,passletter - 48] = 1; } // отмечаем в массиве символов , что символ запрещен
                    if ( passletter>=65 && passletter<=90) // встречен символ из массива [A..Z]
                      big_letters[1,passletter - 90] = 1;                 
                    if ( passletter>=97 && passletter<=122) // встречен символ из массива [a..z]
                      small_letters[1,passletter - 90] = 1;
                }
                // дополнительно отключаем символы если нужно
                if (cbDisableLetter_O_I.Checked == true) // отмечена галка о запрете символов O и I
                      { big_letters[1, 73 - 65] = 1; big_letters[1, 79 - 65] = 1; /*отключаем буквы I и O*/};
                if (cbDisableLetter_o.Checked == true) // отмечена галка о запрете символа o
                      small_letters[1, 111 - 97] = 1;//отключаем букву o
                if (clbPassSymbols.GetItemChecked(0) is true)// группа цифр выбрана
                {
                    for (i = 0; i < 10; i++) // изначальна все цифры разрешены, кроме запрещенных
                    {
                        if (cifer_symbols[1, i] != 1) cifer_symbols[1, i] = 0;
                        cifer_symbols[0, i] = i + 48; //инициализируем символы цифр в массиве
                    }
                    flag_SymIgnor = false; //флаг наличия хотя бы одного символа в группе не находящегося под запретом

                    for (i = 0; i < 10; i++)
                    {
                        if (cifer_symbols[1, i] == 0)
                        { flag_SymIgnor = true;  }//в группе цифр есть как минимум один разрешенный символ
                    }
                    if (flag_SymIgnor == false)// цифры полностью запрещены
                    {  // отключим галочку выбора группы цифр
                        clbPassSymbols.SetItemChecked(0, false); return;
                    }
                    else { nsymbs += 10; }/*подсчитаем количество символов в алфавите пароля + [A..Z]*/
                }
                if (clbPassSymbols.GetItemChecked(1) is true)// группа букв [A..Z]
                {
                    for (i = 0; i < 26; i++) // изначальна все цифры разрешены, если группа цифр разрешена
                    {
                        if( big_letters[1, i] != 1) big_letters[1,i] = 0;
                        big_letters[0, i] = i + 65; //инициализируем символы цифр в массиве
                    }
                    flag_SymIgnor = false; //флаг наличия хотя бы одного символа в группе не находящегося под запретом

                    for (i = 0; i < 26; i++)
                    {
                        if (big_letters[1, i] == 0)
                        { flag_SymIgnor = true; }//в группе цифр есть как минимум один разрешенный символ
                    }
                    if (flag_SymIgnor == false)// группа [A..Z] полностью запрещена
                    {  // отключим галочку выбора группы [A..Z]
                        clbPassSymbols.SetItemChecked(1, false); return;
                    }
                    else { nsymbs += 26;  }/*подсчитаем количество символов в алфавите пароля + [A..Z]*/
                }
                if (clbPassSymbols.GetItemChecked(2) is true)// группа букв [a..z]
                {
                    for (i = 0; i < 26; i++) // изначальна все цифры разрешены, если группа цифр разрешена
                    {
                        if( small_letters[1, i] != 1) small_letters[1,i] = 0;
                        small_letters[0, i] = i + 97; //инициализируем символы цифр в массиве
                    }
                    flag_SymIgnor = false; //флаг наличия хотя бы одного символа в группе не находящегося под запретом

                    for (i = 0; i < 26; i++)
                    {
                        if (small_letters[1, i] == 0)
                        { flag_SymIgnor = true; }//в группе цифр есть как минимум один разрешенный символ
                    }
                    if (flag_SymIgnor == false)// группа [A..Z] полностью запрещена
                    {  // отключим галочку выбора группы [A..Z]
                        clbPassSymbols.SetItemChecked(2, false); return;
                    }
                    else { nsymbs += 26;  }/*подсчитаем количество символов в алфавите пароля + [A..Z]*/
                }
            

            for (i = 1; nudPassLength.Value >= i; i++) //цикл по длине пароля
            {
                int n = rnd.Next(0, clbPassSymbols.CheckedItems.Count); //случайно выбираем набор символов
                if (group_sym[n] == true) // в подборе уже была ипользована такая группа символов
                {
                    //подсчитаем сколько отмеченных груп символов было использовано
                    unused_groups = 0;
                    for (j = 0; j < clbPassSymbols.CheckedItems.Count; j++) if (group_sym[j] == false) unused_groups--;
                    if (unused_groups <= (clbPassSymbols.CheckedItems.Count - i)) { i--; continue; }
                }
                else { group_sym[n] = true; } /*отмечаем, что сейчас выбирем символ из этой группы*/
                string s = clbPassSymbols.CheckedItems[n].ToString(); //получаем его содержимое в строку s
                switch (s)
                {
                    case "Цифры [0..9]":
                        do { rnd_tmp = rnd.Next(10); }//выбираем случайную цифру
                        while (cifer_symbols[1,rnd_tmp] != 0);
                        passletter = cifer_symbols[0,rnd_tmp]; //цифра выбрана
                        break; 
                    case "Прописные буквы [A..Z]":
                        do { rnd_tmp = rnd.Next(26); }//выбираем случайную букву [A..Z]
                        while (big_letters[1, rnd_tmp] != 0);
                        passletter = big_letters[0, rnd_tmp]; //буква выбрана
                        break;
                    case "Строчные буквы [a..z]":
                        do { rnd_tmp = rnd.Next(26); }//выбираем случайную букву [a..z]
                        while (small_letters[1, rnd_tmp] != 0);
                        passletter = small_letters[0, rnd_tmp]; //буква выбрана
                        break;
                    case "Спец.символы ! @ # $ _ / |":
                        do
                        {
                           passletter = special_symbols[rnd.Next(special_symbols.Length)];
                           if (cbDisableUnderline.Checked == false) break;
                        } while (passletter is '_');
                        break;
                    case "Скобки [ ] { } ( ) < >":
                        passletter = bracket_symbols[rnd.Next(bracket_symbols.Length)];
                        break;
                    case "Математ.знаки % ^ & * - + = ~":
                        do
                        {
                           passletter = math_symbols[rnd.Next(math_symbols.Length)];
                           if (cbDisableMinus.Checked == false) break;
                        } while (passletter is '-');
                        break;
                    case "Символ пробела":
                        passletter = 0x20;
                        break;
                    default:
                        passletter = special_symbols2[rnd.Next(special_symbols2.Length)];
                        break;
                } //end of switch
                 

                if (tbSymIgnor.Text.Length != 0) //проверим полученный символ на вхождение в список запрещенных
                {
                    flag_SymIgnor = false;
                    for (j = 0; j < tbSymIgnor.Text.Length; j++)
                    {
                        if (tbSymIgnor.Text[j] == Convert.ToChar(passletter))// встречен запрещенный символ
                        {
                            i++; //перевыбор
                            flag_SymIgnor = true;
                            break;
                        }
                                               
                    }
                    if (flag_SymIgnor == true) { continue; } // выбираем следующий символ пароля в цикле i
                    else { password += Convert.ToChar(passletter); }
                }
                else { password += Convert.ToChar(passletter); }

            }
            tbPassword.Text = password; //выводим пароль в окно
            Clipboard.SetText(password); // записываем пароль в clipboard

            //подсчет числа символов,использующихся в пароле из всех отмеченных групп
            //if (clbPassSymbols.GetItemChecked(0) is true) nsymbs += 10; //цифры
            //if (clbPassSymbols.GetItemChecked(1) is true) nsymbs += 26; // [A..Z]
            //if (clbPassSymbols.GetItemChecked(2) is true) nsymbs += 26; // [a..z]
            if (clbPassSymbols.GetItemChecked(3) is true) nsymbs += 8; // спец.символы ! @ # $ _ / \ |
            if (clbPassSymbols.GetItemChecked(4) is true) nsymbs += 8; // скобки [ ] { } ( ) < >
            if (clbPassSymbols.GetItemChecked(5) is true) nsymbs += 8; // мат.символы % ^ & * - + = ~
            if (clbPassSymbols.GetItemChecked(6) is true) nsymbs += 8; // знаки препинания ; : , . ` " ' ?
            if (clbPassSymbols.GetItemChecked(7) is true) nsymbs += 1; // пробел
            // считаем сложность пароля в битах
            entropy = Math.Floor(Convert.ToDouble(nudPassLength.Value) * Math.Log2(nsymbs));
            str_entropy = Convert.ToString(entropy);

            if (entropy < 56)
            {
                tbPassForce.BackColor = Color.Red; tbPassForce.Text = " bits - очень слабый ";
                                pb1.Value = Convert.ToInt32(10 / 8 * entropy); }
            else if (entropy < 72) { tbPassForce.BackColor = Color.OrangeRed; tbPassForce.Text = " bits - слабый ";
                                     pb1.Value = Convert.ToInt32(10 / 8 * entropy); }//раскрашиваем полоску со сложностью пароля
            else if (entropy < 80) { tbPassForce.BackColor = Color.Orange; tbPassForce.Text = " bits - среднесложный "; 
                                     pb1.Value = Convert.ToInt32(10 / 8 * entropy); }
            else { tbPassForce.BackColor = Color.Green; tbPassForce.Text = " bits - хороший "; pb1.Value = 100; }//<=80bit -is Good
            tbPassForce.Text = str_entropy + tbPassForce.Text + "пароль";
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            
            double m1 = metrica[cbConverterFrom.Text];
            double m2 = metrica[cbConverterTo.Text];
            double n;
            if (tbConverterFrom.Text is "") return; //проверяем что входное значение не пустое
            
            try
            {
                n = Double.Parse(tbConverterFrom.Text);
            }
            catch (FormatException)
            {
                tbConverterTo.Text="Неверные данные !"; tbConverterFrom.Text = "1";
                return;
                
            }
            
            n = Convert.ToDouble(tbConverterFrom.Text); //читаем входное значение в виде числа
            if (Convert.ToInt64((n * m1 / m2)) == Convert.ToDouble(n * m1 / m2))
            { // вывод без дробной части
                if ((n * m1 / m2) < 9999999999) tbConverterTo.Text = String.Format("{0:F0}", (n * m1 / m2));
                else tbConverterTo.Text = String.Format("{0:e}", (n * m1 / m2));
            }
            else
            { 
                if ((n * m1 / m2) < 9999999999) tbConverterTo.Text = String.Format("{0:f8}", (n * m1 / m2)); 
                else tbConverterTo.Text = String.Format("{0:e}", (n * m1 / m2));
            }
  
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tmp_swap=cbConverterFrom.Text;
            cbConverterFrom.Text = cbConverterTo.Text;
            cbConverterTo.Text = tmp_swap;
        }

        private void cbConverterMetrica_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbConverterMetrica.Text)
            {
                case "Длины":
                    metrica.Clear();
                    metrica.Add("mm - millimeters", 1);
                    metrica.Add("cm - cantimeters", 10);
                    metrica.Add("dm - decimeters", 100);
                    metrica.Add("m - meters", 1000);
                    metrica.Add("km - kilometers", 1000000);
                    metrica.Add("ml - miles", 1609344);
                    cbConverterFrom.Items.Clear();
                    cbConverterFrom.Items.Add("mm - millimeters");
                    cbConverterFrom.Items.Add("cm - cantimeters");
                    cbConverterFrom.Items.Add("dm - decimeters");
                    cbConverterFrom.Items.Add("m - meters");
                    cbConverterFrom.Items.Add("km - kilometers");
                    cbConverterFrom.Items.Add("ml - miles");
                    cbConverterFrom.Text = "mm - millimeters";
                    cbConverterTo.Items.Clear();
                    cbConverterTo.Items.Add("mm - millimeters");
                    cbConverterTo.Items.Add("cm - cantimeters");
                    cbConverterTo.Items.Add("dm - decimeters");
                    cbConverterTo.Items.Add("m - meters");
                    cbConverterTo.Items.Add("km - kilometers");
                    cbConverterTo.Items.Add("ml - miles");
                    cbConverterTo.Text = "mm - millimeters";
                    break;

                case "Веса":
                    metrica.Clear();
                    metrica.Add("g - gramms", 1);
                    metrica.Add("kg - kilogramms", 1000);
                    metrica.Add("dt - decitonns", 100000);
                    metrica.Add("t - tonns", 1000000);
                    metrica.Add("lb - pounds", 453.59237);
                    metrica.Add("oz - uncias", 28.349523125);
                    cbConverterFrom.Items.Clear();
                    cbConverterFrom.Items.Add("g - gramms");
                    cbConverterFrom.Items.Add("kg - kilogramms");
                    cbConverterFrom.Items.Add("dt - decitonns");
                    cbConverterFrom.Items.Add("t - tonns");
                    cbConverterFrom.Items.Add("lb - pounds");
                    cbConverterFrom.Items.Add("oz - uncias");
                    cbConverterFrom.Text = "g - gramms";
                    cbConverterTo.Items.Clear();
                    cbConverterTo.Items.Add("g - gramms");
                    cbConverterTo.Items.Add("kg - kilogramms");
                    cbConverterTo.Items.Add("dt - decitonns");
                    cbConverterTo.Items.Add("t - tonns");
                    cbConverterTo.Items.Add("lb - pounds");
                    cbConverterTo.Items.Add("oz - uncias");
                    cbConverterTo.Text = "g - gramms";
                    break;
                default:
                    break;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void nudPassLength_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}


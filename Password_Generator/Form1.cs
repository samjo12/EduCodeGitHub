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
        int[] special_symbols = new int[] { 33, 64, 35, 36, 95, 47, 92, 124 };
        int[] bracket_symbols = new int[] { 91, 93, 123, 125, 40, 41, 60, 62 };
        int[] math_symbols = new int[] { 37, 94, 38, 42, 45, 43, 61, 126 };
        int[] special_symbols2 = new int[] { 59, 58, 44, 46, 96, 34, 39, 63 };
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
            int passletter, nsymbs=0;
            string str_entropy;
            double entropy = 0;
            pb1.Value = 0;// обнуляем прогрессбар
            if (clbPassSymbols.CheckedItems.Count == 0) return; // никаких наборов символов в пароле не выбрано
            string password = "";
            for (int i = 1; nudPassLength.Value >= i; i++) //цикл по длине пароля
            {
                int n = rnd.Next(0, clbPassSymbols.CheckedItems.Count); //случайно выбираем набор символов
                string s = clbPassSymbols.CheckedItems[n].ToString(); //получаем его содержимое в строку s
                switch (s)
                {
                    case "Цифры [0..9]":
                        password += rnd.Next(10).ToString();
                        break; 
                    case "Прописные буквы [A..Z]":
                        do 
                        { passletter = rnd.Next(65, 90);
                            if (cbDisableLetter_O_I.Checked == false) break;
                        } while ((passletter is 'O') || (passletter is 'I'));
                        password += Convert.ToChar(passletter);
                        break;
                    case "Строчные буквы [a..z]":
                        do 
                        { passletter = rnd.Next(97, 122);
                            if (cbDisableLetter_o.Checked == false) break;
                        } while (passletter is 'o'); 
                        password += Convert.ToChar(passletter);
                        entropy += 4.7004;
                        break;
                    case "Спец.символы ! @ # $ _ / |":
                        do
                        {
                          passletter = special_symbols[rnd.Next(special_symbols.Length)];
                            if (cbDisableUnderline.Checked == false) break;
                        } while (passletter is '_');
                        password += Convert.ToChar(passletter);
                        break;
                    case "Скобки [ ] { } ( ) < >":
                        passletter = bracket_symbols[rnd.Next(bracket_symbols.Length)];
                        password += Convert.ToChar(passletter);
                        break;
                    case "Математ.знаки % ^ & * - + = ~":
                        do
                        {
                            passletter = math_symbols[rnd.Next(math_symbols.Length)];
                            if (cbDisableMinus.Checked == false) break;
                        } while (passletter is '-');
                        password += Convert.ToChar(passletter);
                        break;
                    case "Символ пробела":
                        password += " ";
                        break;
                    default:
                        passletter = special_symbols2[rnd.Next(special_symbols2.Length)];
                        password += Convert.ToChar(passletter);
                        break;
                }
                
            }
            tbPassword.Text = password;
            Clipboard.SetText(password);
            /*расчет силы пароля и вывод результата*/

            if (clbPassSymbols.GetItemChecked(0) is true) nsymbs += 10; //цифры
            if (clbPassSymbols.GetItemChecked(1) is true) nsymbs += 26; // [A..Z]
            if (clbPassSymbols.GetItemChecked(2) is true) nsymbs += 26;// [a..z]
            if (clbPassSymbols.GetItemChecked(3) is true) nsymbs += 8;// спец.символы ! @ # $ _ / \ |
            if (clbPassSymbols.GetItemChecked(4) is true) nsymbs += 8; // скобки [ ] { } ( ) < >
            if (clbPassSymbols.GetItemChecked(5) is true) nsymbs += 8;// мат.символы % ^ & * - + = ~
            if (clbPassSymbols.GetItemChecked(6) is true) nsymbs += 8; // знаки препинания ; : , . ` " ' ?
            if (clbPassSymbols.GetItemChecked(7) is true) nsymbs += 1; // пробел
            entropy = Math.Floor(Convert.ToDouble(nudPassLength.Value) * Math.Log2(nsymbs));
            str_entropy = Convert.ToString(entropy); // считаем сложность пароля
            //tbPassForce.Text = Convert.ToString(entropy) + " bits";
            if (entropy < 56) { tbPassForce.BackColor = Color.Red; tbPassForce.Text = str_entropy + " bits - cлишком слабый";
                                pb1.Value = Convert.ToInt32(10 / 8 * entropy); }
            else if (entropy < 72) { tbPassForce.BackColor = Color.OrangeRed; tbPassForce.Text = str_entropy + " bits - слабый";
                                     pb1.Value = Convert.ToInt32(10 / 8 * entropy); }//раскрашиваем полоску со сложностью пароля
            else if (entropy < 80) { tbPassForce.BackColor = Color.Orange; tbPassForce.Text = str_entropy + " bits - средний"; 
                                     pb1.Value = Convert.ToInt32(10 / 8 * entropy); }
            else { tbPassForce.BackColor = Color.Green; tbPassForce.Text = str_entropy + " bits - хороший!";pb1.Value = 100; }//<=80bit -is Good

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


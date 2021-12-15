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
        char[] special_symbols = new char[] { '%', '*', ')', '?', '#', '$', '^', '&', '^' };
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
                    case "Прописные буквы [a..z]":
                        password += Convert.ToChar(rnd.Next(65, 88));
                        break;
                    case "Строчные буквы [A..Z]":
                        password += Convert.ToChar(rnd.Next(97, 122));
                        break;
                    default: 
                        password += special_symbols[rnd.Next(special_symbols.Length)];
                        break;
                }
                tbPassword.Text = password;
                Clipboard.SetText(password);
            }
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
    }
}


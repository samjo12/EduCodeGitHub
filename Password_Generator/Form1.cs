﻿using System;
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
 

        Dictionary<string, double> metrica;

        public Form1()
        {
            InitializeComponent();
            clbPassSymbols.SetItemChecked(0, true);
            clbPassSymbols.SetItemChecked(1, true);
            clbPassSymbols.SetItemChecked(2, true);
            metrica = new Dictionary<string, double>();
            metrica.Add("mm - millimeters", 1);
            metrica.Add("cm - cantimeters", 10);
            metrica.Add("dm - decimeters", 100);
            metrica.Add("m - meters", 1000);
            metrica.Add("km - kilometers", 1000000);
            metrica.Add("ml - miles", 1609344);
        }
       

        
        private void Message_for_user(string s)
        { /* вывод сообщений об ршибках из генератора паролей*/
            tbPassword.Text = s;
            tbPassForce.BackColor = Color.LightGray; tbPassForce.Text = "";

            Size len = TextRenderer.MeasureText(tbPassword.Text, tbPassword.Font);
            /*while(len.Width>tbPassword.Size.Width)
                {
                tbPassword.Font.Size.;
                }*/
            return;
        }

        private void btnCreatePass_Click(object sender, EventArgs e)
        {
            int passletter = 0, nsymbs = 0, i, j, unused_groups, rnd_tmp, n_group, n_wanted_words = 0;
            int len_wanted_words = 0;//длина в символах всех желаемых слов
            string str_entropy, s;
            string[] wanted_words = new string[70];
            double entropy;
            Boolean flag_No_space_sym = false; //Если true - отключим символ пробела и снимем галочку
            Boolean flag_SymIgnor;
            Boolean flag_wanted_words = false;
            /*
Цифры[0..9]
Прописные буквы[a..z]
Строчные буквы[A..Z]
Спец.символы ! @ # $ _ / \ |
Скобки [ ] { } ( ) < >
Математ.знаки % ^ & * - + = ~
Знаки препинания ; : , . ` " ' ?
Пробел " " */
            int[,] cifer_symbols = new int[2, 10];//набор кодов символов Ascii для цифр
            int[,] small_letters = new int[2, 26]; //набор кодов символов Ascii для маленьких букв
            int[,] big_letters = new int[2, 26];//набор кодов символов Ascii для больших букв
            int[,] special_symbols = new int[2, 8] { { 33, 35, 36, 47, 64, 92, 95, 124 }, { 0, 0, 0, 0, 0, 0, 0, 0 } }; //! @ # $ _ / \ |
            int[,] bracket_symbols = new int[2, 8] { { 40, 41, 60, 62, 91, 93, 123, 125 }, { 0, 0, 0, 0, 0, 0, 0, 0 } };
            int[,] math_symbols = new int[2, 8] { { 37, 38, 42, 43, 45, 61, 94, 126 }, { 0, 0, 0, 0, 0, 0, 0, 0 } };
            int[,] special_symbols2 = new int[2, 8] { { 34, 39, 44, 46, 58, 59, 63, 96 }, { 0, 0, 0, 0, 0, 0, 0, 0 } };

            pb1.Value = 0;// обнуляем прогрессбар
            string password = "";

            if (clbPassSymbols.CheckedItems.Count == 0) return; // никаких наборов символов для пароля не выбрано



            //переберем список игнорируемых символов 

            for (i = 0; i < tbSymIgnor.Text.Length; i++)// переберем все запрещенные символы 
            {                                           // и отметим их в массивах символов 1
                passletter = Convert.ToInt32(tbSymIgnor.Text[i]);
                if (passletter >= 48 && passletter <= 57)// встречен запрещенный символ в цифрах
                { cifer_symbols[1, passletter - 48] = 1; } // отмечаем в массиве символов , что символ запрещен
                if (passletter >= 65 && passletter <= 90) // встречен символ из массива [A..Z]
                    big_letters[1, passletter - 65] = 1;
                if (passletter >= 97 && passletter <= 122) // встречен символ из массива [a..z]
                    small_letters[1, passletter - 97] = 1;
                for (j = 0; j < 8; j++) if (passletter == special_symbols[0, j]) special_symbols[1, j] = 1; //проверяем отмечены ли специальные символы
                for (j = 0; j < 8; j++) if (passletter == bracket_symbols[0, j]) bracket_symbols[1, j] = 1; //проверяем отмечены ли  символы скобок
                for (j = 0; j < 8; j++) if (passletter == special_symbols2[0, j]) special_symbols2[1, j] = 1; //проверяем отмечены ли символы препинания
                for (j = 0; j < 8; j++) if (passletter == math_symbols[0, j]) math_symbols[1, j] = 1; //проверяем отмечены ли математические символы
                if (passletter == 32) //найден пробел, отключаем группу символа пробела
                { flag_No_space_sym = true; }
            }
            // дополнительно отключаем символы запрещенные в чекбоксах, если нужно
            if (cbDisableLetter_O_I.Checked == true) // отмечена галка о запрете символов O и I
            { big_letters[1, 73 - 65] = 1; big_letters[1, 79 - 65] = 1; } /*отключаем буквы I и O*/
            if (cbDisableLetter_o.Checked == true) // отмечена галка о запрете символа o
                small_letters[1, 111 - 97] = 1;  //отключаем букву o
            if (cbDisableUnderline.Checked == true)//отметка о запрете символа _ 
                special_symbols[1, 6] = 1; //отключаем символ _
            if (cbDisableMinus.Checked == true)//отметка о запрете символа -
                math_symbols[1, 4] = 1; //отключаем минус

            if (clbPassSymbols.GetItemChecked(0) is true)// группа цифр выбрана
            {
                flag_SymIgnor = false; //флаг наличия хотя бы одного символа в группе не находящегося под запретом

                for (i = 0; i < 10; i++) // изначальна все цифры разрешены, кроме запрещенных
                {
                    if (cifer_symbols[1, i] != 1) { cifer_symbols[1, i] = 0; flag_SymIgnor = true; }

                    cifer_symbols[0, i] = i + 48; //инициализируем символы цифр в массиве
                }
                if (flag_SymIgnor == false)// цифры полностью запрещены
                {  // отключим галочку выбора группы цифр
                    clbPassSymbols.SetItemChecked(0, false); return;
                }
                else { nsymbs += 10; }/*подсчитаем количество символов в алфавите пароля + [0..9]*/
            }
            if (clbPassSymbols.GetItemChecked(1) is true)// группа букв [A..Z]
            {
                flag_SymIgnor = false; //флаг наличия хотя бы одного символа в группе не находящегося под запретом
                for (i = 0; i < 26; i++) // изначальна все цифры разрешены, если группа цифр разрешена
                {
                    if (big_letters[1, i] != 1) { big_letters[1, i] = 0; flag_SymIgnor = true; }
                    big_letters[0, i] = i + 65; //инициализируем символы цифр в массиве
                }

                if (flag_SymIgnor == false)// группа [A..Z] полностью запрещена
                {  // отключим галочку выбора группы [A..Z]
                    clbPassSymbols.SetItemChecked(1, false); return;
                }
                else { nsymbs += 26; }/*подсчитаем количество символов в алфавите пароля + [A..Z]*/
            }

            if (clbPassSymbols.GetItemChecked(2) is true)// группа букв [a..z]
            {
                flag_SymIgnor = false; //флаг наличия хотя бы одного символа в группе не находящегося под запретом
                for (i = 0; i < 26; i++) // изначальна все цифры разрешены, если группа цифр разрешена
                {
                    if (small_letters[1, i] != 1) { small_letters[1, i] = 0; flag_SymIgnor = true; }
                    small_letters[0, i] = i + 97; //инициализируем символы цифр в массиве
                }

                if (flag_SymIgnor == false)// группа [A..Z] полностью запрещена
                {  // отключим галочку выбора группы [A..Z]
                    clbPassSymbols.SetItemChecked(2, false); return;
                }
                else { nsymbs += 26; }/*подсчитаем количество символов в алфавите пароля + [a..z]*/
            }

            if (clbPassSymbols.GetItemChecked(3) is true)// группа спец символов ! @ # $ _ / \ |
            {
                flag_SymIgnor = false; //флаг наличия хотя бы одного символа в группе не находящегося под запретом
                for (i = 0; i < 8; i++)
                {
                    if (special_symbols[1, i] == 0)
                    { flag_SymIgnor = true; break; }//в группе цифр есть как минимум один разрешенный символ
                }
                if (flag_SymIgnor == false)// группа спецсимволов полностью запрещена
                {  // отключим галочку выбора группы спецсимволов
                    clbPassSymbols.SetItemChecked(3, false); return;
                }
                else { nsymbs += 8; }/*подсчитаем количество символов в алфавите пароля + ! @ # $ _ / \ |*/
            }

            if (clbPassSymbols.GetItemChecked(4) is true)// группа символов скобок [ ] { } ( ) < >
            {
                flag_SymIgnor = false; //флаг наличия хотя бы одного символа в группе не находящегося под запретом
                for (i = 0; i < 8; i++)
                {
                    if (bracket_symbols[1, i] == 0)
                    { flag_SymIgnor = true; break; }//в группе цифр есть как минимум один разрешенный символ
                }
                if (flag_SymIgnor == false)// группа спецсимволов полностью запрещена
                {  // отключим галочку выбора группы спецсимволов
                    clbPassSymbols.SetItemChecked(4, false); return;
                }
                else { nsymbs += 8; }/*подсчитаем количество символов в алфавите пароля + [ ] { } ( ) < >*/
            }

            if (clbPassSymbols.GetItemChecked(5) is true)// группа математических символов  % ^ & * - + = ~
            {
                flag_SymIgnor = false; //флаг наличия хотя бы одного символа в группе не находящегося под запретом
                for (i = 0; i < 8; i++)
                {
                    if (math_symbols[1, i] == 0)
                    { flag_SymIgnor = true; break; }//в группе цифр есть как минимум один разрешенный символ
                }
                if (flag_SymIgnor == false)// группа спецсимволов полностью запрещена
                {  // отключим галочку выбора группы спецсимволов
                    clbPassSymbols.SetItemChecked(5, false); return;
                }
                else { nsymbs += 8; }/*подсчитаем количество символов в алфавите пароля + % ^ & * - + = ~*/
            }

            if (clbPassSymbols.GetItemChecked(6) is true)// группа символов препинания ; : , . ` " ' ?
            {
                flag_SymIgnor = false; //флаг наличия хотя бы одного символа в группе не находящегося под запретом
                for (i = 0; i < 8; i++)
                {
                    if (special_symbols2[1, i] == 0)
                    { flag_SymIgnor = true; break; }//в группе цифр есть как минимум один разрешенный символ
                }
                if (flag_SymIgnor == false)// группа спецсимволов полностью запрещена
                {  // отключим галочку выбора группы спецсимволов
                    clbPassSymbols.SetItemChecked(6, false); return;
                }
                else { nsymbs += 8; }/*подсчитаем количество символов в алфавите пароля + ; : , . ` " ' ?*/
            }
            if (clbPassSymbols.GetItemChecked(7) is true)// пробел разрешен
            {
                nsymbs += 1;
            }

            if (tbMyStr.TextLength != 0) // присутствуют желаемые слова и выражения
            { // проверим строчку на количество слов
                char[] delimiterChars = { ' ', ',', '.', ':' }; //возможные символы разделители
                string[] words = tbMyStr.Text.Split(delimiterChars); //соскладируем желаемые слова в массив
                foreach (string word in words)
                {
                    wanted_words[n_wanted_words] = word;
                    len_wanted_words += word.Length;
                    n_wanted_words++;
                }

                if ((len_wanted_words + clbPassSymbols.CheckedItems.Count) > nudPassLength.Value)
                {// длина желаемых слов и символов из отмеченных групп превышает выбранную длину пароля
                    tbPassword.Text = "Длина пароля мала для выбранного набора символов";
                    nudPassLength.Value = len_wanted_words + clbPassSymbols.CheckedItems.Count; //корректируем длину пароля
                    tbPassForce.BackColor = Color.LightGray; tbPassForce.Text = "";
                    return; // показываем пользователю новую длину пароля
                }


            }
            Boolean[] group_sym = new Boolean[clbPassSymbols.CheckedItems.Count + n_wanted_words]; //используемые в пароле группы символов

            for (i = 0; i < (clbPassSymbols.CheckedItems.Count + n_wanted_words); i++) group_sym[i] = false; // initial group_sym
                                                                                                             // отмечаем отмеченные группы символов и желаемые слова как неиспользованные группы
            for (i = 1; i <= nudPassLength.Value; i++) //цикл по длине пароля /подбор
            {

                n_group = rnd.Next(0, clbPassSymbols.CheckedItems.Count + n_wanted_words); //случайно выбираем группу символов учитывая группы с желаемыми словами

                if (group_sym[n_group] == true) // в подборе пароля уже была ипользована такая группа символов
                {
                    //подсчитаем сколько отмеченных груп символов Небыло использовано
                    unused_groups = 0;
                    for (j = 0; j < (clbPassSymbols.CheckedItems.Count); j++) if (group_sym[j] == false) unused_groups++;
                    for (j = 0; j < n_wanted_words; j++) if (group_sym[j + clbPassSymbols.CheckedItems.Count] == false) unused_groups += wanted_words[j].Length;

                    if ((nudPassLength.Value - i) < unused_groups) { i--; continue; }
                    if (n_group >= clbPassSymbols.CheckedItems.Count) { i--; continue; } //желаемое слово не повторяем дважды
                }
                else { group_sym[n_group] = true; }/*отмечаем, что сейчас выбeрем символ из этой группы*/

                if (n_group < clbPassSymbols.CheckedItems.Count) s = clbPassSymbols.CheckedItems[n_group].ToString(); //получаем его содержимое в строку s
                else s = "Wanted"; // прописываем строку в switch для default:
                switch (s)
                {
                    case "Цифры [0..9]":
                        do { rnd_tmp = rnd.Next(10); }//выбираем случайную цифру
                        while (cifer_symbols[1, rnd_tmp] != 0);
                        passletter = cifer_symbols[0, rnd_tmp]; //цифра выбрана
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
                        do { rnd_tmp = rnd.Next(8); }
                        while (special_symbols[1, rnd_tmp] != 0);
                        passletter = special_symbols[0, rnd_tmp];
                        break;
                    case "Скобки [ ] { } ( ) < >":
                        do { rnd_tmp = rnd.Next(8); }
                        while (bracket_symbols[1, rnd_tmp] != 0);
                        passletter = bracket_symbols[0, rnd_tmp];
                        break;
                    case "Математ.знаки % ^ & * - + = ~":
                        do { rnd_tmp = rnd.Next(8); }
                        while (math_symbols[1, rnd_tmp] != 0);
                        passletter = math_symbols[0, rnd_tmp];
                        break;
                    case "Символ пробела":
                        if (flag_No_space_sym is true) { clbPassSymbols.SetItemChecked(7, false); Message_for_user("Вы добавили пробел в список неиспользуемых символов"); return; }
                        if ((i == 1 || i == nudPassLength.Value) || (i > 1 && password[password.Length - 1] == 32)) { i--; continue; } //не может быть первым и последним символом в пароле или двойным
                        passletter = 32;
                        break;
                    case "Знаки препинания  ; : , . ? кавычки":
                        do { rnd_tmp = rnd.Next(8); }
                        while (special_symbols2[1, rnd_tmp] != 0);
                        passletter = special_symbols2[0, rnd_tmp];
                        break;
                    default: // вставка желаемых слов
                        flag_wanted_words = true;
                        break;
                } //end of switch

                if (flag_wanted_words == true)// вставка желаемых слов
                {
                    flag_wanted_words = false;//сброс флага группы
                    password += wanted_words[n_group - clbPassSymbols.CheckedItems.Count];
                    i += wanted_words[n_group - clbPassSymbols.CheckedItems.Count].Length - 1;
                    continue;
                }
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

            if (clbPassSymbols.GetItemChecked(7) is true) nsymbs += 1; // пробел
            // считаем сложность пароля в битах
            entropy = Math.Floor(Convert.ToDouble(nudPassLength.Value) * Math.Log2(nsymbs));
            str_entropy = Convert.ToString(entropy);

            if (password.Length == 0) return; // по каким-то причинам, длина пароля = 0 , выходим
            if (entropy < 20) //пароль не выводим, сообщаем о необходимости усиления пароля
            {
                tbPassword.Text = "Используйте дополнительные категории символов";
                tbPassForce.BackColor = Color.LightGray; tbPassForce.Text = "";
                return;
            }
            if (entropy < 56)
            {
                tbPassForce.BackColor = Color.Red; tbPassForce.Text = " bits - очень слабый ";
                pb1.Value = Convert.ToInt32(10 / 8 * entropy);
            }
            else if (entropy < 72)
            {
                tbPassForce.BackColor = Color.OrangeRed; tbPassForce.Text = " bits - слабый ";
                pb1.Value = Convert.ToInt32(10 / 8 * entropy);
            }//раскрашиваем полоску со сложностью пароля
            else if (entropy < 80)
            {
                tbPassForce.BackColor = Color.Orange; tbPassForce.Text = " bits - среднесложный ";
                pb1.Value = Convert.ToInt32(10 / 8 * entropy);
            }
            else { tbPassForce.BackColor = Color.Green; tbPassForce.Text = " bits - хороший "; pb1.Value = 100; }//<=80bit -is Good
            tbPassForce.Text = str_entropy + tbPassForce.Text + "пароль";
            tbPassword.Text = password; //выводим пароль в окно

            Clipboard.SetText(password); // записываем пароль в clipboard
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {

            double m1 = metrica[cbConverterFrom.Text];
            double m2 = metrica[cbConverterTo.Text];
            double n, Res_Double;
            Int64 Res_Int;
            if (tbConverterFrom.Text is "") return; //проверяем что входное значение не пустое

            try
            {
                n = Double.Parse(tbConverterFrom.Text);
            }
            catch (FormatException)
            {
                tbConverterTo.Text = "Неверные данные !";
                tbConverterFrom.Text = "1";
                return;

            }
            n = Convert.ToDouble(tbConverterFrom.Text); //читаем входное значение в виде числа


            switch (cbConverterMetrica.Text)
            {
                case "Длины":
                    Res_Int = Convert.ToInt64(n * m1 / m2);
                    Res_Double = Convert.ToDouble(n * m1 / m2);

                    break;
                case "Температура":
                    if (cbConverterTo.Text == "F - градусы Форенгейта")
                    {
                        Res_Int = Convert.ToInt64(n * m1 / m2 + 32);
                        Res_Double = Convert.ToDouble(n * m1 / m2 + 32);
                        if (n < -273.15) // проверка на абсолютный 0
                        {
                            tbConverterTo.Text = "Неверные данные !";
                            tbConverterFrom.Text = "1";
                            return;
                        }
                    }
                    else
                    {
                        Res_Int = Convert.ToInt64((n - 32) * m1 / m2);
                        Res_Double = Convert.ToDouble((n - 32) * m1 / m2);
                        if (n < -459.67) // проверка на абсолютный 0
                        {
                            tbConverterTo.Text = "Неверные данные !";
                            tbConverterFrom.Text = "1";
                            return;
                        }
                    }

                    break;
                default:
                    Res_Int = Convert.ToInt64(n * m1 / m2);
                    Res_Double = Convert.ToDouble(n * m1 / m2);
                    break;
            }
            // Вывод результатов
            if (Res_Int == Res_Double)
            { // вывод без дробной части
                if (Res_Int < 9999999999) tbConverterTo.Text = String.Format("{0:F0}", Res_Int);
                else tbConverterTo.Text = String.Format("{0:e}", Res_Int);
            }
            else
            {
                if (Res_Double < 9999999999) tbConverterTo.Text = String.Format("{0:f8}", Res_Double);
                else tbConverterTo.Text = String.Format("{0:e}", Res_Double);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tmp_swap = cbConverterFrom.Text;
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

                case "Температура": //  metrica.Add("F - fahrenheit degree",);
                    metrica.Clear();
                    metrica.Add("С - градусы Цельсия", 1.8);
                    metrica.Add("F - градусы Форенгейта", 1);

                    cbConverterFrom.Items.Clear();
                    cbConverterFrom.Items.Add("С - градусы Цельсия");
                    cbConverterFrom.Items.Add("F - градусы Форенгейта");

                    cbConverterFrom.Text = "С - градусы Цельсия";
                    cbConverterTo.Items.Clear();
                    cbConverterTo.Items.Add("С - градусы Цельсия");
                    cbConverterTo.Items.Add("F - градусы Форенгейта");

                    cbConverterTo.Text = "F - градусы Форенгейта";
                    break;
                default:
                    break;
            }
        }

        private void btnStartMiner_Click(object sender, EventArgs e)
        {
            int buttonsCount = Convert.ToInt32(nudMinerX.Value * nudMinerY.Value);
            //
            //Пишем в переменную InboxData данные.
            //ВНИМАНИЕ! Сначала нужно записать данные в переменную, а затем вызывать метод загрузки данных (Show()). 
            //В противном случае мы не получим данные в дочерней форме
            //SF.X = nudMinerX.Value;
            //SF.Y = nudMinerY.Value;

            //Грузим дочернюю форму
            //
            //this.BackColor = Color.Aquamarine;
            StaticData.X = nudMinerX.Value;
            StaticData.Y = nudMinerY.Value;
            StaticData.S = nudComplicate.Value;
            SlaveForm SF = new SlaveForm();
            
            SF.DialogResult = DialogResult.Retry;
            while (SF.DialogResult == DialogResult.Retry)
            {
                SF.DialogResult = DialogResult.OK;
                //SF = new SlaveForm();
                SF.Show(); 
            }

        }    

        private void tabMiner_Click(object sender, EventArgs e)
        {

        }
    }
    
//Статический класс, одна из переменных которого выступит в качестве буфера для данных
    public static class StaticData
    {
        //Буфер данных
        public static Decimal X = 0;
        public static Decimal Y = 0;
        public static Decimal S = 0;

    }
    public partial class SlaveForm : Form
    {
        int X;
        int Y;
        int S;

        DateTime date1 = new DateTime(0, 0);
        Timer timer1 = new Timer();

        Label labelcont = new Label(); // счетчик мин
        Label labeltime = new Label();
        int W = 30;
        int H = 30;
        int Z; // количество действующих кнопок
        Button[,] _buttons = new Button[30, 30];
        Boolean[,] buttonflags = new Boolean[30, 30];
        Boolean[,] buttonopened = new Boolean[30, 30];
        Label[,] LButtons = new Label[30, 30];
        Boolean flag_detonation = false;
        int[,] minespole = new int[30,30];
        Button Start = new Button();
        Button FlagSW = new Button();
        Random rnd = new Random();
        public SlaveForm()
        {
            // InitializeComponent();
            // Полученные в качестве аргумента данные напрямую пишем в свойство Text текстбокса
            
            Text = "Тихо! Идет разминирование ...";
            X = Convert.ToInt32(StaticData.X);
            Y = Convert.ToInt32(StaticData.Y);
            S = Convert.ToInt32(Math.Round(StaticData.S*X*Y/10)); /*количество мин исходя из уровня сложности */
            Z = S; // количество мин
            // вычисляем размер окна
            this.Width =40+Convert.ToInt32(X)*(W+1);
            this.Height = 70+Convert.ToInt32(Y)*(H+2);

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

            /* draw_all_labels();
             MessageBox.Show("!!");*/
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
                    this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
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
                    int around = 0; //мин вокруг ячейки
                    if (minespole[i, j] == 10) { continue; } //Ячейка с миной, окружение можно не просчитывать
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
           

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            date1 = date1.AddMilliseconds(1);
            labeltime.Text = date1.ToString("mm:ss");
        }
       
            private void dispose_button(Button b) //Нажатие ... отключить кнопку и вывести вместо нее label
        {
            int t = (int)b.Tag;
            int x = t % X;
            int y = t / X;
            
            if (buttonflags[x, y] is true) return; // На клетке стоит флаг, ничего отключать не нужно

            var labelfont = new Font("Arial", 14, FontStyle.Bold); // задаем шрифт -красный текст на черном фоне с центровкой
            var labelbutton = new Label(); // счетчик мин
            //labelbutton.Text = x.ToString() + y.ToString();
            labelbutton.Text = minespole[x, y].ToString();

            switch (minespole[x, y])
            {
                case 0: labelbutton.Text = "";  // нажата пустая клетка -отключаем кнопку и
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
                    labelbutton.Text = "*";
                    if (flag_detonation is true) // нажал прямо в эту мину .... :(
                    { 
                      labelbutton.BackColor = Color.Red;// цвет фона разорвавшейся мины
                      labelfont = new Font("Arial", 18, FontStyle.Bold);
                    } 
                    break;
                default: GameOver(); return; //это очищенная пустая область
                
            }
            // Создаем на месте убитой кнопки лейбл с элементом минного поля

            labelbutton.Width = W;
            labelbutton.Height = H;
            labelbutton.Visible = true;

            //labelbutton.ForeColor = Color.Red; //цвет шрифта
            //labelcont.BackColor = Color.Black; // цвет фона
            labelbutton.TextAlign = ContentAlignment.MiddleCenter;
            labelbutton.BorderStyle = BorderStyle.Fixed3D;
            labelbutton.Font = labelfont;
            
            
            b.Dispose();
            labelbutton.Location = new Point(W * x + 20, H * y + 35);
            Controls.Add(labelbutton);
            buttonopened[x, y] = true;
            if (S == 0 || flag_detonation is true) GameOver(); // Если число флагов и мин совпадает -проверим достигнута ли победа
        }



        private void GameOver() //проверяем на достижение конца игры и чистим хвосты
        {
            int z = X * Y;
            for (int i = 0; i < X; i++)
                for (int j = 0; j < Y; j++)
                    //if (_buttons[i,j].Visible is false) z--;
                    if (buttonopened[i, j] is true) z--;
            if(flag_detonation is true)
            {
                flag_detonation = false;
                for (int i = 0; i < X; i++)
                    for (int j = 0; j < Y; j++)
                        if (_buttons[i, j].Visible is true) dispose_button(_buttons[i, j]); // открываем только неоткрытые кнопки
                if (timer1 != null) { timer1.Dispose(); timer1 = null; }//убиваем таймер, чтобы избежать артефактов при перезапуске

                MessageBox.Show("GameOver ... Try Again .");
                this.DialogResult = DialogResult.Retry;
                this.Close();
                return;
            }

            if (S == 0 && z == Z) // стоит максимальное кол-во флажков и количесво неоткрытых кнопок соответствует числу мин
            {
                //выключаем таймер
                if (timer1 != null) { timer1.Dispose(); timer1 = null; }
                
                MessageBox.Show("YOU ARE WIN !!!");
                this.DialogResult = DialogResult.Retry;
                this.Close(); 
                return;
            }
            if (S != 0 && z == Z) //Проигрыш
            {
                if (timer1 != null) { timer1.Dispose(); timer1 = null; }//убиваем таймер, чтобы избежать артефактов при перезапуске

                MessageBox.Show("GameOver ... Try Again .");
                this.DialogResult = DialogResult.Retry;
                this.Close(); 
                return;
            }
        }
        private void setflag(int x, int y, Boolean flag) // устанавливает/снимает флаг с кнопки
        {
              
            if (flag is true)
            {
                if (S == 0) return;// нельзя ставить мин больше чем по счетчику
                buttonflags[x, y] = flag;
                S--;_buttons[x, y].Text = "?";labelcont.Text = S.ToString("000");
                if (S == 0) {  GameOver();  return; } //проверим - не достигнут ли конец игры
                
            }
            else 
            {  //убираем флаг, и прибавляем значение счетчика неотмеченных мин
                _buttons[x, y].Text = ""; S++; labelcont.Text = S.ToString("000"); 
                buttonflags[x, y] = flag;// массив с флагами на кнопках 
            }
          
        }


        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            var button = (Button)sender;
            int t = (int)button.Tag;
            int x = t % X;
            int y = t / X;
            if (e.Button==MouseButtons.Left) //нажата левая кнопка мыши
            {

                if(timer1.Enabled is false)timer1.Start(); //запускаем таймер,если еще этого не сделали
                if (buttonflags[x, y] is true) return;  // стоит флажек - не обрабатываем нажатие
                if (minespole[x, y] == 10)
                { /*Игра окончена*/
                    timer1.Enabled = false;//останавливаем таймер - взрыв
                    flag_detonation = true; //ставим флаг, что нажата кнопка с миной
                }
                dispose_button(_buttons[x, y]);
                //if (S == 0) { GameOver(); return; }
                return;
            }
                    
            if (e.Button == MouseButtons.Right) //нажата правая кнопка мыши
            { 
                if (buttonflags[x, y] is false)setflag(x, y, true); // ставим или снимаем флажек с кнопки
                else setflag(x, y, false);
                return;
            }

        }
        /* private void button1_Click(object sender, EventArgs eventArgs) // обработка левой кнопки мыши
 {
     var button = (Button)sender;
     int t = (int)button.Tag;
     int x = t % Y;
     int y = t / Y;

     timer1.Start();
     if (buttonflags[x, y] is true) return;  // стоит флажек - не обрабатываем нажатие
     if (minespole[x, y] == 10)
     { //Игра окончена
         timer1.Enabled = false;//останавливаем таймер
         flag_detonation = true; //ставим флаг, что нажата кнопка с миной
     }
     dispose_button(_buttons[x, y]);
     //if (S == 0) { GameOver(); return; }
 }*/

    }

}

        
    


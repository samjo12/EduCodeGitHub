/* Какой есть эффективный и разумный способ парсинга JSON?
Посоветуйте инструменты которые есть и быстрые и эффективные для парсинга JSON файлов на .net
Кроме JObject 
у себя обычно использовали https://lbv.github.io/litjson/
не сказать что самый быстрый и эффективный. но достаточно хорош, и у нас в Unity3D проектах важно что под iOS с её AOT компиляцией отлично все прокатывало))

ах да и конечно же всегда есть еще
https://www.newtonsoft.com/json
если нужно поновее и чтоб умело прям почти все что угодно в json переварить) да и вроде как один из самых популярных. 

https://quicktype.io/


DarkRaven Александр Кузнецов @DarkRaven
разработка программного обеспечения
Можно посмотреть на https://github.com/kevin-montrose/Jil
Есть еще https://github.com/neuecc/Utf8Json , там обещают скорости. Сам не использовал, использовал JSON.Net, который вы не желаете и Jil.

Но, по факту, все они могут проиграть вашему десериализатору, заточенному под ваш формат. Если нужна скорость, лучше писать под формат. 

Пользуюсь простым самописным механизмом:*/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace MyNamespace
{
    /// <summary>
    /// Выполняет преобразование текста формата JSON
    /// в граф объектов и обратно.
    /// </summary>
    public static class Json
    {
        /// <summary>
        /// Преобразует текст в формате JSON в граф объектов.
        /// </summary>
        /// <param name="text">Исходный текст в формате JSON</param>
        /// <returns>Граф объектов</returns>
        public static object Parse(string text)
        {
            return new JsonParser().Parse(text);
        }

        /// <summary>
        /// Преобразует граф объектов в текст формата JSON. Поддерживаются:
        /// - null
        /// - string
        /// - bool
        /// - int, double, decimal
        /// - List˂object˃
        /// - Dictionary˂string,object˃
        /// </summary>
        /// <param name="value">Граф объектов</param>
        /// <returns>Текст формата JSON</returns>
        public static string Stringify(object value)
        {
            return new JsonSerializer().ToString(value);
        }
    }

    class JsonSerializer
    {
        public string ToString(object obj)
        {
            return ObjectToString(obj);
        }

        private string ObjectToString(object o)
        {
            if (o == null)
                return "null";

            if (o is bool b)
                return b ? "true" : "false";

            if (o is int i)
                return i.ToString(CultureInfo.InvariantCulture);

            if (o is double db)
                return db.ToString(CultureInfo.InvariantCulture);

            if (o is decimal dc)
                return dc.ToString(CultureInfo.InvariantCulture);

            if (o is List<object> l)
                return ListToString(l);

            if (o is Dictionary<string, object> d)
                return DictionaryToString(d);

            if (o is string s)
                return StringToString(s);

            throw new ArgumentOutOfRangeException(nameof(o), o, "Unknown type.");
        }

        private static string StringToString(string s)
        {
            return "\"" + s.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\r", "\\r").Replace("\n", "\\n") + "\"";
        }

        private string ListToString(List<object> a)
        {
            var sb = new StringBuilder();
            sb.Append("[");
            for (var i = 0; i < a.Count; ++i)
            {
                if (i > 0)
                    sb.Append(", ");

                var e = a[i];
                sb.Append(ObjectToString(e));
            }
            sb.Append("]");
            return sb.ToString();
        }

        private string DictionaryToString(Dictionary<string, object> d)
        {
            var sb = new StringBuilder();
            sb.Append("{");
            var isFirst = true;
            foreach (var pair in d)
            {
                if (isFirst)
                    isFirst = false;
                else
                    sb.Append(", ");

                sb.Append(StringToString(pair.Key) + ": ");
                sb.Append(ObjectToString(pair.Value));
            }
            sb.Append("}");
            return sb.ToString();
        }
    }

    class JsonParser
    {
        private string Source;

        private int Offset;

        public object Parse(string source)
        {
            Source = source;
            Offset = 0;
            return ReadObject();
        }

        private object ReadObject()
        {
            SkipWhitespace();
            var c = Source[Offset];
            ++Offset;
            object item;
            switch (c)
            {
                case '[':
                    item = ReadArray();
                    break;
                case '{':
                    item = ReadDictionary();
                    break;
                case '\"':
                case '\'':
                    --Offset;
                    item = ReadString();
                    break;
                case 'n':
                    item = ReadLiteral("ull", null);
                    break;
                case 't':
                    item = ReadLiteral("rue", true);
                    break;
                case 'f':
                    item = ReadLiteral("alse", false);
                    break;
                default:
                    if (Char.IsDigit(c) || c == '-' || c == '.')
                    {
                        --Offset;
                        item = ReadNumber();
                    }
                    else
                        throw new FormatException();
                    break;
            }

            return item;
        }

        private Dictionary<string, object> ReadDictionary()
        {
            var result = new Dictionary<string, object>();
            while (true)
            {
                SkipWhitespace();
                var c = Source[Offset];
                if (c == '}')
                {
                    ++Offset;
                    break;
                }

                var key = ReadString();
                SkipWhitespace();
                c = Source[Offset];
                if (c != ':')
                    throw new FormatException();

                ++Offset;
                var value = ReadObject();
                result.Add(key, value);

                SkipWhitespace();
                c = Source[Offset];
                if (c == ',')
                    ++Offset;
                else if (c == '}')
                {
                    ++Offset;
                    break;
                }
                else
                    throw new FormatException();
            }

            return result;
        }

        private void SkipWhitespace()
        {
            while (Offset < Source.Length && Char.IsWhiteSpace(Source[Offset]))
            {
                ++Offset;
            }
        }

        private decimal ReadNumber()
        {
            var sb = new StringBuilder();

            while (Offset < Source.Length)
            {
                var c = Source[Offset];
                if (Char.IsDigit(c) || c == '-' || c == '.')
                    sb.Append(c);
                else
                    break;

                ++Offset;
            }

            return decimal.Parse(sb.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture);
        }

        private object ReadLiteral(string literal, object value)
        {
            if (Source.Length - Offset > literal.Length)
            {
                if (Source.Substring(Offset, literal.Length) == literal)
                {
                    Offset += literal.Length;
                    return value;
                }
            }

            throw new FormatException();
        }

        private string ReadString()
        {
            SkipWhitespace();

            var result = new StringBuilder();

            var quote = '\0';
            var c = Source[Offset];
            if (c == '\"' || c == '\'')
            {
                quote = c;
            }
            else if (!Char.IsLetter(c))
                throw new FormatException();
            else
                result.Append(c);

            while (true)
            {
                ++Offset;
                c = Source[Offset];
                if (quote == '\0')
                {
                    if (!Char.IsLetter(c) && !Char.IsDigit(c) && c != '_')
                        break;
                }
                else if (c == quote)
                {
                    ++Offset;
                    break;
                }
                else if (c == '\\')
                {
                    ++Offset;
                    c = Source[Offset];
                    if (c == 'u')
                    {
                        c = ReadUnicodeChar();
                    }
                }

                result.Append(c);
            }

            return result.ToString();
        }

        private char ReadUnicodeChar()
        {
            ++Offset; // u
            if (Source.Length - Offset < 4)
                throw new FormatException();

            var digits = Source.Substring(Offset, 4);
            var result = ParseUnicodeChar(digits);
            Offset += 3; // 4 digits
            return result;
        }

        public static char ParseUnicodeChar(string digits)
        {
            if (!digits.ToCharArray().Any(Char.IsDigit))
                throw new FormatException();

            var decValue = Convert.ToInt32(digits, 16);
            var s = Char.ConvertFromUtf32(decValue);
            if (s.Length != 1)
                throw new FormatException();

            return s[0];
        }

        private List<object> ReadArray()
        {
            var result = new List<object>();
            while (true)
            {
                SkipWhitespace();

                var c = Source[Offset];
                if (c == ']')
                {
                    ++Offset;
                    break;
                }

                var obj = ReadObject();
                result.Add(obj);

                SkipWhitespace();
                c = Source[Offset];
                if (c == ',')
                {
                    ++Offset;
                    continue;
                }

                if (c == ']')
                {
                    ++Offset;
                    break;
                }
                
                throw new FormatException();
            }
            
            return result;
        }
    }
}
using System;

namespace Homework_7
{
    /// <summary>
    /// структура описывающая запись ежедневника
    /// </summary>
    struct Unit
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public DateTime WritingDate { get; set; } //время записи/изменения
        public double Score { get; set; } //количество баллов

        /// <summary>
        /// конструктор для записей из файла
        /// </summary>
        /// <param name="ln"></param>
        /// <param name="fn"></param>
        /// <param name="Age"></param>
        /// <param name="s"></param>
        /// <param name="dt"></param>
        public Unit(string ln, string fn, int Age, double s, DateTime dt)
        {
            FirstName = fn;
            LastName = ln;
            this.Age = Age;
            WritingDate = dt;
            Score = s;
        }

        /// <summary>
        /// конструктор для создаваемых записей
        /// </summary>
        /// <param name="ln"></param>
        /// <param name="fn"></param>
        /// <param name="Age"></param>
        /// <param name="s"></param>
        public Unit(string ln, string fn, int Age, double s) : this(ln, fn, Age, s, DateTime.Today)
        {
        }

        /// <summary>
        /// индексатор для изменения отдельных свойств структуры с авто изменением даты
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public string this[string s]
        {
            set
            {
                switch (s)
                {
                    case "и":
                        FirstName = value;
                        WritingDate = DateTime.Today;
                        break;
                    case "ф":
                        LastName = value;
                        WritingDate = DateTime.Today;
                        break;
                    case "в":
                        Age = Convert.ToInt32(value);
                        WritingDate = DateTime.Today;
                        break;
                    case "б":
                        Score = Convert.ToDouble(value);
                        WritingDate = DateTime.Today;
                        break;
                }
            }
        }

        /// <summary>
        /// выводит информацию о записи, добавляя разделение строк вывода
        /// </summary>
        public void PrintInfo()
        {
            Console.WriteLine($"{LastName,10}\t{FirstName,10}\t{Age,3}\t{Score,17}\t{WritingDate.ToString("d")}");
            Console.WriteLine(new string('-', Console.WindowWidth));
        }

        /// <summary>
        /// возвращает строку содержащую свойства записи
        /// </summary>
        /// <returns></returns>
        public string getUnitString()
        {
            return $"{LastName}#{FirstName}#{Age}#{Score}#{WritingDate.ToString("d")}";
        }
    }
}

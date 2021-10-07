﻿using System;
using System.Collections.Generic;

namespace Homework_7
{
    /// <summary>
    /// структура записей ежедневника для работы с записями
    /// </summary>
    struct Diary
    {
        public List<Unit> Note; //коллекция для работы с записями
        public Diary(List<Unit> u)
        {
            Note = new List<Unit>();
            Note = u;
        }

        /// <summary>
        /// индексатор для обращения к конкретному Unit в коллекции Note
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public Unit this[int i]
        {
            get { return Note[i]; }
            set { Note[i] = value; }
        }

        /// <summary>
        /// Создание записи
        /// </summary>
        public void CreateUnit()
        {
            string lastName = null;
            string firstName = null;
            string stringAge = null; //для временного хранения ввода возраста
            int age = 0; //для передачи в параметры конструктора
            string stringScore = null; //для временного хранения ввода score
            double score = 0d; //для передачи в параметры конструктора

            while (String.IsNullOrEmpty(lastName)) //цикл проверки что бы не допустить пустое значени
            {
                Console.Write("Введите фамилию: ");
                lastName = Console.ReadLine();
                foreach (char c in lastName) //проверка что в фамилии не содержатся цифры
                {
                    if (!Char.IsLetter(c))
                    {
                        lastName = null;
                        break;
                    }
                }
            }
            while (String.IsNullOrEmpty(firstName)) //цикл проверки что бы не допустить пустое значени
            {
                Console.Write("Введите имя: ");
                firstName = Console.ReadLine();
                foreach (char c in firstName) //проверка что в имени не содержатся цифры
                {
                    if (!Char.IsLetter(c))
                    {
                        firstName = null;
                        break;
                    }
                }
            }
            while (String.IsNullOrEmpty(stringAge)) //цикл проверки что бы не допустить пустое значени
            {
                Console.Write("Введите возраст: ");
                stringAge = Console.ReadLine();
                if (!int.TryParse(stringAge, out age)) //если введеное значение не получится сделать числом, то будет еще этерации ввода возраста
                {
                    stringAge = null;
                }
            }
            while (String.IsNullOrEmpty(stringScore)) //цикл проверки что бы не допустить пустое значени
            {
                Console.Write("Введите score: ");
                stringScore = Console.ReadLine();
                if (!double.TryParse(stringScore, out score)) //если введеное значение не получится сделать числом, то будет еще этерации ввода возраста
                {
                    stringScore = null;
                }
            }
            Note.Add(new Unit(lastName, firstName, age, score)); //добавляется запись в Note
        }

        /// <summary>
        /// удаление записи по вводу пользователя
        /// </summary>
        public void DeleteUnit()
        {
            string p = null; //для хранения ввода пользователя
            int ind = 0; //номер записи в коллекции
            while (String.IsNullOrEmpty(p))
            {
                Console.Write("Введите значение в записи для удаления или для удаления по номеру напишите номер записи (например №3 или #3): ");
                p = Console.ReadLine();
                if (!String.IsNullOrEmpty(p))
                {
                    if (p[0].Equals('№') || p[0].Equals('#')) //удаление по номеру записи
                    {
                        if (int.TryParse(p.Remove(0, 1), out ind)) //если удается получить номер записи, без учета первого символа ввода пользователя
                        {
                            Note.RemoveAt(ind);
                            return;
                        }
                        else
                        {
                            Console.WriteLine("!!!Вы ввели недопустимое значение, повторите ввод.");
                            p = null;
                            continue;
                        }
                    }

                    while (ind < Note.Count) //удаление записей по значению полей
                    {
                        if (p.Equals(Note[ind].FirstName) || p.Equals(Note[ind].LastName) || p.Equals(Note[ind].Age.ToString()) || p.Equals(Note[ind].WritingDate.ToString()) || p.Equals(Note[ind].Score.ToString()))
                        {
                            Note.RemoveAt(ind);
                        }
                        else
                        {
                            ind++;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// редактирование записи
        /// </summary>
        public void EditUnit()
        {
            string p = null; //null присвоент что б проверка IsNullOrEmpty не выдавало ошибку p не присвоено значение
            while (String.IsNullOrEmpty(p)) //повторные запросы если пользователь не выберет номер записи
            {
                Console.Write("Выберете номер записи для редактирования (пример: #3 или №3):");
                p = Console.ReadLine();
                if (!String.IsNullOrEmpty(p)) //выполняется в случае если строка номера записи не пустая
                {
                    Unit tempUnit = new Unit(); //временный экземпляр структуры содержащий копию выбранной записи
                    string str = null; //строка для параметра индексатора структуры Unit
                    if (p[0].Equals('№') || p[0].Equals('#'))
                    {
                        int ind; //для хранения индекса записи
                        if (int.TryParse(p.Remove(0, 1), out ind)) //проверка корректности ввода номера записи
                        {
                            tempUnit = Note[ind];
                            while (String.IsNullOrEmpty(str)) //повторные запросы если пользователь не выберет параметр
                            {
                                Console.Write($"Введите букву из скобок для выбора поля исправления или введите \"всё\"\nимя(и)/фамилия(ф)/возраст(в)/дата(д): ");
                                str = Console.ReadLine();
                                if (!String.IsNullOrEmpty(str)) //выполняет в случае если строка параметра не пустая
                                {
                                    bool check = str.Equals("и") || str.Equals("ф") || str.Equals("в") || str.Equals("д") || str.Equals("всё");
                                    if (check) //проверка что пользователь ввел допустимые поля записи
                                    {
                                        switch (str)
                                        {
                                            case "всё": //для исправления всех пунктов записи
                                                CreateUnit();
                                                Note.RemoveAt(ind);
                                                Note.Insert(ind, Note[Note.Count - 1]);
                                                Note.RemoveAt(Note.Count - 1);
                                                return;
                                            default: //для исправления конкреного пункта записи                                                
                                                string field = null;
                                                while (String.IsNullOrEmpty(field)) //цикл для повторного ввода если пользователь оставит поле пустым
                                                {
                                                    Console.Write("Введите новое значение: ");
                                                    field = Console.ReadLine();
                                                    if (!String.IsNullOrEmpty(field))
                                                    {
                                                        tempUnit[str] = field;
                                                        Note[ind] = tempUnit;
                                                    }
                                                }
                                                return;
                                        }
                                    }
                                    else //если пользователь ввел недопустимые поля записи
                                    {
                                        str = null;
                                    }
                                }
                            }
                        }
                        else //если пользователь ввел не корректное значение для номера записи
                        {
                            p = null;
                        }
                    }
                    else
                    {
                        p = null;
                    }
                }
            }
        }

        /// <summary>
        /// выводит информацию о записях
        /// </summary>
        public void PrintNote()
        {
            for (int i = 0; i < Note.Count; i++)
            {
                Console.Write(i + new string(' ', 5)); //добавляет перед выводом информации о записи, id записи c отступом после 5 символов
                Note[i].PrintInfo();
            }
        }

        /// <summary>
        /// выводит записи от даты до даты
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        public void PrintNote(DateTime dt1, DateTime dt2)
        {
            for (int i = 0; i < Note.Count; i++)
            {
                if (Note[i].WritingDate.CompareTo(dt1) >= 0 && Note[i].WritingDate.CompareTo(dt2) <= 0)
                {
                    Console.Write(i + new string(' ', 5)); //добавляет перед выводом информации о записи, id записи
                    Note[i].PrintInfo();
                }
            }
        }

        /// <summary>
        /// выводит записи за n количество дней от даты
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="ts"></param>
        public void PrintNote(DateTime dt1, TimeSpan n)
        {
            for (int i = 0; i < Note.Count; i++)
            {
                if (Note[i].WritingDate.CompareTo(dt1) >= 0 && Note[i].WritingDate.CompareTo(dt1 + n) <= 0)
                {
                    Console.Write(i + new string(' ', 5)); //добавляет перед выводом информации о записи, id записи
                    Note[i].PrintInfo();
                }
            }
        }

        /// <summary>
        /// меню выбора вывода в консоль записей
        /// </summary>
        public void PrintNoteMenu()
        {
            string answer = null;
            int i;
            bool check = false;
            while (String.IsNullOrEmpty(answer))
            {
                Console.Write("Введите номер вывода записей ежедневника\n[1] Для полного вывода\n[2] Для вывода от даты до даты\n[3] Для вывода N дней от даты\nВведите цифру: ");
                answer = Console.ReadLine();
                if (int.TryParse(answer, out i))
                {
                    switch (i)
                    {
                        case 1:
                            this.PrintNote();
                            break;
                        case 2:
                            Console.WriteLine("Ввeдите 2 даты формата \"dd.mm.yy\"");
                            DateTime dt1;
                            DateTime dt2;
                            while (!check)
                            {
                                Console.Write("От даты: "); ;
                                check = DateTime.TryParse(Console.ReadLine(), out dt1);
                                if (!check) continue;
                                Console.Write("До даты: ");
                                check = DateTime.TryParse(Console.ReadLine(), out dt2);
                                if (!check) continue;
                                this.PrintNote(dt1, dt2);
                            }
                            break;
                        case 3:
                            Console.WriteLine("Ввeдите дату формата \"dd.mm.yy\" и количество дней");
                            DateTime dt3;
                            double d;
                            TimeSpan ts;
                            while (!check)
                            {
                                Console.Write("От даты: "); ;
                                check = DateTime.TryParse(Console.ReadLine(), out dt3);
                                if (!check) continue;
                                Console.Write("Количество дней: ");
                                check = double.TryParse(Console.ReadLine(), out d);
                                if (!check) continue;
                                ts = TimeSpan.FromDays(d);
                                this.PrintNote(dt3, ts);
                            }
                            break;
                        default:
                            Console.WriteLine("!!!Вы ввели недопустимое значение, повторите ввод.");
                            answer = null;
                            continue;
                    }
                }
                else
                {
                    answer = null;
                }
            }
        }

        /// <summary>
        /// сортировка Note по выбранному полю
        /// </summary>
        public void SortNote()
        {
            string answer = null;
            int a;
            while (String.IsNullOrEmpty(answer))
            {
                Console.Write("Введите цифру из скобок для сортировки по:\n[1]фамилии\t[2]имени\t[3]возрасту\t[4]дате\t[5]баллам\nВведите цифру: ");
                answer = Console.ReadLine();
                if (int.TryParse(answer, out a))
                {
                    switch (a)
                    {
                        case 1:
                            Note.Sort(new ISortLastName());
                            break;
                        case 2:
                            Note.Sort(new ISortFirstName());
                            break;
                        case 3:
                            Note.Sort(new ISortAge());
                            break;
                        case 4:
                            Note.Sort(new ISortTime());
                            break;
                        case 5:
                            Note.Sort(new ISortScore());
                            break;
                        default:
                            Console.WriteLine("!!!Вы ввели недопустимое значение, повторите ввод.");
                            answer = null;
                            continue;
                    }
                }
                else
                {
                    answer = null;
                }
            }
        }

        /// <summary>
        /// метод выводит основное меню работы с ежедневником
        /// </summary>
        /// <param name="fDiary"></param>
        public void DiaryMenu(FileDiary fDiary)
        {
            string input = null;
            int i;
            do
            {
                Console.Write("Меню\n[1]Создать запись\n[2]Редактировать запись\n[3]Удалить запись\n[4]Меню вывода записей\n[5]Меню сортировки\n[6]Запись в файл\n[7]Для выхода из программы\nВведите цифру: ");
                input = Console.ReadLine();
                if (int.TryParse(input, out i))
                {
                    switch (i)
                    {
                        case 1:
                            CreateUnit();
                            Console.Clear();
                            input = null;
                            continue;
                        case 2:
                            EditUnit();
                            Console.Clear();
                            input = null;
                            continue;
                        case 3:
                            DeleteUnit();
                            Console.Clear();
                            input = null;
                            continue;
                        case 4:
                            Console.Clear();
                            if (Note.Count == 0)
                            {
                                Console.WriteLine("Ежедневник не содержит записей.");
                                input = null;
                                continue;
                            }
                            PrintNoteMenu();
                            input = null;
                            continue;
                        case 5:
                            Console.Clear();
                            if (Note.Count == 0)
                            {
                                Console.WriteLine("Ежедневник не содержит записей.");
                                input = null;
                                continue;
                            }
                            SortNote();
                            input = null;
                            continue;
                        case 6:
                            fDiary.WriteDiary();
                            input = null;
                            continue;
                        case 7:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("!!!Вы ввели недопустимое значение, повторите ввод.");
                            input = null;
                            continue;
                    }
                }
            } while (String.IsNullOrEmpty(input));
        }

        /// <summary>
        /// сортировка по имени
        /// </summary>
        struct ISortFirstName : IComparer<Unit>
        {
            public int Compare(Unit x, Unit y)
            {
                return x.FirstName.CompareTo(y.FirstName);
            }
        }
        /// <summary>
        /// сортировка по фамилии
        /// </summary>
        struct ISortLastName : IComparer<Unit>
        {
            public int Compare(Unit x, Unit y)
            {
                return x.LastName.CompareTo(y.LastName);
            }
        }
        /// <summary>
        /// сортировка по возрасту
        /// </summary>
        struct ISortAge : IComparer<Unit>
        {
            public int Compare(Unit x, Unit y)
            {
                return x.Age.CompareTo(y.Age);
            }
        }
        /// <summary>
        /// сортировака по времени изменения/записи
        /// </summary>
        struct ISortTime : IComparer<Unit>
        {
            public int Compare(Unit x, Unit y)
            {
                return x.WritingDate.CompareTo(y.WritingDate);
            }
        }
        /// <summary>
        /// сортировка по баллам
        /// </summary>
        struct ISortScore : IComparer<Unit>
        {
            public int Compare(Unit x, Unit y)
            {
                return x.Score.CompareTo(y.Score);
            }
        }
    }
}

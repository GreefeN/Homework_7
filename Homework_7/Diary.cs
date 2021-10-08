using System;
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
        public void CreateUnit(string lastName, string firstName, int age, double score)
        {
            Note.Add(new Unit(lastName, firstName, age, score)); //добавляется запись в Note
        }

        /// <summary>
        /// удаление записи по вводу пользователя
        /// </summary>
        public void DeleteUnit(int id)
        {
            Note.RemoveAt(id);
        }

        /// <summary>
        /// редактирование записи
        /// </summary>
        public void EditUnit(int id, Unit u)
        {
            Note[id] = u;
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
        /// сортировка Note по выбранному полю
        /// </summary>
        public void SortNote(int a)
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
            }
        }
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


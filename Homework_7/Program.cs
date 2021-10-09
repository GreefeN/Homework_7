using System;

namespace Homework_7
{
    /*
     Что нужно сделать
     [] Разработайте ежедневник, который может хранить множество записей.
     [] Каждая запись состоит из нескольких полей, количество которых должно быть не менее пяти.
     [] Поля могут быть произвольными, однако одним из них должна быть дата добавления записи.

     Для записей реализуйте следующие функции:
     [] создание,
     [] удаление,
     [] редактирование.

     Вы можете сделать создание записи двумя способами:
     [] Пользователь вводит информацию вручную с клавиатуры.
     [] Информация генерируется программой.

     [] Удалять запись можно как по номеру, так и по любому полю.
     [] Если несколько записей имеют одинаковые значения полей, следует удалить их все.

     Помимо этого, реализуйте следующие возможности:
     [] загрузка всех записей из файла,
     [] сохранение всех записей в файл,
     [] загрузка записей из файла по диапазону дат,
     [] упорядочивание записей по выбранному полю.

     Что оценивается
     [] Создан ежедневник, в котором могут храниться записи.
     [] Записи имеют как минимум пять полей.
     [] Одно из полей записи ― дата создания.
     [] Все записи сохраняются на диске.
     [] Все записи загружаются с диска.
     [] С диска загружаются записи в выбранном диапазоне дат.
     [] Записи можно создавать, редактировать и удалять.
     [] Записи сортируются по выбранному полю.
     */
    class Program
    {
        static void Main(string[] args)
        {
            string path = $@"C:\Users\{Environment.UserName}\Documents\file.txt"; //путь к файлу ежедневника
            FileDiary fileDiary = new FileDiary(path); //работа с файлом ежедневника            
            DiaryMenu(fileDiary);

            Console.ReadKey();
        }

        /// <summary>
        /// Создание записи
        /// </summary>
        static void CreateUnit(Diary d)
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
            d.CreateUnit(lastName, firstName, age, score);
        }

        /// <summary>
        /// удаление записи по вводу пользователя
        /// </summary>
        static void DeleteUnit(Diary d)
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
                            d.DeleteUnit(ind);
                            return;
                        }
                        else
                        {
                            Console.WriteLine("!!!Вы ввели недопустимое значение, повторите ввод.");
                            p = null;
                            continue;
                        }
                    }

                    while (ind < d.Note.Count) //удаление записей по значению полей
                    {
                        if (p.Equals(d.Note[ind].FirstName) || p.Equals(d.Note[ind].LastName) || p.Equals(d.Note[ind].Age.ToString()) || p.Equals(d.Note[ind].WritingDate.ToString()) || p.Equals(d.Note[ind].Score.ToString()))
                        {
                            d.DeleteUnit(ind);
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
        static void EditUnit(Diary d)
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
                        try //если указанное значение выйдет за пределы коллекции записей
                        {
                            if (int.TryParse(p.Remove(0, 1), out ind)) //проверка корректности ввода номера записи
                            {
                                tempUnit = d[ind];
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
                                                    CreateUnit(d);
                                                    d.EditUnit(ind, d.Note[d.Note.Count - 1]);
                                                    d.Note.RemoveAt(d.Note.Count - 1);
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
                                                            d.EditUnit(ind, tempUnit);
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
                        catch
                        {
                            Console.WriteLine("!!!Вы ввели недопустимое значение, повторите ввод.");
                            p = null;
                            continue;
                        }
                    }
                    else if (p.Equals("выход")) break;
                    else
                    {
                        Console.WriteLine("Запись под данным номером отсутстует.\nПовторите ввод или введите \"выход\" для возврата в главное меню.");
                        p = null;
                    }
                }
            }
        }

        /// <summary>
        /// меню выбора вывода в консоль записей
        /// </summary>
        static void PrintNoteMenu(Diary diary)
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
                            diary.PrintNote();
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
                                diary.PrintNote(dt1, dt2);
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
                                diary.PrintNote(dt3, ts);
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
        static void SortNoteMenu(Diary d)
        {
            string answer = null;
            int a;
            while (String.IsNullOrEmpty(answer))
            {
                Console.Write("Введите цифру из скобок для сортировки по:\n[1]фамилии\t[2]имени\t[3]возрасту\t[4]дате\t[5]баллам\nВведите цифру: ");
                answer = Console.ReadLine();
                if (int.TryParse(answer, out a))
                {
                    if (a >= 1 && a <= 5)
                    {
                        d.SortNote(a);
                    }
                    else
                    {
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
        static void DiaryMenu(FileDiary fDiary)
        {
            string input = null;
            int i;
            Diary d = fDiary.diary;
            do
            {
                Console.Write("Меню\n[1]Создать запись\n[2]Редактировать запись\n[3]Удалить запись\n[4]Меню вывода записей\n[5]Меню сортировки\n[6]Запись в файл\n[7]Для выхода из программы\nВведите цифру: ");
                input = Console.ReadLine();
                if (int.TryParse(input, out i))
                {
                    switch (i)
                    {
                        case 1:
                            CreateUnit(d);
                            Console.Clear();
                            input = null;
                            continue;
                        case 2:
                            EditUnit(d);
                            Console.Clear();
                            input = null;
                            continue;
                        case 3:
                            DeleteUnit(d);
                            Console.Clear();
                            input = null;
                            continue;
                        case 4:
                            Console.Clear();
                            if (d.Note.Count == 0)
                            {
                                Console.WriteLine("Ежедневник не содержит записей.");
                                input = null;
                                continue;
                            }
                            PrintNoteMenu(d);
                            input = null;
                            continue;
                        case 5:
                            Console.Clear();
                            if (d.Note.Count == 0)
                            {
                                Console.WriteLine("Ежедневник не содержит записей.");
                                input = null;
                                continue;
                            }
                            SortNoteMenu(d);
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
    }
}

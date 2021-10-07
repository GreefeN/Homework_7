using System;
using System.Collections.Generic;
using System.IO;

namespace Homework_7
{
    /// <summary>
    /// структура для работы с файлом ежедневника
    /// </summary>
    struct FileDiary
    {
        public Diary diary;
        private string path;

        /// <summary>
        /// конструктор для чтения файла ежедневника или его создания
        /// </summary>
        /// <param name="path"></param>
        public FileDiary(string path)
        {
            using (StreamReader sr = new StreamReader(File.Open(path, FileMode.OpenOrCreate)))
            {                
                string line;
                List<Unit> Note = new List<Unit>();
                while ((line = sr.ReadLine()) != null)
                {
                    string[] arr = line.Split('#');
                    Note.Add(new Unit(arr[0], arr[1], int.Parse(arr[2]), double.Parse(arr[3]), Convert.ToDateTime(arr[4])));
                }
                diary = new Diary(Note);
                this.path = path;
            }
        }

        /// <summary>
        /// метод для записи в файл ежедневника
        /// </summary>
        public void WriteDiary()
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                foreach (Unit u in diary.Note)
                {
                    sw.WriteLine(u.getUnitString());
                }
            }
        }
    }
}

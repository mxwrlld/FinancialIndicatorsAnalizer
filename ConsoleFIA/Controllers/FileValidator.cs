using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ConsoleFIA.Files;

namespace ConsoleFIA.Controllers
{
    static class FileValidator
    {
        public static string ReadPathToSave()
        {
            Console.WriteLine("Текущая директория {0}", Environment.CurrentDirectory);
            do
            {
                Console.Write("Введите путь к файлу: ", Path.DirectorySeparatorChar);
                var fileName = Console.ReadLine();
                if (File.Exists(fileName))
                {
                    Console.Write("Указанный файл существует. Перезаписать? (да/нет)");
                    if (!ReadYesNo())
                    {
                        continue;
                    }
                }
                return fileName;
            } while (true);
        }

        public static bool ReadYesNo()
        {
            do
            {
                var input = Console.ReadLine().ToLower();
                if (input == "да" || input == "д")
                    return true;
                else if (input == "нет" || input == "н")
                    return false;
                else
                    Console.WriteLine("Пожалуйста, введите \"да\" или \"нет\" (без кавычек)");
            } while (true) ;
        }


        public static FileTypes ReadFileType()
        {
            Console.Write("Введите тип файла (xml/json): ");
            do
            {
                var input = Console.ReadLine().ToLower();
                FileTypes? fileType = FileManager.GetFileType(input);
                if (fileType == null)
                {
                    Console.Write("Пожалуйста, введите \"xml\" или \"json\" (без кавычек): ");
                    continue;
                }
                return (FileTypes)fileType;
            } while (true);
        }

        public static string ReadPathToLoad()
        {
            Console.WriteLine("Текущая директория {0}, используется разделитель {1}",
                Environment.CurrentDirectory, Path.DirectorySeparatorChar);
            do
            {
                Console.Write("Введите путь к файлу: ", Path.DirectorySeparatorChar);
                var fileName = Console.ReadLine();
                if (!File.Exists(fileName))
                {
                    Console.WriteLine("Указанный файл не существует.");
                    continue;
                }
                return fileName;
            } while (true);
        }
    }
}

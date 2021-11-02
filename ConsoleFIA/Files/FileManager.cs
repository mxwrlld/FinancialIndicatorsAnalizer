﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;

namespace ConsoleFIA.Files
{
    static class FileManager
    {
        public static void SaveToJson<T>(string fileName, IEnumerable<T> data)
        {
            string jsonString = JsonSerializer.Serialize(data);
            File.WriteAllText(fileName, jsonString);
        }

        public static IEnumerable<T> LoadFromJson<T>(string fileName)
        {
            string jsonString = File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<IEnumerable<T>>(jsonString);
        }

        public static void SaveToXml<T>(string fileName, T[] data)
        {
            XmlSerializer serializer = new XmlSerializer(data.GetType());
            using (var fs = new FileStream(fileName, FileMode.Create))
            {
                serializer.Serialize(fs, data);
            }
        }

        public static T[] LoadFromXml<T>(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T[]));
            using (var fs = new FileStream(fileName, FileMode.Open))
            {
                return (T[])serializer.Deserialize(fs);
            }
        }

        public static FileTypes? GetFileType(string extension)
        {
            switch (extension.ToLower().Trim('.'))
            {
                case "xml": return FileTypes.Xml;
                case "json": return FileTypes.Json;
                default: return null;
            }
        }

        public static FileTypes? CheckFileType(string fileName)
        {
            return GetFileType(Path.GetExtension(fileName));
        }
    }
}
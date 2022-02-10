using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ConnectFour.FileHandler
{
    static class Handler
    {
        private static string currentSavePath = Environment.CurrentDirectory;

        public static void OverwriteFile (string relPath, string dataName, string text) {
            string directoryPath = currentSavePath + relPath;
            string path = currentSavePath + relPath + "/" +  dataName;
            if (File.Exists(path))
            {
                File.WriteAllText(path, text);
            } else {
                Directory.CreateDirectory(directoryPath);
                File.WriteAllText(path, text);
            }           
        }
        public static void WriteLineToFile(string relPath, string dataName, string text)
        {
            string directoryPath = currentSavePath + relPath;
            string path = currentSavePath + relPath + "/" + dataName;
            if (File.Exists(path))
            {
                string oldText = File.ReadAllText(path) + "\r\n";
                File.WriteAllText(path, oldText + text);
            } else {
                Directory.CreateDirectory(directoryPath);                
                File.WriteAllText(path, text);
            } 
        }
        public static string ReadFile(string relPath, string dataName)
        {
            string s = "";
            string path = currentSavePath + relPath + "/" +  dataName;
            if (File.Exists(path))
            {
                using (StreamReader sr = File.OpenText(path)) {
                    while (!sr.EndOfStream) {
                        s += " " + sr.ReadLine();
                    }
                }
            }
            return s;
        }

        public static T ReadJSON<T> (string relPath, string dataName) {
            string jsonString = ReadFile(relPath, dataName);
            return JsonSerializer.Deserialize<T>(jsonString);
        }

        public static void WriteJSON<T> (string relPath, string dataName, T jsonObject) {
            JsonSerializerOptions opt = new JsonSerializerOptions(){ WriteIndented=true };
            string jsonString = JsonSerializer.Serialize<T>(jsonObject, opt);
            OverwriteFile(relPath, dataName, jsonString);
        }
    }

    
}
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace AutoCareApp.InfrastructureLayer
{
    public class FileHandlingService
    {
        public static List<T> ReadFile<T>(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return new List<T>();
            }

            string existingJSON = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<T>>(existingJSON) ?? new List<T>();
        }

        public static void WriteFile<T>(string filePath, List<T> data)
        {
            string jsonToWrite = JsonSerializer.Serialize(data);
            File.WriteAllText(filePath, jsonToWrite);
        }
    }
}

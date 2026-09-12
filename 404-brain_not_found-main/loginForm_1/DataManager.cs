using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json; // Used for converting objects to/from text format

namespace loginForm_1
{
    public static class DataManager
    {
        // File path where user accounts will be saved on your computer
        private static readonly string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "users.json");

        // 1. Load users list from file into memory
        public static List<User> LoadUsers()
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    return new List<User>();
                }

                string json = File.ReadAllText(filePath);
                if (string.IsNullOrWhiteSpace(json))
                {
                    return new List<User>();
                }

                // Deserialization: Converts text back into C# List<User> objects
                return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Deserialization Error: {ex.Message}");
                return new List<User>();
            }
        }

        // 2. Save users list from memory to file
        public static bool SaveUsers(List<User> users)
        {
            try
            {
                // Serialization: Converts C# List<User> objects into JSON text format
                string json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, json);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Serialization Error: {ex.Message}");
                return false;
            }
        }
    }
}
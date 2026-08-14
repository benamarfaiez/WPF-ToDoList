using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using WpfApp.Models;

namespace WpfApp.Services
{
    public class TodoService : ITodoService
    {
        public string FilePath { get; }
        public TodoService(string filePath = "C:\\Users\\fbenamar\\Documents\\todos.json") => FilePath = filePath;

        public List<TodoItem> Load()
        {
            if (!File.Exists(FilePath))
                return [];

            var info = new FileInfo(FilePath);
            if (info.Length == 0)
                return [];

            var json = File.ReadAllText(FilePath);
            if (string.IsNullOrWhiteSpace(json))
                return [];

            return JsonSerializer.Deserialize<List<TodoItem>>(json) ?? [];
        }

        public void Save(IEnumerable<TodoItem> items)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            using var stream = File.Create(FilePath);
            JsonSerializer.Serialize(stream, new List<TodoItem>(items), options);
        }
    }
}

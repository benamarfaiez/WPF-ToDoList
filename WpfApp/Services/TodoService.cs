using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using WpfApp.Models;

namespace WpfApp.Services
{
    public class TodoService : ITodoService
    {
        private readonly string _filePath = "todos.json";

        public List<TodoItem> Load()
        {
            if (!File.Exists(_filePath))
                return [];

            var info = new FileInfo(_filePath);
            if (info.Length == 0)
                return [];

            var json = File.ReadAllText(_filePath);
            if (string.IsNullOrWhiteSpace(json))
                return [];

            return JsonSerializer.Deserialize<List<TodoItem>>(json) ?? [];
        }

        public void Save(IEnumerable<TodoItem> items)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            using var stream = File.Create(_filePath);
            JsonSerializer.Serialize(stream, new List<TodoItem>(items), options);
        }
    }
}

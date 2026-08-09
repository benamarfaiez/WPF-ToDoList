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
                return new List<TodoItem>();

            var info = new FileInfo(_filePath);
            if (info.Length == 0)
                return new List<TodoItem>();

            var json = File.ReadAllText(_filePath);
            if (string.IsNullOrWhiteSpace(json))
                return new List<TodoItem>();

            return JsonSerializer.Deserialize<List<TodoItem>>(json) ?? new List<TodoItem>();
        }

        public void Save(IEnumerable<TodoItem> items)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            using var stream = File.Create(_filePath);
            JsonSerializer.Serialize(stream, new List<TodoItem>(items), options);
        }
    }
}

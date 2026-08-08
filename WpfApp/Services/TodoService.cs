using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
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

            using (var stream = File.OpenRead(_filePath))
            {
                var serializer = new DataContractJsonSerializer(typeof(List<TodoItem>));
                return (List<TodoItem>)serializer.ReadObject(stream) ?? new List<TodoItem>();
            }
        }

        public void Save(IEnumerable<TodoItem> items)
        {
            using (var stream = File.Create(_filePath))
            {
                var serializer = new DataContractJsonSerializer(typeof(List<TodoItem>));
                serializer.WriteObject(stream, new List<TodoItem>(items));
            }
        }
    }
}

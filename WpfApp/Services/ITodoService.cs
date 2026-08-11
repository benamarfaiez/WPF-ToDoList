using System.Collections.Generic;
using WpfApp.Models;

namespace WpfApp.Services
{
    public interface ITodoService
    {
        string FilePath { get; }
        List<TodoItem> Load();
        void Save(IEnumerable<TodoItem> items);
    }
}

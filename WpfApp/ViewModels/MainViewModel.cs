using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WpfApp.Commands;
using WpfApp.Models;
using WpfApp.Services;

namespace WpfApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly TodoService _todoService;
        private string _newTaskTitle = string.Empty;
        public ObservableCollection<TodoItem> Tasks { get; }

        public string NewTaskTitle
        {
            get => _newTaskTitle;
            set => SetProperty(ref _newTaskTitle, value);
        }

        public ICommand AddTaskCommand { get; }
        public ICommand DeleteTaskCommand { get; }

        public MainViewModel()
        {
            _todoService = new TodoService();
            var loadedTasks = _todoService.Load() ?? new List<TodoItem>();
            Tasks = new ObservableCollection<TodoItem>(loadedTasks);

            AddTaskCommand = new RelayCommand(AddTask, CanAddTask);
            DeleteTaskCommand = new RelayCommand(DeleteTask);
        }

        private bool CanAddTask() => !string.IsNullOrWhiteSpace(NewTaskTitle);

        private void AddTask()
        {
            if (!CanAddTask()) return;
            var task = new TodoItem { Title = NewTaskTitle.Trim() };
            Tasks.Add(task);
            NewTaskTitle = string.Empty;
            SaveTasks();
        }

        private void DeleteTask(object parameter)
        {
            if (parameter is TodoItem item)
            {
                Tasks.Remove(item);
                SaveTasks();
            }
        }

        public void SaveTasks()
        {
            _todoService.Save(Tasks);
        }
    }
}

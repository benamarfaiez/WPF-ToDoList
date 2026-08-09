using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WpfApp.Models;
using WpfApp.Services;

namespace WpfApp.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly TodoService _todoService;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddTaskCommand))]
        private string _newTaskTitle = string.Empty;
        public ObservableCollection<TodoItem> Tasks { get; }
        public int PendingCount => Tasks.Count(t => !t.IsCompleted);
        public int CompletedCount => Tasks.Count(t => t.IsCompleted);
        public MainViewModel()
        {
            _todoService = new TodoService();
            var loadedTasks = _todoService.Load() ?? new List<TodoItem>();
            Tasks = new ObservableCollection<TodoItem>(loadedTasks);
            // Écoute des ajouts/suppressions dans la collection
            Tasks.CollectionChanged += OnTasksCollectionChanged;

            // Abonnement initial à chaque tâche existante
            foreach (var item in Tasks)
            {
                item.PropertyChanged += OnItemPropertyChanged;
            }
        }

        private bool CanAddTask() => !string.IsNullOrWhiteSpace(NewTaskTitle);

        [RelayCommand(CanExecute = nameof(CanAddTask))]
        private void AddTask()
        {
            if (!CanAddTask()) return;
            var task = new TodoItem { Title = NewTaskTitle.Trim() };
            Tasks.Add(task);
            NewTaskTitle = string.Empty;
            SaveTasks();
        }

        [RelayCommand]
        private void DeleteTask(TodoItem item)
        {
            if (item is null) return;
            Tasks.Remove(item);
            SaveTasks();
        }        

        public void SaveTasks()
        {
            _todoService.Save(Tasks);
        }

        [RelayCommand]
        private void ToggleTask(object parameter)
        {
            // Dès qu'une case est cochée/décochée, on réenregistre le fichier JSON
            SaveTasks();
        }
        private void OnTasksCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (TodoItem item in e.NewItems)
                    item.PropertyChanged += OnItemPropertyChanged;
            }
            if (e.OldItems != null)
            {
                foreach (TodoItem item in e.OldItems)
                    item.PropertyChanged -= OnItemPropertyChanged;
            }
            UpdateTaskCounts();
        }

        private void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TodoItem.IsCompleted))
            {
                UpdateTaskCounts();
            }
        }
        public void UpdateTaskCounts()
        {
            OnPropertyChanged(nameof(PendingCount));
            OnPropertyChanged(nameof(CompletedCount));
        }

    }
}

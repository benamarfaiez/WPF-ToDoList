using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using WpfApp.Messages;
using WpfApp.Models;
using WpfApp.Services;

namespace WpfApp.ViewModels
{
    // IRecipient<ContactSelectionneMessage> déclare : "je sais réagir à ce message".
    public partial class TacheViewModel : ObservableObject, IRecipient<ContactSelectionneMessage>, IRecipient<DonneesReinitialiseesMessage>
    {
        private readonly ITodoService _todoService;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddTaskCommand))]
        private string _newTaskTitle = string.Empty;
        public ObservableCollection<TodoItem> Taches { get; }
        public int PendingCount => Taches.Count(t => !t.IsCompleted);
        public int CompletedCount => Taches.Count(t => t.IsCompleted);

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(TexteContactFiltre))]
        private Contact contactFiltre;

        private bool CanAddTask() => !string.IsNullOrWhiteSpace(NewTaskTitle);

        [RelayCommand(CanExecute = nameof(CanAddTask))]
        private void AddTask()
        {
            if (!CanAddTask()) return;
            var task = new TodoItem { Title = NewTaskTitle.Trim() , ContactAssigned = ContactFiltre?.NomComplet };
            Taches.Add(task);
            NewTaskTitle = string.Empty;
            SaveTasks();
        }

        [RelayCommand]
        private void DeleteTask(TodoItem item)
        {
            if (item is null) return;
            Taches.Remove(item);
            SaveTasks();
        }

        public void SaveTasks()
        {
            _todoService.Save(Taches);
        }

        [RelayCommand]
        private void ToggleTask(object parameter)
        {
            // Dès qu'une case est cochée/décochée, on réenregistre le fichier JSON
            SaveTasks();
        }
        private void OnTachesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
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

        public string TexteContactFiltre => ContactFiltre is null
            ? "Aucun contact sélectionné dans l'onglet Contacts — les nouvelles tâches ne seront assignées à personne."
            : $"Contact sélectionné : {ContactFiltre.NomComplet}— les nouvelles tâches lui seront assignées.";

        public TacheViewModel(IMessenger messenger, ITodoService todoService)
        {
            _todoService = todoService;
            var loadedTasks = _todoService.Load() ?? Enumerable.Empty<TodoItem>();
            Taches = new ObservableCollection<TodoItem>(loadedTasks);
            // Écoute des ajouts/suppressions dans la collection

            Taches.CollectionChanged += OnTachesCollectionChanged;

            // Abonnement initial à chaque tâche existante
            foreach (var item in Taches)
            {
                item.PropertyChanged += OnItemPropertyChanged;
            }

            // RegisterAll inscrit ce ViewModel pour TOUS les messages
            messenger.RegisterAll(this);

            // Interroge activement l'état ACTUEL au moment de la création de ce ViewModel
            // Envoyer une requête peut échouer si aucun récepteur n'est enregistré. On protège l'accès.
            try
            {
                var request = new ContactSelectionneRequestMessage();
                messenger.Send(request);
                // La propriété Response lance une InvalidOperationException si personne n'a répondu.
                ContactFiltre = request.Response;
            }
            catch (InvalidOperationException)
            {
                ContactFiltre = null;
            }
        }

        // Appelé automatiquement par le Messenger quand ContactViewModel envoie
        // un ContactSelectionneMessage — TacheViewModel n'a jamais appelé ContactViewModel,
        // il ne le connaît même pas.
        public void Receive(ContactSelectionneMessage message)
        {
            ContactFiltre = message.Value;
        }

        public void Receive(DonneesReinitialiseesMessage message)
        {
            Taches.Clear();
        }
    }
}

using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using WpfApp.Messages;
using WpfApp.Models;
using WpfApp.Services;

namespace WpfApp.ViewModels
{
    public partial class SettingsViewModel : ObservableObject
    {
        private readonly ITodoService _todoService;
        private readonly IMessenger _messenger;

        [ObservableProperty]
        private bool modeSombre;

        [ObservableProperty]
        private bool notificationsActivees = true;

        public string CheminFichierDonnees => _todoService.FilePath;

        public SettingsViewModel(ITodoService todoService, IMessenger messenger)
        {
            _todoService = todoService;
            _messenger = messenger;
        }

        [RelayCommand]
        private void ReinitialiserDonnees()
        {
            _messenger.Send(new DonneesReinitialiseesMessage());
            _todoService.Save(new List<TodoItem>());
        }
    }
}

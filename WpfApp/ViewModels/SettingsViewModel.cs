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
        private readonly IThemeService _themeService;

        [ObservableProperty]
        private bool modeSombre;

        public string CheminFichierDonnees => _todoService.FilePath;

        public SettingsViewModel(ITodoService todoService, IMessenger messenger, IThemeService themeService)
        {
            _todoService = todoService;
            _messenger = messenger;
            _themeService = themeService;
        }

        [RelayCommand]
        private void ReinitialiserDonnees()
        {
            _messenger.Send(new DonneesReinitialiseesMessage());
            _todoService.Save(new List<TodoItem>());
        }

        partial void OnModeSombreChanged(bool value)
        {
            _themeService.AppliquerTheme(value);
        }

    }
}

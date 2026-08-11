using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WpfApp.Services;

namespace WpfApp.ViewModels
{
    // Le ViewModel du "shell" (MainWindow). Son seul rôle : exposer le service
    // de navigation à la Window, et fournir les commandes des boutons du menu.
    // Il ne connaît AUCUN détail de ContactViewModel ni de TacheViewModel.
    public partial class MainViewModel : ObservableObject
    {
        public INavigationService Navigation { get; }

        public MainViewModel(INavigationService navigation)
        {
            Navigation = navigation;
            Navigation.NavigateTo<ContactViewModel>(); // écran affiché au démarrage
        }

        [RelayCommand]
        private void AllerAuxContacts() => Navigation.NavigateTo<ContactViewModel>();

        [RelayCommand]
        private void AllerAuxTaches() => Navigation.NavigateTo<TacheViewModel>();

        [RelayCommand]
        private void AllerAuxTParametres() => Navigation.NavigateTo<SettingsViewModel>();
    }
}

using CommunityToolkit.Mvvm.ComponentModel;

namespace WpfApp.Models
{
    public partial class Contact : ObservableObject
    {
        [ObservableProperty]
        private string prenom = string.Empty;

        [ObservableProperty]
        private string nom = string.Empty;

        public string NomComplet => $"{Prenom} {Nom}";
    }
}

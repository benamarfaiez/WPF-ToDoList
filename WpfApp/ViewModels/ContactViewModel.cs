using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using WpfApp.Messages;
using WpfApp.Models;

namespace WpfApp.ViewModels
{
    // IRecipient<ContactSelectionneRequestMessage> : ContactViewModel sait
    // répondre "qui est sélectionné actuellement ?" à qui le demande.
    public partial class ContactViewModel : ObservableObject, IRecipient<ContactSelectionneRequestMessage>
    {
        private readonly IMessenger _messenger;

        public ObservableCollection<Contact> Contacts { get; } =
        [
            new Contact { Prenom = "Marie", Nom = "Dubois" },
            new Contact { Prenom = "Karim", Nom = "Haddad" },
            new Contact { Prenom = "Julie", Nom = "Lefèvre" },
        ];

        [ObservableProperty]
        private Contact contactSelectionne;

        public ContactViewModel(IMessenger messenger)
        {
            _messenger = messenger;
            _messenger.RegisterAll(this);

            // Sélectionne le premier contact par défaut.
            if (Contacts.Count > 0)
            {
                contactSelectionne = Contacts[0];
            }
        }

        // Généré par [ObservableProperty] : appelé automatiquement juste après
        // que ContactSelectionne a changé (et que PropertyChanged a été levé).
        partial void OnContactSelectionneChanged(Contact value)
        {
            // ContactViewModel ne sait pas qui écoute ce message, ni même si
            // quelqu'un l'écoute. Il publie juste un fait : "la sélection a changé".
            _messenger.Send(new ContactSelectionneMessage(value));
        }

        // Répond à la question "quel est le contact sélectionné actuellement ?"
        // posée par un ViewModel créé après coup (typiquement TacheViewModel,
        // la première fois qu'on ouvre l'onglet Tâches).
        public void Receive(ContactSelectionneRequestMessage message)
        {
            message.Reply(ContactSelectionne);
        }
    }
}

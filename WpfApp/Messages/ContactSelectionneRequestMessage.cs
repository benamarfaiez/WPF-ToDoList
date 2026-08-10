using CommunityToolkit.Mvvm.Messaging.Messages;
using WpfApp.Models;

namespace WpfApp.Messages
{
    // Message de type "requête" : contrairement à ContactSelectionneMessage
    // (qui pousse une information au moment où elle change), celui-ci sert à
    // interroger activement "quel est l'état actuel ?" — utile pour un
    // ViewModel créé APRÈS que la sélection initiale ait déjà eu lieu.
    public class ContactSelectionneRequestMessage : RequestMessage<Contact>
    {
    }
}

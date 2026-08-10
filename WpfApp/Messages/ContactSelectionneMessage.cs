using CommunityToolkit.Mvvm.Messaging.Messages;
using WpfApp.Models;

namespace WpfApp.Messages
{
    // Un message est une classe qui transporte une information.
    public class ContactSelectionneMessage : ValueChangedMessage<Contact>
    {
        public ContactSelectionneMessage(Contact contact) : base(contact) { }
    }
}

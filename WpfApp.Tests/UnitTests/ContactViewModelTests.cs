using FluentAssertions;
using CommunityToolkit.Mvvm.Messaging;
using WpfApp.ViewModels;
using Xunit;

namespace WpfApp.Tests.UnitTests
{
    public class ContactViewModelTests
    {
        [Fact]
        public void Contacts_have_default_items_and_default_selection()
        {
            var messenger = new WeakReferenceMessenger();
            var vm = new ContactViewModel(messenger);

            vm.Contacts.Should().HaveCountGreaterOrEqualTo(1);
            vm.ContactSelectionne.Should().Be(vm.Contacts[0]);
        }
    }
}

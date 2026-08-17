using CommunityToolkit.Mvvm.Messaging;
using FluentAssertions;
using Moq;
using WpfApp.ViewModels;
using Xunit;

namespace WpfApp.Tests.UnitTests
{
    public class ContactViewModelTests
    {
        private readonly Mock<IMessenger> _messengerMock = new();
        [Fact]
        public void Contacts_have_default_items_and_default_selection()
        {
            var vm = new ContactViewModel(_messengerMock.Object);

            vm.Contacts.Should().NotBeEmpty();
            vm.ContactSelectionne.Should().Be(vm.Contacts[0]);
        }
    }
}

using FluentAssertions;
using Moq;
using WpfApp.Services;
using WpfApp.ViewModels;
using Xunit;

namespace WpfApp.Tests.UnitTests
{
    public class MainViewModelTests
    {
        [Fact]
        public void Constructor_navigates_to_contact_viewmodel()
        {
            var nav = new Mock<INavigationService>();
            var vm = new MainViewModel(nav.Object);
            nav.Verify(n => n.NavigateTo<ContactViewModel>(), Times.Once);
        }

        [Fact]
        public void AllerAuxTaches_command_calls_navigation()
        {
            var nav = new Mock<INavigationService>();
            var vm = new MainViewModel(nav.Object);
            vm.AllerAuxTachesCommand.Execute(null);
            nav.Verify(n => n.NavigateTo<TacheViewModel>(), Times.AtLeastOnce);
        }
    }
}

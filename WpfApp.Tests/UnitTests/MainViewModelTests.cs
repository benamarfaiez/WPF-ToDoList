using Moq;
using WpfApp.Services;
using WpfApp.ViewModels;
using Xunit;

namespace WpfApp.Tests.UnitTests
{
    public class MainViewModelTests
    {
        private readonly Mock<INavigationService> _navigationServiceMock = new();
        private MainViewModel CreateViewModel()
        {
            return new MainViewModel(
                _navigationServiceMock.Object
            );
        }
        [Fact]
        public void Constructor_ShouldNavigateToContactViewModel_WhenInitialized()
        {
            // Act
            _ = CreateViewModel();

            // Assert
            _navigationServiceMock.Verify(n => n.NavigateTo<ContactViewModel>(), Times.Once);
        }
        [Fact]
        public void AllerAuxTachesCommand_ShouldNavigateToTacheViewModel_WhenExecuted()
        {
            // Arrange
            var vm = CreateViewModel();

            // Act
            vm.AllerAuxTachesCommand.Execute(null);

            // Assert
            _navigationServiceMock.Verify(n => n.NavigateTo<TacheViewModel>(), Times.Once);
        }
    }
}

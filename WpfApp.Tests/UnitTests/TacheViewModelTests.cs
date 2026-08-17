using System.Collections.Generic;
using FluentAssertions;
using Moq;
using CommunityToolkit.Mvvm.Messaging;
using WpfApp.Models;
using WpfApp.Services;
using WpfApp.ViewModels;
using Xunit;

namespace WpfApp.Tests.UnitTests
{
    public class TacheViewModelTests
    {
        private readonly Mock<ITodoService> _todoServiceMock = new();
        private readonly Mock<IMessenger> _messengerMock = new();

        public TacheViewModelTests()
        {
            _todoServiceMock.Setup(t => t.Load()).Returns([]);
        }
        private TacheViewModel CreateViewModel()
        {
            return new TacheViewModel(
                _messengerMock.Object,
                _todoServiceMock.Object
            );
        }

        [Fact]
        public void AddTaskCommand_ShouldAddItemAndSave_WhenTitleIsProvided()
        {
            // Arrange
            var vm = CreateViewModel();
            vm.NewTaskTitle = "Nouvelle tâche";

            // Act
            vm.AddTaskCommand.Execute(null);

            // Assert
            vm.Taches.Should().ContainSingle(t => t.Title == "Nouvelle tâche");
            _todoServiceMock.Verify(t => t.Save(It.IsAny<IEnumerable<TodoItem>>()), Times.Once);
        }

        [Fact]
        public void DeleteTaskCommand_ShouldRemoveItemAndSave_WhenItemExists()
        {
            // Arrange
            var vm = CreateViewModel();
            var itemToDelete = new TodoItem { Title = "To delete" };
            vm.Taches.Add(itemToDelete);

            // Act
            vm.DeleteTaskCommand.Execute(itemToDelete);

            // Assert
            vm.Taches.Should().NotContain(itemToDelete);
            _todoServiceMock.Verify(t => t.Save(It.IsAny<IEnumerable<TodoItem>>()), Times.Once);
        }
    }
}

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
        [Fact]
        public void AddTask_should_add_item_and_save()
        {
            var todoMock = new Mock<ITodoService>();
            todoMock.Setup(t => t.Load()).Returns(new List<TodoItem>());
            var messenger = new WeakReferenceMessenger();
            var vm = new TacheViewModel(messenger, todoMock.Object);

            vm.NewTaskTitle = "Nouvelle tâche";
            vm.AddTaskCommand.Execute(null);

            vm.Taches.Should().ContainSingle(t => t.Title == "Nouvelle tâche");
            todoMock.Verify(t => t.Save(It.IsAny<IEnumerable<TodoItem>>()), Times.AtLeastOnce);
        }

        [Fact]
        public void DeleteTask_should_remove_item_and_save()
        {
            var todoMock = new Mock<ITodoService>();
            todoMock.Setup(t => t.Load()).Returns(new List<TodoItem>());
            var messenger = new WeakReferenceMessenger();
            var vm = new TacheViewModel(messenger, todoMock.Object);

            var item = new TodoItem { Title = "To delete" };
            vm.Taches.Add(item);

            vm.DeleteTaskCommand.Execute(item);

            vm.Taches.Should().NotContain(item);
            todoMock.Verify(t => t.Save(It.IsAny<IEnumerable<TodoItem>>()), Times.AtLeastOnce);
        }
    }
}

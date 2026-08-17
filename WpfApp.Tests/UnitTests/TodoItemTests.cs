using FluentAssertions;
using WpfApp.Models;
using Xunit;

namespace WpfApp.Tests.UnitTests
{
    public class TodoItemTests
    {
        [Fact]
        public void IsCompleted_ShouldRaisePropertyChanged_WhenValueChanged()
        {
            // Arrange
            var item = new TodoItem { Title = "Tâche de test" };
            using var monitoredItem = item.Monitor();

            // Act
            item.IsCompleted = !item.IsCompleted;

            // Assert
            monitoredItem.Should().RaisePropertyChangeFor(x => x.IsCompleted);
        }
    }
}

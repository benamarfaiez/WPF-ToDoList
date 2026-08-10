using System.ComponentModel;
using FluentAssertions;
using WpfApp.Models;
using Xunit;

namespace WpfApp.Tests.UnitTests
{
    public class TodoItemTests
    {
        [Fact]
        public void Changing_IsCompleted_raises_PropertyChanged()
        {
            var item = new TodoItem { Title = "t" };
            bool raised = false;
            item.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(TodoItem.IsCompleted)) raised = true;
            };

            item.IsCompleted = !item.IsCompleted;

            raised.Should().BeTrue();
        }
    }
}

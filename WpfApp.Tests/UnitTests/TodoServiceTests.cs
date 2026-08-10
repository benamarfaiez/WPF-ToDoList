using System;
using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using WpfApp.Models;
using WpfApp.Services;
using Xunit;

namespace WpfApp.Tests.UnitTests
{
    public class TodoServiceTests
    {
        [Fact]
        public void Save_and_Load_should_roundtrip_items()
        {
            var tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".json");
            try
            {
                var service = new TodoService(tempPath);
                var items = new List<TodoItem>
                {
                    new TodoItem{ Title = "Test 1", IsCompleted = false },
                    new TodoItem{ Title = "Test 2", IsCompleted = true }
                };

                service.Save(items);

                var loaded = service.Load();

                loaded.Should().HaveCount(2);
                loaded[0].Title.Should().Be("Test 1");
                loaded[1].IsCompleted.Should().BeTrue();
            }
            finally
            {
                if (File.Exists(tempPath)) File.Delete(tempPath);
            }
        }
    }
}

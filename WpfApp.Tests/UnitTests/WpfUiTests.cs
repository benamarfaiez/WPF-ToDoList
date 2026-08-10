using FluentAssertions;
using WpfApp.Views;
using WpfApp.Tests.Helpers;
using Xunit;

namespace WpfApp.Tests.UnitTests
{
    public class WpfUiTests
    {
        [Fact]
        public void Can_create_TacheView_on_STA_thread()
        {
            WpfTestHelpers.RunOnStaThread(() =>
            {
                var view = new TacheView();
                view.Should().NotBeNull();
            });
        }
    }
}

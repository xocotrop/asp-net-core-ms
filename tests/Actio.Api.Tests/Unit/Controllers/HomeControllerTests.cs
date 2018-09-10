using Actio.Api.Controllers;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;

namespace Actio.Api.Tests.Unit.Controllers
{
    public class HomeControllerTest
    {
        [Fact]
        public void home_controller_get_should_return_string_content()
        {
            var controller = new HomeController();
            var result = controller.Get();
            var contentResult = result as ContentResult;

            contentResult.Should().NotBeNull();
            contentResult.Content.Should().BeEquivalentTo("Hello from Actio API!");
        }
    }
}
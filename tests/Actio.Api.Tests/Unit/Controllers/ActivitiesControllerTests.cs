using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Actio.Api.Controllers;
using Actio.Api.Repositories;
using Action.Common.Commands;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RawRabbit;
using Xunit;

namespace Actio.Api.Tests.Unit.Controllers
{
    public class ActivitiesControllerTests
    {



        [Fact]
        public async Task activities_controller_post_should_return_accepted()
        {
            var busClientMock = new Mock<IBusClient>();
            var repositoryMock = new Mock<IActivityRepository>();
            var controller = new ActivitiesController(busClientMock.Object, repositoryMock.Object);

            var userId = Guid.NewGuid();

            controller.ControllerContext = new Microsoft.AspNetCore.Mvc.ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new System.Security.Claims.ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, userId.ToString()) }, "test"))
                }
            };
            var command = new CreateActivity
            {
                Id = Guid.NewGuid(),
                UserId = userId
            };

            var result = await controller.Post(command);
            var contentResult = result as AcceptedResult;
            contentResult.Should().NotBeNull();
            contentResult.Location.Should().BeEquivalentTo($"activities/{command.Id}");
        }
    }
}
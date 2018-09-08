using System;
using System.Linq;
using System.Threading.Tasks;
using Actio.Api.Repositories;
using Action.Common.Commands;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;

namespace Actio.Api.Controllers
{
    [Route("[controller]")]
    [Authorize("Bearer")]
    public class ActivitiesController : Controller
    {
        private readonly IBusClient _bus;
        private readonly IActivityRepository _activityRepository;
        public ActivitiesController(IBusClient bus, IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
            _bus = bus;
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] CreateActivity command)
        {
            command.Id = Guid.NewGuid();
            command.UserId = Guid.Parse(User.Identity.Name);
            command.CreatedAt = DateTime.Now;
            await _bus.PublishAsync(command);
            return Accepted($"activities/{command.Id}");
        }

        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            var respositories = await _activityRepository.BrowseAsync(Guid.Parse(User.Identity.Name));

            return Json(respositories.Select(x => new { x.Id, x.Name, x.Category, x.CreatedAt }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var activity = await _activityRepository.GetAsync(id);

            if (activity == null)
            {
                return NotFound();
            }

            if (activity.UserId != Guid.Parse(User.Identity.Name))
            {
                return Unauthorized();
            }

            return Json(activity);
        }
    }
}
using System.Threading.Tasks;
using Action.Common.Commands;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;

namespace Actio.Api.Controllers
{
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IBusClient _bus;
        public UsersController(IBusClient bus)
        {
            _bus = bus;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Post([FromBody] CreateUser command)
        {
            await _bus.PublishAsync(command);
            return Accepted();
        }
    }
}
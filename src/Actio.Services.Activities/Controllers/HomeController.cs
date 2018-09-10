using Microsoft.AspNetCore.Mvc;

namespace Actio.Services.Activities.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Get() => Content("Hello from Actio.Services.Activities.API!");
    }
}
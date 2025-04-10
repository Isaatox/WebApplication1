using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using WebApplication1.Resources;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IStringLocalizer<Trad> _localizer;

        public AdminController(IStringLocalizer<Trad> localizer)
        {
            _localizer = localizer;
        }

        [HttpGet("crash")]
        [Authorize(Roles = "Admin")]
        public IActionResult Crash()
        {
            throw new InvalidOperationException(_localizer["CrashMessage"].Value);
        }
    }
}

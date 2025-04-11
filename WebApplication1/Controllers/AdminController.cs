using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using WebApplication1.Identity;
using WebApplication1.Resources;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    [Authorize(Roles = "Administrateur")]
    public class AdminController : ControllerBase
    {
        private readonly IStringLocalizer<Trad> _localizer;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AdminController(
            IStringLocalizer<Trad> localizer,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager
        )
        {
            _localizer = localizer;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost("user")]
        public async Task<IActionResult> CreateUserWithRole([FromBody] CreateUserWithRoleDto dto)
        {
            var existing = await _userManager.FindByEmailAsync(dto.Email);
            if (existing != null)
                return BadRequest();

            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                DisplayName = dto.Nom,
                EmailConfirmed = true,
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                throw new InvalidOperationException(_localizer["UserError"].Value);

            if (!await _roleManager.RoleExistsAsync(dto.RoleName))
            {
                throw new InvalidOperationException(_localizer["RoleNotExist"].Value);
            }

            await _userManager.AddToRoleAsync(user, dto.RoleName);

            return Ok();
        }
    }
}

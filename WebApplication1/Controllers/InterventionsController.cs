using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using WebApplication1.Identity;
using WebApplication1.Resources;

namespace WebApplication1.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class InterventionsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IStringLocalizer<Trad> _localizer;

        public InterventionsController(
            AppDbContext context,
            UserManager<ApplicationUser> userManager,
            IStringLocalizer<Trad> localizer
        )
        {
            _context = context;
            _userManager = userManager;
            _localizer = localizer;
        }

        [HttpPost]
        [Authorize(Roles = "Administrateur")]
        public async Task<IActionResult> Create([FromBody] CreateInterventionDto dto)
        {
            if (dto.Date < DateTime.UtcNow)
                throw new InvalidOperationException(_localizer["DateInPast"].Value);

            if (dto.TechnicienIds.Distinct().Count() > 2)
                throw new InvalidOperationException(_localizer["MaxTechUser"].Value);

            var techniciens = new List<ApplicationUser>();

            foreach (var techId in dto.TechnicienIds.Distinct())
            {
                var user = await _userManager.FindByIdAsync(techId);
                if (user == null)
                    throw new InvalidOperationException(_localizer["notFoundTech"].Value);

                var roles = await _userManager.GetRolesAsync(user);
                if (!roles.Contains("Technicien"))
                    throw new InvalidOperationException(
                        _localizer["UserNotRoleTechUserNotRoleTech"].Value
                    );

                techniciens.Add(user);
            }

            var intervention = new Intervention
            {
                Date = dto.Date,
                Description = dto.Description,
                ClientId = dto.ClientId,
                ServiceTypeId = dto.ServiceTypeId,
                Techniciens = techniciens,
            };

            _context.Interventions.Add(intervention);
            await _context.SaveChangesAsync();

            return Ok(
                new CreateInterventionDto
                {
                    ClientId = intervention.ClientId,
                    Date = intervention.Date,
                    Description = intervention.Description,
                    ServiceTypeId = intervention.ServiceTypeId,
                    TechnicienIds = intervention.Techniciens.Select(t => t.Id).ToList(),
                }
            );
        }

        [HttpGet]
        [Authorize(Roles = "Administrateur,Technicien")]
        public async Task<IActionResult> GetAll()
        {
            var user = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(user!);
            var isAdmin = roles.Contains("Administrateur");

            var query = _context
                .Interventions.Include(i => i.Client)
                .Include(i => i.ServiceType)
                .Include(i => i.Techniciens)
                .AsQueryable();

            if (!isAdmin)
            {
                query = query.Where(i => i.Techniciens.Any(t => t.Id == user!.Id));
            }

            var interventions = await query
                .Select(i => new InterventionDto
                {
                    Id = i.Id,
                    Date = i.Date,
                    Description = i.Description,
                    ClientId = i.ClientId,
                    ClientNomComplet = i.Client.DisplayName!,
                    ServiceTypeId = i.ServiceTypeId,
                    ServiceTypeNom = i.ServiceType.Nom,
                    Techniciens = i
                        .Techniciens.Select(t => new TechnicienDto
                        {
                            Id = t.Id,
                            DisplayName = t.DisplayName!,
                        })
                        .ToList(),
                })
                .ToListAsync();

            return Ok(interventions);
        }
    }
}

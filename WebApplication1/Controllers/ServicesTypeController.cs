using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using WebApplication1.Resources;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrateur")]
    public class ServicesTypeController : ControllerBase
    {
        private readonly ITypeService _service;
        private readonly IStringLocalizer<Trad> _localizer;

        public ServicesTypeController(ITypeService service, IStringLocalizer<Trad> localizer)
        {
            _service = service;
            _localizer = localizer;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceType>>> GetAll()
        {
            var services = await _service.GetAllAsync();
            return Ok(services);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceType>> GetById(int id)
        {
            var service = await _service.GetByIdAsync(id);
            if (service == null)
                throw new InvalidOperationException(_localizer["ServiceNotFound"].Value);
            return Ok(service);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceType>> Create([FromBody] ServiceType service)
        {
            var created = await _service.CreateAsync(service);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                throw new InvalidOperationException(_localizer["ServiceNotFound"].Value);
            return NoContent();
        }
    }
}

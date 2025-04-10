using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Identity
{
    public class ApplicationRole : IdentityRole
    {
        public string? Description { get; set; }
    }
}

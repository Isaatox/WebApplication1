using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Identity;

namespace WebApplication1
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<ServiceType> Services { get; set; }
        public DbSet<Intervention> Interventions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder
                .Entity<Intervention>()
                .HasOne(i => i.Client)
                .WithMany()
                .HasForeignKey(i => i.ClientId);

            builder
                .Entity<Intervention>()
                .HasMany(i => i.Techniciens)
                .WithMany("InterventionsAffectees")
                .UsingEntity(j => j.ToTable("InterventionTechniciens"));
        }
    }
}

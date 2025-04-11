using WebApplication1.Identity;

public class Intervention
{
    public int Id { get; set; }
    public DateTime Date { get; set; }

    public string Description { get; set; } = null!;

    public required string ClientId { get; set; }
    public ApplicationUser Client { get; set; } = null!;

    public int ServiceTypeId { get; set; }
    public ServiceType ServiceType { get; set; } = null!;

    public ICollection<ApplicationUser> Techniciens { get; set; } = new List<ApplicationUser>();
}

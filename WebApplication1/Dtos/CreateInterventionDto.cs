public class CreateInterventionDto
{
    public DateTime Date { get; set; }
    public string Description { get; set; } = null!;
    public required string ClientId { get; set; }
    public int ServiceTypeId { get; set; }
    public List<string> TechnicienIds { get; set; } = new();
}

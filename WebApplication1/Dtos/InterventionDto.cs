public class InterventionDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; } = null!;
    public string ClientId { get; set; } = null!;
    public string ClientNomComplet { get; set; } = null!;
    public int ServiceTypeId { get; set; }
    public string ServiceTypeNom { get; set; } = null!;
    public List<TechnicienDto> Techniciens { get; set; } = new();
}

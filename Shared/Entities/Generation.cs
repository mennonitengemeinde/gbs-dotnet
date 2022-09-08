namespace gbs.Shared.Entities;

public class Generation
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public List<Enrolment> Enrolments { get; set; } = new List<Enrolment>();
}
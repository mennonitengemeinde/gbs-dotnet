namespace gbs.Shared.Entities;

public class Grade
{
    public int EnrolmentId { get; set; }
    public int SubjectId { get; set; }
    public int UserId { get; set; }
    public DateTime Date { get; set; }
    public int Percent { get; set; }
}
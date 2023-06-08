namespace Domain.Entities;

public class StudentContract : BaseEntity
{
    public DateTime DateTime { get; set; }
    public string PathToFile { get; set; }
}
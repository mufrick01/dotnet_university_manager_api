public class Department
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Budget { get; set; }
    public int UniversityId { get; set; }
    public University University { get; set; } = null!;

    public List<Professor> Professors { get; set; } = [];


}
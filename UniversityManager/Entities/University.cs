public class University
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public DateTime FoundationDate { get; set; }

    public List<Department> Departments { get; set; } = [];
}
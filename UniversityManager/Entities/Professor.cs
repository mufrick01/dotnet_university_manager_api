public class Professor
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime HireDate { get; set; }
    public int DepartmentId { get; set; }
    public Department Department { get; set; } = null!;
    public List<Student> Students { get; set; } = [];
    public List<ProfessorCourse> ProfessorCourses { get; set; } = [];

}

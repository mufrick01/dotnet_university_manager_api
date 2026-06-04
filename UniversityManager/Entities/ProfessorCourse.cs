public class ProfessorCourse
{
    public int ProfessorId { get; set; }
    public int CourseId { get; set; }

    public string Semester { get; set; } = string.Empty;

    public Professor Professor { get; set; } = null!;
    public Course Course { get; set; } = null!;
}
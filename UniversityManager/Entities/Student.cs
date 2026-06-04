public class Student
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }

    public int UniversityId { get; set; }
    public University University { get; set; } = null!;

    public int TutorId { get; set; }
    public Professor Tutor { get; set; } = null!;

    public List<Enrollment> Enrollments { get; set; } = [];
}
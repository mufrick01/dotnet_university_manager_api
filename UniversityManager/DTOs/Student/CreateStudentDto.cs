public class CreateStudentDto
{
    public string FullName { get; set; } = string.Empty;

    public DateTime BirthDate { get; set; }

    public int UniversityId { get; set; }

    public int TutorId { get; set; }
}
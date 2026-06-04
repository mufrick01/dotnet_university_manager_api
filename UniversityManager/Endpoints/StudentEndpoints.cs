using Mapster;
using Microsoft.EntityFrameworkCore;

public static class StudentEndpoints
{
    public static void MapStudentEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/students");

        group.MapGet("/", GetAll);
        group.MapGet("/{id:int}", GetById);
        group.MapPost("/", Create);
    }

    private static async Task<IResult> GetAll(
        AppDbContext db)
    {
        var students = await db.Students
            .AsNoTracking()
            .ToListAsync();

        return Results.Ok(
            students.Adapt<List<StudentDto>>());
    }

    private static async Task<IResult> GetById(
        int id,
        AppDbContext db)
    {
        var student = await db.Students
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id);

        if (student is null)
        {
            return Results.NotFound(
                $"Student with id {id} was not found");
        }

        return Results.Ok(
            student.Adapt<StudentDto>());
    }

    private static async Task<IResult> Create(
        CreateStudentDto dto,
        AppDbContext db)
    {
        dto.FullName = dto.FullName?.Trim()
            ?? string.Empty;

        if (string.IsNullOrWhiteSpace(dto.FullName))
        {
            return Results.BadRequest(
                "Full name is required");
        }

        if (dto.FullName.Length > 200)
        {
            return Results.BadRequest(
                "Full name max length is 200");
        }

        if (dto.BirthDate < new DateTime(1900, 1, 1)
            || dto.BirthDate >= DateTime.Today)
        {
            return Results.BadRequest(
                "Invalid birth date");
        }

        bool universityExists =
            await db.Universities
                .AnyAsync(u =>
                    u.Id == dto.UniversityId);

        if (!universityExists)
        {
            return Results.BadRequest(
                $"University with id {dto.UniversityId} does not exist");
        }

        bool tutorExists =
            await db.Professors
                .AnyAsync(p =>
                    p.Id == dto.TutorId);

        if (!tutorExists)
        {
            return Results.BadRequest(
                $"Professor with id {dto.TutorId} does not exist");
        }

        var student =
            dto.Adapt<Student>();

        db.Students.Add(student);

        await db.SaveChangesAsync();

        var studentDto =
            student.Adapt<StudentDto>();

        return Results.Created(
            $"/students/{student.Id}",
            studentDto);
    }
}
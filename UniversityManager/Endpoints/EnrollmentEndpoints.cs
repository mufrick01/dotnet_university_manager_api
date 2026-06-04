using Mapster;
using Microsoft.EntityFrameworkCore;

public static class EnrollmentEndpoints
{
    public static void MapEnrollmentEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/enrollments");

        group.MapGet("/", GetAll);
        group.MapGet(
            "/students/{studentId:int}/courses/{courseId:int}",
            GetById);

        group.MapPost("/", Create);
    }

    private static async Task<IResult> GetAll(
        AppDbContext db)
    {
        var enrollments = await db.Enrollments
            .AsNoTracking()
            .ToListAsync();

        return Results.Ok(
            enrollments.Adapt<List<EnrollmentDto>>());
    }

    private static async Task<IResult> GetById(
        int studentId,
        int courseId,
        AppDbContext db)
    {
        var enrollment = await db.Enrollments
            .AsNoTracking()
            .FirstOrDefaultAsync(e =>
                e.StudentId == studentId &&
                e.CourseId == courseId);

        if (enrollment is null)
        {
            return Results.NotFound(
                $"Enrollment was not found");
        }

        return Results.Ok(
            enrollment.Adapt<EnrollmentDto>());
    }

    private static async Task<IResult> Create(
        CreateEnrollmentDto dto,
        AppDbContext db)
    {
        bool studentExists =
            await db.Students
                .AnyAsync(s =>
                    s.Id == dto.StudentId);

        if (!studentExists)
        {
            return Results.BadRequest(
                $"Student with id {dto.StudentId} does not exist");
        }

        bool courseExists =
            await db.Courses
                .AnyAsync(c =>
                    c.Id == dto.CourseId);

        if (!courseExists)
        {
            return Results.BadRequest(
                $"Course with id {dto.CourseId} does not exist");
        }

        bool enrollmentExists =
            await db.Enrollments
                .AnyAsync(e =>
                    e.StudentId == dto.StudentId &&
                    e.CourseId == dto.CourseId);

        if (enrollmentExists)
        {
            return Results.Conflict(
                "Student is already enrolled in this course");
        }

        var enrollment =
            dto.Adapt<Enrollment>();

        enrollment.EnrollmentDate = DateTime.Now;


        db.Enrollments.Add(enrollment);

        await db.SaveChangesAsync();

        var enrollmentDto =
            enrollment.Adapt<EnrollmentDto>();


        return Results.Created(
            $"/enrollments/students/{enrollment.StudentId}/courses/{enrollment.CourseId}",
            enrollmentDto);
    }
}
using Mapster;
using Microsoft.EntityFrameworkCore;

public static class ProfessorCourseEndpoints
{
    public static void MapProfessorCourseEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/professor-courses");

        group.MapGet("/", GetAll);
        group.MapGet(
            "/{professorId:int}/{courseId:int}",
            GetById);

        group.MapPost("/", Create);
    }

    private static async Task<IResult> GetAll(
        AppDbContext db)
    {
        var professorCourses =
            await db.ProfessorCourses
                .AsNoTracking()
                .ToListAsync();

        return Results.Ok(
            professorCourses.Adapt<List<ProfessorCourseDto>>());
    }

    private static async Task<IResult> GetById(
        int professorId,
        int courseId,
        AppDbContext db)
    {
        var professorCourse =
            await db.ProfessorCourses
                .AsNoTracking()
                .FirstOrDefaultAsync(pc =>
                    pc.ProfessorId == professorId &&
                    pc.CourseId == courseId);

        if (professorCourse is null)
        {
            return Results.NotFound(
                "ProfessorCourse was not found");
        }

        return Results.Ok(
            professorCourse.Adapt<ProfessorCourseDto>());
    }

    private static async Task<IResult> Create(
        CreateProfessorCourseDto dto,
        AppDbContext db)
    {
        dto.Semester =
            dto.Semester?.Trim()
            ?? string.Empty;

        if (string.IsNullOrWhiteSpace(dto.Semester))
        {
            return Results.BadRequest(
                "Semester is required");
        }

        bool professorExists =
            await db.Professors
                .AnyAsync(p =>
                    p.Id == dto.ProfessorId);

        if (!professorExists)
        {
            return Results.BadRequest(
                $"Professor with id {dto.ProfessorId} does not exist");
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

        bool relationExists =
            await db.ProfessorCourses
                .AnyAsync(pc =>
                    pc.ProfessorId == dto.ProfessorId &&
                    pc.CourseId == dto.CourseId);

        if (relationExists)
        {
            return Results.BadRequest(
                "Professor is already assigned to this course");
        }

        var professorCourse =
            dto.Adapt<ProfessorCourse>();

        db.ProfessorCourses.Add(
            professorCourse);

        await db.SaveChangesAsync();

        return Results.Created(
            $"/professor-courses/{dto.ProfessorId}/{dto.CourseId}",
            professorCourse.Adapt<ProfessorCourseDto>());
    }
}
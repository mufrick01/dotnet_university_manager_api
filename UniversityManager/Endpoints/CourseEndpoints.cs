using Mapster;
using Microsoft.EntityFrameworkCore;

public static class CourseEndpoints
{
    public static void MapCourseEndpoints(
        this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/courses");

        group.MapGet("/", GetAll);
        group.MapGet("/{id:int}", GetById);
        group.MapPost("/", Create);
    }

    private static async Task<IResult> GetAll(
        AppDbContext db)
    {
        var courses = await db.Courses
            .AsNoTracking()
            .ToListAsync();

        return Results.Ok(
            courses.Adapt<List<CourseDto>>());
    }

    private static async Task<IResult> GetById(
        int id,
        AppDbContext db)
    {
        var course = await db.Courses
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);

        if (course is null)
        {
            return Results.NotFound(
                $"Course with id {id} was not found");
        }

        return Results.Ok(
            course.Adapt<CourseDto>());
    }

    private static async Task<IResult> Create(
        CreateCourseDto dto,
        AppDbContext db)
    {
        dto.Code = dto.Code?.Trim() ?? string.Empty;
        dto.Name = dto.Name?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(dto.Code))
        {
            return Results.BadRequest(
                "Code is required");
        }

        if (dto.Code.Length > 20)
        {
            return Results.BadRequest(
                "Code max length is 20");
        }

        if (string.IsNullOrWhiteSpace(dto.Name))
        {
            return Results.BadRequest(
                "Name is required");
        }

        if (dto.Name.Length > 200)
        {
            return Results.BadRequest(
                "Name max length is 200");
        }

        if (dto.Credits < 1 || dto.Credits > 20)
        {
            return Results.BadRequest(
                "Credits must be between 1 and 20");
        }

        bool departmentExists =
            await db.Departments
                .AnyAsync(d =>
                    d.Id == dto.DepartmentId);

        if (!departmentExists)
        {
            return Results.BadRequest(
                $"Department with id {dto.DepartmentId} does not exist");
        }

        bool codeExists =
            await db.Courses
                .AnyAsync(c =>
                    c.Code == dto.Code);

        if (codeExists)
        {
            return Results.Conflict(
                $"Course code '{dto.Code}' already exists");
        }

        var course =
            dto.Adapt<Course>();

        db.Courses.Add(course);

        await db.SaveChangesAsync();

        var courseDto =
            course.Adapt<CourseDto>();

        return Results.Created(
            $"/courses/{course.Id}",
            courseDto);
    }
}
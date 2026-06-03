using Mapster;
using Microsoft.EntityFrameworkCore;
using Superpower.Model;

public static class DepartmentEndpoints
{
    public static void MapDepartmentEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/departments");

        group.MapGet("/", GetAll);
        group.MapGet("/{id:int}", GetById);
        group.MapPost("/", Create);
    }

    private static async Task<IResult> GetAll(AppDbContext db)
    {
        var departments = await db.Departments
            .AsNoTracking()
            .ToListAsync();

        return Results.Ok(
            departments.Adapt<List<DepartmentDto>>());
    }

    private static async Task<IResult> GetById(int id, AppDbContext db)
    {
        var department = await db.Departments
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == id);

        if (department is null)
            return Results.NotFound(
                $"Department with id {id} was not found");

        return Results.Ok(
            department.Adapt<DepartmentDto>());
    }

    private static async Task<IResult> Create(
        CreateDepartmentDto dto,
        AppDbContext db)
    {
        dto.Name = dto.Name?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(dto.Name))
            return Results.BadRequest("Name is required");

        if (dto.Name.Length > 100)
            return Results.BadRequest("Name max length is 100");

        if (dto.Budget < 0)
            return Results.BadRequest(
                "Budget cannot be less than 0");

        bool universityExists = await db.Universities
            .AnyAsync(u => u.Id == dto.UniversityId);

        if (!universityExists)
        {
            return Results.NotFound(
                $"University with id {dto.UniversityId} was not found");
        }

        bool departmentExists = await db.Departments
            .AnyAsync(d =>
                d.Name == dto.Name &&
                d.UniversityId == dto.UniversityId);

        if (departmentExists)
        {
            return Results.Conflict(
                "Department already exists in this university");
        }

        var department = dto.Adapt<Department>();

        db.Departments.Add(department);

        await db.SaveChangesAsync();

        var departmentDto = department.Adapt<DepartmentDto>();

        return Results.Created(
            $"/departments/{department.Id}",
            departmentDto);
    }
}
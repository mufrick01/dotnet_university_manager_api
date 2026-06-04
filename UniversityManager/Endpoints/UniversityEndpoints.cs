using Mapster;
using Microsoft.EntityFrameworkCore;
using Superpower.Model;

public static class UniversityEndpoints
{
    public static void MapUniversityEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/universities");

        group.MapGet("/", GetAll);
        group.MapGet("/{id:int}", GetById);
        group.MapPost("/", Create);
    }

    private static async Task<IResult> GetAll(AppDbContext db)
    {
        var universities = await db.Universities.ToListAsync();

        var universitiesDto = universities.Adapt<List<UniversityDto>>();

        return Results.Ok(universitiesDto);
    }

    private static async Task<IResult> GetById(int id, AppDbContext db)
    {
        var university = await db.Universities
            .FirstOrDefaultAsync(u => u.Id == id);

        if (university is null)
            return Results.NotFound();

        var universityDto = university.Adapt<UniversityDto>();

        return Results.Ok(universityDto);
    }

    private static async Task<IResult> Create(
        CreateUniversityDto dto,
        AppDbContext db)
    {
        dto.Name = dto.Name?.Trim() ?? string.Empty;
        dto.City = dto.City?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(dto.Name))
            return Results.BadRequest("Name is required");

        if (string.IsNullOrWhiteSpace(dto.City))
            return Results.BadRequest("City is required");

        if (dto.Name.Length > 100)
            return Results.BadRequest("Name max length is 100");

        if (dto.City.Length > 100)
            return Results.BadRequest("City max length is 100");

        DateTime minDate = new(1700, 1, 1);

        if (
            dto.FoundationDate < minDate ||
            dto.FoundationDate > DateTime.Today
        )
        {
            return Results.BadRequest("Foundation date is not valid");
        }

        bool alreadyExist = await db.Universities
            .AnyAsync(u => u.Name == dto.Name);

        if (alreadyExist)
            return Results.Conflict("Name already exists");

        var university = dto.Adapt<University>();

        db.Universities.Add(university);

        await db.SaveChangesAsync();

        var universityDto = university.Adapt<UniversityDto>();

        return Results.Created(
            $"/universities/{university.Id}",
            universityDto);
    }
}




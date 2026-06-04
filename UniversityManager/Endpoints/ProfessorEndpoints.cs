using System.Text.RegularExpressions;
using Mapster;
using Microsoft.EntityFrameworkCore;

public static class ProfessorEndpoints
{
    public static void MapProfessorEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/professors");
        group.MapGet("/", GetAll);
        group.MapGet("/{id:int}", GetById);
        group.MapPost("/", Create);
    }

    private static async Task<IResult> GetAll(AppDbContext _db)
    {
        var professors = await _db.Professors.ToListAsync();
        var professorsDTO = professors.Adapt<List<ProfessorDto>>();
        return Results.Ok(professorsDTO);
    }

    private static async Task<IResult> GetById(int id, AppDbContext _db)
    {
        var professor = await _db.Professors.FirstOrDefaultAsync(p => p.Id == id);

        return professor is null
            ? Results.NotFound("professor was not found")
            : Results.Ok(professor.Adapt<ProfessorDto>());
    }

    private static async Task<IResult> Create(CreateProfessorDto createDto, AppDbContext _db)
    {
        createDto.FullName = createDto.FullName.Trim();
        createDto.Email = createDto.Email.Trim();

        if (string.IsNullOrWhiteSpace(createDto.FullName))
            return Results.BadRequest();
        if (!Regex.IsMatch(createDto.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            return Results.BadRequest("Invalid email format");
        if (createDto.HireDate < new DateTime(1900, 1, 1) || createDto.HireDate > DateTime.Today)
            return Results.BadRequest("Invalid hire date");
        var DepartmentExist = await _db.Departments.AnyAsync(d => d.Id == createDto.DepartmentId);
        if (!DepartmentExist)
            return Results.BadRequest("Invalid department id");


        var professor = createDto.Adapt<Professor>();
        _db.Professors.Add(professor);
        await _db.SaveChangesAsync();

        var professorDto = professor.Adapt<ProfessorDto>();
        return Results.Created($"/professors/{professor.Id}", professorDto);


    }
}
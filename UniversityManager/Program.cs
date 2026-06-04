using DotNetEnv;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

Env.Load();
var db_connection_string = Environment.GetEnvironmentVariable("DB_CONNECTION")
?? throw new InvalidOperationException("DB_CONNECTION string is not defined");

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(db_connection_string)
);




builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


// ##########################################################
// #################### MAP ENDPOINTS #######################
// ##########################################################

app.MapUniversityEndpoints();
app.MapDepartmentEndpoints();
app.MapProfessorEndpoints();
app.MapStudentEndpoints();
app.MapCourseEndpoints();
app.MapProfessorCourseEndpoints();
// ##########################################################
// ##########################################################

app.Run();

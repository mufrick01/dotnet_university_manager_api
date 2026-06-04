# University Manager API

University Manager API is a RESTful web service built with ASP.NET Core, Entity Framework Core, and SQL Server for managing academic institutions, departments, courses, professors, students, and teaching assignments.

The project follows a clean and pragmatic architecture using Fluent API configurations, DTO-based contracts, Minimal APIs, and Mapster object mapping.

---

## Features

- University management
- Department management
- Course management
- Professor management
- Student management
- Professor-course assignments
- Entity Framework Core Code First approach
- Database migrations
- DTO-based API contracts
- Fluent API entity configuration
- Minimal API endpoints
- Object mapping with Mapster

---

## Technology Stack

- .NET 8
- ASP.NET Core Minimal APIs
- Entity Framework Core
- SQL Server
- Mapster
- C#

---

## Project Structure

```text
Configurations/
├── CourseConfiguration.cs
├── DepartmentConfiguration.cs
├── ProfessorConfiguration.cs
├── ProfessorCourseConfiguration.cs
├── StudentConfiguration.cs
└── UniversityConfiguration.cs

DTOs/
├── Course/
├── Department/
├── Professor/
├── ProfessorCourse/
├── Student/
└── University/

Data/
└── AppDbContext.cs

Endpoints/
├── CourseEndpoints.cs
├── DepartmentEndpoints.cs
├── ProfessorEndpoints.cs
├── ProfessorCourseEndpoints.cs
├── StudentEndpoints.cs
└── UniversityEndpoints.cs

Entities/
├── Course.cs
├── Department.cs
├── Professor.cs
├── ProfessorCourse.cs
├── Student.cs
└── University.cs

Migrations/
└── Entity Framework Core migrations

Program.cs
```

---

## Domain Model

### University

A university contains multiple departments.

```text
University
    └── Departments
```

### Department

A department belongs to a university and contains multiple courses and professors.

```text
University
    └── Department
            ├── Courses
            └── Professors
```

### Course

A course belongs to a department and can be assigned to multiple professors.

```text
Department
    └── Course
```

### Professor

A professor belongs to a department and may teach multiple courses.

```text
Department
    └── Professor
```

### Student

Represents a student enrolled in the institution.

```text
Student
```

### ProfessorCourse

Represents the many-to-many relationship between professors and courses.

```text
Professor
    └── ProfessorCourse
            └── Course
```

Additional assignment information is stored through the relationship entity:

- Semester

---

## Database Design

### One-to-Many Relationships

```text
University 1 ─── N Department

Department 1 ─── N Course

Department 1 ─── N Professor
```

### Many-to-Many Relationships

```text
Professor N ─── N Course
                │
                ▼
         ProfessorCourse
```

The `ProfessorCourse` entity uses a composite primary key:

```text
ProfessorId + CourseId
```

---

## Entity Framework Core

Entity mappings are configured using Fluent API through dedicated configuration classes.

Example responsibilities include:

- Primary keys
- Composite keys
- Foreign keys
- Relationships
- Delete behaviors
- Property constraints
- Indexes

Configuration classes are located in:

```text
Configurations/
```

---

## API Endpoints

### Universities

```http
GET    /universities
GET    /universities/{id}
POST   /universities
```

### Departments

```http
GET    /departments
GET    /departments/{id}
POST   /departments
```

### Courses

```http
GET    /courses
GET    /courses/{id}
POST   /courses
```

### Professors

```http
GET    /professors
GET    /professors/{id}
POST   /professors
```

### Students

```http
GET    /students
GET    /students/{id}
POST   /students
```

### Professor Courses

```http
GET    /professor-courses
GET    /professor-courses/professors/{professorId}/courses/{courseId}
POST   /professor-courses
```

---

## Running the Application

### Clone the repository

```powershell
git clone <repository-url>
```

### Restore dependencies

```powershell
dotnet restore
```

### Apply migrations

```powershell
dotnet ef database update
```

### Run the application

```powershell
dotnet run
```

---

## Database Migrations

Create a migration:

```powershell
dotnet ef migrations add MigrationName
```

Apply migrations:

```powershell
dotnet ef database update
```

Remove last migration:

```powershell
dotnet ef migrations remove
```

---

## Design Principles

- Separation of concerns
- DTO-based API contracts
- Explicit entity configuration
- Database-first consistency through migrations
- RESTful endpoint design
- Minimal API approach
- Strong relationship modeling with Entity Framework Core

---

## License

This project is available for educational, research, and portfolio purposes.

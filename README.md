# University Manager API

University Manager API is a RESTful web service built with ASP.NET 10, Entity Framework Core, SQL Server, and Mapster for managing universities, departments, courses, professors, students, enrollments, and teaching assignments.

The project follows a clean architecture based on Minimal APIs, Fluent API entity configuration, DTO contracts, and a Code First database approach using Entity Framework Core.

---

## Features

- University management
- Department management
- Course management
- Professor management
- Student management
- Student enrollment management
- Grade tracking
- Professor-course assignment management
- Entity Framework Core Code First approach
- Database migrations
- Fluent API entity configuration
- DTO-based API contracts
- Minimal API endpoints
- Object mapping with Mapster

---

## Technology Stack

- .NET 10
- ASP.NET Core Minimal APIs
- Entity Framework Core
- SQL Server
- Mapster
- C#

---

## Architecture

The application is organized around a domain-driven structure that separates:

- Domain entities
- Data access
- Entity configurations
- API contracts (DTOs)
- Endpoint definitions

This approach keeps the codebase maintainable, scalable, and easy to extend.

---

## Project Structure

```text
Configurations/
├── CourseConfiguration.cs
├── DepartmentConfiguration.cs
├── EnrollmentConfiguration.cs
├── ProfessorConfiguration.cs
├── ProfessorCourseConfiguration.cs
├── StudentConfiguration.cs
└── UniversityConfiguration.cs

DTOs/
├── Course/
├── Department/
├── Enrollment/
├── Professor/
├── ProfessorCourse/
├── Student/
└── University/

Data/
└── AppDbContext.cs

Endpoints/
├── CourseEndpoints.cs
├── DepartmentEndpoints.cs
├── EnrollmentEndpoints.cs
├── ProfessorEndpoints.cs
├── ProfessorCourseEndpoints.cs
├── StudentEndpoints.cs
└── UniversityEndpoints.cs

Entities/
├── Course.cs
├── Department.cs
├── Enrollment.cs
├── Professor.cs
├── ProfessorCourse.cs
├── Student.cs
└── University.cs

Migrations/
└── Entity Framework Core migrations

Program.cs
appsettings.json
appsettings.Development.json
```

---

## Domain Model

### University

Represents an academic institution.

```text
University
    └── Departments
```

---

### Department

Represents an academic department within a university.

```text
University
    └── Department
            ├── Courses
            └── Professors
```

---

### Course

Represents an academic course offered by a department.

```text
Department
    └── Course
            ├── Enrollments
            └── ProfessorCourses
```

---

### Professor

Represents a faculty member assigned to a department.

```text
Department
    └── Professor
            └── ProfessorCourses
```

---

### Student

Represents a student enrolled in the institution.

```text
Student
    └── Enrollments
```

---

### Enrollment

Represents the enrollment of a student in a course.

```text
Student
    └── Enrollment
            └── Course
```

Additional information stored:

- EnrollmentDate
- FinalGrade

---

### ProfessorCourse

Represents the teaching assignment between professors and courses.

```text
Professor
    └── ProfessorCourse
            └── Course
```

Additional information stored:

- Semester

---

## Relationship Model

### One-to-Many Relationships

```text
University 1 ─── N Department

Department 1 ─── N Course

Department 1 ─── N Professor

Student 1 ─── N Enrollment

Course 1 ─── N Enrollment

Professor 1 ─── N ProfessorCourse

Course 1 ─── N ProfessorCourse
```

---

### Many-to-Many Relationships

#### Student ↔ Course

```text
Student N ─── N Course
              │
              ▼
          Enrollment
```

Enrollment uses a composite primary key:

```text
StudentId + CourseId
```

---

#### Professor ↔ Course

```text
Professor N ─── N Course
                │
                ▼
         ProfessorCourse
```

ProfessorCourse uses a composite primary key:

```text
ProfessorId + CourseId
```

---

## Entity Framework Core

The project uses Entity Framework Core with Fluent API configurations.

Configured features include:

- Primary keys
- Composite keys
- Foreign keys
- Navigation properties
- Relationship mappings
- Delete behaviors
- Property constraints
- Indexes
- Precision configuration

All entity mappings are located in:

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

---

### Departments

```http
GET    /departments
GET    /departments/{id}
POST   /departments
```

---

### Courses

```http
GET    /courses
GET    /courses/{id}
POST   /courses
```

---

### Professors

```http
GET    /professors
GET    /professors/{id}
POST   /professors
```

---

### Students

```http
GET    /students
GET    /students/{id}
POST   /students
```

---

### Enrollments

```http
GET    /enrollments

GET    /enrollments/students/{studentId}/courses/{courseId}

POST   /enrollments
```

---

### Professor Courses

```http
GET    /professor-courses

GET    /professor-courses/professors/{professorId}/courses/{courseId}

POST   /professor-courses
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

Remove the latest migration:

```powershell
dotnet ef migrations remove
```

---

## Running the Application

Clone the repository:

```powershell
git clone <repository-url>
```

Navigate to the project directory:

```powershell
cd UniversityManager
```

Restore dependencies:

```powershell
dotnet restore
```

Apply database migrations:

```powershell
dotnet ef database update
```

Run the application:

```powershell
dotnet run
```

---

## Design Principles

- Clean separation of concerns
- Explicit relationship modeling
- RESTful API design
- DTO-based contracts
- Code First development workflow
- Fluent API configuration over data annotations
- Minimal API architecture
- Strong typing throughout the application
- Database integrity through foreign keys and constraints

---

## Example Domain Overview

```text
University
│
├── Departments
│   │
│   ├── Courses
│   │   │
│   │   ├── Enrollments
│   │   │   └── Students
│   │   │
│   │   └── ProfessorCourses
│   │       └── Professors
│   │
│   └── Professors
│
└── Students
```

---

## License

This project is available for educational, research, and portfolio purposes.

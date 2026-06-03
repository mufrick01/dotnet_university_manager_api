# Sistema de Gestión Universitaria

## Objetivo

Construir una API en .NET 10 usando EF Core + SQL Server para administrar una universidad.

El objetivo principal es practicar:

- Relaciones One-To-One
- Relaciones One-To-Many
- Relaciones Many-To-Many
- Tablas puente con datos adicionales
- Fluent API
- IEntityTypeConfiguration
- Migraciones
- Consultas complejas con Include y LINQ

---

# Entidades del Sistema

---

# 1. Universidad

Una universidad:

- tiene muchos departamentos
- tiene muchos estudiantes

## Propiedades

```csharp
Id
Nombre
Ciudad
FechaFundacion
```

## Relaciones

```text
Universidad 1 ---- * Departamento
Universidad 1 ---- * Estudiante
```

---

# 2. Departamento

Un departamento:

- pertenece a una universidad
- tiene muchos profesores
- ofrece muchos cursos

## Propiedades

```csharp
Id
Nombre
Presupuesto
UniversidadId
```

## Relaciones

```text
Departamento * ---- 1 Universidad
Departamento 1 ---- * Profesor
Departamento 1 ---- * Curso
```

---

# 3. Profesor

Un profesor:

- pertenece a un departamento
- puede impartir muchos cursos
- puede ser tutor de muchos estudiantes

## Propiedades

```csharp
Id
NombreCompleto
Email
FechaContratacion
DepartamentoId
```

## Relaciones

```text
Profesor * ---- 1 Departamento
Profesor 1 ---- * Estudiante
Profesor * ---- * Curso
```

---

# 4. Estudiante

Un estudiante:

- pertenece a una universidad
- tiene un profesor tutor
- puede inscribirse en muchos cursos

## Propiedades

```csharp
Id
NombreCompleto
FechaNacimiento
UniversidadId
TutorId
```

## Relaciones

```text
Estudiante * ---- 1 Universidad
Estudiante * ---- 1 Profesor
Estudiante * ---- * Curso
```

---

# 5. Curso

Un curso:

- pertenece a un departamento
- puede tener muchos profesores
- puede tener muchos estudiantes inscritos

## Propiedades

```csharp
Id
Codigo
Nombre
Creditos
DepartamentoId
```

## Relaciones

```text
Curso * ---- 1 Departamento
Curso * ---- * Profesor
Curso * ---- * Estudiante
```

---

# Tablas Intermedias

---

# 6. ProfesorCurso

Tabla puente para relación Many-To-Many entre Profesor y Curso.

## Propiedades

```csharp
ProfesorId
CursoId
Semestre
```

## Relaciones

```text
Profesor 1 ---- * ProfesorCurso
Curso 1 ---- * ProfesorCurso
```

---

# 7. Inscripcion

Tabla puente para relación Many-To-Many entre Estudiante y Curso.

## Propiedades

```csharp
EstudianteId
CursoId
FechaInscripcion
NotaFinal
```

## Relaciones

```text
Estudiante 1 ---- * Inscripcion
Curso 1 ---- * Inscripcion
```

---

# Requisitos Técnicos

## Configuración

Usar:

```csharp
IEntityTypeConfiguration<T>
```

para configurar entidades.

---

# Restricciones

## Aplicar:

- DeleteBehavior.Restrict
- Índices únicos
- Restricciones de longitud
- Campos requeridos

---

# Reglas de negocio

## Profesor

- Email debe ser único

## Curso

- Codigo debe ser único

## Inscripcion

- Un estudiante no puede inscribirse dos veces en el mismo curso

---

# Consultas a implementar

## Básicas

- Obtener universidades con departamentos
- Obtener cursos con profesores
- Obtener estudiantes con sus cursos inscritos

---

## Intermedias

- Obtener promedio de notas por curso
- Obtener estudiantes inscritos en más de 3 cursos
- Obtener profesores que enseñan múltiples cursos

---

## Avanzadas

- Obtener cursos con:
  - departamento
  - profesores
  - estudiantes inscritos

- Obtener ranking de estudiantes por promedio

- Obtener departamentos con mayor cantidad de cursos

---

# Bonus

## Agregar entidad:

```text
PeriodoAcademico
```

## Relacionar con:

- Curso
- Inscripcion
- ProfesorCurso

---

# Objetivo Final

El sistema debe permitir practicar:

- Diseño de dominio
- Relaciones complejas
- Navegación bidireccional
- Configuración avanzada de EF Core
- Migraciones SQL Server
- Consultas LINQ complejas
- Optimización básica de modelos relacionales

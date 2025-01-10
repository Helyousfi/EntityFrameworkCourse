# Complete Course on Entity Framework

## Table of Contents

1. [Introduction to Entity Framework](#introduction-to-entity-framework)
2. [Getting Started](#getting-started)
    - [Installation](#installation)
    - [Setting Up a Project](#setting-up-a-project)
3. [Understanding DbContext and DbSet](#understanding-dbcontext-and-dbset)
4. [Code-First Approach](#code-first-approach)
    - [Creating Models](#creating-models)
    - [Migrations](#migrations)
5. [Database-First Approach](#database-first-approach)
6. [Querying Data](#querying-data)
    - [LINQ Queries](#linq-queries)
    - [Raw SQL Queries](#raw-sql-queries)
7. [Updating and Deleting Data](#updating-and-deleting-data)
8. [Relationships in Entity Framework](#relationships-in-entity-framework)
    - [One-to-One](#one-to-one)
    - [One-to-Many](#one-to-many)
    - [Many-to-Many](#many-to-many)
9. [Advanced Topics](#advanced-topics)
    - [Lazy Loading, Eager Loading, Explicit Loading](#loading-strategies)
    - [Concurrency](#concurrency)
10. [Performance Optimization](#performance-optimization)
11. [Best Practices](#best-practices)

---

## Introduction to Entity Framework

Entity Framework (EF) is an Object-Relational Mapper (ORM) for .NET applications. It allows developers to interact with a database using .NET objects, eliminating the need for most data-access code.

---

## Getting Started

### Installation
To install Entity Framework Core, run the following command in the Package Manager Console or use .NET CLI:

```bash
# Using Package Manager Console
Install-Package Microsoft.EntityFrameworkCore

# Using .NET CLI
Dotnet add package Microsoft.EntityFrameworkCore
```

### Setting Up a Project
Create a new .NET project and set up Entity Framework:

```bash
dotnet new console -n EFCoreDemo
cd EFCoreDemo
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
```

---

## Understanding DbContext and DbSet

- `DbContext`: Represents a session with the database and provides APIs for performing CRUD operations.
- `DbSet`: Represents a collection of entities.

### Example:

```csharp
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Your_Connection_String");
    }
}

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}
```

---

## Code-First Approach

### Creating Models
Define your entities as plain C# classes:

```csharp
public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}
```

Add the `DbSet` in `AppDbContext`:

```csharp
public DbSet<Product> Products { get; set; }
```

### Migrations
Enable migrations to manage database schema:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

---

## Database-First Approach

1. Use the EF Tools to reverse-engineer a database into models:

```bash
dotnet ef dbcontext scaffold "Your_Connection_String" Microsoft.EntityFrameworkCore.SqlServer
```

2. This generates entity classes and the `DbContext` for your database.

---

## Querying Data

### LINQ Queries

```csharp
using (var context = new AppDbContext())
{
    var users = context.Users.Where(u => u.Name.Contains("John")).ToList();
}
```

### Raw SQL Queries

```csharp
var users = context.Users.FromSqlRaw("SELECT * FROM Users").ToList();
```

---

## Updating and Deleting Data

### Updating

```csharp
using (var context = new AppDbContext())
{
    var user = context.Users.Find(1);
    user.Name = "Updated Name";
    context.SaveChanges();
}
```

### Deleting

```csharp
using (var context = new AppDbContext())
{
    var user = context.Users.Find(1);
    context.Users.Remove(user);
    context.SaveChanges();
}
```

---

## Relationships in Entity Framework

### One-to-One

```csharp
public class User
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public Profile Profile { get; set; }
}

public class Profile
{
    public int ProfileId { get; set; }
    public string Bio { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}
```

### One-to-Many

```csharp
public class Blog
{
    public int BlogId { get; set; }
    public string Title { get; set; }
    public List<Post> Posts { get; set; }
}

public class Post
{
    public int PostId { get; set; }
    public string Content { get; set; }
    public int BlogId { get; set; }
    public Blog Blog { get; set; }
}
```

### Many-to-Many

```csharp
public class Student
{
    public int StudentId { get; set; }
    public string Name { get; set; }
    public List<Course> Courses { get; set; }
}

public class Course
{
    public int CourseId { get; set; }
    public string Title { get; set; }
    public List<Student> Students { get; set; }
}
```

---

## Advanced Topics

### Loading Strategies

1. **Lazy Loading**: Entities are loaded when accessed.
2. **Eager Loading**: Entities are loaded along with related data:

```csharp
var users = context.Users.Include(u => u.Profile).ToList();
```

3. **Explicit Loading**:

```csharp
context.Entry(user).Reference(u => u.Profile).Load();
```

---

## Performance Optimization

1. Use `AsNoTracking` for read-only queries:

```csharp
var users = context.Users.AsNoTracking().ToList();
```

2. Avoid N+1 query issues by using eager loading.

---

## Best Practices

- Keep `DbContext` instances short-lived.
- Use migrations for schema changes.
- Avoid hardcoding connection strings; use configuration files.

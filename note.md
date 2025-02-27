MSBuild project tools : Autocompletion
Créer une nouvelle app :

```console
dotnet new [Template] -o [Name] -f net[Version]
```

dotnet-ef tool:
extension apportant des fonctionnalités de design et de migration de db

```console
dotnet tool install --global dotnet-ef
```

### Scaffolding

- Création automatique de classes d'entités, de contextes de données et d'autres, à partir d'une base de données existante.

```console
dotnet add package Microsoft.EntityFrameworkCore.Design --version 7.0.8
```

```console
dotnet ef AppDbContext scaffold "Filename=AppDbContext.db" Microsoft.EntityFrameworkCore.Sqlite --table Blogs --table Users --output-dir AutoGeneratedModels --namespace EFCore.AutoGenerated --data-annotations --context BlogApp
```

### Sln

- Fichier utilisé par l'ide pour organiser et gérer un ensemble de projets liés dans une solution .NET.

```console
dotnet new sln --name [Name]
```

```console
dotnet new [Type] -o [Name] -f net[Version]
```

```console
dotnet sln [Name.sln] add [Folder/...csproj]
```

### Migration

```console
dotnet ef migrations add InitialCreate
```

```console
dotnet ef database update
```

### Code

Ancienne syntaxe :

```cs
using System;

namespace ISITECH
{
    class Program
    {
        static void main()
        {
            Console.WriteLine("hello, world!");
        }
    }
}
```

On peut disable/enable le global ici :

```cs
<ImplicitUsings>enable</ImplicitUsings>
```

dotnet --help pour les aides CLI (Comme à peu pres tous les gros framework...)

Supprime des références à certains espaces de noms (using) du projet.
<ItemGroup>
<Using Remove="System" />
<Using Remove="System.Numerics" />
</ItemGroup>
les classes et les types définis dans ces espaces ne seront pas accessibles dans le projet

Variable : CamelCase
Privé : underscore miniscule
Autre PascalCase

string html = """<html></html>"""; garde l'indentation

String interpolatedString = $"{stud.GetType().GetProperty("Name").GetValue(stud)} is ..."; string avec variable

string mixedInterpolatedString = $@"{stud.Name} is .... ""djikuhfdjk dfgj"" "; string avec variable et quote

string json = $$"""{
"Name":"{{stud.Name}}"
}"""; string json

### Annotation

```cs
[Required]
public object Title { get; set; }

[Required]
[StringLength(5)]
public object Director { get; set; }

[DisplayName("Date Released")]
[Required]
public object? DateReleased { get; set; }

[DisplayName("Price")]
[RegularExpression(@"^\$?\d+(\.(\d{2}))?$")]
[ForeignKey(nameOf())]
```

Il existe plusieurs annotations
Le ? => valeur peut etre nul

Avec le Fluent Api:

```cs
using Microsoft.EntityFrameworkCore;

public class Product
{
public int Id { get; set; }
public string Name { get; set; }
public decimal Price { get; set; }
}

public class MyDbContext : DbContext
{
public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasColumnType("decimal(18, 2)")
            .HasDefaultValue(0.00m)
            .IsRequired();
    }

}
```

Modifie la propriété Price de l'entité Product

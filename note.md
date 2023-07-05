MSBuild project tools : Autocompletion
Créer une nouvelle app : dotnet new console -o consoleApp -f net6.0

Fichier sln :
Le fichier SLN (Solution) est un fichier utilisé par l'environnement de développement intégré (IDE) Microsoft Visual Studio pour organiser et gérer un ensemble de projets liés dans une solution .NET.

Ancienne syntaxe :

using System;

namespace ISITECH
{
class Program
{
static void main(){
Console.WriteLine("hello, world!");
}
}
}

On peut disable le global ici :
<ImplicitUsings>enable</ImplicitUsings>

dotnet --help

Possible d'enlever System avec itemgroup et
<Using Remove="System" />
<Using Remove="System.Numerics" />

Variable : CamelCase
privé : underscore miniscule
autre PascalCase

string html = """<html></html>"""; garde l'indentation

String interpolatedString = $"{stud.GetType().GetProperty("Name").GetValue(stud)} is";

string mixedInterpolatedString = $@"{stud.Name} is .... ""djikuhfdjk dfgj"" ";

string json = $$"""{
"Name":"{{stud.Name}}"
}""";

annotation

        [Required]
        public object Title { get; set; }

        [Required]
        [StringLength(5)]
        public object Director { get; set; }

        [DisplayName("Date Released")]
        [Required]
        public object DateReleased { get; set; }



modelBuilder.Entity<Category>()
.property(c => c....) => permet de modifier en live la ppte d'une entity

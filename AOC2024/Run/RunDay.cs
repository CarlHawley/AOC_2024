using System.Diagnostics;
using AOC2024.Solutions;
using Spectre.Console;
using Spectre.Console.Cli;

namespace AOC2024.Run;

public class RunDay : Command<RunDaySettings>
{
    public override int Execute(CommandContext context, RunDaySettings settings)
    {
        var reader = new StreamReader($"B:/workspace/AOC_2024/AOC2024/AOC2024/Input/Day{settings.Day}.txt");
        var data = reader.ReadToEnd().Split("\r\n");
        Debug.Assert(data.Length > 1, "Error: No Data in file");
        
        var solution = typeof(ISolution).Assembly
            .GetTypes()
            .Where(t => t is { IsInterface: false, IsAbstract: false } && typeof(ISolution).IsAssignableFrom(t))
            .FirstOrDefault(s => s.Name == $"Day{settings.Day}Solution");
        Debug.Assert(solution is not null, $"Error: No Solution found for Day {settings.Day}.");
        var sln = Activator.CreateInstance(solution) as ISolution;
        Debug.Assert(sln is not null);
        
        AnsiConsole.MarkupLine($"[yellow]Running Day {settings.Day}.[/]");
        if (settings.Part is null or 'A')
        {
            var partA = sln.PartA(data);
            AnsiConsole.MarkupLine($"Part A: {partA}");
        }
        if (settings.Part is null or 'B')
        {
            var partB = sln.PartB(data);
            AnsiConsole.MarkupLine($"Part B: {partB}");
        }
        return 0;
    }

    public override ValidationResult Validate(CommandContext context, RunDaySettings settings)
    {
        if (settings.Day <= 0)
        {
            return ValidationResult.Error($"Day must be a positive integer.");
        }

        if (settings.Part is not null && Array.FindIndex(['A', 'B'], c => c ==  char.ToUpperInvariant((char)settings.Part)) == -1)
        {
            return ValidationResult.Error($"Invalid part: {settings.Part}. Must be A or B if given.");
        }
        return base.Validate(context,settings);
    }
}
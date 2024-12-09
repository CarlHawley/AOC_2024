using Spectre.Console.Cli;

namespace AOC2024.Run;

public class RunDaySettings : CommandSettings
{
    [CommandArgument(0, "<day>")]
    public uint Day { get; set; } = 0;
    [CommandOption("-p|--part <part>")]
    public char? Part { get; set; }
}
using AOC2024.Run;
using Spectre.Console.Cli;

var app = new CommandApp();
app.Configure(
    config =>
    {
        config.AddCommand<RunDay>("run");
    });
return await app.RunAsync(args);

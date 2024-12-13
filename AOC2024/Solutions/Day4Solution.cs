using System.Diagnostics;
using Spectre.Console;

namespace AOC2024.Solutions;

public class Day4Solution : ISolution
{
    private string[] _grid;

    private static readonly List<(int X, int Y)> Cardinals =
    [
        (-1, 0), (0, -1), (0, 1), (1, 0),
    ];

    private static readonly List<(int X, int Y)> Intercardinals =
    [
        (-1, -1), (-1, 1), (1, -1), (1, 1)
    ];

    public string PartA(IEnumerable<string> data)
    {
        var padding = new string('.', ((string[])data)[0].Length + 2);
        _grid = [padding, .. data.Select(row => $".{row}."), padding];
        var count = 0;
        for (var row = 1; row < _grid.Length - 1; row++)
        {
            for (var col = 1; col < _grid.Length - 1; col++)
            {
                count += CountMatches(row, col, "XMAS");
            }
        }

        return count.ToString();
    }

    public string PartB(IEnumerable<string> data)
    {
        var padding = new string('.', ((string[])data)[0].Length + 2);
        _grid = [padding, .. data.Select(row => $".{row}."), padding];
        var count = 0;
        for (var row = 1; row < _grid.Length - 1; row++)
        {
            for (var col = 1; col < _grid.Length - 1; col++)
            {
                count += IsCrossMatch(row, col, "MAS") ? 1 : 0;
            }
        }

        return count.ToString();
    }

    private int CountMatches(int r, int c, string key)
    {
        return _grid[r][c] == key[0]
            ? Cardinals.Count(d
                  => _grid[r + d.Y * 1][c + d.X * 1] == key[1]
                     && _grid[r + d.Y * 2][c + d.X * 2] == key[2]
                     && _grid[r + d.Y * 3][c + d.X * 3] == key[3]) +
              Intercardinals.Count(d
                  => _grid[r + d.Y * 1][c + d.X * 1] == key[1]
                     && _grid[r + d.Y * 2][c + d.X * 2] == key[2]
                     && _grid[r + d.Y * 3][c + d.X * 3] == key[3])
            : 0;
    }

    private bool IsCrossMatch(int r, int c, string key)
    {
        var i = Intercardinals;
        return _grid[r][c] == key[1] &&
           (
               (_grid[r + i[0].Y][c + i[0].X] == key[0] && _grid[r + i[3].Y][c + i[3].X] == key[2] ||
               _grid[r + i[3].Y][c + i[3].X] == key[0] && _grid[r + i[0].Y][c + i[0].X] == key[2]) &&
               (_grid[r + i[1].Y][c + i[1].X] == key[0] && _grid[r + i[2].Y][c + i[2].X] == key[2] ||
               _grid[r + i[2].Y][c + i[2].X] == key[0] && _grid[r + i[1].Y][c + i[1].X] == key[2])
           );
    }
}

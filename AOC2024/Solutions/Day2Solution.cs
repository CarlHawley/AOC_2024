namespace AOC2024.Solutions;

public class Day2Solution : ISolution
{
    private const int MaxUnsafe = 3;
    private const int MinUnsafe = 1;

    public string PartA(IEnumerable<string> data)
    {
        var rows = data.Select(row => row.Split().Select(int.Parse).ToList());
        return rows.Count(IsSafe).ToString();
    }

    public string PartB(IEnumerable<string> data)
    {
        var rows = data.Select(row => row.Split().Select(int.Parse).ToList());
        return rows.Count(IsDampenerSafe).ToString();
    }

    private static bool IsSafe(List<int> report)
    {
        var reportIsAscending = report[1] - report[0] < 0;
        for (var level = 1; level < report.Count; level++)
        {
            var diff = report[level] - report[level - 1];
            var absDiff = Math.Abs(diff);
            var levelIsAscending = diff < 0;

            if (absDiff is < MinUnsafe or > MaxUnsafe) return false;
            if (reportIsAscending ^ levelIsAscending) return false;
        }

        return true;
    }

    private static bool IsDampenerSafe(List<int> report)
    {
        if (IsSafe(report)) return true;

        for (var level = 0; level < report.Count; level++)
        {
            var subReport = new List<int>(report);
            subReport.RemoveAt(level);
            if (IsSafe(subReport)) return true;
        }

        return false;
    }
}
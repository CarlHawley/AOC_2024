namespace AOC2024.Solutions;

public class Day2Solution : ISolution
{
    public string PartA(IEnumerable<string> data)
    {
        var safe = data.Select(row => row.Split().Select(int.Parse).ToList()).Count(IsSafe);

        return safe.ToString();
    }

    public string PartB(IEnumerable<string> data)
    {
        var safe = data.Select(row => row.Split().Select(int.Parse).ToList()).Count(IsDampenerSafe);

        return safe.ToString();
    }

    private static bool IsSafe(List<int> report)
    {
        var ascending = report[1] - report[0] < 0;
        for (var i = 1; i < report.Count; i++)
        {
            var diff = report[i] - report[i - 1];
            if (Math.Abs(diff) < 1 || Math.Abs(diff) > 3) return false;
            if ((ascending && diff > 0) || (!ascending && diff < 0)) return false;
        }

        return true;
    }


    private static bool IsDampenerSafe(List<int> report)
    {
        if (IsSafe(report))
        {
            return true;
        }

        for (var level = 0; level < report.Count; level++)
        {
            var subReport = new List<int>(report);
            subReport.RemoveAt(level);
            if (IsSafe(subReport)) return true;
        }

        return false;
    }
}
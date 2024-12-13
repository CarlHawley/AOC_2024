using System.Text.RegularExpressions;

namespace AOC2024.Solutions;

public partial class Day3Solution : ISolution
{
    public string PartA(IEnumerable<string> data)
    {
        var matches = data.SelectMany(row => MulRegex().Matches(row));
        var sum = matches.Sum(match => int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value));

        return sum.ToString();
    }
    
    public string PartB(IEnumerable<string> data)
    {
        var flattenedData = string.Join("", data);
        var doMatches = DoRegex().Matches(flattenedData);
        var mulMatches = doMatches.SelectMany(match => MulRegex().Matches(match.Groups[1].Value));
        var sum = mulMatches.Sum(match => int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value));
        
        return sum.ToString();
    }

    [GeneratedRegex(@"mul\((\d{1,3}),(\d{1,3})\)")]
    private static partial Regex MulRegex();
    
    [GeneratedRegex(@"(?:do\(\)|^)(.*?)(?:don't\(\)|$)")]
    private static partial Regex DoRegex();
}
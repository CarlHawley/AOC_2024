namespace AOC2024.Solutions;

public class Day1Solution : ISolution
{
    public string PartA(IEnumerable<string> data)
    {
        var cols = ReformatData(data);
        cols[0].Sort();
        cols[1].Sort();
        var dist = 0;
        for (var i = 0; i < Math.Min(cols[0].Count, cols[1].Count); i++)
        {
            dist += Math.Abs(cols[0][i] - cols[1][i]);
        }

        return dist.ToString();
    }

    public string PartB(IEnumerable<string> data)
    {
        var cols = ReformatData(data);
        var similarity = 0;
        for (var i = 0; i < cols[0].Count; i++)
        {
            similarity += cols[1].Count(r => r.Equals(cols[0][i])) * cols[0][i];
        }
        return similarity.ToString();
    }

    private static List<List<int>> ReformatData(IEnumerable<string> data)
    {
        List<int> col1 = [];
        List<int> col2 = [];
        foreach (var row in data)
        {
            var res = row. Split([' '], StringSplitOptions.RemoveEmptyEntries);
            col1.Add(int.Parse(res[0]));
            col2.Add(int.Parse(res[1]));
        }

        return [col1, col2];
    }
}
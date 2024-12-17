namespace AOC2024.Solutions;

public class Day5Solution : ISolution
{
    private Dictionary<int, IEnumerable<int>> _map = [];
    public string PartA(IEnumerable<string> data)
    {
        var stringData = data.ToArray();
        var rules = stringData.Where(row => row.Contains('|'));
        var updates = stringData.Where(row => row.Contains(','))
            .Select(row => row.Split(',').Select(int.Parse).ToList());
        BuildMap(rules);
        var sum = updates.Where(IsValidUpdate).Sum(update => update[update.Count / 2]);
        return sum.ToString();
    }

    public string PartB(IEnumerable<string> data)
    {
        var stringData = data.ToArray();
        var rules = stringData.Where(row => row.Contains('|'));
        var updates = stringData.Where(row => row.Contains(','))
            .Select(row => row.Split(',').Select(int.Parse).ToList());
        BuildMap(rules);
        var sum = (from update in updates 
            where !IsValidUpdate(update) 
            let sorted = SortUpdate(update) 
            where IsValidUpdate(sorted) // including this for completeness. Never fails with this data.
            select sorted[update.Count / 2]).Sum();

        return sum.ToString();
    }
    private void BuildMap(IEnumerable<string> rules)
    {
        _map = [];
        foreach (var rule in rules)
        {
            var kvp = rule.Split("|").Select(int.Parse).ToArray();
            if(!_map.ContainsKey(kvp.First()))
            {
                _map[kvp.First()] = [];
            }
            _map[kvp.First()] = _map[kvp.First()].Append(kvp.Last());
        }
    }

    private bool IsValidUpdate(List<int> update)
    {
        for (var i = 0; i < update.Count - 1; i++)
        {
            
            if (!_map.ContainsKey(update.ElementAt(i)) || !_map[update[i]].Contains(update[i + 1]))
            {
                return false;
            }
        }
        return true;
    }
    
    private List<int> SortUpdate(List<int> update)
    {
        List<int> sortedUpdate = [..update];
        sortedUpdate.Sort((x, y) =>
        {
            if (_map[x].Contains(y))
            {
                return -1;
            }
            if (_map[y].Contains(x))
            {
                return 1;
            }
            
            return 0; //not found goes here
        });
        return sortedUpdate;
    }
}

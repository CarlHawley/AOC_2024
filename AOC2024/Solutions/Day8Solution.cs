using System.Text.RegularExpressions;

namespace AOC2024.Solutions;

public partial class Day8Solution : ISolution
{
    public string PartA(IEnumerable<string> data)
    {
        var map = new AntennaMapper(data.ToList());
        map.MapAntinodes();
        return map.Antinodes.Count.ToString();
    }

    public string PartB(IEnumerable<string> data)
    {
        var map = new AntennaMapper(data.ToList(), true);
        map.MapAntinodes();
        return map.Antinodes.Count.ToString();
    }

    public partial class AntennaMapper
    {
        private readonly Dictionary<char, List<(int row, int col)>> _nodes = [];
        public HashSet<(int row, int col)> Antinodes { get; } = [];
        private readonly int _size;
        private readonly bool _harmonic;

        [GeneratedRegex(@"(\d|\w)")]
        private static partial Regex AntennaRegex();

        public AntennaMapper(List<string> data, bool harmonic = false)
        {
            _size = data.Count;
            _harmonic = harmonic;
            for (var row = 0; row < data.Count; row++)
            {
                for (var col = 0; col < data[row].Length; col++)
                {
                    if (IsValidAntenna(data[row][col]))
                    {
                        if (!_nodes.TryGetValue(data[row][col], out var bucket))
                        {
                            bucket = [];
                        }

                        bucket.Add((row, col));
                        _nodes[data[row][col]] = bucket;
                    }
                }
            }
        }

        private static bool IsValidAntenna(char c) =>
            AntennaRegex().IsMatch(c.ToString());

        private bool IsValidAntinode((int row, int col) a) =>
            a.row >= 0 && a.row < _size && a.col < _size && a.col >= 0;

        public void MapAntinodes()
        {
            foreach (var bucket in _nodes.Values)
            {
                for (var i = 0; i < bucket.Count; i++)
                {
                    for (var j = i + 1; j < bucket.Count; j++)
                    {
                        if (_harmonic)
                        {
                            Antinodes.Add(bucket[i]);
                            Antinodes.Add(bucket[j]);
                        }

                        MarchAntinodes(bucket[i], bucket[j]);
                        MarchAntinodes(bucket[j], bucket[i]);
                    }
                }
            }
        }

        private void MarchAntinodes((int row, int col) cur, (int row, int col) prev)
        {
            var next = GetNextAntinode(cur, prev);
            while (IsValidAntinode(next))
            {
                Antinodes.Add(next);
                prev = cur;
                cur = next;
                next = GetNextAntinode(cur, prev);
                if (!_harmonic) break;
            }
        }

        private static (int row, int col) GetNextAntinode((int row, int col) a, (int row, int col) b)
        {
            var xdist = a.row - b.row;
            var ydist = a.col - b.col;
            return (a.row + xdist, a.col + ydist);
        }
    }
}
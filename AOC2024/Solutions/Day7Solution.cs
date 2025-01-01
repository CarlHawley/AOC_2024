using System.Text.RegularExpressions;

namespace AOC2024.Solutions;

public partial class Day7Solution : ISolution
{
    public string PartA(IEnumerable<string> data)
    {
        List<Func<long, long, long>> ops =
        [
            (lhs, rhs) => lhs + rhs,
            (lhs, rhs) => lhs * rhs
        ];
        var sum = data.Select(row => new Calibration(row, ops))
            .Select(cali => cali.IsValid()
                ? cali.Result
                : 0)
            .Sum();
        return sum.ToString();
    }

    public string PartB(IEnumerable<string> data)
    {
        List<Func<long, long, long>> ops =
        [
            (last, first) => last + first,
            (last, first) => last * first,
            (last, first) => long.Parse(string.Join("", [first.ToString(), last.ToString()])),
        ];
        var sum = data.Select(row => new Calibration(row, ops))
            .Select(cali => cali.IsValid()
                ? cali.Result
                : 0)
            .Sum();
        return sum.ToString();
    }

    public partial class Calibration
    {
        public long Result { get; }
        public List<long> Values { get; }
        private readonly List<Func<long, long, long>> _operations;

        public Calibration(string row, List<Func<long, long, long>> operations)
        {
            _operations = operations;
            var matches = Regex().Matches(row).Select(m => long.Parse(m.Groups[1].Value)).ToArray();
            Result = matches[0];
            Values = matches.Skip(1).ToList();
        }

        [GeneratedRegex(@"(?:(\d+))(?:[:\s]+)?")]
        private static partial Regex Regex();

        public bool IsValid() => Permute(Values).Any(x => x == Result);

        private List<long> Permute(List<long> values)
        {
            if (values.Count == 1)
            {
                return values;
            }

            List<long> result = [];
            var last = values.Last();
            var rest = Permute(values.Take(values.Count - 1).ToList());

            foreach (var subResult in rest)
            {
                foreach (var op in _operations)
                {
                    result.Add(op(last, subResult));
                }
            }

            return result;
        }
    }
}
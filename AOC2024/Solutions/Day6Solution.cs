namespace AOC2024.Solutions;

public class Day6Solution : ISolution
{
    public string PartA(IEnumerable<string> data)
    {
        var route = new PatrolRoute(data.ToList());
        route.RunPatrol();
        return route.CountVisited().ToString();
    }

    public string PartB(IEnumerable<string> data)
    {
        var route = new PatrolRoute(data.ToList());
        route.RunPatrol();
        var count = 0;
        foreach (var possible in route.Visited)
        {
            route.SetObstacle(possible, true);
            count += route.IsPatrolLoop(possible) ? 1 : 0;
            route.SetObstacle(possible, false);
        }

        return count.ToString();
    }

    private class PatrolRoute
    {
        private enum Direction
        {
            Up = 0,
            Right = 1,
            Down = 2,
            Left = 3,
        }
        
        private readonly char[,] _grid;
        public HashSet<(int Row, int Col)> Visited { get; } = [];
        private readonly (int Row, int Col) _startPosition;
        private readonly Direction _startDirection;

        public PatrolRoute(List<string> input)
        {
            _grid = new char[input.Count, input.First().Length];

            for (var row = 0; row < input.Count; row++)
            {
                for (var col = 0; col < input.First().Length; col++)
                {
                    _grid[row, col] = input[row][col];
                    if (IsGuard(_grid[row, col]))
                    {
                        _startPosition = (row, col);
                        _startDirection = GetStartingDirection(_grid[row, col]);
                    }
                }
            }
        }
        
        private static bool IsGuard(char input) => input is '^' or 'v' or '<' or '>';
        
        private static Direction GetStartingDirection(char input) =>
            input switch
            {
                '^' => Direction.Up,
                'v' => Direction.Down,
                '<' => Direction.Left,
                '>' => Direction.Right,
                _ => throw new ArgumentException($"Invalid direction: {input}")
            };
        
        private static (int Row, int Col) GetNextPoint((int Row, int Col) current, Direction dir) => 
            dir switch
            {
                Direction.Up => (current.Row - 1, current.Col),
                Direction.Down => (current.Row + 1, current.Col),
                Direction.Left => (current.Row, current.Col - 1),
                Direction.Right => (current.Row, current.Col + 1),
                _ => throw new ArgumentOutOfRangeException(nameof(dir), dir, null)
            };
        
        private static Direction Rotate(Direction dir) => (Direction)(((int)dir + 1) % 4);
        
        private bool IsValidPoint((int Row, int Col) point) =>
            point.Row >= 0 && point.Row < _grid.GetLength(0) && point.Col >= 0 && point.Col < _grid.GetLength(0);

        public int CountVisited() => Visited.Count;
        
        private bool IsObstacle((int Row, int Col) point) => _grid[point.Row, point.Col] == '#';
        
        public void RunPatrol()
        {
            var pos = _startPosition;
            var dir = _startDirection;
            var peek = GetNextPoint(pos, dir);
            while (IsValidPoint(peek))
            {
                if (IsObstacle(peek))
                {
                    dir = Rotate(dir);
                }
                else
                {
                    pos = peek;
                    Visited.Add(pos);
                }

                peek = GetNextPoint(pos, dir);
            }
        }

        public void SetObstacle((int Row, int Col) point, bool emplace)
        {
            _grid[point.Row, point.Col] = emplace ? '#' : '.';
        }

        public bool IsPatrolLoop((int Row, int Col) newObstacle)
        {
            var pos = _startPosition;
            var dir = _startDirection;
            var savedPos = _startPosition;
            var visited = 0;
            var peek = GetNextPoint(pos, dir);
            while (IsValidPoint(peek))
            {
                if (IsObstacle(peek))
                {
                    if (peek.Row == newObstacle.Row && peek.Col == newObstacle.Col)
                    {
                        if (savedPos.Row == pos.Row && savedPos.Col == pos.Col)
                            return true;
                        savedPos = pos;
                    }

                    dir = Rotate(dir);
                }
                else
                {
                    pos = peek;
                    visited++;
                }

                peek = GetNextPoint(pos, dir);

                if (visited >= _grid.Length) // fallback in case the loop does not contain the generated obstacle
                {
                    return true;
                }
            }
            return false;
        }
    }
}
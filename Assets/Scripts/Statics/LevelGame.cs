using System.Collections.Generic;
using System.Linq;

public static class LevelGame
{
    private static int _levelGame;

    private static int[][] _level =
    {
        new []{2, 3, 4},
        new []{1, 2, 3, 4, 7},
        new []{1, 2, 4, 5, 6, 7},
        new []{1, 2, 3, 4, 5, 7},
        new []{1, 2, 3, 4, 5, 6},
        new []{1, 2, 3, 4, 5, 6, 7}
    };

    public static IReadOnlyList<int> GetMatchNumbers(int level) =>
        level < _level.Length ? _level[level].ToList() : null;

    public static void SetLevel(int number) => _levelGame = number;

    public static int GetLevel() =>
        _levelGame > 0 || _levelGame < _level.Length ? _levelGame : 0;
}

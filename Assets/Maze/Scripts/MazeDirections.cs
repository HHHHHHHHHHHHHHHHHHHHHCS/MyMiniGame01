using System;
using Random = UnityEngine.Random;

public enum MazeDirection
{
    North,
    East,
    South,
    West,
}

public static class MazeDirections
{
    private static int _count = -1;

    public static int Count
    {
        get
        {
            if (_count < 0)
            {
                _count = Enum.GetValues(typeof(MazeDirection)).Length;
            }

            return _count;
        }
    }

    public static MazeDirection RandomValue => (MazeDirection) Random.Range(0, Count);

    private static IntVector2[] vectors =
    {
        new IntVector2(0, 1),
        new IntVector2(1, 0),
        new IntVector2(0, -1),
        new IntVector2(-1, 0),
    };

    public static IntVector2 ToIntVector2(this MazeDirection direction)
    {
        return vectors[(int) direction];
    }
}
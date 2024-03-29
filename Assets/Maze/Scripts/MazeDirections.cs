﻿using System;
using UnityEngine;
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
    private static MazeDirection[] opposites =
    {
        MazeDirection.South,
        MazeDirection.West,
        MazeDirection.North,
        MazeDirection.East,
    };

    private static Quaternion[] rotations =
    {
        Quaternion.identity,
        Quaternion.Euler(0f, 90f, 0f),
        Quaternion.Euler(0f, 180f, 0f),
        Quaternion.Euler(0f, 270f, 0f),
    };


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

    public static MazeDirection GetOpposite(this MazeDirection direction)
    {
        return opposites[(int) direction];
    }

    public static Quaternion ToRotation(this MazeDirection direction)
    {
        return rotations[(int) direction];
    }

    public static MazeDirection GetNextClockwise(this MazeDirection direction)
    {
        return (MazeDirection) (((int) direction + 1) % Count);
    }

    public static MazeDirection GetNextCounterclockwise(this MazeDirection direction)
    {
        return (MazeDirection) (((int) direction + Count - 1) % Count);
    }
}
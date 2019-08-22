﻿using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class MazeCell : MonoBehaviour
{
    public IntVector2 coordinates;

    private MazeCellEdge[] edges = new MazeCellEdge[MazeDirections.Count];

    private int initializedEdgeCount;

    public bool IsFullyInitialized
    {
        get => initializedEdgeCount == MazeDirections.Count;
    }

    public MazeDirection RandomUninitializedDirection
    {
        get
        {
            //随机一个跳过的数值  然后走过跳过数值后  再插入
            int skips = Random.Range(0, MazeDirections.Count - initializedEdgeCount);
            for (int i = 0; i < MazeDirections.Count; i++)
            {
                if (edges[i] == null)
                {
                    if (skips == 0)
                    {
                        return (MazeDirection) i;
                    }

                    skips -= 1;
                }
            }

            throw new InvalidOperationException("MazeCell has no uninitialized directions left.");
        }
    }

    public MazeCellEdge GetEdge(MazeDirection direction)
    {
        return edges[(int) direction];
    }

    public void SetEdge(MazeDirection direction, MazeCellEdge edge)
    {
        edges[(int) direction] = edge;
        initializedEdgeCount += 1;
    }
}
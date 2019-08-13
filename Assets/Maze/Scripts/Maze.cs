﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    public float generationStepDelay = 0.05f;

    public MazeCell cellPrefab;

    public IntVector2 size = new IntVector2(20, 20);


    private MazeCell[,] cells;

    public IEnumerator Generate()
    {
        WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
        cells = new MazeCell[size.x, size.z];
        for (int x = 0; x < size.x; x++)
        {
            for (int z = 0; z < size.z; z++)
            {
                yield return delay;
                CreateCell(new IntVector2(x, z));
            }
        }
    }

    private void CreateCell(IntVector2 coordinates)
    {
        MazeCell newCell = Instantiate(cellPrefab, transform, true);
        cells[coordinates.x, coordinates.z] = newCell;
        newCell.name = $"MazeCell_{coordinates.x}_{coordinates.z}";
        newCell.transform.localPosition =
            new Vector3(coordinates.x - size.x * 0.5f + 0.5f, 0f, coordinates.z - size.z * 0.5f + 0.5f);
    }
}
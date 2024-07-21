using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridHex<TGridObject>
{
    public int width, height;
    float cellSize;
    Vector3 originPosition;
    TGridObject[,] gridArray;

    public GridHex(int width, int height, float cellSize, Vector3 originPosition,
        Func<GridHex<TGridObject>, int, int, TGridObject> createGridObject)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;
        gridArray = new TGridObject[width, height];
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                gridArray[x, y] = createGridObject(this, x, y);
            }
        }
    }

    public Vector3 GetWorldPosition(int x, int y){
        return 
            new Vector3(x, 0) * cellSize + 
            new Vector3(0, y) * cellSize * .87f +
            ((y%2) == 1 ? new Vector3(.5f, 0) * cellSize : Vector3.zero) +
            originPosition;
    }
}

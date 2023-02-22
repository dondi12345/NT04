using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public enum Tetromino
{
    I, 
    J, 
    L, 
    O, 
    S, 
    T, 
    Z,
    unknow
}

[System.Serializable]
public struct TetrominoData
{
    public Tile tile;
    public Tetromino tetromino;

    public Vector2Int[] cells { get; private set; }
    public List<Vector2Int[]> variationCells;

    public void Initialize()
    {
        cells = TetrominoCells.Cells[tetromino];
        // wallKicks = Data.WallKicks[tetromino];

        if(this.variationCells == null) this.variationCells = new List<Vector2Int[]>();
        Vector2Int[,] listCell = TetrominoCells.ListCells[tetromino];
        for (int i = 0; i < listCell.GetLength(0); i++)
        {
            Vector2Int[] varCell = new Vector2Int[listCell.GetLength(1)];
            for (int j = 0; j < listCell.GetLength(1); j++)
            {
                varCell[j] = listCell[i,j];
            }
            this.variationCells.Add(varCell);
        }
    }

}
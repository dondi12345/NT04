using UnityEngine;
using UnityEngine.Tilemaps;

public enum Tetromino
{
    I, 
    J, 
    L, 
    O, 
    S, 
    T, 
    Z
}

[System.Serializable]
public struct TetrominoData
{
    public Tile tile;
    public Tetromino tetromino;

    public Vector2Int[] cells { get; private set; }

    public void Initialize()
    {
        cells = TetrominoCells.Cells[tetromino];
        // wallKicks = Data.WallKicks[tetromino];
    }

}
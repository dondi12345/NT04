using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : LoadBehaviour
{
    public Tilemap tilemap;
    // public Piece activePiece { get; private set; }
    public Piece piece;

    public bool isWorking = false;

    public RectInt Bounds {
        get
        {
            Vector2Int position = new Vector2Int(-TetrisManager.instance.boardSize.x / 2, - TetrisManager.instance.boardSize.y / 2);
            return new RectInt(position, TetrisManager.instance.boardSize);
        }
    }

    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTilemap();
    }

    protected void LoadTilemap(){
        tilemap = GetComponentInChildren<Tilemap>();
    }

    public void ClearLines()
    {
        RectInt bounds = Bounds;
        int row = bounds.yMin;

        // Clear from bottom to top
        while (row < bounds.yMax)
        {
            // Only advance to the next row if the current is not cleared
            // because the tiles above will fall down when a row is cleared
            if (IsLineFull(row)) {
                LineClear(row);
            } else {
                row++;
            }
        }
    }

    public bool IsLineFull(int row)
    {
        RectInt bounds = Bounds;

        for (int col = bounds.xMin; col < bounds.xMax; col++)
        {
            Vector3Int position = new Vector3Int(col, row, 0);

            // The line is not full if a tile is missing
            if (!tilemap.HasTile(position)) {
                return false;
            }
        }

        return true;
    }

    public void LineClear(int row)
    {
        RectInt bounds = Bounds;

        // Clear all tiles in the row
        for (int col = bounds.xMin; col < bounds.xMax; col++)
        {
            Vector3Int position = new Vector3Int(col, row, 0);
            tilemap.SetTile(position, null);
        }

        // Shift every row above down one
        while (row < bounds.yMax)
        {
            for (int col = bounds.xMin; col < bounds.xMax; col++)
            {
                Vector3Int position = new Vector3Int(col, row + 1, 0);
                TileBase above = tilemap.GetTile(position);

                position = new Vector3Int(col, row, 0);
                tilemap.SetTile(position, above);
            }

            row++;
        }
    }

    public void Set(Piece piece)
    {
        this.isWorking = true;
        if(piece == null) return;
        for (int i = 0; i < piece.cellDatas.Length; i++)
        {
            Vector3Int tilePosition = piece.cellDatas[i].position + piece.position;
            tilemap.SetTile(tilePosition, piece.data.tile);
        }
        this.ClearLines();
        this.SpawnRow();
        this.isWorking = false;
    }

    public void Replay(){
        this.tilemap.ClearAllTiles();
        this.isWorking = false;
        this.piece = null;
    }

    public void SpawnRow(){
        RectInt bounds = Bounds;

        int row = bounds.yMax;
        while (row > bounds.yMin)
        {
            for (int col = bounds.xMin; col < bounds.xMax; col++)
            {
                Vector3Int positionUnder = new Vector3Int(col, row - 1, 0);
                TileBase under = tilemap.GetTile(positionUnder);

                Vector3Int position = new Vector3Int(col, row, 0);
                tilemap.SetTile(position, under);
                
            }
            Debug.LogWarning(row);
            row--;
        }

        for (int col = bounds.xMin; col < bounds.xMax; col++)
        {
            Vector3Int positionBottom = new Vector3Int(col, bounds.yMin, 0);
            Tile tile = TetrisManager.instance.tetrominoes[Random.Range(0, TetrisManager.instance.tetrominoes.Length)].tile;
            tilemap.SetTile(positionBottom, tile);
        }
        Vector3Int positionNull = new Vector3Int(Random.Range(bounds.xMin, bounds.xMax),bounds.yMin,0);
        tilemap.SetTile(positionNull, null);
    }
}

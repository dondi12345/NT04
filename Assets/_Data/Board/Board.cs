using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : LoadBehaviour
{
    public Tilemap tilemap;
    // public Piece activePiece { get; private set; }
    public Vector3Int spawnPosition = new Vector3Int(-1, 8, 0);

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

    public void Set(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            tilemap.SetTile(tilePosition, piece.data.tile);
        }
    }
}

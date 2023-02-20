using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Ghost : LoadBehaviour
{
    public Tilemap tilemap;
    public Tile tile;
    public Vector3Int[] cells;
    public Vector3Int position;

    public List<Vector3Int> positions;

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
        this.cells = piece.cells;
        this.position = piece.position;
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + piece.position;
            tilemap.SetTile(tilePosition, tile);
        }
    }

    public void Clear(){
        for (int i = 0; i < cells.Length; i++)
        {
            Vector3Int tilePosition = cells[i] + position;
            tilemap.SetTile(tilePosition, null);
        }
    }



}

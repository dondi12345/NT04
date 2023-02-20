using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class CellData
{
    public Vector3Int position;
    public Tile tile;

    public CellData(Vector3Int position, Tile tile){
        this.position = position;
        this.tile = tile;
    }
}

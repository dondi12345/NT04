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

    public CellData(CellData cellData){
        this.position = cellData.position;
        this.tile = cellData.tile;
    }

    public static CellData[] Copy(CellData[] cellDatasIn){
        CellData[] cellDatas = new CellData[cellDatasIn.Length];
        for (int i = 0; i < cellDatasIn.Length; i++)
        {
            cellDatas[i] = new CellData(cellDatasIn[i]);
        }
        return cellDatas;
    }
}

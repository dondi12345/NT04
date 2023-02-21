using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PieceData
{
    public Vector3Int position;
    public CellData[] cellDatas;
    public float minY;

    public PieceData(Vector3Int position, CellData[] cellDatas){
        this.position = position;
        this.cellDatas = CellData.Copy(cellDatas);
    }

    public void CountMinY(){
        this.minY = 100;
        foreach (CellData item in this.cellDatas)
        {
            if(minY > item.position.y) continue;
            minY = item.position.y;
        }
        this.minY += this.position.y;
    }

}

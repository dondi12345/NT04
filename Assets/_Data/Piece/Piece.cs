using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : LoadBehaviour
{
    public TetrominoData data { get; private set; }
    public CellData[] cellDatas;
    public Vector3Int position;

    public void Initialize(Vector3Int position, TetrominoData data)
    {
        this.data = data;
        this.position = position;
        this.cellDatas = new CellData[data.cells.Length];
        for (int i = 0; i < data.cells.Length; i++)
        {
            CellData cellData = new CellData((Vector3Int) data.cells[i], data.tile);
            cellDatas[i] = cellData;
        }
    }

    public void SetPositionCell(Vector2Int[] positions){
        if(this.cellDatas == null) this.cellDatas = new CellData[positions.Length];
        for (int i = 0; i < positions.Length; i++)
        {
            this.cellDatas[i].position = (Vector3Int) positions[i];
        }
    }

    public void SetData(PieceData pieceData){
        this.position = pieceData.position;
        this.cellDatas = CellData.Copy(pieceData.cellDatas);
    }
}

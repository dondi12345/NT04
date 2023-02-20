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
}

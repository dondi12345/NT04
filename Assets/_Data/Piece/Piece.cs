using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : LoadBehaviour
{
    public TetrominoData data { get; private set; }
    public Vector3Int[] cells { get; private set; }
    public CellData[] cellDatas;
    public Vector3Int position { get; set; }

    public void Initialize(Vector3Int position, TetrominoData data)
    {
        this.data = data;
        this.position = position;

        if (cells == null) {
            cells = new Vector3Int[data.cells.Length];
        }

        for (int i = 0; i < cells.Length; i++) {
            cells[i] = (Vector3Int)data.cells[i];
        }
        this.cellDatas = new CellData[data.cells.Length];
        for (int i = 0; i < data.cells.Length; i++)
        {
            cellDatas[i].position = (Vector3Int) data.cells[i];
            cellDatas[i].tile = data.tile;
        }
    }
}

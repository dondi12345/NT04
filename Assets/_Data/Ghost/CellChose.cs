using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellChose : LoadBehaviour
{
    public Vector3Int postion;
    public PieceChose pieceChose;
    void OnMouseDown()
    {
        if(this.pieceChose == null) return;
        this.pieceChose.Onclick();
    }
}

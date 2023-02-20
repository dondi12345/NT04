using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceChose : LoadBehaviour
{
    public Tetromino tetromino;
    public Vector3Int postion;
    public Ghost ghost;
    void OnMouseDown()
    {
        this.ghost.SelectPosition(this.postion);
    }
}

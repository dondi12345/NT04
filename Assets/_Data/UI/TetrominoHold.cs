using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrominoHold : LoadBehaviour
{
    public TetrominoView tetrominoView;

    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTetrominoView();
    }

    protected void LoadTetrominoView(){
        if(this.tetrominoView != null) return;
        this.tetrominoView = transform.Find("TetrominoView").GetComponent<TetrominoView>();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        this.UpdateData();
    }

    protected void UpdateData(){
        this.tetrominoView.UpdateData(TetrisManager.instance.holdTetromino);
    }
}

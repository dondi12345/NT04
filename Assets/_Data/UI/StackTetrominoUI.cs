using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackTetrominoUI : LoadBehaviour
{
    public List<TetrominoView> tetrominoViews;

    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTetrominoView();
    }

    protected void LoadTetrominoView(){
        this.tetrominoViews.Clear();
        foreach (Transform item in transform)
        {
            if(item.TryGetComponent<TetrominoView>(out TetrominoView tetrominoView)){
                this.tetrominoViews.Add(tetrominoView);
            }
        }
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        this.UpdateData();
    }

    public void UpdateData(){
        for (int i = 0; i < TetrisManager.instance.stackTetromino.Count; i++)
        {
            this.tetrominoViews[i].UpdateData(TetrisManager.instance.stackTetromino[i]);
        }
    }
}

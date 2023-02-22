using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrominoView : LoadBehaviour
{
    public List<TetrominoUI> tetrominoUIs;

    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTetrominoUI();
    }

    protected void LoadTetrominoUI(){
        this.tetrominoUIs.Clear();
        foreach (Transform item in transform)
        {
            if(item.TryGetComponent<TetrominoUI>(out TetrominoUI tetrominoUI)){
                this.tetrominoUIs.Add(tetrominoUI);
            }
        }
    }

    public void UpdateData(Tetromino tetromino){
        foreach (TetrominoUI item in this.tetrominoUIs)
        {
            if(item.tetromino == tetromino) item.gameObject.SetActive(true);
            else item.gameObject.SetActive(false);
        }
    }
}

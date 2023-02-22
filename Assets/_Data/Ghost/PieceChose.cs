using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceChose : LoadBehaviour
{
    public List<CellChose> cellChoses;

    public List<Transform> borderCells;
    public Vector3Int position;
    public Transform mask;
    public Vector3 positionMask;

    public float height = 0;
    public float width = 0;

    public Ghost ghost;
    public PieceData pieceData;

    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCellChose();
        this.LoadBorderCells();
        this.LoadMask();
    }

    protected void LoadCellChose(){
        this.cellChoses.Clear();
        foreach (Transform item in transform.Find("Front"))
        {
            if(item.TryGetComponent<CellChose>(out CellChose cellChose)){
                this.cellChoses.Add(cellChose);
            }
        }
    }
    protected void LoadBorderCells(){
        this.borderCells.Clear();
        foreach (Transform item in transform.Find("Back"))
        {
            this.borderCells.Add(item);
        }
    }

    protected void LoadMask(){
        this.mask = transform.Find("Front").Find("Mask");
    }

    public void SetData(PieceData pieceData, Ghost ghost){
        this.ghost = ghost;
        this.pieceData = pieceData;
        int minX = 100;
        int minY = 100;
        int maxX = -100;
        int maxY = -100;
        this.position = pieceData.position;
        transform.position = pieceData.position;
        for (int i = 0; i < this.cellChoses.Count; i++)
        {
            Vector3Int positionCell = pieceData.cellDatas[i].position;
            if(positionCell.x > maxX) maxX = positionCell.x;
            if(positionCell.y > maxY) maxY = positionCell.y;
            if(positionCell.x < minX) minX = positionCell.x;
            if(positionCell.y < minY) minY = positionCell.y;

            this.cellChoses[i].transform.localPosition = positionCell;
            this.cellChoses[i].postion = position;
            this.cellChoses[i].pieceChose = this;
            this.borderCells[i].localPosition = positionCell;
        }
        this.height = maxY - minY +1;
        this.width = maxX - minX + 1;
        this.mask.localScale = new Vector3(this.width, this.height, 1);
        this.positionMask = new Vector3(this.width/2 - 0.5f + minX, this.height/2 - 0.5f + minY, 0);
        mask.localPosition = this.positionMask;
    }

    public void UpdateTime(float time){
        if(time >= 1) {
            this.Onclick();
            return;
        }
        this.mask.localPosition = new Vector3(this.positionMask.x, this.positionMask.y - time * this.height, 0);
    }

    public void Onclick(){
        this.ghost.SelectPosition(this.pieceData);
    }
}

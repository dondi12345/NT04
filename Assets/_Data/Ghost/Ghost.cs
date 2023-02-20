using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Ghost : LoadBehaviour
{
    public Tilemap tilemap;
    public Tile tile;
    public CellData[] cellDatas;
    public Vector3Int position;

    public List<Vector3Int> positionsGuess;
    public Piece piece;
    public Transform holder;
    public PieceChose pieceChose;

    public bool isWorking = false;

    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTilemap();
        this.LoadHoler();
        this.LoadPieceChose();
    }

    protected void LoadTilemap(){
        tilemap = GetComponentInChildren<Tilemap>();
    }

    protected void LoadHoler(){
        this.holder = transform.Find("Holder");
    }

    protected void LoadPieceChose(){
        this.pieceChose = transform.Find("Collections").Find("PieceChose").GetComponent<PieceChose>();
    }

    public void Clear(){
        for (int i = 0; i < this.cellDatas.Length; i++)
        {
            Vector3Int tilePosition = this.cellDatas[i].position + this.position;
            tilemap.SetTile(tilePosition, null);
        }
    }

    public void SetGuess(Piece piece){
        this.isWorking = true;
        this.piece = piece;

        this.positionsGuess.Clear();
        RectInt bounds = TetrisManager.instance.Bounds;
        for (int i = bounds.xMin; i < bounds.xMax; i++)
        {
            Vector3Int posStart = new Vector3Int(i, bounds.yMax -2, 0);
            if(!TetrisManager.instance.IsValidPosition(piece,posStart)) continue;
            posStart = TetrisManager.instance.Drop(piece, posStart);
            this.positionsGuess.Add(posStart);
        }
        this.InstancePieceChose();
    }

    public void InstancePieceChose(){
        NTFunction.ClearChild(this.holder);
        List<Vector3Int> temp = new List<Vector3Int>(); 
        foreach (Vector3Int item in this.positionsGuess)
        {
            temp.Add(item);
        }
        for (int time = 0; time < 3; time++)
        {
            Vector3Int position = temp[Random.Range(0, temp.Count)];
            temp.Remove(position);
            for (int i = 0; i < this.piece.cellDatas.Length; i++)
            {
                PieceChose pc = Instantiate<PieceChose>(this.pieceChose);
                pc.transform.position = this.piece.cellDatas[i].position + position;
                pc.postion = position;
                pc.ghost = this;
                pc.transform.SetParent(this.holder);
                pc.gameObject.SetActive(true);
            }
        }
    }

    public void SelectPosition(Vector3Int position){
        if(!this.isWorking) return;
        TetrisManager.instance.SetWaiting(position);
        this.isWorking = false;
    }

    public void Replay(){
        this.tilemap.ClearAllTiles();
        this.isWorking = false;
        this.piece = null;
        NTFunction.ClearChild(this.holder);
    }


}

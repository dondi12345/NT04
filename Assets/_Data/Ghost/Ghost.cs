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
    public List<PieceData> pieceDatasGuess;
    public Piece piece;
    public Transform holder;
    public PieceChose pieceChose;
    public List<PieceChose> pieceChoses;

    public bool isWorking = false;

    public float timeDelay = 3;
    public float countDelay = 0; 

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

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if(!this.isWorking) return;
        this.UpdatePieceChose();
    }

    protected void UpdatePieceChose(){
        this.countDelay += Time.fixedDeltaTime;
        foreach (PieceChose item in this.pieceChoses)
        {
            item.UpdateTime(this.countDelay/ this.timeDelay);
        }
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

        this.LoadSelection(piece);

        this.InstancePieceChose();
    }

    public void LoadSelection(Piece piece){
        this.pieceDatasGuess.Clear();
        List<PieceData> tempPieceData = new List<PieceData>();
        RectInt bounds = TetrisManager.instance.Bounds;

        for (int i = 0; i < piece.data.variationCells.Count; i++)
        {
            piece.SetPositionCell(piece.data.variationCells[i]);

            for (int j = bounds.xMin; j < bounds.xMax; j++)
            {
                Vector3Int posStart = new Vector3Int(j, bounds.yMax -2, 0);
                if(!TetrisManager.instance.IsValidPosition(piece,posStart)) continue;
                posStart = TetrisManager.instance.Drop(piece, posStart);
                PieceData pieceData = new PieceData(posStart, piece.cellDatas);
                pieceData.CountMinY();
                tempPieceData.Add(pieceData);
            }
        }
        for (int i = 0; i < 4; i++)
        {
            if(tempPieceData.Count < 1) break;
            float minY = 100;
            PieceData pieceDataMinY = tempPieceData[0];
            foreach (PieceData item in tempPieceData)
            {
                if(minY < item.minY) continue;
                minY = item.minY;
                pieceDataMinY = item;
            }
            tempPieceData.Remove(pieceDataMinY);
            this.pieceDatasGuess.Add(pieceDataMinY);
        }
        for (int i = 0; i < 4; i++)
        {
            if(tempPieceData.Count < 1) break;
            float minPosY = 100;
            PieceData pieceDataMinPosY = tempPieceData[0];
            foreach (PieceData item in tempPieceData)
            {
                if(minPosY < item.position.y) continue;
                minPosY = item.position.y;
                pieceDataMinPosY = item;
            }
            tempPieceData.Remove(pieceDataMinPosY);
            this.pieceDatasGuess.Add(pieceDataMinPosY);
        }
        for (int i = 0; i < 4; i++)
        {
            if(tempPieceData.Count < 1) break;
            int index = Random.Range(0, tempPieceData.Count);
            this.pieceDatasGuess.Add(tempPieceData[index]);
        }

    }

    public int timeRefresh = 0;
    public void InstancePieceChose(){
        NTFunction.ClearChild(this.holder);
        this.pieceChoses.Clear();

        int step = this.timeRefresh%6;
        for (int time = 0; time < 2; time++)
        {
            int index = step + time*6;
            if(index >= pieceDatasGuess.Count) break;
            PieceData pieceData = pieceDatasGuess[index];

            PieceChose pc = Instantiate<PieceChose>(this.pieceChose);
            pc.SetData(pieceData,this);
            pc.transform.SetParent(this.holder);
            pc.gameObject.SetActive(true);
            this.pieceChoses.Add(pc);

            // this.tilemap.ClearAllTiles();
            // this.piece.SetData(pieceData);
            // this.Set(this.piece);
        }
    }

    public void Set(Piece piece)
    {
        this.piece = piece;
        for (int i = 0; i < piece.cellDatas.Length; i++)
        {
            Vector3Int tilePosition = piece.cellDatas[i].position + piece.position;
            tilemap.SetTile(tilePosition, this.tile);
        }
    }

    public void SelectPosition(PieceData pieceData){
        if(!this.isWorking) return;
        this.countDelay = 0;
        TetrisManager.instance.SetWaiting(pieceData);
        this.isWorking = false;
    }

    public void NexGess(){
        this.timeRefresh++;
        this.InstancePieceChose();
    }

    public void Replay(){
        this.tilemap.ClearAllTiles();
        this.isWorking = false;
        this.piece = null;
        NTFunction.ClearChild(this.holder);
    }


}

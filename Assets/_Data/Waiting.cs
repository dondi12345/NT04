using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Waiting : LoadBehaviour
{
    public Tilemap tilemap;
    public Piece piece;

    public bool isWorking = false;

    public float timeDelayMove = 0.5f;
    public float countDelayMove = 0;
    
    public Vector3Int target;

    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadTilemap();
    }

    protected void LoadTilemap(){
        tilemap = GetComponentInChildren<Tilemap>();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if(!this.isWorking) return;
        this.MovePiece();
    }

    public void MovePiece(){
        if(this.piece == null){
            this.isWorking = false;
            return;
        }
        this.isWorking = true;
        if(this.piece.position.x > this.target.x) this.Move(Vector2Int.left);
        else if(this.piece.position.x < this.target.x) this.Move(Vector2Int.right);
        else this.Move(Vector2Int.down);
    }

    private void Move(Vector2Int translation)
    {
        this.countDelayMove += Time.fixedDeltaTime;
        if(this.countDelayMove < this.timeDelayMove) return;

        Vector3Int newPosition = this.piece.position;
        newPosition.x += translation.x;
        newPosition.y += translation.y;

        bool valid = TetrisManager.instance.IsValidPosition(this.piece, newPosition);

        // Only save the movement if the new position is valid
        if (valid)
        {
            this.Clear();
            piece.position = newPosition;
            this.countDelayMove = 0;
            this.Set(this.piece);
        }else{
            TetrisManager.instance.board.Set(this.piece);
            this.piece = null;
            this.isWorking = false;
        }
    }

    public void Initialize(Piece piece, Vector3Int positionTarget)
    {
        this.isWorking = true;
        this.tilemap.ClearAllTiles();
        this.piece = piece;
        this.target = positionTarget;
        this.Set(this.piece);
    }

    public void Set(Piece piece)
    {
        this.piece = piece;
        for (int i = 0; i < piece.cellDatas.Length; i++)
        {
            Vector3Int tilePosition = piece.cellDatas[i].position + piece.position;
            tilemap.SetTile(tilePosition, piece.cellDatas[i].tile);
        }
    }

    public void Clear(){
        if(this.piece == null) return;
        for (int i = 0; i < piece.cellDatas.Length; i++)
        {
            Vector3Int tilePosition = piece.cellDatas[i].position + piece.position;
            tilemap.SetTile(tilePosition, null);
        }
    }

    public void Replay(){
        this.tilemap.ClearAllTiles();
        this.isWorking = false;
        this.piece = null;
    }
}

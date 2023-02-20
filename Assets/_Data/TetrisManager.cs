using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisManager : LoadBehaviour
{
    public TetrominoData[] tetrominoes;
    public Board board;
    public Waiting waiting;
    public Vector2Int boardSize = new Vector2Int(10, 20);
    public Ghost ghost;
    public Piece activePiece;
    public Piece chosePiece;

    public Vector3Int spawnPosition = new Vector3Int(-1, 8, 0);

    public RectInt Bounds {
        get
        {
            Vector2Int position = new Vector2Int(-boardSize.x / 2, -boardSize.y / 2);
            return new RectInt(position, boardSize);
        }
    }

    public static TetrisManager instance;
    protected override void Awake()
    {
        base.Awake();
        if (TetrisManager.instance != null) Debug.LogError("Only 1 TetrisManager allow");
        TetrisManager.instance = this;
    }


    public override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBoard();
        this.LoadTetromino();
        this.LoadActivePiece();
        this.ghost = GetComponentInChildren<Ghost>();
        this.waiting = GetComponentInChildren<Waiting>();
    }

    protected void LoadBoard(){
        this.board = transform.Find("Board").GetComponent<Board>();
    }

    protected void LoadTetromino(){
        for (int i = 0; i < tetrominoes.Length; i++) {
            tetrominoes[i].Initialize();
        }
    }

    protected void LoadActivePiece(){
        activePiece = transform.Find("ActivePiece").GetComponent<Piece>();
        chosePiece = transform.Find("ChosePiece").GetComponent<Piece>();
    }

    protected override void Start()
    {
        this.SpawnPiece();
    }

    public void SpawnPiece(){
        this.SpawnActivePiece();
        this.SpanwChosePiece();
    }

    public void SpawnActivePiece()
    {
        int random = Random.Range(0, tetrominoes.Length);
        TetrominoData data = tetrominoes[random];

        activePiece.Initialize(spawnPosition, data);
        // this.board.Set(activePiece);

        if (IsValidPosition(activePiece, spawnPosition)) {
            this.waiting.Set(activePiece);
        } else {
            this.GameOver();
        }
    }

    public void SpanwChosePiece(){

        this.chosePiece.Initialize(this.Drop(this.activePiece), this.activePiece.data);
        this.ghost.Set(chosePiece);
    }

    private Vector3Int Drop(Piece piece)
    {
        Vector3Int position = piece.position;
        Vector3Int positionValid = piece.position;

        int current = position.y;
        int bottom = -this.boardSize.y / 2 - 1;

        for (int row = current; row >= bottom; row--)
        {
            position.y = row;

            if (this.IsValidPosition(piece, position)) {
                positionValid = position;
            } else {
                break;
            }
        }
        return positionValid;
    }



    public void Down(){
        this.board.Set(this.chosePiece);
        this.waiting.Clear();
        this.ghost.Clear();
        this.SpawnPiece();
    }

    public bool IsValidPosition(Piece piece, Vector3Int position)
    {
        RectInt bounds = Bounds;

        // The position is only valid if every cell is valid
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + position;

            // An out of bounds tile is invalid
            if (!bounds.Contains((Vector2Int)tilePosition)) {
                return false;
            }

            // A tile already occupies the position, thus invalid
            if (this.board.tilemap.HasTile(tilePosition)) {
                return false;
            }
        }

        return true;
    }

    public void GameOver(){
        this.board.tilemap.ClearAllTiles();
    }

}

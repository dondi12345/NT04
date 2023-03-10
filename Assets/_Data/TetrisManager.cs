using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisManager : LoadBehaviour
{
    public TetrominoData[] tetrominoes;
    public Board board;
    public Waiting waiting;
    public Vector2Int boardSize = new Vector2Int(8, 18);
    public Ghost ghost;
    public Piece activePiece;

    public Tetromino holdTetromino = Tetromino.unknow;
    public Tetromino activeTetromino = Tetromino.unknow;
    public List<Tetromino> stackTetromino;

    public bool isPieceInStack = false;
    public bool isGameOver = false;

    public Vector3Int spawnPosition = new Vector3Int(0, 7, 0);

    public int timeMaxSpwan = 5;
    public int step = 0;
    public int currentMaxSpwan = 5;
    public int countSpwan = 0;
    public int timeDelaySelection = 7;

    public int score = 0;
    public GameOverUI gameOverUI;

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
    }

    protected override void OnEnable()
    {
        this.Replay();
    }

    protected override void FixedUpdate()
    {
        if(this.ghost.isWorking) return;
        if(this.waiting.isWorking) return;
        if(this.board.isWorking) return;
        if(this.isGameOver) return;
        this.PushStackTetromino();
        if(this.activeTetromino == Tetromino.unknow){
            this.activeTetromino = this.stackTetromino[0];
            this.stackTetromino.RemoveAt(0);
        }
        this.PushStackTetromino();
        this.SpawnPiece(this.activeTetromino);
    }

    public void PushStackTetromino(){
        if(this.stackTetromino.Count >= 3) return;
        int random = Random.Range(0, tetrominoes.Length);
        Tetromino tetromino = tetrominoes[random].tetromino;
        this.stackTetromino.Add(tetromino);
        this.PushStackTetromino();

    }

    public void SpawnPiece(Tetromino tetromino){
        this.currentMaxSpwan = this.timeMaxSpwan - this.step / 10;
        if(this.currentMaxSpwan <=0 ) this.currentMaxSpwan = 1;
        this.step ++;

        // int random = Random.Range(0, tetrominoes.Length);
        TetrominoData data = this.GetTetrominoDataByCode(tetromino);
        // TetrominoData data = tetrominoes[1];

        activePiece.Initialize(spawnPosition, data);
        // this.board.Set(activePiece);

        if (IsValidPosition(activePiece, spawnPosition)) {
            this.waiting.Set(activePiece);
        } else {
            this.GameOver();
        }
        this.ghost.SetGuess(activePiece);
    }

    public void SetWaiting(PieceData pieceData){
        this.activePiece.SetData(pieceData);
        this.activePiece.position = this.spawnPosition;
        this.waiting.Initialize(this.activePiece, pieceData.position);
        this.activeTetromino = Tetromino.unknow;
    }

    public Vector3Int Drop(Piece piece, Vector3Int position)
    {
        Vector3Int positionValid = position;

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

    public bool IsValidPosition(Piece piece, Vector3Int position)
    {
        RectInt bounds = Bounds;

        // The position is only valid if every cell is valid
        for (int i = 0; i < piece.cellDatas.Length; i++)
        {
            Vector3Int tilePosition = piece.cellDatas[i].position + position;

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
        this.isGameOver = true;
        Debug.LogWarning("Game Over");
        this.gameOverUI.gameObject.SetActive(true);
    }

    public void Replay(){
        this.board.Replay();
        this.ghost.Replay();
        this.waiting.Replay();
        this.isGameOver = false;
        this.currentMaxSpwan = this.timeMaxSpwan;
        this.countSpwan = 0;
        this.step = 0;
        this.score = 0;
        this.gameOverUI.gameObject.SetActive(false);
    }

    public void Hold(){
        if(this.activeTetromino == Tetromino.unknow) return;
        if(this.holdTetromino != Tetromino.unknow){
            Tetromino temp = this.holdTetromino;
            this.holdTetromino = this.activeTetromino;
            this.activeTetromino = temp;
            this.ghost.Replay();
            this.waiting.Replay();
            return;
        }
        this.holdTetromino = this.activeTetromino;
        this.activeTetromino = this.stackTetromino[0];
        this.stackTetromino.RemoveAt(0);
        this.PushStackTetromino();
        this.ghost.Replay();
        this.waiting.Replay();
    }

    public TetrominoData GetTetrominoDataByCode(Tetromino tetromino){
        for (int i = 0; i < tetrominoes.Length; i++)
        {
            if(tetrominoes[i].tetromino == tetromino) return tetrominoes[i];
        }
        return this.tetrominoes[0];
    }

}

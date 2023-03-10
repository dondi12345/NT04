using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TetrominoCells
{
    public static readonly Dictionary<Tetromino, Vector2Int[]> Cells = new Dictionary<Tetromino, Vector2Int[]>()
    {
        { Tetromino.I, new Vector2Int[] { new Vector2Int(-1, 1), new Vector2Int( 0, 1), new Vector2Int( 1, 1), new Vector2Int( 2, 1) } },
        { Tetromino.L, new Vector2Int[] { new Vector2Int(-1, 1), new Vector2Int(-1, 0), new Vector2Int( 0, 0), new Vector2Int( 1, 0) } },
        { Tetromino.J, new Vector2Int[] { new Vector2Int( 1, 1), new Vector2Int(-1, 0), new Vector2Int( 0, 0), new Vector2Int( 1, 0) } },
        { Tetromino.O, new Vector2Int[] { new Vector2Int( 0, 1), new Vector2Int( 1, 1), new Vector2Int( 0, 0), new Vector2Int( 1, 0) } },
        { Tetromino.S, new Vector2Int[] { new Vector2Int( 0, 1), new Vector2Int( 1, 1), new Vector2Int(-1, 0), new Vector2Int( 0, 0) } },
        { Tetromino.T, new Vector2Int[] { new Vector2Int( 0, 1), new Vector2Int(-1, 0), new Vector2Int( 0, 0), new Vector2Int( 1, 0) } },
        { Tetromino.Z, new Vector2Int[] { new Vector2Int(-1, 1), new Vector2Int( 0, 1), new Vector2Int( 0, 0), new Vector2Int( 1, 0) } },
    };

    public static readonly Dictionary<Tetromino, Vector2Int[,]> ListCells = new Dictionary<Tetromino, Vector2Int[,]>()
    {
        { Tetromino.I, new Vector2Int[,] { 
            {new Vector2Int(-1, 1), new Vector2Int( 0, 1), new Vector2Int( 1, 1), new Vector2Int( 2, 1) }, 
            {new Vector2Int(0, -2), new Vector2Int( 0, -1), new Vector2Int( 0, 0), new Vector2Int( 0, 1) }, 
        }},
        { Tetromino.L, new Vector2Int[,] { 
            {new Vector2Int(-1, 1), new Vector2Int(-1, 0), new Vector2Int( 0, 0), new Vector2Int( 1, 0) }, 
            {new Vector2Int(0, -1), new Vector2Int( 0, 0), new Vector2Int( 0, 1), new Vector2Int( 1, 1) }, 
            {new Vector2Int(-1, 1), new Vector2Int( 0, 1), new Vector2Int( 1, 1), new Vector2Int( 1, 0) }, 
            {new Vector2Int(-1, -1), new Vector2Int( 0, -1), new Vector2Int( 0, 0), new Vector2Int( 0, 1) }, 
        }},
        { Tetromino.J, new Vector2Int[,] { 
            { new Vector2Int( 1, 1), new Vector2Int(-1, 0), new Vector2Int( 0, 0), new Vector2Int( 1, 0) }, 
            {new Vector2Int(0, -1), new Vector2Int( 0, 0), new Vector2Int( 0, 1), new Vector2Int( 1, -1) }, 
            {new Vector2Int(-1, 1), new Vector2Int( 0, 1), new Vector2Int( 1, 1), new Vector2Int( -1, 0) }, 
            {new Vector2Int(-1, 1), new Vector2Int( 0, -1), new Vector2Int( 0, 0), new Vector2Int( 0, 1) }, 
        }},
        { Tetromino.O, new Vector2Int[,] { 
            { new Vector2Int( 0, 1), new Vector2Int( 1, 1), new Vector2Int( 0, 0), new Vector2Int( 1, 0) },
        }},
        { Tetromino.S, new Vector2Int[,] { 
            { new Vector2Int( 0, 1), new Vector2Int( 1, 1), new Vector2Int(-1, 0), new Vector2Int( 0, 0) },
            { new Vector2Int( 1, -1), new Vector2Int( 1, 0), new Vector2Int(0, 1), new Vector2Int( 0, 0) },
        }},
        { Tetromino.T, new Vector2Int[,] { 
            { new Vector2Int( 0, 1), new Vector2Int(-1, 0), new Vector2Int( 1, 0), new Vector2Int( 0, 0) },
            { new Vector2Int( 0, 1), new Vector2Int(-1, 0), new Vector2Int( 0, -1), new Vector2Int( 0, 0) },
            { new Vector2Int( 0, -1), new Vector2Int(-1, 0), new Vector2Int( 1, 0), new Vector2Int( 0, 0) },
            { new Vector2Int( 0, -1), new Vector2Int(0, 1), new Vector2Int( 1, 0), new Vector2Int( 0, 0) },
        }},
        { Tetromino.Z, new Vector2Int[,] { 
            { new Vector2Int(-1, 1), new Vector2Int( 0, 1), new Vector2Int( 0, 0), new Vector2Int( 1, 0) },
            { new Vector2Int( -1, -1), new Vector2Int( -1, 0), new Vector2Int(0, 1), new Vector2Int( 0, 0) },
        }},
    };




}

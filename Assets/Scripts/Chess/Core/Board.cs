using System.Collections.Generic;
using UnityEngine;

public class Board {
    
    public static readonly int[] Square = new int[64];
    public static readonly GameObject[] Pieces = new GameObject[64];
    
    public static List<int[]> prevSquares = new();
    public static List<int[]> prevPieces = new();

    public static bool[] enPassantSquare = new bool[64]; //NOSONAR
    public static List<bool[]> prevEnPassantSquares = new(); //NOSONAR
    
    public static List<int> prevColorToMove = new();
    
    public static List<bool> prevWKCastling = new();
    public static List<bool> prevWQCastling = new();
    public static List<bool> prevBKCastling = new();
    public static List<bool> prevBQCastling = new();

    public static int colorToMove = Piece.WHITE; //NOSONAR
    public static int opponentColor = Piece.BLACK; //NOSONAR

    public static bool wKingsideCastle = true; //NOSONAR
    public static bool wQueensideCastle = true; //NOSONAR
    public static bool bKingsideCastle = true; //NOSONAR
    public static bool bQueensideCastle = true; //NOSONAR
    
}

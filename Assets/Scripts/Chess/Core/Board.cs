using System.Collections.ObjectModel;
using UnityEngine;

namespace Core {
    public static class Board {
        public static readonly int[] Square = new int[64];
        public static readonly GameObject[] Pieces = new GameObject[64];
    
        public static Collection<int[]> prevSquares = new();
        public static Collection<int[]> prevPieces = new();

        public static bool[] enPassantSquare = new bool[64]; //NOSONAR
        public static Collection<bool[]> prevEnPassantSquares = new(); //NOSONAR
    
        public static Collection<int> prevColorToMove = new();
    
        public static Collection<bool> prevWKCastling = new();
        public static Collection<bool> prevWQCastling = new();
        public static Collection<bool> prevBKCastling = new();
        public static Collection<bool> prevBQCastling = new();

        public static int colorToMove = Piece.WHITE; //NOSONAR
        public static int opponentColor = Piece.BLACK; //NOSONAR

        public static bool wKingsideCastle = true; //NOSONAR
        public static bool wQueensideCastle = true; //NOSONAR
        public static bool bKingsideCastle = true; //NOSONAR
        public static bool bQueensideCastle = true; //NOSONAR
    
    }
}

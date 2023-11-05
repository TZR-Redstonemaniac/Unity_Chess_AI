using UnityEngine;

public abstract class Piece {

    public const int NONE = 0;
    public const int PAWN = 1;
    public const int KNIGHT = 2;
    public const int BISHOP = 3;
    public const int ROOK = 4;
    public const int KING = 5;
    public const int QUEEN = 6;
    public const int KNOOK = 7;

    public const int WHITE = 32;
    public const int BLACK = 64;

    public static bool IsColor (int piece, int colour) {
        return (piece & colour) != 0;
    }

    public static int PieceType (int piece) {
        return piece > 64 ? piece - 64 : piece - 32;
    }

    public static bool PieceChecker(int pieceToCheck, int referencePiece){
        return PieceType(pieceToCheck) == referencePiece;
    }

    public static bool PieceChecker(int pieceToCheck, int referencePiece, int color){
        return PieceType(pieceToCheck) == referencePiece && IsColor(pieceToCheck, color);
    }

    public static string PieceName(int piece) {
        if (piece > 64) piece -= 64;
        else piece -= 32;

        return piece switch {
            1 => "Pawn",
            2 => "Knight",
            3 => "Bishop",
            4 => "Rook",
            5 => "King",
            6 => "Queen",
            7 => "Knook",
            _ => "None"
        };
    }

    public static string PosFromIndex (int index){
        char file = (char) ('a' + index % 8);
        char rank = (char) ('1' + index / 8);
        return (file + rank).ToString();
    }

    // Rank (0 to 7) of square 
    public static int RankIndex(int squareIndex) {
        return squareIndex >> 3;
    }

    // File (0 to 7) of square 
    public static int FileIndex(int squareIndex) {
        return squareIndex & 0b000111;
    }
}

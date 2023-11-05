using System.Collections.Generic;

public class FenManager {
    
    private const string START_FEN = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";

    private static readonly Dictionary<char, int> pieceTypeFromSymbol = new();

    public static void Init() {
        pieceTypeFromSymbol.Add('k', Piece.KING);
        pieceTypeFromSymbol.Add('p', Piece.PAWN);
        pieceTypeFromSymbol.Add('n', Piece.KNIGHT);
        pieceTypeFromSymbol.Add('b', Piece.BISHOP);
        pieceTypeFromSymbol.Add('r', Piece.ROOK);
        pieceTypeFromSymbol.Add('q', Piece.QUEEN);
        pieceTypeFromSymbol.Add('o', Piece.KNOOK);
        
        LoadFromFen(START_FEN);
    }

    public static void LoadFromFen(string fen) {
        for (int i = 0; i < 64; i++) Board.Square[i] = 0;

        string[] sections = fen.Split(" ");

        int file = 0;
        int rank = 7;

        foreach (char symbol in sections[0]) {
            if (symbol == '/') {
                file = 0;
                rank--;
            }
            else {
                if (char.IsDigit(symbol)) {
                    file += (int)char.GetNumericValue(symbol);
                }
                else {
                    int pieceColour = char.IsUpper(symbol) ? Piece.WHITE : Piece.BLACK;
                    int pieceType = pieceTypeFromSymbol[char.ToLower(symbol)];
                    Board.Square[rank * 8 + file] = pieceType | pieceColour;
                    file++;
                }
            }
        }

        Board.colorToMove = sections[1] == "w" ? Piece.WHITE : Piece.BLACK;
        Board.opponentColor = sections[1] == "w" ? Piece.BLACK : Piece.WHITE;

        string castlingRights = sections.Length > 2 ? sections[2] : "KQkq";
        Board.wKingsideCastle = castlingRights.Contains("K");
        Board.wQueensideCastle = castlingRights.Contains("Q");
        Board.bKingsideCastle = castlingRights.Contains("k");
        Board.bQueensideCastle = castlingRights.Contains("q");
    }
}
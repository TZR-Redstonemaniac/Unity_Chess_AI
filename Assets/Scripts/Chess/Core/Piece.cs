namespace Core {
    public static class Piece {

        public static int NONE;
        public static int PAWN => 1;
        public static int KNIGHT => 2;
        public static int BISHOP => 3;
        public static int ROOK => 4;
        public static int KING => 5;
        public static int QUEEN => 6;
        public static int KNOOK => 7;

        public static int WHITE => 32;
        public static int BLACK => 64;

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
            piece -= piece > 64 ? 64 : 32;

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
            char file = (char) ('a' + RankIndex(index));
            char rank = (char) ('1' + FileIndex(index));
            return (file + rank).ToString();
        }

        // Rank (0 to 7) of square 
        public static int RankIndex(int squareIndex) {
            return squareIndex >> 3; //NOSONAR
        }

        // File (0 to 7) of square 
        public static int FileIndex(int squareIndex) {
            return squareIndex & 0b000111;
        }
    }
}


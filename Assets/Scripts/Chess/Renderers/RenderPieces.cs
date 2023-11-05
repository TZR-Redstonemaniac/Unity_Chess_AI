using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RenderPieces : MonoBehaviour {

    [Header("Config")]
    [SerializeField] private Camera cam;
    [SerializeField] private MovementManager movementManager;

    [Header("White Sprites")]
    [SerializeField] private Sprite WHITE_PAWN;
    [SerializeField] private Sprite WHITE_KNIGHT;
    [SerializeField] private Sprite WHITE_BISHOP;
    [SerializeField] private Sprite WHITE_ROOK;
    [SerializeField] private Sprite WHITE_KING;
    [SerializeField] private Sprite WHITE_QUEEN;
    [SerializeField] private Sprite WHITE_KNOOK;
    
    [Header("Black Sprites")]
    [SerializeField] private Sprite BLACK_PAWN;
    [SerializeField] private Sprite BLACK_KNIGHT;
    [SerializeField] private Sprite BLACK_BISHOP;
    [SerializeField] private Sprite BLACK_ROOK;
    [SerializeField] private Sprite BLACK_KING;
    [SerializeField] private Sprite BLACK_QUEEN;
    [SerializeField] private Sprite BLACK_KNOOK;
    
    private int screenWidth;
    private int screenHeight;

    private readonly List<float> cameraXCoordinates = new();
    private readonly List<float> cameraYCoordinates = new();
    
    private readonly List<float> boardXCoordinates = new();
    private readonly List<float> boardYCoordinates = new();
    
    private float xSize;
    private float ySize;
    
    private void Start() {
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        
        float regionSize = screenWidth / 18.0f;

        for (int yIndex = 1; yIndex <= 8; yIndex++) {
            float y = yIndex * regionSize / screenHeight - .0835f;
            
            cameraYCoordinates.Add(y);
        }
        
        for (int xIndex = 5; xIndex <= 12; xIndex++) {
            float x = xIndex * regionSize / screenWidth;
            
            cameraXCoordinates.Add(x);
        }
        
        ConvertCoordinatesToWorldPosition();

        for (int i = 0; i < Board.Square.Length; i++) {
            int piece = Board.Square[i];
           
            if (Piece.IsColor(piece, Piece.WHITE)) {
                switch (Piece.PieceName(piece)) {
                    case "Pawn":
                        CreatePiece(WHITE_PAWN, i);
                        break;
                    case "Knight":
                        CreatePiece(WHITE_KNIGHT, i);
                        break;
                    case "Bishop":
                        CreatePiece(WHITE_BISHOP, i);
                        break;
                    case "Rook":
                        CreatePiece(WHITE_ROOK, i);
                        break;
                    case "King":
                        CreatePiece(WHITE_KING, i);
                        break;
                    case "Queen":
                        CreatePiece(WHITE_QUEEN, i);
                        break;
                    case "Knook":
                        CreatePiece(WHITE_KNOOK, i);
                        break;
                }
            }
            else {
                switch (Piece.PieceName(piece)) {
                    case "Pawn":
                        CreatePiece(BLACK_PAWN, i);
                        break;
                    case "Knight":
                        CreatePiece(BLACK_KNIGHT, i);
                        break;
                    case "Bishop":
                        CreatePiece(BLACK_BISHOP, i);
                        break;
                    case "Rook":
                        CreatePiece(BLACK_ROOK, i);
                        break;
                    case "King":
                        CreatePiece(BLACK_KING, i);
                        break;
                    case "Queen":
                        CreatePiece(BLACK_QUEEN, i);
                        break;
                    case "Knook":
                        CreatePiece(BLACK_KNOOK, i);
                        break;
                }
            }
        }
    }

    private void Update() {
        for (int i = 0; i < Board.Pieces.Length; i++) {
            if (Board.Pieces[i] == null || movementManager.pickedUp) continue;
            
            Board.Pieces[i].transform.position = new Vector3(boardXCoordinates[Piece.FileIndex(i)] + xSize / 2 + 0.01f,
                boardYCoordinates[Piece.RankIndex(i)] + ySize / 2, 0);
        }
    }

    private void ConvertCoordinatesToWorldPosition() {
        foreach (var worldPos in cameraXCoordinates.Select(normalizedX => 
                     cam.ViewportToWorldPoint(new Vector3(normalizedX, 0, 0)))) {
            boardXCoordinates.Add(worldPos.x);
        }

        foreach (var worldPos in cameraYCoordinates.Select(normalizedY => 
                     cam.ViewportToWorldPoint(new Vector3(0, normalizedY, 0)))) {
            boardYCoordinates.Add(worldPos.y);
        }

        xSize = Vector3.Distance(new Vector3(boardXCoordinates[0], 0, 0), new Vector3(boardXCoordinates[1], 0, 0));
        ySize = Vector3.Distance(new Vector3(0, boardYCoordinates[0], 0), new Vector3(0, boardYCoordinates[1], 0));
    }

    private void CreatePiece(Sprite piece, int index) {
        GameObject PieceObject = new GameObject {
            transform = {
                position = new Vector3(boardXCoordinates[Piece.FileIndex(index)] + xSize / 2, boardYCoordinates[Piece.RankIndex(index)] +
                                                                                             ySize / 2, 0),
                parent = transform,
                name = index.ToString()
            }
        };

        PieceObject.AddComponent<SpriteRenderer>().sprite = piece;
        PieceObject.GetComponent<SpriteRenderer>().drawMode = SpriteDrawMode.Sliced;
        PieceObject.GetComponent<SpriteRenderer>().size = new Vector2(xSize, ySize);

        Board.Pieces[index] = PieceObject;
    }
}

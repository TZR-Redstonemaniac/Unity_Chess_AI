using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MovementManager : MonoBehaviour {
    
    [Header("Config")]
    [SerializeField] private Camera cam;
    
    private int screenWidth;
    private int screenHeight;

    private readonly List<float> cameraXCoordinates = new();
    private readonly List<float> cameraYCoordinates = new();

    [HideInInspector] public bool pickedUp;
    private int pickedUpIndex;
    
    private void Start() {
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        
        float regionSize = screenWidth / 18.0f;

        for (int yIndex = 1; yIndex <= 9; yIndex++) {
            float y = yIndex * regionSize / screenHeight - .0835f;
            
            cameraYCoordinates.Add(y);
        }
        
        for (int xIndex = 5; xIndex <= 13; xIndex++) {
            float x = xIndex * regionSize / screenWidth;
            
            cameraXCoordinates.Add(x);
        }
    }

    private void Update() {
        if (screenWidth != Screen.width || screenHeight != Screen.height) {
            screenWidth = Screen.width;
            screenHeight = Screen.height;
        }
        
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 100;

        int x = FindIndices(cameraXCoordinates.ToArray(), mousePos.x / screenWidth);
        int y = FindIndices(cameraYCoordinates.ToArray(), mousePos.y / screenHeight);

        int index = x + 8 * y;
        
        if (x == -1 || y == -1) index = -1;
        
        if (Input.GetButtonDown("Fire1")) PickupObject(index);
        if (Input.GetButtonUp("Fire1")) DropObject(index);
        
        if (pickedUp) MoveObject(cam.ScreenToWorldPoint(mousePos));
    }

    private int FindIndices(float[] sortedArray, float x) {
        if (x < sortedArray[0] || x > sortedArray[^1]) return -1;
        
        int left = -1;

        for (int i = 0; i < sortedArray.Length; i++) {
            if (!(sortedArray[i] > x)) continue;
            
            left = i - 1;
            break;
        }

        return left;
    }

    private void PickupObject(int index) {
        pickedUp = true;

        pickedUpIndex = index;
    }

    private void DropObject(int index) {
        pickedUp = false;
        
        if (pickedUpIndex == index || index == -1) return;

        if (Board.Square[index] != 0) Destroy(Board.Pieces[index]);

        Board.Pieces[index] = Board.Pieces[pickedUpIndex];
        Board.Pieces[index].transform.position = new Vector3(Board.Pieces[index].transform.position.x, Board.Pieces[index].transform.position.y, 0);
        Board.Pieces[index].GetComponent<SpriteRenderer>().sortingOrder = 0;
        Board.Pieces[pickedUpIndex] = null;

        Board.Square[index] = Board.Square[pickedUpIndex];
        Board.Square[pickedUpIndex] = 0;
        
        pickedUpIndex = -1;
    }
    
    private void MoveObject(Vector3 mousePos) {
        mousePos.z = 50;
        Board.Pieces[pickedUpIndex].transform.position = mousePos;
        Board.Pieces[pickedUpIndex].GetComponent<SpriteRenderer>().sortingOrder = 100;
    }
    
}

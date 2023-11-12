using System.Collections.Generic;
using Core;
using Renderers;
using UnityEngine;
// ReSharper disable InvertIf

namespace Managers {
    public class MovementManager : MonoBehaviour {

        [Header("Config")] 
        [SerializeField] private Camera cam;
        [SerializeField] private RenderBoard boardRenderer;
        [SerializeField] private AudioManager audioManager;

        private int screenWidth;
        private int screenHeight;

        private readonly List<float> cameraXCoordinates = new();
        private readonly List<float> cameraYCoordinates = new();

        public bool PickedUp { get; private set; }
        private static int PickedUpIndex { get; set; }

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

            mousePos.x /= Screen.width;
            mousePos.y /= Screen.height;

            int x = FindIndices(cameraXCoordinates.ToArray(), mousePos.x);
            int y = FindIndices(cameraYCoordinates.ToArray(), mousePos.y);

            int index = x + 8 * y;

            if (x == -1 || y == -1) index = -1;

            if (Input.GetButtonDown("Fire1")) PickupObject(index, mousePos);
            if (Input.GetButtonUp("Fire1")) DropObject(index, mousePos);

            if (PickedUp) MoveObject(cam.ScreenToWorldPoint(Input.mousePosition));
        }

        private int FindIndices(float[] sortedArray, float x) {
            if (x < sortedArray[0] || x > sortedArray[^1]) return -1;

            int left = -1;

            for (int i = 0; i < sortedArray.Length; i++) {
                if (sortedArray[i] >= x) {
                    left = i - 1;
                    break;
                }
            }

            return left;
        }

        private void PickupObject(int index, Vector3 mousePos) {
            PickedUp = true;
            PickedUpIndex = index;

            boardRenderer.RenderPickupColor(mousePos);
            audioManager.PlayPickupSound();
        }

        private void DropObject(int index, Vector3 mousePos) {
            PickedUp = false;

            if (PickedUpIndex == index || index == -1) return;

            if (Board.Square[index] != 0) Destroy(Board.Pieces[index]);

            Board.Pieces[index] = Board.Pieces[PickedUpIndex];
            Board.Pieces[index].transform.position =
                new Vector3(Board.Pieces[index].transform.position.x, Board.Pieces[index].transform.position.y, 0);
            Board.Pieces[index].GetComponent<SpriteRenderer>().sortingOrder = 0;
            Board.Pieces[PickedUpIndex] = null;

            Board.Square[index] = Board.Square[PickedUpIndex];
            Board.Square[PickedUpIndex] = 0;

            PickedUpIndex = -1;
            
            boardRenderer.RenderDropColor(mousePos);
            audioManager.PlayDropSound();
        }

        private void MoveObject(Vector3 mousePos) {
            mousePos.z = 50;
            Board.Pieces[PickedUpIndex].transform.position = mousePos;
            Board.Pieces[PickedUpIndex].GetComponent<SpriteRenderer>().sortingOrder = 100;
        }
    }
}

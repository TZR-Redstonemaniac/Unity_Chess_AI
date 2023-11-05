using System;
using Shapes;
using UnityEngine;

public class ShapesBoardRender : ImmediateModeShapeDrawer  {
    
    [Header("Board Colors")]
    [SerializeField] private Color lightColor;
    [SerializeField] private Color darkColor;
    
    private int screenWidth;
    private int screenHeight;
    
    private void Start() {
        //Get the initial screen dimensions
        screenWidth = Screen.width;
        screenHeight = Screen.height;
    }

    public override void DrawShapes(Camera cam) {
        using (Draw.Command(cam)) {
            Draw.Rectangle(new Vector3(0, 0, 0), Quaternion.identity, screenWidth, screenHeight, new Color(.1f, .1f, .1f, 1f));
        }

        //Define size of the squares
        float regionSize = screenWidth / 18.0f;
        int squareIndex = -1;
                
        for (int yIndex = 1; yIndex <= 8; yIndex++)
        {
            for (int xIndex = 5; xIndex <= 12; xIndex++)
            {
                //Add to the index for light or dark color checking
                squareIndex++;
                        
                //Calculate the region boundaries
                float xRegionStart = xIndex * regionSize / screenWidth;
                float yRegionStart = yIndex * regionSize / screenHeight - .0835f;

                using (Draw.Command(cam)) {
                    Vector3 pos = cam.ViewportToWorldPoint(new Vector3(xRegionStart, yRegionStart, 0));
                    
                    // Check if the index is even or odd
                    if (squareIndex % 2 != 0) Draw.Rectangle(pos, Quaternion.identity, regionSize, regionSize, darkColor); //Dark color for odd regions
                    else Draw.Rectangle(pos, Quaternion.identity, regionSize, regionSize, lightColor); //Light color for even regions
                }
            }

            //Edge case fixer when looping to the next layer
            squareIndex++;
        }
    }
}

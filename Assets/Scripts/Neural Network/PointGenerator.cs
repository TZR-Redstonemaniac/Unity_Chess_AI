using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointGenerator : MonoBehaviour
{
    
    [SerializeField] private GameObject SamplePointPrefab;
    
    private readonly List<float> xCoords = new();
    private readonly List<float> yCoords = new();
    
    // Start is called before the first frame update
    private void Start() {
        for (int i = 0; i <= 250; i++){
            xCoords.Add(Random.Range(-90, 90) / 10f);
            yCoords.Add(Random.Range(-42.5f, 42.5f) / 10f);
        }

        DrawPoints();
    }

    private void DrawPoints() {
        for (int i = 0; i <= 250; i++) {
            GameObject point = Instantiate(SamplePointPrefab, transform);
            Vector3 transformPosition = new Vector3 {
                x = xCoords[i],
                y = yCoords[i]
            };

            point.transform.position = transformPosition;
            
            if (xCoords[i] < -2 && yCoords[i] < 0) point.GetComponent<SpriteRenderer>().color = Color.blue;
            else point.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
}

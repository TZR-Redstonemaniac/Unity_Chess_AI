using UnityEngine;

public class Visualize : MonoBehaviour {
    
    [SerializeField] private Texture2D texture;
    [SerializeField] private NeuralNetwork network;
    
    private void Start() {
        if (texture == null)
        {
            Debug.LogError("Texture is not assigned.");
            return;
        }

        Color[] pixels = texture.GetPixels();
        int width = texture.width; // Get the width of the texture

        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int index = y * width + x; // Calculate the index in the 1D pixel array
                double[] input = {x - 8.5f, y - 4f};
                
                if (network.Classify(input) == 0) {
                    pixels[index] = new Color(100, 0, 0, .5f);
                }
                else {
                    pixels[index] = new Color(0, 0, 100, .5f);
                }
                
            }
        }

        texture.SetPixels(pixels);
        texture.Apply();
    }
}

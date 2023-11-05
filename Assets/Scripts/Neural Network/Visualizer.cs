using System.Threading;
using System.Threading.Tasks;
using Shapes;
using UnityEngine;
using UnityEngine.UI;

public class Visualizer : ImmediateModeShapeDrawer  {

    [SerializeField] private NetworkManager networkManager;

    private int screenWidth;
    private int screenHeight;
    
    // Start is called before the first frame update
    private void Start() {
        screenWidth = Screen.width;
        screenHeight = Screen.height;
    }
    
    void OnPreRenderCallback(Camera cam)
    {
        Debug.Log("Camera callback: Camera name is " + cam.name);

        // Unity calls this for every active Camera.
        // If you're only interested in a particular Camera,
        // check whether the Camera is the one you're interested in
        if (cam != Camera.main) return;
        
        using (Draw.Command(cam)) {
            
            
            Draw.ThicknessSpace = ThicknessSpace.Pixels;
            Draw.Thickness = 1;
            
            for (int x = 0; x < screenWidth; x++)
            {
                for (int y = 0; y < screenHeight; y++)
                {
                    double output = networkManager.network.Classify(x, y);

                    Color color = output < 0.5f ? new Color(100, 0, 0, 100) : new Color(0, 0, 100, 100);

                    Vector3 pos = cam.ViewportToWorldPoint(new Vector3(x, y, 0));
                    
                    Draw.Rectangle(pos, Quaternion.identity, 1, 1, color);
                }
            }
        }
    }
}

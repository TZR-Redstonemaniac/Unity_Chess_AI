using Managers;
using UnityEngine;
using UnityEngine.UI;
// ReSharper disable ConvertIfStatementToSwitchStatement

namespace Renderers {
    [ExecuteInEditMode]
    public class RenderBoard : MonoBehaviour {

        [Header("Shaders")] 
        [SerializeField] private Shader boardShader;

        [Header("Board Colors")] 
        [SerializeField] private Color lightColor;
        [SerializeField] private Color lightPickupColor;
        [SerializeField] private Color lightDropColor;
        
        [SerializeField] private Color darkColor;
        [SerializeField] private Color darkPickupColor;
        [SerializeField] private Color darkDropColor;

        [Header("Background")] 
        [SerializeField] private Image background;
        
        private Material boardMaterial;
        
        private static readonly int DropMouseX = Shader.PropertyToID("_DropMouseX");
        private static readonly int DropMouseY = Shader.PropertyToID("_DropMouseY");
        private static readonly int PickupMouseX = Shader.PropertyToID("_PickupMouseX");
        private static readonly int PickupMouseY = Shader.PropertyToID("_PickupMouseY");

        private static readonly int LightColor = Shader.PropertyToID("_LightColor");
        private static readonly int LightPickupColor = Shader.PropertyToID("_LightPickupColor");
        private static readonly int LightDropColor = Shader.PropertyToID("_LightDropColor");
        
        private static readonly int DarkColor = Shader.PropertyToID("_DarkColor");
        private static readonly int DarkPickupColor = Shader.PropertyToID("_DarkPickupColor");
        private static readonly int DarkDropColor = Shader.PropertyToID("_DarkDropColor");
        
        private static readonly int Pickup = Shader.PropertyToID("_Pickup");
        private static readonly int Drop = Shader.PropertyToID("_Drop");

        private void Awake() {
            boardMaterial = new Material(boardShader);
            background.material = boardMaterial;
        }

        private void Update() {
            boardMaterial.SetColor(LightColor, lightColor);
            boardMaterial.SetColor(LightPickupColor, lightPickupColor);
            boardMaterial.SetColor(LightDropColor, lightDropColor);

            boardMaterial.SetColor(DarkColor, darkColor);
            boardMaterial.SetColor(DarkPickupColor, darkPickupColor);
            boardMaterial.SetColor(DarkDropColor, darkDropColor);

            background.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
        }

        public void RenderPickupColor(Vector3 mousePos) {
            boardMaterial.SetFloat(PickupMouseX, mousePos.x);
            boardMaterial.SetFloat(PickupMouseY, mousePos.y);
            
            boardMaterial.SetInt(Pickup, 1);
        }
        
        public void RenderDropColor(Vector3 mousePos) {
            boardMaterial.SetFloat(DropMouseX, mousePos.x);
            boardMaterial.SetFloat(DropMouseY, mousePos.y);
            
            boardMaterial.SetInt(Drop, 1);
        }
    }
}

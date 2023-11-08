using UnityEngine;
using UnityEngine.UI;

namespace Renderers {
    [ExecuteInEditMode]
    public class RenderBoard : MonoBehaviour {

        [Header("Shaders")] [SerializeField] private Shader boardShader;

        [Header("Board Colors")] [SerializeField]
        private Color lightColor;

        [SerializeField] private Color darkColor;

        [Header("Background")] [SerializeField]
        private Image background;

        private Material boardMaterial;

        private static readonly int DarkColor = Shader.PropertyToID("_DarkColor");
        private static readonly int LightColor = Shader.PropertyToID("_LightColor");

        private void Awake() {
            boardMaterial = new Material(boardShader);
            background.material = boardMaterial;
        }

        private void Update() {
            boardMaterial.SetColor(DarkColor, darkColor);
            boardMaterial.SetColor(LightColor, lightColor);

            background.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
        }
    }
}

Shader "Custom/RenderBoard"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _LightColor("Light Color", Color) = (0, 0, 0, 1)
        _DarkColor("Dark Color", Color) = (0, 0, 0, 1)
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float2 texcoord : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert(const appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = v.texcoord;
                return o;
            }

            int _SquareIndex = -1;
            half4 _DarkColor;
            half4 _LightColor;

            half4 frag(v2f i) : SV_Target
            {
                //Define size of the squares
                const float regionSize = _ScreenParams.x / 18.0;
                
                for (int yIndex = 1; yIndex <= 8; yIndex++)
                {
                    for (int xIndex = 5; xIndex <= 12; xIndex++)
                    {
                        //Add to the index for light or dark color checking
                        _SquareIndex++;
                        
                        //Calculate the region boundaries
                        const float xRegionStart = xIndex * regionSize / _ScreenParams.x;
                        const float xRegionEnd = (xIndex + 1) * regionSize / _ScreenParams.x;
                        const float yRegionStart = yIndex * regionSize / _ScreenParams.y - .0835;
                        const float yRegionEnd = (yIndex + 1) * regionSize / _ScreenParams.y - .0835;

                        // Check if the current pixel is within the specified region
                        if (i.texcoord.x >= xRegionStart && i.texcoord.x < xRegionEnd && i.texcoord.y >= yRegionStart && i.texcoord.y < yRegionEnd) {
                            // Check if the index is even or odd
                            if (_SquareIndex % 2 != 0) return _DarkColor; //Dark color for odd regions
                            
                            return _LightColor; //Light color for even regions
                        }
                    }

                    //Edge case fixer when looping to the next layer
                    _SquareIndex++;
                }

                return half4(.1, .1, .1, 1); //Draw the background
            }
            ENDCG
        }
    }
}

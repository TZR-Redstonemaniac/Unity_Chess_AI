Shader "Custom/RenderBoard"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        
        _LightColor("Light Color", Color) = (0, 0, 0, 1)
        _LightPickupColor("Light Pickup Color", Color) = (0, 0, 0, 1)
        _LightDropColor("Light Drop Color", Color) = (0, 0, 0, 1)
        
        _DarkColor("Dark Color", Color) = (0, 0, 0, 1)
        _DarkPickupColor("Dark Pickup Color", Color) = (0, 0, 0, 1)
        _DarkDropColor("Dark Drop Color", Color) = (0, 0, 0, 1)
        
        _Pickup("Pickup", int) = 0
        _Drop("Drop", int) = 0
        
        _PickupMouseX("Pickup Mouse X", Float) = -1
        _PickupMouseY("Pickup Mouse Y", Float) = -1
        _DropMouseX("Drop Mouse X", Float) = -1
        _DropMouseY("Drop Mouse Y", Float) = -1
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

            float _PickupMouseX = -1;
            float _PickupMouseY = -1;
            float _DropMouseX = -1;
            float _DropMouseY = -1;
            
            int _Pickup = 0;
            int _Drop = 0;
            
            half4 _LightColor;
            half4 _LightPickupColor;
            half4 _LightDropColor;
            
            half4 _DarkColor;
            half4 _DarkPickupColor;
            half4 _DarkDropColor;

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
                        if (i.texcoord.x >= xRegionStart && i.texcoord.x < xRegionEnd && i.texcoord.y >= yRegionStart
                            && i.texcoord.y < yRegionEnd) {
                            if (_PickupMouseX >= xRegionStart && _PickupMouseX < xRegionEnd && _PickupMouseY >= yRegionStart
                                && _PickupMouseY < yRegionEnd && _Pickup > 0) {
                                //Dark color for odd regions
                                //Light color for even regions
                                return _SquareIndex % 2 != 0 ? _DarkPickupColor : _LightPickupColor;
                            }

                            if (_DropMouseX >= xRegionStart && _DropMouseX < xRegionEnd && _DropMouseY >= yRegionStart
                                && _DropMouseY < yRegionEnd && _Drop > 0) {
                                //Dark color for odd regions
                                //Light color for even regions
                                return _SquareIndex % 2 != 0 ? _DarkDropColor : _LightDropColor;
                            }
                            
                            //Dark color for odd regions
                            //Light color for even regions
                            return _SquareIndex % 2 != 0 ? _DarkColor : _LightColor; 
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

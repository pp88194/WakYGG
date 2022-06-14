// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Gradient"
{
    Properties
    {
        [PerRandererData] _MainTex ("", 2D) = "" {}
        _BeginColor("BeginColor", Color) = (1, 1, 1, 1)
        _EndColor("EndColor", Color) = (0, 0, 0, 1)
        _X("X", Float) = 0
        _Y("Y", Float) = 1
    }
    SubShader
    {
        Tags { 
            "Queue" = "Background"
            "IgnoreProjector" = "True"
        }
        LOD 100
        ZWrite On

        Pass {
            CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"

            fixed4 _BeginColor;
            fixed4 _EndColor;
            float _X;
            float _Y;
            struct v2f {
                float4 position: SV_POSITION;
                fixed4 color : COLOR;
            };
            v2f vert(appdata_full v) {
                v2f o;
                o.position = UnityObjectToClipPos(v.vertex);
                o.color = lerp(_BeginColor, _EndColor, v.texcoord.x * _X + v.texcoord.y * _Y);
                return o;
            }
            float4 frag(v2f i) : COLOR{
                return i.color;
            }
            ENDCG
        }
    }
}

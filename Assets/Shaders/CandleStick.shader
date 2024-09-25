Shader "Unlit/CandleStick"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        [HDR] _GlowColor ("Color", Color) = (1,1,1,1)
        _Color ("Color", Color) = (1,1,1,1)
        _Offset ("Offset", Range(-0.5, 0.5)) = 0
        _Multiplier ("Multiplier", Range(1, 50)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 localPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _GlowColor, _Color;
            float _Offset, _Multiplier;

            v2f vert (appdata v)
            {
                v2f o;
                o.localPos = v.vertex.xyz;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //fixed4 col = tex2D(_MainTex, i.uv);

                float height = (i.localPos.y + _Offset) * _Multiplier;
                float4 col = lerp(_Color, _GlowColor, height);
                //return col;
                return col;
            }
            ENDCG
        }
    }
    Fallback "Diffuse"
}

Shader "Unlit/LockedDoor"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _RingTex ("Texture", 2D) = "white" {}
        [HDR] _Color ("Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

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
            };

            sampler2D _MainTex, _RingTex;
            float4 _MainTex_ST, _Color;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 newUV = i.uv - 0.5;
                float d = (1 - smoothstep(-0.1, 0.9, length(newUV))) * .5;
                fixed4 col = tex2D(_MainTex, float2(0.5, 0.5) + (normalize(newUV) * d));

                float2 ringUV = newUV * 6 * (1 - sqrt(frac(_Time.y * 0.4)));
                float4 ring = tex2D(_RingTex, float2(0.5, 0.5) + ringUV);

                col.a = col.r * ring.r;
                return col * _Color;
            }
            ENDCG
        }
    }
}

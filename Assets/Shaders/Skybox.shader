Shader "Unlit/Skybox"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _HighColor ("High Color", Color) = (1,1,1,1)
        _LowColor ("Low Color", Color) = (1,1,1,1)
        _MoonColor ("Moon Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Background" }
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
                float4 worldPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _LowColor, _HighColor, _MoonColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }

            float sdfCircle(float3 p, float3 center, float r)
            {
                p = p - center;
                return length(p) - r;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //fixed4 col = tex2D(_MainTex, i.uv);
                float3 worldPos = normalize(i.worldPos.xyz);
                float horizonAngle = asin(worldPos.y);
                float3 skyColor = lerp(_LowColor, _HighColor, smoothstep(-0.3, 0.6, horizonAngle));

                float3 moonPos = normalize(float3(0.5, 0.4, -0.7));
                float d = length(moonPos - worldPos);
                float3 bloom = pow((0.035 / d) * _MoonColor, 2);

                skyColor += bloom;
                skyColor = lerp(_MoonColor, skyColor, smoothstep(0.09, 0.094, d));
                return float4(skyColor, 1);
            }
            ENDCG
        }
    }
}

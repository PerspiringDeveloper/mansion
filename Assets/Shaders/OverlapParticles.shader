Shader "Unlit/OverlapParticles"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _CloudTex ("Cloud Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)

        [Enum(UnityEngine.Rendering.BlendMode)]
        _SrcFactor ("Src Factor", Float) = 5
        [Enum(UnityEngine.Rendering.BlendMode)]
        _DstFactor ("Dst Factor", Float) = 10
        [Enum(UnityEngine.Rendering.BlendOp)]
        _Opp ("Operation", Float) = 0

        _Bloom ("Bloom", Range(0, 5)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100
        ZWrite Off

        // Blending Equation:
        // finalValue = sourceFactor * sourceValue operation destinationFactor * destinationValue
        // Where operation is Add by default
        // sourceValue is the output of the shader
        // destinationValue is the value already in the render target
        Blend [_SrcFactor] [_DstFactor]
        BlendOp [_Opp]

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

            sampler2D _MainTex, _CloudTex;
            float4 _MainTex_ST, _Color;
            float _Bloom;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 noise = tex2D(_CloudTex, i.uv);
                col.a = col.r * (noise.r + 0.1);
                return col * _Bloom + _Color;
            }
            ENDCG
        }
    }
}

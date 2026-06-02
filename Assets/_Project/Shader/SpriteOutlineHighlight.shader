Shader "Custom/SpriteOutlineHighlight_WebGLSafe"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint Color", Color) = (1,1,1,1)

        _OutlineColor ("Outline Color", Color) = (1,0,0,1)
        _OutlineThickness ("Outline Thickness (px)", Range(1,4)) = 1
        _Highlight ("Highlight", Range(0,1)) = 1
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "IgnoreProjector"="True"
        }

        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                half2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;

            half4 _Color;
            half4 _OutlineColor;
            half _OutlineThickness;
            half _Highlight;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                half4 baseCol = tex2D(_MainTex, i.uv) * _Color;
                half alpha = baseCol.a;

                // Ранний выход — экономит до 70% нагрузки
                if (alpha > 0.001h)
                    return baseCol;

                half2 texel = _MainTex_TexelSize.xy * _OutlineThickness;

                // 8 фиксированных семплов вместо циклов
                half a1 = tex2D(_MainTex, i.uv + texel * half2(1, 0)).a;
                half a2 = tex2D(_MainTex, i.uv + texel * half2(-1, 0)).a;
                half a3 = tex2D(_MainTex, i.uv + texel * half2(0, 1)).a;
                half a4 = tex2D(_MainTex, i.uv + texel * half2(0, -1)).a;

                half a5 = tex2D(_MainTex, i.uv + texel * half2(1, 1)).a;
                half a6 = tex2D(_MainTex, i.uv + texel * half2(-1, 1)).a;
                half a7 = tex2D(_MainTex, i.uv + texel * half2(1, -1)).a;
                half a8 = tex2D(_MainTex, i.uv + texel * half2(-1, -1)).a;

                half neighborAlpha = max(
                    max(max(a1, a2), max(a3, a4)),
                    max(max(a5, a6), max(a7, a8))
                );

                half outline = saturate(neighborAlpha) * _Highlight;

                return half4(_OutlineColor.rgb, outline);
            }

            ENDCG
        }
    }
}
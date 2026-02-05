Shader "Custom/SpriteOutlineHighlight"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint Color", Color) = (1,1,1,1)

        _OutlineColor ("Outline Color", Color) = (1,0,0,1)
        _OutlineThickness ("Outline Thickness (px)", Range(1,8)) = 2
        _Highlight ("Highlight", Range(0,1)) = 0
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
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;

            float4 _Color;
            float4 _OutlineColor;
            float _OutlineThickness;
            float _Highlight;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 baseCol = tex2D(_MainTex, i.uv) * _Color;
                float alpha = baseCol.a;

                // Размер одного пикселя текстуры в UV
                float2 texel = _MainTex_TexelSize.xy * _OutlineThickness;

                // Ищем соседние непрозрачные пиксели
                float neighborAlpha = 0.0;

                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        if (x == 0 && y == 0) continue;
                        neighborAlpha = max(
                            neighborAlpha,
                            tex2D(_MainTex, i.uv + texel * float2(x, y)).a
                        );
                    }
                }

                // Обводка = есть сосед, но текущий пиксель прозрачный
                float outline = saturate(neighborAlpha - alpha) * _Highlight;

                fixed3 finalRGB = lerp(baseCol.rgb, _OutlineColor.rgb, outline);

                return fixed4(finalRGB, max(alpha, outline));
            }

            ENDCG
        }
    }
}

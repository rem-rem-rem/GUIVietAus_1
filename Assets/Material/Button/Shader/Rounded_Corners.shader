Shader "UI/RoundedCorners"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _Radius ("Corner Radius", Range(0, 0.5)) = 0.1
    }

    SubShader
    {
        Tags
        {
            "Queue" = "Overlay"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
        }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            float4 _Color;
            float _Radius;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            // Fragment shader không phụ thuộc texture
            fixed4 frag(v2f i) : SV_Target
            {
                // Tính toán UV để đưa về (-1,1)
                float2 uv = i.uv * 2.0 - 1.0;

                // Tính khoảng cách từ UV đến biên
                float2 dist = abs(uv) - (1.0 - _Radius);

                // Điều chỉnh alpha để bo góc
                float alpha = 1.0 - step(0.0, max(dist.x, dist.y));

                // Kết hợp với màu
                fixed4 col = _Color;
                col.a *= alpha;

                return col;
            }
            ENDCG
        }
    }

    FallBack "UI/Default"
}

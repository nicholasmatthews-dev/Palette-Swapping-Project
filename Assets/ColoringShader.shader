Shader "Unlit/ColoringShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _GradientTex("Gradient", 2D) = "white" {}
        _Color("Tint", Color) = (0, 0, 0, 1)
        _Offset("Offset", float) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" 
               "Queue"="Transparent"
        }

        Blend SrcAlpha OneMinusSrcAlpha

        ZWrite off
        Cull off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;

            sampler2D _GradientTex;
            float4 _GradientTex_ST;

            fixed4 _Color;
            fixed4 _White = fixed4(1, 1, 1, 1);

            float _Offset;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                fixed4 color : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
            };


            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 colA = tex2D(_MainTex, i.uv);
                float2 index = float2(colA[0],colA[0]) + _Offset;
                index = frac(index);
                fixed4 colB = tex2D(_GradientTex, index);
                colB[3] = colA[3];
                return colB;
            }

            ENDCG
        }
    }
}

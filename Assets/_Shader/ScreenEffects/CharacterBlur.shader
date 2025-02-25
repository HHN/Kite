Shader "Custom/CharacterBlur"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
    }
        SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Blend SrcAlpha OneMinusSrcAlpha

        Stencil
        {
            Ref 1
            Comp Less
        }

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
                fixed4 color : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.color = v.color;
                return o;
            }

            sampler2D _MainTex;
            float2 _MainTex_TexelSize;
            float _BlurAmount = 5;

            fixed4 frag(v2f i) : SV_Target
            {

                fixed4 col = tex2D(_MainTex, i.uv);
                col += tex2D(_MainTex, half2(i.uv.x, i.uv.y - 4.0 * _BlurAmount)) * 0.05;
                col += tex2D(_MainTex, half2(i.uv.x, i.uv.y - 3.0 * _BlurAmount)) * 0.09;
                col += tex2D(_MainTex, half2(i.uv.x, i.uv.y - 2.0 * _BlurAmount)) * 0.12;
                col += tex2D(_MainTex, half2(i.uv.x, i.uv.y - _BlurAmount)) * 0.15;
                col += tex2D(_MainTex, half2(i.uv.x, i.uv.y)) * 0.16;
                col += tex2D(_MainTex, half2(i.uv.x, i.uv.y + _BlurAmount)) * 0.15;
                col += tex2D(_MainTex, half2(i.uv.x, i.uv.y + 2.0 * _BlurAmount)) * 0.12;
                col += tex2D(_MainTex, half2(i.uv.x, i.uv.y + 3.0 * _BlurAmount)) * 0.09;
                col += tex2D(_MainTex, half2(i.uv.x, i.uv.y + 4.0 * _BlurAmount)) * 0.05;

                return col;
            }
            ENDCG
        }
    }
}

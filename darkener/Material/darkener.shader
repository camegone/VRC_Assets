Shader "camegone/darkener"
{
    Properties
    {
        [MainColor] _UdonDarkenerColor("Overlay Color", Color) = (1.0, 1.0, 1.0, 1.0)
    }
        SubShader
    {
        Tags {"Queue" = "Transparent+10000"}
        BlendOp Add
        Blend Zero SrcColor
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 _UdonDarkenerColor;

            fixed4 frag(v2f i) : SV_Target
            {
                return _UdonDarkenerColor;
            }
            ENDCG
        }
    }
}

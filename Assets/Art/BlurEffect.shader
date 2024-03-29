Shader "Custom/Blur" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _BlurSize ("Blur Size", Range(0.0, 10.0)) = 1.0
    }
    
    SubShader {
        Tags { "Queue"="Overlay" }
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
 
            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };
 
            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };
 
            sampler2D _MainTex;
            float _BlurSize;
 
            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
 
            fixed4 frag (v2f i) : SV_Target {
                fixed4 col = tex2D(_MainTex, i.uv);
                col += tex2D(_MainTex, i.uv + float2(0, _BlurSize)) * 0.5;
                col += tex2D(_MainTex, i.uv - float2(0, _BlurSize)) * 0.5;
                col += tex2D(_MainTex, i.uv + float2(_BlurSize, 0)) * 0.5;
                col += tex2D(_MainTex, i.uv - float2(_BlurSize, 0)) * 0.5;
                col *= 0.2;
                return col;
            }
            ENDCG
        }
    }
}

Shader "Custom/MainMenu/RoundCornerRect"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Radius ("Radius", Range(0, 0.5)) = 0.25
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 200

        pass{
            CGPROGRAM
        
            #pragma vertex vert
            #pragma fragment frag
            #include "unitycg.cginc"

            sampler2D _MainTex;
            fixed4 _Color;
            fixed _Radius;

            struct v2f
            {
                float4 pos:SV_POSITION;
                float2 srcUV:TEXCOORD0;
                float2 adaptUV:TexCOORD1;
            };

            v2f vert(appdata_base v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.srcUV = v.texcoord;
            
                o.adaptUV = o.srcUV - fixed2(0.5, 0.5);
                return o;
            }
            
            fixed4 frag(v2f i):COLOR
            {
                fixed4 col = fixed4(0, 0, 0, 0);
                
                if(abs(i.adaptUV).x < (0.5 - _Radius) || abs(i.adaptUV).y < (0.5 - _Radius))
                {
                    col = tex2D(_MainTex, i.srcUV);
                }
                else
                {
                    if(length(abs(i.adaptUV) - fixed2(0.5 - _Radius, 0.5 - _Radius)) < _Radius)
                    {
                        col = tex2D(_MainTex, i.srcUV);
                    }
                    else
                    {
                        discard;
                    }
                }
                return col * _Color;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}

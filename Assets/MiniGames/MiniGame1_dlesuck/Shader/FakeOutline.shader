Shader "kkj/0FakeOutline"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _BumpMap("NormalMap", 2D) = "bump" {}

    }
        SubShader
    {
        Tags { "RenderType" = "Opaque" }

        cull back
        CGPROGRAM
        #pragma surface surf Toon//Standard fullforwardshadows

        #pragma target 3.0

        sampler2D _MainTex;
    sampler2D _BumpMap;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_BumpMap;

        };

        fixed4 _Color;


        void surf(Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Alpha = c.a;

            o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
        }

        float4 LightingToon(SurfaceOutput s, float3 lightDir, float3 viewDir, float atten) {
            float ndotl = dot(s.Normal, lightDir) * 0.5 + 0.5;

            if (ndotl > 0.8) {
                ndotl = 1;
            }

            else if (ndotl > 0.6) {
                    ndotl = 0.5;
            }

            else
            {
                ndotl = 0.3;
            }

            float rim = abs(dot(s.Normal, viewDir));
            if (rim > 0.2*0.3+0.3) {
                rim = 1;
            }
            else {
                rim = -1;
            }

            float4 final;

            final.rgb = s.Albedo * ndotl * _LightColor0.rgb * rim;
            final.a = s.Alpha;
            return final;
        }

        ENDCG
    }
        FallBack "Diffuse"
}

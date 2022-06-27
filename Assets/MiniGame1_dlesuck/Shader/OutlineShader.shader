Shader "kkj/OutlineShader"
{
    Properties
    {
       _Color("Color", Color) = (1,1,1,1)
       _MainTex("Albedo (RGB)", 2D) = "white" {}
       _Outline("Outline",Float) = 0.1
       _OutlineColor("Outline Color",Color) = (1,1,1,1)
    }
        SubShader
       {
          Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }

          //외곽선 그리기
           Pass{

            Blend SrcAlpha OneMinusSrcAlpha
            Cull Front
            ZWrite Off

            CGPROGRAM

          #pragma vertex vert
          #pragma fragment frag

          half _Outline;
          half4 _OutlineColor;

          struct vertexInput {
             float4 vertex:POSITION;
          };

          struct vertexOutput {
             float4 pos:SV_POSITION;
          };

          float4 CreatOutline(float4 vertPos, float Outline) {
             float4x4 scaleMat;
             scaleMat[0][0] = 1.01f + Outline;
             scaleMat[0][1] = 0.0f + Outline;
             scaleMat[0][2] = 0.0f;
             scaleMat[0][3] = 0.0f;
             scaleMat[1][0] = 0.0f + Outline;
             scaleMat[1][1] = 1.0f + Outline;
             scaleMat[1][2] = 0.0f;
             scaleMat[1][3] = 0.0f;
             scaleMat[2][0] = 0.0f;
             scaleMat[2][1] = 0.0f;
             scaleMat[2][2] = 1.0f + Outline;
             scaleMat[2][3] = 0.0f;
             scaleMat[3][0] = 0.0f;
             scaleMat[3][1] = 0.0f;
             scaleMat[3][2] = 0.0f;
             scaleMat[3][3] = 1.0f;
             return mul(scaleMat, vertPos)+0.001;
          }

          vertexOutput vert(vertexInput v)
          {
 
              vertexOutput o;
             o.pos = UnityObjectToClipPos(CreatOutline(v.vertex, _Outline));
             return o;
          }

          half4 frag(vertexOutput i) :COLOR
          {
             return _OutlineColor;
          }
             ENDCG
       }

           //정상으로 그리기
           Pass{
              Blend SrcAlpha OneMinusSrcAlpha
              CGPROGRAM
              #pragma vertex vert
              #pragma fragment frag

           half4 _Color;
           sampler2D _MainTex;
           float4 _MainTex_ST;

           struct vertexInput
           {
              float4 vertex: POSITION;
              float4 texcoord: TEXCOORD0;

           };

           struct vertexOutput
           {
           float4 pos:SV_POSITION;
           float4 texcoord:TEXCOORD0;
           };

           vertexOutput vert(vertexInput v) {

              vertexOutput o;
              o.pos = UnityObjectToClipPos(v.vertex);
              o.texcoord.xy = (v.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw);
              return o;
           }

           half4 frag(vertexOutput i) :COLOR
           {
           return tex2D(_MainTex,i.texcoord) * _Color;

           }

        ENDCG


        }

       }
}

/*

Shader "Tutorial/Outline" {

    Properties {

        _Color ("Color", Color) = (1, 1, 1, 1)
        _Glossiness ("Smoothness", Range(0, 1)) = 0.5
        _Metallic ("Metallic", Range(0, 1)) = 0

        _OutlineColor ("Outline Color", Color) = (0, 0, 0, 1)
        _OutlineWidth ("Outline Width", Range(0, 0.1)) = 0.03

    }

    Subshader {

        Tags {
            "RenderType" = "Opaque"
        }

        CGPROGRAM

        #pragma surface surf Standard fullforwardshadows

        Input {
            float4 color : COLOR
        }

        half4 _Color;
        half _Glossiness;
        half _Metallic;

        void surf(Input IN, inout SufaceStandardOutput o) {
            o.Albedo = _Color.rgb * IN.color.rgb;
            o.Smoothness = _Glossiness;
            o.Metallic = _Metallic;
            o.Alpha = _Color.a * IN.color.a;
        }

        ENDCG

        Pass {

            Cull Front

            CGPROGRAM

            #pragma vertex VertexProgram
            #pragma fragment FragmentProgram

            half _OutlineWidth;

            float4 VertexProgram(
                    float4 position : POSITION,
                    float3 normal : NORMAL) : SV_POSITION {

                position.xyz += normal * _OutlineWidth;

                return UnityObjectToClipPos(position);

            }

            half4 _OutlineColor;

            half4 FragmentProgram() : SV_TARGET {
                return _OutlineColor;
            }

            ENDCG

        }

    }

}

float4 VertexProgram(
        float4 position : POSITION,
        float3 normal : NORMAL) : SV_POSITION {

    float4 clipPosition = UnityObjectToClipPos(position);
    float3 clipNormal = mul((float3x3) UNITY_MATRIX_VP, mul((float3x3) UNITY_MATRIX_M, normal));

    clipPosition.xyz += normalize(clipNormal) * _OutlineWidth;

    return clipPosition;

}


float2 offset = normalize(clipNormal.xy) * _OutlineWidth;
clipPosition.xy += offset;
-
float2 offset = normalize(clipNormal.xy) * _OutlineWidth * clipPosition.w;
clipPosition.xy += offset;
-
float2 offset = normalize(clipNormal.xy) / _ScreenParams.xy * _OutlineWidth * clipPosition.w;
float2 offset = normalize(clipNormal.xy) / _ScreenParams.xy * _OutlineWidth * clipPosition.w * 2;

*/
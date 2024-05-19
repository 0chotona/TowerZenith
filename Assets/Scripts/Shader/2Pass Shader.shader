Shader "Custom/2Pass Shader"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}

        _RampTex("Ramp Tex(RGB)", 2D) = "white" {}

        _Color("Color", Color) = (1,1,1,1)

        _BumpMap("Normal Map", 2D) = "bump" {}
        _BumpRate("Normal Rate", Range(0,1)) = 0.5

        _BrightRate("Bright", Range(-1,5)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        
        

        CGPROGRAM
        
        #pragma surface surf MyLight noambient

        sampler2D _MainTex;
        sampler2D _BumpMap;
        sampler2D _RampTex;

        float4 _Color;

        float _BumpRate;

        float _BrightRate;

        int index = 0;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_BumpMap;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb * _BrightRate;

            fixed4 n = tex2D(_BumpMap, IN.uv_BumpMap);
            fixed3 Normal = UnpackNormal(n);
            if(_BumpRate >= 0.01)
                o.Normal = float3(Normal.x * _BumpRate, Normal.y * _BumpRate, Normal.z);

            o.Alpha = c.a;
        }
        float4 LightingMyLight(SurfaceOutput s, float3 lightDir, float atten)
        {
            if(index == 0)
            {
                float ndotl = dot(s.Normal, lightDir) * 0.5 + 0.5;

                if(ndotl > 0.7) ndotl = 1;
                else if (ndotl > 0.4) ndotl = 0.5;
                else ndotl = 0.1;

                float4 final;
                final.rgb = s.Albedo * ndotl * _LightColor0.rgb * 2.0;
                final.a = s.Alpha;

                return final;
            }
            else if(index == 1)
            {
                float ndotl = dot(s.Normal, lightDir) * 0.5 + 0.5;

                ndotl *= 5;
                ndotl = ceil(ndotl);
                ndotl *= 0.2;

                float4 final;
                final.rgb = s.Albedo * ndotl * _LightColor0.rgb * 2.0;
                final.a = s.Alpha;

                return final;
            }
            else
            {
                float ndotl = dot(s.Normal, lightDir) * 0.5 + 0.5;

                float4 ramp = tex2D(_RampTex, float2(ndotl, 0.5));
                ramp.rgb *= s.Albedo * 2.0;

                return ramp;
            }
            
        }
        ENDCG
    }
    FallBack "Diffuse"
}

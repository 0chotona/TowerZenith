Shader "Custom/Invisible"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Transp("Transparency", Range(0,1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue" = "Transparent"}
        zwrite on

        ColorMask 0

        CGPROGRAM
        #pragma surface surf nolight noambient noforwardadd nolightmap novertexlights noshadow

        sampler2D _MainTex;

        struct Input
        {
            float3 color:COLOR;
        };


        void surf (Input IN, inout SurfaceOutput o) {  }

        float4 Lightingnolight(SurfaceOutput s, float3 lightDir, float atten)
        {
            return float4(0, 0, 0, 0);
        }
        ENDCG

        zwrite off
        CGPROGRAM
        #pragma surface surf Lambert alpha:fade
        sampler2D _MainTex;
        struct Input 
        { 
            float2 uv_MainTex; 
        };
        float _Transp;

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb;
            o.Alpha = _Transp;
        }
        ENDCG

    }
    FallBack "Diffuse"
}
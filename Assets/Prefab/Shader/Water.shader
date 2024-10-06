Shader "Custom/WaterShader"
{
    Properties
    {
        _Color ("Water Color", Color) = (0.0, 0.5, 1.0, 0.5)
        _WaveHeight ("Wave Height", Range (0, 1)) = 0.1
        _WaveSpeed ("Wave Speed", Range (0, 1)) = 0.5
        _WaveFrequency ("Wave Frequency", Range (0, 10)) = 1.0
        _SpecularColor ("Specular Color", Color) = (1.0, 1.0, 1.0, 1.0)
        _Shininess ("Shininess", Range (0, 1)) = 0.5
    }
    
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200
        
        CGPROGRAM
        #pragma surface surf Lambert vertex:vert

        sampler2D _MainTex;
        float4 _Color;
        float _WaveHeight;
        float _WaveSpeed;
        float _WaveFrequency;
        float4 _SpecularColor;
        float _Shininess;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldPos;
        };

        void vert (inout appdata_full v)
        {
            // Vertex displacement for wave effect
            float wave = sin(v.vertex.x * _WaveFrequency + _Time.y * _WaveSpeed) * _WaveHeight;
            v.vertex.y += wave;
        }

        void surf (Input IN, inout SurfaceOutput o)
        {
            // Base color for the water
            o.Albedo = _Color.rgb;

            // Specular highlight
            o.Specular = _Shininess;
            o.Gloss = _Shininess;

            // Transparency
            o.Alpha = _Color.a;
        }
        ENDCG
    }

    // Make the shader support transparency
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 200
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct v2f
            {
                float4 pos : SV_POSITION;
                float4 color : COLOR;
            };

            float4 _Color;

            v2f vert (appdata_base v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.color = _Color;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                return i.color;
            }
            ENDCG
        }
    }

    Fallback "Diffuse"
}

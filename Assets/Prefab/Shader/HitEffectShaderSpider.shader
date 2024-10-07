Shader "Custom/HitEffectShaderSpider"
{
    Properties
    {
        _MainColor ("Main Color", Color) = (0, 0, 0, 255) // The main color of the object
        _HitColor ("Hit Color", Color) = (255, 0, 0, 150)   // The color for the hit effect (red)
        _Blend ("Blend Factor", Range(0, 1)) = 0.0      // The blend factor between main color and hit color
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Lambert

        struct Input
        {
            float4 color : COLOR;
        };

        float4 _MainColor;
        float4 _HitColor;
        float _Blend;

        void surf (Input IN, inout SurfaceOutput o)
        {
            // Interpolate between the main color and the hit color based on the blend factor
            o.Albedo = lerp(_MainColor.rgb, _HitColor.rgb, _Blend);
        }
        ENDCG
    }
    Fallback "Diffuse"
}
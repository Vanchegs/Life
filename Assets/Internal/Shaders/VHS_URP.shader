Shader "Custom/VHS_URP"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ScanlineIntensity ("Scanline Intensity", Range(0, 0.5)) = 0.1
        _ScanlineCount ("Scanline Count", Range(100, 1000)) = 500
        _NoiseIntensity ("Noise Intensity", Range(0, 0.3)) = 0.1
        _VignetteIntensity ("Vignette Intensity", Range(0, 1)) = 0.5
        _ColorShift ("Color Shift", Range(0, 0.05)) = 0.02
        _GlitchIntensity ("Glitch Intensity", Range(0, 0.5)) = 0.1
        _GlitchFrequency ("Glitch Frequency", Range(0.1, 5)) = 1
        _VHSDistortion ("VHS Distortion", Range(0, 0.3)) = 0.1
    }
    
    SubShader
    {
        Tags { 
            "RenderType" = "Opaque" 
            "RenderPipeline" = "UniversalPipeline"
        }
        
        Pass
        {
            Name "VHSEffect"
            Tags { "LightMode" = "UniversalForward" }
            
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            
            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };
            
            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };
            
            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            float4 _MainTex_TexelSize;
            
            float _ScanlineIntensity;
            float _ScanlineCount;
            float _NoiseIntensity;
            float _VignetteIntensity;
            float _ColorShift;
            float _GlitchIntensity;
            float _GlitchFrequency;
            float _VHSDistortion;
            
            float random(float2 uv)
            {
                return frac(sin(dot(uv, float2(12.9898, 78.233))) * 43758.5453);
            }
            
            Varyings vert(Attributes input)
            {
                Varyings output;
                output.positionCS = TransformObjectToHClip(input.positionOS.xyz);
                output.uv = input.uv;
                return output;
            }
            
            half4 frag(Varyings input) : SV_Target
            {
                float2 uv = input.uv;
                
                // VHS искажение
                float distortion = sin(uv.y * 50 + _Time.y * 2) * _VHSDistortion;
                uv.x += distortion;
                
                // Глитч
                float glitch = step(0.98, random(float2(floor(_Time.y * _GlitchFrequency), 0)));
                float glitchOffset = random(float2(floor(_Time.y * _GlitchFrequency), 1)) * _GlitchIntensity;
                uv.x += glitch * glitchOffset;
                
                // RGB разложение
                float2 uvR = uv + float2(_ColorShift, 0);
                float2 uvG = uv;
                float2 uvB = uv - float2(_ColorShift, 0);
                
                half4 colorR = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uvR);
                half4 colorG = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uvG);
                half4 colorB = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uvB);
                
                half4 color = half4(colorR.r, colorG.g, colorB.b, 1);
                
                // Шум
                float noise = random(uv + _Time.y * 0.5);
                color.rgb += (noise - 0.5) * _NoiseIntensity;
                
                // Сканирующие линии
                float scanline = sin(uv.y * _ScanlineCount * 3.14159);
                scanline = abs(scanline);
                color.rgb -= scanline * _ScanlineIntensity;
                
                // Виньетка
                float2 vignetteUV = uv - 0.5;
                float vignette = 1 - dot(vignetteUV, vignetteUV) * _VignetteIntensity;
                color.rgb *= vignette;
                
                return color;
            }
            ENDHLSL
        }
    }
}

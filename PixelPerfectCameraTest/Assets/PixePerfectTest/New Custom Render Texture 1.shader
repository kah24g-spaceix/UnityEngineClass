Shader "Sprites/Pixel Snap/URP-2D-Lit-PixelSnap"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" "LightMode"="Universal2D" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

        Pass
        {
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            // URP Core 및 2D 조명 유틸리티 포함
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/LightingUtility.hlsl"

            // 텍스처 및 샘플러 정의
            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            float4 _MainTex_TexelSize;

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                float2 screenPos : TEXCOORD1;
            };

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionCS = TransformObjectToHClip(IN.positionOS);
                OUT.uv = IN.uv;
                OUT.screenPos = ComputeScreenPos(OUT.positionCS);
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                // 픽셀 스냅 적용
                float2 pixelSize = _MainTex_TexelSize.xy;
                float2 snappedUV = floor(IN.uv / pixelSize) * pixelSize;

                // 텍스처 샘플링
                half4 baseColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, snappedUV);

                // 2D 조명 계산
                Light2DInput lightInput;
                lightInput.positionWS = TransformObjectToWorld(IN.positionCS).xyz;
                lightInput.normalWS = float3(0, 0, -1);
                lightInput.color = baseColor;
                half4 lighting = SampleBatched2DLighting(lightInput, IN.screenPos.xy);

                // 최종 색상 계산
                return baseColor * lighting;
            }
            ENDHLSL
        }
    }
}

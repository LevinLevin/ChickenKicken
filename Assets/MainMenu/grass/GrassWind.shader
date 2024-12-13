Shader "Custom/FlagShader"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1, 1, 1, 1)
        _WaveAmplitude ("Wave Amplitude", Range(0, 1)) = 0.1
        _WaveFrequency ("Wave Frequency", Range(0.1, 10)) = 1
        _WaveSpeed ("Wave Speed", Range(0.1, 10)) = 1
        _RandomSeed ("Random Seed", Range(0, 1)) = 0.5
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _Color;
            float _WaveAmplitude;
            float _WaveFrequency;
            float _WaveSpeed;
            float _RandomSeed;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;

                // Calculate the wave offset based on time, frequency, and speed with added random variation
                float waveOffset = sin(_Time.y * _WaveSpeed + v.uv.y * _WaveFrequency + _RandomSeed) * _WaveAmplitude;
                o.vertex.x += waveOffset;

                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 texColor = tex2D(_MainTex, i.uv);
                fixed4 finalColor = texColor * _Color;

                return finalColor;
            }
            ENDCG
        }
    }
}

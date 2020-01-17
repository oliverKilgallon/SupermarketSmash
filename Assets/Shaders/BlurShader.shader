Shader "Unlit/BlurShader"
{
    Properties
    {
		[HideInInspector]
        _MainTex ("Texture", 2D) = "white" {}
		_BlurSize("Blur Size", Range(0, 0.1)) = 0
		[KeywordEnum(Low, Medium, High)] _Samples("Sample amount", Float) = 0
		[Toggle(GAUSS)] _Gauss("Gaussian Blur", Float) = 0
		[PowerSlider(3)] _StandardDeviation ("Standard Deviation (Gauss only)", Range(0.00, 0.3)) = 0.02
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

		//Horizontal pass
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
			#pragma shader_feature GAUSS
			#pragma multi_compile _SAMPLES_LOW _SAMPLES_MEDIUM _SAMPLES_HIGH
			#if _SAMPLES_LOW
				#define SAMPLES 10
			#elif _SAMPLES_MEDIUM
				#define SAMPLES 30
			#elif _SAMPLES_HIGH
				#define SAMPLES 100
			#endif
			#define PI 3.14159265359
			#define EULER 2.71828182846
            #include "UnityCG.cginc"

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

            sampler2D _MainTex;
            float4 _MainTex_ST;
			float _BlurSize;
			float _StandardDeviation;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				#if GAUSS
					if (_StandardDeviation == 0) 
						return tex2D(_MainTex, i.uv);
				#endif
				#if GAUSS
					float sum = 0;
				#else
					float sum = SAMPLES;
				#endif
				float4 col = 0;
				for (float index = 0; index < SAMPLES; index++) 
				{
					float offset = (index / (SAMPLES - 1) - 0.5) * _BlurSize;
					float2 uv = i.uv + float2(0, offset);
					#if !GAUSS
						col += tex2D(_MainTex, uv);
					#else
						float stDevSquared = _StandardDeviation * _StandardDeviation;
						float gauss = (1 / sqrt(2 * PI * stDevSquared)) * pow(EULER, -((offset*offset) / (2 * stDevSquared)));
						sum += gauss;
						col += tex2D(_MainTex, uv) * gauss;
					#endif
				}

				col = col / sum;
                return col;
            }
            ENDCG
        }

		//Vertical pass
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile _SAMPLES_LOW _SAMPLES_MEDIUM _SAMPLES_HIGH
			#if _SAMPLES_LOW
				#define SAMPLES 10
			#elif _SAMPLES_MEDIUM
				#define SAMPLES 30
			#elif _SAMPLES_HIGH
				#define SAMPLES 100
			#endif
			#define PI 3.14159265359
			#define EULER 2.71828182846
			#if GAUSS
				float sum = 0;
			#else
				float sum = SAMPLES;
			#endif
			#include "UnityCG.cginc"

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

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _BlurSize;
			float _StandardDeviation;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				#if GAUSS
					if (_StandardDeviation == 0)
						return tex2D(_MainTex, i.uv);
				#endif
				#if GAUSS
					float sum = 0;
				#else
					float sum = SAMPLES;
				#endif
				float4 col = 0;
				float invAspect = _ScreenParams.y / _ScreenParams.x;
				for (float index = 0; index < SAMPLES; index++)
				{
					float offset = (index / (SAMPLES - 1) - 0.5) * _BlurSize;
					float2 uv = i.uv + float2(0, offset);
					#if !GAUSS
						col += tex2D(_MainTex, uv);
					#else
						float stDevSquared = _StandardDeviation * _StandardDeviation;
						float gauss = (1 / sqrt(2 * PI * stDevSquared)) * pow(EULER, -((offset*offset) / (2 * stDevSquared)));
						sum += gauss;
						col += tex2D(_MainTex, uv) * gauss;
					#endif
				}

				col = col / sum;
				return col;
			}
			ENDCG
		}
    }
}

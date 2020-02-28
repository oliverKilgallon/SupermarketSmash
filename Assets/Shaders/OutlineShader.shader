Shader "Unlit/OutlineShader"
{
	Properties
	{
		[HideInInspector]
		_MainTex("Texture", 2D) = "white" {}
		_BlurAmount("Blur Amount", Range(0, 1)) = 0.0075
		_NormalMult("Normal Outline Multiplier", Range(0, 4)) = 1
		_NormalBias("Normal Outline Bias", Range(1, 4)) = 1
		_DepthMult("Depth Outline Multiplier", Range(0, 4)) = 1
		_DepthBias("Depth Outline Bias", Range(1, 4)) = 1
		_OutlineColor("Outline color", Color) = (0, 0, 0, 1)

	}
		SubShader
		{
			//Tags { "RenderType" = "Opaque" }
			//Outline shader
			Pass
			{
				Cull Off
				ZWrite Off
				ZTest Always

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"
				sampler2D _MainTex;
				sampler2D _CameraDepthNormalsTexture;

				float4 _CameraDepthNormalsTexture_TexelSize;
				float _NormalMult;
				float _NormalBias;
				float _DepthMult;
				float _DepthBias;
				float4 _OutlineColor;

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



				//Vertex shader
				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = v.uv;
					return o;
				}

				void Compare(inout float depthOutline, inout float normalOutline, float baseDepth, float3 baseNormal, float2 uv, float2 offset)
				{
					//Neighbour calc
					float4 neighbourDepthNormal = tex2D(_CameraDepthNormalsTexture, uv + _CameraDepthNormalsTexture_TexelSize.xy * offset);

					float3 neighbourNormal;
					float neighbourDepth;

					DecodeDepthNormal(neighbourDepthNormal, neighbourDepth, neighbourNormal);

					neighbourDepth = neighbourDepth * _ProjectionParams.z;

					float depthDifference = baseDepth - neighbourDepth;
					depthOutline = depthOutline + depthDifference;

					float3 normalDifference = baseNormal - neighbourNormal;
					normalDifference = normalDifference.r + normalDifference.g + normalDifference.b;
					normalOutline = normalOutline + normalDifference;
					
				}

				//Fragment shader
				fixed4 frag(v2f i) : SV_Target
				{
					float4 depthnormal = tex2D(_CameraDepthNormalsTexture, i.uv);

					float3 normal;
					float depth;
					DecodeDepthNormal(depthnormal, depth, normal);

					depth = depth * _ProjectionParams.z;

					float depthDifference = 0;
					float normalDifference = 0;

					//Compare(depthDifference, normalDifference, depth, normal, i.uv, float2(-1, 1));
					//Compare(depthDifference, normalDifference, depth, normal, i.uv, float2(1, 1));
					Compare(depthDifference, normalDifference, depth, normal, i.uv, float2(1, 0));
					Compare(depthDifference, normalDifference, depth, normal, i.uv, float2(0, 1));
					Compare(depthDifference, normalDifference, depth, normal, i.uv, float2(-1, 0));
					Compare(depthDifference, normalDifference, depth, normal, i.uv, float2(0, -1));
					//Compare(depthDifference, normalDifference, depth, normal, i.uv, float2(-1, -1));
					//Compare(depthDifference, normalDifference, depth, normal, i.uv, float2(1, -1));

					depthDifference = depthDifference * _DepthMult;
					depthDifference = saturate(depthDifference);
					depthDifference = pow(depthDifference, _DepthBias);

					normalDifference = normalDifference * _NormalMult;
					normalDifference = saturate(normalDifference);
					normalDifference = pow(normalDifference, _NormalBias);

					//Blur this dependant on the distance from the camera
					float outline = depthDifference + normalDifference;

					//float fwidthOutline = fwidth(outline);


					float4 sourceColor = tex2D(_MainTex, i.uv);
					float4 color = lerp(sourceColor, _OutlineColor, outline);

					return color;
				}
				ENDCG
			}
			/*
			//Horizontal Blur pass
			Pass
			{
				Blend SrcAlpha OneMinusSrcAlpha
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"

				sampler2D _MainTex;
				float _BlurAmount;
				float4 _MainTex_TexelSize;

				struct v2f
				{
					float4 pos : SV_POSITION;
					float2 uv : TEXCOORD0;
				};

				float4 _MainTex_ST;

				v2f vert(appdata_base v)
				{
					v2f o;
					o.pos = UnityObjectToClipPos(v.vertex);
					o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
					#if UNITY_UV_STARTS_AT_TOP
					if (_MainTex_TexelSize.y < 0)
						o.uv.y = 1 - o.uv.y;
					#endif
					return o;
				}

				half4 frag(v2f i) : COLOR
				{
					half4 sum = half4(0, 0, 0, 0);

					sum += tex2D(_MainTex, float2(i.uv.x - _BlurAmount, i.uv.y)) * 	0.06136;
					sum += tex2D(_MainTex, float2(i.uv.x - _BlurAmount, i.uv.y)) * 0.27901;
					sum += tex2D(_MainTex, float2(i.uv.x, i.uv.y)) * 0.44198;
					sum += tex2D(_MainTex, float2(i.uv.x + _BlurAmount, i.uv.y)) * 0.27901;
					sum += tex2D(_MainTex, float2(i.uv.x + _BlurAmount, i.uv.y)) * 	0.06136;

					sum = sum / 5;

					return sum;
				}
				ENDCG
			}

			//Vertical Blur pass
			Pass
			{
				Blend SrcAlpha OneMinusSrcAlpha
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"

				sampler2D _MainTex;
				float _BlurAmount;
				float4 _MainTex_TexelSize;

				struct v2f
				{
					float4 pos : SV_POSITION;
					float2 uv : TEXCOORD0;
				};

				float4 _MainTex_ST;

				v2f vert(appdata_base v)
				{
					v2f o;
					o.pos = UnityObjectToClipPos(v.vertex);
					o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
					#if UNITY_UV_STARTS_AT_TOP
					if (_MainTex_TexelSize.y < 0)
						o.uv.y = 1 - o.uv.y;
					#endif
					return o;
				}

				half4 frag(v2f i) : COLOR
				{
					half4 sum = half4(0, 0, 0, 0);

					sum += tex2D(_MainTex, float2(i.uv.x, i.uv.y + _BlurAmount)) * 0.06136;
					sum += tex2D(_MainTex, float2(i.uv.x, i.uv.y - _BlurAmount)) * 0.27901;
					sum += tex2D(_MainTex, float2(i.uv.x, i.uv.y)) * 0.44198;
					sum += tex2D(_MainTex, float2(i.uv.x, i.uv.y + _BlurAmount)) * 0.27901;
					sum += tex2D(_MainTex, float2(i.uv.x, i.uv.y + _BlurAmount)) * 0.06136;

					sum = sum / 5;

					return sum;
				}
				ENDCG
			}
			*/
		}
}

Shader "Unlit/DepthNormalShader"
{
	Properties
	{
		[HideInInspector]
		_MainTex("Texture", 2D) = "white" {}
	}
		SubShader
		{
			//Tags { "RenderType" = "Opaque" }

			Cull Off
			ZWrite Off
			ZTest Always

			Pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				#include "UnityCG.cginc"
				sampler2D _MainTex;
				sampler2D _CameraDepthNormalsTexture;

				float4x4 _viewToWorld;

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

				//Fragment shader
				fixed4 frag(v2f i) : SV_Target
				{
					float4 depthnormal = tex2D(_CameraDepthNormalsTexture, i.uv);

					float3 normal;
					float depth;
					DecodeDepthNormal(depthnormal, depth, normal);

					depth = depth * _ProjectionParams.z;

					normal = normal = mul((float3x3)_viewToWorld, normal);

					return float4(normal, 1);
				}
				ENDCG
			}
		}
}

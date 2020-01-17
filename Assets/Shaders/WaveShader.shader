Shader "Unlit/WaveShader"
{
    Properties
    {
		[HideInInspector]
        _MainTex ("Texture", 2D) = "white" {}
		[Header(Wave)]
		_WaveDistance("Distance from player", float) = 10
		_WaveTrail("Length of the trail", Range(0, 5)) = 1
		_WaveColor("Color", Color) = (1, 0, 0, 1)
	}
		SubShader
		{
			Tags { "RenderType"="Opaque" }

			Cull Off
			ZWrite Off
			ZTest Always

			Pass
			{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

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

			float _WaveDistance;
			float _WaveTrail;
			float4 _WaveColor;

            sampler2D _MainTex;
			sampler2D _CameraDepthTexture;

			//Vertex shader
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

			//Fragment shader
            fixed4 frag (v2f i) : SV_Target
            {
				float depth = tex2D(_CameraDepthTexture, i.uv).r;
				
				//Linear depth from camera and far clipping plane
				depth = Linear01Depth(depth);

				//Depth as distance from camera in Unity units
				depth = depth * _ProjectionParams.z;

				fixed4 source = tex2D(_MainTex, i.uv);
				if (depth >= _ProjectionParams.z)
					return source;

				//Calc wave
				float waveFront = step(depth, _WaveDistance);
				float waveTrail = smoothstep(_WaveDistance - _WaveTrail, _WaveDistance, depth);
				float wave = waveFront * waveTrail;

				fixed4 col = lerp(source, _WaveColor, wave);
                return col;
            }
            ENDCG
        }
    }
}

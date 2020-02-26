Shader "Custom/WaterRippleShader"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}

		//Color and height values for height-based color mapping
		_ColorMin("Color at min height", Color) = (1, 0, 0, 1)
		_ColorMax("Color at max height", Color) = (1, 0, 0, 1)

		_HeightMin("Height Min", float) = 0
		_HeightMax("Height Max", float) = 1

		//Values related to wave deformation
		_Amplitude ("Amplitude", float) = 1
		_Speed ("Speed", float) = 1
		_Frequency ("Frequency", float) = 1

		[HideInInspector]
		_WaveAmplitude ("WaveAmplitude", float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Lambert vertex:vert

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
		fixed4 _ColorMin, _ColorMax;
		float _HeightMin, _HeightMax, _Amplitude, _Speed, _Frequency;
		float _WaveAmplitude;
		float _OffsetX, _OffsetZ;

        struct Input
        {
            float2 uv_MainTex;
			float3 worldPos;
        };

        UNITY_INSTANCING_BUFFER_START(Props)

        UNITY_INSTANCING_BUFFER_END(Props)

		void vert(inout appdata_full v) 
		{
			half offsetVert = ((v.vertex.x * v.vertex.x) + (v.vertex.z * v.vertex.z)); //+ (v.vertex.y * v.vertex.y));
			half value = _Amplitude * sin(_Time.w * _Speed *_Frequency + offsetVert + (v.vertex.x * _OffsetX) + (v.vertex.z * _OffsetZ) );
			//v.vertex.xyz += v.normal.xyz * value;
			//v.normal.xyz += value;
			v.vertex.y += value * _WaveAmplitude;
			v.normal.y += value * _WaveAmplitude;

		}

        void surf (Input IN, inout SurfaceOutput o)
        {
            half4 c = tex2D (_MainTex, IN.uv_MainTex);
			float h = (_HeightMax - IN.worldPos.y) / (_HeightMax - _HeightMin);
			fixed4 tintColor = lerp(_ColorMax.rgba, _ColorMin.rgba, h);
            o.Albedo = c.rgb * tintColor.rgb;
            o.Alpha = c.a * tintColor.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}

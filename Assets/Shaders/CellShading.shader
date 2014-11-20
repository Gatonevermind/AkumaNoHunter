Shader "Custom/CellShading" 
{
	Properties 
	{
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_NormalMap ("Normal Map", 2D) = "white" {}
		
	}
	SubShader 
	{
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert


		half4 _Color;
		sampler2D _MainTex;
		sampler2D _NormalMap;
		
		half diffuseTerm(half3 a, half3 b)
		{
			return max(0, dot(normalize(a), normalize(b)));
		}
		
		half4 LightingSpecMap(SurfaceOutput o, half3 lightDir, half3 viewDir, half atten)
		{
			half d = diffuseTerm(o.Normal, lightDir);
			
			half3 diffuseColor = _LightColor0 * 0.Albedo * (1-d);
			
			half3 returnColor = diffuseColor * atten * 2;
			
			return half4(retunColor, o.Alpha);
			
		}

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
			
			half3 n = UnpackNormal (tex2D(_NormalMap, IN.uv_MainTex));
			
			0.Normal = n;
			
		}
		ENDCG
	} 
	FallBack "Diffuse"
}

﻿Shader "Hidden/GlitchFX/GlitchFX_Shift"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_GlitchMap("Glitch Map", 2D) = "white"{}
	}
		SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

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

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			sampler2D _GlitchMap;

			float _GlitchAmount;
			float _GlitchRandom;

			float rand(float2 co) 
			{
				return frac(sin(dot(co.xy, float2(12.9898, 78.233))) * 43758.5453);
			}


			fixed4 frag(v2f i) : SV_Target
			{
				fixed3 glitch = (tex2D(_GlitchMap, i.uv)).rgb;

				float r = (rand(float2(glitch.r, _GlitchRandom)));
				float gFlag = max(0.0, ceil(_GlitchAmount-r));

				float2 uvShift = (glitch.gb * 2.0 - 1.0) * r * gFlag;

				fixed4 col = tex2D(_MainTex, frac(i.uv + uvShift));
				return col;
			}
				
			ENDCG
		}
	}
}

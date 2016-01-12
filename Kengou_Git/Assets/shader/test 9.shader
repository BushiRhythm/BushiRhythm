Shader "Custom/Skybox Cubed" {
Properties {
	_Tex ("Cubemap", CUBE) = "white" {}
	_OffSet("OffSet",Vector) = (0,0,0,0)
}

SubShader {
	Tags { "Queue"="Background" "RenderType"="Background" }
	Cull Off ZWrite Off Fog { Mode Off }

	Pass {
		
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#pragma fragmentoption ARB_precision_hint_fastest

		#include "UnityCG.cginc"

		samplerCUBE _Tex;
		half4 _OffSet;
		
		struct appdata_t {
			float4 vertex : POSITION;
			float3 texcoord : TEXCOORD0;
		};

		struct v2f {
			float4 vertex : POSITION;
			float3 texcoord : TEXCOORD0;
		};

		v2f vert (appdata_t v)
		{
			v2f o;
			o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
			o.texcoord = v.texcoord;
			return o;
		}

		fixed4 frag (v2f i) : COLOR
		{
			fixed4 tex = texCUBE (_Tex, i.texcoord + _OffSet.xyz);
			return tex;
		}
		ENDCG 
	}
} 	


SubShader {
	Tags { "Queue"="Background" "RenderType"="Background" }
	Cull Off ZWrite Off Fog { Mode Off }
	Pass {
		SetTexture [_Tex] { combine texture }
	}
}

Fallback Off

}
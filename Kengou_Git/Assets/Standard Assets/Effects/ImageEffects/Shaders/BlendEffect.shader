Shader "Hidden/BlendEffect" {
	Properties { _MainTex ("", any) = "" {}
	_JudgeTex ("", any) = "" {}  }
	CGINCLUDE
	#include "UnityCG.cginc"
	struct v2f {
		float4 pos : SV_POSITION;
		half2 uv : TEXCOORD0;
	};
	sampler2D _MainTex;
	sampler2D _BlendTex;
	sampler2D _JudgeTex;
	float _JudgeValue;
	v2f vert( appdata_img v ) {
		v2f o; 
		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
		o.uv = v.texcoord;
		return o;
	}
	half4 frag(v2f i) : SV_Target {
		float BlendValue = _JudgeValue * tex2D(_JudgeTex, i.uv).r;
		if(BlendValue > 1.0f)
			BlendValue = 1.0f;

		if(BlendValue < .0f)
			BlendValue = .0f;
		


		half4 color = tex2D(_MainTex,  i.uv) * (1.0f - BlendValue) +tex2D(_BlendTex,  i.uv) * BlendValue ;
		return color;
	}
	ENDCG
	SubShader {
		 Pass {
			  ZTest Always Cull Off ZWrite Off

			  CGPROGRAM
			  #pragma vertex vert
			  #pragma fragment frag
			  ENDCG
		  }
	}
	Fallback off
}

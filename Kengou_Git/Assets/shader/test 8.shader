Shader "Hidden/ColorScreen" {
	Properties { _MainTex ("", any) = "" {}}
	CGINCLUDE
	#include "UnityCG.cginc"
	struct v2f {
		float4 pos : SV_POSITION;
		half2 uv : TEXCOORD0;
	};
	sampler2D _MainTex;
	half4 _Color;
	v2f vert( appdata_img v ) {
		v2f o; 
		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
		o.uv = v.texcoord;
		return o;
	}
	fixed4 frag(v2f i) : SV_Target {
		fixed4 color = tex2D(_MainTex,  i.uv) *(1.0 - _Color.a) + _Color * _Color.a;
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

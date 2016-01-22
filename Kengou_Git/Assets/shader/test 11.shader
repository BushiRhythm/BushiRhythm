Shader "Custom/RingAlphaShader" {
	Properties {
	    // properties for water shader
	    _MainTex ("Texture", 2D) = "white" { }
		_JudgeTex ("JudgeTex", 2D) = "white" { }
		_Color ("Color",Color) = (1.0, 1.0,1.0, 1.0)
	    _CenterValue ("Center Value", Range (0.0, 1.0)) = 0.3 // sliders
	    _WidthValue ("Width Value", Range (0.0, 1.0)) = 0.2 // sliders
		_pow("Pow", Range(0.0, 20.0)) = 0.2 // sliders
		_maxAlpha("Max Alpha", Range(0.8, 1.2)) = 1.0 // sliders
	} 
	
	SubShader {
		Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			uniform sampler2D _MainTex;
			uniform sampler2D _JudgeTex;
			uniform float4 _Color;
			uniform float _CenterValue;
			uniform float _WidthValue;
			uniform float _pow;
			uniform float _maxAlpha;

			struct v2f {
			    float4  pos : SV_POSITION;
			    float2  uv : TEXCOORD0;
			};
			
			float4 _MainTex_ST;
			
			v2f vert (appdata_base v)
			{
			    v2f o;
			    o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
			    o.uv = TRANSFORM_TEX (v.texcoord, _MainTex);
			    return o;
			}
			
			fixed4 frag (v2f i) : COLOR
			{
		    	    fixed4 texcol = float4(0.0, 0.0, 0.0, 0.1);
					fixed r = tex2D(_JudgeTex, i.uv).r;
					fixed l = r - _CenterValue;
					fixed absL = abs(l);
					if(absL < _WidthValue) {
		    		texcol = _Color;
					fixed multiple = (_WidthValue - abs(l)) / _WidthValue;
					texcol.a *= pow(multiple, _pow) * _maxAlpha;
		    	    } else {
		    		discard;
		    	    }
		    	
		    	    return texcol;
			}

			ENDCG
		}
	}
}
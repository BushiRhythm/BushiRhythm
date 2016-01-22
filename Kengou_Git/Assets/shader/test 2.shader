Shader "Custom/RingShader" {
	Properties {
	    // properties for water shader
	    _MainTex ("Texture", 2D) = "white" { }
		_JudgeTex ("JudgeTex", 2D) = "white" { }
		_Color ("Color",Color) = (1.0, 1.0,1.0, 1.0)
	    _CenterValue ("Center Value", Range (0.0, 1.0)) = 0.3 // sliders
	    _WidthValue ("Width Value", Range (0.0, 1.0)) = 0.2 // sliders
	} 
	
	SubShader {
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
		    	    fixed4 texcol = float4(0.0, 0.0, 0.0, 1.0);
					fixed r = tex2D(_JudgeTex, i.uv).r;
					if(r < _CenterValue + _WidthValue && r > _CenterValue - _WidthValue) {
		    		texcol = _Color;
		    	    } else {
		    		discard;
		    	    }
		    	
		    	    return texcol;
			}

			ENDCG
		}
	}
}
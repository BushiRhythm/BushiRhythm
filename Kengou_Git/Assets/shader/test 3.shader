Shader "Custom/ColorMesh" {
	Properties {
	    // properties for water shader
	    _MainTex ("Texture", 2D) = "white" { }
		_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
	} 
	
	SubShader {	
	Tags {"Queue"="Transparent+1" "IgnoreProjector"="True" "RenderType"="Transparent"}

	Blend SrcAlpha OneMinusSrcAlpha 
		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

		

			uniform sampler2D _MainTex;
			uniform float _Cutoff;
			
			struct appdata {
			float4 vertex : POSITION;
			float2 texcoord : TEXCOORD0;

			fixed4 color : COLOR;
			};


			struct v2f {
			    float4  pos : SV_POSITION;
			    float2  uv : TEXCOORD0;
				fixed4  color : COLOR;
			};
			
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
			    v2f o;
			    o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
			    o.uv = v.texcoord;
				o.color = v.color;
			    return o;
			}
			
			fixed4 frag (v2f i) : COLOR
			{
		    	    fixed4 texcol = i.color;
					fixed4 tex = tex2D(_MainTex, i.uv);
					if(tex.a < _Cutoff) {
		    		discard;
		    	    }
		    		texcol = tex * texcol;

		    	    return texcol;
			}
			

			ENDCG
		}
	}
}
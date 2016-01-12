Shader "Custom/KabukiShader" {
	Properties {
	    // properties for water shader
	    _MainTex ("Texture", 2D) = "white" { }
		_BackTex ("Back", 2D) = "white" { }
	} 
	
	SubShader {	
		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

		

			uniform sampler2D _MainTex;
			uniform sampler2D _BackTex;
			
			struct appdata {
			float4 vertex : POSITION;
			float2 texcoord : TEXCOORD0;

			fixed4 color : COLOR;
			};


			struct v2f {
			    float4  pos : SV_POSITION;
			    float2  uv : TEXCOORD0;
				fixed4  color : COLOR;
				fixed2  wpos : TEXCOORD1;
			};
			
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
			    v2f o;
			    o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
			    o.uv = v.texcoord;
				o.color = v.color;
				o.wpos = o.pos.xy;
			    return o;
			}
			
			fixed4 frag (v2f i) : COLOR
			{
		    	    fixed4 texcol = i.color;
					fixed4 tex = tex2D(_MainTex, i.uv);
					fixed4 blend = tex2D(_BackTex, i.wpos);
		    		texcol = tex * texcol * blend;

		    	    return texcol;
			}
			

			ENDCG
		}
	}
}
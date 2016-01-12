Shader "Custom/KabukiShaderOutline" {
	Properties {
	    // properties for water shader
	    _MainTex ("Texture", 2D) = "white" { }
		_BackTex ("Back", 2D) = "white" { }
		_Outline ("OutLine", float) = 0.1 
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

			//float4 color : COLOR;
			};


			struct v2f {
			    float4  pos : SV_POSITION;
			    float2  uv : TEXCOORD0;
				//float4  color : COLOR;
				fixed2  wpos : TEXCOORD1;
			};
			
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
			    v2f o;
			    o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
			    o.uv = v.texcoord;
				//o.color = v.color;
				o.wpos = o.pos.xy;
			    return o;
			}
			
			fixed4 frag (v2f i) : COLOR
			{
		    	    //half4 texcol = i.color;
					fixed4 tex = tex2D(_MainTex, i.uv);
					fixed4 blend = tex2D(_BackTex, i.wpos);
		    		tex = tex * blend;

		    	    return tex;
			}
			

			ENDCG

		}

		Cull Front

		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct vf {
			    float4  pos : SV_POSITION;
			};
			
			uniform float _Outline;

						struct appdata {
			float4 vertex : POSITION;
			float3 normal : NORMAL;
			float4 color : COLOR;
			};
			
			vf vert (appdata v)
			{
			    vf o;
				float4 pos;
				float Value = 2.0f - v.color.r;
				pos.xyz = v.vertex.xyz + v.normal * Value * _Outline  * -UNITY_MATRIX_MV[2].w;
				pos.w = v.vertex.w;
			    o.pos = mul (UNITY_MATRIX_MVP, pos);
			    return o;
			}
			
			fixed4 frag (vf i) : COLOR
			{
		    	    return fixed4(0,0,0,1);
			}
			

			ENDCG
		}
	}
}
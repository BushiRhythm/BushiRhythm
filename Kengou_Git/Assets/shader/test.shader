// Unlit alpha-blended shader.
// - no lighting
// - no lightmap support
// - no per-material color

Shader "Unlit/ZEnableTransparent" {
Properties {
	_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
}

SubShader {
	Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
	LOD 200
	
	ZWrite On
	Blend SrcAlpha OneMinusSrcAlpha 

	Pass {
		Lighting Off
		Alphatest GEqual  [_Cutoff]
		SetTexture [_MainTex] { combine texture } 
	}
}
}


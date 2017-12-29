// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

// Simplified Alpha Blended Particle shader. Differences from regular Alpha Blended Particle one:
// - no Tint color
// - no Smooth particle support
// - no AlphaTest
// - no ColorMask

Shader "Custom/Particles/Alpha Blended" {
Properties {
    _MainTex ("Particle Texture", 2D) = "white" {}
    [Enum(Equal,3,NotEqual,6)] _StencilTest ("Stencil Test", int) = 6
}

Category {
    Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" }
    Blend SrcAlpha OneMinusSrcAlpha
    Cull Off Lighting Off ZWrite Off Fog { Color (0,0,0,0) }

    BindChannels {
        Bind "Color", color
        Bind "Vertex", vertex
        Bind "TexCoord", texcoord
    }

    SubShader {
    	Stencil{
			Ref 1
			Comp [_StencilTest]
		}
        Pass {
            SetTexture [_MainTex] {
                combine texture * primary
            }
        }
    }
}
}
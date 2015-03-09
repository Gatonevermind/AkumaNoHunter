Shader "Toon/Lighted Bumped Outline" {
    Properties {
        _Color ("Main Color", Color) = (0.5,0.5,0.5,1)
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        _Outline ("Outline width", Range (.002, 0.03)) = .005
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _Ramp ("Toon Ramp (RGB)", 2D) = "gray" {}
        _BumpMap ("Normalmap", 2D) = "bump" {}
    }
 
    SubShader {
        Tags { "RenderType"="Opaque" }
        UsePass "Toon/Lighted Bumped/FORWARD"
        UsePass "Toon/Basic Outline/OUTLINE"
    }
 
    Fallback "Toon/Lighted Bumped"
}
 
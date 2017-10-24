// Used by cities material

Shader "World Political Map/Unlit Alpha Texture Color" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white"
    }

   	SubShader {
       Tags {
       	"Queue"="Geometry+1"
       }
       Lighting On
       ZWrite Off
       ZTest Always
       Blend SrcAlpha OneMinusSrcAlpha
       Material {
              Emission [_Color]
       }
       Pass {
          SetTexture [_MainTex] {
            Combine Texture * Primary, Texture * Primary
          }
       }
   } 
    
}
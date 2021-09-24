// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'
/**/
Shader "Custom/SpriteGradient" {
    Properties{
        _Tex("Sprite Texture", 2D) = "white" {}
        _Hue("Left Hue", Float) = 0
        _Hue2("Right Hue", Float) = 1
        _Scale("Scale", Float) = 1

            // these six unused properties are required when a shader
            // is used in the UI system, or you get a warning.
            // look to UI-Default.shader to see these.
            _StencilComp("Stencil Comparison", Float) = 8
            _Stencil("Stencil ID", Float) = 0
            _StencilOp("Stencil Operation", Float) = 0
            _StencilWriteMask("Stencil Write Mask", Float) = 255
            _StencilReadMask("Stencil Read Mask", Float) = 255
            _ColorMask("Color Mask", Float) = 15
            // see for example
            // http://answers.unity3d.com/questions/980924/ui-mask-with-shader.html

    }
    SubShader{
            Tags {"RenderType" = "Transparent" "Queue" = "Transparent"  "IgnoreProjector" = "True"}
            LOD 100
            Blend SrcAlpha OneMinusSrcAlpha
            //ZWrite On

            

            Pass {
                CGPROGRAM
                #pragma vertex vert  
                #pragma fragment frag
                #include "UnityCG.cginc"
                #include "../HsvRgb.cginc"

                sampler2D _Tex;
                float _Hue;
                float _Hue2;
                fixed  _Scale;

                struct v2f {
                    float2 uv : TEXCOORD0;
                    float4 pos : SV_POSITION;
                    fixed4 col : COLOR;
                };


                v2f vert(appdata_full v)
                {
                    v2f o;
                    o.pos = UnityObjectToClipPos(v.vertex);
                    float3 hsv = float3(lerp(_Hue, _Hue2, v.texcoord.x), 1, 1);//hsvRgb(hsv)
                    o.col = float4(hsv, 1);//
                    o.uv = v.texcoord;
                    //            o.col = half4( v.vertex.y, 0, 0, 1);
                    return o;
                }


                float4 frag(v2f i) : COLOR{
                    //float4 c = i.col;
                    float4 c = float4(hsvRgb(i.col), 1);
                    float4 ci = tex2D(_Tex, i.uv);
                    //c.a = 1;
                    //ci
                    return c*ci;// float4(i.uv.x, i.uv.y, 0, 0);
                }
                ENDCG
            }
        }
}
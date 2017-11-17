// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:1,cusa:True,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:True,tesm:0,olmd:1,culm:2,bsrc:0,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:True,atwp:True,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:1873,x:34063,y:32825,varname:node_1873,prsc:2|custl-7286-OUT;n:type:ShaderForge.SFN_ScreenPos,id:4527,x:32121,y:32740,varname:node_4527,prsc:2,sctp:2;n:type:ShaderForge.SFN_Slider,id:9326,x:31981,y:33032,ptovrint:False,ptlb:offset,ptin:_offset,varname:node_9326,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.00854701,max:1;n:type:ShaderForge.SFN_Subtract,id:3248,x:32409,y:32933,varname:node_3248,prsc:2|A-4527-U,B-9326-OUT;n:type:ShaderForge.SFN_Subtract,id:4908,x:32409,y:33112,varname:node_4908,prsc:2|A-4527-V,B-9326-OUT;n:type:ShaderForge.SFN_Append,id:8746,x:32625,y:32948,varname:node_8746,prsc:2|A-3248-OUT,B-4527-V;n:type:ShaderForge.SFN_SceneColor,id:8282,x:32849,y:32948,varname:node_8282,prsc:2|UVIN-8746-OUT;n:type:ShaderForge.SFN_Append,id:3427,x:32625,y:33112,varname:node_3427,prsc:2|A-4527-U,B-4908-OUT;n:type:ShaderForge.SFN_SceneColor,id:9411,x:32849,y:33112,varname:node_9411,prsc:2|UVIN-3427-OUT;n:type:ShaderForge.SFN_Add,id:9204,x:33128,y:32833,varname:node_9204,prsc:2|A-6254-RGB,B-3625-RGB,C-8282-RGB,D-9411-RGB;n:type:ShaderForge.SFN_Append,id:9488,x:32625,y:32542,varname:node_9488,prsc:2|A-3656-OUT,B-4527-V;n:type:ShaderForge.SFN_Append,id:6300,x:32625,y:32741,varname:node_6300,prsc:2|A-4527-U,B-6992-OUT;n:type:ShaderForge.SFN_SceneColor,id:6254,x:32849,y:32544,varname:node_6254,prsc:2|UVIN-9488-OUT;n:type:ShaderForge.SFN_Add,id:3656,x:32409,y:32542,varname:node_3656,prsc:2|A-4527-U,B-9326-OUT;n:type:ShaderForge.SFN_Add,id:6992,x:32409,y:32741,varname:node_6992,prsc:2|A-4527-V,B-9326-OUT;n:type:ShaderForge.SFN_SceneColor,id:3625,x:32849,y:32743,varname:node_3625,prsc:2|UVIN-6300-OUT;n:type:ShaderForge.SFN_Divide,id:7286,x:33751,y:32916,varname:node_7286,prsc:2|A-9713-OUT,B-8893-OUT;n:type:ShaderForge.SFN_ValueProperty,id:8893,x:33624,y:33194,ptovrint:False,ptlb:node_8893,ptin:_node_8893,varname:node_8893,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:8;n:type:ShaderForge.SFN_Divide,id:6039,x:32117,y:33412,varname:node_6039,prsc:2|A-9326-OUT,B-870-OUT;n:type:ShaderForge.SFN_Vector1,id:870,x:31920,y:33476,varname:node_870,prsc:2,v1:2;n:type:ShaderForge.SFN_Subtract,id:5810,x:32409,y:33868,varname:node_5810,prsc:2|A-4527-V,B-6039-OUT;n:type:ShaderForge.SFN_Subtract,id:6993,x:32409,y:33689,varname:node_6993,prsc:2|A-4527-U,B-6039-OUT;n:type:ShaderForge.SFN_Add,id:8115,x:32409,y:33298,varname:node_8115,prsc:2|A-4527-U,B-6039-OUT;n:type:ShaderForge.SFN_Add,id:115,x:32409,y:33497,varname:node_115,prsc:2|A-6039-OUT,B-4527-V;n:type:ShaderForge.SFN_Append,id:3979,x:32625,y:33298,varname:node_3979,prsc:2|A-8115-OUT,B-4527-V;n:type:ShaderForge.SFN_Append,id:2187,x:32625,y:33497,varname:node_2187,prsc:2|A-4527-U,B-115-OUT;n:type:ShaderForge.SFN_Append,id:3407,x:32625,y:33689,varname:node_3407,prsc:2|A-6993-OUT,B-4527-V;n:type:ShaderForge.SFN_Append,id:1799,x:32625,y:33868,varname:node_1799,prsc:2|A-4527-U,B-5810-OUT;n:type:ShaderForge.SFN_SceneColor,id:6742,x:32849,y:33868,varname:node_6742,prsc:2|UVIN-1799-OUT;n:type:ShaderForge.SFN_SceneColor,id:1788,x:32849,y:33704,varname:node_1788,prsc:2|UVIN-3407-OUT;n:type:ShaderForge.SFN_SceneColor,id:1506,x:32849,y:33499,varname:node_1506,prsc:2|UVIN-2187-OUT;n:type:ShaderForge.SFN_SceneColor,id:7644,x:32849,y:33300,varname:node_7644,prsc:2|UVIN-3979-OUT;n:type:ShaderForge.SFN_Add,id:8492,x:33123,y:33458,varname:node_8492,prsc:2|A-7644-RGB,B-1506-RGB,C-1788-RGB,D-6742-RGB;n:type:ShaderForge.SFN_Add,id:9713,x:33474,y:32995,varname:node_9713,prsc:2|A-9204-OUT,B-8492-OUT;proporder:9326-8893;pass:END;sub:END;*/

Shader "Shader Forge/NewShader" {
    Properties {
        _offset ("offset", Range(0, 1)) = 0.00854701
        _node_8893 ("node_8893", Float ) = 8
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
        _Stencil ("Stencil ID", Float) = 0
        _StencilReadMask ("Stencil Read Mask", Float) = 255
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilComp ("Stencil Comparison", Float) = 8
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilOpFail ("Stencil Fail Operation", Float) = 0
        _StencilOpZFail ("Stencil Z-Fail Operation", Float) = 0
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "CanUseSpriteAtlas"="True"
            "PreviewType"="Plane"
        }
        GrabPass{ }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            
            Stencil {
                Ref [_Stencil]
                ReadMask [_StencilReadMask]
                WriteMask [_StencilWriteMask]
                Comp [_StencilComp]
                Pass [_StencilOp]
                Fail [_StencilOpFail]
                ZFail [_StencilOpZFail]
            }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #pragma multi_compile _ PIXELSNAP_ON
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _GrabTexture;
            uniform float _offset;
            uniform float _node_8893;
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 projPos : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos( v.vertex );
                #ifdef PIXELSNAP_ON
                    o.pos = UnityPixelSnap(o.pos);
                #endif
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float2 sceneUVs = (i.projPos.xy / i.projPos.w);
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
////// Lighting:
                float node_6039 = (_offset/2.0);
                float3 finalColor = (((tex2D( _GrabTexture, float2((sceneUVs.r+_offset),sceneUVs.g)).rgb+tex2D( _GrabTexture, float2(sceneUVs.r,(sceneUVs.g+_offset))).rgb+tex2D( _GrabTexture, float2((sceneUVs.r-_offset),sceneUVs.g)).rgb+tex2D( _GrabTexture, float2(sceneUVs.r,(sceneUVs.g-_offset))).rgb)+(tex2D( _GrabTexture, float2((sceneUVs.r+node_6039),sceneUVs.g)).rgb+tex2D( _GrabTexture, float2(sceneUVs.r,(node_6039+sceneUVs.g))).rgb+tex2D( _GrabTexture, float2((sceneUVs.r-node_6039),sceneUVs.g)).rgb+tex2D( _GrabTexture, float2(sceneUVs.r,(sceneUVs.g-node_6039))).rgb))/_node_8893);
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #pragma multi_compile _ PIXELSNAP_ON
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos( v.vertex );
                #ifdef PIXELSNAP_ON
                    o.pos = UnityPixelSnap(o.pos);
                #endif
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}

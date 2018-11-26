// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:True,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.1610384,fgcg:0.143058,fgcb:0.1985294,fgca:1,fgde:0.005,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:8586,x:33319,y:32386,varname:node_8586,prsc:2|diff-8195-OUT,emission-8195-OUT,clip-3570-OUT;n:type:ShaderForge.SFN_Tex2d,id:2266,x:30653,y:33247,ptovrint:False,ptlb:mask 02,ptin:_mask02,varname:node_2266,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:058827299af00354090f2ce53a449a6b,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:9387,x:29993,y:32745,ptovrint:False,ptlb:Fire Mask 01,ptin:_FireMask01,varname:_node_2266_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:88ba0a0ec81070e469245f326ee1b2a4,ntxv:0,isnm:False|UVIN-5992-OUT;n:type:ShaderForge.SFN_TexCoord,id:5028,x:28622,y:32768,varname:node_5028,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Add,id:1792,x:30208,y:32828,varname:node_1792,prsc:2|A-9387-R,B-2349-G;n:type:ShaderForge.SFN_Panner,id:2175,x:29368,y:32764,varname:node_2175,prsc:2,spu:0.2,spv:-0.1|UVIN-2517-OUT,DIST-2723-OUT;n:type:ShaderForge.SFN_Tex2d,id:2349,x:29993,y:32932,ptovrint:False,ptlb:Fire Mask 02,ptin:_FireMask02,varname:_node_2266_copy_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:88ba0a0ec81070e469245f326ee1b2a4,ntxv:0,isnm:False|UVIN-7144-OUT;n:type:ShaderForge.SFN_Panner,id:5989,x:29415,y:32955,varname:node_5989,prsc:2,spu:0.01,spv:-0.4|UVIN-2517-OUT,DIST-2723-OUT;n:type:ShaderForge.SFN_Tex2d,id:2376,x:32066,y:32332,ptovrint:False,ptlb:fire,ptin:_fire,varname:_FireMask02,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:88ba0a0ec81070e469245f326ee1b2a4,ntxv:0,isnm:False|UVIN-3772-OUT;n:type:ShaderForge.SFN_Multiply,id:3860,x:30449,y:32828,varname:node_3860,prsc:2|A-1792-OUT,B-710-OUT;n:type:ShaderForge.SFN_Vector1,id:710,x:30257,y:33010,varname:node_710,prsc:2,v1:0.33;n:type:ShaderForge.SFN_Multiply,id:7327,x:30659,y:32828,varname:node_7327,prsc:2|A-3860-OUT,B-7628-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7628,x:30449,y:32993,ptovrint:False,ptlb:DistortionFactor,ptin:_DistortionFactor,varname:node_7628,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2.3;n:type:ShaderForge.SFN_TexCoord,id:794,x:30653,y:33423,varname:node_794,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_OneMinus,id:8891,x:31041,y:33247,varname:node_8891,prsc:2|IN-2807-OUT;n:type:ShaderForge.SFN_Multiply,id:3074,x:31298,y:32861,varname:node_3074,prsc:2|A-7327-OUT,B-8891-OUT;n:type:ShaderForge.SFN_TexCoord,id:6416,x:31064,y:32219,varname:node_6416,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Add,id:4678,x:31619,y:32399,varname:node_4678,prsc:2|A-6416-V,B-3074-OUT;n:type:ShaderForge.SFN_Append,id:3772,x:31847,y:32332,varname:node_3772,prsc:2|A-6416-U,B-4678-OUT;n:type:ShaderForge.SFN_Multiply,id:3570,x:33051,y:32638,varname:node_3570,prsc:2|A-719-OUT,B-6776-A;n:type:ShaderForge.SFN_VertexColor,id:6776,x:32271,y:32949,varname:node_6776,prsc:2;n:type:ShaderForge.SFN_Multiply,id:719,x:32821,y:32565,varname:node_719,prsc:2|A-9357-OUT,B-2736-A;n:type:ShaderForge.SFN_Tex2d,id:2736,x:32507,y:32684,ptovrint:False,ptlb:node_2736,ptin:_node_2736,varname:node_2736,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:058827299af00354090f2ce53a449a6b,ntxv:0,isnm:False;n:type:ShaderForge.SFN_OneMinus,id:9357,x:32484,y:32480,varname:node_9357,prsc:2|IN-2376-B;n:type:ShaderForge.SFN_Color,id:7632,x:31847,y:32017,ptovrint:False,ptlb:colour2,ptin:_colour2,varname:node_7632,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.579716,c3:0.4558824,c4:1;n:type:ShaderForge.SFN_Color,id:2942,x:32116,y:31789,ptovrint:False,ptlb:Colour1,ptin:_Colour1,varname:node_2942,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.7457404,c3:0.1985294,c4:1;n:type:ShaderForge.SFN_Multiply,id:2162,x:32371,y:32201,varname:node_2162,prsc:2|A-4660-OUT,B-2376-G;n:type:ShaderForge.SFN_Multiply,id:9167,x:32336,y:31812,varname:node_9167,prsc:2|A-2942-RGB,B-2376-R;n:type:ShaderForge.SFN_Add,id:8195,x:32552,y:32084,varname:node_8195,prsc:2|A-7704-OUT,B-2162-OUT;n:type:ShaderForge.SFN_Multiply,id:2807,x:30876,y:33247,varname:node_2807,prsc:2|A-2266-G,B-794-V;n:type:ShaderForge.SFN_Time,id:2267,x:28868,y:33108,varname:node_2267,prsc:2;n:type:ShaderForge.SFN_Multiply,id:2723,x:29093,y:33173,varname:node_2723,prsc:2|A-2267-T,B-1982-OUT;n:type:ShaderForge.SFN_Multiply,id:2517,x:29118,y:32764,varname:node_2517,prsc:2|A-5028-UVOUT,B-7090-OUT;n:type:ShaderForge.SFN_Multiply,id:1982,x:28921,y:33242,varname:node_1982,prsc:2|A-7090-OUT,B-8778-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7090,x:28559,y:33007,ptovrint:False,ptlb:Distortion,ptin:_Distortion,varname:node_7090,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.1;n:type:ShaderForge.SFN_ValueProperty,id:8778,x:28559,y:33189,ptovrint:False,ptlb:Speed,ptin:_Speed,varname:node_8778,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.3;n:type:ShaderForge.SFN_Add,id:5992,x:29689,y:32763,varname:node_5992,prsc:2|A-2175-UVOUT,B-4626-OUT;n:type:ShaderForge.SFN_Add,id:7144,x:29689,y:32935,varname:node_7144,prsc:2|A-5989-UVOUT,B-4626-OUT;n:type:ShaderForge.SFN_ValueProperty,id:4626,x:29514,y:32864,ptovrint:False,ptlb:Offset,ptin:_Offset,varname:node_4626,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.2;n:type:ShaderForge.SFN_Multiply,id:4660,x:32037,y:32135,varname:node_4660,prsc:2|A-7632-RGB,B-9846-RGB;n:type:ShaderForge.SFN_VertexColor,id:9846,x:31833,y:32172,varname:node_9846,prsc:2;n:type:ShaderForge.SFN_Multiply,id:7704,x:32385,y:31968,varname:node_7704,prsc:2|A-9167-OUT,B-7862-RGB;n:type:ShaderForge.SFN_VertexColor,id:7862,x:32128,y:31964,varname:node_7862,prsc:2;proporder:9387-2349-2266-2376-7628-2736-7632-2942-7090-8778-4626;pass:END;sub:END;*/

Shader "Unlit/ProcFire" {
    Properties {
        _FireMask01 ("Fire Mask 01", 2D) = "white" {}
        _FireMask02 ("Fire Mask 02", 2D) = "white" {}
        _mask02 ("mask 02", 2D) = "white" {}
        _fire ("fire", 2D) = "white" {}
        _DistortionFactor ("DistortionFactor", Float ) = 2.3
        _node_2736 ("node_2736", 2D) = "white" {}
        _colour2 ("colour2", Color) = (1,0.579716,0.4558824,1)
        _Colour1 ("Colour1", Color) = (1,0.7457404,0.1985294,1)
        _Distortion ("Distortion", Float ) = 0.1
        _Speed ("Speed", Float ) = 0.3
        _Offset ("Offset", Float ) = 0.2
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }
        LOD 100
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _mask02; uniform float4 _mask02_ST;
            uniform sampler2D _FireMask01; uniform float4 _FireMask01_ST;
            uniform sampler2D _FireMask02; uniform float4 _FireMask02_ST;
            uniform sampler2D _fire; uniform float4 _fire_ST;
            uniform float _DistortionFactor;
            uniform sampler2D _node_2736; uniform float4 _node_2736_ST;
            uniform float4 _colour2;
            uniform float4 _Colour1;
            uniform float _Distortion;
            uniform float _Speed;
            uniform float _Offset;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 vertexColor : COLOR;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float4 node_2267 = _Time;
                float node_2723 = (node_2267.g*(_Distortion*_Speed));
                float2 node_2517 = (i.uv0*_Distortion);
                float2 node_5992 = ((node_2517+node_2723*float2(0.2,-0.1))+_Offset);
                float4 _FireMask01_var = tex2D(_FireMask01,TRANSFORM_TEX(node_5992, _FireMask01));
                float2 node_7144 = ((node_2517+node_2723*float2(0.01,-0.4))+_Offset);
                float4 _FireMask02_var = tex2D(_FireMask02,TRANSFORM_TEX(node_7144, _FireMask02));
                float4 _mask02_var = tex2D(_mask02,TRANSFORM_TEX(i.uv0, _mask02));
                float2 node_3772 = float2(i.uv0.r,(i.uv0.g+((((_FireMask01_var.r+_FireMask02_var.g)*0.33)*_DistortionFactor)*(1.0 - (_mask02_var.g*i.uv0.g)))));
                float4 _fire_var = tex2D(_fire,TRANSFORM_TEX(node_3772, _fire));
                float4 _node_2736_var = tex2D(_node_2736,TRANSFORM_TEX(i.uv0, _node_2736));
                clip((((1.0 - _fire_var.b)*_node_2736_var.a)*i.vertexColor.a) - 0.5);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float3 node_8195 = (((_Colour1.rgb*_fire_var.r)*i.vertexColor.rgb)+((_colour2.rgb*i.vertexColor.rgb)*_fire_var.g));
                float3 diffuseColor = node_8195;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float3 emissive = node_8195;
/// Final Color:
                float3 finalColor = diffuse + emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _mask02; uniform float4 _mask02_ST;
            uniform sampler2D _FireMask01; uniform float4 _FireMask01_ST;
            uniform sampler2D _FireMask02; uniform float4 _FireMask02_ST;
            uniform sampler2D _fire; uniform float4 _fire_ST;
            uniform float _DistortionFactor;
            uniform sampler2D _node_2736; uniform float4 _node_2736_ST;
            uniform float4 _colour2;
            uniform float4 _Colour1;
            uniform float _Distortion;
            uniform float _Speed;
            uniform float _Offset;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 vertexColor : COLOR;
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float4 node_2267 = _Time;
                float node_2723 = (node_2267.g*(_Distortion*_Speed));
                float2 node_2517 = (i.uv0*_Distortion);
                float2 node_5992 = ((node_2517+node_2723*float2(0.2,-0.1))+_Offset);
                float4 _FireMask01_var = tex2D(_FireMask01,TRANSFORM_TEX(node_5992, _FireMask01));
                float2 node_7144 = ((node_2517+node_2723*float2(0.01,-0.4))+_Offset);
                float4 _FireMask02_var = tex2D(_FireMask02,TRANSFORM_TEX(node_7144, _FireMask02));
                float4 _mask02_var = tex2D(_mask02,TRANSFORM_TEX(i.uv0, _mask02));
                float2 node_3772 = float2(i.uv0.r,(i.uv0.g+((((_FireMask01_var.r+_FireMask02_var.g)*0.33)*_DistortionFactor)*(1.0 - (_mask02_var.g*i.uv0.g)))));
                float4 _fire_var = tex2D(_fire,TRANSFORM_TEX(node_3772, _fire));
                float4 _node_2736_var = tex2D(_node_2736,TRANSFORM_TEX(i.uv0, _node_2736));
                clip((((1.0 - _fire_var.b)*_node_2736_var.a)*i.vertexColor.a) - 0.5);
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 node_8195 = (((_Colour1.rgb*_fire_var.r)*i.vertexColor.rgb)+((_colour2.rgb*i.vertexColor.rgb)*_fire_var.g));
                float3 diffuseColor = node_8195;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Back
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _mask02; uniform float4 _mask02_ST;
            uniform sampler2D _FireMask01; uniform float4 _FireMask01_ST;
            uniform sampler2D _FireMask02; uniform float4 _FireMask02_ST;
            uniform sampler2D _fire; uniform float4 _fire_ST;
            uniform float _DistortionFactor;
            uniform sampler2D _node_2736; uniform float4 _node_2736_ST;
            uniform float _Distortion;
            uniform float _Speed;
            uniform float _Offset;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 node_2267 = _Time;
                float node_2723 = (node_2267.g*(_Distortion*_Speed));
                float2 node_2517 = (i.uv0*_Distortion);
                float2 node_5992 = ((node_2517+node_2723*float2(0.2,-0.1))+_Offset);
                float4 _FireMask01_var = tex2D(_FireMask01,TRANSFORM_TEX(node_5992, _FireMask01));
                float2 node_7144 = ((node_2517+node_2723*float2(0.01,-0.4))+_Offset);
                float4 _FireMask02_var = tex2D(_FireMask02,TRANSFORM_TEX(node_7144, _FireMask02));
                float4 _mask02_var = tex2D(_mask02,TRANSFORM_TEX(i.uv0, _mask02));
                float2 node_3772 = float2(i.uv0.r,(i.uv0.g+((((_FireMask01_var.r+_FireMask02_var.g)*0.33)*_DistortionFactor)*(1.0 - (_mask02_var.g*i.uv0.g)))));
                float4 _fire_var = tex2D(_fire,TRANSFORM_TEX(node_3772, _fire));
                float4 _node_2736_var = tex2D(_node_2736,TRANSFORM_TEX(i.uv0, _node_2736));
                clip((((1.0 - _fire_var.b)*_node_2736_var.a)*i.vertexColor.a) - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}

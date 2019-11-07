Shader "Custom/LWRPToonShader"
{
    Properties
    {
		  _MainTex("Texture", 2D) = "white" {}
		_Color("Color", Color) = (1, 1, 1, 1)
		[HDR] _AmbientColor("Ambient Color", Color) = (0.4, 0.4, 0.4, 1)
		[HDR] _SpecularColor("Specular Color", Color) = (0.9, 0.9, 0.9, 1)
		[HDR] _Glossiness("Glossiness", Float) = 32
		[HDR] _RimColor("RimColor", Color) = (1,1,1,1)
		[HDR] _RimAmount("Rim Amount", Range(0,1)) = 0.76
			  _RimThreshold("Rim Threshold", Range(0, 1)) = 0.1
			  _Outline("Outline Width", Range(0.0, 2.0)) = 1.1
			  _OutlineColor("Outline Color", Color) = (0,0,0,1)
    }

    SubShader
    {
		
        Tags 
		{ 
			"RenderPipeline" = "LightweightPipeline"
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
		}

		HLSLINCLUDE
		#pragma target 3.0
		ENDHLSL
		
        Pass
        {
            Tags 
			{ 
				"LightMode"="LightweightForward"
				"PassFlags" = "OnlyDirectional"
			}

            Name "OutLine Pass"

		   ZWrite Off
           Cull Back

            HLSLPROGRAM

            // Required to compile gles 2.0 with standard srp library
            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x

            // -------------------------------------
            // Lightweight Pipeline keywords
            #pragma shader_feature _SAMPLE_GI

            // GPU Instancing
            #pragma multi_compile_instancing
            
            #pragma vertex vert
            #pragma fragment frag


            // Lighting include is needed because of GI
            #include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
            #include "Packages/com.unity.render-pipelines.lightweight/Shaders/UnlitInput.hlsl"

           	
            struct VertexInput
            {
                half4 vertex : POSITION;
				half3 normal : NORMAL;
				half2 uv : TEXCOORD0;
				half4 color : COLOR;
				
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct VertexOutput
            {
                half4 position : POSITION;
				half2 uv : TEXCOORD1;
				half3 viewDir :TEXCOORD2;
				half3 worldNormal : NORMAL;
				half3 color : COLOR;
				
                UNITY_VERTEX_INPUT_INSTANCE_ID
                UNITY_VERTEX_OUTPUT_STEREO
            };

			CBUFFER_START(UnityPerMaterial)
				half  _Outline;
				half4 _OutlineColor;
			CBUFFER_END
			
            VertexOutput vert (VertexInput v)
            {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_TRANSFER_INSTANCE_ID(v, o);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				
				v.vertex.xyz += _Outline * normalize(v.vertex.xyz);

				VertexPositionInputs vInput = GetVertexPositionInputs(v.vertex.xyz);
				o.position = vInput.positionCS;
				
                return o;
            }

            half4 frag (VertexOutput IN ) : COLOR
            {
                UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(IN);
				
		       
                return _OutlineColor;
            }
            ENDHLSL
        }

		
        Pass
        {
			Tags
			{
				"RenderPipeline" = "LightweightPipeline"
				"RenderType" = "Opaque"
				"Queue" = "Geometry"
			}
			
            Name "Toon Pass"

			Cull Back
			ZWrite On
			ZTest LEqual

            HLSLPROGRAM
            #define ASE_SRP_VERSION 60901

            // Required to compile gles 2.0 with standard srp library
            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x
            
            //--------------------------------------
            // GPU Instancing
            #pragma multi_compile_instancing

            #pragma vertex ToonPassVertex
            #pragma fragment ToonPassFragment


            #include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"

            
			
			struct VertexInput
			{
				half4 vertex : POSITION;
				half3 normal : NORMAL;
				half2 uv : TEXCOORD0;
				half4 color : COLOR;

				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				half4 position : POSITION;
				half2 uv : TEXCOORD1;
				half3 viewDir :TEXCOORD2;
				half3 worldNormal : NORMAL;
				half4 color : COLOR;

				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};


			TEXTURE2D(_MainTex);
			SAMPLER(sampler_MainTex);
			half4 _MainTex_ST;

			CBUFFER_START(UnityPerMaterial)
				half4 _AmbientColor;
				half  _SpecularColor;
				half4  _RimColor;
				half  _RimAmount;
				half  _RimThreshold;
				half  _Outline;
				half4 _OutlineColor;
				half _Glossiness;
				half4 _Color;
			CBUFFER_END
			
            VertexOutput ToonPassVertex(VertexInput v )
            {
                VertexOutput o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_TRANSFER_INSTANCE_ID(v, o);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				 
				VertexPositionInputs vInput = GetVertexPositionInputs(v.vertex.xyz);
			
				o.position = vInput.positionCS;
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				VertexNormalInputs nInput = GetVertexNormalInputs(v.normal);
				o.worldNormal = nInput.normalWS;
						
				half3 vsDir =  mul((half3x3)unity_CameraToWorld, vInput.positionVS.xyz);

				o.viewDir = vsDir;               
				o.color = v.color;

                return o;
            }

            half4 ToonPassFragment(VertexOutput i ) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(i);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);


				Light mainLight = GetMainLight();

				half3 normal = normalize(i.worldNormal);
				half NdotL = dot(mainLight.direction, normal);
				
				half shadow = mainLight.shadowAttenuation;

				half lightIntensity = smoothstep(0, 0.001, NdotL * shadow);
				half4 lightColor = half4(mainLight.color, 1);
				half4 light = lightIntensity * lightColor;

				half3 viewDir = normalize(i.viewDir);
				half3 halfVec = normalize(mainLight.direction + viewDir);

				half NdotH = dot(normal, halfVec);

				half specIntensity = pow(abs(NdotH * lightIntensity), _Glossiness * _Glossiness);
				half specSmooth = smoothstep(0.005, 0.01, specIntensity);
				half4 specular = specSmooth * _SpecularColor;

				half4 rimDot = 1 - dot(viewDir, normal);
				half4 rimIntensity = rimDot * pow(abs(NdotL), _RimThreshold);
				rimIntensity = smoothstep(_RimAmount - 0.01, _RimAmount + 0.01, rimIntensity);
				half4 rim = rimIntensity * _RimColor;

		
				half4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex ,i.uv.xy);
			
                return _Color * col * (_AmbientColor + light + specular + rim);
            }

            ENDHLSL
        }

		
        Pass
        {
			
            Name "DepthOnly"
            Tags { "LightMode"="DepthOnly" }

            ZWrite On
			ZTest LEqual
			ColorMask 0

            HLSLPROGRAM

            // Required to compile gles 2.0 with standard srp library
            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x
            #pragma target 2.0

            //--------------------------------------
            // GPU Instancing
            #pragma multi_compile_instancing

            #pragma vertex vert
            #pragma fragment frag


            #include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
	
			struct GraphVertexInput
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

            struct VertexOutput
            {
                float4 clipPos : SV_POSITION;
				
                UNITY_VERTEX_INPUT_INSTANCE_ID
                UNITY_VERTEX_OUTPUT_STEREO
            };

			
			VertexOutput vert( GraphVertexInput v  )
			{
					VertexOutput o = (VertexOutput)0;
					UNITY_SETUP_INSTANCE_ID(v);
					UNITY_TRANSFER_INSTANCE_ID(v, o);
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
								
					v.normal =  v.normal ;
					o.clipPos = TransformObjectToHClip(v.vertex.xyz);

					return o;
			}

            half4 frag( VertexOutput IN  ) : SV_TARGET
            {
                UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(IN);
				

				float Alpha = 1;
				float AlphaClipThreshold = AlphaClipThreshold;

			#if _AlphaClip
        		clip(Alpha - AlphaClipThreshold);
			#endif
                return 0;
			#ifdef LOD_FADE_CROSSFADE
				LODDitheringTransition (IN.clipPos.xyz, unity_LODFade.x);
			#endif
            }
            ENDHLSL
        }
		
    }
    Fallback "Hidden/InternalErrorShader"	
}

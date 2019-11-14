Shader "Custom/LWRPToonShader"
{
    Properties
    {
		  _MainTex("Texture", 2D) = "white" {}
		[HDR]  _Color("Color", Color) = (1, 1, 1, 1)
		[HDR] _AmbientColor("Ambient Color", Color) = (0.4, 0.4, 0.4, 1)
		[HDR] _SpecularColor("Specular Color", Color) = (0.9, 0.9, 0.9, 1)		      
		[HDR] _RimColor("RimColor", Color) = (1,1,1,1)
			  _Glossiness("Glossiness", Float) = 32
		 	  _RimAmount("Rim Amount", Range(0,1)) = 0.76
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

		#pragma multi_compile _ _MAIN_LIGHT_SHADOWS
		#pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
		#pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
		#pragma multi_compile _ _ADDITIONAL_LIGHT_SHADOWS
		#pragma multi_compile _ _SHADOWS_SOFT
		#pragma multi_compile _ _MIXED_LIGHTING_SUBTRACTIVE
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
           
			#include "OutlinePass.hlsl"
           
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

            
			#include "ToonPass.hlsl"	
		

            ENDHLSL
        }

			Pass
		{
			Name "ShadowCaster"
			Tags{"LightMode" = "ShadowCaster"}

			ZWrite On
			ZTest LEqual
			Cull[_Cull]

			HLSLPROGRAM
			// Required to compile gles 2.0 with standard srp library
			#pragma prefer_hlslcc gles
			#pragma exclude_renderers d3d11_9x

			// -------------------------------------
			// Material Keywords
			#pragma shader_feature _ALPHATEST_ON
			#pragma shader_feature _GLOSSINESS_FROM_BASE_ALPHA

			//--------------------------------------
			// GPU Instancing
			#pragma multi_compile_instancing

			#pragma vertex ShadowPassVertex
			#pragma fragment ShadowPassFragment

			#include "Packages/com.unity.render-pipelines.lightweight/Shaders/SimpleLitInput.hlsl"
			#include "Packages/com.unity.render-pipelines.lightweight/Shaders/ShadowCasterPass.hlsl"
			ENDHLSL
		}

		Pass
		{
			Name "DepthOnly"
			Tags{"LightMode" = "DepthOnly"}

			ZWrite On
			ColorMask 0
			Cull[_Cull]

			HLSLPROGRAM
			// Required to compile gles 2.0 with standard srp library
			#pragma prefer_hlslcc gles
			#pragma exclude_renderers d3d11_9x
			
			#pragma vertex DepthOnlyVertex
			#pragma fragment DepthOnlyFragment

			// -------------------------------------
			// Material Keywords
			#pragma shader_feature _ALPHATEST_ON
			#pragma shader_feature _GLOSSINESS_FROM_BASE_ALPHA

			//--------------------------------------
			// GPU Instancing
			#pragma multi_compile_instancing

			#include "Packages/com.unity.render-pipelines.lightweight/Shaders/SimpleLitInput.hlsl"
			#include "Packages/com.unity.render-pipelines.lightweight/Shaders/DepthOnlyPass.hlsl"
			ENDHLSL
		}
    }
    Fallback "Hidden/InternalErrorShader"	
}

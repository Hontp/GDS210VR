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
		
    }
    Fallback "Hidden/InternalErrorShader"	
}

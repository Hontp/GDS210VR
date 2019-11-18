﻿Shader "Custom/LWRPToonV3"
{
	Properties
	{
		// Specular vs Metallic workflow
		[HideInInspector] _WorkflowMode("WorkflowMode", Float) = 1.0

		[MainColor][HDR] _BaseColor("Color", Color) = (0.5,0.5,0.5,1)
		[MainTexture] _BaseMap("Albedo", 2D) = "white" {}

		_Cutoff("Alpha Cutoff", Range(0.0, 1.0)) = 0.5

		_Smoothness("Smoothness", Range(0.0, 1.0)) = 0.5
		_GlossMapScale("Smoothness Scale", Range(0.0, 1.0)) = 1.0
		_SmoothnessTextureChannel("Smoothness texture channel", Float) = 0

		[Gamma] _Metallic("Metallic", Range(0.0, 1.0)) = 0.0
		_MetallicGlossMap("Metallic", 2D) = "white" {}

		[HDR]_SpecColor("Specular", Color) = (0.2, 0.2, 0.2)
		_SpecGlossMap("Specular", 2D) = "white" {}

		[ToggleOff] _SpecularHighlights("Specular Highlights", Float) = 1.0
		[ToggleOff] _EnvironmentReflections("Environment Reflections", Float) = 1.0

		_BumpScale("Scale", Float) = 1.0
		_BumpMap("Normal Map", 2D) = "bump" {}

		_OcclusionStrength("Strength", Range(0.0, 1.0)) = 1.0
		_OcclusionMap("Occlusion", 2D) = "white" {}

		[HDR]_EmissionColor("Color", Color) = (0.4, 0.4, 0.4, 1)
		_EmissionMap("Emission", 2D) = "white" {}
		
		[HDR] _RimColor("RimColor", Color) = (1,1,1,1)
		_RimAmount("Rim Amount", Range(0,1)) = 0.76
		_RimThreshold("Rim Threshold", Range(0, 1)) = 0.1
		
		// Blending state
		[HideInInspector] _Surface("__surface", Float) = 0.0
		[HideInInspector] _Blend("__blend", Float) = 0.0
		[HideInInspector] _AlphaClip("__clip", Float) = 0.0
		[HideInInspector] _SrcBlend("__src", Float) = 1.0
		[HideInInspector] _DstBlend("__dst", Float) = 0.0
		[HideInInspector] _ZWrite("__zw", Float) = 1.0
		[HideInInspector] _Cull("__cull", Float) = 2.0

		_ReceiveShadows("Receive Shadows", Float) = 1.0

			// Editmode props
			[HideInInspector] _QueueOffset("Queue offset", Float) = 0.0
	}

		SubShader
		{
			
			Tags
			{
				"RenderType" = "Opaque" 
				"RenderPipeline" = "LightweightPipeline" 
				"IgnoreProjector" = "True"
			}

			HLSLINCLUDE
			// Required to compile gles 2.0 with standard SRP library
			// All shaders must be compiled with HLSLcc and currently only gles is not using HLSLcc by default
			#pragma prefer_hlslcc gles
			#pragma exclude_renderers d3d11_9x
			#pragma target 2.0

			// -------------------------------------
			// Material Keywords
			// unused shader_feature variants are stripped from build automatically
			#pragma shader_feature _NORMALMAP
			#pragma shader_feature _ALPHATEST_ON
			#pragma shader_feature _ALPHAPREMULTIPLY_ON
			#pragma shader_feature _EMISSION
			#pragma shader_feature _METALLICSPECGLOSSMAP
			#pragma shader_feature _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
			#pragma shader_feature _OCCLUSIONMAP

			#pragma shader_feature _SPECULARHIGHLIGHTS_OFF
			#pragma shader_feature _GLOSSYREFLECTIONS_OFF
			#pragma shader_feature _SPECULAR_SETUP
			#pragma shader_feature _RECEIVE_SHADOWS_OFF


			#pragma multi_compile _ _MAIN_LIGHT_SHADOWS
			#pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
			#pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
			#pragma multi_compile _ _ADDITIONAL_LIGHT_SHADOWS
			#pragma multi_compile _ _SHADOWS_SOFT
			#pragma multi_compile _ _MIXED_LIGHTING_SUBTRACTIVE

			// -------------------------------------
			// Unity defined keywords
			#pragma multi_compile _ DIRLIGHTMAP_COMBINED
			#pragma multi_compile _ LIGHTMAP_ON
			#pragma multi_compile_fog
			//--------------------------------------
			// GPU Instancing
			#pragma multi_compile_instancing
			ENDHLSL

			LOD 300

			Pass
			{
				Name "Toon Pass"
			
				Tags
					{
						"LightMode" = "LightweightForward"
						"RenderType" ="Opaque"
						"Queue" ="Geometry"
					}

				Blend[_SrcBlend][_DstBlend]
				ZWrite[_ZWrite]
				Cull[_Cull]

				HLSLPROGRAM
				
				#pragma vertex ToonPassVertex
				#pragma fragment ToonPassFragment

				#include "ToonPass.hlsl"
				
				ENDHLSL
			}

			Pass
			{
			  Name "Meta"
			  
			  Tags
				{
					"LightMode" = "Meta"
				}

			  Cull Off

			  HLSLPROGRAM
				// Required to compile gles 2.0 with standard srp library
				#pragma prefer_hlslcc gles
				#pragma exclude_renderers d3d11_9x

				#pragma vertex LightweightVertexMeta
				#pragma fragment LightweightFragmentMeta

				#pragma shader_feature _SPECGLOSSMAP

				#include "ToonInput.hlsl"
				#include "Packages/com.unity.render-pipelines.lightweight/Shaders/LitMetaPass.hlsl"

				ENDHLSL
			}

			UsePass "Lightweight Render Pipeline/Lit/ShadowCaster"
			UsePass "Lightweight Render Pipeline/Lit/DepthOnly"
		}

			FallBack "Hidden/InternalErrorShader"
}
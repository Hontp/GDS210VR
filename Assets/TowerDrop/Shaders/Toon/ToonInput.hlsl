#ifndef TOON_LIT_INPUT_INCLUDE
#define TOON_LIT_INPUT_INCLUDE

#include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/Core.hlsl"
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/CommonMaterial.hlsl"
#include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/SurfaceInput.hlsl"


UNITY_INSTANCING_BUFFER_START(UnityPerMaterial)
UNITY_DEFINE_INSTANCED_PROP(half4, _BaseMap_ST)
UNITY_DEFINE_INSTANCED_PROP(half4, _RimColor)
UNITY_DEFINE_INSTANCED_PROP(half4, _BaseColor)
UNITY_DEFINE_INSTANCED_PROP(half4, _EmissionColor)
UNITY_DEFINE_INSTANCED_PROP(half4, _SpecColor)
UNITY_DEFINE_INSTANCED_PROP(half, _Cutoff)
UNITY_DEFINE_INSTANCED_PROP(half, _Smoothness)
UNITY_DEFINE_INSTANCED_PROP(half, _Metallic)
UNITY_DEFINE_INSTANCED_PROP(half, _BumpScale)
UNITY_DEFINE_INSTANCED_PROP(half, _OcclusionStrength)
UNITY_DEFINE_INSTANCED_PROP(half, _RimAmount)
UNITY_DEFINE_INSTANCED_PROP(half, _RimThreshold)
UNITY_DEFINE_INSTANCED_PROP(half, _Glossiness)
UNITY_INSTANCING_BUFFER_END(UnityPerMaterial)

TEXTURE2D(_OcclusionMap);       
SAMPLER(sampler_OcclusionMap);

TEXTURE2D(_MetallicGlossMap);   
SAMPLER(sampler_MetallicGlossMap);

TEXTURE2D(_SpecGlossMap);       
SAMPLER(sampler_SpecGlossMap);

#ifdef _SPECULAR_SETUP
#define SAMPLE_METALLICSPECULAR(uv) SAMPLE_TEXTURE2D(_SpecGlossMap, sampler_SpecGlossMap, uv)
#else
#define SAMPLE_METALLICSPECULAR(uv) SAMPLE_TEXTURE2D(_MetallicGlossMap, sampler_MetallicGlossMap, uv)
#endif

half4 SampleMetallicSpecGloss(float2 uv, half albedoAlpha)
{
	half4 specGloss;

	half metallicProp = UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial, _Metallic);
	half smoothnessProp = UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial, _Smoothness);

#ifdef _METALLICSPECGLOSSMAP
	specGloss = SAMPLE_METALLICSPECULAR(uv);
#ifdef _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
	specGloss.a = albedoAlpha * _Smoothness;
#else
	specGloss.a *= smoothnessProp;
#endif
#else
#if _SPECULAR_SETUP
	specGloss.rgb = _SpecColor.rgb;
#else
	specGloss.rgb = metallicProp.rrr;
#endif

#ifdef _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
	specGloss.a = albedoAlpha * _Smoothness;
#else
	specGloss.a = smoothnessProp;
#endif
#endif

	return specGloss;
}

half SampleOcclusion(float2 uv)
{
#ifdef _OCCLUSIONMAP
#if defined(SHADER_API_GLES)
	return SAMPLE_TEXTURE2D(_OcclusionMap, sampler_OcclusionMap, uv).g;
#else
	half occ = SAMPLE_TEXTURE2D(_OcclusionMap, sampler_OcclusionMap, uv).g;
	return LerpWhiteTo(occ, _OcclusionStrength);
#endif
#else
	return 1.0;
#endif
}

inline void InitializeStandardLitSurfaceData(float2 uv, out SurfaceData outSurfaceData)
{
	half4 baseColorProp = UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial, _BaseColor);
	half4 emissionColorProp = UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial, _EmissionColor);
	half cutoffProp = UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial, _Cutoff);
	half bumpscaleProp = UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial, _BumpScale);


	half4 albedoAlpha = SampleAlbedoAlpha(uv, TEXTURE2D_ARGS(_BaseMap, sampler_BaseMap));
	outSurfaceData.alpha = Alpha(albedoAlpha.a, baseColorProp, cutoffProp);

	half4 specGloss = SampleMetallicSpecGloss(uv, albedoAlpha.a);
	outSurfaceData.albedo = albedoAlpha.rgb * baseColorProp.rgb;

#if _SPECULAR_SETUP
	outSurfaceData.metallic = 1.0h;
	outSurfaceData.specular = specGloss.rgb;
#else
	outSurfaceData.metallic = specGloss.r;
	outSurfaceData.specular = half3(0.0h, 0.0h, 0.0h);
#endif

	outSurfaceData.smoothness = specGloss.a;
	outSurfaceData.normalTS = SampleNormal(uv, TEXTURE2D_ARGS(_BumpMap, sampler_BumpMap), bumpscaleProp);
	outSurfaceData.occlusion = SampleOcclusion(uv);
	outSurfaceData.emission = SampleEmission(uv, emissionColorProp.rgb, TEXTURE2D_ARGS(_EmissionMap, sampler_EmissionMap));
}

#endif


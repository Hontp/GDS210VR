#ifndef TOON_PASS_INCLUDE
#define TOON_PASS_INCLUDE

#include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/Core.hlsl"
#include "Packages/com.unity.render-pipelines.lightweight/ShaderLibrary/Lighting.hlsl"
#include "ToonInput.hlsl"

struct Attributes
{
	half4 positionOS   : POSITION;
	half3 normalOS     : NORMAL;
	half4 tangentOS    : TANGENT;
	half2 uv           : TEXCOORD0;
	half2 uvLM        : TEXCOORD1;
	half4 color		   : COLOR;


	UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct Varyings
{
	half2 uv                       : TEXCOORD0;
	half2 uvLM                     : TEXCOORD1;
	half4 positionWSAndFogFactor   : TEXCOORD2; 
	DECLARE_LIGHTMAP_OR_SH(lightmapUV, vertexSH, 3);


#if _NORMALMAP
	half4 tangentWS                 : TEXCOORD4;
	half4 bitangentWS               : TEXCOORD5;
	half4 normalWS                : TEXCOORD6;
#else
	half3 normalWS                : TEXCOORD7;
#endif

	half3 viewDirWS					: TEXCOORD8;

#ifdef _MAIN_LIGHT_SHADOWS
	half4 shadowCoord              : TEXCOORD9;
#endif
	half4 positionCS               : SV_POSITION;
	half4 color						: COLOR;

	UNITY_VERTEX_INPUT_INSTANCE_ID
		UNITY_VERTEX_OUTPUT_STEREO
};

void InitializeInputData(Varyings input, half3 normalTS, out InputData inputData)
{
	inputData = (InputData)0;

#ifdef _ADDITIONAL_LIGHTS
	inputData.positionWS = input.positionCS;
#endif

#ifdef _NORMALMAP
	half3 viewDirWS = half3(input.normalWS.w, input.tangentWS.w, input.bitangentWS.w);
	inputData.normalWS = TransformTangentToWorld(normalTS,
		half3x3(input.tangentWS.xyz, input.bitangentWS.xyz, input.normalWS.xyz));
#else
	half3 viewDirWS = input.viewDirWS;
	inputData.normalWS = input.normalWS;
#endif

	inputData.normalWS = NormalizeNormalPerPixel(inputData.normalWS);
	viewDirWS = SafeNormalize(viewDirWS);

	inputData.viewDirectionWS = viewDirWS;
#if defined(_MAIN_LIGHT_SHADOWS) && !defined(_RECEIVE_SHADOWS_OFF)
	inputData.shadowCoord = input.shadowCoord;
#else
	inputData.shadowCoord = float4(0, 0, 0, 0);
#endif
	inputData.fogCoord = input.positionWSAndFogFactor.x;
	inputData.vertexLighting = input.positionWSAndFogFactor.yzw;
	inputData.bakedGI = SAMPLE_GI(input.lightmapUV, input.vertexSH, inputData.normalWS);
}


Varyings ToonPassVertex(Attributes input)
{
	Varyings output = (Varyings)0;
	UNITY_SETUP_INSTANCE_ID(input);
	UNITY_TRANSFER_INSTANCE_ID(input, output);
	UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);

	VertexPositionInputs vertexInput = GetVertexPositionInputs(input.positionOS.xyz);
	VertexNormalInputs vertexNormalInput = GetVertexNormalInputs(input.normalOS, input.tangentOS);
	
	float fogFactor = ComputeFogFactor(vertexInput.positionCS.z);

	half4 baseTex = UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial, _BaseMap_ST);
	output.uv = input.uv * baseTex.xy + baseTex.zw;

	output.uvLM = input.uvLM.xy * unity_LightmapST.xy + unity_LightmapST.zw;

	output.positionWSAndFogFactor = float4(vertexInput.positionWS, fogFactor);
	half3 viewDirectionWS = GetCameraPositionWS() - output.positionWSAndFogFactor.xyz;

#ifdef _NORMALMAP
	output.normalWS = half4(vertexNormalInput.normalWS, viewDirectionWS.x);
	output.tangentWS = half4(vertexNormalInput.tangentWS, viewDirectionWS.y);
	output.bitangentWS = half4(vertexNormalInput.bitangentWS, viewDirectionWS.z);

#else
	output.normalWS = NormalizeNormalPerVertex(vertexNormalInput.normalWS);
	output.viewDirWS = viewDirectionWS;
#endif

#ifdef _MAIN_LIGHT_SHADOWS
	output.shadowCoord = GetShadowCoord(vertexInput);
#endif

	
	output.positionCS = vertexInput.positionCS;
	output.color = input.color;

	return output;
}

half4 ToonPassFragment(Varyings input) : SV_Target
{
	UNITY_SETUP_INSTANCE_ID(input);
	UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

	
	half gloss = UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial, _Glossiness);
	half rimThresholdProp = UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial, _RimThreshold);
	half rimAmountProp = UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial, _RimAmount);
	half4 rimColorProp = UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial, _RimColor);
	half4 specColorProp = UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial, _SpecColor);
	half4 emissionColorProp = UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial, _EmissionColor);
	half4 baseMap = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, input.uv.xy);
	half4 baseColor = UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial, _BaseColor);


	SurfaceData surfaceData;
	InitializeStandardLitSurfaceData(input.uv, surfaceData);

	InputData inputData;
	InitializeInputData(input, surfaceData.normalTS, inputData);
	
	Light mainLight = GetMainLight();

	float shadow = mainLight.shadowAttenuation;

	half NdotL = dot(mainLight.direction, inputData.normalWS);

	half lightIntensity = smoothstep(0, 0.001, NdotL * shadow);
	half4 lightColor = half4(mainLight.color, 1);
	half4 light = lightIntensity * lightColor;

	half3 halfVec = normalize(mainLight.direction + input.viewDirWS);

	half NdotH = dot(inputData.normalWS, halfVec);

	half specIntensity = pow(abs(NdotH * lightIntensity), gloss * gloss);
	half specSmooth = smoothstep(0.005, 0.01, specIntensity);
	half4 specular = specSmooth * specColorProp;

	half4 rimDot = 1 - dot(input.viewDirWS, inputData.normalWS);
	half4 rimIntensity = rimDot * pow(abs(NdotL), rimThresholdProp);
	rimIntensity = smoothstep(rimAmountProp - 0.01, rimAmountProp + 0.01, rimIntensity);
	half4 rim = rimIntensity * rimColorProp;

	half4 color = LightweightFragmentPBR(inputData, surfaceData.albedo, surfaceData.metallic, surfaceData.specular,
		surfaceData.smoothness, surfaceData.occlusion, surfaceData.emission, surfaceData.alpha);

	half4 finalBaseColor = baseMap * baseColor;

	color += half4(surfaceData.emission * rim, 1.0);
	color += finalBaseColor * (emissionColorProp + light + specular + rim);

	float fogFactor = input.positionWSAndFogFactor.w;

	color.rgb = MixFog(color.rgb, inputData.fogCoord);

	return color;
}

#endif
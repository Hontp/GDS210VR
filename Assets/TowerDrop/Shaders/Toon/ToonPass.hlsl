#ifndef TOON_INCLUDE
#define TOON_INCLUDE

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


UNITY_INSTANCING_BUFFER_START(UnityPerMaterialColor)
	UNITY_DEFINE_INSTANCED_PROP(half4, _MainTex_ST)	
	UNITY_DEFINE_INSTANCED_PROP(half4, _RimColor)
	UNITY_DEFINE_INSTANCED_PROP(half4, _Color)
UNITY_INSTANCING_BUFFER_END(UnityPerMaterialColor)

UNITY_INSTANCING_BUFFER_START(UnityPerMaterial)
	UNITY_DEFINE_INSTANCED_PROP(half4, _AmbientColor)
	UNITY_DEFINE_INSTANCED_PROP(half4, _SpecularColor)
	UNITY_DEFINE_INSTANCED_PROP(half, _RimAmount)
	UNITY_DEFINE_INSTANCED_PROP(half, _RimThreshold)
	UNITY_DEFINE_INSTANCED_PROP(half, _Glossiness)
UNITY_INSTANCING_BUFFER_END(UnityPerMaterial)

VertexOutput ToonPassVertex(VertexInput v)
{
	VertexOutput o;
	UNITY_SETUP_INSTANCE_ID(v);
	UNITY_TRANSFER_INSTANCE_ID(v, o);
	UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

	VertexPositionInputs vInput = GetVertexPositionInputs(v.vertex.xyz);

	o.position = vInput.positionCS;

	half4 baseTex = UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterialColor, _MainTex_ST);
	o.uv = v.uv * baseTex.xy + baseTex.zw;
	VertexNormalInputs nInput = GetVertexNormalInputs(v.normal);
	o.worldNormal = nInput.normalWS;

	half3 vsDir = mul((half3x3)unity_CameraToWorld, vInput.positionVS.xyz);

	o.viewDir = vsDir;
	o.color = v.color;

	return o;
}

half4 ToonPassFragment(VertexOutput i) : SV_Target
{
	UNITY_SETUP_INSTANCE_ID(i);
	UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);

	half gloss = UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial, _Glossiness);
	half rimThresholdProp  = UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial, _RimThreshold);
	half rimAmountProp = UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial, _RimAmount);
	half4 rimColorProp = UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterialColor, _RimColor);
	half4 specColorProp = UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial, _SpecularColor);
	half4 ambientColorProp = UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial, _AmbientColor);



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

	half specIntensity = pow(abs(NdotH * lightIntensity),  gloss * gloss);
	half specSmooth = smoothstep(0.005, 0.01, specIntensity);
	half4 specular = specSmooth * specColorProp;

	half4 rimDot = 1 - dot(viewDir, normal);
	half4 rimIntensity = rimDot * pow(abs(NdotL), rimThresholdProp);
	rimIntensity = smoothstep(rimAmountProp - 0.01, rimAmountProp + 0.01, rimIntensity);
	half4 rim = rimIntensity * rimColorProp;


	half4 baseMap = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex ,i.uv.xy);
	half4 baseColor = UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterialColor, _Color);

	half4 finalColor = baseMap * baseColor;

	return finalColor * (ambientColorProp + light + specular + rim);
}

#endif
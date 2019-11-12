#ifndef OUTLINE_INCLUDE
#define OUTLINE_INCLUDE

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
	half4 position : SV_POSITION;
	half2 uv : TEXCOORD1;
	half3 viewDir :TEXCOORD2;
	half3 worldNormal : NORMAL;
	half3 color : COLOR;

	UNITY_VERTEX_INPUT_INSTANCE_ID
		UNITY_VERTEX_OUTPUT_STEREO
};

UNITY_INSTANCING_BUFFER_START(UnityPerMaterial)
UNITY_DEFINE_INSTANCED_PROP(half, _Outline)
UNITY_DEFINE_INSTANCED_PROP(half4, _OutlineColor)
UNITY_INSTANCING_BUFFER_END(UnityPerMaterial)

VertexOutput vert(VertexInput v)
{
	VertexOutput o = (VertexOutput)0;
	UNITY_SETUP_INSTANCE_ID(v);
	UNITY_TRANSFER_INSTANCE_ID(v, o);
	UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

	v.vertex.xyz += UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial, _Outline ) * normalize(v.vertex.xyz);

	VertexPositionInputs vInput = GetVertexPositionInputs(v.vertex.xyz);
	o.position = vInput.positionCS;

	return o;
}

half4 frag(VertexOutput IN) : COLOR
{
	UNITY_SETUP_INSTANCE_ID(IN);
	UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(IN);

	half4 outline = UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial, _OutlineColor);

	return outline;
}

#endif

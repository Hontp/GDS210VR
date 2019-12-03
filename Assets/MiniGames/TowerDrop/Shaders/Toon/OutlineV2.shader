Shader "Custom/SobelOutline" 
{

	HLSLINCLUDE

	#include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

	struct Varyings 
	{
		half4 positionCS : SV_POSITION;
		half2 uv[2] : TEXCOORD0;
	};

	TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
	
	half4 _MainTex_TexelSize;
	half4 _MainTex_ST;

	TEXTURE2D_SAMPLER2D(_CameraDepthNormalsTexture, sampler_CameraDepthNormalsTexture);
	half4 _CameraDepthNormalsTexture_ST;

	TEXTURE2D_SAMPLER2D(_CameraDepthTexture, sampler_CameraDepthTexture);
	half4 _CameraDepthTexture_ST;

	half4 _FadeColor;
	half _FadeStength;
	half _SampleDistance;
	half _Exponent;

	Varyings SobelVertexPass(AttributesDefault v)
	{
		Varyings o;
		o.positionCS = half4(v.vertex.xy, 0.0, 1.0);

		half2 uv = TransformTriangleVertexToUV(v.vertex.xy);

#if UNITY_UV_STARTS_AT_TOP
		uv = uv * half2(1.0, -1.0) + half2(0.0, 1.0);
#endif

		o.uv[0] = UnityStereoScreenSpaceUVAdjust(uv, _MainTex_ST);

		o.uv[1] = uv;

		return o;
	}

	half4 SobelFragmentPass(Varyings i) : SV_Target
	{

		half2 adjustedUV = UnityStereoTransformScreenSpaceTex(i.uv[1]);

		half centerDepth = Linear01Depth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sampler_CameraDepthTexture,

		UnityStereoScreenSpaceUVAdjust(i.uv[1], _CameraDepthTexture_ST)));
		
	half4 depthsDiag;
		half4 depthsAxis;

		half2 uvDist = _SampleDistance * _MainTex_TexelSize.xy;

		depthsDiag.x = Linear01Depth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sampler_CameraDepthTexture, UnityStereoScreenSpaceUVAdjust(i.uv[1] + uvDist, _CameraDepthTexture_ST))); // TR
		depthsDiag.y = Linear01Depth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sampler_CameraDepthTexture, UnityStereoScreenSpaceUVAdjust(i.uv[1] + uvDist * half2(-1,1), _CameraDepthTexture_ST))); // TL
		depthsDiag.z = Linear01Depth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sampler_CameraDepthTexture, UnityStereoScreenSpaceUVAdjust(i.uv[1] - uvDist * half2(-1,1), _CameraDepthTexture_ST))); // BR
		depthsDiag.w = Linear01Depth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sampler_CameraDepthTexture, UnityStereoScreenSpaceUVAdjust(i.uv[1] - uvDist, _CameraDepthTexture_ST))); // BL

		depthsAxis.x = Linear01Depth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sampler_CameraDepthTexture, UnityStereoScreenSpaceUVAdjust(i.uv[1] + uvDist * half2(0,1), _CameraDepthTexture_ST))); // T
		depthsAxis.y = Linear01Depth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sampler_CameraDepthTexture, UnityStereoScreenSpaceUVAdjust(i.uv[1] - uvDist * half2(1,0), _CameraDepthTexture_ST))); // L
		depthsAxis.z = Linear01Depth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sampler_CameraDepthTexture, UnityStereoScreenSpaceUVAdjust(i.uv[1] + uvDist * half2(1,0), _CameraDepthTexture_ST))); // R
		depthsAxis.w = Linear01Depth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, sampler_CameraDepthTexture, UnityStereoScreenSpaceUVAdjust(i.uv[1] - uvDist * half2(0,1), _CameraDepthTexture_ST))); // B
	
		depthsDiag = (depthsDiag > centerDepth.xxxx) ? depthsDiag : centerDepth.xxxx;
		depthsAxis = (depthsAxis > centerDepth.xxxx) ? depthsAxis : centerDepth.xxxx;

		depthsDiag -= centerDepth;
		depthsAxis /= centerDepth;

		const half4 HorizDiagCoeff = half4(1,1,-1,-1);
		const half4 VertDiagCoeff = half4(-1,1,-1,1);
		const half4 HorizAxisCoeff = half4(1,0,0,-1);
		const half4 VertAxisCoeff = half4(0,1,-1,0);

		half4 SobelH = depthsDiag * HorizDiagCoeff + depthsAxis * HorizAxisCoeff;
		half4 SobelV = depthsDiag * VertDiagCoeff + depthsAxis * VertAxisCoeff;

		half SobelX = dot(SobelH, half4(1,1,1,1));
		half SobelY = dot(SobelV, half4(1,1,1,1));
		half Sobel = sqrt(SobelX * SobelX + SobelY * SobelY);

		Sobel = 1.0 - pow(saturate(Sobel), _Exponent);
		return Sobel * lerp(SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, adjustedUV), _FadeColor, _FadeStength);
	}


		ENDHLSL

		Subshader 
		{			
			Pass
			{
				 ZTest Always Cull Off ZWrite Off

				 HLSLPROGRAM
				 #pragma target 3.0   
				 #pragma vertex SobelVertexPass
				 #pragma fragment SobelFragmentPass
				 ENDHLSL
			}	
	}

	Fallback off
} 
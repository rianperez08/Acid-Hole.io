Shader "Custom/Mask"
{

	SubShader
	{

		Tags { 
			"RenderType" = "Opaque"
			"RenderPipelined" = "UniversalPipeline"
			"Queued" = "Geometry"
			}

		Pass 
		{
		Blend Zero One
		ZWrite Off
		}
	}
}
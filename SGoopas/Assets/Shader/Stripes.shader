Shader "Unlit/Stripes"
{
	Properties
	{
		// Color property for material inspector, default to white
		_Color("Main Color", Color) = (1,1,1,1)
	}
	SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0


			void vert(
				float4 vertex : POSITION, // vertex position input
				out float4 outpos : SV_POSITION // clip space position output
			)
			{
				outpos = UnityObjectToClipPos(vertex);
			}

			// color from the material
			fixed4 _Color;

			fixed4 frag(UNITY_VPOS_TYPE screenPos : VPOS) : SV_Target
			{
				// screenPos.xy will contain pixel integer coordinates.
				// use them to implement a stripe pattern that skips rendering
				// 8 pixels tall stripes of pixels

				// stripe value will be negative for 16 pixels tall stripes of pixels, 0 for 8 pixels tall stripes of pixels, the negative for 16, 0 for 8... so on and so forth
				// in a checkerboard pattern.
				float stripe = abs(screenPos.x - screenPos.y);
				stripe = floor(stripe * 0.125 + 0.5) * 0.25;
				stripe = -frac(stripe);

				// clip HLSL instruction stops rendering a pixel if value is negative
				clip(stripe);

				// for pixels that were kept, output the color;
				return _Color;
			}
			ENDCG
		}
	}
}

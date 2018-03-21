// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "FlamingSands/PBR Metallic POM"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
		_Tiling("Tiling", Float) = 1
		_Offset("Offset", Vector) = (0,0,0,0)
		_Albedo("Albedo", 2D) = "white" {}
		_RMA("RMA", 2D) = "white" {}
		_MetallnessValue("Metallness Value", Float) = 1
		_Glossiness("Glossiness", Float) = 1
		_Height("Height", 2D) = "white" {}
		_Bias("Bias", Range( 0 , 1)) = 0.5
		_Depth("Depth", Range( 0 , 1)) = 0.02
		_NormalIntensity("Normal Intensity", Float) = 1
		_Normal("Normal", 2D) = "bump" {}
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGINCLUDE
		#include "UnityStandardUtils.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#ifdef UNITY_PASS_SHADOWCASTER
			#undef INTERNAL_DATA
			#undef WorldReflectionVector
			#undef WorldNormalVector
			#define INTERNAL_DATA half3 internalSurfaceTtoW0; half3 internalSurfaceTtoW1; half3 internalSurfaceTtoW2;
			#define WorldReflectionVector(data,normal) reflect (data.worldRefl, half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal)))
			#define WorldNormalVector(data,normal) fixed3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal))
		#endif
		struct Input
		{
			float2 texcoord_0;
			float3 viewDir;
			INTERNAL_DATA
			float3 worldNormal;
			float3 worldPos;
		};

		uniform float _NormalIntensity;
		uniform sampler2D _Normal;
		uniform float _Tiling;
		uniform float2 _Offset;
		uniform sampler2D _Height;
		uniform float _Depth;
		uniform float _Bias;
		uniform float4 _Height_ST;
		uniform sampler2D _Albedo;
		uniform sampler2D _RMA;
		uniform float _MetallnessValue;
		uniform float _Glossiness;


		inline float2 POM( sampler2D heightMap, float2 uvs, float2 dx, float2 dy, float3 normalWorld, float3 viewWorld, float3 viewDirTan, int minSamples, int maxSamples, float parallax, float refPlane, float2 tilling, float2 curv )
		{
			float3 result = 0;
			int stepIndex = 0;
			int numSteps = ( int )lerp( maxSamples, minSamples, dot( normalWorld, viewWorld ) );
			float layerHeight = 1.0 / numSteps;
			float2 plane = parallax * ( viewDirTan.xy / viewDirTan.z );
			uvs += refPlane * plane;
			float2 deltaTex = -plane * layerHeight;
			float2 prevTexOffset = 0;
			float prevRayZ = 1.0f;
			float prevHeight = 0.0f;
			float2 currTexOffset = deltaTex;
			float currRayZ = 1.0f - layerHeight;
			float currHeight = 0.0f;
			float intersection = 0;
			float2 finalTexOffset = 0;
			while ( stepIndex < numSteps + 1 )
			{
				currHeight = tex2Dgrad( heightMap, uvs + currTexOffset, dx, dy ).r;
				if ( currHeight > currRayZ )
				{
					stepIndex = numSteps + 1;
				}
				else
				{
					stepIndex++;
					prevTexOffset = currTexOffset;
					prevRayZ = currRayZ;
					prevHeight = currHeight;
					currTexOffset += deltaTex;
					currRayZ -= layerHeight;
				}
			}
			int sectionSteps = 2;
			int sectionIndex = 0;
			float newZ = 0;
			float newHeight = 0;
			while ( sectionIndex < sectionSteps )
			{
				intersection = ( prevHeight - prevRayZ ) / ( prevHeight - currHeight + currRayZ - prevRayZ );
				finalTexOffset = prevTexOffset + intersection * deltaTex;
				newZ = prevRayZ - intersection * layerHeight;
				newHeight = tex2Dgrad( heightMap, uvs + finalTexOffset, dx, dy ).r;
				if ( newHeight > newZ )
				{
					currTexOffset = finalTexOffset;
					currHeight = newHeight;
					currRayZ = newZ;
					deltaTex = intersection * deltaTex;
					layerHeight = intersection * layerHeight;
				}
				else
				{
					prevTexOffset = finalTexOffset;
					prevHeight = newHeight;
					prevRayZ = newZ;
					deltaTex = ( 1 - intersection ) * deltaTex;
					layerHeight = ( 1 - intersection ) * layerHeight;
				}
				sectionIndex++;
			}
			return uvs + finalTexOffset;
		}


		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float2 temp_cast_0 = (_Tiling).xx;
			o.texcoord_0.xy = v.texcoord.xy * temp_cast_0 + _Offset;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float3 ase_worldNormal = WorldNormalVector( i, float3( 0, 0, 1 ) );
			float3 worldViewDir = normalize( UnityWorldSpaceViewDir( i.worldPos ) );
			float2 OffsetPOM34 = POM( _Height, i.texcoord_0, ddx(i.texcoord_0), ddx(i.texcoord_0), ase_worldNormal, worldViewDir, i.viewDir, 8, 16, _Depth, _Bias, _Height_ST.xy, float2(0,0) );
			o.Normal = UnpackScaleNormal( tex2D( _Normal,OffsetPOM34, ddx( i.texcoord_0 ), ddy( i.texcoord_0 )) ,_NormalIntensity );
			o.Albedo = tex2D( _Albedo,OffsetPOM34).rgb;
			float4 tex2DNode19 = tex2D( _RMA,OffsetPOM34);
			o.Metallic = clamp( ( tex2DNode19.g * _MetallnessValue ) , 0.0 , 1.0 );
			o.Smoothness = clamp( ( tex2DNode19.r * _Glossiness ) , 0.0 , 1.0 );
			o.Occlusion = tex2DNode19.b;
			o.Alpha = 1;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard keepalpha vertex:vertexDataFunc 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			# include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float3 worldPos : TEXCOORD6;
				float4 tSpace0 : TEXCOORD1;
				float4 tSpace1 : TEXCOORD2;
				float4 tSpace2 : TEXCOORD3;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				fixed3 worldTangent = UnityObjectToWorldDir( v.tangent.xyz );
				fixed tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				fixed3 worldBinormal = cross( worldNormal, worldTangent ) * tangentSign;
				o.tSpace0 = float4( worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x );
				o.tSpace1 = float4( worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y );
				o.tSpace2 = float4( worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z );
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			fixed4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				float3 worldPos = float3( IN.tSpace0.w, IN.tSpace1.w, IN.tSpace2.w );
				fixed3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.viewDir = IN.tSpace0.xyz * worldViewDir.x + IN.tSpace1.xyz * worldViewDir.y + IN.tSpace2.xyz * worldViewDir.z;
				surfIN.worldPos = worldPos;
				surfIN.worldNormal = float3( IN.tSpace0.z, IN.tSpace1.z, IN.tSpace2.z );
				surfIN.internalSurfaceTtoW0 = IN.tSpace0.xyz;
				surfIN.internalSurfaceTtoW1 = IN.tSpace1.xyz;
				surfIN.internalSurfaceTtoW2 = IN.tSpace2.xyz;
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=7003
-1913;29;1906;1004;2017.802;449.9019;1.3;True;False
Node;AmplifyShaderEditor.RangedFloatNode;25;-2435.721,-120.1711;Float;False;Property;_Tiling;Tiling;1;0;1;0;0;FLOAT
Node;AmplifyShaderEditor.Vector2Node;26;-2432.421,-21.17109;Float;False;Property;_Offset;Offset;2;0;0,0;FLOAT2;FLOAT;FLOAT
Node;AmplifyShaderEditor.TexturePropertyNode;31;-1921.221,-334.1714;Float;True;Property;_Height;Height;7;0;None;False;white;Auto;SAMPLER2D
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;28;-1985.224,121.4299;Float;False;Tangent;FLOAT3;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;29;-2054.224,266.4292;Float;False;Property;_Bias;Bias;8;0;0.5;0;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;30;-2018.428,42.22986;Float;False;Property;_Depth;Depth;9;0;0.02;0;1;FLOAT
Node;AmplifyShaderEditor.TextureCoordinatesNode;27;-2077.231,-113.4699;Float;False;0;-1;2;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;FLOAT2;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.ParallaxOcclusionMappingNode;34;-1438.926,-10.77078;Float;False;0;8;16;2;0.02;0;False;1,1;False;0,0;0;FLOAT2;0,0;False;1;SAMPLER2D;;False;2;FLOAT;0.02;False;3;FLOAT3;0,0,0;False;4;FLOAT;0.0;False;5;FLOAT2;0,0;False;FLOAT2
Node;AmplifyShaderEditor.SamplerNode;19;-912,160;Float;True;Property;_RMA;RMA;4;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;2;-896,352;Float;False;Property;_MetallnessValue;Metallness Value;5;0;1;0;0;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;13;-896,80;Float;False;Property;_Glossiness;Glossiness;6;0;1;0;0;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;-387,91;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;9;-1242,507;Float;False;Property;_NormalIntensity;Normal Intensity;10;0;1;0;0;FLOAT
Node;AmplifyShaderEditor.DdxOpNode;32;-1417.721,175.8286;Float;False;0;FLOAT2;0.0;False;FLOAT2
Node;AmplifyShaderEditor.DdyOpNode;33;-1419.721,242.8283;Float;False;0;FLOAT2;0.0;False;FLOAT2
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;-496,256;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0,0,0,0;False;FLOAT
Node;AmplifyShaderEditor.SamplerNode;1;-912,-112;Float;True;Property;_Albedo;Albedo;3;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.ClampOpNode;23;-209.2129,135.5993;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;FLOAT
Node;AmplifyShaderEditor.SamplerNode;7;-896,432;Float;True;Property;_Normal;Normal;11;0;None;True;0;True;bump;Auto;True;Object;-1;Derivative;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;FLOAT3;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.ClampOpNode;24;-203.2129,11.5993;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;FLOAT
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;128,0;Float;False;True;2;Float;ASEMaterialInspector;0;Standard;FlamingSands/PBR Metallic POM;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;Opaque;0.55;True;True;0;False;Opaque;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;False;0;4;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;Add;Add;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;Relative;0;;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;OBJECT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;13;OBJECT;0.0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False
WireConnection;27;0;25;0
WireConnection;27;1;26;0
WireConnection;34;0;27;0
WireConnection;34;1;31;0
WireConnection;34;2;30;0
WireConnection;34;3;28;0
WireConnection;34;4;29;0
WireConnection;19;1;34;0
WireConnection;12;0;19;1
WireConnection;12;1;13;0
WireConnection;32;0;27;0
WireConnection;33;0;27;0
WireConnection;6;0;19;2
WireConnection;6;1;2;0
WireConnection;1;1;34;0
WireConnection;23;0;6;0
WireConnection;7;1;34;0
WireConnection;7;3;32;0
WireConnection;7;4;33;0
WireConnection;7;5;9;0
WireConnection;24;0;12;0
WireConnection;0;0;1;0
WireConnection;0;1;7;0
WireConnection;0;3;23;0
WireConnection;0;4;24;0
WireConnection;0;5;19;3
ASEEND*/
//CHKSM=0EB6EBDE8137D2249CF2B3F8759839D1C8516310
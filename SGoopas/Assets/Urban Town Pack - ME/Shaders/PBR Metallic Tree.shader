// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "FlamingSands/Nature/PBR Metallic Tree"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
		_Albedo("Albedo", 2D) = "white" {}
		_MetallnessValue("Metallness Value", Float) = 0
		_Glossiness("Glossiness", Float) = 1
		_NormalIntensity("Normal Intensity", Float) = 1
		_Normal("Normal", 2D) = "bump" {}
		_DetailAlbedo("Detail Albedo", 2D) = "white" {}
		_DetailNormal("Detail Normal", 2D) = "bump" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityStandardUtils.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			float2 uv_texcoord;
			float2 texcoord_0;
			float4 vertexColor : COLOR;
		};

		uniform float _NormalIntensity;
		uniform sampler2D _Normal;
		uniform float4 _Normal_ST;
		uniform sampler2D _DetailNormal;
		uniform sampler2D _DetailAlbedo;
		uniform sampler2D _Albedo;
		uniform float4 _Albedo_ST;
		uniform float _MetallnessValue;
		uniform float _Glossiness;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			o.texcoord_0.xy = v.texcoord1.xy * float2( 1,1 ) + float2( 0,0 );
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Normal = i.uv_texcoord * _Normal_ST.xy + _Normal_ST.zw;
			float4 tex2DNode22 = tex2D( _DetailAlbedo,i.texcoord_0);
			o.Normal = lerp( UnpackScaleNormal( tex2D( _Normal,uv_Normal) ,_NormalIntensity ) , UnpackScaleNormal( tex2D( _DetailNormal,i.texcoord_0) ,_NormalIntensity ) , tex2DNode22.a );
			float2 uv_Albedo = i.uv_texcoord * _Albedo_ST.xy + _Albedo_ST.zw;
			o.Albedo = lerp( tex2D( _Albedo,uv_Albedo) , tex2DNode22 , tex2DNode22.a ).rgb;
			o.Metallic = clamp( _MetallnessValue , 0.0 , 1.0 );
			o.Smoothness = clamp( _Glossiness , 0.0 , 1.0 );
			o.Occlusion = i.vertexColor.r;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=7003
-1913;29;1901;1004;2825.915;1105.388;2.823404;True;True
Node;AmplifyShaderEditor.RangedFloatNode;9;-1212,506;Float;False;Property;_NormalIntensity;Normal Intensity;3;0;1;0;0;FLOAT
Node;AmplifyShaderEditor.TextureCoordinatesNode;24;-1531.706,704.7992;Float;False;1;-1;2;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;FLOAT2;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;23;-896,864;Float;True;Property;_DetailNormal;Detail Normal;6;0;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;FLOAT3;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;7;-896,432;Float;True;Property;_Normal;Normal;4;0;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;FLOAT3;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;13;-896,80;Float;False;Property;_Glossiness;Glossiness;2;0;1;0;0;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;2;-896,352;Float;False;Property;_MetallnessValue;Metallness Value;1;0;0;0;0;FLOAT
Node;AmplifyShaderEditor.SamplerNode;22;-896,672;Float;True;Property;_DetailAlbedo;Detail Albedo;5;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;1;-912,-112;Float;True;Property;_Albedo;Albedo;0;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.ClampOpNode;20;-192,-32;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;FLOAT
Node;AmplifyShaderEditor.LerpOp;26;-112,576;Float;False;0;FLOAT3;0.0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0.0;False;FLOAT3
Node;AmplifyShaderEditor.VertexColorNode;27;-880,176;Float;False;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.LerpOp;25;-108.908,423.9985;Float;False;0;COLOR;0,0,0,0;False;1;COLOR;0.0,0,0,0;False;2;FLOAT;0.0;False;COLOR
Node;AmplifyShaderEditor.ClampOpNode;21;-192,224;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;FLOAT
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1536,0;Float;False;True;2;Float;ASEMaterialInspector;0;Standard;FlamingSands/Nature/PBR Metallic Tree;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;Opaque;0.5;True;True;0;False;Opaque;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;False;0;4;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;Add;Add;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;Relative;0;;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;OBJECT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;13;OBJECT;0.0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False
WireConnection;23;1;24;0
WireConnection;23;5;9;0
WireConnection;7;5;9;0
WireConnection;22;1;24;0
WireConnection;20;0;13;0
WireConnection;26;0;7;0
WireConnection;26;1;23;0
WireConnection;26;2;22;4
WireConnection;25;0;1;0
WireConnection;25;1;22;0
WireConnection;25;2;22;4
WireConnection;21;0;2;0
WireConnection;0;0;25;0
WireConnection;0;1;26;0
WireConnection;0;3;21;0
WireConnection;0;4;20;0
WireConnection;0;5;27;0
ASEEND*/
//CHKSM=6B1D2C34CF66E3941C9CB9E46B16AFFD37F7CF96
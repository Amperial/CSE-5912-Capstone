// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "FlamingSands/PBR Metallic Projection"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
		_Tiling("Tiling", Float) = 5
		_Albedo("Albedo", 2D) = "white" {}
		_RMA("RMA", 2D) = "white" {}
		_MetallnessValue("Metallness Value", Float) = 0
		_Glossiness("Glossiness", Float) = 1
		_NormalIntensity("Normal Intensity", Float) = 1
		_Normal("Normal", 2D) = "bump" {}
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityStandardUtils.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float3 worldPos;
		};

		uniform float _NormalIntensity;
		uniform sampler2D _Normal;
		uniform float _Tiling;
		uniform sampler2D _Albedo;
		uniform sampler2D _RMA;
		uniform float _MetallnessValue;
		uniform float _Glossiness;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float3 ase_worldPos = i.worldPos;
			float2 appendResult24 = float2( ase_worldPos.x , ase_worldPos.z );
			float2 temp_output_25_0 = ( appendResult24 / _Tiling );
			o.Normal = UnpackScaleNormal( tex2D( _Normal,temp_output_25_0) ,_NormalIntensity );
			o.Albedo = tex2D( _Albedo,temp_output_25_0).rgb;
			float4 tex2DNode19 = tex2D( _RMA,temp_output_25_0);
			o.Metallic = clamp( ( tex2DNode19.g * _MetallnessValue ) , 0.0 , 1.0 );
			o.Smoothness = clamp( ( tex2DNode19.r * _Glossiness ) , 0.0 , 1.0 );
			o.Occlusion = tex2DNode19.b;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=7003
-1913;29;1906;1004;2504.409;252.3004;1;True;False
Node;AmplifyShaderEditor.WorldPosInputsNode;22;-2026.159,63.19958;Float;False;FLOAT3;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.AppendNode;24;-1692.159,77.19958;Float;False;FLOAT2;0;0;0;0;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;FLOAT2
Node;AmplifyShaderEditor.RangedFloatNode;23;-1661.159,198.1996;Float;False;Property;_Tiling;Tiling;0;0;5;0;0;FLOAT
Node;AmplifyShaderEditor.SimpleDivideOpNode;25;-1414.159,60.19958;Float;False;0;FLOAT2;0.0;False;1;FLOAT;0,0;False;FLOAT2
Node;AmplifyShaderEditor.RangedFloatNode;2;-896,352;Float;False;Property;_MetallnessValue;Metallness Value;3;0;0;0;0;FLOAT
Node;AmplifyShaderEditor.SamplerNode;19;-912,160;Float;True;Property;_RMA;RMA;2;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;13;-896,80;Float;False;Property;_Glossiness;Glossiness;4;0;1;0;0;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;9;-1212,506;Float;False;Property;_NormalIntensity;Normal Intensity;5;0;1;0;0;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;-384,224;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0,0,0,0;False;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;-384,96;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.SamplerNode;7;-896,432;Float;True;Property;_Normal;Normal;6;0;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;FLOAT3;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;1;-912,-112;Float;True;Property;_Albedo;Albedo;1;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.ClampOpNode;21;-192,224;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;FLOAT
Node;AmplifyShaderEditor.ClampOpNode;20;-192,64;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;FLOAT
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;128,0;Float;False;True;2;Float;ASEMaterialInspector;0;Standard;FlamingSands/PBR Metallic Projection;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;Opaque;0.5;True;True;0;False;Opaque;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;False;0;4;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;Add;Add;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;Relative;0;;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;OBJECT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;13;OBJECT;0.0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False
WireConnection;24;0;22;1
WireConnection;24;1;22;3
WireConnection;25;0;24;0
WireConnection;25;1;23;0
WireConnection;19;1;25;0
WireConnection;6;0;19;2
WireConnection;6;1;2;0
WireConnection;12;0;19;1
WireConnection;12;1;13;0
WireConnection;7;1;25;0
WireConnection;7;5;9;0
WireConnection;1;1;25;0
WireConnection;21;0;6;0
WireConnection;20;0;12;0
WireConnection;0;0;1;0
WireConnection;0;1;7;0
WireConnection;0;3;21;0
WireConnection;0;4;20;0
WireConnection;0;5;19;3
ASEEND*/
//CHKSM=41F03A757A98ECEFFB61703F169836EE4270ECC7
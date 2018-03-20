// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "FlamingSands/PBR Metallic Emissive"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
		_Emission("Emission", Float) = 1
		_EmmisiveColor("Emmisive Color", Color) = (0,0,0,0)
		_Albedo("Albedo", 2D) = "white" {}
		_RMAA("RMA (A)", 2D) = "white" {}
		_MetallnessValue("Metallness Value", Float) = 0
		_Glossiness("Glossiness", Float) = 1
		_NormalIntensity("Normal Intensity", Float) = 1
		_Normal("Normal", 2D) = "bump" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityStandardUtils.cginc"
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float _NormalIntensity;
		uniform sampler2D _Normal;
		uniform float4 _Normal_ST;
		uniform sampler2D _Albedo;
		uniform float4 _Albedo_ST;
		uniform sampler2D _RMAA;
		uniform float4 _RMAA_ST;
		uniform float _Emission;
		uniform float4 _EmmisiveColor;
		uniform float _MetallnessValue;
		uniform float _Glossiness;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Normal = i.uv_texcoord * _Normal_ST.xy + _Normal_ST.zw;
			o.Normal = UnpackScaleNormal( tex2D( _Normal,uv_Normal) ,_NormalIntensity );
			float2 uv_Albedo = i.uv_texcoord * _Albedo_ST.xy + _Albedo_ST.zw;
			float4 tex2DNode1 = tex2D( _Albedo,uv_Albedo);
			o.Albedo = tex2DNode1.rgb;
			float2 uv_RMAA = i.uv_texcoord * _RMAA_ST.xy + _RMAA_ST.zw;
			float4 tex2DNode19 = tex2D( _RMAA,uv_RMAA);
			float temp_output_40_0 = ( 5.0 * _Time.y );
			o.Emission = ( ( clamp( ( tex2DNode19.a * ( _Emission * clamp( ( 1.0 - radians( ( tan( temp_output_40_0 ) * sin( temp_output_40_0 ) ) ) ) , 0.25 , 1.0 ) ) ) , 0 , 1 ) * _EmmisiveColor ) * ( tex2DNode1 * 0.5 ) ).rgb;
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
-1915;27;1906;1004;1038.318;235.4008;1;True;True
Node;AmplifyShaderEditor.RangedFloatNode;39;-2499.212,577.399;Float;False;Constant;_Speed;Speed;8;0;5;0;0;FLOAT
Node;AmplifyShaderEditor.TimeNode;41;-2541.212,682.399;Float;False;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;40;-2223.212,614.399;Float;True;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.TanOpNode;48;-1982.913,617.2994;Float;True;0;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.SinOpNode;50;-1978.913,866.2994;Float;True;0;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;51;-1764.913,727.2994;Float;True;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.RadiansOpNode;54;-1476.913,743.2993;Float;True;0;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.OneMinusNode;46;-1234.913,648.2994;Float;True;0;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;22;-1072,352;Float;False;Property;_Emission;Emission;0;0;1;0;0;FLOAT
Node;AmplifyShaderEditor.ClampOpNode;45;-1072.213,657.399;Float;True;0;FLOAT;0.0;False;1;FLOAT;0.25;False;2;FLOAT;1.0;False;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;44;-720,544;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.SamplerNode;19;-1104,160;Float;True;Property;_RMAA;RMA (A);3;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;21;-480,240;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;2;-768,0;Float;False;Property;_MetallnessValue;Metallness Value;4;0;0;0;0;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;13;-768,128;Float;False;Property;_Glossiness;Glossiness;5;0;1;0;0;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;30;-332,-101;Float;False;Constant;_Float0;Float 0;8;0;0.5;0;0;FLOAT
Node;AmplifyShaderEditor.ClampOpNode;25;-303,241;Float;False;0;FLOAT;0.0;False;1;FLOAT;0;False;2;FLOAT;1;False;FLOAT
Node;AmplifyShaderEditor.ColorNode;27;-768,368;Float;False;Property;_EmmisiveColor;Emmisive Color;1;0;0,0,0,0;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;1;-1104,-112;Float;True;Property;_Albedo;Albedo;2;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;28;-80,240;Float;True;0;FLOAT;0.0;False;1;COLOR;0.0;False;COLOR
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;31;-88,14;Float;True;0;COLOR;0.0;False;1;FLOAT;0.0,0,0,0;False;COLOR
Node;AmplifyShaderEditor.RangedFloatNode;9;-1344,512;Float;False;Property;_NormalIntensity;Normal Intensity;6;0;1;0;0;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;-480,-16;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0,0,0,0;False;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;-480,112;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;29;171.587,199.7992;Float;True;0;COLOR;0.0,0,0,0;False;1;COLOR;0,0,0,0;False;COLOR
Node;AmplifyShaderEditor.ClampOpNode;24;-304,112;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;FLOAT
Node;AmplifyShaderEditor.SamplerNode;7;-1088,432;Float;True;Property;_Normal;Normal;7;0;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;FLOAT3;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.ClampOpNode;23;-304,-16;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;FLOAT
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;381,-1;Float;False;True;2;Float;ASEMaterialInspector;0;Standard;FlamingSands/PBR Metallic Emissive;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;Opaque;0.55;True;True;0;False;Opaque;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;False;0;4;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;Add;Add;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;Relative;0;;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;OBJECT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;13;OBJECT;0.0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False
WireConnection;40;0;39;0
WireConnection;40;1;41;2
WireConnection;48;0;40;0
WireConnection;50;0;40;0
WireConnection;51;0;48;0
WireConnection;51;1;50;0
WireConnection;54;0;51;0
WireConnection;46;0;54;0
WireConnection;45;0;46;0
WireConnection;44;0;22;0
WireConnection;44;1;45;0
WireConnection;21;0;19;4
WireConnection;21;1;44;0
WireConnection;25;0;21;0
WireConnection;28;0;25;0
WireConnection;28;1;27;0
WireConnection;31;0;1;0
WireConnection;31;1;30;0
WireConnection;6;0;19;2
WireConnection;6;1;2;0
WireConnection;12;0;19;1
WireConnection;12;1;13;0
WireConnection;29;0;28;0
WireConnection;29;1;31;0
WireConnection;24;0;12;0
WireConnection;7;5;9;0
WireConnection;23;0;6;0
WireConnection;0;0;1;0
WireConnection;0;1;7;0
WireConnection;0;2;29;0
WireConnection;0;3;23;0
WireConnection;0;4;24;0
WireConnection;0;5;19;3
ASEEND*/
//CHKSM=CC27BB435B378BBA14EE73FA5D6ABDE965E9B878
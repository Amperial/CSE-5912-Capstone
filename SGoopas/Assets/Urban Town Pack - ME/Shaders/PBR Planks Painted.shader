// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "FlamingSands/PBR Planks Painted"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
		_AlbedoColor("Albedo Color", Color) = (1,1,1,0)
		_Albedo("Albedo", 2D) = "white" {}
		_Normal("Normal", 2D) = "bump" {}
		_Metallic("Metallic", Float) = 0.21
		_Glossiness("Glossiness", Range( 0 , 2)) = 1
		_RMA("RMA", 2D) = "white" {}
		_PaintMask("Paint Mask", 2D) = "white" {}
		_PaintColor("Paint Color", Color) = (0.2666667,0.427451,0.7411765,0)
		_PaintGlossiness("Paint Glossiness", Range( 0 , 1)) = 0.5
		_PaintIntensity("Paint Intensity", Range( 0 , 1)) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _Normal;
		uniform float4 _Normal_ST;
		uniform sampler2D _Albedo;
		uniform float4 _Albedo_ST;
		uniform float4 _AlbedoColor;
		uniform sampler2D _PaintMask;
		uniform float4 _PaintMask_ST;
		uniform float4 _PaintColor;
		uniform float _PaintIntensity;
		uniform float _Metallic;
		uniform sampler2D _RMA;
		uniform float4 _RMA_ST;
		uniform float _Glossiness;
		uniform float _PaintGlossiness;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Normal = i.uv_texcoord * _Normal_ST.xy + _Normal_ST.zw;
			o.Normal = UnpackNormal( tex2D( _Normal,uv_Normal) );
			float2 uv_Albedo = i.uv_texcoord * _Albedo_ST.xy + _Albedo_ST.zw;
			float4 tex2DNode1 = tex2D( _Albedo,uv_Albedo);
			float2 uv_PaintMask = i.uv_texcoord * _PaintMask_ST.xy + _PaintMask_ST.zw;
			float4 tex2DNode20 = tex2D( _PaintMask,uv_PaintMask);
			float temp_output_27_0 = dot( _PaintIntensity , tex2DNode20.r );
			o.Albedo = lerp( lerp( tex2DNode1 , ( _AlbedoColor * tex2DNode1 ) , tex2DNode20.g ) , _PaintColor , temp_output_27_0 ).rgb;
			float2 uv_RMA = i.uv_texcoord * _RMA_ST.xy + _RMA_ST.zw;
			float4 tex2DNode10 = tex2D( _RMA,uv_RMA);
			o.Metallic = ( _Metallic * tex2DNode10.g );
			o.Smoothness = lerp( ( _Glossiness * tex2DNode10.r ) , _PaintGlossiness , temp_output_27_0 );
			o.Occlusion = tex2DNode10.b;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=7003
7;29;1901;1004;1641.779;233.8381;1.3;True;True
Node;AmplifyShaderEditor.ColorNode;28;-1216,-368;Float;False;Property;_AlbedoColor;Albedo Color;0;0;1,1,1,0;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;1;-1232,-32;Float;True;Property;_Albedo;Albedo;1;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;24;-900,737;Float;False;Property;_PaintIntensity;Paint Intensity;9;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;29;-784,-128;Float;False;0;COLOR;0.0;False;1;FLOAT4;0.0,0,0,0;False;FLOAT4
Node;AmplifyShaderEditor.RangedFloatNode;13;-578,448;Float;False;Property;_Glossiness;Glossiness;4;0;1;0;2;FLOAT
Node;AmplifyShaderEditor.SamplerNode;10;-976,352;Float;True;Property;_RMA;RMA;5;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;20;-976,544;Float;True;Property;_PaintMask;Paint Mask;6;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.LerpOp;30;-576,-112;Float;False;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0,0,0,0;False;2;FLOAT;0.0;False;FLOAT4
Node;AmplifyShaderEditor.ColorNode;18;-1215,-192;Float;False;Property;_PaintColor;Paint Color;7;0;0.2666667,0.427451,0.7411765,0;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;21;-402,572;Float;False;Property;_PaintGlossiness;Paint Glossiness;8;0;0.5;0;1;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;16;-279,450;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;12;-400,224;Float;False;Property;_Metallic;Metallic;3;0;0.21;0;0;FLOAT
Node;AmplifyShaderEditor.DotProductOpNode;27;-548,723;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;-192,224;Float;False;0;FLOAT;0.0;False;1;FLOAT;0,0,0,0;False;FLOAT
Node;AmplifyShaderEditor.LerpOp;19;-316,17;Float;False;0;FLOAT4;0.0,0,0,0;False;1;COLOR;0.0,0,0,0;False;2;FLOAT;0.0,0,0,0;False;COLOR
Node;AmplifyShaderEditor.SamplerNode;9;-976,160;Float;True;Property;_Normal;Normal;2;0;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;FLOAT3;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.LerpOp;22;-64,544;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;304,16;Float;False;True;2;Float;ASEMaterialInspector;0;Standard;FlamingSands/PBR Planks Painted;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;Opaque;0.5;True;True;0;False;Opaque;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;False;0;4;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;Add;Add;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;Relative;0;;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;OBJECT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;13;OBJECT;0.0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False
WireConnection;29;0;28;0
WireConnection;29;1;1;0
WireConnection;30;0;1;0
WireConnection;30;1;29;0
WireConnection;30;2;20;2
WireConnection;16;0;13;0
WireConnection;16;1;10;1
WireConnection;27;0;24;0
WireConnection;27;1;20;1
WireConnection;11;0;12;0
WireConnection;11;1;10;2
WireConnection;19;0;30;0
WireConnection;19;1;18;0
WireConnection;19;2;27;0
WireConnection;22;0;16;0
WireConnection;22;1;21;0
WireConnection;22;2;27;0
WireConnection;0;0;19;0
WireConnection;0;1;9;0
WireConnection;0;3;11;0
WireConnection;0;4;22;0
WireConnection;0;5;10;3
ASEEND*/
//CHKSM=95D6CC38817E9B666823804602430BB09B8FDFF1
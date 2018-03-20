// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "FlamingSands/Decals/PBR Detail Decal"
{
	Properties
	{
		_MaskClipValue( "Mask Clip Value", Float ) = 0.5
		[HideInInspector] __dirty( "", Int ) = 1
		_Albedo("Albedo", 2D) = "white" {}
		_Glossiness("Glossiness", Float) = 1
		_Metallic("Metallic", Float) = 1
		_RMA("RMA", 2D) = "white" {}
		_NormalIntensity("Normal Intensity", Float) = 1
		_Normal("Normal", 2D) = "bump" {}
		_Tiling("Tiling", Float) = 1
		_Height("Height", 2D) = "white" {}
		_HeightTiling("Height Tiling", Float) = 1
		_HeightContrast("Height Contrast", Float) = 1
		_AlbedoDetail("Albedo Detail", Range( 0 , 1)) = 1
		_DetailBrightness("Detail Brightness", Float) = 1
		_DetailAlbedo("Detail Albedo", 2D) = "white" {}
		_DetailNormal("Detail Normal", 2D) = "bump" {}
		_DetailTiling("Detail Tiling", Float) = 1
		_DetailNormalIntensity("Detail Normal Intensity", Float) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "AlphaTest+0" }
		Cull Back
		CGPROGRAM
		#include "UnityStandardUtils.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			float2 texcoord_0;
			float2 texcoord_1;
			float2 texcoord_2;
			float4 vertexColor : COLOR;
		};

		uniform float _NormalIntensity;
		uniform sampler2D _Normal;
		uniform float _Tiling;
		uniform float _DetailNormalIntensity;
		uniform sampler2D _DetailNormal;
		uniform float _DetailTiling;
		uniform sampler2D _Albedo;
		uniform float _DetailBrightness;
		uniform sampler2D _DetailAlbedo;
		uniform float _AlbedoDetail;
		uniform sampler2D _RMA;
		uniform float _Metallic;
		uniform float _Glossiness;
		uniform sampler2D _Height;
		uniform float _HeightTiling;
		uniform float _HeightContrast;
		uniform float _MaskClipValue = 0.5;


		float4 CalculateContrast( float contrastValue, float4 colorTarget )
		{
			float t = 0.5 * ( 1.0 - contrastValue );
			return mul( float4x4( contrastValue, 0, 0, 0,0,contrastValue, 0, 0,0,0,contrastValue, 0,t, t, t, 1 ), colorTarget );
		}


		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float2 temp_cast_0 = (_Tiling).xx;
			o.texcoord_0.xy = v.texcoord.xy * temp_cast_0 + float2( 0,0 );
			float2 temp_cast_1 = (_DetailTiling).xx;
			o.texcoord_1.xy = v.texcoord.xy * temp_cast_1 + float2( 0,0 );
			float2 temp_cast_2 = (_HeightTiling).xx;
			o.texcoord_2.xy = v.texcoord.xy * temp_cast_2 + float2( 0,0 );
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			o.Normal = BlendNormals( UnpackScaleNormal( tex2D( _Normal,i.texcoord_0) ,_NormalIntensity ) , UnpackScaleNormal( tex2D( _DetailNormal,i.texcoord_1) ,_DetailNormalIntensity ) );
			float4 tex2DNode1 = tex2D( _Albedo,i.texcoord_0);
			o.Albedo = lerp( tex2DNode1 , ( clamp( ( _DetailBrightness * tex2D( _DetailAlbedo,i.texcoord_1).b ) , 0.0 , 1.0 ) * tex2DNode1 ) , _AlbedoDetail ).rgb;
			float4 tex2DNode32 = tex2D( _RMA,i.texcoord_0);
			o.Metallic = ( tex2DNode32.g * _Metallic );
			o.Smoothness = ( tex2DNode32.r * _Glossiness );
			o.Occlusion = tex2DNode32.a;
			o.Alpha = 1;
			float4 temp_cast_1 = (_HeightContrast).xxxx;
			float componentMask51 = round( clamp( CalculateContrast(clamp( ( ( tex2D( _Height,i.texcoord_2).r - 1.0 ) + ( i.vertexColor.r * 2.0 ) ) , 0.0 , 1.0 ),temp_cast_1) , float4( 0,0,0,0 ) , float4( 1,0,0,0 ) ) ).r;
			clip( componentMask51 - _MaskClipValue );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=7003
-1913;29;1906;990;2614.114;535.113;1;True;False
Node;AmplifyShaderEditor.RangedFloatNode;78;-3286.518,-520.1098;Float;False;Property;_HeightTiling;Height Tiling;8;0;1;0;0;FLOAT
Node;AmplifyShaderEditor.TextureCoordinatesNode;77;-3021.319,-537.0097;Float;False;0;-1;2;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;FLOAT2;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.VertexColorNode;37;-1920,-784;Float;False;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;36;-2016,-1104;Float;True;Property;_Height;Height;7;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;70;-1792,-896;Float;False;Constant;_Float1;Float 1;16;0;1;0;0;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;69;-1744,-640;Float;False;Constant;_Float0;Float 0;16;0;2;0;0;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;91;-1826.813,-30.11383;Float;False;Property;_DetailTiling;Detail Tiling;13;0;1;0;0;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;68;-1568,-768;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.SimpleSubtractOpNode;67;-1600,-912;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;71;-1344,-880;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.TextureCoordinatesNode;90;-1593.212,-74.91388;Float;False;0;-1;2;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;FLOAT2;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;79;-960,-512;Float;True;Property;_DetailAlbedo;Detail Albedo;12;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;81;-960,-592;Float;False;Property;_DetailBrightness;Detail Brightness;11;0;1;0;0;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;74;-1184,-784;Float;False;Property;_HeightContrast;Height Contrast;9;0;1;0;0;FLOAT
Node;AmplifyShaderEditor.ClampOpNode;72;-1136,-912;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;76;-3282.4,-333.6;Float;False;Property;_Tiling;Tiling;6;0;1;0;0;FLOAT
Node;AmplifyShaderEditor.TextureCoordinatesNode;29;-3042.4,-269.6;Float;False;0;-1;2;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;FLOAT2;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;82;-517.912,-532.8127;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.SimpleContrastOpNode;73;-928,-864;Float;False;0;FLOAT;0.0;False;1;COLOR;0,0,0,0;False;COLOR
Node;AmplifyShaderEditor.SamplerNode;1;-960,-320;Float;True;Property;_Albedo;Albedo;0;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.ClampOpNode;83;-288,-544;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;FLOAT
Node;AmplifyShaderEditor.ClampOpNode;75;-656,-880;Float;False;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,0,0,0;False;COLOR
Node;AmplifyShaderEditor.RangedFloatNode;89;-1281,511;Float;False;Property;_DetailNormalIntensity;Detail Normal Intensity;14;0;1;0;0;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;9;-1280,303;Float;False;Property;_NormalIntensity;Normal Intensity;4;0;1;0;0;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;86;-118.912,-630.8127;Float;False;Property;_AlbedoDetail;Albedo Detail;10;0;1;0;1;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;2;-896,160;Float;False;Property;_Metallic;Metallic;2;0;1;0;0;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;84;-27.91199,-457.8127;Float;False;0;FLOAT;0.0;False;1;COLOR;0.0;False;COLOR
Node;AmplifyShaderEditor.SamplerNode;80;-960,448;Float;True;Property;_DetailNormal;Detail Normal;13;0;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;FLOAT3;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;7;-960,256;Float;True;Property;_Normal;Normal;5;0;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;FLOAT3;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;32;-960,-128;Float;True;Property;_RMA;RMA;3;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RoundOpNode;66;-464,-1024;Float;False;0;COLOR;0.0;False;COLOR
Node;AmplifyShaderEditor.RangedFloatNode;13;-896,80;Float;False;Property;_Glossiness;Glossiness;1;0;1;0;0;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;-387,91;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.LerpOp;85;229.088,-552.8127;Float;False;0;COLOR;0.0;False;1;COLOR;0.0,0,0,0;False;2;FLOAT;0.0;False;COLOR
Node;AmplifyShaderEditor.BlendNormalsNode;87;-256,304;Float;False;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;FLOAT3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;33;-384,192;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.ComponentMaskNode;51;-352,-928;Float;False;True;False;False;False;0;COLOR;0,0;False;FLOAT
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;640,-256;Float;False;True;2;Float;ASEMaterialInspector;0;Standard;FlamingSands/Decals/PBR Detail Decal;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;Masked;0.5;True;True;0;False;TransparentCutout;AlphaTest;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;False;0;4;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;Add;Add;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;Relative;0;;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;OBJECT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;13;OBJECT;0.0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False
WireConnection;77;0;78;0
WireConnection;36;1;77;0
WireConnection;68;0;37;1
WireConnection;68;1;69;0
WireConnection;67;0;36;1
WireConnection;67;1;70;0
WireConnection;71;0;67;0
WireConnection;71;1;68;0
WireConnection;90;0;91;0
WireConnection;79;1;90;0
WireConnection;72;0;71;0
WireConnection;29;0;76;0
WireConnection;82;0;81;0
WireConnection;82;1;79;3
WireConnection;73;0;72;0
WireConnection;73;1;74;0
WireConnection;1;1;29;0
WireConnection;83;0;82;0
WireConnection;75;0;73;0
WireConnection;84;0;83;0
WireConnection;84;1;1;0
WireConnection;80;1;90;0
WireConnection;80;5;89;0
WireConnection;7;1;29;0
WireConnection;7;5;9;0
WireConnection;32;1;29;0
WireConnection;66;0;75;0
WireConnection;12;0;32;1
WireConnection;12;1;13;0
WireConnection;85;0;1;0
WireConnection;85;1;84;0
WireConnection;85;2;86;0
WireConnection;87;0;7;0
WireConnection;87;1;80;0
WireConnection;33;0;32;2
WireConnection;33;1;2;0
WireConnection;51;0;66;0
WireConnection;0;0;85;0
WireConnection;0;1;87;0
WireConnection;0;3;33;0
WireConnection;0;4;12;0
WireConnection;0;5;32;4
WireConnection;0;10;51;0
ASEEND*/
//CHKSM=758E937C847A4B41469A74F486823C7D6A4145D7
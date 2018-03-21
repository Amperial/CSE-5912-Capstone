// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "FlamingSands/Transparent/PBR Metallic Masked 2 Colors Wind"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
		_MaskClipValue( "Mask Clip Value", Float ) = 0.55
		_BaseColor("Base Color", Color) = (1,1,1,0)
		_Mask("Mask", 2D) = "white" {}
		_TopColor("Top Color", Color) = (0.3308824,0.5293103,1,0)
		_AlbedoA("Albedo (A)", 2D) = "white" {}
		_RMA("RMA", 2D) = "white" {}
		_MetallnessValue("Metallness Value", Float) = 1
		_Glossiness("Glossiness", Float) = 1
		_NormalIntensity("Normal Intensity", Float) = 1
		_Normal("Normal", 2D) = "bump" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}

		_ShakeTime ("Shake Time", Range (0, 1.0)) = 1.0
		_ShakeWindspeed ("Shake Windspeed", Range (0, 1.0)) = 1.0
		_ShakeBending ("Shake Bending", Range (0, 1.0)) = 1.0
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "AlphaTest+0" }
		Cull Back
		CGPROGRAM
		#include "UnityStandardUtils.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha vertex:vert addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float _NormalIntensity;
		uniform sampler2D _Normal;
		uniform float4 _Normal_ST;
		uniform sampler2D _AlbedoA;
		uniform float4 _AlbedoA_ST;
		uniform float4 _BaseColor;
		uniform sampler2D _Mask;
		uniform float4 _Mask_ST;
		uniform float4 _TopColor;
		uniform sampler2D _RMA;
		uniform float4 _RMA_ST;
		uniform float _MetallnessValue;
		uniform float _Glossiness;
		uniform float _MaskClipValue = 0.55;

		float _ShakeTime;
		float _ShakeWindspeed;
		float _ShakeBending;

		void FastSinCos (float4 val, out float4 s, out float4 c) {
			val = val * 6.408849 - 3.1415927;
			float4 r5 = val * val;
			float4 r6 = r5 * r5;
			float4 r7 = r6 * r5;
			float4 r8 = r6 * r5;
			float4 r1 = r5 * val;
			float4 r2 = r1 * r5;
			float4 r3 = r2 * r5;
			float4 sin7 = {1, -0.16161616, 0.0083333, -0.00019841} ;
			float4 cos8  = {-0.5, 0.041666666, -0.0013888889, 0.000024801587} ;
			s =  val + r1 * sin7.y + r2 * sin7.z + r3 * sin7.w;
			c = 1 + r5 * cos8.x + r6 * cos8.y + r7 * cos8.z + r8 * cos8.w;
		}

		void vert (inout appdata_full v) {
       
			const float _WindSpeed  = (_ShakeWindspeed  +  v.color.g );    
   
			const float4 _waveXSize = float4(0.048, 0.06, 0.24, 0.096);
			const float4 _waveZSize = float4 (0.024, .08, 0.08, 0.2);
			const float4 waveSpeed = float4 (1.2, 2, 1.6, 4.8);
 
			float4 _waveXmove = float4(0.024, 0.04, -0.12, 0.096);
			float4 _waveZmove = float4 (0.006, .02, -0.02, 0.1);
   
			float4 waves;
			waves = v.vertex.x * _waveXSize;
			waves += v.vertex.z * _waveZSize;
 
			waves += _Time.x * (1 - _ShakeTime * 2 - v.color.b ) * waveSpeed *_WindSpeed;
 
			float4 s, c;
			waves = frac (waves);
			FastSinCos (waves, s,c);
 
			//float waveAmount = v.texcoord.y * (v.color.a + _ShakeBending);
			float waveAmount = (v.color.r * _ShakeBending);
			s *= waveAmount;
 
			s *= normalize (waveSpeed);
 
			s = s * s;
			float fade = dot (s, 1.3);
			s = s * s;
			float3 waveMove = float3 (0,0,0);
			waveMove.x = dot (s, _waveXmove);
			waveMove.z = dot (s, _waveZmove);
			v.vertex.xz -= mul ((float3x3)unity_WorldToObject, waveMove).xz;
   
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Normal = i.uv_texcoord * _Normal_ST.xy + _Normal_ST.zw;
			o.Normal = UnpackScaleNormal( tex2D( _Normal,uv_Normal) ,_NormalIntensity );
			float2 uv_AlbedoA = i.uv_texcoord * _AlbedoA_ST.xy + _AlbedoA_ST.zw;
			float4 tex2DNode1 = tex2D( _AlbedoA,uv_AlbedoA);
			float2 uv_Mask = i.uv_texcoord * _Mask_ST.xy + _Mask_ST.zw;
			float4 tex2DNode183 = tex2D( _Mask,uv_Mask);
			o.Albedo = lerp( lerp( tex2DNode1 , ( _BaseColor * tex2DNode1 ) , tex2DNode183.r ) , ( _TopColor * tex2DNode1 ) , tex2DNode183.g ).rgb;
			float2 uv_RMA = i.uv_texcoord * _RMA_ST.xy + _RMA_ST.zw;
			float4 tex2DNode19 = tex2D( _RMA,uv_RMA);
			o.Metallic = clamp( ( tex2DNode19.g * _MetallnessValue ) , 0.0 , 1.0 );
			o.Smoothness = clamp( ( tex2DNode19.r * _Glossiness ) , 0.0 , 0.0 );
			o.Occlusion = tex2DNode19.b;
			o.Alpha = 1;
			clip( tex2DNode1.a - _MaskClipValue );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=7003
-1913;29;1901;1004;2007.671;1097.148;1.487234;True;True
Node;AmplifyShaderEditor.ColorNode;186;-1120,-1008;Float;False;Property;_BaseColor;Base Color;0;0;1,1,1,0;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;1;-1158.5,-416.3001;Float;True;Property;_AlbedoA;Albedo (A);3;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;187;-564.1878,-793.5061;Float;True;0;COLOR;0.0;False;1;COLOR;0;False;COLOR
Node;AmplifyShaderEditor.SamplerNode;183;-1152,-672;Float;True;Property;_Mask;Mask;1;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.ColorNode;185;-1120,-849;Float;False;Property;_TopColor;Top Color;2;0;0.3308824,0.5293103,1,0;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;13;-640,-272;Float;False;Property;_Glossiness;Glossiness;6;0;1;0;0;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;2;-640,-32;Float;False;Property;_MetallnessValue;Metallness Value;5;0;1;0;0;FLOAT
Node;AmplifyShaderEditor.SamplerNode;19;-1158.5,-144.3;Float;True;Property;_RMA;RMA;4;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.LerpOp;188;-309.2297,-648.9235;Float;True;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0.0;False;COLOR
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;189;-262.5575,-895.0348;Float;True;0;COLOR;0,0,0,0;False;1;COLOR;0.0;False;COLOR
Node;AmplifyShaderEditor.RangedFloatNode;9;-1540.5,153.6992;Float;False;Property;_NormalIntensity;Normal Intensity;7;0;1;0;0;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;-384,-48;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0,0,0,0;False;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;-384,-288;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.ClampOpNode;182;-192,-48;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;FLOAT
Node;AmplifyShaderEditor.ClampOpNode;181;-192,-304;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.LerpOp;190;-16.28086,-660.8214;Float;True;0;COLOR;0.0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0.0;False;COLOR
Node;AmplifyShaderEditor.SamplerNode;7;-1152,128;Float;True;Property;_Normal;Normal;8;0;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;FLOAT3;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;128,0;Float;False;True;2;Float;ASEMaterialInspector;0;Standard;FlamingSands/Transparent/PBR Metallic Masked 2 Colors Wind;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;Masked;0.55;True;True;0;False;TransparentCutout;AlphaTest;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;False;0;4;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;Add;Add;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;Relative;0;;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;OBJECT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;13;OBJECT;0.0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False
WireConnection;187;0;186;0
WireConnection;187;1;1;0
WireConnection;188;0;1;0
WireConnection;188;1;187;0
WireConnection;188;2;183;1
WireConnection;189;0;185;0
WireConnection;189;1;1;0
WireConnection;6;0;19;2
WireConnection;6;1;2;0
WireConnection;12;0;19;1
WireConnection;12;1;13;0
WireConnection;182;0;6;0
WireConnection;181;0;12;0
WireConnection;190;0;188;0
WireConnection;190;1;189;0
WireConnection;190;2;183;2
WireConnection;7;5;9;0
WireConnection;0;0;190;0
WireConnection;0;1;7;0
WireConnection;0;3;182;0
WireConnection;0;4;181;0
WireConnection;0;5;19;3
WireConnection;0;10;1;4
ASEEND*/
//CHKSM=1CD0C65B3FC8D2348F16E511C34099E975A339A6
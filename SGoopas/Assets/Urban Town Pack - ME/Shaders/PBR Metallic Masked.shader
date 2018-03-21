// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "FlamingSands/Transparent/PBR Metallic Masked"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
		_MaskClipValue( "Mask Clip Value", Float ) = 0.21
		_AlbedoA("Albedo (A)", 2D) = "white" {}
		_RMA("RMA", 2D) = "white" {}
		_MetallnessValue("Metallness Value", Float) = 1
		_Glossiness("Glossiness", Float) = 1
		_NormalIntensity("Normal Intensity", Float) = 1
		_Normal("Normal", 2D) = "bump" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "AlphaTest+0" }
		Cull Back
		CGPROGRAM
		#include "UnityStandardUtils.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float _NormalIntensity;
		uniform sampler2D _Normal;
		uniform float4 _Normal_ST;
		uniform sampler2D _AlbedoA;
		uniform float4 _AlbedoA_ST;
		uniform sampler2D _RMA;
		uniform float4 _RMA_ST;
		uniform float _MetallnessValue;
		uniform float _Glossiness;
		uniform float _MaskClipValue = 0.21;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_Normal = i.uv_texcoord * _Normal_ST.xy + _Normal_ST.zw;
			o.Normal = UnpackScaleNormal( tex2D( _Normal,uv_Normal) ,_NormalIntensity );
			float2 uv_AlbedoA = i.uv_texcoord * _AlbedoA_ST.xy + _AlbedoA_ST.zw;
			float4 tex2DNode1 = tex2D( _AlbedoA,uv_AlbedoA);
			o.Albedo = tex2DNode1.rgb;
			float2 uv_RMA = i.uv_texcoord * _RMA_ST.xy + _RMA_ST.zw;
			float4 tex2DNode19 = tex2D( _RMA,uv_RMA);
			o.Metallic = clamp( ( tex2DNode19.g * _MetallnessValue ) , 0.0 , 1.0 );
			o.Smoothness = clamp( ( tex2DNode19.r * _Glossiness ) , 0.0 , 1.0 );
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
7;29;1906;1004;1595.241;516.6509;1;True;False
Node;AmplifyShaderEditor.SamplerNode;19;-1158.5,-144.3;Float;True;Property;_RMA;RMA;2;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;13;-640,-272;Float;False;Property;_Glossiness;Glossiness;4;0;1;0;0;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;2;-698,-16;Float;False;Property;_MetallnessValue;Metallness Value;3;0;1;0;0;FLOAT
Node;AmplifyShaderEditor.CommentaryNode;164;-5241.5,3188.7;Float;False;2028.701;631;Random wind;16;148;149;150;151;152;153;154;156;157;158;159;155;160;161;162;163;Random Wind
Node;AmplifyShaderEditor.CommentaryNode;132;-7513.5,3636.7;Float;False;873;428;Turn into scalar wave and apply it to all axis;5;127;128;130;129;131;
Node;AmplifyShaderEditor.CommentaryNode;136;-6569.5,3668.7;Float;False;301.499;320.4955;Scale wave by wind strength;2;133;134;
Node;AmplifyShaderEditor.CommentaryNode;126;-7769.5,3588.7;Float;False;204;160;make a wave;1;125;
Node;AmplifyShaderEditor.CommentaryNode;116;-10425.5,3556.7;Float;False;823.503;376.6082;Time Is Multiplied By Windspeed then -1;6;88;90;95;96;97;94;
Node;AmplifyShaderEditor.CommentaryNode;118;-9641.504,4276.701;Float;False;448.9012;191.3003;Getting the lengh of wind direction (Gives strengh);2;102;101;
Node;AmplifyShaderEditor.CommentaryNode;117;-10025.5,4020.7;Float;False;471.7996;171.2026;Noralizing Wind Direction (Takes Strenth Out);2;100;99;
Node;AmplifyShaderEditor.CommentaryNode;119;-10121.5,3124.7;Float;False;611.765;297.4124;World position is devided by a number to get the size of the wave;3;93;91;92;
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;-384,-48;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0,0,0,0;False;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;9;-1540.5,153.6992;Float;False;Property;_NormalIntensity;Normal Intensity;5;0;1;0;0;FLOAT
Node;AmplifyShaderEditor.CommentaryNode;121;-8969.504,3620.7;Float;False;236;179;Pan Time Through space;1;120;
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;-384,-288;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.CommentaryNode;137;-6137.5,3700.7;Float;False;219;183;Amplitude;1;135;
Node;AmplifyShaderEditor.CommentaryNode;123;-8537.502,3412.7;Float;False;423.9999;160;Cancel out waves not in line with wind direction;1;122;
Node;AmplifyShaderEditor.SimpleAddOpNode;155;-4409.5,3684.7;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;153;-4153.5,3396.7;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;150;-4841.5,3460.7;Float;False;0;FLOAT3;0.0;False;1;FLOAT;0,0,0;False;FLOAT3
Node;AmplifyShaderEditor.WorldPosInputsNode;61;-11558.74,4211.752;Float;False;FLOAT3;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;146;-5433.5,3156.7;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.SimpleTimeNode;154;-4729.5,3700.7;Float;False;0;FLOAT;1.0;False;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;82;-11033.5,3972.7;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.Vector4Node;98;-10601.5,4036.7;Float;False;Property;_WindDirectionAndSpeed;WindDirectionAndSpeed;7;0;1,0,1,1;FLOAT4;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;130;-7017.5,3796.7;Float;False;0;FLOAT;0.0,0,0;False;1;FLOAT3;0.0;False;FLOAT3
Node;AmplifyShaderEditor.VertexColorNode;139;-7769.5,2468.7;Float;False;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;134;-6521.5,3876.7;Float;False;Property;_FineWaveStrength;FineWaveStrength;7;0;0.5;0;0;FLOAT
Node;AmplifyShaderEditor.ToggleSwitchNode;144;-4249.5,4004.7;Float;False;Property;_LOD;LOD?;9;0;0;0;FLOAT;0.0;False;1;FLOAT3;0.0;False;FLOAT3
Node;AmplifyShaderEditor.SimpleDivideOpNode;93;-9785.504,3252.7;Float;False;0;FLOAT3;0.0;False;1;FLOAT;0.0,0,0;False;FLOAT3
Node;AmplifyShaderEditor.WireNode;168;-3865.5,2900.7;Float;False;0;FLOAT4;0.0;False;FLOAT4
Node;AmplifyShaderEditor.RangedFloatNode;143;-5161.5,4292.701;Float;False;Property;_WPOWindRippleStrength;WPO Wind Ripple Strength;8;0;1;0;0;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;163;-3369.5,3300.7;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.WorldPosInputsNode;148;-5177.5,3380.7;Float;False;FLOAT3;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.Vector3Node;129;-7257.5,3876.7;Float;False;Constant;_Vector2;Vector 2;7;0;1,1,1;FLOAT3;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleDivideOpNode;124;-7961.5,3636.7;Float;False;0;FLOAT3;0.0;False;1;FLOAT;0.0,0,0;False;FLOAT3
Node;AmplifyShaderEditor.SinOpNode;159;-3897.5,3684.7;Float;False;0;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;147;-5817.5,3236.7;Float;False;Property;_WPO_SwayOffsetAmount;WPO_SwayOffsetAmount;9;0;0;0;0;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;88;-10361.5,3700.7;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;135;-6089.5,3764.7;Float;False;0;FLOAT3;0.0;False;1;FLOAT;0,0,0;False;FLOAT3
Node;AmplifyShaderEditor.RangedFloatNode;149;-5193.5,3604.7;Float;False;Constant;_Float11;Float 11;9;0;0.015;0;0;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;92;-10073.5,3348.7;Float;False;Constant;_Float4;Float 4;7;0;128;0;0;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;113;-8953.504,4724.702;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.ComponentMaskNode;84;-11257.5,4052.7;Float;False;False;False;True;False;0;FLOAT3;0,0,0;False;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;112;-9193.504,4676.701;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;141;-5033.5,4100.7;Float;False;0;FLOAT3;0.0;False;1;FLOAT;0,0,0;False;FLOAT3
Node;AmplifyShaderEditor.SinOpNode;125;-7721.5,3636.7;Float;False;0;FLOAT3;0.0;False;FLOAT3
Node;AmplifyShaderEditor.SimpleAddOpNode;131;-6793.5,3684.7;Float;False;0;FLOAT3;0,0,0;False;1;FLOAT3;0.0;False;FLOAT3
Node;AmplifyShaderEditor.SimpleAddOpNode;85;-10649.5,3844.7;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;138;-5673.5,3812.7;Float;False;0;FLOAT;0.0;False;1;FLOAT3;0;False;FLOAT3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;133;-6425.5,3716.7;Float;False;0;FLOAT3;0.0;False;1;FLOAT;0.0,0,0;False;FLOAT3
Node;AmplifyShaderEditor.RangedFloatNode;127;-7465.5,3860.7;Float;False;Constant;_Float10;Float 10;7;0;0;0;0;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;101;-9593.504,4356.701;Float;False;Constant;_Float6;Float 6;7;0;0;0;0;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;77;-12089.5,4036.7;Float;False;Constant;_Float1;Float 1;8;0;128;0;0;FLOAT
Node;AmplifyShaderEditor.WorldPosInputsNode;91;-10057.5,3172.7;Float;False;FLOAT3;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;89;-10585.5,3396.7;Float;False;Constant;_FineWaveSizeInverse;FineWaveSize Inverse;7;0;1;0;0;FLOAT
Node;AmplifyShaderEditor.OneMinusNode;111;-9481.504,4692.701;Float;False;0;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;145;-4569.5,3988.7;Float;False;Constant;_Float3;Float 3;9;0;0;0;0;FLOAT
Node;AmplifyShaderEditor.ComponentMaskNode;151;-4537.5,3396.7;Float;False;True;False;False;False;0;FLOAT3;0,0,0,0;False;FLOAT
Node;AmplifyShaderEditor.OneMinusNode;115;-8729.502,4708.702;Float;False;0;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;122;-8473.501,3460.7;Float;False;0;FLOAT;0.0,0,0;False;1;FLOAT3;0.0;False;FLOAT3
Node;AmplifyShaderEditor.NormalizeNode;100;-9737.504,4068.7;Float;False;0;FLOAT;0,0,0,0;False;FLOAT
Node;AmplifyShaderEditor.SimpleDivideOpNode;78;-11849.5,3940.7;Float;False;0;FLOAT3;0.0;False;1;FLOAT;0.0,0,0;False;FLOAT3
Node;AmplifyShaderEditor.SinOpNode;157;-3897.5,3396.7;Float;False;0;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;103;-9481.504,3764.7;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;162;-3609.5,3236.7;Float;False;Constant;_Float12;Float 12;9;0;0.7;0;0;FLOAT
Node;AmplifyShaderEditor.DistanceOpNode;102;-9369.504,4324.701;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.DistanceOpNode;128;-7257.5,3732.7;Float;False;0;FLOAT3;0.0;False;1;FLOAT;0.0,0,0;False;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;81;-11417.5,3972.7;Float;False;0;FLOAT3;0.0;False;1;FLOAT;0.0,0,0;False;FLOAT3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;142;-4537.5,4116.701;Float;False;0;FLOAT3;0.0;False;1;FLOAT;0.0,0,0;False;FLOAT3
Node;AmplifyShaderEditor.ComponentMaskNode;152;-4537.5,3556.7;Float;False;False;False;True;False;0;FLOAT3;0,0,0,0;False;FLOAT
Node;AmplifyShaderEditor.ComponentMaskNode;99;-9977.504,4084.7;Float;False;True;True;True;False;0;FLOAT;0,0,0,0;False;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;108;-10137.5,4676.701;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.ComponentMaskNode;166;-3568.2,2972.9;Float;False;False;False;False;True;0;FLOAT4;0,0,0,0;False;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;107;-10361.5,4708.702;Float;False;Constant;_Float8;Float 8;7;0;0.8;0;0;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;97;-9769.504,3748.7;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.VertexColorNode;53;-11193.5,3556.7;Float;False;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;110;-9689.504,4692.701;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.SinOpNode;158;-3897.5,3556.7;Float;False;0;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.ClampOpNode;109;-9897.504,4692.701;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;80;-11641.5,4052.7;Float;False;Constant;_Float2;Float 2;8;0;0;0;0;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;104;-9209.504,3764.7;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;114;-9177.504,4836.702;Float;False;Constant;_Float9;Float 9;7;0;0.5;0;0;FLOAT
Node;AmplifyShaderEditor.ComponentMaskNode;94;-10297.5,3812.7;Float;False;False;False;False;True;0;FLOAT;0,0,0,0;False;FLOAT
Node;AmplifyShaderEditor.SinOpNode;106;-10345.5,4612.701;Float;False;0;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;105;-9417.504,3908.7;Float;False;Constant;_Float7;Float 7;7;0;0.5;0;0;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;96;-10009.5,3812.7;Float;False;Constant;_Float5;Float 5;7;0;-1;0;0;FLOAT
Node;AmplifyShaderEditor.SimpleTimeNode;56;-11129.5,3812.7;Float;False;0;FLOAT;1.0;False;FLOAT
Node;AmplifyShaderEditor.RangedFloatNode;86;-11177.5,3476.7;Float;False;Constant;_Float0;Float 0;7;0;256;0;0;FLOAT
Node;AmplifyShaderEditor.AppendNode;175;-1789.93,2561.547;Float;False;FLOAT4;0;0;0;0;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;0.0;False;3;FLOAT;0.0;False;FLOAT4
Node;AmplifyShaderEditor.RangedFloatNode;178;-1859.827,2809.447;Float;False;Property;_WPOWindStrength;WPO Wind Strength;10;0;0.25;0;0;FLOAT
Node;AmplifyShaderEditor.ComponentMaskNode;177;-2028.929,2558.547;Float;False;True;True;True;True;0;FLOAT3;0,0,0,0;False;FLOAT3
Node;AmplifyShaderEditor.ComponentMaskNode;83;-11257.5,3924.7;Float;False;True;False;False;False;0;FLOAT3;0,0,0,0;False;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;95;-9993.504,3700.7;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;173;-2542.728,2730.747;Float;False;0;FLOAT3;0.0;False;1;FLOAT;0.0,0,0;False;FLOAT3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;160;-3609.5,3396.7;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;156;-4153.5,3556.7;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;161;-3609.5,3556.7;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;172;-2559.724,2498.647;Float;False;0;FLOAT;0,0,0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.SimpleAddOpNode;174;-2229.228,2578.346;Float;False;0;FLOAT;0.0,0,0;False;1;FLOAT3;0;False;FLOAT3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;90;-10217.5,3604.7;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.CustomExpressionNode;167;-3133.917,2612.748;Float;False;float angle = b;1;1;True;In0;FLOAT;0.0;0;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;169;-2607.8,2900.199;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;171;-2383.8,2948.199;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.ComponentMaskNode;165;-3568.2,2812.901;Float;False;True;True;True;False;0;FLOAT4;0,0,0,0;False;FLOAT3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;179;-1553.827,2710.447;Float;False;0;FLOAT4;0.0;False;1;FLOAT;0.0,0,0,0;False;FLOAT4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;87;-10905.5,3524.7;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;FLOAT
Node;AmplifyShaderEditor.FractNode;79;-11625.5,3908.7;Float;False;0;FLOAT3;0.0;False;FLOAT3
Node;AmplifyShaderEditor.SimpleAddOpNode;120;-8921.504,3684.7;Float;False;0;FLOAT3;0.0;False;1;FLOAT;0.0,0,0;False;FLOAT3
Node;AmplifyShaderEditor.RangedFloatNode;170;-2673.719,3029.748;Float;False;Constant;_Float13;Float 13;10;0;0.075;0;0;FLOAT
Node;AmplifyShaderEditor.SamplerNode;1;-1158.5,-416.3001;Float;True;Property;_AlbedoA;Albedo (A);1;0;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;COLOR;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.SamplerNode;7;-1142.5,127.6998;Float;True;Property;_Normal;Normal;6;0;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;1.0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1.0;False;FLOAT3;FLOAT;FLOAT;FLOAT;FLOAT
Node;AmplifyShaderEditor.ClampOpNode;182;-192,-48;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;FLOAT
Node;AmplifyShaderEditor.ClampOpNode;181;-192,-304;Float;False;0;FLOAT;0.0;False;1;FLOAT;0.0;False;2;FLOAT;1.0;False;FLOAT
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;128,0;Float;False;True;2;Float;ASEMaterialInspector;0;Standard;FlamingSands/Transparent/PBR Metallic Masked;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;Masked;0.21;True;True;0;False;TransparentCutout;AlphaTest;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;False;0;4;10;25;False;0.5;True;0;Zero;Zero;0;Zero;Zero;Add;Add;0;False;0;0,0,0,0;VertexOffset;False;Cylindrical;Relative;0;;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0.0;False;4;FLOAT;0.0;False;5;FLOAT;0.0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0.0;False;9;FLOAT;0.0;False;10;OBJECT;0.0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;13;OBJECT;0.0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False
WireConnection;6;0;19;2
WireConnection;6;1;2;0
WireConnection;12;0;19;1
WireConnection;12;1;13;0
WireConnection;155;0;146;0
WireConnection;155;1;154;0
WireConnection;153;0;151;0
WireConnection;153;1;155;0
WireConnection;150;0;148;0
WireConnection;150;1;149;0
WireConnection;146;0;139;2
WireConnection;146;1;147;0
WireConnection;82;0;83;0
WireConnection;82;1;84;0
WireConnection;130;0;128;0
WireConnection;130;1;129;0
WireConnection;144;0;145;0
WireConnection;144;1;142;0
WireConnection;93;0;91;0
WireConnection;93;1;92;0
WireConnection;168;0;98;0
WireConnection;163;0;162;0
WireConnection;163;1;160;0
WireConnection;124;0;122;0
WireConnection;124;1;89;0
WireConnection;159;0;155;0
WireConnection;88;0;87;0
WireConnection;88;1;85;0
WireConnection;135;0;133;0
WireConnection;135;1;134;0
WireConnection;113;0;112;0
WireConnection;113;1;114;0
WireConnection;84;0;81;0
WireConnection;112;0;111;0
WireConnection;112;1;111;0
WireConnection;141;0;138;0
WireConnection;141;1;115;0
WireConnection;125;0;124;0
WireConnection;131;0;125;0
WireConnection;131;1;130;0
WireConnection;85;0;56;0
WireConnection;85;1;82;0
WireConnection;138;0;139;3
WireConnection;138;1;135;0
WireConnection;133;0;131;0
WireConnection;133;1;102;0
WireConnection;111;0;110;0
WireConnection;151;0;150;0
WireConnection;115;0;113;0
WireConnection;122;0;100;0
WireConnection;122;1;120;0
WireConnection;100;0;99;0
WireConnection;78;0;61;0
WireConnection;78;1;77;0
WireConnection;157;0;153;0
WireConnection;103;0;97;0
WireConnection;103;1;100;0
WireConnection;102;0;99;0
WireConnection;102;1;101;0
WireConnection;128;0;125;0
WireConnection;128;1;127;0
WireConnection;81;0;79;0
WireConnection;81;1;80;0
WireConnection;142;0;141;0
WireConnection;142;1;143;0
WireConnection;152;0;150;0
WireConnection;99;0;98;4
WireConnection;108;0;106;0
WireConnection;108;1;107;0
WireConnection;166;0;168;0
WireConnection;97;0;95;0
WireConnection;97;1;96;0
WireConnection;110;0;109;0
WireConnection;110;1;109;0
WireConnection;158;0;156;0
WireConnection;109;0;108;0
WireConnection;104;0;103;0
WireConnection;104;1;105;0
WireConnection;94;0;98;4
WireConnection;106;0;82;0
WireConnection;175;0;177;0
WireConnection;175;1;177;0
WireConnection;175;2;177;0
WireConnection;175;3;171;0
WireConnection;177;0;174;0
WireConnection;83;0;81;0
WireConnection;95;0;90;0
WireConnection;95;1;94;0
WireConnection;173;0;165;0
WireConnection;173;1;161;0
WireConnection;160;0;157;0
WireConnection;160;1;159;0
WireConnection;156;0;152;0
WireConnection;156;1;155;0
WireConnection;161;0;158;0
WireConnection;161;1;159;0
WireConnection;172;0;167;0
WireConnection;172;1;163;0
WireConnection;174;0;172;0
WireConnection;174;1;173;0
WireConnection;90;0;89;0
WireConnection;90;1;88;0
WireConnection;167;0;165;0
WireConnection;169;0;166;0
WireConnection;169;1;139;1
WireConnection;171;0;169;0
WireConnection;171;1;170;0
WireConnection;165;0;168;0
WireConnection;179;0;175;0
WireConnection;179;1;178;0
WireConnection;87;0;86;0
WireConnection;87;1;53;2
WireConnection;79;0;78;0
WireConnection;120;0;93;0
WireConnection;120;1;104;0
WireConnection;7;5;9;0
WireConnection;182;0;6;0
WireConnection;181;0;12;0
WireConnection;0;0;1;0
WireConnection;0;1;7;0
WireConnection;0;3;182;0
WireConnection;0;4;181;0
WireConnection;0;5;19;3
WireConnection;0;10;1;4
ASEEND*/
//CHKSM=223A10B0572658722435BE744A00FA01464ED7CB
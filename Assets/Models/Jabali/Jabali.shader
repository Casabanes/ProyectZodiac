// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Jabali"
{
	Properties
	{
		_jabali_Standardmaterial_2_AlbedoTransparency("jabali_Standardmaterial_2_AlbedoTransparency", 2D) = "white" {}
		_jabali_Standardmaterial_2_Normal("jabali_Standardmaterial_2_Normal", 2D) = "bump" {}
		_redLevel("_redLevel", Range( 0 , 0.8)) = 4.38
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
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

		uniform sampler2D _jabali_Standardmaterial_2_Normal;
		uniform float4 _jabali_Standardmaterial_2_Normal_ST;
		uniform sampler2D _jabali_Standardmaterial_2_AlbedoTransparency;
		uniform float4 _jabali_Standardmaterial_2_AlbedoTransparency_ST;
		uniform float _redLevel;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_jabali_Standardmaterial_2_Normal = i.uv_texcoord * _jabali_Standardmaterial_2_Normal_ST.xy + _jabali_Standardmaterial_2_Normal_ST.zw;
			o.Normal = UnpackNormal( tex2D( _jabali_Standardmaterial_2_Normal, uv_jabali_Standardmaterial_2_Normal ) );
			float2 uv_jabali_Standardmaterial_2_AlbedoTransparency = i.uv_texcoord * _jabali_Standardmaterial_2_AlbedoTransparency_ST.xy + _jabali_Standardmaterial_2_AlbedoTransparency_ST.zw;
			float4 tex2DNode1 = tex2D( _jabali_Standardmaterial_2_AlbedoTransparency, uv_jabali_Standardmaterial_2_AlbedoTransparency );
			float4 color15 = IsGammaSpace() ? float4(1,0.6650944,0.9149241,0) : float4(1,0.3998791,0.8172685,0);
			float4 color13 = IsGammaSpace() ? float4(0.9150943,0.9150943,0.9150943,0) : float4(0.8176128,0.8176128,0.8176128,0);
			float4 lerpResult14 = lerp( tex2DNode1 , color15 , color13);
			float4 color34 = IsGammaSpace() ? float4(1,0,0,0.07058824) : float4(1,0,0,0.07058824);
			float4 lerpResult35 = lerp( lerpResult14 , color34 , _redLevel);
			o.Albedo = lerpResult35.rgb;
			float4 color19 = IsGammaSpace() ? float4(1,1,1,0) : float4(1,1,1,0);
			float4 lerpResult16 = lerp( color19 , float4( 0,0,0,0 ) , tex2DNode1);
			o.Smoothness = ( lerpResult16 * 0.5 ).r;
			o.Occlusion = ( tex2DNode1.r + 0.0 );
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18900
339;487;1106;443;441.374;353.0418;1;True;False
Node;AmplifyShaderEditor.SamplerNode;1;-1204.03,-79.62418;Inherit;True;Property;_jabali_Standardmaterial_2_AlbedoTransparency;jabali_Standardmaterial_2_AlbedoTransparency;0;0;Create;True;0;0;0;False;0;False;-1;25d022cfdcc6f96419085cc91d2b5dad;25d022cfdcc6f96419085cc91d2b5dad;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;19;-530.1593,413.1193;Inherit;False;Constant;_Color2;Color 2;3;0;Create;True;0;0;0;False;0;False;1,1,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;13;-742.7482,-278.8201;Inherit;False;Constant;_Color0;Color 0;3;0;Create;True;0;0;0;False;0;False;0.9150943,0.9150943,0.9150943,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;15;-424.2437,-368.1729;Inherit;False;Constant;_Color1;Color 1;3;0;Create;True;0;0;0;False;0;False;1,0.6650944,0.9149241,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;22;87.8407,418.1193;Inherit;False;Constant;_Float0;Float 0;3;0;Create;True;0;0;0;False;0;False;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;14;-245.6611,-79.72168;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;16;-231.5575,272.3581;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;34;-160.9799,-361.664;Inherit;False;Constant;_Color3;Color 3;3;0;Create;True;0;0;0;False;0;False;1,0,0,0.07058824;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;37;145.4201,-289.6638;Inherit;False;Property;_redLevel;_redLevel;3;0;Create;True;0;0;0;False;0;False;4.38;0;0;0.8;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;3;-1187.451,343.5868;Inherit;True;Property;_jabali_Standardmaterial_2_Normal;jabali_Standardmaterial_2_Normal;2;0;Create;True;0;0;0;False;0;False;-1;23249eea1b777f645ab412f366dfee57;23249eea1b777f645ab412f366dfee57;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-1203.434,127.1867;Inherit;True;Property;_jabali_Standardmaterial_2_MetallicSmoothness;jabali_Standardmaterial_2_MetallicSmoothness;1;0;Create;True;0;0;0;False;0;False;-1;1f23673e177434649a374d4f4892df2d;1f23673e177434649a374d4f4892df2d;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;21;120.8407,260.1193;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;27;-708.3223,91.52634;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;35;363.8203,-168.0638;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;664.9389,-37.26459;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Jabali;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;14;0;1;0
WireConnection;14;1;15;0
WireConnection;14;2;13;0
WireConnection;16;0;19;0
WireConnection;16;2;1;0
WireConnection;21;0;16;0
WireConnection;21;1;22;0
WireConnection;27;0;1;1
WireConnection;35;0;14;0
WireConnection;35;1;34;0
WireConnection;35;2;37;0
WireConnection;0;0;35;0
WireConnection;0;1;3;0
WireConnection;0;4;21;0
WireConnection;0;5;27;0
ASEEND*/
//CHKSM=1C9627B215C919D73219130A3CDB223FCE415BE7
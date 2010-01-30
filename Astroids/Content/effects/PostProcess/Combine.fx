
sampler tex0 : register(s0);
sampler tex1 : register(s1);

float saturation0;
float saturation1;
float intensity0;
float intensity1;

// Helper for modifying the saturation of a color.
float4 AdjustSaturation(float4 color, float saturation)
{
    // The constants 0.3, 0.59, and 0.11 are chosen because the
    // human eye is more sensitive to green light, and less to blue.
    float grey = dot(color, float3(0.3, 0.59, 0.11));

    return lerp(grey, color, saturation);
}

struct InputData
{
    float2 texCoord : TEXCOORD0;
};

float4 PixelShaderFunction(InputData input) : COLOR0
{
    float4 color0 = tex2D( tex0, input.texCoord );
    float4 color1 = tex2D( tex1, input.texCoord );
    
    color0 = AdjustSaturation( color0, saturation0 ) * intensity0;
    color1 = AdjustSaturation( color1, saturation1 ) * intensity1;

	color0 *= (1.0-saturate(color1));
	
    return (color0+color1);
}

technique Combine
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}

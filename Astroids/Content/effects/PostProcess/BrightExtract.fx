sampler texture0 : register(s0);

struct InputData
{
    float2 texCoord : TEXCOORD0;
};

float BrightnessThreshold;

float4 PixelShaderFunction(InputData input) : COLOR0
{
	float4 color = tex2D( texture0, input.texCoord );
	
    return saturate( (color - BrightnessThreshold) / ( 1.0f - BrightnessThreshold ) );
}

technique BrightExtract
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}

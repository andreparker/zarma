sampler tex0 : register(s0);

float2 scrollVector;

struct PixelShaderInput
{
	float4 color    : COLOR0;
    float2 texCoord : TEXCOORD0;    
};


float4 PixelShaderFunction(PixelShaderInput input) : COLOR0
{
	
	float2 texCoord = input.texCoord + scrollVector;
	
	float4 color = tex2D( tex0, texCoord ) * input.color;
	
    return color;
}

technique TextureScroll
{
    pass Pass1
    {
        // TODO: set renderstates here.

        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}

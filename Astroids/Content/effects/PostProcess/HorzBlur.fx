sampler tex0 : register(s0);

struct InputData
{
    float2 texCoord : TEXCOORD0;
};

float weights[9];
float scale = 0.125;

float4 PixelShaderFunction(InputData input) : COLOR0
{
	float2 texCoords = input.texCoord;
	
	float4 c = tex2D( tex0, texCoords ) * weights[4];
	
	c += tex2D( tex0, float2(texCoords.x + -0.001,texCoords.y ) ) * weights[3];
	c += tex2D( tex0, float2(texCoords.x + -0.002,texCoords.y ) ) * weights[2];
	c += tex2D( tex0, float2(texCoords.x + -0.003,texCoords.y ) ) * weights[1];
	c += tex2D( tex0, float2(texCoords.x + -0.004,texCoords.y ) ) * weights[0];
																 
																 
	c += tex2D( tex0, float2(texCoords.x + 0.001,texCoords.y ) ) * weights[5];
	c += tex2D( tex0, float2(texCoords.x + 0.002,texCoords.y ) ) * weights[6];
	c += tex2D( tex0, float2(texCoords.x + 0.003,texCoords.y ) ) * weights[7];
	c += tex2D( tex0, float2(texCoords.x + 0.004,texCoords.y ) ) * weights[8];
	
    return c;
}

technique HorzBlur
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}

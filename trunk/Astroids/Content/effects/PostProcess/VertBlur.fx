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
	
	c += tex2D( tex0, float2(texCoords.x,texCoords.y + -0.001) ) * weights[3];
	c += tex2D( tex0, float2(texCoords.x,texCoords.y + -0.002) ) * weights[2];
	c += tex2D( tex0, float2(texCoords.x,texCoords.y + -0.003) ) * weights[1];
	c += tex2D( tex0, float2(texCoords.x,texCoords.y + -0.004) ) * weights[0];
																
																
	c += tex2D( tex0, float2(texCoords.x,texCoords.y + 0.001) ) * weights[5];
	c += tex2D( tex0, float2(texCoords.x,texCoords.y + 0.002) ) * weights[6];
	c += tex2D( tex0, float2(texCoords.x,texCoords.y + 0.003) ) * weights[7];
	c += tex2D( tex0, float2(texCoords.x,texCoords.y + 0.004) ) * weights[8];
	
	
    return c;
}

technique VertBlur
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}

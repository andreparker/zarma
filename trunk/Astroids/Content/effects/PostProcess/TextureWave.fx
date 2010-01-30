
sampler texture0 : register(s0);

// time used to wave texture coords
float time;

struct InputData
{
    float2 texCoord : TEXCOORD0;
};


float4 PixelShaderFunction(InputData input) : COLOR0
{
    // TODO: add your pixel shader code here.
    float2 texCoords = input.texCoord;
    
    texCoords.x +=  cos( time + texCoords.x * 10 ) * 0.01;
    texCoords.y +=  sin( time + texCoords.y * 10 ) * 0.01;
    
    float4 color = tex2D( texture0, texCoords );
    
    return color;
}

technique Wave
{
    pass Pass1
    {
        // TODO: set renderstates here.
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}

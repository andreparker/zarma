using System;

namespace Asteroids.Components.Math
{
    using Microsoft.Xna.Framework;
    public struct MathUtil
    {
        public static float ComputeGaussian( float pos, float theta )
        {
            return (float)((1.0 / System.Math.Sqrt(2 * System.Math.PI * theta)) *
                           System.Math.Exp(-(pos * pos) / (2 * theta * theta)));
        }

        public static void ComputeGaussianTable( ref float[] table, int tableSize, float theta )
        {
            float totalWeight = 0.0f;
            for( int i = -(tableSize/2); i < (tableSize/2) + 1; ++i )
            {
                float weight = ComputeGaussian((float)i, theta);
                table[i + (tableSize / 2)] = weight;
                totalWeight += weight;
            }

            for( int i = 0; i < tableSize; ++i )
            {
                table[i] /= totalWeight;
            }
        }
    }
}

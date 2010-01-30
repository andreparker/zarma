using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids.Components.PostProcessing.PostEffects
{
    using Microsoft.Xna.Framework.Graphics;
    using Components.Math;

    class HorzBlurPostEffect : PostProcessEffect
    {
        Effect effect;
        RenderTarget2D surface;
        private readonly int numWeights = 9;
        float intensity = 16.0f;

        float[] weights = null;

        public HorzBlurPostEffect(GraphicsDevice device_, PostProcessManager manager_)
            : base(device_, manager_)
        {
            weights = new float[numWeights];
        }

        private void InitializeWeights()
        {
            MathUtil.ComputeGaussianTable(ref weights, numWeights, intensity);
        }

        public override void Initialize()
        {
            PresentationParameters present = GraphicsDevice.PresentationParameters;

            // down sample for performance
            int w = (int)(present.BackBufferWidth * 0.5f);
            int h = (int)(present.BackBufferHeight * 0.5f);

            surface = new RenderTarget2D(GraphicsDevice, w, h, 1, present.BackBufferFormat);

            effect = Manager.Content.Load<Effect>("HorzBlur");
            InitializeWeights();

            effect.Parameters["weights"].SetValue(weights);
        }

        public override Texture2D Process(Texture2D baseTexture_, Texture2D inputTexture_, float tick)
        {
            effect.CurrentTechnique = effect.Techniques["HorzBlur"];
            Draw_Base(effect, surface, inputTexture_, tick);

            return surface.GetTexture();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids.Components.PostProcessing.PostEffects
{
    using Microsoft.Xna.Framework.Graphics;
    using Components.Math;
    class VertBlurPostEffect : PostProcessEffect
    {
        RenderTarget2D surface;
        Effect effect;
        private readonly int numWeights = 9;
        private float intensity = 16.0f;

        float[] weights = null;

        public VertBlurPostEffect(GraphicsDevice device_, PostProcessManager manager_)
            : base(device_,manager_)
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

            int w = (int)(present.BackBufferWidth * 0.5f);
            int h = (int)(present.BackBufferHeight * 0.5f);

            surface = new RenderTarget2D(GraphicsDevice, w, h, 1, present.BackBufferFormat);

            effect = Manager.Content.Load<Effect>("VertBlur");
            InitializeWeights();
            effect.Parameters["weights"].SetValue(weights);
        }

        public override Texture2D Process(Texture2D baseTexture_, Texture2D inputTexture_, float tick)
        {
            effect.CurrentTechnique = effect.Techniques["VertBlur"];
            Draw_Base(effect, surface, inputTexture_, tick);

            return surface.GetTexture();
        }
    }
}

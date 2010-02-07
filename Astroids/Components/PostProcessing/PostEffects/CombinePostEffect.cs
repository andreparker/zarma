using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids.Components.PostProcessing.PostEffects
{
    using Microsoft.Xna.Framework.Graphics;

    class CombinePostEffect : PostProcessEffect
    {
        Effect effect;
        RenderTarget2D surface;

        private float saturation0 = 1.0f;
        private float saturation1 = 1.0f;
        private float intensity0 = 1.0f;
        private float intensity1 = 2.00f;

        public float Intensity1
        {
            get { return intensity1; }
            set { intensity1 = value; }
        }

        public float Intensity0
        {
            get { return intensity0; }
            set { intensity0 = value; }
        }

        public float Saturation0
        {
            get { return saturation0; }
            set { saturation0 = value; }
        }

        public float Saturation1
        {
            get { return saturation1; }
            set { saturation1 = value; }
        }

        public CombinePostEffect(GraphicsDevice device_, PostProcessManager manager_)
            : base( device_, manager_ )
        {
        }

        public override void Initialize()
        {
            PresentationParameters present = GraphicsDevice.PresentationParameters;

            surface = new RenderTarget2D(GraphicsDevice,
                (int)(present.BackBufferWidth),
                (int)(present.BackBufferHeight),
                1, present.BackBufferFormat);

            effect = Manager.Content.Load<Effect>("Combine");

        }

        public override Texture2D Process(Texture2D baseTexture_, Texture2D inputTexture_, float tick)
        {
            
            effect.Parameters["saturation0"].SetValue(saturation0);
            effect.Parameters["saturation1"].SetValue(saturation1);
            effect.Parameters["intensity0"].SetValue(intensity0);
            effect.Parameters["intensity1"].SetValue(intensity1);

            GraphicsDevice.Textures[1] = inputTexture_;
            Draw_Base(effect, surface, baseTexture_, tick);

            return surface.GetTexture();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids.Components.PostProcessing.PostEffects
{
    using Microsoft.Xna.Framework.Graphics;

    class WavePostEffect : PostProcessEffect
    {
        RenderTarget2D renderSurface;
        float timer = 0.0f;
        Effect wave;

        public WavePostEffect(GraphicsDevice device_, PostProcessManager manager_)
            : base(device_, manager_)
        {
        }

        public override void Initialize()
        {
            PresentationParameters present = GraphicsDevice.PresentationParameters;
            renderSurface = new RenderTarget2D(GraphicsDevice,
                present.BackBufferWidth,
                present.BackBufferHeight,
                1, present.BackBufferFormat);

            wave = Manager.Content.Load<Effect>("TextureWave");
        }

        public override Texture2D Process(Texture2D baseTexture_, Texture2D inputTexture_, float tick )
        {
            timer += tick;
            wave.CurrentTechnique = wave.Techniques["Wave"];

            wave.Parameters["time"].SetValue(timer);
            Draw_Base(wave, renderSurface, inputTexture_, tick);

            return renderSurface.GetTexture();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids.Components.PostProcessing.PostEffects
{
    using Microsoft.Xna.Framework.Graphics;
    class BrightExtractPostEffect : PostProcessEffect
    {
        Effect effect;
        RenderTarget2D surface;
        float brightnessThreshold = 0.15f;

        public float BrightnessThreshold
        {
            get { return brightnessThreshold; }
            set { brightnessThreshold = value; }
        }

        public BrightExtractPostEffect(GraphicsDevice device_, PostProcessManager manager_)
            : base(device_, manager_)
        {

        }

        public override void Initialize()
        {
            PresentationParameters present = GraphicsDevice.PresentationParameters;

            int w = (int)(present.BackBufferWidth * 0.5f);
            int h = (int)(present.BackBufferHeight * 0.5f);

            surface = new RenderTarget2D(GraphicsDevice, w, h, 1, present.BackBufferFormat);

            effect = Manager.Content.Load<Effect>("BrightExtract");
        }

        public override Texture2D Process(Texture2D baseTexture_, Texture2D inputTexture_, float tick)
        {
            effect.Parameters["BrightnessThreshold"].SetValue(brightnessThreshold);
            effect.CurrentTechnique = effect.Techniques["BrightExtract"];

            Draw_Base(effect, surface,inputTexture_, tick);

            return surface.GetTexture();
        }
    }
}

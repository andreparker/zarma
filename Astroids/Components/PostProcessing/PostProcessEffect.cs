using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids.Components.PostProcessing
{
    using Microsoft.Xna.Framework.Graphics;

    public abstract class PostProcessEffect
    {
        private GraphicsDevice device;
        private PostProcessManager manager;
        private bool isInitialized = false;

        public bool IsInitialized
        {
            set { isInitialized = value; }
            get { return isInitialized; }
        }

        public PostProcessEffect(GraphicsDevice device_, PostProcessManager manager_ )
        {
            device = device_;
            manager = manager_;
        }

        public abstract void Initialize();
        public abstract Texture2D Process(Texture2D baseTexture_,Texture2D inputTexture_, float tick );

        protected void Draw_Base(Effect effect_, RenderTarget2D surface_, Texture2D texture0_, float tick_)
        {
            GraphicsDevice.SetRenderTarget(0, surface_);

            Manager.SpriteBatch.Begin(SpriteBlendMode.None, SpriteSortMode.Immediate, SaveStateMode.None);
            effect_.Begin();

            foreach (EffectPass pass in effect_.CurrentTechnique.Passes)
            {
                pass.Begin();

                PostProcessManager.DrawScreenRect(Manager.SpriteBatch, texture0_);

                pass.End();
            }

            effect_.End();

            Manager.SpriteBatch.End();
            GraphicsDevice.SetRenderTarget(0, null);
        }

        public GraphicsDevice GraphicsDevice
        {
            get { return device; }
        }

        public PostProcessManager Manager
        {
            get { return manager; }
        }
    }
}

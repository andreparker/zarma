using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids.Components.PostProcessing
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    public class PostProcessManager : DrawableGameComponent
    {
        private ContentManager contentManager;
        private RenderTarget2D mainSurface;
        private SpriteBatch    spriteBatch;
        private Texture2D      outPutTexture;
        private List<PostProcessEffect> effects;

        public ContentManager Content
        {
            get{ return contentManager; }
        }

        public RenderTarget2D RenderTarget
        {
            set { mainSurface = value; }
            get { return mainSurface; }
        }

        public SpriteBatch SpriteBatch
        {
            set { spriteBatch = value; }
            get { return spriteBatch; }
        }

        public Texture2D OutPutTexture
        {
            get { return outPutTexture; }
        }

        public PostProcessManager(Game game_ , SpriteBatch spriteBatch_, RenderTarget2D surface_ )
            : base(game_)
        {
            spriteBatch = spriteBatch_;
            mainSurface = surface_;

            contentManager = new ContentManager(game_.Services,"Content/effects/PostProcess");
            effects = new List<PostProcessEffect>();
        }

        public void AddProcess(PostProcessEffect effect)
        {
            effects.Add(effect);
            if (effect.IsInitialized == false)
            {
                effect.Initialize();
                effect.IsInitialized = true;
            }
        }

        public void RemoveProcess(PostProcessEffect effect)
        {
            while (effects.Contains(effect) == true)
            {
                effects.Remove(effect);
            }
        }

        public void ClearProcesses()
        {
            effects.Clear();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Draw(GameTime gameTime)
        {
            Texture2D baseTexture = mainSurface.GetTexture();
            outPutTexture = baseTexture;

            float tick = (float)gameTime.ElapsedGameTime.TotalSeconds;

            foreach (PostProcessEffect effect in effects)
            {
                outPutTexture = effect.Process(baseTexture, outPutTexture,tick);
            }

            base.Draw(gameTime);
        }

        public static void DrawScreenRect(SpriteBatch spriteBatch_, Texture2D texture_)
        {
            GraphicsDevice device = spriteBatch_.GraphicsDevice;
            Rectangle rect = new Rectangle(0, 0, device.Viewport.Width, device.Viewport.Height);

            spriteBatch_.Draw(texture_, rect, Color.White);
        }
    }
}

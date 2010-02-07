using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids.Components.ParticleSystem
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    public class ParticleSystem : DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        List<ParticleEmitter> emitters;
        ContentManager content;

        public ContentManager Content
        {
            get { return content; }
        }

        public ParticleSystem( Game game_, SpriteBatch spriteBatch_):
            base( game_ )
        {
            spriteBatch = spriteBatch_;
            emitters = new List<ParticleEmitter>();
            content = new ContentManager(game_.Services, "Content/Particles");
        }

        public void AddEmitter( ParticleEmitter emitter_ )
        {
            emitters.Add(emitter_);
        }

        public void RemoveEmitter( ParticleEmitter emitter_ )
        {
            emitters.Remove(emitter_);
        }

        public override void Update(GameTime gameTime)
        {
            foreach( ParticleEmitter emitter in emitters )
            {
                emitter.Update(gameTime);
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach( ParticleEmitter emitter in emitters )
            {
                emitter.Draw(gameTime, spriteBatch);
            }

            base.Draw(gameTime);
        }
    }
}

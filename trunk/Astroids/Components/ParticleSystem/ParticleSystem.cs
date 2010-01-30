using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids.Components.ParticleSystem
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    class ParticleSystem : DrawableGameComponent
    {
        SpriteBatch spriteBatch;

        ParticleSystem( Game game_, SpriteBatch spriteBatch_):
            base( game_ )
        {
            spriteBatch = spriteBatch_;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}

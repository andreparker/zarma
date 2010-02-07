using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids.Components.ParticleSystem.Emitters
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class SmokeEmitter : ParticleEmitter
    {
        Random randomAngle;
        Vector2 direction;
        float minAngle = -4.0f,
            maxAngle = 4.0f;

        public float MaxAngle
        {
            get { return maxAngle; }
            set { maxAngle = value; }
        }

        public float MinAngle
        {
            get { return minAngle; }
            set { minAngle = value; }
        }
        public Vector2 Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        public SmokeEmitter(
            Texture2D particleImage_,
            float particleTime_,
            Color startColor_,
            Color endColor_ ,
            float velocity_,
            int particleCount_ )
            : base
            ( 
            particleImage_,
            particleTime_,
            startColor_,
            endColor_,
            velocity_,
            particleCount_
            )
        {
            randomAngle = new Random();
        }

        protected override void ActivateParticle(Particle particle)
        {
            float val = (float)randomAngle.NextDouble();
            float angle = MathHelper.ToRadians( ((maxAngle - minAngle) * val) + minAngle );

            float s = (float)System.Math.Sin((double)angle);
            float c = (float)System.Math.Cos((double)angle);

            Vector2 dir = new Vector2
                (c * Direction.X - s * Direction.Y,s * Direction.X + c * Direction.Y );
            
            dir.Normalize();

            dir.X *= Velocity;
            dir.Y *= Velocity;

            particle.Velocity = dir;

            base.ActivateParticle(particle);
        }

        public override void Draw(GameTime time_, SpriteBatch spriteBatch_)
        {
            spriteBatch_.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None);
            {
                foreach (Particle particle in Particles)
                {
                    if( particle != null && 
                        particle.Active == true )
                    {
                        spriteBatch_.Draw(
                            Image, 
                            new Rectangle( 
                                (int)(particle.Position.X + 0.9999f),
                                (int)(particle.Position.Y + 0.9999f),
                                4,
                                4 ),
                            particle.Color);
                    }
                }
                spriteBatch_.End();
            }
        }
    }
}

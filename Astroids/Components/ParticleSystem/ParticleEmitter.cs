using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids.Components.ParticleSystem
{
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework;

    public abstract class ParticleEmitter
    {
        private Color startColor,endColor;
        private Vector2 position;
        private float velocity;
       
     
        private Texture2D image;
        private float particleTime;
        private int particleCount;
        private int particleIndex;
        private Particle[] particles;

        public float Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public Particle[] Particles
        {
            get { return particles; }
        }

        public Texture2D Image
        {
            get { return image; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public float ParticleTime
        {
            get { return particleTime; }
        }

        public ParticleEmitter( 
            Texture2D particleImage_,
            float particleTime_,
            Color startColor_,
            Color endColor_ ,
            float velocity_,
            int particleCount_ )
        {
            particleTime = particleTime_;
            image = particleImage_;
            startColor = startColor_;
            endColor = endColor_;
            velocity = velocity_;
            particleCount = particleCount_;
            particleIndex = 0;

            position = new Vector2();
            particles = new Particle[particleCount];
        }


        protected virtual void ActivateParticle( Particle particle )
        {
            particle.Active = true;
            particle.Color = startColor;
            particle.Position = position;
            particle.StartTime = 0.0f;
        }

        public void Emit(int count)
        {
            UInt32 particleSearchCount = 0;
            int particlesAdded = 0;
            while( particleSearchCount < (UInt32)particleCount 
                   && particlesAdded < count )
            {
                if( particles[ particleIndex ] == null ||
                    particles[ particleIndex ].Active == false )
                {
                    ++particlesAdded;
                    if( particles[particleIndex] == null )
                    {
                        particles[particleIndex] = new Particle();
                    }

                    ActivateParticle(particles[particleIndex]);

                }

                ++particleSearchCount;
                ++particleIndex;

                if (particleIndex >= particleCount) particleIndex = 0;
            }
        }

        public virtual void Update(GameTime time_)
        {
            float tick = (float)time_.ElapsedGameTime.TotalSeconds;
            foreach ( Particle particle in particles )
            {
                if( particle != null && particle.Active )
                {
                    float life = particle.StartTime / ParticleTime;

                    Color clr = Color.Lerp(startColor, endColor, life);
                    clr.A = (byte)MathHelper.Lerp(255.0f, 0.0f, life);

                    particle.Color = clr;

                    particle.Update(tick);

                    if( life >= 1.0f )
                    {
                        particle.Active = false;
                    }
                }
            }
        }

        public abstract void Draw(GameTime time_,SpriteBatch spriteBatch_);
    }
}

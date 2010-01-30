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
        private Vector2 velocity,position;
     
        private Texture2D image;
        private float particleTime;
        private int particleCount;
        private int particleIndex;
        private Particle[] particles;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public ParticleEmitter( 
            Texture2D particleImage_,
            float particleTime_,
            Color startColor_,
            Color endColor_ ,
            Vector2 velocity_,
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
            int particleSearcCount = 0;
            int particlesAdded = 0;
            while( particleSearcCount < particleCount 
                   || particlesAdded == count )
            {
                if( particles[ particleIndex ].Active == false )
                {
                    ++particlesAdded;
                    ActivateParticle(particles[particleIndex]);

                }

                ++particleSearcCount;
                ++particleIndex;

                if (particleIndex >= particleCount) particleIndex = 0;
            }
        }

        public abstract void Update(GameTime time_);
        public abstract void Draw(GameTime time_);
    }
}

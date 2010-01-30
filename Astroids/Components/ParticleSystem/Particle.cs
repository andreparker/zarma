using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids.Components.ParticleSystem
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public class Particle
    {
        private Vector2 position;
        private float startTime = 0.0f;
        Color color;
        bool active;

        public float StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public bool Active
        {
            get { return active; }
            set { active = value; }
        }

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        public Particle()
        {
            position = new Vector2();
            color = new Color();
            active = false;
        }

        public void Update( float tick )
        {
            startTime += tick;
        }
    }
}

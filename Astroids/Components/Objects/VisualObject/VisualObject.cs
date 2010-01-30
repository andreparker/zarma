using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Asteroids.Components.Objects.VisualObject
{
    using GameObject;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public abstract class VisualObject
    {
        private GameObject gameObject = null;
        private bool       is_visible = true;
        private bool       remove = false;

        public VisualObject( GameObject gameObject_ )
        {
            if( gameObject_ != null )
            {
                gameObject = gameObject_;
                gameObject.UpdateCallBack += new GameObject.UpdateCalled(this.UpdateCaller);
            }
           
        }

        protected virtual void UpdateCaller( GameTime time_)
        {
            if (gameObject != null && gameObject.Remove)
            {
                remove = true;
            }
        }

        public abstract void Update(GameTime time_);
        public abstract void Draw(GameTime time_,SpriteBatch spriteBatch_);

        public bool IsVisible
        {
            get { return is_visible; }
            set { is_visible = value;}
        }

        public bool Remove
        {
            get { return remove; }
            set { remove = value; }
        }
    }
}

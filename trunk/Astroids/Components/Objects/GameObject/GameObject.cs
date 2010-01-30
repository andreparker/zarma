using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;


namespace Asteroids.Components.Objects.GameObject
{
    using Components.Input;
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public abstract class GameObject
    {
        public delegate void UpdateCalled(GameTime time_);

        private Vector2 position;
        private int object_type;
        private bool remove;
        private InputController<GameObject> controller;

        public event UpdateCalled UpdateCallBack = null;

        public GameObject(int type_ )
        {
            object_type = type_;
        }

        public void SetController(InputController<GameObject> controller_)
        {
            controller = controller_;
        }

        public void Update(GameTime gameTime)
        {
            if (UpdateCallBack != null)
            {
                UpdateCallBack(gameTime);
            }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public bool Remove
        {
            get { return remove; }
            set { remove = value; }
        }

        public int ObjectType
        {
            get{ return object_type;}
        }
    }
}
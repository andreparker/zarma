using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace Asteroids.Components.Sprite
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    public class SpriteManager
    {
        private ContentManager content;
        private Hashtable spriteHash;

        public SpriteManager(Game game_)
        {
            content = new ContentManager( game_.Services, "Content/Sprites" );
            spriteHash = new Hashtable();
        }

        public void Load(string spriteName_)
        {
            Texture2D sprite = content.Load<Texture2D>(spriteName_);
            spriteHash.Add(spriteName_, sprite);
        }

        public void UnLoad()
        {
            spriteHash.Clear();
            content.Unload();
        }

        public Texture2D GetSprite(string spriteName_)
        {
            Texture2D spriteImage = null;

            if (spriteHash.Contains(spriteName_) == true)
            {
                spriteImage = spriteHash[spriteName_] as Texture2D;
            }

            return spriteImage;
        }

    }
}

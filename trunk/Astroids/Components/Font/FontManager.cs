using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Asteroids.Components.Font
{

    public class FontManager
    {
        private readonly string InvalidBeginCall = "Invalid call to begin. End was not called to last begin.";
        private readonly string InvalidEndCall = "Invalid call to end. No matching begin.";
        private readonly string InvalidDrawCall = "Invalid call to draw. Current font is null or no Begin called.";


        private SpriteBatch spriteRender = null;
        private Hashtable fontHash;
        private bool isEnd = true;
        private SpriteFont currentFont = null;

        public FontManager( SpriteBatch batch )
        {
            spriteRender = batch;

            fontHash = new Hashtable();
        }

        public void Begin(string fontName)
        {
            if (isEnd == true)
            {
                currentFont = fontHash[fontName] as SpriteFont;
                isEnd = false;
                spriteRender.Begin(SpriteBlendMode.AlphaBlend,SpriteSortMode.Deferred,SaveStateMode.None);
            }
            else
            {
                throw new InvalidOperationException( InvalidBeginCall );
            }
        }

        public void End()
        {
            if (currentFont != null && isEnd == false)
            {
                spriteRender.End();
                currentFont = null;
                isEnd = true;
            }
            else
            {
                throw new InvalidOperationException(InvalidEndCall);
            }
        }

        public void AddFont( SpriteFont font, string name )
        {
            if (font != null)
            {
                fontHash.Add(name, font);
            }
            else
            {
                throw new ArgumentNullException("font is null");
            }
        }

        public void Draw(Vector2 position, string text, Color clr)
        {
            if (isEnd == false && currentFont != null)
            {
                spriteRender.DrawString(currentFont, text, position, clr);
            }
            else
            {
                throw new InvalidOperationException(InvalidDrawCall);
            }
        }

        public void Draw(Vector2 position, string text)
        {
            Draw(position, text, Color.White);
        }

        public SpriteFont GetFont( string name )
        {
            return (SpriteFont)fontHash[ name ];
        }

        public SpriteBatch spriteBatch
        {
            get
            {
                return spriteRender;
            }
        }

    }

}
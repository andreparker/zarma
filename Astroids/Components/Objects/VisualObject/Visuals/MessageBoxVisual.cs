using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids.Components.Objects.VisualObject.Visuals
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Components.Font;
    using Components.Objects.Common;


    class MessageBoxVisual : VisualObject
    {
        Texture2D image;
        SpriteFont font;
        string message;
        Vector2 position,stringSize;

        public MessageBoxVisual( 
            FontManager fontManager_, 
            Texture2D messageBoxTex_,
            string text,
            Nullable<Vector2> position_ )
            : base( null )
        {
            image = messageBoxTex_;
            position = (Vector2)position_;
            message = text;

            font = fontManager_.GetFont("Arial");
            stringSize = font.MeasureString(message);
        }

        public override void Draw(GameTime time_, SpriteBatch spriteBatch_)
        {

            spriteBatch_.Begin(SpriteBlendMode.AlphaBlend);
            spriteBatch_.Draw(image, position, Color.White);
            spriteBatch_.DrawString(font, message, new Vector2(position.X + 10.0f, position.Y + 10.0f), Color.White);
            spriteBatch_.End();
            
        }

        public override void Update(GameTime time_)
        {
        }
    }

    public class MessageVisualFactory : AbstractTypeFactory<VisualObject>
    {
        public MessageVisualFactory( Game game_ )
            : base( game_ )
        {
            Type = VisualTypes.MessageVisualType;
        }

        public override bool CanCreateType(int type_)
        {
            return (type_ == Type ? true : false);
        }

        public override VisualObject CreateFromType(int type_, params object[] args)
        {
            return new MessageBoxVisual(
                args[0] as FontManager,
                args[1] as Texture2D,
                args[2] as string,
                args[3] as Nullable<Vector2>);
        }
    }
}

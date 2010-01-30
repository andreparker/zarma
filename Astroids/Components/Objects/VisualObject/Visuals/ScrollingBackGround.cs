using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids.Components.Objects.VisualObject.Visuals
{
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework;
    using Components.Objects.Common;

    public class ScrollingBackGround : VisualObject
    {
        public delegate void OnUpdate(GameTime time_);

        Texture2D image;
        Effect effect;
        Vector2 direction;
        float distance;
        Color blendColor;
        private OnUpdate updateCallBack = null;

        public OnUpdate UpdateCallBack
        {
            set { updateCallBack = value; }
        }

        public float Distance
        {
            get { return distance; }
            set { distance = value; }
        }
        
        public Vector2 Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        public ScrollingBackGround(
            Texture2D backgroundImage_,
            Effect effect_,
            Nullable<Color> blendColor_ )
            : base( null )
        {
            effect = effect_;
            image = backgroundImage_;
            blendColor = (Color)blendColor_;

            direction = new Vector2();
            distance = 0.0f;
        }

        public override void Draw(GameTime time_, SpriteBatch spriteBatch_)
        {
            Rectangle screenRect = new Rectangle
               ( 0, 0, 
                spriteBatch_.GraphicsDevice.Viewport.Width,
                spriteBatch_.GraphicsDevice.Viewport.Height );
            spriteBatch_.Begin(SpriteBlendMode.Additive, SpriteSortMode.Immediate, SaveStateMode.None);
            {
                effect.CurrentTechnique = effect.Techniques[0];
                effect.Begin();
                {
                    spriteBatch_.GraphicsDevice.SamplerStates[0].AddressU = TextureAddressMode.Wrap;
                    spriteBatch_.GraphicsDevice.SamplerStates[0].AddressV = TextureAddressMode.Wrap;

                    effect.Parameters["scrollVector"].SetValue(new Vector2(distance * direction.X, distance * direction.Y));
                    effect.CurrentTechnique.Passes[0].Begin();
                    {
                        spriteBatch_.Draw(image, screenRect, blendColor);
                        effect.CurrentTechnique.Passes[0].End();
                    }
                    effect.End();
                }
                spriteBatch_.End();
            }
        }

        public override void Update(GameTime time_)
        {
            if( updateCallBack != null )
            {
                updateCallBack(time_);
            }
        }
    }

    public class ScrollingBackGroundFactory : AbstractTypeFactory<VisualObject>
    {
        public ScrollingBackGroundFactory( Game game_ )
            : base( game_ )
        {
            Type = VisualTypes.BackGroundScrollVisualType;
        }

        public override bool CanCreateType(int type_)
        {
            return (Type == type_ ? true : false);
        }

        public override VisualObject CreateFromType(int type_, params object[] args)
        {
            return new ScrollingBackGround(
                args[0] as Texture2D,
                args[1] as Effect,
                args[2] as Nullable<Color>);
        }
    }
}

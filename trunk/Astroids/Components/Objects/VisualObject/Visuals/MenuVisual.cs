using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids.Components.Objects.VisualObject.Visuals
{
    using Components.Gui;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Components.Objects.Common;
    using Components.Objects.VisualObject.Visuals;

    public class MenuVisual : VisualObject
    {
        SimpleMenu menu;
        public MenuVisual( SimpleMenu menu_ )
            : base( null )
        {
            menu = menu_;
        }

        public override void Draw(GameTime time_, SpriteBatch spriteBatch_)
        {
            menu.Draw(time_);
        }

        public override void Update(GameTime time_)
        {
        }
    }

    public class MenuVisualFactory : AbstractTypeFactory<VisualObject>
    {
        public MenuVisualFactory( Game game_ )
            :base( game_ )
        {
            Type = VisualTypes.MenuVisualType;
        }

        public override bool CanCreateType(int type_)
        {
            return (type_ == Type ? true : false);
        }

        public override VisualObject CreateFromType(int type_, params object[] args)
        {
            return new MenuVisual(args[0] as SimpleMenu);
        }
    }
}

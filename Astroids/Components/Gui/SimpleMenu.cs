using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids.Components.Gui
{
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework;
    using Components.Font;

    public class SimpleMenu
    {
        private GraphicsDevice device;
        private FontManager fontManager;
        private Vector2 menuPosition;
        public delegate void MenuEntryCallBack(SimpleMenu sender, int itemIndex);
        
        private List<string> menuItems;
        private int itemIndex = 0;
        public event MenuEntryCallBack OnSelected = null;

        public enum SelectionMoveDir
        {
            MoveUp,
            MoveDown
        }

        public SimpleMenu(GraphicsDevice device_,FontManager fontManager_,Vector2 position_)
        {
            device = device_;
            fontManager = fontManager_;
            menuPosition = position_;

            menuItems = new List<string>();
        }

        public void ExecuteMenuItem()
        {
            if (OnSelected != null)
            {
                OnSelected(this, itemIndex);
            }
        }

        public void AddMenuItem(string menuItem_)
        {
            menuItems.Add(menuItem_);
        }

        public string GetCurrentItem()
        {
            return menuItems[itemIndex];
        }

        public void SetCurrentItemIndex( int index )
        {
            itemIndex = index;
            if (itemIndex > menuItems.Count - 1) itemIndex = menuItems.Count - 1;
            if (itemIndex < 0) itemIndex = 0;
        }

        public void Draw(GameTime time_)
        {
            int count = menuItems.Count;
            Vector2 itemOffset;
            Vector2 position = new Vector2(menuPosition.X,menuPosition.Y);

            fontManager.Begin("Arial");
            SpriteFont arial = fontManager.GetFont("Arial");
            
            fontManager.Draw(position, menuItems[itemIndex], Color.Yellow);
            itemOffset = arial.MeasureString(menuItems[itemIndex]);
            position.Y += itemOffset.Y;

            int i;
            string str;

            for ( i = itemIndex+1; i < count; ++i)
            {
                str = menuItems[i];
                itemOffset = arial.MeasureString(str);
                fontManager.Draw(position, str );
                position.Y += itemOffset.Y;
            }

            for (i = 0; i < itemIndex; ++i)
            {
                str = menuItems[i];
                itemOffset = arial.MeasureString(str);
                fontManager.Draw(position, str);
                position.Y += itemOffset.Y;
            }

            fontManager.End();
        }

        public void MoveSelection(SelectionMoveDir dir)
        {
            switch (dir)
            {
                case SelectionMoveDir.MoveUp:
                    {
                        if (itemIndex > 0)
                        {
                            --itemIndex;
                        }
                        else
                        {
                            itemIndex = menuItems.Count - 1;
                        }
                        break;
                    }
                case SelectionMoveDir.MoveDown:
                    {
                        if (itemIndex < menuItems.Count - 1)
                        {
                            ++itemIndex;
                        }
                        else
                        {
                            itemIndex = 0;
                        }
                        break;
                    }
                default:
                    break;
            }
        }





    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids.Components.Input.Inputs
{
    using Components.Gui;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    class MenuGamePadController : InputController<SimpleMenu>
    {
        private Game game;
        private float delayTime = 0.0f;
        private const float kMaxDelay = 0.2f;
        private const float kStickThreshold = 0.60f;

        public MenuGamePadController( Game game_ )
        {
            game = game_;
        }

        public override void ProcessInput(SimpleMenu object_, GameTime time_)
        {
            if (delayTime > kMaxDelay)
            {
                GamePadState state = GamePad.GetState(PlayerIndex.One);
                if (state.ThumbSticks.Left.Y > kStickThreshold)
                    object_.MoveSelection(SimpleMenu.SelectionMoveDir.MoveUp);
                if (state.ThumbSticks.Left.Y < -kStickThreshold)
                    object_.MoveSelection(SimpleMenu.SelectionMoveDir.MoveDown);
                if (state.Buttons.A == ButtonState.Pressed)
                    object_.ExecuteMenuItem();
                if (state.Buttons.B == ButtonState.Pressed)
                {
                    object_.SetCurrentItemIndex(2);
                    object_.ExecuteMenuItem();
                }

                delayTime = 0.0f;
            }

            delayTime += (float)time_.ElapsedGameTime.TotalSeconds;
           
        }
    }
}

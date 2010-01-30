using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids.Components.GameState.States
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Components.Input;
    using Components.Objects.VisualObject.Visuals;
    using Components.Objects.VisualObject;
    using Components.Font;

    class MessageBoxState : State
    {
        private State RestoreState;
        private string displayString;
        private float delayTime = 0.0f;
        private VisualObject vis;
        private InputController<MessageBoxState> controller;

        public MessageBoxState( 
            StateManager manager_,
            string str, State saveState_,
            InputController<MessageBoxState> controller_ )
            : base( manager_ )
        {
            RestoreState = saveState_;
            displayString = str;
            controller = controller_;
        }

        public override void Enter()
        {
            Asteroids game_ = Manager.VisualManager.Game as Asteroids;
            Texture2D image = game_.MessageBoxImage;

            vis = Manager.VisualManager.CreateVisualObjectArg(
                VisualTypes.MessageVisualType,
                game_.FontManager,
                image,
                displayString,
                new Vector2(280.0f, 300.0f));
        }

        public override void Update(GameTime gameTime_)
        {
            if( delayTime > 0.5f )
            {
                controller.ProcessInput(this,gameTime_);
            }

            delayTime += (float)gameTime_.ElapsedGameTime.TotalSeconds;
        }

        public override State Transition(int eventType_)
        {
            return RestoreState;
        }

        public override void Exit()
        {
            if( vis != null )
            {
                vis.Remove = true;
            }
        }
    }
}

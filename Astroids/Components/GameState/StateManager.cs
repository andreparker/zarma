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


namespace Asteroids.Components.GameState
{
    using Components.Objects.GameObject;
    using Components.Objects.VisualObject;

    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class StateManager : GameComponent
    {
        private State currentState = null;
        private VisualObjectManager visualManager;
        private GameObjectManager objectManager;

        public VisualObjectManager VisualManager
        {
            get { return visualManager; }
        }

        public GameObjectManager ObjectManager
        {
            get { return objectManager; }
        }

        public StateManager(Game game,VisualObjectManager visualManager_,GameObjectManager objectManager_)
            : base(game)
        {
            visualManager = visualManager_;
            objectManager = objectManager_;
        }

        public void SetState(State state_)
        {
            SetRawState(state_, true, true);
        }

        public void SetRawState(State state_,bool callEnter, bool callExit )
        {
            if (callExit == true && currentState != null)
            {
                currentState.Exit();
            }

            currentState = state_;

            if( callEnter == true )
            {
                currentState.Enter();
            }
        }

        public void SendEvent(int eventType_)
        {
            if (currentState != null )
            {
                State state_ = currentState.Transition(eventType_);
                if (state_ != null )
                {
                    SetState(state_);
                }
            }
        }
        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {

            if (currentState != null)
            {
                currentState.Update(gameTime);
            }

            base.Update(gameTime);
        }
    }
}
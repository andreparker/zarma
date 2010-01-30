using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids.Components.GameState
{
    using Microsoft.Xna.Framework;

    public abstract class State
    {
        private StateManager stateManager;

        public State(StateManager stateManager_)
        {
            stateManager = stateManager_;
        }

        public abstract void Enter();
        public abstract void Update(GameTime gameTime_);
        public abstract State Transition( int eventType_ );
        public abstract void Exit();

        public StateManager Manager
        {
            get { return stateManager; }
            set { stateManager = value; }
        }
    }
}

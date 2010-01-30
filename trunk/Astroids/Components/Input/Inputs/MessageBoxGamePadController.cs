using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids.Components.Input.Inputs
{
    using Components.GameState.States;
    using Microsoft.Xna.Framework.Input;
    using Microsoft.Xna.Framework;

    class MessageBoxGamePadController : InputController< MessageBoxState >
    {
        public delegate void OnAccept(MessageBoxState state);
        public delegate void OnCancel(MessageBoxState state);

        private OnAccept AcceptCallBack = null;
        private OnCancel CancelCallBack = null;

        public MessageBoxGamePadController( OnAccept acceptDelegate_, OnCancel cancelDelegate_ )
        {
            AcceptCallBack = acceptDelegate_;
            CancelCallBack = cancelDelegate_;
        }

        public override void ProcessInput(MessageBoxState object_,GameTime time_)
        {
            GamePadState state = GamePad.GetState( PlayerIndex.One );
            if( state.Buttons.A == ButtonState.Pressed )
            {
                if (AcceptCallBack != null) AcceptCallBack(object_);
            }else if( state.Buttons.B == ButtonState.Pressed )
            {
                if (CancelCallBack != null) CancelCallBack(object_);
            }
        }
    }
}

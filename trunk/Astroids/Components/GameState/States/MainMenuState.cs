using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids.Components.GameState.States
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Components.Font;
    using Components.Gui;
    using Components.Input;
    using Components.Input.Inputs;
    using Components.Objects.VisualObject.Visuals;
    using Components.ParticleSystem.Emitters;

    class MainMenuState : State
    {
        MenuVisual menuVisual;
        ScrollingBackGround backGround0, backGround1;

        Vector2 scrollDirection;
        float scrollDistance;
        float tick = 0.0f;

        Texture2D backGroundImage;
        Effect scrollEffect;

        SimpleMenu menu;
        InputController<SimpleMenu> controller;

        SmokeEmitter emitter;

        public MainMenuState( 
            StateManager manager_,
            GraphicsDevice device_, 
            FontManager fontManager_,
            InputController<SimpleMenu> controller_ )
            : base( manager_ )
        {
            menu = new SimpleMenu(device_, fontManager_, new Vector2(512.0f, 300.0f));
            menu.OnSelected += new SimpleMenu.MenuEntryCallBack(this.OnMenuSeletection);
            controller = controller_;

            scrollDirection = new Vector2(1.0f, 0.0f);
            scrollDistance = 0.0f;

            emitter = new SmokeEmitter
                (
                Manager.ParticleSystem.Content.Load<Texture2D>("particle_puff"),
                1.5f, Color.Orange,Color.DarkGray, 1.0f, 500
                );

            Vector2 direction = new Vector2(0.0f, 1.0f);
            Vector2 position = new Vector2(526.0f, 125.0f);

            emitter.Direction = direction;
            emitter.Position = position;
           
            Manager.ParticleSystem.AddEmitter(emitter);
        }

        public override void Enter()
        {
            menu.AddMenuItem("Start");
            menu.AddMenuItem("Options");
            menu.AddMenuItem("Exit");

            backGroundImage = Manager.Game.Content.Load<Texture2D>("Images/stars0");
            scrollEffect = Manager.Game.Content.Load<Effect>("effects/TextureScroll");

            backGround0 = Manager.VisualManager.CreateVisualObjectArg
                (VisualTypes.BackGroundScrollVisualType,
                backGroundImage,
                scrollEffect,
                Color.LightBlue
                ) as ScrollingBackGround;
            backGround0.UpdateCallBack = new ScrollingBackGround.OnUpdate(this.UpdateBackGround0);

            backGround1 = Manager.VisualManager.CreateVisualObjectArg
                (VisualTypes.BackGroundScrollVisualType,
                backGroundImage,
                scrollEffect,
                Color.White
                ) as ScrollingBackGround;
            backGround1.UpdateCallBack = new ScrollingBackGround.OnUpdate(this.UpdateBackGround1);

            menuVisual = Manager.VisualManager.CreateVisualObjectArg(VisualTypes.MenuVisualType, menu) as MenuVisual;
            
        }

        public override void Update(GameTime gameTime_)
        {
            tick += (float)gameTime_.ElapsedGameTime.TotalSeconds;

            controller.ProcessInput(menu,gameTime_);

            if( tick > 0.05f )
            {
                tick = 0.0f;
                emitter.Emit(10);
            }
            
        }

        public void UpdateBackGround0( GameTime time_ )
        {
            scrollDistance += (float)time_.ElapsedGameTime.TotalSeconds;
            // update the backgrounds
            backGround0.Direction = scrollDirection;
            backGround0.Distance = scrollDistance * 0.005f;
        }

        public void UpdateBackGround1(GameTime time_)
        {
            // update the backgrounds
            backGround1.Direction = scrollDirection;
            backGround1.Distance = scrollDistance * 0.010f;
        }

        public override State Transition(int eventType_)
        {
            return null;
        }

        public override void Exit()
        {
            backGroundImage.Dispose();
            scrollEffect.Dispose();

            backGround0.Remove = true;
            backGround1.Remove = true;
        }

        public void OnMenuSeletection( SimpleMenu menu, int itemIndex )
        {
            switch (itemIndex)
            {
                case 2:
                    {
                        menuVisual.IsVisible = false;
                        // call this to avoid exiting previous state
                        // we save this state instead to return to it
                        Manager.SetRawState(new MessageBoxState(
                            Manager, "Are you sure you want to exit?", this,
                            new MessageBoxGamePadController(
                                new MessageBoxGamePadController.OnAccept(this.OnAcceptExit),
                                new MessageBoxGamePadController.OnCancel(this.OnCancelExit))),
                            true, false );
                        break;
                    }
            }
        }

        public void OnAcceptExit( MessageBoxState state )
        {
            Manager.Game.Exit();
        }

        public void OnCancelExit( MessageBoxState state )
        {
            menuVisual.IsVisible = true;
            Manager.SetRawState(state.Transition(0), false, true);
        }
    }
}

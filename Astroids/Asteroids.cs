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

namespace Asteroids
{
    using Components.Font;
    using Components.GameState;
    using Components.GameState.States;
    using Components.Objects.GameObject;
    using Components.Objects.VisualObject;
    using Components.Objects.VisualObject.Visuals;
    using Components.PostProcessing;
    using Components.PostProcessing.PostEffects;
    using Components.Gui;
    using Components.Input.Inputs;
    using Components.Sprite;

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Asteroids : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        FontManager fontManager;
        RenderTarget2D mainSurface;
        StateManager stateManager;
        GameObjectManager gameObjectManager;
        VisualObjectManager visualObjectManager;
        PostProcessManager postEffectManager;
        SpriteManager spriteManager;

        float tick = 0;

        Texture2D messageBoxImage;

        public FontManager FontManager
        {
            get { return fontManager; }
        }

        public Texture2D MessageBoxImage
        {
            get { return messageBoxImage; }
        }

        int numBlurPasses = 6;


        public Asteroids()
        {
            graphics = new GraphicsDeviceManager(this);

            InitializeDevice();
           

            Content.RootDirectory = "Content"; 
        }

        private void InitializeComponents()
        {

            spriteManager = new SpriteManager(this);
            gameObjectManager = new GameObjectManager(this);
            visualObjectManager = new VisualObjectManager(this,spriteBatch,spriteManager);
            postEffectManager = new PostProcessManager(this,spriteBatch,mainSurface);
            stateManager = new StateManager(this,visualObjectManager,gameObjectManager);

            stateManager.UpdateOrder = 1;
            gameObjectManager.UpdateOrder = 2;
            visualObjectManager.UpdateOrder = 3;
            visualObjectManager.DrawOrder = 1;

            visualObjectManager.RegisterFactory(new MenuVisualFactory(this));
            visualObjectManager.RegisterFactory(new MessageVisualFactory(this));
            visualObjectManager.RegisterFactory(new ScrollingBackGroundFactory(this));

            // get live sign setup
            //Components.Add(new GamerServicesComponent(this));

            stateManager.SetState(new MainMenuState(stateManager, GraphicsDevice,
                fontManager, new MenuGamePadController(this)));

            Components.Add(stateManager);
            Components.Add(gameObjectManager);
            Components.Add(visualObjectManager);
        }

        private void InitializeMiscSettings()
        {
        }

        private void InitializeRenderTargets()
        {
            PresentationParameters present = GraphicsDevice.PresentationParameters;
            mainSurface = new RenderTarget2D(GraphicsDevice,
                present.BackBufferWidth,
                present.BackBufferHeight, 1,
                present.BackBufferFormat
                );
        }

        private void InitializeDevice()
        {
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            graphics.PreferredBackBufferFormat = SurfaceFormat.Color;
            graphics.MinimumPixelShaderProfile = ShaderProfile.PS_2_0;
            graphics.MinimumVertexShaderProfile = ShaderProfile.VS_1_1;
            graphics.PreferMultiSampling = false;
            graphics.IsFullScreen = false;

            graphics.ApplyChanges();
        }
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            InitializeRenderTargets();

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            fontManager = new FontManager(spriteBatch);

            InitializeComponents();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            
            // TODO: use this.Content to load your game content here
            SpriteFont arialFont = Content.Load<SpriteFont>("Fonts/Arial");
            messageBoxImage = Content.Load<Texture2D>("Images/MessageBox");
            spriteManager.Load("ship0");

            PostProcessEffect horzEffect = new HorzBlurPostEffect(GraphicsDevice, postEffectManager);
            PostProcessEffect VertEffect = new VertBlurPostEffect(GraphicsDevice, postEffectManager);

            postEffectManager.AddProcess(new BrightExtractPostEffect(GraphicsDevice, postEffectManager));

            for (int i = 0; i < numBlurPasses; ++i)
            {
                postEffectManager.AddProcess(VertEffect);
                postEffectManager.AddProcess(horzEffect);
            }
            
            postEffectManager.AddProcess(new CombinePostEffect(GraphicsDevice, postEffectManager));
            //postEffectManager.AddProcess(new WavePostEffect(GraphicsDevice, postEffectManager));

            fontManager.AddFont(arialFont, "Arial");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            tick += (float)gameTime.ElapsedGameTime.TotalSeconds;

            graphics.GraphicsDevice.SetRenderTarget(0, mainSurface);
            GraphicsDevice.Clear(Color.Black);
           
            // componets draw here
            //=================================
            base.Draw(gameTime);

            Texture2D shipTexture = spriteManager.GetSprite("ship0");
            spriteBatch.Begin(SpriteBlendMode.AlphaBlend);
            spriteBatch.Draw(shipTexture, new Rectangle(512, 100, 32, 32), Color.White);
            spriteBatch.End();
           

            graphics.GraphicsDevice.SetRenderTarget(0, null);


            //=================================
            // post drawing
            postEffectManager.Draw(gameTime);
            spriteBatch.Begin(SpriteBlendMode.None);

            PostProcessManager.DrawScreenRect(spriteBatch, postEffectManager.OutPutTexture);

            spriteBatch.End();
        }
    }
}

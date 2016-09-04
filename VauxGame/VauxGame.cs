using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Maps.Tiled;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.BitmapFonts;
using VauxGame.Components;
using VauxGame.Commands;

namespace VauxGame
{
    public class VauxGame : Game
    {
        #region - CONSTANTS -

        private const bool CURSOR_VISIBILITY = false;
        private const bool SYNCHRONIZE_WITH_VERTICAL_RETRACE = false;
        private const int WINDOW_WIDTH = 800;
        private const int WINDOW_HEIGHT = 480;

        #endregion

        private GraphicsDeviceManager graphics;
        private ViewportAdapter _viewportAdapter;
        private FpsCounterAdvanced _fpsCounter;
        private SpriteBatch _spriteBatch;
        private Camera2D _camera;
        private TiledMap _map;
        private Texture2D _icon;
        private Sprite _sprite;
        private Cursor _cursor;

        public VauxGame()
        {
            graphics = new GraphicsDeviceManager(this) { SynchronizeWithVerticalRetrace = SYNCHRONIZE_WITH_VERTICAL_RETRACE };
            Content.RootDirectory = "Content";

            IsMouseVisible = CURSOR_VISIBILITY;
            IsFixedTimeStep = false;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, WINDOW_WIDTH, WINDOW_HEIGHT);
            _camera = new Camera2D(_viewportAdapter) {
                MinimumZoom = 1f,
                MaximumZoom = 3f
            };
            _fpsCounter = new FpsCounterAdvanced();

            _cursor = new Cursor();

            Window.AllowUserResizing = true;
            Window.Position = Point.Zero;
            Window.Title = $"Window: ({WINDOW_WIDTH}, {WINDOW_HEIGHT}) | Mouse visibility: {CURSOR_VISIBILITY}";
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //TODO: use this.Content to load your game content here 
            _icon = Content.Load<Texture2D>("Icon");
            _sprite = new Sprite(_icon) { Position = new Vector2(600, 240) };
            _fpsCounter.LoadContent(Content);
            _cursor.LoadContent(Content);

            _map = this.Content.Load<TiledMap>("maps/map2");
            _camera.LookAt(new Vector2(_map.WidthInPixels, _map.HeightInPixels) * 0.5f);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // For Mobile devices, this logic will close the Game when the Back button is pressed
            // Exit() is obsolete on iOS
            #if !__IOS__ &&  !__TVOS__
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            #endif
            
            // TODO: Add your update logic here

            var deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var keyboardState = Keyboard.GetState();
            var mouseState = Mouse.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape))
                Exit();

            const float cameraSpeed = 75f;
            const float zoomSpeed = 1f;
            var zoomFactor = zoomSpeed * deltaSeconds;

            if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up))
                _camera.Move(new Vector2(0, -cameraSpeed)*deltaSeconds);

            if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
                _camera.Move(new Vector2(-cameraSpeed, 0)*deltaSeconds);

            if (keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down))
                _camera.Move(new Vector2(0, cameraSpeed)*deltaSeconds);

            if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
                _camera.Move(new Vector2(cameraSpeed, 0)*deltaSeconds);

            if (keyboardState.IsKeyDown(Keys.R)) {
                _camera.ZoomIn(zoomFactor);
                _sprite.Scale = Vector2.Multiply(_sprite.Scale, 1 + zoomFactor);
            }

            if (keyboardState.IsKeyDown(Keys.F)) {
                _camera.ZoomOut(zoomFactor);
                _sprite.Scale = Vector2.Multiply(_sprite.Scale, 1 - zoomFactor);
            }

            _sprite.Rotation += MathHelper.ToRadians(100) * deltaSeconds;
            //_sprite.Position = _camera.ScreenToWorld(mouseState.X, mouseState.Y);
            var cursorMoveCommand = new MoveCommand(_cursor, new Vector2(mouseState.X, mouseState.Y));
            cursorMoveCommand.Execute();

            _fpsCounter.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
            
            // Game World
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp, 
                blendState: BlendState.AlphaBlend, 
                transformMatrix: _camera.GetViewMatrix());
            
            _map.Draw(_spriteBatch);
            _spriteBatch.Draw(_sprite);

            _spriteBatch.End();
            // End of Game World

            // Game UI
            _spriteBatch.Begin(
                samplerState: SamplerState.PointClamp, 
                blendState: BlendState.AlphaBlend
            );

            _fpsCounter.Draw(gameTime, _spriteBatch);
            _cursor.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();
            // End of Game UI

            base.Draw(gameTime);
        }
    }
}


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
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

        #region - Fields -
        private readonly GraphicsDeviceManager _graphics;
        private ViewportAdapter _viewportAdapter;
        private ComponentSubject _componentSubject;
        private SpriteBatch _spriteBatch;
        private Camera2D _camera;
        #endregion

        #region - Constructors -
        public VauxGame()
        {
            _graphics = new GraphicsDeviceManager(this) { SynchronizeWithVerticalRetrace = SYNCHRONIZE_WITH_VERTICAL_RETRACE };
            Content.RootDirectory = "Content";

            IsMouseVisible = CURSOR_VISIBILITY;
            IsFixedTimeStep = false;
        }
        #endregion
        
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            _viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, WINDOW_WIDTH, WINDOW_HEIGHT);
            _camera = new Camera2D(_viewportAdapter) {
                MinimumZoom = 1f,
                MaximumZoom = 3f
            };

            _componentSubject = new ComponentSubject();
            _componentSubject.AddComponent(new FpsCounterAdvanced())
                             .AddComponent(new Cursor())
                             .AddComponent(new WorldComponent(_camera));

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
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _componentSubject.LoadContent(Content);
            
            _camera.LookAt(new Vector2(_componentSubject.WorldComponent.WidthInPixels, _componentSubject.WorldComponent.HeightInPixels) * 0.5f);
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
            }

            if (keyboardState.IsKeyDown(Keys.F)) {
                _camera.ZoomOut(zoomFactor);
            }

            var cursorMoveCommand = new MoveCommand(_componentSubject.Cursor, new Vector2(mouseState.X, mouseState.Y));
            cursorMoveCommand.Execute();

            _componentSubject.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            _graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
            
            _componentSubject.Draw(gameTime, _spriteBatch, _camera);

            base.Draw(gameTime);
        }
    }
}


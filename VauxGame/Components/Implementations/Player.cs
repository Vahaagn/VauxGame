using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Shapes;
using VauxGame.Behaviors;
using VauxGame.Behaviors.Interfaces;
using VauxGame.Handlers;

namespace VauxGame.Components.Implementations
{
    public class Player : IComponent, IMovable
    {
        private const int WIDTH = 32;
        private const int HEIGHT = 48;

        #region - Fields -

        private IAnimation _animation;
        private readonly WorldComponent _world;

        #endregion

        #region - Properties -

        public RectangleF BoundingBox
            => new RectangleF(Position, new Vector2(WIDTH, HEIGHT) * GetScaleVector());

        #endregion

        public Player()
        {
            _spritePoint = Point.Zero;
            //Position = Vector2.Zero;
            Position = new Vector2(16*16, 8*16);
            _world = ComponentManager.Instance.GetInstance<WorldComponent>();

            RegisterEvents();

            _animation = new IdleAnimation();
        }

        public void Update(GameTime gameTime)
        {
            var moveVector = GetCalculatedVector(gameTime);

            if (!_world.Collisions.Any(c =>
                    c.Intersects(new RectangleF(Position + moveVector, GetCurrentSpriteRect().Size.ToVector2() * GetScaleVector()))))
            {
                Position += moveVector;
            }

            UpdateAnimation();
            _animation.Update(gameTime);

            _spritePoint = _animation.CurrentAnimation;
        }

        public bool IsVisible => true;

        public void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>("sprites/players/base");
        }

        public void UnloadContent(ContentManager content)
        {
            throw new NotImplementedException();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                texture: _texture,
                position: Position,
                sourceRectangle: GetCurrentSpriteRect(),
                scale: GetScaleVector()
            );

            // DEBUG 
            spriteBatch.DrawRectangle(Position, GetCurrentSpriteRect().Size.ToVector2() * GetScaleVector(), Color.BlueViolet);
        }

        public void MoveTo(Vector2 position)
        {
            Position = position;
        }

        #region - Private methods -

        private void UpdateAnimation()
        {
            if (IsUpPressed && !IsDownPressed && !(IsLeftPressed || IsRightPressed) && !(_animation is RunningBackAnimation))
                _animation = new RunningBackAnimation();
            else if (IsDownPressed && !IsUpPressed && !(IsLeftPressed || IsRightPressed) && !(_animation is RunningFrontAnimation))
                _animation = new RunningFrontAnimation();
            else if (IsLeftPressed && !IsRightPressed && !(_animation is RunningLeftAnimation))
                _animation = new RunningLeftAnimation();
            else if (IsRightPressed && !IsLeftPressed && !(_animation is RunningRightAnimation))
                _animation = new RunningRightAnimation();
            else if (!IsUpPressed && !IsDownPressed && !IsLeftPressed && !IsRightPressed && !(_animation is IdleAnimation))
                _animation = new IdleAnimation();
        }

        private Rectangle GetCurrentSpriteRect()
        {
            var spriteSize = new Point(WIDTH, HEIGHT);

            return new Rectangle(_spritePoint * spriteSize, spriteSize);
        }

        private Vector2 GetScaleVector()
        {
            return new Vector2(0.5f);
        }

        private void RegisterEvents()
        {
            var inputHandler = ComponentManager.Instance.GetInstance<InputHandler>();

            //inputHandler.MouseListener.MouseMoved += (sender, mouseEventArgs) => {
            //    _position = mouseEventArgs.Position - new Point(WIDTH / 2, HEIGHT / 2);
            //};

            inputHandler.KeyboardListener.KeyReleased += (sender, args) => {
                //Console.WriteLine($"[LOG] KeyReleased: {args.Key}");

                _keyStates[args.Key] = KeyState.Up;
            };

            inputHandler.KeyboardListener.KeyPressed += (sender, args) => {
                //Console.WriteLine($"[LOG] KeyPressed: {args.Key}");

                _keyStates[args.Key] = KeyState.Down;
            };
        }

        private Vector2 GetCalculatedVector(GameTime gameTime)
        {
            return GetDirectionVector() *
                   GetSpeedVector() *
                   GetDeltaVector(gameTime);
        }

        private Vector2 GetDirectionVector()
        {
            float xAxis = 0,
                yAxis = 0;

            if (IsLeftPressed && !IsRightPressed)
                xAxis = -1;
            else if (IsRightPressed && !IsLeftPressed)
                xAxis = 1;
            else if (IsUpPressed && !IsDownPressed)
                yAxis = -1;
            else if (IsDownPressed && !IsUpPressed)
                yAxis = 1;

            return new Vector2(xAxis, yAxis);
        }

        private Vector2 GetDeltaVector(GameTime gameTime)
        {
            return new Vector2(gameTime.GetElapsedSeconds());
        }

        private Vector2 GetSpeedVector()
        {
            return new Vector2(50);
        }

        private bool IsLeftPressed => Convert.ToBoolean(_keyStates[Keys.A]) | Convert.ToBoolean(_keyStates[Keys.Left]);
        private bool IsRightPressed => Convert.ToBoolean(_keyStates[Keys.D]) | Convert.ToBoolean(_keyStates[Keys.Right]);
        private bool IsUpPressed => Convert.ToBoolean(_keyStates[Keys.W]) | Convert.ToBoolean(_keyStates[Keys.Up]);
        private bool IsDownPressed => Convert.ToBoolean(_keyStates[Keys.S]) | Convert.ToBoolean(_keyStates[Keys.Down]);

        #endregion

        public ComponentTypeEnum ComponentType => ComponentTypeEnum.User;

        #region - Fields -

        private Texture2D _texture;
        private Point _spritePoint;
        public Vector2 Position { get; set; }

        private readonly Dictionary<Keys, KeyState> _keyStates =
            new Dictionary<Keys, KeyState>(Enum.GetValues(typeof(Keys))
                .Cast<Keys>()
                .ToDictionary(k => k, k => KeyState.Up));

        #endregion
    }
}
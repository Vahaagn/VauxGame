using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using VauxGame.Handlers;

namespace VauxGame.Components
{
    public class Cursor : IComponent, Components.IMovable
    {
        #region - Fields -
        public Vector2 Position { get; set; }
        private Vector2 _size;
        private Texture2D _texture;
        private InputHandler _inputHandler;
        #endregion

        #region - Properties -
        public bool IsVisible => true;
        public ComponentTypeEnum ComponentType => ComponentTypeEnum.GameInterface;
        #endregion

        #region - Constructors -
        public Cursor()
        {
            _size = new Vector2(25);
            _inputHandler = ComponentManager.Instance.GetInstance<InputHandler>();

            RegisterEvents();
        }
        #endregion

        #region - Public methods -
        public void MoveTo(Vector2 position)
        {
            Position = position;
        }

        public void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            _texture = content.Load<Texture2D>("gui/cursor-3-xxl");
        }

        public void UnloadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            throw new NotImplementedException();
        }

        public void Update(GameTime gameTime)
        {
            // TODO: Implement interactions with buttons
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var destinationRectangle = new Rectangle(Position.ToPoint(), _size.ToPoint());
            spriteBatch.Draw(_texture, destinationRectangle, Color.White);
        }
        #endregion

        #region - Private methods -

        private void RegisterEvents()
        {
            _inputHandler.MouseListener.MouseMoved += (sender, args) => {
                MoveTo(args.Position.ToVector2());
            };
        }
        #endregion
    }
}


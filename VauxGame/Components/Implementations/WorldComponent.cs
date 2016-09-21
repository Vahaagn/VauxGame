using System;
using MonoGame.Extended;
using MonoGame.Extended.Maps.Tiled;
using VauxGame.Components;

namespace VauxGame
{
    public class WorldComponent : IComponent
    {
        #region - Fields -

        private TiledMap _map;
        private readonly Camera2D _camera;

        #endregion

        #region - Properties -

        public ComponentTypeEnum ComponentType => ComponentTypeEnum.World;

        public bool IsVisible => true;

        public int WidthInPixels => _map.WidthInPixels;
        public int HeightInPixels => _map.HeightInPixels;

        #endregion

        #region - Constructors -

        public WorldComponent(Camera2D camera)
        {
            _camera = camera;
        }

        #endregion

        #region - Public methods -

        public void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            _map = content.Load<TiledMap>("maps/map2");
        }

        public void UnloadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            throw new NotImplementedException();
        }

        public void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            _map.Draw(spriteBatch, _camera);
        }

        public void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            //throw new NotImplementedException();
        }

        #endregion
    }
}


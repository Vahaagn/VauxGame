using System;
using VauxGame.Components;

namespace VauxGame
{
    public class WorldComponent : IComponent
    {
        #region - Fields -

        #endregion

        #region - Properties -

        public ComponentTypeEnum ComponentType => ComponentTypeEnum.World;

        public bool IsVisible => true;

        #endregion

        #region - Constructors -

        public WorldComponent()
        {
        }

        #endregion

        #region - Public methods -

        public void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            throw new NotImplementedException();
        }

        public void UnloadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            throw new NotImplementedException();
        }

        public void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        public void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}


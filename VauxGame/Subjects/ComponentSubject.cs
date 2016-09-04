using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace VauxGame
{
    public class ComponentSubject
    {
        #region - Fields -

        private List<DrawableGameComponent> _components = new List<DrawableGameComponent>();

        #endregion

        #region - Constructors -

        public ComponentSubject()
        {
            
        }

        #endregion

        #region - Public methods -

        public void Update(GameTime gameTime)
        {
            foreach (var component in _components) {
                component.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var component in _components) {
                component.Draw(gameTime);
            }
        }

        #endregion
    }
}


using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using VauxGame.Components;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended;

namespace VauxGame
{
    public class ComponentSubject
    {
        #region - Fields -
        private List<IComponent> _components = new List<IComponent>();
        #endregion

        #region - Constructors -
        public ComponentSubject()
        {            
        }
        #endregion

        #region - Properties -
        public Cursor Cursor => _components.OfType<Cursor>().SingleOrDefault();
        public WorldComponent WorldComponent => _components.OfType<WorldComponent>().SingleOrDefault();

        #endregion

        #region - Public methods -
        public ComponentSubject AddComponent(IComponent component)
        {
            if (_components.Contains(component))
                return this;

            _components.Add(component);

            return this;
        }

        public ComponentSubject RemoveComponent(IComponent component)
        {
            if (_components.Contains(component) == false)
                return this;

            _components.Remove(component);

            return this;
        }

        public void LoadContent(ContentManager contentManager)
        {
            foreach (var component in _components) {
                component.LoadContent(contentManager);
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (var component in _components) {
                component.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Camera2D camera)
        {
            DrawWorld(gameTime, spriteBatch, camera);
            DrawGameInterface(gameTime, spriteBatch);
        }
        #endregion

        #region - Private methods -
        private void DrawGameInterface(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(
                samplerState: SamplerState.PointClamp, 
                blendState: BlendState.AlphaBlend
            );

            foreach (var component in _components
                .Where(c => c.IsVisible && 
                            c.ComponentType == ComponentTypeEnum.GameInterface)
            ) {
                component.Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();
        }

        private void DrawWorld(GameTime gameTime, SpriteBatch spriteBatch, Camera2D camera)
        {
            spriteBatch.Begin(
                samplerState: SamplerState.PointClamp, 
                blendState: BlendState.AlphaBlend, 
                transformMatrix: camera.GetViewMatrix()
            );

            foreach (var component in _components.Where(c => c.IsVisible &&
                                                             c.ComponentType == ComponentTypeEnum.World)
            )
            {
                component.Draw(gameTime, spriteBatch);
            }

            //_map.Draw(_spriteBatch);
            //_spriteBatch.Draw(_sprite);

            spriteBatch.End();
        }
        #endregion
    }
}


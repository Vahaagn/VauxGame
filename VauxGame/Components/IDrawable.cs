using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace VauxGame.Components
{
    public interface IDrawable
    {
        void LoadContent(ContentManager content);
        void UnloadContent(ContentManager content);

        void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}


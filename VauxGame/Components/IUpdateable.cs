using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace VauxGame.Components
{
    public interface IUpdateable
    {
        void Update(GameTime gameTime);
    }
}


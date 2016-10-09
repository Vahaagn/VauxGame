using System;
using Microsoft.Xna.Framework;

namespace VauxGame.Components
{
    public interface IMovable
    {
        Vector2 Position { get; set; }

        void MoveTo(Vector2 position);
    }
}


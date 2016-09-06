using System;
using VauxGame.Components;

namespace VauxGame.Components
{
    public interface IComponent : IUpdateable, IDrawable
    {
        ComponentTypeEnum ComponentType { get; }
    }
}


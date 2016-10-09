using System;
using Microsoft.Xna.Framework;

namespace VauxGame.Behaviors.Interfaces
{
    public interface IAnimation : IBehavior
    {
        TimeSpan StepDuration { get; set; }
        int MaxSteps { get; set; }
        Point CurrentAnimation { get; }
    }
}

using System;
using Microsoft.Xna.Framework;

namespace VauxGame.Behaviors
{
    public class IdleAnimation : AnimationBehavior
    {
        protected override int VerticalPosition => 0;
        public override int MaxSteps => 1;
        public override Point CurrentAnimation => new Point(0, 0);

        public override void Update(GameTime gameTime)
        {

        }
    }
}
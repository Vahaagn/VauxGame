using Microsoft.Xna.Framework;

namespace VauxGame.Behaviors
{
    public class RunningLeftAnimation : AnimationBehavior
    {
        protected override int VerticalPosition => 2;
        public override Point CurrentAnimation { get; protected set; } = new Point(0, 2);
    }
}
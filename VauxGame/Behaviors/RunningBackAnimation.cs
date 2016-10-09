using Microsoft.Xna.Framework;

namespace VauxGame.Behaviors
{
    public class RunningBackAnimation : AnimationBehavior
    {
        protected override int VerticalPosition => 1;

        public override Point CurrentAnimation { get; protected set; } = new Point(0, 1);
    }
}
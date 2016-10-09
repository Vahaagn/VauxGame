using Microsoft.Xna.Framework;

namespace VauxGame.Behaviors
{
    public class RunningFrontAnimation : AnimationBehavior
    {
        protected override int VerticalPosition => 0;
        public override Point CurrentAnimation { get; protected set; } = new Point(0, 0);
    }
}
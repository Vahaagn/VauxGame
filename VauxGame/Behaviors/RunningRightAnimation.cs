using Microsoft.Xna.Framework;

namespace VauxGame.Behaviors
{
    class RunningRightAnimation : AnimationBehavior
    {
        protected override int VerticalPosition => 3;
        public override Point CurrentAnimation { get; protected set; } = new Point(0, 3);
    }
}

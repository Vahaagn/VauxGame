using System;
using Microsoft.Xna.Framework;
using VauxGame.Behaviors.Interfaces;

namespace VauxGame.Behaviors
{
    public abstract class AnimationBehavior : IAnimation
    {
        #region - Fields -

        private TimeSpan _elapsedTime;
        private int _currentStep;

        #endregion

        #region - Properties -

        protected virtual int VerticalPosition { get; }

        public virtual TimeSpan StepDuration { get; set; } = TimeSpan.FromMilliseconds(100);
        public virtual int MaxSteps { get; set; } = 8;
        public virtual Point CurrentAnimation { get; protected set; }

        #endregion

        public virtual void Update(GameTime gameTime)
        {
            _elapsedTime += gameTime.ElapsedGameTime;

            if (_elapsedTime >= StepDuration)
                PerformStep();
        }

        private void PerformStep()
        {
            _currentStep++;

            if (_currentStep >= MaxSteps)
                _currentStep = 0;

            CurrentAnimation = new Point(_currentStep, VerticalPosition);
            _elapsedTime = TimeSpan.Zero;
        }
    }
}
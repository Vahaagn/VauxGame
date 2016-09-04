using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;

using IUpdateable = VauxGame.Components.IUpdateable;
using IDrawable = VauxGame.Components.IDrawable;
using MonoGame.Extended.BitmapFonts;

namespace VauxGame
{
    public class FpsCounterAdvanced : IUpdateable, IDrawable
    {
        #region - Fields -
        private readonly Queue<float> _sampleBuffer = new Queue<float>();
        private BitmapFont _bitmapFont;
        #endregion

        #region - Properties -
        public long TotalFrames { get; private set; }
        public float AverageFramesPerSecond { get; private set; }
        public float CurrentFramesPerSecond { get; private set; }
        public int MaximumSamples { get; }
        #endregion

        #region - Constructors -
        public FpsCounterAdvanced(int maximumSamples = 100)
        {
            MaximumSamples = maximumSamples;
        }
        #endregion

        #region - Public methods -
        public void Reset()
        {
            TotalFrames = 0;
            _sampleBuffer.Clear();
        }

        public void Update(GameTime gameTime)
        {
            Update((float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        public void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            _bitmapFont = content.Load<BitmapFont>("fonts/montserrat-32");
        }

        public void UnloadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            throw new NotImplementedException();
        }

        public void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            //Update(gameTime);

            spriteBatch.DrawString(
                bitmapFont: _bitmapFont, 
                text: $"FPS: {AverageFramesPerSecond:0}", 
                position: Vector2.One, 
                color: Color.Goldenrod
            );
        }
        #endregion

        #region - Private methods -
        private void Update(float deltaTime)
        {
            CurrentFramesPerSecond = 1.0f / deltaTime;

            _sampleBuffer.Enqueue(CurrentFramesPerSecond);

            if (_sampleBuffer.Count > MaximumSamples)
            {
                _sampleBuffer.Dequeue();
                AverageFramesPerSecond = _sampleBuffer.Average(i => i);
            }
            else
            {
                AverageFramesPerSecond = CurrentFramesPerSecond;
            }

            TotalFrames++;
        }
        #endregion
    }
}


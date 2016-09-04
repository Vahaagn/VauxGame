using System;
using MonoGame.Extended.BitmapFonts;
using Microsoft.Xna.Framework;

using IUpdateable = VauxGame.Components.IUpdateable;
using IDrawable = VauxGame.Components.IDrawable;
using MonoGame.Extended;
using Microsoft.Xna.Framework.Graphics;

namespace VauxGame
{
    public class FpsCounter : IUpdateable, IDrawable
    {
        #region - Constants -
        private static readonly TimeSpan ONE_SECOND_TSTAMP = TimeSpan.FromSeconds(1);
        #endregion

        #region - Fields -
        private TimeSpan _timer = ONE_SECOND_TSTAMP;
        private int _framesCounter;
        private BitmapFont _bitmapFont;
        #endregion

        #region - Properties -
        public int FramesPerSecond { get; private set; }
        #endregion

        #region - Constructors -
        public FpsCounter()
        {
        }
        #endregion

        #region - Public methods -
        public void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            _timer += gameTime.ElapsedGameTime;

            if (_timer <= ONE_SECOND_TSTAMP)
                return;

            FramesPerSecond = _framesCounter;
            _framesCounter = 0;
            _timer -= ONE_SECOND_TSTAMP;
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
            _framesCounter++;

            spriteBatch.DrawString(
                bitmapFont: _bitmapFont, 
                text: $"FPS: {FramesPerSecond:0}", 
                position: Vector2.One, 
                color: Color.Goldenrod
            );
        }
        #endregion
    }
}


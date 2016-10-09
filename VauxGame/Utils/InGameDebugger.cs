using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;

namespace VauxGame.Utils
{
    public static class InGameDebugger
    {
        #region - Fields -

        private static SpriteBatch _spriteBatch;
        private static StringBuilder _stringBuilder;
        private static BitmapFont _bitmapFont;
#endregion

        public static void Initialize(SpriteBatch spriteBatch, BitmapFont bitmapFont)
        {
            _spriteBatch = spriteBatch;
            _bitmapFont = bitmapFont;

            _stringBuilder = new StringBuilder(50);
        }

        public static void Append(string text)
        {
            _stringBuilder.Append(text + "|");
        }

        public static void AppendLine(string text)
        {
            _stringBuilder.AppendLine(text);
        }

        public static void Flush()
        {
            _spriteBatch.Begin();
            _spriteBatch.DrawString(_bitmapFont, _stringBuilder.ToString(), new Vector2(0, 25), Color.GreenYellow);
            _spriteBatch.End();

            _stringBuilder.Clear();
        }
    }
}
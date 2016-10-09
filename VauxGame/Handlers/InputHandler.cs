using Microsoft.Xna.Framework;
using MonoGame.Extended.InputListeners;
using IUpdateable = VauxGame.Components.IUpdateable;

namespace VauxGame.Handlers
{
    public class InputHandler : IUpdateable
    {
        private readonly InputListenerComponent _inputListener;

        #region - Events -

        public MouseListener MouseListener { get; private set; }
        public KeyboardListener KeyboardListener { get; private set; }

        #endregion

        public InputHandler(Game game)
        {
            MouseListener = new MouseListener();
            KeyboardListener = new KeyboardListener();

            _inputListener = new InputListenerComponent(game, MouseListener, KeyboardListener);
        }

        public void Update(GameTime gameTime)
        {
            _inputListener.Update(gameTime);            
        }
    }
}

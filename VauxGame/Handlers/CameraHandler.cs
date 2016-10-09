using MonoGame.Extended;

namespace VauxGame.Handlers
{
    public class CameraHandler
    {
        #region - Fields -

        public Camera2D Camera { get; }
        private readonly InputHandler _inputHandler;
        private Components.IMovable _movableObject;

        #endregion

        #region - Constructors -

        public CameraHandler(Camera2D camera, InputHandler inputHandler)
        {
            Camera = camera;
            _inputHandler = inputHandler;
        }

        #endregion

        #region Public Methods -

        public void Handle()
        {
            Camera.LookAt(_movableObject.Position);
        }

        public void LookAt(Components.IMovable movableObject)
        {
            _movableObject = movableObject;
        }

        #endregion

        #region - Private methods -

        private void RegisterEvents()
        {
            
        }

        #endregion
    }
}
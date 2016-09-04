using System;
using Microsoft.Xna.Framework;
using VauxGame.Components;

namespace VauxGame.Commands
{
    public class MoveCommand : ICommand
    {
        private Vector2 _newPosition;
        private IMovable _movableObject;

        public MoveCommand(IMovable entity, Vector2 position)
        {
            _movableObject = entity;
            _newPosition = position;
        }

        public void Execute()
        {
            _movableObject.MoveTo(_newPosition);
        }
    }
}


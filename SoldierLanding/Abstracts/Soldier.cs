using SoldierLanding.Utils;

namespace SoldierLanding.Abstracts
{
    public class Soldier
    {
        private Coordinate _position;
        private Direction _direction;

        private bool _isAlive;

        public Soldier(Coordinate position, Direction direction)
        {
            _position = position;
            _direction = direction;

            _isAlive = true;
        }

        // Returns the soldier status in last known position, direction, and if the soldier has been lost.
        // e.g. "0 0 N" or "2 2 E LOST" if not alive.
        public override string ToString()
        {
            var output = _position + " " + _direction;

            if (!_isAlive)
            {
                output += " LOST";
            }

            return output;
        }

        public enum MoveDirection
        {
            Forward
        }

        public enum TurnDirection
        {
            Left,
            Right
        }

        public bool DoCommand(char command, Field field)
        {
            if (command == 'F')
                Move(Soldier.MoveDirection.Forward, field);
            else if (command == 'L')
                Turn(Soldier.TurnDirection.Left);
            else if (command == 'R')
                Turn(Soldier.TurnDirection.Right);

            return _isAlive;
        }

        private void Move(MoveDirection moveDirection, Field field)
        {
            if (!_isAlive) return;

            switch (moveDirection)
            {
                case MoveDirection.Forward:
                    MoveForward(field);
                    break;

                default:
                    throw new ArgumentException("Invalid Argument for Move Command.");
            }
        }

        private void MoveForward(Field field)
        {
            var destination = _position + _direction;

            TryMove(destination, field);
        }

        private void TryMove(Coordinate destination, Field field)
        {
            if (field.IsInsideBounds(destination))
            {
                _position = destination;
            }
            else
            {
                // Mark prevents the move off the edge.
                if (field.IsMark(_position)) return;

                // Walks off the edge.
                _isAlive = false;
                field.AddMark(_position);
            }
        }

        private void Turn(TurnDirection turnDirection)
        {
            if (!_isAlive) return;

            switch (turnDirection)
            {
                case TurnDirection.Left:
                    _direction.RotateAntiClockwise();
                    break;

                case TurnDirection.Right:
                    _direction.RotateClockwise();
                    break;

                default:
                    throw new ArgumentException("Invalid Argument for Turn Command.");
            }
        }
    }
}

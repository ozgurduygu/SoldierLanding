namespace SoldierLanding.Utils
{
    public struct Direction
    {
        public Coordinate coordinate;

        public Direction(Coordinate coordinate)
        {
            this.coordinate = coordinate;
        }

        public enum Cardinal
        {
            North,
            South,
            West,
            East
        }

        public Direction(Cardinal cardinal)
        {
            coordinate = cardinal switch
            {
                Cardinal.North => Coordinate.Up,
                Cardinal.South => Coordinate.Down,
                Cardinal.West => Coordinate.Left,
                Cardinal.East => Coordinate.Right,
                _ => throw new ArgumentException("Invalid Argument for Constructor."),
            };
        }

        public static readonly Direction North = new(Cardinal.North);
        public static readonly Direction South = new(Cardinal.South);
        public static readonly Direction West = new(Cardinal.West);
        public static readonly Direction East = new(Cardinal.East);

        // Rotates Direction 90 degrees clockwise.
        public Direction RotateClockwise()
        {
            this.coordinate.x *= -1;
            this.coordinate.Swap();
            return this;
        }

        // Rotates Direction 90 degrees anticlockwise.
        public Direction RotateAntiClockwise()
        {
            this.coordinate.y *= -1;
            this.coordinate.Swap();
            return this;
        }

        public static Direction Parse(string? shorthandCardinal)
        {
            if (shorthandCardinal == null)
            {
                throw new ArgumentNullException("Can't Parse Null into Coordinate.");
            }

            return shorthandCardinal switch
            {
                "N" => new Direction(Cardinal.North),
                "S" => new Direction(Cardinal.South),
                "W" => new Direction(Cardinal.West),
                "E" => new Direction(Cardinal.East),
                _ => throw new ArgumentException("Invalid Input for Direction Parser"),
            };
        }

        public override string? ToString()
        {
            if (this == North)
            {
                return "N";
            }
            else if (this == South)
            {
                return "S";
            }
            else if (this == West)
            {
                return "W";
            }
            else if (this == East)
            {
                return "E";
            }
            else
            {
                return coordinate.ToString();
            }
        }

        public bool Equals(Direction other)
        {
            return coordinate.Equals(other.coordinate);
        }

        public override bool Equals(object? obj)
        {
            if (this.GetType() != obj?.GetType()) return false;

            return this.Equals((Direction)obj);
        }

        public override int GetHashCode()
        {
            return coordinate.GetHashCode();
        }

        public static bool operator ==(Direction a, object? b) => a.Equals(b);
        public static bool operator !=(Direction a, object? b) => !a.Equals(b);

        public static Direction operator +(Direction a, Direction b) => new(a.coordinate + b.coordinate);
        public static Direction operator -(Direction a, Direction b) => new(a.coordinate - b.coordinate);
    }
}

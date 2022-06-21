namespace SoldierLanding.Utils
{
    public struct Coordinate
    {
        public int x, y;

        public Coordinate(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static readonly Coordinate Zero = new(0, 0);
        public static readonly Coordinate Up = new(0, 1);
        public static readonly Coordinate Down = new(0, -1);
        public static readonly Coordinate Left = new(-1, 0);
        public static readonly Coordinate Right = new(1, 0);

        // Swaps x and y values.
        public Coordinate Swap()
        {
            (this.y, this.x) = (this.x, this.y);
            return this;
        }

        // Parses a string with two space-separeted integers and constructs a Coordinate.
        // e.g. "5 5" => Coordinate(5 5).
        public static Coordinate Parse(string? input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("Can't Parse Null into Coordinate.");
            }

            string[] inputs = input.Split(' ');

            if (inputs.Length != 2)
                throw new ArgumentException("Takes two space-separated integers as input.");

            var x = int.Parse(inputs[0]);
            var y = int.Parse(inputs[1]);

            return new Coordinate(x, y);
        }

        // Returns a string with space-separated x and y values.
        // e.g. Coordinate(5 5) => "5 5".
        public override string ToString()
        {
            return this.x + " " + this.y;
        }

        public bool Equals(Coordinate other)
        {
            return this.x == other.x && this.y == other.y;
        }

        public override bool Equals(object? obj)
        {
            if (this.GetType() != obj?.GetType()) return false;

            return this.Equals((Coordinate)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y);
        }

        public static bool operator ==(Coordinate a, object? b) => a.Equals(b);
        public static bool operator !=(Coordinate a, object? b) => !a.Equals(b);

        public static explicit operator Coordinate(Direction direction) => direction.coordinate;

        public static Coordinate operator +(Coordinate a, Coordinate b) => new(a.x + b.x, a.y + b.y);
        public static Coordinate operator -(Coordinate a, Coordinate b) => new(a.x - b.x, a.y - b.y);

        public static Coordinate operator +(Coordinate a, Direction b) => new(a.x + b.coordinate.x, a.y + b.coordinate.y);
        public static Coordinate operator -(Coordinate a, Direction b) => new(a.x - b.coordinate.x, a.y - b.coordinate.y);

        public static bool operator <(Coordinate a, Coordinate b) => a.x < b.x || a.y < b.y;
        public static bool operator >(Coordinate a, Coordinate b) => a.x > b.x || a.y > b.y;
        public static bool operator <=(Coordinate a, Coordinate b) => a.x <= b.x && a.y <= b.y;
        public static bool operator >=(Coordinate a, Coordinate b) => a.x >= b.x && a.y >= b.y;
    }
}

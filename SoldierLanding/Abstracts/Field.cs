using SoldierLanding.Utils;

namespace SoldierLanding.Abstracts
{
    public class Field
    {
        public Coordinate size;

        private readonly List<Coordinate> _marks;

        public Field(Coordinate size)
        {
            this.size = size;
            _marks = new List<Coordinate>();
        }

        public bool IsInsideBounds(Coordinate coordinate)
        {
            return coordinate >= Coordinate.Zero && coordinate <= size;
        }

        public void AddMark(Coordinate coordinate)
        {
            if (!IsInsideBounds(coordinate))
            {
                throw new ArgumentOutOfRangeException("Mark coordinate must be in bounds.");
            }

            _marks.Add(coordinate);
        }

        public bool IsMark(Coordinate coordinate)
        {
            if (!IsInsideBounds(coordinate)) return false;

            return _marks.Contains(coordinate);   
        }
    }
}

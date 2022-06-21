using SoldierLanding.Abstracts;
using SoldierLanding.Utils;

namespace SoldierLanding.Tests
{
    public class FieldTests
    {
        [Fact]
        public void FieldConstructorTest()
        {
            var field = new Field(new Coordinate(0, 0));
            Assert.NotNull(field);
            Assert.IsType<Field>(field);
            Assert.Equal(Coordinate.Zero, field.size);
        }

        [Theory]
        [InlineData(0, 0, 0, 0, true)]
        [InlineData(50, 50, 50, 50, true)]
        [InlineData(-1, -1, 0, 0, false)]
        [InlineData(-1, -1, -1, -1, false)]
        [InlineData(int.MaxValue, int.MaxValue, 0, 0, true)]
        [InlineData(int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue, true)]
        [InlineData(int.MinValue, int.MinValue, int.MinValue, int.MinValue, false)]
        [InlineData(int.MinValue, int.MinValue, int.MaxValue, int.MaxValue, false)]
        [InlineData(int.MaxValue, int.MaxValue, int.MinValue, int.MinValue, false)]
        [InlineData(int.MinValue, int.MaxValue, int.MinValue, int.MaxValue, false)]
        [InlineData(int.MaxValue, int.MinValue, int.MaxValue, int.MinValue, false)]
        [InlineData(int.MinValue, int.MaxValue, int.MaxValue, int.MinValue, false)]
        [InlineData(int.MaxValue, int.MinValue, int.MinValue, int.MaxValue, false)]
        public void FieldBoundsTest(int fieldX, int fieldY, int checkX, int checkY, bool expected)
        {
            var fieldSize = new Coordinate(fieldX, fieldY);
            var field = new Field(fieldSize);

            var coordinates = new Coordinate(checkX, checkY);
            Assert.Equal(expected, field.IsInsideBounds(coordinates));
        }

        [Theory]
        [InlineData(0, 0, 0, 0, true)]
        [InlineData(0, 0, 0, 1, false)]
        [InlineData(0, 0, 1, 1, false)]
        [InlineData(0, 0, int.MaxValue, int.MaxValue, false)]
        [InlineData(0, 0, int.MinValue, int.MinValue, false)]
        [InlineData(0, 0, int.MinValue, int.MaxValue, false)]
        [InlineData(0, 0, int.MaxValue, int.MinValue, false)]
        [InlineData(5, 5, int.MaxValue, int.MaxValue, false)]
        [InlineData(5, 5, int.MinValue, int.MinValue, false)]
        [InlineData(5, 5, int.MaxValue, int.MinValue, false)]
        [InlineData(5, 5, int.MinValue, int.MaxValue, false)]
        public void FieldMarksTest(int markX, int markY, int checkX, int checkY, bool expected)
        {
            var fieldSize = new Coordinate(5, 5);
            var field = new Field(fieldSize);
            field.AddMark(new Coordinate(markX, markY));
            
            var coordinates = new Coordinate(checkX, checkY);
            Assert.Equal(expected, field.IsMark(coordinates));
        }

        [Theory]
        [InlineData(0, 0, 0, 0, true)]
        [InlineData(0, 0, 1, 1, true)]
        [InlineData(5, 5, 5, 5, true)]
        [InlineData(50, 50, 0, 0, true)]
        public void FieldNotMarkTest(int fieldX, int fieldY, int checkX, int checkY, bool expected)
        {
            var fieldSize = new Coordinate(fieldX, fieldY);
            var field = new Field(fieldSize);
            
            // Mark all the coordinates except for the check coordinates.
            for(int i = 0; i < fieldSize.x; i++)
            {
                for(int j = 0; j < fieldSize.y; j++)
                {
                    if (i == checkX && j == checkY) continue;

                    var coordinate = new Coordinate(i, j);
                    field.AddMark(coordinate);
                    Assert.True(field.IsMark(coordinate));
                }
            }

            var coordinates = new Coordinate(checkX, checkY);
            Assert.NotEqual(expected, field.IsMark(coordinates));
        }
    }
}
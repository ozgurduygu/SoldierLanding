using SoldierLanding.Utils;

namespace SoldierLanding.Tests
{
    public class TypeTests
    {
        public class CoordinateTests
        {
            [Fact]
            public void CoordinateConstantsTest()
            {
                Assert.Equal(Coordinate.Zero, new Coordinate(0, 0));
                Assert.Equal(Coordinate.Left, new Coordinate(-1, 0));
                Assert.Equal(Coordinate.Right, new Coordinate(1, 0));
                Assert.Equal(Coordinate.Up, new Coordinate(0, 1));
                Assert.Equal(Coordinate.Down, new Coordinate(0, -1));
            }

            [Fact]
            public void CoordinateSwapTest()
            {
                Assert.Equal(Coordinate.Zero, Coordinate.Zero.Swap());
                Assert.Equal(Coordinate.Up, Coordinate.Right.Swap());
                Assert.Equal(Coordinate.Down, Coordinate.Left.Swap());
            }

            [Fact]
            public void CoordinateOperatorsTest()
            {
                Assert.Equal(Coordinate.Zero, Coordinate.Left - Coordinate.Left);
                Assert.Equal(Coordinate.Zero, Coordinate.Up - Direction.North);
                Assert.Equal(Coordinate.Zero, Coordinate.Left + Coordinate.Right);
                Assert.Equal(Coordinate.Zero, Coordinate.Up + Coordinate.Down);

                Assert.True(Coordinate.Up > Coordinate.Down);
                Assert.True(Coordinate.Down < Coordinate.Left);
                Assert.True(Coordinate.Left < Coordinate.Down);
                Assert.False(Coordinate.Zero < Coordinate.Left);
                Assert.False(Coordinate.Zero < new Coordinate(0, 0));

                Assert.True(Coordinate.Up >= Coordinate.Down);
                Assert.False(Coordinate.Down <= Coordinate.Left);
                Assert.False(Coordinate.Down >= Coordinate.Left);
                Assert.True(Coordinate.Zero <= new Coordinate(0, 0));
                Assert.False(Coordinate.Zero <= Coordinate.Left);

                Assert.True(Coordinate.Left == new Coordinate(-1, 0));
                Assert.False(Coordinate.Zero == Coordinate.Left);
                Assert.False(Coordinate.Zero == "Foo");
                Assert.True(Coordinate.Zero != "Foo");
                Assert.False(Coordinate.Left != new Coordinate(-1, 0));

                Assert.NotEqual(Coordinate.Left + Direction.North, Coordinate.Left);
            }

            [Theory]
            [InlineData("0 0", 0, 0)]
            [InlineData("5 8", 5, 8)]
            [InlineData("-1 -1", -1, -1)]
            public void CoordinateParseTest(string input, int x, int y)
            {
                var coordinate = new Coordinate(x, y);
                var parsedCoordinate = Coordinate.Parse(input);

                Assert.Equal(coordinate, parsedCoordinate);
            }

            [Theory]
            [InlineData("1-1", typeof(ArgumentException))]
            [InlineData("1 1 1", typeof(ArgumentException))]
            [InlineData("1, 1", typeof(FormatException))]
            [InlineData("", typeof(ArgumentException))]
            [InlineData(null, typeof(ArgumentNullException))]
            public void CoordinateParseExceptionTest(string input, Type exceptionType)
            {
                try
                {
                    Coordinate.Parse(input);
                }
                catch (Exception exception)
                {
                    Assert.Equal(exception.GetType(), exceptionType);
                }
            }

            [Theory]
            [InlineData(0, 0, "0 0")]
            [InlineData(-1, -1, "-1 -1")]
            [InlineData(int.MinValue, int.MaxValue, "-2147483648 2147483647")]
            public void CoordinateToStringTest(int x, int y, string expected)
            {
                var output = new Coordinate(x, y).ToString();

                Assert.Equal(expected, output);
            }
        }


        public class DirectionTests
        {
            [Fact]
            public void DirectionConstantsTest()
            {
                Assert.Equal((Coordinate)Direction.North, Coordinate.Up);
                Assert.Equal((Coordinate)Direction.South, Coordinate.Down);
                Assert.Equal((Coordinate)Direction.West, Coordinate.Left);
                Assert.Equal((Coordinate)Direction.East, Coordinate.Right);
            }

            [Fact]
            public void DirectionRotateTest()
            {
                Assert.Equal(Direction.North, Direction.West.RotateClockwise());
                Assert.Equal(Direction.North, Direction.East.RotateAntiClockwise());
                Assert.Equal(Direction.West, Direction.South.RotateClockwise());
                Assert.Equal(Direction.East, Direction.South.RotateAntiClockwise());
            }

            [Fact]
            public void DirectionOperatorsTest()
            {
                Assert.Equal((Coordinate)Direction.North, Coordinate.Zero - (Coordinate)Direction.South);
                Assert.Equal(Coordinate.Zero, (Coordinate)(Direction.South - Direction.South));
                Assert.Equal(Coordinate.Zero, (Coordinate)(Direction.West + Direction.East));
                Assert.Equal(Coordinate.Zero, (Coordinate)(Direction.North + Direction.South));

                Assert.True(Direction.West == new Direction(new Coordinate(-1, 0)));
                Assert.False(Coordinate.Zero == Direction.North);
                Assert.False(Direction.South == "Foo");
                Assert.True(Direction.South != "Foo");
                Assert.False((Coordinate)Direction.West != new Coordinate(-1, 0));
                Assert.True((Coordinate)Direction.West != new Coordinate(1, 0));
            }

            [Theory]
            [InlineData("N", 0, 1)]
            [InlineData("S", 0, -1)]
            [InlineData("W", -1, 0)]
            [InlineData("E", 1, 0)]
            public void DirectionParseTest(string input, int x, int y)
            {
                var direction = new Direction(new Coordinate(x, y));
                var parsedDirection = Direction.Parse(input);

                Assert.Equal(direction, parsedDirection);
            }

            [Theory]
            [InlineData("Ñ", typeof(ArgumentException))]
            [InlineData("Ş", typeof(ArgumentException))]
            [InlineData("w", typeof(ArgumentException))]
            [InlineData("Ê", typeof(ArgumentException))]
            [InlineData(null, typeof(ArgumentNullException))]
            public void DirectionParseExceptionTest(string input, Type exceptionType)
            {
                try
                {
                    Direction.Parse(input);
                }
                catch (Exception exception)
                {
                    Assert.Equal(exception.GetType(), exceptionType);
                }
            }

            [Theory]
            [InlineData(0, -1, "S")]
            [InlineData(-1, 0, "W")]
            [InlineData(0, 1, "N")]
            public void DirectionToStringTest(int x, int y, string expected)
            {
                var output = new Direction(new Coordinate(x, y)).ToString();

                Assert.Equal(expected, output);
            }
        }
    }
}
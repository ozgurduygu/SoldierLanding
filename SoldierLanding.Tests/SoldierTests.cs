using SoldierLanding.Abstracts;
using SoldierLanding.Utils;

namespace SoldierLanding.Tests
{
    public class SoldierTests
    {
        [Fact]
        public void SoldierConstructorTest()
        {
            var soldier = new Soldier(Coordinate.Zero, Direction.South);
            Assert.NotNull(soldier);
            Assert.IsType<Soldier>(soldier);

            Assert.Equal("0 0 W", new Soldier(Coordinate.Zero, Direction.West).ToString());
            Assert.Equal("-1 -1 E", new Soldier(new Coordinate(-1, -1), new Direction(new Coordinate(1, 0))).ToString());
        }

        [Fact]
        public void SoldierCommandTest()
        {
            var field = new Field(new Coordinate(5, 5));
            var soldier = new Soldier(Coordinate.Zero, Direction.North);

            soldier.DoCommand('F', field);
            Assert.Equal("0 1 N", soldier.ToString());

            soldier.DoCommand('R', field);
            Assert.Equal("0 1 E", soldier.ToString());

            soldier.DoCommand('F', field);
            Assert.Equal("1 1 E", soldier.ToString());

            soldier.DoCommand('L', field);
            soldier.DoCommand('L', field);
            Assert.Equal("1 1 W", soldier.ToString());
        }
    }
}
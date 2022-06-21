using SoldierLanding.Utils;

namespace SoldierLanding.Tests
{
    public class ApplicationTest
    {
        [Fact]
        public void SampleInputTest()
        {
            var field = Input.HandleFieldInput("4 3");

            var soldier = Input.HandleSoldierLandingInput("2 1 E");
            soldier = Input.HandleSoldierCommandsInput("FRFRFLF", soldier, field);
            Assert.Equal("2 0 S LOST", soldier.ToString());

            soldier = Input.HandleSoldierLandingInput("2 2 S");
            soldier = Input.HandleSoldierCommandsInput("FLFRFRF", soldier, field);
            Assert.Equal("2 0 W", soldier.ToString());

            soldier = Input.HandleSoldierLandingInput("0 0 N");
            soldier = Input.HandleSoldierCommandsInput("FFFRFFRF", soldier, field);
            Assert.Equal("2 2 S", soldier.ToString());
        }

        [Fact]
        public void CornersPreventLossesTest()
        {
            var field = Input.HandleFieldInput("0 0");

            var soldier = Input.HandleSoldierLandingInput("0 0 N");
            soldier = Input.HandleSoldierCommandsInput("F", soldier, field);
            Assert.Equal("0 0 N LOST", soldier.ToString());

            soldier = Input.HandleSoldierLandingInput("0 0 S");
            soldier = Input.HandleSoldierCommandsInput("FFFFFFF", soldier, field);
            Assert.Equal("0 0 S", soldier.ToString());

            soldier = Input.HandleSoldierLandingInput("0 0 S");
            soldier = Input.HandleSoldierCommandsInput("FLFLF", soldier, field);
            Assert.Equal("0 0 N", soldier.ToString());
        }

        [Fact]
        public void CornersAllowLossesTest()
        {
            var field = Input.HandleFieldInput("1 1");

            var soldier = Input.HandleSoldierLandingInput("1 1 N");
            soldier = Input.HandleSoldierCommandsInput("F", soldier, field);
            Assert.Equal("1 1 N LOST", soldier.ToString());

            soldier = Input.HandleSoldierLandingInput("0 0 S");
            soldier = Input.HandleSoldierCommandsInput("F", soldier, field);
            Assert.Equal("0 0 S LOST", soldier.ToString());

            soldier = Input.HandleSoldierLandingInput("0 1 W");
            soldier = Input.HandleSoldierCommandsInput("F", soldier, field);
            Assert.Equal("0 1 W LOST", soldier.ToString());

            soldier = Input.HandleSoldierLandingInput("1 0 E");
            soldier = Input.HandleSoldierCommandsInput("F", soldier, field);
            Assert.Equal("1 0 E LOST", soldier.ToString());
        }
    }
}
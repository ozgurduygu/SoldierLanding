using SoldierLanding.Abstracts;

namespace SoldierLanding.Utils
{
    public class Input
    {
        public enum InputType
        {
            Field,
            SoldierLanding,
            SoldierCommands
        }

        public static object ListenFor(InputType inputType, Soldier soldier, Field field)
        {
            if (inputType != InputType.SoldierCommands)
            {
                return ListenFor(inputType);
            }
            else
            {
                var input = ReadInput();
                return HandleSoldierCommandsInput(input, soldier, field);
            }
        }

        public static object ListenFor(InputType inputType)
        {
            if (inputType == InputType.SoldierCommands)
            {
                throw new NotImplementedException("Soldier Commands Input type must take 3 arguments.");
            }

            var input = ReadInput();

            if (inputType == InputType.Field)
            {
                return HandleFieldInput(input);
            }
            else if (inputType == InputType.SoldierLanding)
            {
                return HandleSoldierLandingInput(input);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        private static string ReadInput()
        {
            string? input = Console.ReadLine()?.ToUpper();

            if (input == null)
            {
                throw new ArgumentNullException("Input cannot be null");
            }
            else if (input.Length > 100)
            {
                throw new ArgumentOutOfRangeException("Input must not be more than 100 characters.");
            }

            return input;
        }

        public static Field HandleFieldInput(string input)
        {
            var fieldSize = Coordinate.Parse(input);

            return new Field(fieldSize);
        }

        public static Soldier HandleSoldierLandingInput(string input)
        {
            string[] soldierDropInputs = input.Split(' ');

            var soldierCoords = Coordinate.Parse(soldierDropInputs[0] + ' ' + soldierDropInputs[1]);
            var soldierDirection = Direction.Parse(soldierDropInputs[2]);
            
            return new Soldier(soldierCoords, soldierDirection);
        }

        public static Soldier HandleSoldierCommandsInput(string input, Soldier soldier, Field field)
        {
            foreach (var command in input)
            {
                var isAlive = soldier.DoCommand(command, field);

                if (!isAlive) break;
            }

            return soldier;
        }
    }
}

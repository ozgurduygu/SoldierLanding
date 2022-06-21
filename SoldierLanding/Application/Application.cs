using SoldierLanding.Abstracts;
using SoldierLanding.Utils;

namespace SoldierLanding
{
    public class Application
    {
        private static void Main()
        {
            // Read field creation input and construct the field.
            var field = (Field) Input.ListenFor(Input.InputType.Field);

            while(true)
            {
                // Read soldier landing input construct the soldier.
                var soldier = (Soldier) Input.ListenFor(Input.InputType.SoldierLanding);

                // Read soldier commands and process.
                Input.ListenFor(Input.InputType.SoldierCommands, soldier, field);

                // Output soldier status.
                Console.WriteLine(soldier);
            }
        }
    }
}
using System;
using System.Linq;
using CommandSystem;
using modifiers.modifiers;

namespace modifiers.commands.config
{

    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    public class SetBallOnDeathChanceCommand : ICommand
    {
        public string Command => "modifiers_config_ballondeath_set_chance";

        public string[] Aliases => new string[] { };

        public string Description => "Sets the percent chance that SCP 018 will be spawned when somebody dies";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (sender.CheckPermission(PlayerPermissions.FacilityManagement))
            {
                if (arguments.Count != 1)
                {
                    response = "Please provide the percent chance that SCP 018 will be spawned when somebody dies.";
                    return false;
                }

                if (!float.TryParse(arguments.ElementAt(0), out float percentChance))
                {
                    response = "Unable to parse percent chance that SCP 018 will be spawned when somebody dies. Please provide decimal value between 0 and 100.";
                    return false;
                }

                if (percentChance < 0 || percentChance > 100)
                {
                    response = "Percent chance must be between 0 and 100";
                    return false;
                }

                BallOnDeath.Instance.PercentChance = percentChance;
                response = "Set Chance to " + percentChance.ToString();

                return true;
            }

            response = "You don't have enough permission to run this command";
            return false;
        }
    }

}
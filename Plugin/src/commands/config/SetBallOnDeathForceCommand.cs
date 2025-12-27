using System;
using System.Linq;
using CommandSystem;
using modifiers.modifiers;

namespace modifiers.commands.config
{

    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    public class SetBallOnDeathForceCommand : ICommand
    {
        public string Command => "modifiers_config_ballondeath_set_force";

        public string[] Aliases => new string[] { };

        public string Description => "Sets the force with which SCP 018 is thrown after someone dies";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (sender.CheckPermission(PlayerPermissions.FacilityManagement))
            {
                if (arguments.Count != 1)
                {
                    response = "Please provide the with force which SCP 018 is thrown.";
                    return false;
                }

                if (!float.TryParse(arguments.ElementAt(0), out float force))
                {
                    response = "Unable to parse force with which SCP 018 is thrown. Please provide decimal value.";
                    return false;
                }

                BallOnDeath.Instance.LaunchForce = force;
                response = "Set Force to " + force.ToString();

                return true;
            }

            response = "You don't have enough permission to run this command";
            return false;
        }
    }

}
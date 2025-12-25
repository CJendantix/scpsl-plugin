using System;
using System.Linq;
using CommandSystem;

[CommandHandler(typeof(RemoteAdminCommandHandler))]
[CommandHandler(typeof(GameConsoleCommandHandler))]
public class ChangeDefaultGravityCommand : ICommand
{
    public string Command => "modifiers_ballondeath_enable";

    public string[] Aliases => new string[] { };

    public string Description => "Enables the BallOnDeath modifier, which spawns SCP 018 every time someone dies";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (sender.CheckPermission(PlayerPermissions.FacilityManagement))
        {
            if (arguments.Count != 1)
            {
                response = "Please provide whether to enable or disable the plugin (true/false)";
                return false;
            }

            if (!bool.TryParse(arguments.ElementAt(0), out bool enable))
            {
                response = "Unable to parse whether to enable or disable the plugin. Please provide boolean value (true/false).";
                return false;
            }

            if (BallOnDeath.Instance.IsEnabled == enable)
            {
                response = "BallOnDeath is already " + (enable ? "enabled." : "disabled.");
                return false;
            }

            if (enable)
            {
                BallOnDeath.Instance.Enable();
            }
            else
            {
                BallOnDeath.Instance.Disable();
            }
            ;

            response = (enable ? "Enabled Modifier: " : "Disabled Modifier: ") + "BallOnDeath";

            return true;
        }

        response = "You don't have enough permission to run this command";
        return false;
    }
}
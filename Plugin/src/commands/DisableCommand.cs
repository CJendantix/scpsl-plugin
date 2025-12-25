using System;
using System.Linq;
using CommandSystem;

[CommandHandler(typeof(RemoteAdminCommandHandler))]
[CommandHandler(typeof(GameConsoleCommandHandler))]
public class DisableCommand : ICommand
{
    public string Command => "modifiers_disable";

    public string[] Aliases => new string[] { };

    public string Description => "Disables a modifier";

    public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
    {
        if (sender.CheckPermission(PlayerPermissions.FacilityManagement))
        {
            if (arguments.Count != 1)
            {
                response = "Please provide the name of which modifier to disable. List modifiers with modifiers_list.";
                return false;
            }
            
            Modifier modifier = null;
            foreach (var m in Modifier.Modifiers) {
                if (arguments.ElementAt(0) == m.Name)
                    modifier = m;
            }

            if (modifier == null)
            {
                response = "Invalid modifier name provided. List valid modifiers with modifiers_list.";
                return false;
            }

            if (!modifier.IsEnabled)
            {
                response = modifier.Name + " is not enabled.";
                return false;
            }
            
            BallOnDeath.Instance.Disable();

            response = "Disabled Modifier: " + modifier.Name;

            return true;
        }

        response = "You don't have enough permission to run this command";
        return false;
    }
}
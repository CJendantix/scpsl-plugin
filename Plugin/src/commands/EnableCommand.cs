using System;
using System.Linq;
using CommandSystem;

namespace modifiers.commands
{

    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    public class EnableCommand : ICommand, IUsageProvider
    {
        public string Command => "modifiers_enable";

        public string[] Aliases => new string[] { };

        public string Description => "Enables a modifier";

        public string[] Usage => new string[] { "Modifier Name" };

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (sender.CheckPermission(PlayerPermissions.FacilityManagement))
            {
                if (arguments.Count != 1)
                {
                    response = "Please provide the name of which modifier to enable. List modifiers with modifiers_list.";
                    return false;
                }

                Modifier modifier = null;
                foreach (var m in Modifier.Modifiers)
                {
                    if (arguments.ElementAt(0) == m.Name)
                        modifier = m;
                }

                if (modifier == null)
                {
                    response = "Invalid modifier name provided. List valid modifiers with modifiers_list.";
                    return false;
                }

                if (modifier.IsEnabled)
                {
                    response = modifier.Name + " is already enabled.";
                    return false;
                }

                modifier.Enable();

                response = "Enabled Modifier: " + modifier.Name;

                return true;
            }

            response = "You don't have enough permission to run this command";
            return false;
        }
    }

}
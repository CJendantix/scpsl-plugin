using System;
using System.Collections.Generic;
using System.Linq;
using CommandSystem;
using InventorySystem.Items.Firearms.Modules;

namespace modifiers.commands
{

    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    public class ListCommand : ICommand
    {
        public string Command => "modifiers_list";

        public string[] Aliases => new string[] { };

        public string Description => "Lists valid modifiers";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (sender.CheckPermission(PlayerPermissions.FacilityManagement))
            {
                response = "Valid Modifiers:";
                foreach (var m in Modifier.Modifiers)
                {
                    response += "\n - " + m.Name;
                }

                return true;
            }

            response = "You don't have enough permission to run this command";
            return false;
        }
    }

}
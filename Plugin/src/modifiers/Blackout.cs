using LabApi.Events.Arguments.PlayerEvents;
using PlayerRoles;

[AutoModifier]
public sealed partial class Blackout : Modifier
{
    public override string Name => "Blackout";

    public override void OnServerRoundStarted()
    {
        foreach (RoomLightController instance in RoomLightController.Instances)
        {
            instance.ServerFlickerLights(1000000000);
        }
    }

    public override void OnPlayerChangedRole(PlayerChangedRoleEventArgs ev)
    {
        if (!ev.NewRole.RoleTypeId.IsHuman())
            return;

        ev.Player.AddItem(ItemType.Flashlight);
    }
}
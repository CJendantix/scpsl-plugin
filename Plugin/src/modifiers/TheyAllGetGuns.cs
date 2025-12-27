using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Features.Extensions;

[AutoModifier]
public sealed partial class TheyAllGetGuns : Modifier
{
    public override string Name => "TheyAllGetGuns";

    public override void OnPlayerChangedRole(PlayerChangedRoleEventArgs ev)
    {
        if (ev.NewRole.RoleTypeId.IsMilitary())
            return;

        ev.Player.AddItem(ItemType.GunCOM15);
        ev.Player.AddAmmo(ItemType.Ammo9x19, 12);
    }
}
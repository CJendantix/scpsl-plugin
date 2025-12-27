using LabApi.Events.Arguments.PlayerEvents;
using PlayerRoles;
using UnityEngine;

[AutoModifier]
public sealed partial class FunSize : Modifier
{
    public override string Name => "FunSize";

    public override void OnPlayerChangedRole(PlayerChangedRoleEventArgs ev)
    {
        if (!ev.NewRole.RoleTypeId.IsAlive())
            return;

        ev.Player.Scale = GetScale();
    }

    private static Vector3 GetScale()
    {
        return Vector3.one * Random.Range(0.4f, 0.6f);
    }

}
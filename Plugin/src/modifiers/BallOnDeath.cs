using Footprinting;
using InventorySystem;
using InventorySystem.Items;
using InventorySystem.Items.Pickups;
using InventorySystem.Items.ThrowableProjectiles;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Features.Wrappers;
using Mirror;
using UnityEngine;
using UnityEngine.Assertions;
using Object = UnityEngine.Object;
using ThrowableItem = InventorySystem.Items.ThrowableProjectiles.ThrowableItem;

[AutoModifier(nameof(Instance))]
public sealed class BallOnDeath : Modifier
{
    private static readonly ThrowableItem SCP018;

    static BallOnDeath()
    {
        var succeeded = InventoryItemLoader.TryGetItem(ItemType.SCP018, out ThrowableItem item);
        Assert.IsTrue(succeeded);
        SCP018 = item;
    }

    public static BallOnDeath Instance { get; } = new BallOnDeath();
    private BallOnDeath() { }

    public override string Name => "BallOnDeath";

    [Configurable(Description = "Force applied to SCP-018", Default = 75f)]
    public float LaunchForce { get; set; }

    [Configurable(Description = "Chance percent", Default = 50f)]
    public float PercentChance { get; set; }

    private void ThrowSCP018(Player player, float force)
    {
        ThrownProjectile projectile = Object.Instantiate(SCP018.Projectile, player.Position, player.Rotation);

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 forward = player.ReferenceHub.transform.forward;
            rb.linearVelocity = forward * force;
        }

        PickupSyncInfo psi = new PickupSyncInfo(ItemType.SCP018, SCP018.Weight, ItemSerialGenerator.GenerateNext())
        {
            Locked = true
        };

        projectile.Info = psi;
        projectile.PreviousOwner = new Footprint(player.ReferenceHub);
        NetworkServer.Spawn(projectile.gameObject);
    }

    public override void OnPlayerDying(PlayerDyingEventArgs ev)
    {
        if (Random.value >= PercentChance / 100f)
            return;

        ThrowSCP018(ev.Player, LaunchForce);
    }
}

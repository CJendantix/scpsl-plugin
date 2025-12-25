

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
    public static BallOnDeath Instance { get; } = new BallOnDeath();

    private BallOnDeath() {}

    private static readonly ThrowableItem SCP018;

    static BallOnDeath()
    {
        // ItemType.SCP018 is a ThrowableItem
        var succeeded = InventoryItemLoader.TryGetItem(ItemType.SCP018, out ThrowableItem item);
        Assert.IsTrue(succeeded);

        SCP018 = item;
    }

    public override string Name => "BallOnDeath";

    public float Force { get; set; } = 0;

    private void ThrowSCP018(Player player, float force)
    {
        ThrownProjectile projectile = Object.Instantiate(SCP018.Projectile, player.Position, player.Rotation);

        // Add initial velocity here
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Throw forward from the player's perspective
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
        ThrowSCP018(ev.Player, Force);
    }

    public override void LoadConfig(Configuration config)
    {
        if (config.BallOnDeath.Enable) {
            Enable();
        }
        Force = config.BallOnDeath.LaunchForce;
    }

    public override void SaveConfig(Configuration config)
    {
        config.BallOnDeath.Enable = IsEnabled;
        config.BallOnDeath.LaunchForce = Force;
    }
}

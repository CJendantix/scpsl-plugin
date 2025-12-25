using System;
using LabApi.Events.Handlers;
using LabApi.Features;
using LabApi.Features.Console;
using LabApi.Loader.Features.Plugins;

public class CJsModifiers : Plugin<Configuration>
{
    // The name of the plugin
    public override string Name { get; } = "CJ's Modifiers";

    // The description of the plugin
    public override string Description { get; } = "Extensible Modifier Framework for SCP: Secret Laboratory";

    // The author of the plugin
    public override string Author { get; } = "CJendantix";

    // The current version of the plugin
    public override Version Version { get; } = new Version(1, 0, 0, 0);

    // The required version of LabAPI (usually the version the plugin was built with)
    public override Version RequiredApiVersion { get; } = new Version(LabApiProperties.CompiledVersion);

    private void EnableModifiers()
    {
        if (Config.BallOnDeath.Enable)
        {
            BallOnDeath.Instance.Enable();
        }
    }

    private void DisableModifiers()
    {
        foreach (var modifier in Modifier.Modifiers) {
            if (modifier.IsEnabled) {
                modifier.Disable();
            }
        }
    }

    private void LoadConfig()
    {
        BallOnDeath.Instance.Force = Config.BallOnDeath.LaunchForce;
    }

    private void UpdateConfig()
    {
        Config.BallOnDeath.Enable = BallOnDeath.Instance.IsEnabled;
        Config.BallOnDeath.LaunchForce = BallOnDeath.Instance.Force;
        SaveConfig();
    }

    public override void Enable()
    {
        ServerEvents.Shutdown += UpdateConfig;

        LoadConfig();
        EnableModifiers();
    }

    public override void Disable()
    {
        DisableModifiers();
    }
}
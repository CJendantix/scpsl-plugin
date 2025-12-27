using System;
using LabApi.Events.Handlers;
using LabApi.Features;
using LabApi.Features.Console;
using LabApi.Loader.Features.Plugins;

public class Main : Plugin<Configuration>
{
    public override string Name { get; } = "CJ's Modifiers";
    public override string Description { get; } = "Extensible Modifier Framework for SCP: Secret Laboratory";
    public override string Author { get; } = "CJendantix";
    public override Version Version { get; } = new Version(1, 0, 0, 0);
    public override Version RequiredApiVersion { get; } = new Version(LabApiProperties.CompiledVersion);

    public override void Enable()
    {
        GeneratedModifierLoader.LoadAllModifiers();
        ServerEvents.Shutdown += SaveAllConfigs;

        Config.ApplyAllModifierConfigurations();
    }

    public override void Disable()
    {
        Config.PullAllModifierConfigurations();
        foreach (var m in Modifier.Modifiers)
            m.Disable();
    }

    private void SaveAllConfigs()
    {
        Config.PullAllModifierConfigurations();
        SaveConfig();
    }
}
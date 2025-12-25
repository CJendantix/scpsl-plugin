using System;
using LabApi.Events.Handlers;
using LabApi.Features;
using LabApi.Features.Console;
using LabApi.Loader.Features.Plugins;

public class Main : Plugin<Configuration>
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

    public override void Enable()
    {
        GeneratedModifierLoader.LoadAll();

        ServerEvents.Shutdown += SaveAllConfigs;

        foreach (var m in Modifier.Modifiers)
        {
            Logger.Info("test: " + m.Name);
            m.LoadConfig(Config);
        }
    }

    public override void Disable()
    {
        foreach (var m in Modifier.Modifiers) {
            m.SaveConfig(Config);
            m.Disable();
        }
    }

    private void SaveAllConfigs()
    {
        foreach (var m in Modifier.Modifiers)
            m.SaveConfig(Config);

        SaveConfig();
    }
}
using System.Collections.Generic;
using LabApi.Events.CustomHandlers;
using LabApi.Features.Console;

public abstract class Modifier : CustomEventsHandler
{
    public static List<Modifier> Modifiers { get; } = new List<Modifier>();

    protected Modifier() {
        Modifiers.Add(this);
    }

    public bool IsEnabled { get; protected set; } = false;

    public abstract string Name { get; }

    public abstract void LoadConfig(Configuration config);
    public abstract void SaveConfig(Configuration config);

    public void Enable()
    {
        Logger.Info("Enabling Modifier " + this.Name);
        CustomHandlersManager.RegisterEventsHandler(this);
        IsEnabled = true;
    }

    public void Disable()
    {
        Logger.Info("Disabling Modifier " + this.Name);
        CustomHandlersManager.UnregisterEventsHandler(this);
        IsEnabled = false;
    }
}

using System.Collections.Generic;
using LabApi.Events.CustomHandlers;
using LabApi.Features.Console;

public abstract class Modifier : CustomEventsHandler
{
    public bool IsEnabled { get; protected set; } = false;

    public abstract string Name { get; }

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

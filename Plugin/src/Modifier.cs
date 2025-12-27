using System.Collections.Generic;
using LabApi.Events.CustomHandlers;
using LabApi.Features.Console;

namespace modifiers
{

    public abstract class Modifier : CustomEventsHandler
    {
        public static List<Modifier> Modifiers { get; } = new List<Modifier>();

        protected Modifier()
        {
            Modifiers.Add(this);
        }

        public bool IsEnabled { get; private set; }

        public abstract string Name { get; }

        public void Enable()
        {
            if (IsEnabled)
                return;

            Logger.Info("Enabling Modifier " + Name);
            CustomHandlersManager.RegisterEventsHandler(this);
            IsEnabled = true;
            OnEnabled();
        }

        public void Disable()
        {
            if (!IsEnabled)
                return;

            Logger.Info("Disabling Modifier " + Name);
            CustomHandlersManager.UnregisterEventsHandler(this);
            IsEnabled = false;
            OnDisabled();
        }

        protected virtual void OnEnabled() { }
        protected virtual void OnDisabled() { }
    }

}